using System;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing.Design;
using Alteration.Update;

namespace Alteration.Settings
{

    public abstract class ISettings
    {

        public class EncryptedAttribute : System.Attribute
        {
            public readonly bool Encrypted = true;

            public EncryptedAttribute() { }
            public EncryptedAttribute(bool encrypted) { Encrypted = encrypted; }
        }

        public virtual void Read(BinaryReader br)
        {
            //For each property lets get the value and write it
            foreach (PropertyInfo pi in this.GetType().GetProperties(
                BindingFlags.Public | BindingFlags.Instance))
            {
                object[] attributes = pi.GetCustomAttributes(typeof(EncryptedAttribute), false);
                if (attributes.Length > 0)
                    pi.SetValue(this, ReadValue(br, pi.PropertyType,
                        ((EncryptedAttribute)attributes[0]).Encrypted), null);
                else
                    pi.SetValue(this, ReadValue(br, pi.PropertyType, false), null);
            }
        }

        public virtual void Write(BinaryWriter bw)
        {
            //For each property lets get the value and write it
            foreach (PropertyInfo pi in this.GetType().GetProperties(
                BindingFlags.Public | BindingFlags.Instance))
            {
                object[] attributes = pi.GetCustomAttributes(typeof(EncryptedAttribute), false);
                if (attributes.Length > 0)
                    WriteValue(bw, pi.GetValue(this, null), ((EncryptedAttribute)attributes[0]).Encrypted);
                else
                    WriteValue(bw, pi.GetValue(this, null), false);
            }
        }

        private object ReadValue(BinaryReader br, Type type, bool encrypted)
        {
            if (type == typeof(int))
                return br.ReadInt32();
            else if (type == typeof(double))
                return br.ReadDouble();
            else if (type == typeof(bool))
                return br.ReadBoolean();
            else if (type == typeof(string))
            {
                if (!encrypted)
                    return br.ReadString();

                byte size = br.ReadByte();
                byte[] data = RC4Engine.Decrypt(br.ReadBytes(size));
                return Encoding.ASCII.GetString(data);
            }
            else if (type == typeof(byte[]))
                return br.ReadBytes(br.ReadInt32());
            else if (type.IsEnum)
            {
                Type t = Enum.GetUnderlyingType(type);
                if (t == typeof(byte))
                    return br.ReadByte();
                else if (t == typeof(short))
                    return br.ReadInt16();
                else if (t == typeof(int))
                    return br.ReadInt32();
                else if (t == typeof(long))
                    return br.ReadInt64();
                else
                {
                    MessageBox.Show("Unable to read this enum type! Type: " + t.ToString());
                }
            }
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ISettingList<>))
            {
                object settingList = Activator.CreateInstance(type, new object[] { });
                settingList.GetType().GetMethod("Read").Invoke(settingList, new object[] { br });
                return settingList;
            }
            else
                MessageBox.Show("Cannot read type " + type.ToString());

            return null;
        }

        private void WriteValue(BinaryWriter bw, object value, bool encrypted)
        {
            Type type = value.GetType();

            if (type == typeof(int))
                bw.Write((int)value);
            else if (type == typeof(double))
                bw.Write((double)value);
            else if (type == typeof(bool))
                bw.Write((bool)value);
            else if (type == typeof(string))
            {
                if (!encrypted)
                {
                    bw.Write((string)value);
                    return;
                }

                byte[] data = Encoding.ASCII.GetBytes((string)value);
                bw.Write((byte)data.Length);
                bw.Write(RC4Engine.Encrypt(data));
            }
            else if (type == typeof(byte[]))
            {
                bw.Write((int)((byte[])value).Length);
                bw.Write((byte[])value);
            }
            else if (type.IsEnum)
            {
                Type t = Enum.GetUnderlyingType(type);
                if (t == typeof(byte))
                    bw.Write((byte)value);
                else if (t == typeof(short))
                    bw.Write((short)value);
                else if (t == typeof(int))
                    bw.Write((int)value);
                else if (t == typeof(long))
                    bw.Write((long)value);
                else
                {
                    MessageBox.Show("Unable to write this enum type! Type: " + t.ToString());
                }
            }
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ISettingList<>))
            {
                MethodInfo mi = type.GetMethod("Write");
                mi.Invoke(value, new object[] { bw });
            }
            else
                MessageBox.Show("Cannot write type " + type);
        }
    }

    public class ISettingList<T> : List<T> where T : ISettings
    {
        public void Read(BinaryReader br)
        {
            int count = br.ReadInt32();
            for (int x = 0; x < count; x++)
            {
                T setting = (T)Activator.CreateInstance(typeof(T));
                setting.Read(br);
                Add(setting);
            }
        }

        public void Write(BinaryWriter bw)
        {
            bw.Write(this.Count);
            for (int x = 0; x < this.Count; x++)
                this[x].Write(bw);
        }
    }

    public class Settings : ISettings
    {

        //This is our settings version we will increase if we add more settings
        public const double SettingsVersion = 0.01;

        private string settingsAuthor;
        [Category("General Settings"),
        Description("User that created this settings file"),
        Browsable(false)]
        public string SettingsAuthor
        {
            get { return settingsAuthor; }
            set { settingsAuthor = value; }
        }

        #region General Settings

        private bool _showWelcomeForm = true;
        [Category("General Settings"),
        Description("If set to true the Welcome form will be shown on startup")]
        public bool ShowWelcomeForm
        {
            get { return _showWelcomeForm; }
            set { _showWelcomeForm = value; }
        }

        #endregion

        #region XDK Settings

        private string _xdkname = "";
        [Category("XDK Settings"),
        Description("Name of Xbox")]
        public string XDKName
        {
            get { return _xdkname; }
            set { _xdkname = value; }
        }

        #endregion

        #region Meta Editor Settings

        private bool _showinvisibles = false;
        [Category("Meta Editor Settings"),
        Description("Show invisibles?")]
        public bool ShowInvisibles
        {
            get { return _showinvisibles; }
            set { _showinvisibles = value; }
        }

        public enum LastGridViews : byte
        {
            Structure = 0x00,
            Ident = 0x01,
            Strings = 0x02
        }
        private LastGridViews _lastgridview = LastGridViews.Structure;
        [Category("Meta Editor Settings"),
       Description("Last grid view used"),
      Encrypted, BrowsableAttribute(false)]
        public LastGridViews LastGridView
        {
            get { return _lastgridview; }
            set { _lastgridview = value; }
        }

        #endregion

        #region Screenshot Settings

        private bool _adjustGamma = true;
        [Category("Screenshot Settings"),
        Description("Should we adjust the gamma?")]
        public bool AdjustGamma
        {
            get { return _adjustGamma; }
            set { _adjustGamma = value; }
        }

        private double _gammaValue = 0.5;
        [Category("Screenshot Settings"),
        Description("Gamma value to adjust by")]
        public double GammaValue
        {
            get { return _gammaValue; }
            set { _gammaValue = value; }
        }

        private bool _resizeScreenshots = false;
        [Category("Screenshot Settings"),
        Description("Should we resize our screenshots?")]
        public bool ResizeScreenshots
        {
            get { return _resizeScreenshots; }
            set { _resizeScreenshots = value; }
        }

        private int _screenshotHeight = 640;
        [Category("Screenshot Settings"),
        Description("If Resize Screenshots is set to true this will be the new height")]
        public int ScreenshotHeight
        {
            get { return _screenshotHeight; }
            set { _screenshotHeight = value; }
        }

        private int _screenshotWidth = 1152;
        [Category("Screenshot Settings"),
        Description("If Resize Screenshots is set to true this will be the new width")]
        public int ScreenshotWidth
        {
            get { return _screenshotWidth; }
            set { _screenshotWidth = value; }
        }

        #endregion

    }
    public static class AppSettings
    {

        //Our Settings
        private static Settings settings = new Settings();
        public static Settings Settings
        {
            get { return settings; }
        }

        //settings location
        public static string settingsDirectory = "";
        public static string settingsName = "\\AppSettings.dat";

        public static void SaveSettings()
        {
            //Create a settings file
            FileStream fs = new FileStream(settingsDirectory + settingsName,
                FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);

            //Write our settings version
            bw.Write(Settings.SettingsVersion);

            //Set the author of this settings
            settings.SettingsAuthor = SystemInformation.UserName;

            //set our encryption key.. just incase we need anything encrypted
            RC4Engine.BinaryEncryptionKey = Security.Security.GetUserKeyBin();

            //Write our settings
            settings.Write(bw);

            //Close the file
            fs.Close();
        }

        public static void LoadSettings()
        {
            //Check if theres a settings file, if not then write it
            if (!File.Exists(settingsDirectory + settingsName))
                SaveSettings();

            //Open a settings file
            FileStream fs = new FileStream(settingsDirectory + settingsName,
                FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            //Read the settings version and check if it matches
            if (Settings.SettingsVersion != br.ReadDouble())
            {
                //Close the settings and stop
                fs.Close();

                MessageBox.Show("You are using a different version of settings. Your settings will be cleared to prevent problems.",
                    "Different settings Ver.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //Delete the settings file and create a new class and file
                File.Delete(settingsDirectory + settingsName);
                settings = new Settings();
                SaveSettings();

                //Now just return
                return;
            }

            //set our encryption key.. just incase we need anything encrypted
            RC4Engine.BinaryEncryptionKey = Security.Security.GetUserKeyBin();

            //read our settings
            settings.Read(br);

            //close the file
            fs.Close();

            //Find out if we are the author of this settings file
            if (settings.SettingsAuthor != SystemInformation.UserName)
            {
                DialogResult dr = MessageBox.Show("The settings file indicates that you are not user that created this Settings File. Would you like to start with a fresh Settings File?",
                      "Hmmm...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    //Delete the settings file and create a new class and file
                    File.Delete(settingsDirectory + settingsName);
                    settings = new Settings();
                    SaveSettings();
                }
                else
                {
                    //They don't want to create a new one, so lets just save it again
                    //this will change the author to them
                    SaveSettings();
                }
            }
        }

        public static void ClearSettings()
        {
            //Reset the settings data
            settings = new Settings();

            //Now Save them
            SaveSettings();
        }

        public static void DeleteSettings()
        {
            //Check if theres a settings file, then delete it
            if (File.Exists(settingsDirectory + settingsName))
                File.Delete(settingsDirectory + settingsName);
        }

    }

}
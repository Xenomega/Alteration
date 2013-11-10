using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace Alteration.Settings
{
    public abstract class SettingsHandler
    {
        private static string _xdkname;
        public static string XDKName
        {
            get { return _xdkname; }
            set { _xdkname = value; }
        }
        private static bool _showinvisibles;
        public static bool ShowInvisibles
        {
            get { return _showinvisibles; }
            set { _showinvisibles = value; }
        }
        private static LastGridView _lastgridview;
        public static LastGridView Last_Grid_View
        {
            get { return _lastgridview; }
            set { _lastgridview = value; }
        }
        public enum LastGridView
        {
            Structure = 0x00,
            Ident = 0x01
        }
        public static void SaveSettings()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey("Alteration");
            key.SetValue("XDKName", XDKName);
            key.SetValue("ShowInvisibles", ShowInvisibles.ToString());
            key.SetValue("Last_Grid_View", (byte)Last_Grid_View);
            key.Close();
        }
        public static void LoadSettings()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Alteration");
            XDKName = key.GetValue("XDKName").ToString();
            ShowInvisibles = bool.Parse(key.GetValue("ShowInvisibles").ToString());
            Last_Grid_View = (LastGridView)byte.Parse(key.GetValue("Last_Grid_View").ToString());
            key.Close();
        }
        public static bool SettingsExist()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Alteration");
                XDKName = key.GetValue("XDKName").ToString();
                ShowInvisibles = bool.Parse(key.GetValue("ShowInvisibles").ToString());
                Last_Grid_View = (LastGridView)byte.Parse(key.GetValue("Last_Grid_View").ToString());
                key.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}

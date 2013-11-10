using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Alteration.Forms;
using HaloDeveloper;
using Alteration.Halo_3.Plugins;
using Alteration.Settings;
using Alteration.Halo_3.Raw;
using Alteration.Halo_3.Map_Package;
using System.IO;
using Alteration.Halo_3.Map_Package.Package_Classes.Image_Editting;
using System.Drawing.Imaging;
using Alteration.Forms.Dialog;
using Alteration.Update;

namespace Alteration
{
    public partial class Form1 : Form
    {
        bool welcomeUser = true;
        public Form1()
        {

            InitializeComponent();
            //if settings exist
            if (SettingsHandler.SettingsExist())
            {
                //Load the settings
                LoadSettings();
                //Hide the settings panel
                MakeSettingsVisible(false);
            }
            else
            {
                //Show the settings panel
                MakeSettingsVisible(true);
            }
            //Focus on the XDK IP box so the button doesn't look bad.
            this.txtXDKName.Focus();
        }
        private void menuButtonItem1_Activate(object sender, EventArgs e)
        {
            //Initialize our OpenFileDialog
            OpenFileDialog ofd = new OpenFileDialog();
            //Set the filter for Halo 3 Map Files.
            ofd.Filter = "Halo 3 Map Files|*.map";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Initialize our instance of the Halo 3 Map Class.
                HaloMap map = new HaloMap(ofd.FileName);
                //Initialize a new instance of our MDI child form.
                MapForm mapForm = new MapForm(map);
                //Set the mapForm's MDI Parent Container as this form.
                mapForm.MdiParent = this;
                //Show our mapForm.
                mapForm.Show();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //If we are to welcome the user..
            if (welcomeUser)
            {
                //Then initialize our welcome form
                WelcomeForm welcomeForm = new WelcomeForm();
                //Set its MDI parent as this.
                welcomeForm.MdiParent = this;
                //Show it as a dialog
                welcomeForm.Show();
            }
            //Show the form
            Show();
            /*
            //Dispose garbage update, if it exists
            AutoUpdate.CleanBackupFiles();
            //If your not Xenon.7
            if (Security.Security.GetUserKey() != "5011B350BE0B4C9A992F12D1FA3C8DFFBC305563")
            {
                //Check for autoupdates... and if there is one...
                if (AutoUpdate.isUpdate())
                {
                    //Initialize our autoupdate form.
                    AutoUpdateForm autoupdateform = new AutoUpdateForm();
                    //Set it's mdi parent as this
                    autoupdateform.MdiParent = this;
                    //Show it
                    autoupdateform.Show(); autoupdateform.BringToFront();
                    //Do the updates
                    autoupdateform.ApplyUpdate();
                }
            }
            */
            menuScreenshot.Visible = Security.Security.HasDebugExtension;
        }
        private void MakeSettingsVisible(bool visible)
        {
            //If visible
            if (visible)
            {
                //Set the expanded text
                btnCollapse.Text = "-";
                //Resize it
                panelEx1.Size = new Size(221, 1);
                return;
            }
            else
            {
                //Set the collapsed text
                btnCollapse.Text = "+";
                //Collapse it
                this.panelEx1.Size = new Size(btnCollapse.Size.Width, 1);
            }
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.txtXDKName.Focus();
            //If its expanded
            if (btnCollapse.Text == "+")
            {
                MakeSettingsVisible(true);
            }
            else
            {
                MakeSettingsVisible(false);
            }
        }
        private void SaveSettings()
        {
            SettingsHandler.XDKName = txtXDKName.ControlText;
            SettingsHandler.ShowInvisibles = chkShowInvisibles.Checked;
            SettingsHandler.SaveSettings();
        }
        private void LoadSettings()
        {
            //If settings exist..
            if (SettingsHandler.SettingsExist())
            {
                //Load settings values
                SettingsHandler.LoadSettings();
                //Set the text for XDK name
                txtXDKName.ControlText = SettingsHandler.XDKName;
                //Set the checked
                chkShowInvisibles.Checked = SettingsHandler.ShowInvisibles;
            }
        }
        private void buttonItem4_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }
        private void btnLoadSettings_Click(object sender, EventArgs e)
        {
            LoadSettings();
        }
        private void menuButtonItem2_Activate(object sender, EventArgs e)
        {
            //Initialize our instance of the aboutForm
            About aboutForm = new About();
            //Set its mdi parent, which is this form.
            aboutForm.MdiParent = this;
            //Show our form.
            aboutForm.Show();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Instantly exit, kill the process.
            Environment.Exit(0);
        }
        private void menuButtonItem3_Activate(object sender, EventArgs e)
        {
            //Initialize our instance of the map Package creator
            CreateMapPackageForm cmpf = new CreateMapPackageForm();
            //Set its mdi parent as this
            cmpf.MdiParent = this;
            //Show the form
            cmpf.Show();
        }
        private void menuScreenshot_Activate(object sender, EventArgs e)
        {
            //Create our screenshot form.
            ScreenshotForm screenCapForm = new ScreenshotForm();
            //Set it's mdi parent as this
            screenCapForm.MdiParent = this;
            //Show the form
            screenCapForm.Show();
        }
        private void menuBar1_ButtonClick(object sender, TD.SandBar.ToolBarItemEventArgs e)
        {

        }
    }
}
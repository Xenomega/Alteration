using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Alteration.Details;
using Alteration.Forms.Dialog;
using Alteration.Halo_3.Content.Usermap;
using Alteration.Halo_3.Map_Package;
using Alteration.Patches.Memory_Patch.RTH_Data;
using Alteration.Patches.Memory_Patch.RTH_Data.Classes;
using Alteration.Settings;
using Alteration.Update;
using HaloDeveloper.Map;
using HaloDevelopmentExtender;
using Alteration.Forms.Xbox_Debug_Communicator;
using System.Threading;
using Alteration.Halo_3.Plugins;
using Alteration.Forms.Dialog.General;
using Alteration.Halo_3.Game;

namespace Alteration.Forms
{
    public partial class Form1 : Form
    {
        private const bool forceWelcome = false;
        private bool updatesEnabled = true;
        public Form1()
        {
            InitializeComponent();

            //If its a debug build lets output that so we know ;)
#if DEBUG
            Text += " - DEBUG";
#endif

            //Load the settings
            //LoadSettings(); REMOVED FOR NOW SINCE WE HAVE A SEPERATE FORM!
            //Hide the settings panel
            //MakeSettingsVisible(false);
            //Focus on the XDK IP box so the button doesn't look bad.
            //txtXDKName.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //If we are to welcome the user..
            if (AppSettings.Settings.ShowWelcomeForm || forceWelcome)
            {
                //Then initialize our welcome form
                WelcomeForm welcomeForm = new WelcomeForm();
                welcomeForm.MdiParent = this;
                welcomeForm.Show();
            }

            //Show the form
            Show();

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
            //Set the collapsed text
            btnCollapse.Text = "+";
            //Collapse it
            panelEx1.Size = new Size(btnCollapse.Size.Width, 1);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            //Focus on our XDK name box.
            txtXDKName.Focus();
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
            // Set our XDK Name
            AppSettings.Settings.XDKName = txtXDKName.ControlText;
            // Set our show invisibles value
            AppSettings.Settings.ShowInvisibles = chkShowInvisibles.Checked;
            // Get our gamma
            double gammaVal;
            if (!double.TryParse(txtGamma.ControlText, out gammaVal))
                gammaVal = 0.5;
            //Set our gamma
            AppSettings.Settings.GammaValue = gammaVal;
            AppSettings.Settings.AdjustGamma = chkAdjustGamma.Checked;
            // Save our settings.
            AppSettings.SaveSettings();
        }

        private void LoadSettings()
        {
            // Set the text for XDK name
            txtXDKName.ControlText = AppSettings.Settings.XDKName;
            // Set the checked
            chkShowInvisibles.Checked = AppSettings.Settings.ShowInvisibles;
            // Set our gamma
            txtGamma.ControlText = AppSettings.Settings.GammaValue.ToString();
            chkAdjustGamma.Checked = AppSettings.Settings.AdjustGamma;
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void btnLoadSettings_Click(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Instantly exit, kill the process.
            Environment.Exit(0);
        }

        private void openMapToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Initialize our instance of the aboutForm
            About aboutForm = new About();
            //Set its mdi parent, which is this form.
            aboutForm.MdiParent = this;
            //Show our form.
            aboutForm.Show();
        }

        private void createToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new Patches.Memory_Patch.RTH_Data.Forms.Create.CreateRTHDataPatchForm().ShowDialog();
        }

        private void applyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Patches.Memory_Patch.RTH_Data.Forms.Apply.ApplyRTHDataPatchForm().ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new SettingsForm().ShowDialog();
        }

        private void applyDataToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Initialize our Open File Dialog
            OpenFileDialog ofd = new OpenFileDialog();
            //Set it's filter
            ofd.Filter = "RTH Data File(.rthdat)|*.rthdat";
            //If the result of the shown dialog is ok
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Apply our data
                RTHDataHandler.PokeRTHData(new RTHData(new FileStream(ofd.FileName, FileMode.Open)));
                //Show our messagebox
                MessageBox.Show("Done.");
            }
        }

        private void screenshotToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Create our screenshot form.
            ScreenshotForm screenCapForm = new ScreenshotForm();
            //Set it's mdi parent as this
            screenCapForm.MdiParent = this;
            //Show the form
            screenCapForm.Show();
        }

        private void usermapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Initialize our open usermap form
            OpenUserMapForm openUserMap = new OpenUserMapForm();
            //Set its mdiparent
            openUserMap.MdiParent = this;
            //Show the form
            openUserMap.Show();
        }

        private void createToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //Initialize our instance of the map Package creator
            CreateMapPackageForm cmpf = new CreateMapPackageForm();
            //Set its mdi parent as this
            cmpf.MdiParent = this;
            //Show the form
            cmpf.Show();
        }

        private void ChangeHalo3FillMode()
        {
            //If the user didnt set the XDK name.
            if (AppSettings.Settings.XDKName == "")
            {
                //Show our error.
                MessageBox.Show(
                    "The Xbox Development Kit could not be connected to because it's means of connection were not set.");

                //Stop processing code in this stub.
                return;
            }

            //Initialize our Xbox Debug Communicator, with our XDK name/IP.
            XboxDebugCommunicator xdc = new XboxDebugCommunicator(AppSettings.Settings.XDKName);

            //Connect to our Xbox Debug Communicator.
            xdc.Connect();

            //Create an Xbox Memory Stream.
            XboxMemoryStream xbms = xdc.ReturnXboxMemoryStream();

            //Create an endian IO for our Memory Stream.
            EndianIO IO = new EndianIO(xbms, EndianType.BigEndian);

            //Open our IO.
            IO.Open();

            //Go to the address of the fillmode.
            IO.SeekTo(0x82462F74);

            //If our solid item is checked.
            if (menuButtonItem18.Checked)
                //Write our solid value.
                IO.Out.Write(0x38800000);

            //Otherwise if our Point item is checked.
            else if (menuButtonItem19.Checked)
                //Write our point value.
                IO.Out.Write(0x38800001);

            //Otherwise if our Wireframe item is checked.
            else if (menuButtonItem20.Checked)
                //Write our wireframe value.
                IO.Out.Write(0x38800025);

            //Close our IO.
            IO.Close();

            //Disconnect from our Xbox.
            xdc.Disconnect();
        }
        private void ChangeHaloODSTFillMode()
        {
            //If the user didnt set the XDK name.
            if (AppSettings.Settings.XDKName == "")
            {
                //Show our error.
                MessageBox.Show(
                    "The Xbox Development Kit could not be connected to because it's means of connection were not set.");

                //Stop processing code in this stub.
                return;
            }

            //Initialize our Xbox Debug Communicator, with our XDK name/IP.
            XboxDebugCommunicator xdc = new XboxDebugCommunicator(AppSettings.Settings.XDKName);

            //Connect to our Xbox Debug Communicator.
            xdc.Connect();

            //Create an Xbox Memory Stream.
            XboxMemoryStream xbms = xdc.ReturnXboxMemoryStream();

            //Create an endian IO for our Memory Stream.
            EndianIO IO = new EndianIO(xbms, EndianType.BigEndian);

            //Open our IO.
            IO.Open();

            //Go to the address of the fillmode.
            IO.SeekTo(0x821A8C60);

            //If our solid item is checked.
            if (menuButtonItem32.Checked)
                //Write our solid value.
                IO.Out.Write(0x38800000);

            //Otherwise if our Point item is checked.
            else if (menuButtonItem33.Checked)
                //Write our point value.
                IO.Out.Write(0x38800001);

            //Otherwise if our Wireframe item is checked.
            else if (menuButtonItem34.Checked)
                //Write our wireframe value.
                IO.Out.Write(0x38800025);

            //Close our IO.
            IO.Close();

            //Disconnect from our Xbox.
            xdc.Disconnect();
        }

        private void menuButtonItem18_Activate(object sender, EventArgs e)
        {
            //Check the appropriate items.
            menuButtonItem18.Checked = true;
            menuButtonItem19.Checked = false;
            menuButtonItem20.Checked = false;

            //Change the fill mode.
            ChangeHalo3FillMode();
        }

        private void menuButtonItem19_Activate(object sender, EventArgs e)
        {
            //Check the appropriate items.
            menuButtonItem19.Checked = true;
            menuButtonItem18.Checked = false;
            menuButtonItem20.Checked = false;

            //Change the fill mode.
            ChangeHalo3FillMode();
        }

        private void menuButtonItem20_Activate(object sender, EventArgs e)
        {
            //Check the appropriate items.
            menuButtonItem20.Checked = true;
            menuButtonItem18.Checked = false;
            menuButtonItem19.Checked = false;

            //Change the fill mode.
            ChangeHalo3FillMode();
        }

        private void menuButtonItem16_Activate(object sender, EventArgs e)
        {
            //Inverse our check.
            menuButtonItem16.Checked = !menuButtonItem16.Checked;

            //Change our debug hud text.

            //If the user didnt set the XDK name.
            if (AppSettings.Settings.XDKName == "")
            {
                //Show our error.
                MessageBox.Show(
                    "The Xbox Development Kit could not be connected to because it's means of connection were not set.");

                //Stop processing code in this stub.
                return;
            }

            //Initialize our Xbox Debug Communicator, with our XDK name/IP.
            XboxDebugCommunicator xdc = new XboxDebugCommunicator(AppSettings.Settings.XDKName);

            //Connect to our Xbox Debug Communicator.
            xdc.Connect();

            //Create an Xbox Memory Stream.
            XboxMemoryStream xbms = xdc.ReturnXboxMemoryStream();

            //Create an endian IO for our Memory Stream.
            EndianIO IO = new EndianIO(xbms, EndianType.BigEndian);

            //Open our IO.
            IO.Open();

            //Goto the Address of the jump to the debug text.
            IO.SeekTo(0x821A04B8);

            //If our print cam info item is checked..
            if (menuButtonItem16.Checked)
                //Write our ASM value indicating a no operation.
                IO.Out.Write(0x60000000);//Nop

            //Otherwise
            else
                //Write our jump offset.
                IO.Out.Write(0x419A01DC);

            //Close our IO.
            IO.Close();

            //Disconnect from our xbox.
            xdc.Disconnect();
        }

        private void labelItem1_Click(object sender, EventArgs e)
        {

        }

        private void menuButtonItem21_Activate(object sender, EventArgs e)
        {
            //Initialize our Xbox Debug Communicator
            XboxDebugCommunicatorForm xdcf = new XboxDebugCommunicatorForm();

            //Set our parent
            xdcf.MdiParent = this;

            //Show our form.
            xdcf.Show();
        }

        private void menuButtonItem22_Activate(object sender, EventArgs e)
        {
            //Initialize our MapProtectorForm
            MapProtectorForm map_protector_form = new MapProtectorForm();

            //Set it's mdi parent as this.
            map_protector_form.MdiParent = this;

            //Show it
            map_protector_form.Show();
        }

        private void menuButtonItem23_Activate(object sender, EventArgs e)
        {
            //Initialize our PluginGeneratorForm
            PluginGeneratorForm plugin_generator_form = new PluginGeneratorForm();

            //Set it's mdi parent
            plugin_generator_form.MdiParent = this;

            //Show the form
            plugin_generator_form.Show();
        }

        private void menuButtonItem25_Activate(object sender, EventArgs e)
        {
            //Create our PluginRenamerForm
            PluginRenamerForm prf = new PluginRenamerForm();

            //Set it's mdi parent as this
            prf.MdiParent = this;

            //Show our form
            prf.Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }





        private void menuButtonItem28_Activate_1(object sender, EventArgs e)
        {
            //Inverse our check.
            menuButtonItem28.Checked = !menuButtonItem28.Checked;

            //Change our debug hud text.
            
             //If the user didnt set the XDK name.
            if (AppSettings.Settings.XDKName == "")
            {
                //Show our error.
                MessageBox.Show(
                    "The Xbox Development Kit could not be connected to because it's means of connection were not set.");

                //Stop processing code in this stub.
                return;
            }

            //Initialize our Xbox Debug Communicator, with our XDK name/IP.
            XboxDebugCommunicator xdc = new XboxDebugCommunicator(AppSettings.Settings.XDKName);

            //Connect to our Xbox Debug Communicator.
            xdc.Connect();

            //Create an Xbox Memory Stream.
            XboxMemoryStream xbms = xdc.ReturnXboxMemoryStream();

            //Create an endian IO for our Memory Stream.
            EndianIO IO = new EndianIO(xbms, EndianType.BigEndian);

            //Open our IO.
            IO.Open();

            //Goto the Address of the jump to the debug text.
            IO.SeekTo(0x821978B0);

            //If our print cam info item is checked..
            if (menuButtonItem28.Checked)
                //Write our ASM value indicating a no operation.
                IO.Out.Write(0x60000000);//Nop

            //Otherwise
            else
                //Write our jump offset.
                IO.Out.Write(0x419A01D8);

            //Close our IO.
            IO.Close();

            //Disconnect from our xbox.
            xdc.Disconnect();
        }

        private void menuButtonItem32_Activate(object sender, EventArgs e)
        {
            //Set our checks.
            menuButtonItem32.Checked = true;
            menuButtonItem33.Checked = false;
            menuButtonItem34.Checked = false;
            //Change our fillmode.
            ChangeHaloODSTFillMode();
        }

        private void menuButtonItem33_Activate(object sender, EventArgs e)
        {
            //Set our checks.
            menuButtonItem32.Checked = false;
            menuButtonItem33.Checked = true;
            menuButtonItem34.Checked = false;
            //Change our fillmode.
            ChangeHaloODSTFillMode();
        }

        private void menuButtonItem34_Activate(object sender, EventArgs e)
        {
            //Set our checks.
            menuButtonItem32.Checked = false;
            menuButtonItem33.Checked = false;
            menuButtonItem34.Checked = true;
            //Change our fillmode.
            ChangeHaloODSTFillMode();
        }

    }
}
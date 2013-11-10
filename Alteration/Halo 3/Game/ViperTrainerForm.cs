using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HaloDevelopmentExtender;
using Alteration.Settings;

namespace Alteration.Halo_3.Game
{
    public partial class ViperTrainerForm : Form
    {
        public ViperTrainerForm()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            //If the XDK Name isn't blank
            if (AppSettings.Settings.XDKName != "")
            {
                //Initialize our Xbox Debug Communicator, with our Xbox Name
                XboxDebugCommunicator Xbox_Debug_Communicator = new XboxDebugCommunicator(AppSettings.Settings.XDKName);
                //Connect
                Xbox_Debug_Communicator.Connect();

                //Get our memory stream
                XboxMemoryStream xbms = Xbox_Debug_Communicator.ReturnXboxMemoryStream();

                //Create our IO
                EndianIO IO = new EndianIO(xbms, EndianType.BigEndian);

                //Open our IO
                IO.Open();

                //If we are to remove soft barriers
                if (chkRemoveSoftBarriers.Checked)
                {

                }

                //Close our IO
                IO.Close();

                //Disconnect
                Xbox_Debug_Communicator.Disconnect();
                //Null our communicator
                Xbox_Debug_Communicator = null;


            }
            else
            {
                MessageBox.Show("No XDK Name is set. Please set it in settings before continuing.");
            }
        }
    }
}
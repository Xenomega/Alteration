using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using HaloDeveloper.Map;
using Alteration.Halo_3.Map_File.Building;

namespace Alteration.Forms.Dialog
{
    public partial class MapProtectorForm : Form
    {
        public MapProtectorForm()
        {
            InitializeComponent();
        }


        private void btnFindUnprotected_Click(object sender, EventArgs e)
        {
            //Create our openfiledialog
            OpenFileDialog ofd = new OpenFileDialog();
            //Set our filter
            ofd.Filter = "Halo 3 Map Files(.map)|*.map";

            //If our user finds a file
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Set our textbox's text as our found file
                txtUnprotectedLocation.Text = ofd.FileName;
            }
        }

        private void btnFindProtected_Click(object sender, EventArgs e)
        {
            //Create our savefiledialog
            SaveFileDialog sfd = new SaveFileDialog();
            //Set our filter
            sfd.Filter = "Halo 3 Map Files(.map)|*.map";

            //If our user finds a file
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //Set our textbox's text as our save file location.
                txtProtectedLocation.Text = sfd.FileName;
            }
        }

        private void btnStartProtect_Click(object sender, EventArgs e)
        {
            //Check our fields
            if(txtProtectedLocation.Text != "" && txtUnprotectedLocation.Text != "")
                if (txtProtectedLocation.Text == txtUnprotectedLocation.Text)
                {
                    //Show our error Messagebox
                    MessageBox.Show("You cannot save the protected map in the path of the original map. Please choose somewhere else to save to.");
                }
                else
                {
                    //Let's protect our map..

                    //Get our idle button text
                    string idleButtonText = btnStartProtect.Text;

                    //Disable the button
                    btnStartProtect.Enabled = false;

                    //Set our status as copying the file
                    btnStartProtect.Text = "Copying Map file to save destination..";
                    Application.DoEvents();
                    //Start copying
                    if (File.Exists(txtProtectedLocation.Text))
                        File.Delete(txtProtectedLocation.Text);
                    File.Copy(txtUnprotectedLocation.Text, txtProtectedLocation.Text);


                    //Set our status as loading the map.
                    btnStartProtect.Text = "Loading map..";
                    Application.DoEvents();
                    //Let's load that map.
                    HaloMap map = new HaloMap(txtProtectedLocation.Text);



                    //Set our status as protecting the map
                    btnStartProtect.Text = "Protecting map..";
                    Application.DoEvents();
                    //Let's protect our map.
                    MapBuilder map_functions = new MapBuilder(map);
                    map_functions.ProtectMap();

                    //Enable our button
                    btnStartProtect.Enabled = true;

                    //Set our buttons text back as the original text
                    btnStartProtect.Text = idleButtonText;
                }
        }


    }
}
using System;
using System.Windows.Forms;
using Alteration.Settings;
using HaloDeveloper.Locale;
using HaloDeveloper.Map;
using HaloDeveloper.IO;
using Alteration.Patches.File_Patch.Classes;

namespace Alteration.Patches.Memory_Patch.RTH_Data.Forms.Create
{
    public partial class CreateRTHDataPatchForm : Form
    {
        public CreateRTHDataPatchForm()
        {
            InitializeComponent();
            //Assign the default author name
            txtAuthor.Text = Alteration.Details.AlterationDetails.Username;
        }

        private void panelEx1_Click(object sender, EventArgs e)
        {
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            //Initialize a SaveFileDialog
            SaveFileDialog sfd = new SaveFileDialog();
            //Set the filter
            sfd.Filter = "Patch Data File(.patchdat)|*.patchdat";
            //If our result is OK
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //Assign our textbox's path
                txtSaveRTHData.Text = sfd.FileName;
            }
        }

        private void btnOriginalMap_Click(object sender, EventArgs e)
        {
            //Initialize our OpenFileDialog
            OpenFileDialog ofd = new OpenFileDialog();
            //Set our filter
            //ofd.Filter = "Halo 3 Map Files (.map)|*.map";
            //If the result of the shown dialog is ok
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Set our textbox's path
                txtOriginalMap.Text = ofd.FileName;
            }
        }

        private void btnModdedMap_Click(object sender, EventArgs e)
        {
            //Initialize our OpenFileDialog
            OpenFileDialog ofd = new OpenFileDialog();
            //Set our filter
            //ofd.Filter = "Halo 3 Map Files (.map)|*.map";
            //If the result of the shown dialog is ok
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Set our textbox's path
                txtModdedMap.Text = ofd.FileName;
            }
        }

        private void btnCreateRTHData_Click(object sender, EventArgs e)
        {
            //If one of our fields are empty..
            if (txtDescription.Text == "" | txtModdedMap.Text == "" | txtOriginalMap.Text == "" |
                txtSaveRTHData.Text == "")
            {
                //Show our error messagebox
                MessageBox.Show(
                    "One of the following fields were left empty and must be created before you can continue.");
                //Stop processing code in this stub
                return;
            }
            //Otherwise...

            DateTime start = DateTime.Now;

            //Read our Original Map
            HaloMap origMap = new HaloMap(txtOriginalMap.Text);
            //Read our Modified Map
            //HaloMap modMap = new HaloMap(txtModdedMap.Text);

            TimeSpan end = DateTime.Now.Subtract(start);
            string outString = end.ToString() + ", ";
            start = DateTime.Now;

            //Create our patch
            AlteredPatch patch = new AlteredPatch();
            patch.CreatePatch(origMap, txtModdedMap.Text, txtSaveRTHData.Text,txtAuthor.Text,
                txtDescription.Text);

            end = DateTime.Now.Subtract(start);
            outString += end.ToString();

            //Close our maps
            //origMap.CloseIO();
            //modMap.CloseIO();

            MessageBox.Show("Done! " + outString);

        }

        private void buttonX1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
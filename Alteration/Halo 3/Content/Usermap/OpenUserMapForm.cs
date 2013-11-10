using System;
using System.Windows.Forms;
using HaloDeveloper.Map;

namespace Alteration.Halo_3.Content.Usermap
{
    public partial class OpenUserMapForm : Form
    {
        public OpenUserMapForm()
        {
            InitializeComponent();
        }

        private void panelEx1_Click(object sender, EventArgs e)
        {
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            //If the text fields are not empty
            if (txtOpenMapFile.Text != "" && txtOpenUsermap.Text != "")
            {
                //Initialize a new UserMapForm
                UserMapForm userMapForm = new UserMapForm(new HaloUserMap(txtOpenUsermap.Text),
                                                          new HaloMap(txtOpenMapFile.Text));
                //Set its mdiparent to be the same as this form's mdi parent
                userMapForm.MdiParent = MdiParent;
                //Show the form
                userMapForm.Show();
                //Close this dialog
                Close();
            }
        }

        private void btnOpenUsermap_Click(object sender, EventArgs e)
        {
            //Initialize a new OpenFileDialog
            OpenFileDialog ofd = new OpenFileDialog();
            //If the result of us showing the dialog is the user clicking OK(meaning he/she selected a file..)
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Set the text for the textBox
                txtOpenUsermap.Text = ofd.FileName;
            }
        }

        private void btnOpenMapFile_Click(object sender, EventArgs e)
        {
            //If the usermap was not selected...
            if (txtOpenUsermap.Text == "")
            {
                //Tell them to select it first
                MessageBox.Show("You must open a sandbox.map or a usermap Container file before opening the Map File.");
                //Stop processing code.
                return;
            }

            //Load our usermap
            HaloUserMap usermap = new HaloUserMap(txtOpenUsermap.Text);
            //Get our filter
            string filter = "Halo 3 Map File|*.map";
            //Get our map ID
            MapIdentifier.MapID mapID = (MapIdentifier.MapID) usermap.Map_Variant_Header.Map_ID;
            //if the map ID isn't "other"
            if (mapID != MapIdentifier.MapID.Other)
            {
                //Set our filter for this usermap.
                filter = mapID.ToString().ToLower() + ".map|*" + mapID.ToString().ToLower() + ".map";
            }

            //Initialize a new OpenFileDialog
            OpenFileDialog ofd = new OpenFileDialog();
            //Set the filter
            ofd.Filter = filter;
            //If the result of us showing the dialog is the user clicking OK(meaning he/she selected a file..)
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Set the text for the textBox
                txtOpenMapFile.Text = ofd.FileName;
            }
        }
    }
}
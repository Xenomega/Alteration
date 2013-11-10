using System;
using System.Drawing;
using System.Windows.Forms;

namespace Alteration.Halo_3.Map_Package.Stages
{
    public partial class CMP_Locate : UserControl
    {
        private CreateMapPackageForm _mappackageform;

        public CMP_Locate(CreateMapPackageForm mappackageform)
        {
            InitializeComponent();
            //Set our map package form
            MapPackageForm = mappackageform;
        }

        public CreateMapPackageForm MapPackageForm
        {
            get { return _mappackageform; }
            set { _mappackageform = value; }
        }

        private void btnOpenMap_Click(object sender, EventArgs e)
        {
            //Initialize our openfiledialog
            OpenFileDialog ofd = new OpenFileDialog();
            //Set our filter
            ofd.Filter = "Halo 3 Map Files|*.map";
            //If a file has been selected...
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Set the map location
                txtMapFile.Text = ofd.FileName;
            }
        }

        private void btnLocateImage_Click(object sender, EventArgs e)
        {
            //Initialize our openfiledialog
            OpenFileDialog ofd = new OpenFileDialog();
            //Set our filter
            ofd.Filter = "Image Files|*.jpg*;*.jpeg;*.png;*.tiff;*.jfif;*.bmp;*.gif";
            //If a file has been selected...
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Set the image location
                txtImageFile.Text = ofd.FileName;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //Clear the controls.
            MapPackageForm.Controls.Clear();
            //Initialize a Welcome control.
            CMP_Welcome cmp_welcome = new CMP_Welcome(MapPackageForm);
            //Add it to the map package form
            MapPackageForm.Controls.Add(cmp_welcome);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (txtMapFile.Text == "" | txtImageFile.Text == "")
            {
                MessageBox.Show("One of the fields in this step were not filled out.");
                return;
            }
            //Set the parent form's values
            MapPackageForm.MapLocation = txtMapFile.Text;
            MapPackageForm.Image_File = Image.FromFile(txtImageFile.Text);
            //Clear the controls.
            MapPackageForm.Controls.Clear();
            //Initialize a Welcome control.
            CMP_MapInfo cmp_mapinfo = new CMP_MapInfo(MapPackageForm);
            //Add it to the map package form
            MapPackageForm.Controls.Add(cmp_mapinfo);
        }
    }
}
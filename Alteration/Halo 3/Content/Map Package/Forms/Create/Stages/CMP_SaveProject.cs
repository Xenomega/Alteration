using System;
using System.Windows.Forms;

namespace Alteration.Halo_3.Map_Package.Stages
{
    public partial class CMP_SaveProject : UserControl
    {
        private CreateMapPackageForm _mappackageform;

        public CMP_SaveProject(CreateMapPackageForm mappackageform)
        {
            InitializeComponent();
            //Set our map package form
            MapPackageForm = mappackageform;
            //Set our map id
            int mapid = (new Random().Next(1, 9) * 100) + (new Random().Next(1, 9) * 10);
            txtMapID.Text = mapid.ToString();
        }

        public CreateMapPackageForm MapPackageForm
        {
            get { return _mappackageform; }
            set { _mappackageform = value; }
        }

        private void btnSaveProjDir_Click(object sender, EventArgs e)
        {
            //Initialize our openfiledialog
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            //If a folder has been selected...
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                //Set the image location
                txtSaveProjDir.Text = fbd.SelectedPath + "\\";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSaveProjDir.Text == "" | txtMapID.Text == "")
            {
                MessageBox.Show("One of the fields in this step were not filled out.");
                return;
            }
            try { int.Parse(txtMapID.Text); }
            catch
            {
                MessageBox.Show("An invalid Map Identifier was specified.");
                return;
            }

            MapPackageForm.SaveProject(txtSaveProjDir.Text,int.Parse(txtMapID.Text));
        }
    }
}
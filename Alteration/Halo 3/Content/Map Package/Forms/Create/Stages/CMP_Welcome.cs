using System;
using System.Windows.Forms;

namespace Alteration.Halo_3.Map_Package.Stages
{
    public partial class CMP_Welcome : UserControl
    {
        private CreateMapPackageForm _mappackageform;

        public CMP_Welcome(CreateMapPackageForm mappackageform)
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

        private void CMP_Welcome_Load(object sender, EventArgs e)
        {
        }

        private void btnDisagree_Click(object sender, EventArgs e)
        {
            MapPackageForm.Close();
        }

        private void btnAgree_Click(object sender, EventArgs e)
        {
            //Clear the controls.
            MapPackageForm.Controls.Clear();
            //Initialize a location control to locate a menu image and the map file.
            CMP_Locate cmp_locate = new CMP_Locate(MapPackageForm);
            //Add it to the map package form
            MapPackageForm.Controls.Add(cmp_locate);
        }
    }
}
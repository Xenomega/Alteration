using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Alteration.Patches.Memory_Patch.RTH_Data.Forms.Apply
{
    public partial class ApplyRTHDataPatchForm : Form
    {
        public ApplyRTHDataPatchForm()
        {
            InitializeComponent();
        }

        private void btnOriginalMap_Click(object sender, EventArgs e)
        {
            //Initialize our OpenFileDialog
            OpenFileDialog ofd = new OpenFileDialog();
            //Set our filter
            ofd.Filter = "Patch Data File(.patchdat)|*.patchdat";
            //If the result of the shown dialog is ok
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Set our textbox's path
                txtOriginalMap.Text = ofd.FileName;
                LoadPreview();
            }
        }

        private void btnModdedMap_Click(object sender, EventArgs e)
        {
            //Initialize our OpenFileDialog
            OpenFileDialog ofd = new OpenFileDialog();
            //Set our filter
            ofd.Filter = "Halo 3 Map Files (.map)|*.map";
            //If the result of the shown dialog is ok
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Set our textbox's path
                txtModdedMap.Text = ofd.FileName;
            }
        }

        private void LoadPreview()
        {
            BinaryReader pio = new BinaryReader(new FileStream(txtOriginalMap.Text, FileMode.Open, FileAccess.Read));
            label1.Text = pio.ReadString();
            label2.Text = pio.ReadString();
            pio.Close();
        }

        private void btnCreateRTHData_Click(object sender, EventArgs e)
        {

            //If one of our fields are empty..
            if (txtModdedMap.Text == "" | txtOriginalMap.Text == "")
            {
                //Show our error messagebox
                MessageBox.Show(
                    "One of the following fields were left empty and must be created before you can continue.");
                //Stop processing code in this stub
                return;
            }

            //Open our ios
            BinaryReader pio = new BinaryReader(new FileStream(txtOriginalMap.Text, FileMode.Open, FileAccess.Read));
            BinaryWriter mio = new BinaryWriter(new FileStream(txtModdedMap.Text, FileMode.Open, FileAccess.Write));

            //Read past the info
            pio.ReadString();
            pio.ReadString();

            //Now lets loop and patch
            while (pio.BaseStream.Position != pio.BaseStream.Length)
            {
                //Read the data
                int pos = pio.ReadInt32();
                int size = pio.ReadInt32();

                //Now move and patch
                mio.BaseStream.Position = pos;
                mio.Write(pio.ReadBytes(size));
            }

            pio.Close();
            mio.Close();

            MessageBox.Show("Done!");
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HaloDeveloper.Map;
using System.Drawing.Imaging;
using Alteration.Halo_3.Map_File.Building;

namespace HaloDeveloper.Raw.BitmapRaw
{
    public partial class BitmapViewer : UserControl
    {
        BitmapInfo bi;
        HaloMap map;
        int tagIndex;

        public BitmapViewer()
        {
            InitializeComponent();
        }

        public void LoadBitmapTag(HaloMap Map, int TagIndex)
        {
            map = Map;
            map.RawInformation.ExternalMaps.CreateIOs();
            map.RawInformation.ExternalMaps.OpenIOs();
            tagIndex = TagIndex;

            // Load our bitmap info for this tag
            bi = BitmapFunctions.GetBitmapInfo(map, tagIndex);

            // Now lets clear our list
            listView1.Items.Clear();

            // Now lets add our submaps to it
            for (int x = 0; x < bi.bitmapList.Count; x++)
            {
                string[] infoString = new string[] {
                x.ToString(),
                bi.bitmapList[x].Format.ToString(),
                bi.bitmapList[x].Type.ToString(),
                bi.bitmapList[x].Height.ToString(),
                bi.bitmapList[x].Width.ToString(),
                string.Format("0x{0:X}", bi.bitmapList[x].RawLength)};
                ListViewItem lvi = new ListViewItem(infoString);
                lvi.Tag = x;
                listView1.Items.Add(lvi);
            }

            if (listView1.Items.Count > 0)
            {
                listView1.FocusedItem = listView1.Items[0];
                listView1_SelectedIndexChanged(null, null);
            }
            map.RawInformation.ExternalMaps.CloseIOs();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // First lets get our selected bitmap
            if (listView1.FocusedItem == null)
                return;
            map.RawInformation.ExternalMaps.OpenIOs();
                
            int submapIndex = (int)listView1.FocusedItem.Tag;

    

            // Now lets generate our preview
            try
            {
                pictureBox1.Image = bi.GeneratePreview(submapIndex);
            }
            catch
            {
                Bitmap bitmap = new Bitmap(200, 200);
                Graphics g = Graphics.FromImage(bitmap);
                g.Clear(Color.CornflowerBlue);
                string drawString = "Error Previewing!";
                SizeF size = g.MeasureString(drawString, new Font(FontFamily.GenericSerif, 15f));
                g.DrawString(drawString, new Font(FontFamily.GenericSerif, 15f),
                    Brushes.Black,
                    new PointF(100 - (size.Width / 2), 100 - (size.Height / 2)));
                pictureBox1.Image = bitmap;
            }

            map.RawInformation.ExternalMaps.CloseIOs();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Toggle the color
            if (pictureBox1.BackColor == Color.White)
                pictureBox1.BackColor = Color.Black;
            else
                pictureBox1.BackColor = Color.White;
        }

        private void extractBitmapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // First lets get our selected bitmap
            if (listView1.FocusedItem == null)
                return;

            int submapIndex = (int)listView1.FocusedItem.Tag;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "DDS File (*.dds)|*.dds|Tif Image (*.tif)|*.tif";
            string[] split = map.IndexItems[tagIndex].Name.Split('\\');
            sfd.FileName = split[split.Length - 1];
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                map.RawInformation.ExternalMaps.OpenIOs();
                if (sfd.FilterIndex == 1)
                    bi.Extract(sfd.FileName, submapIndex);
                else
                    bi.GeneratePreview(submapIndex).Save(sfd.FileName, ImageFormat.Tiff);
                map.RawInformation.ExternalMaps.CloseIOs();
                MessageBox.Show("Texture Extracted!");
            }
        }

        private void extractRawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // First lets get our selected bitmap
            if (listView1.FocusedItem == null)
                return;
    
            int submapIndex = (int)listView1.FocusedItem.Tag;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Bin File (*.bin)|*.bin";
            string[] split = map.IndexItems[tagIndex].Name.Split('\\');
            sfd.FileName = split[split.Length - 1];
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                map.RawInformation.ExternalMaps.OpenIOs();
                bi.ExtractRaw(sfd.FileName, submapIndex);
                map.RawInformation.ExternalMaps.CloseIOs();
                MessageBox.Show("Texture Extracted!");
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
   
        }
    }
}
 using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Alteration.Settings;
using HaloDevelopmentExtender;
using Alteration.Halo_3;

namespace Alteration.Forms.Dialog
{
    public partial class ScreenshotForm : Form
    {
        public ScreenshotForm()
        {
            InitializeComponent();
            //Set the text as the time now.
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Text = "Screenshot: " + DateTime.Now;
            // ReSharper restore DoNotCallOverridableMethodsInConstructor

            //Initialize our Xbox Debug Communicator, with our Xbox Name
            XboxDebugCommunicator Xbox_Debug_Communicator = new XboxDebugCommunicator(AppSettings.Settings.XDKName);
            
            //Connect
            Xbox_Debug_Communicator.Connect();

            //Take our screenshot
            Image img = H3Screenshot.TakeScreenshot(Xbox_Debug_Communicator);
            
            //Disconnect
            Xbox_Debug_Communicator.Disconnect();


            //Set the pictureBox image as the provided image
            pictureBox1.Image = H3Screenshot.ResizeImage((Bitmap)img);
            //Set it to stretch the image to fit
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }



        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Initialize our save file dialog
            SaveFileDialog sfd = new SaveFileDialog();
            //Set it's filter as PNG
            sfd.Filter = "PNG Image File (*.png)|*.png" +
                "|JPEG File (*.jpg)|*.jpg" +
                "|BMP File (*.bmp)|*.bmp" +
                "|TIFF File (*.tiff)|*.tiff";
            sfd.FilterIndex = 0; // Make PNG the default

            //If a destination to save to has been selected
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //Now save our image as a PNG
                switch (sfd.FilterIndex)
                {
                    case 0:
                        pictureBox1.Image.Save(sfd.FileName, ImageFormat.Png);
                        break;
                    case 1:
                        pictureBox1.Image.Save(sfd.FileName, ImageFormat.Jpeg);
                        break;
                    case 2:
                        pictureBox1.Image.Save(sfd.FileName, ImageFormat.Bmp);
                        break;
                    case 3:
                        pictureBox1.Image.Save(sfd.FileName, ImageFormat.Tiff);
                        break;
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
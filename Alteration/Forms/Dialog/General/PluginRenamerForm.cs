using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Alteration.Forms.Dialog.General
{
    public partial class PluginRenamerForm : Form
    {
        public PluginRenamerForm()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            //Get each file in our halo 3 plugins folder
            string[] files = Directory.GetFiles(Application.StartupPath + "\\Plugins\\halo 3\\");

            //Loop for each file in our plugins folder
            foreach (string loc_f in files)
            {
                //Get our extension
                string[] tempStr = loc_f.Split('.');
                string ext = tempStr[tempStr.Length - 1];

                //If our extension == ent
                if (ext == "ent")
                {
                    //Rename our file
                    File.Move(loc_f, loc_f.Substring(0, loc_f.Length - 3) + "alt");
                }
            }

            //Get each file in our plugins folder
            files = Directory.GetFiles(Application.StartupPath + "\\Plugins\\odst\\");

            //Loop for each file in our plugins folder
            foreach (string loc_f in files)
            {
                //Get our extension
                string[] tempStr = loc_f.Split('.');
                string ext = tempStr[tempStr.Length - 1];

                //If our extension == ent
                if (ext == "ent")
                {
                    //Rename our file
                    File.Move(loc_f, loc_f.Substring(0, loc_f.Length - 3) + "alt");
                }
            }
            //Show our done message
            MessageBox.Show("Done.");
        }
    }
}
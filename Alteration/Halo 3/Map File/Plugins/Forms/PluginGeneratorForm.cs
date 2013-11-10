using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HaloDeveloper.Map;
using System.Threading;
using System.IO;

namespace Alteration.Halo_3.Plugins
{
    public partial class PluginGeneratorForm : Form
    {
        private PluginLayoutCreator _pluginlayoutcreator;
        /// <summary>
        /// Our plugin layout creator to create plugins with.
        /// </summary>
        public PluginLayoutCreator Plugin_Layout_Creator
        {
            get { return _pluginlayoutcreator; }
            set { _pluginlayoutcreator = value; }
        }

        public PluginGeneratorForm()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnOpenMap_Click(object sender, EventArgs e)
        {
            //Initialize our OpenFileDialog 
            OpenFileDialog ofd = new OpenFileDialog();

            //Set it's filter
            ofd.Filter = "Halo 3 Map Files(.map)|*.map";

            //If we selected a file..
            if (ofd.ShowDialog() == DialogResult.OK)
            {

                //Set our text for our textbox
                txtOpenMap.Text = ofd.FileName;

            }
        }

        private void btnOutputFolder_Click(object sender, EventArgs e)
        {
            //Initialize our FolderBrowserDialog
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            //If we select a folder
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                //Set the folder in our textbox
                txtOutputFolder.Text = fbd.SelectedPath;
            }
        }

        private void btnGeneratePlugins_Click(object sender, EventArgs e)
        {
            //Check our fields.
            if (txtOpenMap.Text == "" | txtOutputFolder.Text == "")
                return;

            //If we aren't mapping all maps..
            if (!checkBoxX1.Checked)
            {
                Plugin_Layout_Creator = new PluginLayoutCreator(new HaloMap(txtOpenMap.Text));
                Plugin_Layout_Creator.GeneratePlugins(txtOutputFolder.Text,true);
                MessageBox.Show("Done.");
            }
            //If we are mapping all maps
            else
            {
                //Get our folder path.
                string[] tempString = txtOpenMap.Text.Split('\\');
                string folderPath = txtOpenMap.Text.Substring(0, txtOpenMap.Text.Length - tempString[tempString.Length - 1].Length);
                //Get our files in this dir
                string[] mapFiles = Directory.GetFiles(folderPath);

                //Create our list of mapheader values
                List<PluginLayoutCreator.MetaHeader_DataBlock> DataBlocksList = new List<PluginLayoutCreator.MetaHeader_DataBlock>();

                //Loop for each file
                foreach (string map in mapFiles)
                {
                    if (map.Contains(".map") && !map.Contains("shared.map"))
                    {
                        try
                        {
                            //Initialize our map class
                            HaloMap Map = new HaloMap(map);

                            //Map our MetaHeaders and add it to our list.
                            PluginLayoutCreator plc = new PluginLayoutCreator(Map);
                            DataBlocksList.AddRange(plc.GetMetaHeaderDataBlocksMapped(true,true));
                        }
                        catch { }
                    }
                }
                //Create our new PluginLayoutCreator
                PluginLayoutCreator plc2 = new PluginLayoutCreator(null);
                plc2.MetaHeader_Data_Blocks = DataBlocksList;
                //Generate our plugins
                plc2.GeneratePlugins(txtOutputFolder.Text,false);
            }
        }
    }
}
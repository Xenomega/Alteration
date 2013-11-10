using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HaloDeveloper.Map;
using HaloDeveloper.Helpers;
using Alteration.Halo_3.Plugins;

namespace Alteration.Halo_3.Map_File.Misc.Dialogs
{
    public partial class SwapTagHeader : Form
    {
        private HaloMap.TagItem _meta;
        /// <summary>
        /// Our meta we are swapping for.
        /// </summary>
        public HaloMap.TagItem Meta
        {
            get { return _meta; }
            set { _meta = value; }
        }

        /// <summary>
        /// This function loads the UI and sets the meta instance.
        /// </summary>
        /// <param name="meta">The meta instance to set and swap.</param>
        public SwapTagHeader(HaloMap.TagItem meta)
        {
            //Initialize our component.
            InitializeComponent();

            //Set our meta instance
            Meta = meta;

            //Loop through our tag hierarchy classes
            foreach (TagHierarchy.TagHClass TClass in Meta.Map.TagHierarchy.TagClasses)
            {

                //If this is our tagclass we're looking for.
                if (TClass.TagClass == Meta.Class)
                {

                    //Loop for each meta
                    foreach (TagHierarchy.TagHName TName in TClass.Tags)
                    {

                        //Add our tagname to the combobox.
                        comboBoxEx1.Items.Add(TName.TagName);

                    }

                }

            }

            //Sort our combobox
            comboBoxEx1.Sorted = true;

            //Select our first item
            comboBoxEx1.SelectedIndex = 0;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            //Get our plugin path.
            string pluginPath = Application.StartupPath + "\\plugins\\" +
                                Meta.Class.Replace(" ", "").Replace("<", "_").Replace(">", "_") +
                                ".alt";

            //Get our header size
            XmlParser xmlparser = new XmlParser();

            //Parse our plugin
            xmlparser.ParsePlugin(pluginPath);

            //Get our headersize
            int HeaderSize = xmlparser.HeaderSize;

            //Get our meta for our tag to swap to
            HaloMap.TagItem Meta_To_Swap_To = Meta.Map.IndexItems[Meta.Map.GetTagIndexByClassAndName(Meta.Class, comboBoxEx1.Text)];

            //Open our map IO
            Meta.Map.OpenIO();

            //Go to our meta to swap to
            Meta.Map.IO.In.BaseStream.Position = Meta_To_Swap_To.Offset;

            //Read our bytes
            byte[] meta_header = Meta.Map.IO.In.ReadBytes(HeaderSize);

            //Go to our meta header for our tag to overwrite
            Meta.Map.IO.Out.BaseStream.Position = Meta.Offset;

            //Write our meta_header.
            Meta.Map.IO.Out.Write(meta_header);

            //Close our map IO
            Meta.Map.CloseIO();

            //Close our dialog.
            this.Close();

            //Show our messagebox
            MessageBox.Show("Done.");
        }
    }
}
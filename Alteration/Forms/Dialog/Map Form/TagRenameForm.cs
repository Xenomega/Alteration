using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HaloDeveloper.Map;
using Alteration.Halo_3.Map_File.Building;

namespace Alteration.Forms.Dialog.Map_Form
{
    public partial class TagRenameForm : Form
    {
        private HaloMap _map;
        /// <summary>
        /// Our map instance to be editting.
        /// </summary>
        public HaloMap Map
        {
            get { return _map; }
            set { _map = value; }
        }

        private int _tagindex;
        /// <summary>
        /// Our index of our tag we'll be renaming.
        /// </summary>
        public int Tag_Index
        {
            get { return _tagindex; }
            set { _tagindex = value; }
        }

        public TagRenameForm(HaloMap map, int tagIndex)
        {
            InitializeComponent();

            //Set our instances of our map class and tag index
            Map = map;
            Tag_Index = tagIndex;

            //Set our tagname
            txtTagName.Text = Map.IndexItems[Tag_Index].Name;

            //Focus our textbox
            txtTagName.Focus();
        }

        private void btnRenameTag_Click(object sender, EventArgs e)
        {
            //Initialize our map functions
            MapBuilder map_functions = new MapBuilder(Map);

            //Rename our tag
            map_functions.RenameTag(Tag_Index, txtTagName.Text);

            //Close this.
            this.Close();
        }

        private void txtTagName_KeyDown(object sender, KeyEventArgs e)
        {
            //If we click enter..
            if (e.KeyCode == Keys.Enter)
                //Act as if
                btnRenameTag_Click(null, null);
        }


    }
}
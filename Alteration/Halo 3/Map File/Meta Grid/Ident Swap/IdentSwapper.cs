using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using HaloDeveloper.Map;

namespace Alteration.Halo_3.Meta_Grid.Ident_Swap
{
    public partial class IdentSwapper : Form
    {
        private ListView _identgrid;

        private HaloMap _map;

        public IdentSwapper(HaloMap map, ListView listView)
        {
            InitializeComponent();
            //Assign the ident grid
            IdentGrid = listView;
            //Assign the map instance
            Map = map;
            //Load classes
            LoadClasses();
            //Select the tagClass
            SelectItem(cmbxClass, listView.SelectedItems[0].SubItems[2].Text);
            //Select the tagName
            SelectItem(cmbxName, listView.SelectedItems[0].SubItems[3].Text);
        }

        public ListView IdentGrid
        {
            get { return _identgrid; }
            set { _identgrid = value; }
        }

        public HaloMap Map
        {
            get { return _map; }
            set { _map = value; }
        }

        private void LoadClasses()
        {
            //Initialize our list of treeNodes
            List<string> tagClasses = new List<string>();
            for (int i = 0; i < Map.IndexItems.Count; i++)
            {
                //Search to see if this tagClass is already initialized in a treenode.

                //So we start with an index of -1(null)
                int index = -1;
                //Loop through all the tagClasses
                for (int z = 0; z < tagClasses.Count; z++)
                {
                    //If the tagClass is that of the tagClass we're looking for
                    if (tagClasses[z] == Map.IndexItems[i].Class)
                    {
                        //Set our index of the tagClass
                        index = z;
                        //Break out of the loop.
                        break;
                    }
                }
                //If the treeNode doesn't exist...
                if (index == -1)
                {
                    //Add the tag to it.
                    tagClasses.Add(Map.IndexItems[i].Class);
                }
            }
            //Clear the tagClasscomboBox
            cmbxClass.Items.Clear();
            //Loop through the tagClasses
            for (int i = 0; i < tagClasses.Count; i++)
            {
                //Add the tagClass to the comboBox
                cmbxClass.Items.Add(tagClasses[i]);
            }
            //Sort the items
            cmbxClass.Sorted = true;
        }

        private void cmbx_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void SelectItem(ComboBoxEx comboBox, string text)
        {
            //Loop through the items of the combobox
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                //If the indexed item's text is that of the provided text...
                if (comboBox.Items[i].ToString() == text)
                {
                    //...Then select it
                    comboBox.SelectedIndex = i;
                    //Break out of the loop
                    break;
                }
            }
        }

        private void cmbx_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //Clear the tag name comboBox
            cmbxName.Items.Clear();
            //Loop through all the tags of the HaloMap class
            for (int i = 0; i < Map.IndexHeader.tagCount; i++)
            {
                //If the tag's name matches the class selected...
                if (Map.IndexItems[i].Class == cmbxClass.Text)
                {
                    //...Add the tag name instance
                    cmbxName.Items.Add(Map.IndexItems[i].Name);
                }
            }
            //Sort the comboBox
            cmbxName.Sorted = true;
            //Set it to not sort anymore so we can add null to the bottom.
            cmbxName.Sorted = false;
            //Add the null instance.
            cmbxName.Items.Add("<<Null>>");
        }

        private void cmbxName_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            //Open our IO
            Map.OpenIO();
            //Get our index for the tag
            int index = Map.GetTagIndexByClassAndName(cmbxClass.Text, cmbxName.Text);
            //Loop through all the offset
            for (int i = 0; i < IdentGrid.SelectedItems.Count; i++)
            {
                //Obtain our address
                int offset = int.Parse(IdentGrid.SelectedItems[i].SubItems[1].Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
                
                //Write our tagref
                if (index != -1)
                {
                    //Go to our tagref offset
                    Map.IO.Out.BaseStream.Position = offset;
                    //Write our tagRef
                    Map.IO.Out.WriteAsciiString(Map.IndexItems[index].Class, 4);
                }
                //Go to our tagid offset
                Map.IO.Out.BaseStream.Position = offset + 12;
                if (index != -1)
                {
                    //Write our tag ident
                    Map.IO.Out.Write(Map.IndexItems[index].Ident);
                    //Change our tag name & class
                    IdentGrid.SelectedItems[i].SubItems[2].Text = Map.IndexItems[index].Class;
                    IdentGrid.SelectedItems[i].SubItems[3].Text = Map.IndexItems[index].Name;
                }
                else
                {
                    //Write a null tag ident.
                    Map.IO.Out.Write(-1);
                    //Change our tag name & class
                    IdentGrid.SelectedItems[i].SubItems[2].Text = "Null";
                    IdentGrid.SelectedItems[i].SubItems[3].Text = "Null";
                }
            }
            //Close our IO
            Map.CloseIO();
            //Close our dialog
            Close();
        }
    }
}
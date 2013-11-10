using System;
using System.Windows.Forms;
using HaloDeveloper.Map;
using HaloDeveloper.Raw;
using HaloDeveloper.Helpers;
using System.IO;

namespace Alteration.Halo_3.Raw
{
    public partial class RawViewerControl : UserControl
    {
        private HaloMap _map;

        public RawViewerControl(HaloMap map)
        {
            InitializeComponent();
            //Set our instance of the HaloMap class
            Map = map;
            //Load our raw Items
            LoadRawItems(false,txtFilter.Text);

            //Lets loop through our classes
            foreach (TagHierarchy.TagHClass hclass in Map.TagHierarchy.TagClasses)
            {
                //Add it to our class combobox
                cmbxTagClass.Items.Add(hclass.TagClass);
            }
            //Sort our combobox
            cmbxTagClass.Sorted = true;
            cmbxTagClass.Sorted = false;

            //Add our null instance.
            cmbxTagClass.Items.Add("<<null>>");
        }

        public HaloMap Map
        {
            get { return _map; }
            set { _map = value; }
        }

        private void LoadRawItems(bool showNulls, string filter)
        {
            //Clear our rawGrid
            rawGrid.Items.Clear();
            //Loop through the RawInformation class' raw items
            foreach (RawInformation.RawEntry Raw_Block in Map.RawInformation.RawEntries)
            {
                //If we aren't to show nulls, and the block is null...
                if (Raw_Block.TagID == -1 && showNulls == false)
                {
                    //Skip to the next raw block
                    goto NextRawBlock;
                }
                if(!Raw_Block.TagName.Contains(filter) && !Raw_Block.TagClass.Contains(filter))
                    goto NextRawBlock;
                //Initialize our listview item
                ListViewItem List_Raw_Item = new ListViewItem();
                //Set our text(index)
                List_Raw_Item.Text = Raw_Block.Index.ToString();
                //Add our subitem(rawID) as a hex value
                List_Raw_Item.SubItems.Add("0x" + Raw_Block.RawID.ToString("X"));
                //Add our subitem(tagClass)
                List_Raw_Item.SubItems.Add(Raw_Block.TagClass);
                //Add our subitem(tagName)
                List_Raw_Item.SubItems.Add(Raw_Block.TagName);
                //Add our subitem(Raw Pool Index)
                List_Raw_Item.SubItems.Add(Raw_Block.RawLocationsIndex.ToString());
                //Add our subitem(Offset)
                List_Raw_Item.SubItems.Add("0x" + Raw_Block.Offset.ToString("X"));
                //Add our subitem(Size)
                List_Raw_Item.SubItems.Add("0x" + Raw_Block.Size.ToString("X"));
                //Add it to the list
                rawGrid.Items.Add(List_Raw_Item);
                //This is our goto point if we want to go to the next loop
                NextRawBlock:
                ;
            }
            //Autoresize
            //rawGrid.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void lblCount0_Paint(object sender, PaintEventArgs e)
        {
        }

        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            //Load our raw items with our checkBox checked bool.
            LoadRawItems(cbxShowNull.Checked,txtFilter.Text);
        }

        private void rawGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            //If we have atleast one item selected
            if (rawGrid.SelectedItems.Count != 0)
            {
                //Get our rawBlock instance
                RawInformation.RawEntry selectedRawBlock =
                    Map.RawInformation.RawEntries[int.Parse(rawGrid.SelectedItems[0].Text)];
                //Set our offset and size
                txtOffset.Text = "0x" + selectedRawBlock.Offset.ToString("X");
                txtSize.Text = "0x" + selectedRawBlock.Size.ToString("X");
                //Set our raw pool index
                txtRawPoolIndex.Text = selectedRawBlock.RawLocationsIndex.ToString();
                //Set our meta name and class
                cmbxTagClass.Text = selectedRawBlock.TagClass;
                cmbxTagName.Text = selectedRawBlock.TagName;
            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            //If we have a selected item
            if (rawGrid.SelectedItems.Count > 0)
            {
                //Lets get our tag index
                int index = Map.GetTagIndexByClassAndName(cmbxTagClass.Text, cmbxTagName.Text);
                //Create our null
                int ident = -1;

                //If it isn't -1 set it
                if (index != -1)
                    ident = Map.IndexItems[index].Ident;

                //Lets open our IO
                Map.OpenIO();

                //Let's go to our offset
                Map.IO.Out.BaseStream.Position = Map.RawInformation.RawEntries[int.Parse(rawGrid.SelectedItems[0].Text)].ZoneChunkOffset;

                //If our ident is -1, write -1 as an tagrefstr
                if (ident == -1)
                    Map.IO.Out.Write((int)-1);
                else
                    Map.IO.Out.WriteAsciiString(Map.IndexItems[index].Class,4);

                //Skip 8 bytes
                Map.IO.Out.BaseStream.Position += 8;

                //Write our ident
                Map.IO.Out.Write(ident);

                //Let's close our IO
                Map.CloseIO();

                //If our ident isn't -1
                if (ident != -1)
                {
                    //Set our items text.
                    rawGrid.SelectedItems[0].SubItems[2].Text = cmbxTagClass.Text;
                    rawGrid.SelectedItems[0].SubItems[3].Text = cmbxTagName.Text;
                }
                else
                {
                    //If it is
                    rawGrid.SelectedItems[0].SubItems[2].Text = "null";
                    rawGrid.SelectedItems[0].SubItems[3].Text = "null";
                }
            }
        }

        private void cmbxTagClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Loop through each tag hierarchy till we find our class
            foreach (TagHierarchy.TagHClass hclass in Map.TagHierarchy.TagClasses)
            {
                //If we found our class
                if (hclass.TagClass == cmbxTagClass.Text)
                {
                    //Clear our tagname combobox
                    cmbxTagName.Items.Clear();

                    //Loop through each tag name.
                    foreach (TagHierarchy.TagHName hname in hclass.Tags)
                        //Add our tag to our combobox
                        cmbxTagName.Items.Add(hname.TagName);

                    //Sort
                    cmbxTagName.Sorted = true;
                    cmbxTagName.Sorted = false;

                    //Break
                    break;
                }
            }

            //Add our null entry
            cmbxTagName.Items.Add("<<null>>");

            //Select our first item
            cmbxTagName.SelectedIndex = cmbxTagName.Items.Count - 1;
        }

        private void btnRefreshFilter_Click(object sender, EventArgs e)
        {
            //Load our raw items with our checkBox checked bool.
            LoadRawItems(cbxShowNull.Checked, txtFilter.Text);
        }
   }
}
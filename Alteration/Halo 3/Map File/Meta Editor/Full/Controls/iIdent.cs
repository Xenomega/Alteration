using System;
using System.Windows.Forms;
using Alteration.Halo_3.Values;
using Alteration.Patches.Memory_Patch.RTH_Data;
using HaloDeveloper.Helpers;
using HaloDeveloper.IO;
using DevComponents.DotNetBar.Controls;
using HaloDeveloper.Map;
using Alteration.Halo_3.Map_File.Meta_Editor.Forms;
using DevComponents.DotNetBar;

namespace Alteration.Halo_3.Meta_Editor.Controls
{
    public partial class iIdent : UserControl
    {
        private bool _editted;
        private mIdent _identdata;
        private int lastRealOffset;

        private HaloMap _map;

        public iIdent()
        {
            InitializeComponent();
        }

        public iIdent(mIdent identdata)
        {
            InitializeComponent();
            //Set our Ident Data Value
            IdentData = identdata;

            //Set our tooltip name.
            ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText =
                "Type: " + IdentData.Attributes.ToString() + Environment.NewLine +
                "Name: " + IdentData.Name + Environment.NewLine +
                "Offset: " + IdentData.Offset.ToString() + Environment.NewLine +
                "Visible: " + IdentData.Visible.ToString();

            //Loop through each control..
            for (int i = 0; i < this.Controls.Count; i++)
            {
                //Set the super tool tip info.
                try
                {
                    ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this.Controls[i]]).HeaderText = ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).HeaderText;
                    ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this.Controls[i]]).BodyText = ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText;
                    ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this.Controls[i]]).FooterVisible = false;
                }
                catch
                {
                    this.superTooltip1.SetSuperTooltip(this.Controls[i], new DevComponents.DotNetBar.SuperTooltipInfo(((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).HeaderText, "", ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText, null, null, DevComponents.DotNetBar.eTooltipColor.System, true, false, new System.Drawing.Size(0, 0)));

                }
            }

            //Set the name of this ident.
            lblName.Text = IdentData.Name;
        }

        public mIdent IdentData
        {
            get { return _identdata; }
            set { _identdata = value; }
        }

        public HaloMap HMap
        {
            get { return _map; }
            set { _map = value; }
        }

        public bool Editted
        {
            get { return _editted; }
            set { _editted = value; }
        }

        public void LoadValue(HaloMap map, int parentOffset)
        {
            //Set our instance of the HaloMap class
            HMap = map;
            //If we have no class
            if (cmbxClass.Items.Count == 0)
            {
                LoadClasses();
            }
            //Go to the value position
            HMap.IO.In.BaseStream.Position = parentOffset + IdentData.Offset;

            //Set our last real offset
            lastRealOffset = (int)HMap.IO.In.BaseStream.Position;

            //Set our context menu.
            this.ContextMenuStrip = contextMenuStrip1;
            foreach (Control c in this.Controls)
                c.ContextMenuStrip = contextMenuStrip1;


            //Read the Ident
            int ident = HMap.IO.In.ReadInt32();

            //Get the tagIndex by using the ident
            int index = HMap.GetTagIndexByIdent(ident);

            //Select the tagClass
            if (index != -1)
            {
                SelectItem(cmbxClass, map.IndexItems[index].Class);
            }
            else
            {
                cmbxClass.SelectedIndex = 0;
            }
            //Select the tagName
            if (index != -1)
            {
                SelectItem(cmbxName, map.IndexItems[index].Name);
            }
            else
            {
                SelectItem(cmbxName, "<<null>>");
            }
            Editted = false;
        }

        public void SaveValue(EndianIO IO, int parentOffset)
        {
            //Go to the required offset
            IO.Out.BaseStream.Position = (parentOffset + IdentData.Offset) - 12;

            //Get new ident-tag index
            if (cmbxName.Text != "<<null>>")
            {
                //Get the index using the tagname and class
                int index = HMap.GetTagIndexByClassAndName(cmbxClass.Text, cmbxName.Text);
                //If the index is null...
                if (index == -1)
                {
                    goto WriteNullIdent;
                }
                //Write the tagref
                IO.Out.WriteAsciiString(HMap.IndexItems[index].Class, 4);
                //Skip the 8 byte gap.
                IO.Out.BaseStream.Position += 8;
                //Write the ident
                IO.Out.Write(HMap.IndexItems[index].Ident);
                //Returned
                return;
            }
            //Otherwise...

            WriteNullIdent:
            ;
            //Jump to the ident offset
            IO.Out.BaseStream.Position += 12;
            //Write the ident
            IO.Out.Write(-1);
        }

        public RTHData.RTHDataBlock ReturnRTHDataBlock(int parentMemoryOffset)
        {
            //Initialize our RTH Data Block
            RTHData.RTHDataBlock RTH_Data_Block = new RTHData.RTHDataBlock();
            //Set our memory offset
            RTH_Data_Block.Memory_Offset = (uint) (parentMemoryOffset + IdentData.Offset) - 12;
            //Get our class data
            byte[] classData = ExtraFunctions.StringToBytes(cmbxClass.Text);
            //Get our blank data
            byte[] blankData = new byte[8];
            //Create our identData array
            byte[] identData = null;
            //If the value isn't null
            if (cmbxName.Text != "<<null>>")
            {
                //Assign our ident
                identData =
                    ExtraFunctions.HexStringToBytes(
                        HMap.IndexItems[HMap.GetTagIndexByClassAndName(cmbxClass.Text, cmbxName.Text)].Ident.ToString("X"));
            }
            else
            {
                //Otherwise, if it is, we assign a null value
                identData = new byte[4] {0xFF, 0xFF, 0xFF, 0xFF};
            }
            RTH_Data_Block.Data_Block =
                ExtraFunctions.HexStringToBytes(ExtraFunctions.BytesToHexString(classData) +
                                                ExtraFunctions.BytesToHexString(blankData) +
                                                ExtraFunctions.BytesToHexString(identData));
            //Set our block size
            RTH_Data_Block.Block_Size = 16;
            //Return the RTH Data Block instance
            return RTH_Data_Block;
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

        private void cmbxClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadNames();
        }

        private void LoadClasses()
        {
            //Clear our items.
            cmbxClass.Items.Clear();
            //Loop through our tagclasses
            for (int i = 0; i < HMap.TagHierarchy.TagClasses.Count; i++)
            {
                //Add our indexed tagclass
                cmbxClass.Items.Add(HMap.TagHierarchy.TagClasses[i].TagClass);
            }
            //Sort our combobox
            cmbxClass.Sorted = true;
            cmbxClass.Sorted = false;
            //Insert our null item as the first item
            cmbxClass.Items.Insert(0, "ÿÿÿÿ");
        }

        private void LoadNames()
        {
            //Clear the tag name comboBox
            cmbxName.Items.Clear();

            //Get our tagclass instance
            TagHierarchy.TagHClass parentClass = HMap.TagHierarchy.TagClasses.ReturnTagHClass(cmbxClass.Text);
            //If our parent class isn't invalid
            if (parentClass != null)
            {
                //Loop through the tagclasses in the parentClass
                for (int i = 0; i < parentClass.Tags.Count; i++)
                {
                    //Add our indexed class to the combobox
                    cmbxName.Items.Add(parentClass.Tags[i].TagName);
                }
                //Sort the comboBox
                cmbxName.Sorted = true;
                //Set it to not sort anymore so we can add null to the bottom.
                cmbxName.Sorted = false;
            }
            //Add the null instance.
            cmbxName.Items.Add("<<null>>");
            //Set our selected index as 0
            cmbxName.SelectedIndex = cmbxName.Items.Count - 1;
        }

        private void cmbxName_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void lblName_Click(object sender, EventArgs e)
        {
        }

        private void viewValueAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //If we didn't set our last real offset, exit.
            if (lastRealOffset == 0)
                return;

            //Initialize our view value as form
            iViewValueAs vva = new iViewValueAs(HMap, lastRealOffset);

            //Show it
            vva.Show();
        }

        private void cmbxName_TextChanged(object sender, EventArgs e)
        {
            Editted = true;
        }
    }
}
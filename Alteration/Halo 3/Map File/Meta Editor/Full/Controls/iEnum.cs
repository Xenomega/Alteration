using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Alteration.Halo_3.Values;
using Alteration.Patches.Memory_Patch.RTH_Data;
using HaloDeveloper.Helpers;
using HaloDeveloper.IO;
using HaloDeveloper.Map;
using Alteration.Halo_3.Map_File.Meta_Editor.Forms;
using DevComponents.DotNetBar;

namespace Alteration.Halo_3.Meta_Editor.Controls
{
    public partial class iEnum : UserControl
    {
        private bool _editted;
        private mEnum _enumdata;
        private int lastRealOffset;
        private HaloMap _map;
        /// <summary>
        /// Our instance of the map class.
        /// </summary>
        public HaloMap HMap
        {
            get { return _map; }
            set { _map = value; }
        }
        private List<mEnumOption> _enumoptions;

        public iEnum()
        {
            InitializeComponent();
        }

        public iEnum(mValue enumdata)
        {
            InitializeComponent();
            //Set our Enum Data
            EnumData = (mEnum) enumdata;

            //Set our tooltip name.
            ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText =
                "Type: " + EnumData.Attributes.ToString() + Environment.NewLine +
                "Name: " + EnumData.Name + Environment.NewLine +
                "Offset: " + EnumData.Offset.ToString() + Environment.NewLine +
                "Visible: " + EnumData.Visible.ToString();

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

            //Set our enumName
            lblName.Text = EnumData.Name;
            //Set our enum options
            EnumOptions = EnumData.Options;
            //Determine our type of enum
            switch (enumdata.Attributes)
            {
                    //set the data accordingly
                case mValue.ObjectAttributes.Enum8:
                    {
                        lblValueType.Text = "enum8";
                        break;
                    }
                case mValue.ObjectAttributes.Enum16:
                    {
                        lblValueType.Text = "enum16";
                        break;
                    }
                case mValue.ObjectAttributes.Enum32:
                    {
                        lblValueType.Text = "enum32";
                        break;
                    }
            }
            LoadOptions();
        }

        public mEnum EnumData
        {
            get { return _enumdata; }
            set { _enumdata = value; }
        }

        public List<mEnumOption> EnumOptions
        {
            get { return _enumoptions; }
            set { _enumoptions = value; }
        }

        public bool Editted
        {
            get { return _editted; }
            set { _editted = value; }
        }

        public void LoadOptions()
        {
            //Add our options to the list.

            //Clear the items.
            cmbxSelections.Items.Clear();
            //Loop through the options.
            for (int i = 0; i < EnumOptions.Count; i++)
            {
                //Add them.
                cmbxSelections.Items.Add(EnumOptions[i].Name);
            }
        }

        public void LoadValue(HaloMap Map, int parentOffset)
        {
            //Go to the value offset with the IO
            Map.IO.In.BaseStream.Position = parentOffset + EnumData.Offset;

            //Set our last real offset
            lastRealOffset = (int)Map.IO.In.BaseStream.Position;
            HMap = Map;
            //Set our context menu.
            this.ContextMenuStrip = contextMenuStrip1;
            foreach (Control c in this.Controls)
                c.ContextMenuStrip = contextMenuStrip1;

            //Determine our value type
            switch (EnumData.Attributes)
            {
                case mValue.ObjectAttributes.Enum8:
                    {
                        SelectOption(Map.IO.In.ReadByte());
                        break;
                    }
                case mValue.ObjectAttributes.Enum16:
                    {
                        SelectOption(Map.IO.In.ReadInt16());
                        break;
                    }
                case mValue.ObjectAttributes.Enum32:
                    {
                        SelectOption(Map.IO.In.ReadInt32());
                        break;
                    }
            }
            Editted = false;
        }

        public void SaveValue(EndianIO IO, int parentOffset)
        {
            //If no item is selected
            if (cmbxSelections.SelectedIndex == -1)
            {
                //Exit
                return;
            }
            //Go to the required offset
            IO.Out.BaseStream.Position = parentOffset + EnumData.Offset;
            //Determine type and write accordingly
            switch (EnumData.Attributes)
            {
                case mValue.ObjectAttributes.Enum8:
                    {
                        IO.Out.Write((byte) EnumOptions[cmbxSelections.SelectedIndex].Value);
                        break;
                    }
                case mValue.ObjectAttributes.Enum16:
                    {
                        IO.Out.Write((short) EnumOptions[cmbxSelections.SelectedIndex].Value);
                        break;
                    }
                case mValue.ObjectAttributes.Enum32:
                    {
                        IO.Out.Write(EnumOptions[cmbxSelections.SelectedIndex].Value);
                        break;
                    }
            }
        }

        public void SelectOption(int value)
        {
            //Loop through the values.
            for (int i = 0; i < EnumOptions.Count; i++)
            {
                //If the enum option value is this value
                if (EnumOptions[i].Value == value)
                {
                    //Select it.
                    cmbxSelections.SelectedIndex = i;
                    //Return out of this function
                    return;
                }
            }
            //Otherwise add the new read value
            mEnumOption enumOption = new mEnumOption();
            //Set the name
            enumOption.Name = "<unknown>(" + value + ")";
            //Set the value
            enumOption.Value = value;
            //Add it to the list
            EnumOptions.Add(enumOption);
            //Add it to the comboBox
            cmbxSelections.Items.Add(enumOption.Name);
            //Select it
            cmbxSelections.SelectedIndex = cmbxSelections.Items.Count - 1;
        }

        public RTHData.RTHDataBlock ReturnRTHDataBlock(int parentMemoryOffset)
        {
            //Initialize our RTH Data Block
            RTHData.RTHDataBlock RTH_Data_Block = new RTHData.RTHDataBlock();
            //Set our memory offset
            RTH_Data_Block.Memory_Offset = (uint) (parentMemoryOffset + EnumData.Offset);
            //Determine type and assign accordingly
            switch (EnumData.Attributes)
            {
                case mValue.ObjectAttributes.Enum8:
                    {
                        RTH_Data_Block.Data_Block =
                            ExtraFunctions.ValueToBytes((byte) EnumOptions[cmbxSelections.SelectedIndex].Value);
                        break;
                    }
                case mValue.ObjectAttributes.Enum16:
                    {
                        RTH_Data_Block.Data_Block =
                            ExtraFunctions.ValueToBytes((short) EnumOptions[cmbxSelections.SelectedIndex].Value);
                        break;
                    }
                case mValue.ObjectAttributes.Enum32:
                    {
                        RTH_Data_Block.Data_Block =
                            ExtraFunctions.ValueToBytes(EnumOptions[cmbxSelections.SelectedIndex].Value);
                        break;
                    }
            }
            //Set our block size
            RTH_Data_Block.Block_Size = RTH_Data_Block.Data_Block.Length;
            //Return the RTH Data Block instance
            return RTH_Data_Block;
        }

        private void cmbxSelections_SelectedIndexChanged(object sender, EventArgs e)
        {
            Editted = true;
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
    }
}
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Alteration.Halo_3.Values;
using Alteration.Helpers;
using Alteration.Patches.Memory_Patch.RTH_Data;
using HaloDeveloper.Helpers;
using HaloDeveloper.IO;
using HaloDeveloper.Map;
using Alteration.Halo_3.Map_File.Meta_Editor.Forms;
using DevComponents.DotNetBar;

namespace Alteration.Halo_3.Meta_Editor.Controls
{
    public partial class iBitmask : UserControl
    {
        private mValue _bitmaskdata;

        private bool _editted;

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

        public iBitmask()
        {
            InitializeComponent();
        }

        public iBitmask(mValue bitmaskdata)
        {
            InitializeComponent();
            //Set our bitmask Data
            BitmaskData = bitmaskdata;

            //Chec what type of bitmask it is
            switch (BitmaskData.Attributes)
            {
                    //Based off what type of bitmask it is, we set the name and count.
                case mValue.ObjectAttributes.Bitmask8:
                    {
                        mBitmask8 mbit8 = (mBitmask8) BitmaskData;
                        lblName.Text = mbit8.Name;
                        lblCount.Text = "(8)";
                        LoadBit8Options(mbit8);
                        break;
                    }
                case mValue.ObjectAttributes.Bitmask16:
                    {
                        mBitmask16 mbit16 = (mBitmask16) BitmaskData;
                        lblName.Text = mbit16.Name;
                        lblCount.Text = "(16)";
                        LoadBit16Options(mbit16);
                        break;
                    }
                case mValue.ObjectAttributes.Bitmask32:
                    {
                        mBitmask32 mbit32 = (mBitmask32) BitmaskData;
                        lblName.Text = mbit32.Name;
                        lblCount.Text = "(32)";
                        LoadBit32Options(mbit32);
                        break;
                    }
            }

            //Set our tooltip name.
            ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText =
                "Type: " + BitmaskData.Attributes.ToString() + Environment.NewLine +
                "Name: " + BitmaskData.Name + Environment.NewLine +
                "Offset: " + BitmaskData.Offset.ToString() + Environment.NewLine +
                "Visible: " + BitmaskData.Visible.ToString() + Environment.NewLine + Environment.NewLine +
                "OPTIONS:";
            
            //Loop for each option
            for (int i = 0; i < chkBits.Items.Count; i++)
                if(chkBits.Items[i].ToString() != "Bit " + i.ToString())
                ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText += Environment.NewLine + i.ToString() + " - " + chkBits.Items[i].ToString();
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
        }

        public mValue BitmaskData
        {
            get { return _bitmaskdata; }
            set { _bitmaskdata = value; }
        }

        public bool Editted
        {
            get { return _editted; }
            set { _editted = value; }
        }

        public void LoadValue(HaloMap Map, int parentOffset)
        {
            //Go to the position
            Map.IO.In.BaseStream.Position = parentOffset + BitmaskData.Offset;

            //Set our last real offset
            lastRealOffset = (int)Map.IO.In.BaseStream.Position;
            HMap = Map;
            //Set our context menu.
            this.ContextMenuStrip = contextMenuStrip1;
            foreach (Control c in this.Controls)
                c.ContextMenuStrip = contextMenuStrip1;


            //Determine the bitType, and read the values accordingly
            List<bool> bitFlags = null;
            switch (BitmaskData.Attributes)
            {
                case mValue.ObjectAttributes.Bitmask8:
                    bitFlags = BitHelper.LoadValue(Map.IO.In.ReadByte(), 8);
                    break;
                case mValue.ObjectAttributes.Bitmask16:
                    bitFlags = BitHelper.LoadValue(Map.IO.In.ReadInt16(), 16);
                    break;
                case mValue.ObjectAttributes.Bitmask32:
                    bitFlags = BitHelper.LoadValue(Map.IO.In.ReadInt32(), 32);
                    break;
            }
            for (int i = 0; i < chkBits.Items.Count; i++)
            {
                chkBits.SetItemChecked(i, bitFlags[i]);
            }
            Editted = false;
        }

        public void SaveValue(EndianIO IO, int parentOffset)
        {
            //Go to the required offset
            IO.Out.BaseStream.Position = parentOffset + BitmaskData.Offset;
            //Determine type and write accordingly
            switch (BitmaskData.Attributes)
            {
                case mValue.ObjectAttributes.Bitmask8:
                    {
                        IO.Out.Write((byte) BitHelper.ConvertToWriteableInteger(ReturnCheckedList()));
                        break;
                    }
                case mValue.ObjectAttributes.Bitmask16:
                    {
                        IO.Out.Write((short) BitHelper.ConvertToWriteableInteger(ReturnCheckedList()));
                        break;
                    }
                case mValue.ObjectAttributes.Bitmask32:
                    {
                        IO.Out.Write(BitHelper.ConvertToWriteableInteger(ReturnCheckedList()));
                        break;
                    }
            }
        }

        private List<bool> ReturnCheckedList()
        {
            //Initialize a new bool list
            List<bool> boolList = new List<bool>();
            //Loop through all items
            for (int i = 0; i < chkBits.Items.Count; i++)
            {
                //Add their checkstate to the boolList
                boolList.Add(chkBits.GetItemChecked(i));
            }
            //Return the list
            return boolList;
        }

        private void LoadBit8Options(mBitmask8 mBit8)
        {
            //Clear all items.
            chkBits.Items.Clear();
            //Initialize our boolean to see if an item has been added.
            bool added = false;
            //Loop through the bitcount
            for (int i = 0; i < 8; i++)
            {
                //Set the added bool
                added = false;
                //Loop through all the bitOptions
                for (int z = 0; z < mBit8.Options.Count; z++)
                {
                    //If the bitOption's index is this bitIndex we're adding...
                    if (mBit8.Options[z].BitIndex == i)
                    {
                        //Then add it to the list.
                        chkBits.Items.Add(mBit8.Options[z].Name);
                        //Set that it has been added.
                        added = true;
                        //Break out of the loop
                        break;
                    }
                }
                //If it hasn't been added.
                if (!added)
                {
                    //Add one with no name.
                    chkBits.Items.Add("Bit " + i);
                }
            }
        }

        private void LoadBit16Options(mBitmask16 mBit16)
        {
            //Clear all items.
            chkBits.Items.Clear();
            //Initialize our boolean to see if an item has been added.
            bool added = false;
            //Loop through the bitcount
            for (int i = 0; i < 16; i++)
            {
                //Set the added bool
                added = false;
                //Loop through all the bitOptions
                for (int z = 0; z < mBit16.Options.Count; z++)
                {
                    //If the bitOption's index is this bitIndex we're adding...
                    if (mBit16.Options[z].BitIndex == i)
                    {
                        //Then add it to the list.
                        chkBits.Items.Add(mBit16.Options[z].Name);
                        //Set that it has been added.
                        added = true;
                        //Break out of the loop
                        break;
                    }
                }
                //If it hasn't been added.
                if (!added)
                {
                    //Add one with no name.
                    chkBits.Items.Add("Bit " + i);
                }
            }
        }

        private void LoadBit32Options(mBitmask32 mBit32)
        {
            //Clear all items.
            chkBits.Items.Clear();
            //Initialize our boolean to see if an item has been added.
            bool added = false;
            //Loop through the bitcount
            for (int i = 0; i < 32; i++)
            {
                //Set the added bool
                added = false;
                //Loop through all the bitOptions
                for (int z = 0; z < mBit32.Options.Count; z++)
                {
                    //If the bitOption's index is this bitIndex we're adding...
                    if (mBit32.Options[z].BitIndex == i)
                    {
                        //Then add it to the list.
                        chkBits.Items.Add(mBit32.Options[z].Name);
                        //Set that it has been added.
                        added = true;
                        //Break out of the loop
                        break;
                    }
                }
                //If it hasn't been added.
                if (!added)
                {
                    //Add one with no name.
                    chkBits.Items.Add("Bit " + i);
                }
            }
        }

        public RTHData.RTHDataBlock ReturnRTHDataBlock(int parentMemoryOffset)
        {
            //Initialize our RTH Data Block
            RTHData.RTHDataBlock RTH_Data_Block = new RTHData.RTHDataBlock();
            //Set our memory offset
            RTH_Data_Block.Memory_Offset = (uint) (parentMemoryOffset + BitmaskData.Offset);
            //Determine type and assign accordingly
            switch (BitmaskData.Attributes)
            {
                case mValue.ObjectAttributes.Bitmask8:
                    {
                        RTH_Data_Block.Data_Block =
                            ExtraFunctions.ValueToBytes((byte) BitHelper.ConvertToWriteableInteger(ReturnCheckedList()));
                        break;
                    }
                case mValue.ObjectAttributes.Bitmask16:
                    {
                        RTH_Data_Block.Data_Block =
                            ExtraFunctions.ValueToBytes((short) BitHelper.ConvertToWriteableInteger(ReturnCheckedList()));
                        break;
                    }
                case mValue.ObjectAttributes.Bitmask32:
                    {
                        RTH_Data_Block.Data_Block =
                            ExtraFunctions.ValueToBytes(BitHelper.ConvertToWriteableInteger(ReturnCheckedList()));
                        break;
                    }
            }
            //Reverse our value
            //Array.Reverse(RTH_Data_Block.Data_Block);
            //Set our block size
            RTH_Data_Block.Block_Size = RTH_Data_Block.Data_Block.Length;
            //Return the RTH Data Block instance
            return RTH_Data_Block;
        }

        private void chkBits_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void chkBits_ItemCheck(object sender, ItemCheckEventArgs e)
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
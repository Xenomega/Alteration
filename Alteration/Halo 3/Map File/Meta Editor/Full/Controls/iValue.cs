using System;
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
    public partial class iValue : UserControl
    {
        private bool _editted;
        private mValue _valuedata;
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

        public iValue()
        {
            InitializeComponent();
        }

        public iValue(mValue valdata)
        {
            InitializeComponent();
            //Set our value data.
            ValueData = valdata;

            //Set our tooltip name.
            ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText =
                "Type: " + ValueData.Attributes.ToString() + Environment.NewLine +
                "Name: " + ValueData.Name + Environment.NewLine +
                "Offset: " + ValueData.Offset.ToString() + Environment.NewLine +
                "Visible: " + ValueData.Visible.ToString() + Environment.NewLine + Environment.NewLine;
            //Set our type
            ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).HeaderText = ValueData.Attributes.ToString();

            //Determine our type
            switch (ValueData.Attributes)
            {
                case mValue.ObjectAttributes.Byte:
                    {
                        //Set our tooltip name.
                        ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText +=
                          "This value is a byte.";
                        break;
                    }
                case mValue.ObjectAttributes.Float:
                    {
                        //Set our tooltip name.
                        ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText +=
                          "This value is a floating point integer(4 bytes in length).";
                        break;
                    }
                case mValue.ObjectAttributes.Int16:
                    {
                        //Set our tooltip name.
                        ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText +=
                          "This value is a short(int16, 2 bytes in length).";
                        break;
                    }
                case mValue.ObjectAttributes.Int32:
                    {
                        //Set our tooltip name.
                        ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText +=
                          "This value is a integer/long(int32, 4 bytes in length).";
                        break;
                    }
                case mValue.ObjectAttributes.UInt16:
                    {
                        //Set our tooltip name.
                        ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText +=
                          "This value is a unsigned-short(uint16, 2 bytes in length).";
                        break;
                    }
                case mValue.ObjectAttributes.UInt32:
                    {
                        //Set our tooltip name.
                        ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText +=
                          "This value is a unsigned-integer/long(uint32, 4 bytes in length).";
                        break;
                    }
                case mValue.ObjectAttributes.Undefined:
                    {
                        //Set our tooltip name.
                        ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText +=
                          "This value is undefined, and will be read as a floating-point integer.";
                        break;
                    }
                case mValue.ObjectAttributes.String32:
                    {
                        //Set our tooltip name.
                        ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText +=
                          "This value is a 32-byte long ASCII string.";
                        break;
                    }
                case mValue.ObjectAttributes.String256:
                    {
                        //Set our tooltip name.
                        ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText +=
                          "This value is a 256-byte long ASCII string.";
                        break;
                    }
                case mValue.ObjectAttributes.Unicode64:
                    {
                        //Set our tooltip name.
                        ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText +=
                          "This value is a 64-byte long Unicode string.";
                        break;
                    }
                case mValue.ObjectAttributes.Unicode256:
                    {
                        //Set our tooltip name.
                        ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText +=
                          "This value is a 64-byte long Unicode string.";
                        break;
                    }
                case mValue.ObjectAttributes.StringID:
                    {
                        //Set our tooltip name.
                        ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText +=
                          "This value is a StringID.";
                        break;
                    }
            }

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

            //Set our Name
            lblName.Text = ValueData.Name;
            //Set our valueType
            lblType.Text = ValueData.Attributes.ToString().ToLower();
        }

        public mValue ValueData
        {
            get { return _valuedata; }
            set { _valuedata = value; }
        }

        public bool Editted
        {
            get { return _editted; }
            set { _editted = value; }
        }

        public void LoadValue(HaloMap Map, int parentOffset)
        {
            //Go to the value offset with the IO
            Map.IO.In.BaseStream.Position = parentOffset + ValueData.Offset;

            //Set our last real offset
            lastRealOffset = (int)Map.IO.In.BaseStream.Position;
            HMap = Map;
            //Set our context menu.
            this.ContextMenuStrip = contextMenuStrip1;
            foreach (Control c in this.Controls)
                c.ContextMenuStrip = contextMenuStrip1;



            //Determine the type and read it accordingly.
            switch (ValueData.Attributes)
            {
                case mValue.ObjectAttributes.Byte:
                    {
                        //Read a byte
                        txtValue.Text = Map.IO.In.ReadByte().ToString();
                        break;
                    }
                case mValue.ObjectAttributes.Float:
                    {
                        //Read a single/float
                        txtValue.Text = Map.IO.In.ReadSingle().ToString();
                        break;
                    }
                case mValue.ObjectAttributes.Int16:
                    {
                        //Read a short/int16
                        txtValue.Text = Map.IO.In.ReadInt16().ToString();
                        break;
                    }
                case mValue.ObjectAttributes.Int32:
                    {
                        //Read an int32/int/long
                        txtValue.Text = Map.IO.In.ReadInt32().ToString();
                        break;
                    }
                case mValue.ObjectAttributes.UInt16:
                    {
                        //Read a uint16/ushort
                        txtValue.Text = Map.IO.In.ReadUInt16().ToString();
                        break;
                    }
                case mValue.ObjectAttributes.UInt32:
                    {
                        //Read a uint/uint32/ulong
                        txtValue.Text = Map.IO.In.ReadUInt32().ToString();
                        break;
                    }
                case mValue.ObjectAttributes.String32:
                    {
                        //Read the string 32-bytes long
                        txtValue.Text = Map.IO.In.ReadAsciiString(32);
                        //Set the maximum length
                        txtValue.MaxLength = 32;
                        break;
                    }
                case mValue.ObjectAttributes.String256:
                    {
                        //Read the string 256-bytes long
                        txtValue.Text = Map.IO.In.ReadAsciiString(256);
                        //Set the maximum length
                        txtValue.MaxLength = 256;
                        break;
                    }
                case mValue.ObjectAttributes.Unicode64:
                    {
                        //Read the unicode 64-bytes long
                        txtValue.Text = Map.IO.In.ReadUnicodeString(32);
                        //Set the maximum length
                        txtValue.MaxLength = 32;
                        break;
                    }
                case mValue.ObjectAttributes.Unicode256:
                    {
                        //Read the unicode 256-bytes long
                        txtValue.Text = Map.IO.In.ReadUnicodeString(128);
                        //Set the maximum length
                        txtValue.MaxLength = 128;
                        break;
                    }
                case mValue.ObjectAttributes.TagRef:
                    {
                        //Read the tag, which is 4 bytes long
                        txtValue.Text = Map.IO.In.ReadAsciiString(4);
                        //Set the maximum length
                        txtValue.MaxLength = 4;
                        //Disable the textBox
                        txtValue.ReadOnly = true;
                        break;
                    }
                case mValue.ObjectAttributes.Undefined:
                    {
                        //Read a single/float
                        txtValue.Text = Map.IO.In.ReadSingle().ToString();
                        break;
                    }
                case mValue.ObjectAttributes.StringID:
                    {
                        //Read our StringID ID
                        int stringIDID = Map.IO.In.ReadInt32();

                        //Get our index for the StringID
                        int index = Map.StringTable.GetStringItemIndexByID(Map,stringIDID);

                        //Try to..
                        try
                        {
                            //Set the text as the string items name
                            txtValue.Text = Map.StringTable.StringItems[index].Name;
                        }
                        //Incase of an error.
                        catch
                        {
                            //Set the text as the null
                            txtValue.Text = "<<null>>";
                        }
                        break;
                    }
            }
            Editted = false;
        }

        public void SaveValue(EndianIO IO, int parentOffset)
        {
            //Go to the required offset
            IO.Out.BaseStream.Position = parentOffset + ValueData.Offset;
            //Determine type and write accordingly
            switch (ValueData.Attributes)
            {
                case mValue.ObjectAttributes.Byte:
                    {
                        IO.Out.Write(byte.Parse(txtValue.Text));
                        break;
                    }
                case mValue.ObjectAttributes.Float:
                    {
                        IO.Out.Write(float.Parse(txtValue.Text));
                        break;
                    }
                case mValue.ObjectAttributes.Int16:
                    {
                        IO.Out.Write(short.Parse(txtValue.Text));
                        break;
                    }
                case mValue.ObjectAttributes.UInt16:
                    {
                        IO.Out.Write(ushort.Parse(txtValue.Text));
                        break;
                    }
                case mValue.ObjectAttributes.Int32:
                    {
                        IO.Out.Write(int.Parse(txtValue.Text));
                        break;
                    }
                case mValue.ObjectAttributes.UInt32:
                    {
                        IO.Out.Write(uint.Parse(txtValue.Text));
                        break;
                    }
                case mValue.ObjectAttributes.Undefined:
                    {
                        IO.Out.Write(float.Parse(txtValue.Text));
                        break;
                    }
                case mValue.ObjectAttributes.String32:
                    {
                        IO.Out.WriteAsciiString(txtValue.Text, 32);
                        break;
                    }
                case mValue.ObjectAttributes.String256:
                    {
                        IO.Out.WriteAsciiString(txtValue.Text, 256);
                        break;
                    }
                case mValue.ObjectAttributes.Unicode64:
                    {
                        IO.Out.WriteUnicodeString(txtValue.Text, 32);
                        break;
                    }
                case mValue.ObjectAttributes.Unicode256:
                    {
                        IO.Out.WriteUnicodeString(txtValue.Text, 128);
                        break;
                    }
            }
        }

        public RTHData.RTHDataBlock ReturnRTHDataBlock(int parentMemoryOffset)
        {
            //Initialize our RTH Data Block
            RTHData.RTHDataBlock RTH_Data_Block = new RTHData.RTHDataBlock();
            //Set our memory offset
            RTH_Data_Block.Memory_Offset = (uint) (parentMemoryOffset + ValueData.Offset);
            //Determine type and assign accordingly
            switch (ValueData.Attributes)
            {
                case mValue.ObjectAttributes.Byte:
                    {
                        RTH_Data_Block.Data_Block = ExtraFunctions.ValueToBytes(byte.Parse(txtValue.Text));
                        break;
                    }
                case mValue.ObjectAttributes.Float:
                    {
                        RTH_Data_Block.Data_Block = ExtraFunctions.ValueToBytes(float.Parse(txtValue.Text));
                        break;
                    }
                case mValue.ObjectAttributes.Int16:
                    {
                        RTH_Data_Block.Data_Block = ExtraFunctions.ValueToBytes(short.Parse(txtValue.Text));
                        break;
                    }
                case mValue.ObjectAttributes.UInt16:
                    {
                        RTH_Data_Block.Data_Block = ExtraFunctions.ValueToBytes(ushort.Parse(txtValue.Text));
                        break;
                    }
                case mValue.ObjectAttributes.Int32:
                    {
                        RTH_Data_Block.Data_Block = ExtraFunctions.ValueToBytes(int.Parse(txtValue.Text));
                        break;
                    }
                case mValue.ObjectAttributes.UInt32:
                    {
                        RTH_Data_Block.Data_Block = ExtraFunctions.ValueToBytes(uint.Parse(txtValue.Text));
                        break;
                    }
                case mValue.ObjectAttributes.Undefined:
                    {
                        RTH_Data_Block.Data_Block = ExtraFunctions.ValueToBytes(float.Parse(txtValue.Text));
                        break;
                    }
                case mValue.ObjectAttributes.String32:
                    {
                        RTH_Data_Block.Data_Block = ExtraFunctions.StringToBytes(txtValue.Text);
                        break;
                    }
                case mValue.ObjectAttributes.String256:
                    {
                        RTH_Data_Block.Data_Block = ExtraFunctions.StringToBytes(txtValue.Text);
                        break;
                    }
                case mValue.ObjectAttributes.StringID:
                    {
                        RTH_Data_Block.Data_Block = new byte[0];
                        break;
                    }
                case mValue.ObjectAttributes.Unicode64:
                    {
                        RTH_Data_Block.Data_Block = ExtraFunctions.StringToUnicodeBytes(txtValue.Text);
                        break;
                    }
                case mValue.ObjectAttributes.Unicode256:
                    {
                        RTH_Data_Block.Data_Block = ExtraFunctions.StringToUnicodeBytes(txtValue.Text);
                        break;
                    }
            }
            //Set our block size
            RTH_Data_Block.Block_Size = RTH_Data_Block.Data_Block.Length;
            //Return the RTH Data Block instance
            return RTH_Data_Block;
        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            Editted = true;
        }

        private void viewValueAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //If we didn't set our last real offset, exit.
            if (lastRealOffset == 0)
                return;

            //Initialize our view value as form
            iViewValueAs vva = new iViewValueAs(HMap,lastRealOffset);

            //Show it
            vva.Show();
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;
using Alteration.Halo_3.Meta_Editor.Loader;
using Alteration.Halo_3.Values;
using Alteration.Patches.Memory_Patch.RTH_Data;
using HaloDeveloper.IO;
using DevComponents.DotNetBar.Controls;
using HaloDeveloper.Map;
using Alteration.Halo_3.Map_File.Meta_Editor.Chunk_Clipboard;
using DevComponents.DotNetBar;

namespace Alteration.Halo_3.Meta_Editor.Controls
{
    public partial class iStruct : UserControl
    {
        private HaloMap _map;

        private mReflexive _reflexivedata;

        public int previous_height;

        private int lastRealOffset;

        public iStruct()
        {
            InitializeComponent();
        }

        public iStruct(mReflexive mReflex)
        {
            InitializeComponent();
            //Set our reflexive data
            ReflexiveData = mReflex;

            #region Set Tooltip
            //Set our tooltip name.
            ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText =
                "Type: " + ReflexiveData.Attributes.ToString() + Environment.NewLine +
                "Name: " + ReflexiveData.Name + Environment.NewLine +
                "Offset: " + ReflexiveData.Offset.ToString() + Environment.NewLine +
                "Size: " + ReflexiveData.Size.ToString() + Environment.NewLine +
                "Value Count: " + ReflexiveData.Values.Count + Environment.NewLine +
                "Visible: " + ReflexiveData.Visible.ToString();

            //Set the super tool tip info.
            try
            {
                ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[pnlHeader]).HeaderText = ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).HeaderText;
                ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[pnlHeader]).BodyText = ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText;
                ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[pnlHeader]).FooterVisible = false;
            }
            catch
            {
                this.superTooltip1.SetSuperTooltip(pnlHeader, new DevComponents.DotNetBar.SuperTooltipInfo(((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).HeaderText, "", ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText, null, null, DevComponents.DotNetBar.eTooltipColor.System, true, false, new System.Drawing.Size(0, 0)));

            }

            //Loop through each control..
            for (int i = 0; i < this.pnlHeader.Controls.Count; i++)
            {
                //Set the super tool tip info.
                try
                {
                    ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this.pnlHeader.Controls[i]]).HeaderText = ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).HeaderText;
                    ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this.pnlHeader.Controls[i]]).BodyText = ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText;
                    ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this.pnlHeader.Controls[i]]).FooterVisible = false;
                }
                catch
                {
                    this.superTooltip1.SetSuperTooltip(this.pnlHeader.Controls[i], new DevComponents.DotNetBar.SuperTooltipInfo(((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).HeaderText, "", ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText, null, null, DevComponents.DotNetBar.eTooltipColor.System, true, false, new System.Drawing.Size(0, 0)));

                }
            }
            #endregion

            //Change the reflexive name on the UI
            lblName.Text = ReflexiveData.Name.ToUpper();
        }

        public HaloMap Map
        {
            get { return _map; }
            set { _map = value; }
        }

        public mReflexive ReflexiveData
        {
            get { return _reflexivedata; }
            set { _reflexivedata = value; }
        }

        public Panel returnValuePanel()
        {
            //Return our value panel
            return pnlValues;
        }

        public ComboBoxEx returnComboBox()
        {
            //Return our comboBox
            return comboChunks;
        }

        public void ResizeHeight()
        {
            //Set the main(header) height as a default
            int height = 32;
            //Loop through the controls
            for (int i = 0; i < pnlValues.Controls.Count; i++)
            {
                //Add to the height for each control in the panel
                height += pnlValues.Controls[i].Size.Height;
            }
            //Change our header size, as it is also the sidebar size, to appropriately fit
            pnlHeader.Size = new Size(pnlHeader.Size.Width, height);
            //Change our value panel size to fit all the values contained within it.
            pnlValues.Size = new Size(pnlValues.Size.Width, height - 32);
            //Edit the usercontrol size itself, to accustom the size of both above panels.
            Size = new Size(Width, pnlHeader.Height);
        }

        public void RefreshBaseItems(iStruct istruct, int change)
        {
            int c = pnlValues.Controls.Count - 1;
            bool now = false;
            //Loop for every control
            for (int i = 0; i < c + 1; i++)
            {
                if (pnlValues.Controls[c - i] == istruct)
                {
                    now = true;
                }
                else
                {
                    if (now)
                    {
                        pnlValues.Controls[c - i].Location = new Point(0, pnlValues.Controls[c - i].Location.Y + change);
                    }
                }
            }
        }

        public void ResizeParentsRecursively()
        {
            iStruct lastStruct = this;
            //Set the first control
            Control parent = this;
            //Get our height change
            int heightChange = Height - previous_height;
            //Continue to loop
            while (true)
            {
                //Set the parent, this will keep setting and looping
                parent = parent.Parent;
                //...Until the parent is null
                if (Parent == null)
                {
                    //...Then it'll exit the loop
                    break;
                }
                //...Or until we hit the meta editor container
                if (parent.Name == "MetaEditorContainer")
                {
                    //Then resize base children and break
                    ((MetaEditorContainer) parent).RefreshBaseItems(lastStruct, heightChange);
                    //Break
                    break;
                }
                //But if its a structure as a parent
                if (parent.Name == "iStruct")
                {
                    //Size it to match its values.
                    ((iStruct) parent).ResizeHeight();
                    ((iStruct) parent).RefreshBaseItems(lastStruct, heightChange);
                    lastStruct = ((iStruct) parent);
                }
            }
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            #region TODO

            previous_height = Height;
            //If the control is maximized
            if (btnResize.Text == "-")
            {
                //Set the size to minimize it.
                Size = new Size(Size.Width, 32);
                //Change the text of the button to indicate it is now minimized
                btnResize.Text = "+";
                //Size the parents recursively for this new size change.
                ResizeParentsRecursively();
            }
                //Otherwise the control is minimized.
            else
            {
                //So we size it to its height
                ResizeHeight();
                //Resize its parents.
                ResizeParentsRecursively();
                //Set the text to indicate its now maximized.
                btnResize.Text = "-";
            }

            #endregion
        }

        public void LoadStructure(HaloMap map, int parentOffset)
        {
            //Set our map instance
            Map = map;
            //Make sure the IO is open
            if (!Map.IOisOpen)
            {
                //If it isn't. Then open it
                Map.OpenIO();
            }
            //Move to the structure position
            Map.IO.In.BaseStream.Position = parentOffset + ReflexiveData.Offset;
            //Read the chunkcount and assign it.
            ReflexiveData.ChunkCount = Map.IO.In.ReadInt32();
            //Read the memoryPointer and assign it
            ReflexiveData.MemoryPointer = Map.IO.In.ReadInt32();
            //Apply the memory address modifier to get the file offset of the pointer
            ReflexiveData.Pointer = ReflexiveData.MemoryPointer - Map.MapHeader.mapMagic;

            //Clear the chunkcomboBox
            comboChunks.Items.Clear();
            //If its a chunkcount of 0...
            if (ReflexiveData.ChunkCount <= 0 | ReflexiveData.ChunkCount > 100000 | ReflexiveData.Pointer < 0 | ReflexiveData.Pointer >= (int)Map.IO.In.BaseStream.Length)
            {
                //Disable the combobox
                comboChunks.Enabled = false;
                //Clear the combobox text if any remaining...
                comboChunks.Text = "";
                //Disable the value panel.
                pnlValues.Enabled = false;
                //Quit this function
                return;
            }
            //Otherwise...
            if (ReflexiveData.ChunkCount > 0)
            {
                //Set our last real offset
                lastRealOffset = parentOffset + ReflexiveData.Offset;

                //Enable our controls.
                comboChunks.Enabled = true;
                pnlValues.Enabled = true;
                //Populate our chunkBox
                for (int i = 0; i < ReflexiveData.ChunkCount; i++)
                {
                    comboChunks.Items.Add(i + ": " + ReflexiveData.Name.ToLower() + " chunk");
                }
                //Select the first item.
                comboChunks.SelectedIndex = 0;
            }
        }

        public void SaveStructure(HaloMap map, int parentOffset)
        {
            //If the reflexive is enabled...
            if (comboChunks.Enabled)
            {
                //Obtain our offset for our chunk
                int chunkOffset = ReflexiveData.Pointer + (comboChunks.SelectedIndex*ReflexiveData.Size);
                //Save values.
                MetaEditorHandler.SaveChangedValues(map, pnlValues, chunkOffset);
            }
        }

        public void PokeStructure(EndianIO IO, int magic, bool onlyChanged)
        {

                //If the reflexive is enabled...
                if (comboChunks.Enabled)
                {
                    //Obtain our offset for our chunk
                    int chunkOffset = ReflexiveData.Pointer + (comboChunks.SelectedIndex*ReflexiveData.Size);
                    //Save values.
                    MetaEditorHandler.PokeValues(IO, pnlValues, chunkOffset, magic, onlyChanged);
                }

        }

        public void BuildRTHData(RTHData RTH_Data, int magic, bool onlyChanged)
        {
            //If the reflexive is enabled...
            if (comboChunks.Enabled)
            {
                //Obtain our offset for our chunk
                int chunkOffset = ReflexiveData.Pointer + (comboChunks.SelectedIndex*ReflexiveData.Size);
                //Save values.
                MetaEditorHandler.BuildRTHData(RTH_Data, pnlValues, chunkOffset, magic, onlyChanged);
            }
        }

        private void comboChunks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboChunks.SelectedIndex >= 0)
            {
                //Try to..
                try
                {
                    //Obtain our offset for our chunk
                    int chunkOffset = ReflexiveData.Pointer + (comboChunks.SelectedIndex * ReflexiveData.Size);
                    //Load values
                    MetaEditorHandler.LoadPluginValues(Map, pnlValues, chunkOffset);
                    //Close the IO
                    Map.CloseIO();
                }
                //Incase of an error..
                catch
                {
                    //Show our error
                    MessageBox.Show("Could not load values for Reflexive/Structure: " + ReflexiveData.Name);
                }
            }
        }

        private void copyPointerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Clear the clipboard
            Clipboard.Clear();
            //Set the text
            Clipboard.SetText("0x" + ReflexiveData.Pointer.ToString("X"));
        }

        private void copySelectedChunkOffsetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Clear the clipboard
            Clipboard.Clear();
            //Get our chunk offset
            int chunkOffset = ReflexiveData.Pointer + (comboChunks.SelectedIndex*ReflexiveData.Size);
            //Set the text
            Clipboard.SetText("0x" + chunkOffset.ToString("X"));
        }

        private void copySizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Clear the clipboard
            Clipboard.Clear();
            //Set the text
            Clipboard.SetText("0x" + ReflexiveData.Size.ToString("X"));
        }

        public void UnloadMetaEditor()
        {
            foreach (Control control in pnlValues.Controls)
            {
                control.Dispose();
            }
        }

        private void disableReflexiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //If we are enabled...
            if (pnlValues.Enabled)
            {
                //Show our warning.
                if (MessageBox.Show("Warning: Doing this will be irreversable. It will set the chunkcount to 0. Would you like to continue?", "Warning!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //Open our IO
                    Map.OpenIO();

                    //Go to the reflexive offset
                    Map.IO.Out.BaseStream.Position = lastRealOffset;

                    //Write our 0 chunkcount
                    Map.IO.Out.Write((int)0);
                    Map.IO.Out.Write((int)0);

                    //Disable the combobox
                    comboChunks.Enabled = false;
                    //Clear our combobox items
                    comboChunks.Items.Clear();
                    //Clear the combobox text if any remaining...
                    comboChunks.Text = "";
                    //Disable the value panel.
                    pnlValues.Enabled = false;

                    //Close our IO.
                    Map.CloseIO();
                }
            }
        }

        private void chunkDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Set our chunk name.
            ChunkClipboard.Name = ReflexiveData.Name;

            //Set our chunk size
            ChunkClipboard.Size = ReflexiveData.Size;

            //Open our IO
            Map.OpenIO();

            //Go to the chunk offset
            Map.IO.In.BaseStream.Position = ReflexiveData.Pointer + (comboChunks.SelectedIndex * ReflexiveData.Size);

            //Read our data
            ChunkClipboard.Chunk = Map.IO.In.ReadBytes(ChunkClipboard.Size);

            //Close our IO.
            Map.CloseIO();
        }

        private void chunkDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check our chunk data
            if (ChunkClipboard.Chunk == null)
                MessageBox.Show("There is no copied chunk.");

            //Check our name and size.
            if (ChunkClipboard.Name != ReflexiveData.Name | ChunkClipboard.Size != ReflexiveData.Size)
                MessageBox.Show("This chunk does not belong to this reflexive.");
       

            //Open our IO
            Map.OpenIO();

            //Go to the chunk offset
            Map.IO.Out.BaseStream.Position = ReflexiveData.Pointer + (comboChunks.SelectedIndex * ReflexiveData.Size);

            //Write our data
            Map.IO.Out.Write(ChunkClipboard.Chunk);

            //Obtain our offset for our chunk
            int chunkOffset = ReflexiveData.Pointer + (comboChunks.SelectedIndex * ReflexiveData.Size);
            
            //Load values
            MetaEditorHandler.LoadPluginValues(Map, pnlValues, chunkOffset);

            //Set all values as editted.
            MetaEditorHandler.SetAllAsEditted(Map, pnlValues, chunkOffset);

            //Close our IO.
            Map.CloseIO();
        }

        private void disableChunkToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void chunkDataForAllChunksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check our chunk data
            if (ChunkClipboard.Chunk == null)
                MessageBox.Show("There is no copied chunk.");

            //Check our name and size.
            if (ChunkClipboard.Name != ReflexiveData.Name | ChunkClipboard.Size != ReflexiveData.Size)
                MessageBox.Show("This chunk does not belong to this reflexive.");


            //Open our IO
            Map.OpenIO();
            for (int i = 0; i < ReflexiveData.ChunkCount; i++)
            {

                //Go to the chunk offset
                Map.IO.Out.BaseStream.Position = ReflexiveData.Pointer + (i * ReflexiveData.Size);

                //Write our data
                Map.IO.Out.Write(ChunkClipboard.Chunk);
            }

            //Obtain our offset for our chunk
            int chunkOffset = ReflexiveData.Pointer + (comboChunks.SelectedIndex * ReflexiveData.Size);

            //Load values
            MetaEditorHandler.LoadPluginValues(Map, pnlValues, chunkOffset);

            //Set all values as editted.
            MetaEditorHandler.SetAllAsEditted(Map, pnlValues, chunkOffset);

            //Close our IO.
            Map.CloseIO();
        }

        private void offsetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Clear the clipboard
            Clipboard.Clear();
            //Set the text
            Clipboard.SetText("0x" + this.lastRealOffset.ToString("X"));
        }
    }
}
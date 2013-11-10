using System;
using System.Windows.Forms;
using Alteration.Forms;
using Alteration.Halo_3.Meta_Grid.Ident_Swap;
using Alteration.Halo_3.Meta_Parser;
using Alteration.Settings;
using HaloDeveloper.Map;
using HaloDevelopmentExtender;

namespace Alteration.Halo_3.Meta_Grid
{
    public partial class MetaGrid : UserControl
    {
        private HaloMap _map;

        public MetaGrid(MetaParser metaParser)
        {
            InitializeComponent();

            #region Structures

            //Loop through all the structures
            for (int i = 0; i < metaParser.Structures.Count; i++)
            {
                //Initialize a new listViewItem
                ListViewItem lvi = new ListViewItem();
                //Assign the name
                lvi.SubItems[0].Text = metaParser.Structures[i].Name;
                //Add the offset
                lvi.SubItems.Add("0x" + metaParser.Structures[i].Offset.ToString("X"));
                //Add the count
                lvi.SubItems.Add("0x" + metaParser.Structures[i].Count.ToString("X"));
                //Add the pointer
                lvi.SubItems.Add("0x" + metaParser.Structures[i].Pointer.ToString());
                //Add the size
                lvi.SubItems.Add("0x" + metaParser.Structures[i].Size.ToString("X"));
                //Add it to the listView
                reflexiveGrid.Items.Add(lvi);
            }

            #endregion

            #region Idents

            //Loop through all the idents
            for (int i = 0; i < metaParser.Idents.Count; i++)
            {
                //Initialize a new listViewItem
                ListViewItem lvi = new ListViewItem();
                //Assign the name
                lvi.SubItems[0].Text = metaParser.Idents[i].Name;
                //Assign the offset
                lvi.SubItems.Add("0x" + metaParser.Idents[i].Offset.ToString("X"));
                //Assign the TagClass
                lvi.SubItems.Add(metaParser.Idents[i].TagClass);
                //Assign the TagName
                lvi.SubItems.Add(metaParser.Idents[i].TagName);
                //Add it to the listView
                identGrid.Items.Add(lvi);
            }

            #endregion

            #region Strings
            //Loop through all the idents
            for (int i = 0; i < metaParser.Strings.Count; i++)
            {
                //Initialize a new listViewItem
                ListViewItem lvi = new ListViewItem();
                //Assign the name
                lvi.SubItems[0].Text = metaParser.Strings[i].Name;
                //Assign the offset
                lvi.SubItems.Add("0x" + metaParser.Strings[i].Offset.ToString("X"));
                //Assign the string index
                lvi.SubItems.Add(metaParser.Strings[i].StringIndex.ToString());
                //Assign the string id
                lvi.SubItems.Add("0x" + metaParser.Strings[i].Identifier.ToString("X"));
                //Assign the string name
                lvi.SubItems.Add(metaParser.Strings[i].StringName);
                //Add it to the listView
                stringGrid.Items.Add(lvi);
            }
            #endregion

            #region Finishing Touches

            //Dock all grids.
            reflexiveGrid.Dock = DockStyle.Fill;
            identGrid.Dock = DockStyle.Fill;
            stringGrid.Dock = DockStyle.Fill;

            //Determine the last viewed grid
            switch (AppSettings.Settings.LastGridView)
            {
                case Settings.Settings.LastGridViews.Structure:
                    {
                        //Select the structure list
                        SelectStructureList();
                        break;
                    }
                case Settings.Settings.LastGridViews.Ident:
                    {
                        //Select the ident list
                        SelectIdentList();
                        break;
                    }
                case Alteration.Settings.Settings.LastGridViews.Strings:
                    {
                        //Select the string list
                        SelectStringList();
                        break;
                    }
            }

            //resize columns for all grids
            reflexiveGrid.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            identGrid.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            stringGrid.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            #endregion

            //Assign our instance of the HaloMap class
            Map = metaParser.Map;
        }

        public HaloMap Map
        {
            get { return _map; }
            set { _map = value; }
        }

        private void SelectStructureList()
        {
            structuresToolStripMenuItem.Checked = true;
            identsToolStripMenuItem.Checked = false;
            structuresToolStripMenuItem1.Checked = true;
            identsToolStripMenuItem1.Checked = false;
            stringsToolStripMenuItem.Checked = false;
            stringsToolStripMenuItem1.Checked = false;
            stringGrid.Visible = false;
            //Make the structure visible
            reflexiveGrid.Visible = true;
            //make the idents invisible
            identGrid.Visible = false;
            //Set the structure list as the last viewed
            AppSettings.Settings.LastGridView = Settings.Settings.LastGridViews.Structure;
            //Save our settings.
            AppSettings.SaveSettings();
        }

        private void SelectIdentList()
        {
            structuresToolStripMenuItem.Checked = false;
            identsToolStripMenuItem.Checked = true;
            stringsToolStripMenuItem.Checked = false;
            stringsToolStripMenuItem1.Checked = false;
            stringGrid.Visible = false;
            structuresToolStripMenuItem1.Checked = false;
            identsToolStripMenuItem1.Checked = true;
            //Make the structure visible
            reflexiveGrid.Visible = false;
            //make the idents invisible
            identGrid.Visible = true;
            //Set the ident list as the last grid
            AppSettings.Settings.LastGridView = Settings.Settings.LastGridViews.Ident;
            AppSettings.SaveSettings();
        }

        private void SelectStringList()
        {
            structuresToolStripMenuItem.Checked = false;
            identsToolStripMenuItem.Checked = false;
            structuresToolStripMenuItem1.Checked = false;
            identsToolStripMenuItem1.Checked = false;
            stringsToolStripMenuItem.Checked = true;
            stringsToolStripMenuItem1.Checked = true;
            stringGrid.Visible = true;
            //Make the structure visible
            reflexiveGrid.Visible = false;
            //make the idents invisible
            identGrid.Visible = false;
            //Set the structure list as the last viewed
            AppSettings.Settings.LastGridView = Settings.Settings.LastGridViews.Strings;
            //Save our settings.
            AppSettings.SaveSettings();
        }

        private void structuresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectStructureList();
        }

        private void identsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectIdentList();
        }

        private void structuresToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectStructureList();
        }

        private void identsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectIdentList();
        }

        private void swapSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Only continue if theres atleast one item selected
            if (identGrid.SelectedItems.Count > 0)
            {
                //Initialize our ident Swapper
                IdentSwapper identSwapper = new IdentSwapper(Map, identGrid);
                //Show the identSwapper
                identSwapper.ShowDialog();
            }
        }

        private void identGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void GoToTag(bool inNewTab)
        {
            //Initialize our parent control
            Control parent = this;

            //Create an instance of MapForm which we will obtain
            MapForm ParentMapForm = null;

            //Loop
            while (true)
            {
                //If our parent's parent is null
                if (parent.Parent == null)
                {
                    //Break out of the loop.
                    break;
                }
                //Set our parent as the previous control's parent.
                parent = parent.Parent;
                //If our parent is the map form
                if (parent.GetType().ToString() == "Alteration.Forms.MapForm")
                {
                    //Set our map form instance
                    ParentMapForm = (MapForm)parent;
                    //Break out of the loop
                    break;
                }
            }

            //If our parent map form is null..
            if (ParentMapForm == null)
            {
                //Stop processing code
                return;
            }
            //Otherwise...

            //If we are to make a new tab
            if (inNewTab)
                //Make it.
                ParentMapForm.CreateNewDocument();

            //Go to the tag
            ParentMapForm.GotoTag(identGrid.SelectedItems[0].SubItems[2].Text,
                                  identGrid.SelectedItems[0].SubItems[3].Text);
        }

        private void goToSelectedTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
           //Go to our tag
            GoToTag(false);
        }

        private void menuButtonItem1_Activate(object sender, EventArgs e)
        {
            SelectStructureList();
        }

        private void menuButtonItem2_Activate(object sender, EventArgs e)
        {
            SelectIdentList();
        }

        private void menuBarItem2_BeforePopup(object sender, TD.SandBar.MenuPopupEventArgs e)
        {
            //If the XDK Name isn't blank
            if (AppSettings.Settings.XDKName != "")
            {
                //Initialize our Xbox Debug Communicator, with our Xbox Name
                XboxDebugCommunicator Xbox_Debug_Communicator = new XboxDebugCommunicator(AppSettings.Settings.XDKName);
                //Connect
                Xbox_Debug_Communicator.Connect();
                //Get the memoryStream
                XboxMemoryStream xbms = Xbox_Debug_Communicator.ReturnXboxMemoryStream();
                //Endian IO
                EndianIO IO = new EndianIO(xbms, EndianType.BigEndian);
                IO.Open();
                //Loop through every ident
                for (int i = 0; i < identGrid.Items.Count; i++)
                {
                    //Get the offset
                    int offset = int.Parse(identGrid.Items[i].SubItems[1].Text.Substring(2),System.Globalization.NumberStyles.HexNumber) + Map.MapHeader.mapMagic;
                    try
                    {
                        //Get the tagclass and name
                        string tagClass = identGrid.Items[i].SubItems[2].Text;
                        string tagName = identGrid.Items[i].SubItems[3].Text;
                        //Get our tag ID
                        int tagID = Map.IndexItems[Map.GetTagIndexByClassAndName(tagClass, tagName)].Ident;
                        //Make our IO go to the offset
                        IO.Out.BaseStream.Position = offset;
                        //Write our tagclass
                        IO.Out.WriteAsciiString(tagClass, 4);
                        //Skip 8 bytes
                        IO.Out.BaseStream.Position += 8;
                        //Write our ID
                        IO.Out.Write(tagID);
                    }
                    catch
                    {
                    }
                }
                IO.Close();
                xbms.Close();
                //Disconnect
                Xbox_Debug_Communicator.Disconnect();
                MessageBox.Show("Done.");
            }
            else
            {
                //Show our error dialog
                MessageBox.Show("XDK Name/IP was not set. Please set it before continuing.");
                //Stop processing code.
                return;
            }
        }

        private void inNewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoToTag(true);
        }

        private void stringsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectStringList();
        }

        private void stringsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectStringList();
        }

        private void menuButtonItem3_Activate(object sender, EventArgs e)
        {
            SelectStringList();
        }
    }
}
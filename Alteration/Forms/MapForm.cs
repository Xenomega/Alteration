using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Alteration.Halo_3.Meta_Editor;
using Alteration.Halo_3.Meta_Grid;
using Alteration.Halo_3.Meta_Parser;
using Alteration.Halo_3.Plugins;
using Alteration.Halo_3.Raw;
using Alteration.Halo_3.String___Locale;
using Alteration.Settings;
using AlterationLib;
using HaloDeveloper.Map;
using TD.SandDock;
using TD.SandBar;
using Alteration.Forms.Dialog;
using Alteration.Halo_3.Map_File.Misc.Dialogs;
using HaloDeveloper.Raw;
using Alteration.Forms.Dialog.Map_Form;
using HaloDeveloper.Raw.BitmapRaw;
using Alteration.Halo_3.Map_File.Building;

namespace Alteration.Forms
{
    public partial class MapForm : Form
    {
        private LibraryLoader _libraryloader;
        private HaloMap _map;

        private bool ChangingTab;
        private bool Painting = true;

        public MapForm(HaloMap map)
        {
            //Initialize our component
            InitializeComponent();
            //Set the Map instance to this paramater of map.
            Map = map;
            //Create our external map IOs
            map.RawInformation.ExternalMaps.CreateIOs();
            //Load our map information(tags, strings, values) into the UI
            LoadMapInformation(true);
            //Edit the title of our form to be the location of the map.
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Text = Map.FileName;
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
            //Label all of our tags
            //PluginLayoutCreator plc = new PluginLayoutCreator(Map);
            //plc.GetMetaHeaderDataBlocksMapped(false, true);
            //MapFunctions mf = new MapFunctions(Map);
            //mf.RenameTag(3, "I've got a ballin bunch of coconuts yo.");
            Map.RawInformation.ExternalMaps.CreateIOs();
           // MapBuilder mp = new MapBuilder(map);
           //mp.InternalizeRawChunk(1035);
        }

        public HaloMap Map
        {
            get { return _map; }
            set { _map = value; }
        }

        public LibraryLoader Library_Loader
        {
            get { return _libraryloader; }
            set { _libraryloader = value; }
        }

        protected override void WndProc(ref Message m)
        {
            // Code courtesy of Mark Mihevc

            // sometimes we want to eat the paint message so we don't have to see all the

            // flicker from when we select the text to change the color.
            if (m.Msg == 0x00f)
            {
                if (Painting)

                    base.WndProc(ref m); // if we decided to paint this control, just call the RichTextBox WndProc

                else

                    m.Result = IntPtr.Zero;
                //  not painting, must set this to IntPtr.Zero if not painting otherwise serious problems.
            }

            else

                base.WndProc(ref m); // message other than WM_PAINT, jsut do what you normally do.
        }

        public void LoadMapInformation(bool LoadTree)
        {

            #region Map Informational Values

            //Set the signaturebox text
            txtChecksum.Text = Map.MapHeader.Signature.ToString("X");
            //Set the IndexOffset text
            txtIndexOffset.Text = Map.MapHeader.indexOffset.ToString("X");
            //Set the mapMagic Text
            txtMapMagic.Text = Map.MapHeader.mapMagic.ToString("X");
            //Set the mapMagicBaseAddrModifier1 Text
            txtRawStart.Text = Map.MapHeader.RawTableOffset.ToString("X");
            //Set the mapMagicBaseAddrModifier2 Text
            txtRawTableSize.Text = Map.MapHeader.RawTableSize.ToString("X");
            //Set the mapMagicBase Text
            txtBaseMagic.Text = Map.MapHeader.mapMagicBaseAddress.ToString("X");
            //Set the mapInternalName text
            txtMapName.Text = Map.MapHeader.internalName;
            //Set the scenario name text
            txtScenario.Text = Map.MapHeader.scenarioName;
            //Set the string count text
            txtStringCount.Text = Map.MapHeader.stringTableCount.ToString();

            #endregion

            if (LoadTree)
            {
                #region Load Treeview

                //Load the tags into the treeView
                Map.LoadTagsIntoTreeview(tvMetaTree);

                //Loop through each class
                foreach (TreeNode node in tvMetaTree.Nodes)
                {
                    //Set our imageindex as the folder
                    node.ImageIndex = 11;

                    //Set our open folder image index
                    node.SelectedImageIndex = 12;

                    //Loop through each tag
                    foreach (TreeNode node2 in node.Nodes)
                    {
                        //Set our imageindex as the folder
                        node2.ImageIndex = 15;

                        //Set our open folder image index
                        node2.SelectedImageIndex = 15;
                    }
                }
                //Label our treenodes
                LabelTreeNodes();
                #endregion
            }


        }

        public void LoadMetaInformation(int tagIndex)
        {
            //Set the tagClass text
            txtTagClass.Text = Map.IndexItems[tagIndex].Class;
            //Set the tagName text
            txtTagName.Text = Map.IndexItems[tagIndex].Name;
            //Set the identifier text
            txtTagIdentifier.Text = Map.IndexItems[tagIndex].Ident.ToString("X");
            //Set the offset text
            txtTagOffset.Text = Map.IndexItems[tagIndex].Offset.ToString("X");

#if DEBUG
            //If our tag is that of a raw type..
            if (txtTagClass.Text == "sbsp" |
                txtTagClass.Text == "bitm" |
                txtTagClass.Text == "mode" |
                txtTagClass.Text == "jmad" |
                txtTagClass.Text == "snd!" |
                txtTagClass.Text == "pmdf" |
                txtTagClass.Text == "sLdT")
            {

               
                //Toggle our visibility.
                //xpanelRawInformation.Visible = true;
            }
            else
            {
                //Toggle our visibility.
                //xpanelRawInformation.Visible = false;
            }
#endif
        }

        private void tvMetaTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Set our priority
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            
            //Get our  tag Index
            int tagIndex = GetSelectedTagIndex();
            
            //If we have a selected and valid tag...
            if (tagIndex != -1)
            {
                //Load the editor using our tag Index
                LoadEditor(tagIndex);
            }
            
            //Get our checked menu item
            MenuButtonItem selectedLibrary = null;
            
            //Loop through all the libraries
            for (int i = 0; i < menuButtonItem3.Items.Count; i++)
            {
                //If the item is checked
                if (menuButtonItem3.Items[i].Checked)
                {
                    //Set our selected library
                    selectedLibrary = menuButtonItem3.Items[i];
                    
                    //break out of the loop.
                    break;
                }
            }

            //Reload our Libraries
            LoadLibraries();
            
            //If we had a library selected before...
            if (selectedLibrary != null)
            {
                //Load our selected Library once more
                Library_Activate(selectedLibrary, new EventArgs());
            }

            
            //Focus on our treeView
            tvMetaTree.Focus();
        }

        private int GetSelectedTagIndex()
        {
            //Start with a null index
            int index = -1;
            //If there is a selected item
            if (tvMetaTree.SelectedNode != null)
            {
                //If it has a parent node
                if (tvMetaTree.SelectedNode.Parent != null)
                {
                    //Then get its index by ident
                    index = Map.GetTagIndexByIdent(int.Parse(tvMetaTree.SelectedNode.Tag.ToString()));
                }
            }
            //Return the index
            return index;
        }

        public void LoadEditor(int tagIndex)
        {
            //Try to..
            try
            {
                //Obtain an instance of the meta editor container.
                MetaEditorContainer existingMetaEditor = (MetaEditorContainer)documentContainer1.ActiveDocument.Controls[0];
            }
            catch
            {
                try
                {
                    MetaGrid MG = (MetaGrid)documentContainer1.ActiveDocument.Controls[0];
                }
                catch
                {
                    //We failed.. its not a meta editor.
                    //Try disposing our current library
                    try
                    {
                        //Dispose our current
                        documentContainer1.ActiveDocument.Controls[0].Dispose();
                    }
                    catch { }
                }
            }

            //If its not null tag Index
            if (tagIndex != -1)
            {
                //Load our Meta Information
                LoadMetaInformation(tagIndex);
                //If we're changing tabs
                if (ChangingTab)
                {
                    //Stop code
                    return;
                }
                Painting = false;
                //Determine the editor to use and load it.

                #region Meta Grid

                if (menuButtonMetaGrid.Checked)
                {
                    //Initialize our metaParser
                    MetaParser metaParser = new MetaParser(Map, tagIndex);

                    //Get our plugin path.
                    string pluginPath = "";

                    //If it's retail halo 3
                    if (Map.Halo_Map_Version == HaloMap.HaloMapVersion.Halo3Retail)
                        pluginPath = Application.StartupPath + "\\plugins\\halo 3\\" +
                                             Map.IndexItems[tagIndex].Class.Replace(" ", "").Replace("<", "_").Replace(">", "_") +
                                             ".alt";
                    else if (Map.Halo_Map_Version == HaloMap.HaloMapVersion.Halo3ODST)
                        pluginPath = Application.StartupPath + "\\plugins\\odst\\" +
                             Map.IndexItems[tagIndex].Class.Replace(" ", "").Replace("<", "_").Replace(">", "_") +
                             ".alt";

                    //Initialize our XmlParser
                    XmlParser xmlparser = new XmlParser();
                    //Parse our xml
                    xmlparser.ParsePlugin(pluginPath);
                    //Parse our meta
                    metaParser.ParseMeta(xmlparser);
                    //Initialize our instance of metaGrid
                    MetaGrid metaGrid = new MetaGrid(metaParser);

                    //Create a new document which we will load in, or grab the selected.
                    DockControl document = GetCurrentSelectedDocument(true);

                    //Clear all controls on this document
                    document.Controls.Clear();

                    //Get our tag last index of name.
                    string[] temporaryString = Map.IndexItems[tagIndex].Name.Split('\\');
                    string lastIndexName = temporaryString[temporaryString.Length - 1];

                    //Set our document text.
                    document.Text = lastIndexName + "." + Map.IndexItems[tagIndex].Class;

                    //Set our document tag to our tag Index.
                    document.Tag = tagIndex;

                    //Add the meta editor to the document
                    document.Controls.Add(metaGrid);
                    //Set our metaEditorContainer to dock.
                    metaGrid.Dock = DockStyle.Fill;
                    //Select it as the active document.
                    documentContainer1.ActiveDocument = document;
                }

                #endregion

                #region Meta Editor

                if (menuButtonMetaEditor.Checked)
                {
                    //Create a new document which we will load in.
                    DockControl document = GetCurrentSelectedDocument(true);

                    //Get our tag last index of name.
                    string[] temporaryString = Map.IndexItems[tagIndex].Name.Split('\\');
                    string lastIndexName = temporaryString[temporaryString.Length - 1];

                    //Set our document text.
                    document.Text = lastIndexName + "." + Map.IndexItems[tagIndex].Class;

                    //Set our document tag to our tag Index.
                    document.Tag = tagIndex;

                    //Try to...
                    try
                    {
                        //Obtain an instance of the meta editor container.
                        MetaEditorContainer existingMetaEditor = (MetaEditorContainer)document.Controls[0];
                        //Switch our tag in our existing meta editor
                        existingMetaEditor.SwitchToTag(tagIndex);
                    }
                    //If its not an existing editor.
                    catch
                    {
                        //Clear all controls on this document
                        document.Controls.Clear();

                        //Create our instance of the Meta Editor container.
                        MetaEditorContainer metaEditorContainer = new MetaEditorContainer(Map);
                        //Add the meta editor to the document
                        document.Controls.Add(metaEditorContainer);
                        //Set our metaEditorContainer to dock.
                        metaEditorContainer.Dock = DockStyle.Fill;
                        //Select it as the active document.
                        documentContainer1.ActiveDocument = document;
                        //Load the meta Editor controls and values
                        metaEditorContainer.LoadMetaEditor(tagIndex, AppSettings.Settings.ShowInvisibles);
                    }
                }

                #endregion

                #region Bitmap Editor

                //Decide whether to show our menu item.
                menuBitmapEditor.Visible = (Map.IndexItems[tagIndex].Class == "bitm");
                //If our menuBitmapEditor is invisible.
                if (menuBitmapEditor.Visible && menuBitmapEditor.Checked)
                {
                    //Create a new document which we will load in, or grab the selected.
                    DockControl document = GetCurrentSelectedDocument(true);

                    //Clear all controls on this document
                    document.Controls.Clear();

                    //Get our tag last index of name.
                    string[] temporaryString = Map.IndexItems[tagIndex].Name.Split('\\');
                    string lastIndexName = temporaryString[temporaryString.Length - 1];

                    //Set our document text.
                    document.Text = lastIndexName + "." + Map.IndexItems[tagIndex].Class;

                    //Set our document tag to our tag Index.
                    document.Tag = tagIndex;

                    //Create our bitmap viewer
                    BitmapViewer bitmap_viewer = new BitmapViewer();


                    //Add the bitmap editor to the document
                    document.Controls.Add(bitmap_viewer);

                    //Load our bitmap stuff..
                    bitmap_viewer.LoadBitmapTag(Map, tagIndex);
                    bitmap_viewer.Dock = DockStyle.Fill;

                    //Select it as the active document.
                    documentContainer1.ActiveDocument = document;
                }

                #endregion

                Painting = true;
            }
        }

        private void SelectEditorType(ButtonItemBase menuButtonItem)
        {
            //Loop through all the menu bar items.
            for (int i = 0; i < menuBarItem1.Items.Count; i++)
            {
                //Uncheck all of them.
                menuBarItem1.Items[i].Checked = false;
            }
            //Loop through all the libraries
            for (int i = 0; i < menuButtonItem3.Items.Count; i++)
            {
                //Uncheck all items
                menuButtonItem3.Items[i].Checked = false;
            }
            //Check the provided MenuButtonItem
            menuButtonItem.Checked = true;
        }

        private void menuButtonItem1_Activate(object sender, EventArgs e)
        {
            SelectEditorType(menuButtonMetaGrid);
            LoadEditor(GetSelectedTagIndex());
        }

        private void menuButtonItem2_Activate(object sender, EventArgs e)
        {
            SelectEditorType(menuButtonMetaEditor);
            LoadEditor(GetSelectedTagIndex());
        }

        private void LoadLibraries()
        {
            //Clear out menuItems from before
            menuButtonItem3.Items.Clear();
            //Initialize our Library_Loader
            Library_Loader = new LibraryLoader();
            //Initialize our Library List
            Library_Loader.Libraries = new List<LibraryLoader.LibraryClass>();
            //Load our Libraries
            Library_Loader.LoadLibraries(Application.StartupPath + "\\Libraries\\");
            //Loop through the Libraries
            foreach (LibraryLoader.LibraryClass Alteration_Library in Library_Loader.Libraries)
            {
                #region Determine Valid For this Tag

                bool invalidForThisTag = false;
                if (Alteration_Library.Library.TagClassFilter != "")
                {
                    if (tvMetaTree.SelectedNode == null)
                    {
                        invalidForThisTag = true;
                    }
                    else
                    {
                        if (tvMetaTree.SelectedNode.Parent == null)
                        {
                            invalidForThisTag = true;
                        }
                        else
                        {
                            List<string> tagClasses =
                                new List<string>(Alteration_Library.Library.TagClassFilter.Split(','));
                            if (tagClasses.Contains(tvMetaTree.SelectedNode.Parent.Tag.ToString()) == false &&
                                Alteration_Library.Library.TagClassFilter != "")
                            {
                                invalidForThisTag = true;
                            }
                        }
                    }
                }
                //If the library isn't for this tag..
                if (invalidForThisTag)
                {
                    //Set the tag to be null
                    Alteration_Library.Library.Selected_Tag_Index = -1;
                    //Load the next library
                    goto NextLibrary;
                }

                #endregion

                try
                {
                    //Set our instance of the selected tag
                    Alteration_Library.Library.Selected_Tag_Index =
                            Map.GetTagIndexByClassAndName(tvMetaTree.SelectedNode.Parent.Tag.ToString(),
                                                          tvMetaTree.SelectedNode.Text);
                }
                catch
                {
                    //If it errored, the set our instance of a null
                    Alteration_Library.Library.Selected_Tag_Index = -1;
                }
                //Set the instance of the map
                Alteration_Library.Library.Map_Location = Map.FileName;
                //Add the menu item for it using the Library Name
                menuButtonItem3.Items.Add(Alteration_Library.Library.LibraryName);
                //Set the function for when it is activated
                menuButtonItem3.Items[menuButtonItem3.Items.Count - 1].Activate += Library_Activate;
            //Set our EntryPoint for the next Library
            NextLibrary:
                ;
            }
        }

        private void Library_Activate(object sender, EventArgs e)
        {

            //Loop through all the Libraries
            foreach (LibraryLoader.LibraryClass Alteration_Library in Library_Loader.Libraries)
            {
                //If the indexed library matches the library name we clicked...
                if (Alteration_Library.Library.LibraryName == ((MenuButtonItem)sender).Text)
                {
                    //Get our existing tab, or create it.
                    DockControl libdoc = GetCurrentSelectedDocument(true);
                    //For each control on the document, clear it.
                    libdoc.Controls.Clear();
                    //Set its text to be the library name
                    libdoc.Text = "Library: " + Alteration_Library.Library.LibraryName;
                    //Set its indicating name to be a library document
                    libdoc.Name = "Library";
                    //Set our tag to indicate it's a library.
                    libdoc.Tag = "Library";
                    //Make our document autoScrollAble
                    libdoc.AutoScroll = true;
                    //Add the control to this document.
                    libdoc.Controls.Add(Alteration_Library.Library);
                    //Select it as the active document
                    documentContainer1.ActiveDocument = libdoc;
                    //Uncheck all menu items, except our selected one
                    for (int i = 0; i < menuButtonItem3.Items.Count; i++)
                    {
                        menuButtonItem3.Items[i].Checked = menuButtonItem3.Items[i].Text ==
                                                           Alteration_Library.Library.LibraryName;
                    }
                    //Uncheck all our editors
                    for (int i = 0; i < menuBarItem1.Items.Count; i++)
                    {
                        menuBarItem1.Items[i].Checked = false;
                    }
                }
            }
        }

        private void MapForm_Load(object sender, EventArgs e)
        {
            //Load our Libraries
            LoadLibraries();
        }

        private void LabelTreeNodes()
        {
            //Initialize our TagListFilename...
            string TagListFileName = Application.StartupPath + "\\TagList.txt";
            //if the file exists...
            if (File.Exists(TagListFileName))
            {
                //Open our stream reader
                StreamReader sr = new StreamReader(new FileStream(TagListFileName, FileMode.Open));
                //Get our lines, close our stream
                RichTextBox rtb = new RichTextBox();
                rtb.Text = sr.ReadToEnd();
                sr.Close();
                //use them in a list
                //Initialize our string list
                List<string> labelEntry = new List<string>(rtb.Lines);
                //Loop through all the entries..
                for (int i = 0; i < labelEntry.Count; i++)
                {
                    //Make our temporary string
                    string[] temp = labelEntry[i].Split(',');
                    //Assign our tagclass
                    string tagClass = temp[0].Replace("\"", "");
                    //Lets now get our tagDescription
                    string tagDescription = "";
                    //Loop through all the parts..
                    for (int z = 1; z < temp.Length; z++)
                    {
                        //Add to the description
                        tagDescription += temp[z].Replace("\"", "");
                    }
                    //Now loop through all the treeView nodes and label the nodes.
                    for (int z = 0; z < tvMetaTree.Nodes.Count; z++)
                    {
                        //If the text is that of this tagClass
                        if (tvMetaTree.Nodes[z].Text == tagClass)
                        {
                            //Assign it it's new name
                            tvMetaTree.Nodes[z].Text = "[" + tagClass + "] - " + tagDescription;
                        }
                    }
                }
            }
        }

        private void btnLocaleStringEditor_Click(object sender, EventArgs e)
        {
            //Create a new document, or get the current.
            DockControl document = GetCurrentSelectedDocument(true);
            //Clear all the stuff on it
            document.Controls.Clear();
            //Set it's text, tag and name
            const string documentName = "String & Locale Editor";
            document.Text = documentName;
            document.Tag = documentName;
            document.Name = documentName;
            //Initialize a new StringsAndLocale Editor
            StringLocaleEditor stringLocaleEditor = new StringLocaleEditor(Map);
            //Set it's dock style.
            stringLocaleEditor.Dock = DockStyle.Fill;
            //Add it to this document.
            document.Controls.Add(stringLocaleEditor);
            //Set it as the active document
            documentContainer1.ActiveDocument = document;
        }

        private void ribbonBarOtherEditors_Click(object sender, EventArgs e)
        {
        }

        private void btnRawContainer_Click(object sender, EventArgs e)
        {
            //Create a new document, or get the current.
            DockControl document = GetCurrentSelectedDocument(true);
            //Clear all the stuff on it
            document.Controls.Clear();
            //Set it's text, tag and name
            const string documentName = "Raw Table Viewer";
            document.Text = documentName;
            document.Tag = documentName;
            document.Name = documentName;
            //Initialize a new Raw Item viewer
            RawViewerControl rawViewer = new RawViewerControl(Map);
            //Set it's dock style.
            rawViewer.Dock = DockStyle.Fill;
            //Add it to this document.
            document.Controls.Add(rawViewer);
            //Set it as the active document
            documentContainer1.ActiveDocument = document;
        }

        public void GotoTag(string tagClass, string tagName)
        {
            //Loop through all the treeview nodes
            for (int i = 0; i < tvMetaTree.Nodes.Count; i++)
            {
                //If this indexed node's tag is the tagClass...
                if (tvMetaTree.Nodes[i].Tag.ToString() == tagClass)
                {
                    //Loop through all the child nodes
                    for (int z = 0; z < tvMetaTree.Nodes[i].Nodes.Count; z++)
                    {
                        //If the indexed node's text is the tagName
                        if (tvMetaTree.Nodes[i].Nodes[z].Text == tagName)
                        {
                            //Collapse all nodes
                            tvMetaTree.CollapseAll();
                            //Select it
                            tvMetaTree.SelectedNode = tvMetaTree.Nodes[i].Nodes[z];
                            //Return and stop processing any looping code here.
                            return;
                        }
                    }
                }
            }
        }

        private void documentContainer1_ActiveDocumentChanged(object sender, ActiveDocumentEventArgs e)
        {
            //Try to parse the tag as an integer.
            try
            {
                int.Parse(documentContainer1.ActiveDocument.Tag.ToString());
            }
            //Return if we catch an error
            catch
            {
                return;
            }

            //Set our changing status
            ChangingTab = true;

            //Get our index item
            HaloMap.TagItem tabbedTag = Map.IndexItems[int.Parse(documentContainer1.ActiveDocument.Tag.ToString())];

            //Loop through all treenodes
            for (int i = 0; i < tvMetaTree.Nodes.Count; i++)
            {
                //If it's class is that of this tag
                if (tvMetaTree.Nodes[i].Tag.ToString() == tabbedTag.Class)
                {
                    //Loop through all child nodes
                    for (int z = 0; z < tvMetaTree.Nodes[i].Nodes.Count; z++)
                    {
                        //If it's name is that of this tag
                        if (tvMetaTree.Nodes[i].Nodes[z].Text == tabbedTag.Name)
                        {
                            //Select that tag
                            tvMetaTree.SelectedNode = tvMetaTree.Nodes[i].Nodes[z];
                            //Break out of this loop of code
                            break;
                        }
                    }
                    //Break out of this block of code
                    break;
                }
            }

            //Set our changing status
            ChangingTab = false;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //If our selected document isn't null
            if (documentContainer1.ActiveDocument != null)
            {
                //Close it
                documentContainer1.ActiveDocument.Close();
            }
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Loop for each of our documents
            foreach (DockControl dockControl in documentContainer1.Documents)
            {
                //Close our document.
                dockControl.Close();
            }
        }

        private void closeAllButThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Loop for each of our documents
            foreach (DockControl dockControl in documentContainer1.Documents)
            {
                //If our dock control isn't our active document
                if (dockControl != documentContainer1.ActiveDocument)
                {
                    //Close our document.
                    dockControl.Close();
                }
            }
        }

        public void CreateNewDocument()
        {
            //Create our document.
            DockControl document = new DockControl();
            //Set it's text as a new document
            document.Text = "*New Document*";
            //Add it to the document list
            documentContainer1.AddDocument(document);
            //Set it as the active document
            documentContainer1.ActiveDocument = document;
        }

        private void newDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewDocument();
        }

        private DockControl GetCurrentSelectedDocument(bool createIfNotExist)
        {
            //If our selected document is null
            if (documentContainer1.ActiveDocument == null)
            {
                //If we are to create a new one
                if (createIfNotExist)
                {
                    //Create a new one.
                    CreateNewDocument();
                }
            }
            //Return our active document
            return documentContainer1.ActiveDocument;
        }

        private void menuButtonItem3_Activate(object sender, EventArgs e)
        {
        }

        private void menuButtonNewDocument_Activate(object sender, EventArgs e)
        {
            CreateNewDocument();
        }

        private void contxtDocuments_Opening(object sender, CancelEventArgs e)
        {
        }

        private void menuButtonCloseDoc_Activate(object sender, EventArgs e)
        {
            //If our selected document isn't null
            if (documentContainer1.ActiveDocument != null)
            {
                //Close it
                documentContainer1.ActiveDocument.Close();
            }
        }

        private void menuButtonCloseAllDocs_Activate(object sender, EventArgs e)
        {
            //Loop for each of our documents
            foreach (DockControl dockControl in documentContainer1.Documents)
            {
                //Close our document.
                dockControl.Close();
            }
        }

        private void menuButtonCloseAllButThis_Activate(object sender, EventArgs e)
        {
            //Loop for each of our documents
            foreach (DockControl dockControl in documentContainer1.Documents)
            {
                //If our dock control isn't our active document
                if (dockControl != documentContainer1.ActiveDocument)
                {
                    //Close our document.
                    dockControl.Close();
                }
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dockControl1_Closing(object sender, CancelEventArgs e)
        {

        }

        private void swapTagHeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Get our tag index
            int tagIndex = GetSelectedTagIndex();

            //If our tag index isn't null
            if (tagIndex != -1)
            {
                //Initialize our Tag Header Swapper
                SwapTagHeader sth = new SwapTagHeader(Map.IndexItems[tagIndex]);

                //Show the dialog
                sth.ShowDialog();
            }
        }

        private void renameTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //If our treeview has a selected item
            if (tvMetaTree.SelectedNode != null)
            {
                //If it has a parent
                if (tvMetaTree.SelectedNode.Parent != null)
                {
                    //Get our tag index
                    int tagIndex = GetSelectedTagIndex();

                    //Get our ID
                    int tagID = Map.IndexItems[tagIndex].Ident;

                    //Create our form
                    TagRenameForm tag_rename_form = new TagRenameForm(Map, tagIndex);

                    //Show the dialog
                    tag_rename_form.ShowDialog();

                    //Once it's done showing, set the bool to true so it doesnt reload the same tag
                    ChangingTab = true;

                    //Reload our tags
                    LoadMapInformation(false);

                    //Set our tagname
                    tvMetaTree.SelectedNode.Text = Map.IndexItems[tagIndex].Name;

                    //Change the bool back to false
                    ChangingTab = false;
                }
            }
        }

        private void MapForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void btnExtractRaw_Click(object sender, EventArgs e)
        {
            
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            //Create our FolderBrowserDialog
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            //If we selected a folder
            if (fbd.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void menuBitmapEditor_Activate(object sender, EventArgs e)
        {
            SelectEditorType(menuBitmapEditor);
            LoadEditor(GetSelectedTagIndex());
        }






    }
}
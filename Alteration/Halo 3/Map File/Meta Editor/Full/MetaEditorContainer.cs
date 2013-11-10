using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Alteration.Halo_3.Meta_Editor.Controls;
using Alteration.Halo_3.Meta_Editor.Loader;
using Alteration.Halo_3.Plugins;
using Alteration.Patches.Memory_Patch.RTH_Data;
using Alteration.Patches.Memory_Patch.RTH_Data.Forms;
using Alteration.Settings;
using HaloDeveloper.IO;
using HaloDeveloper.Map;
using HaloDevelopmentExtender;

namespace Alteration.Halo_3.Meta_Editor
{
    public partial class MetaEditorContainer : UserControl
    {
        private HaloMap _map;

        private int _tagindex;

        public MetaEditorContainer(HaloMap map)
        {
            InitializeComponent();

            //Set our instance of the HaloMap class
            Map = map;

        }

        public HaloMap Map
        {
            get { return _map; }
            set { _map = value; }
        }

        public int TagIndex
        {
            get { return _tagindex; }
            set { _tagindex = value; }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        public void LoadMetaEditor(int tagIndex, bool showInvisibles)
        {
            //Set our thread priority
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            //Set our instance of tag index
            TagIndex = tagIndex;

            //Refresh and suspend our layout
            Refresh();
            SuspendLayout();


            //Clear all controls
            UnloadMetaEditor();

            //Set the menu show invisibles check flag
            menuButtonItem5.Checked = showInvisibles;

            //Get our plugin path.
            string pluginPath = "";
            
            //If it's retail halo 3
            if(Map.Halo_Map_Version == HaloMap.HaloMapVersion.Halo3Retail)
            pluginPath = Application.StartupPath + "\\plugins\\halo 3\\" +
                                 Map.IndexItems[TagIndex].Class.Replace(" ", "").Replace("<", "_").Replace(">", "_") +
                                 ".alt";
            else if(Map.Halo_Map_Version == HaloMap.HaloMapVersion.Halo3ODST)
            pluginPath = Application.StartupPath + "\\plugins\\odst\\" +
                 Map.IndexItems[TagIndex].Class.Replace(" ", "").Replace("<", "_").Replace(">", "_") +
                 ".alt";

            //Create a new instance of our plugin parser.
            XmlParser xmlParser = new XmlParser();
            //Parse the plugin
            xmlParser.ParsePlugin(pluginPath);

            //Load the meta editor
            MetaEditorHandler.LoadNewPlugin(Map, xmlParser.ValueList, panelEx1, Map.IndexItems[TagIndex].Offset,
                                            showInvisibles);
            //Load the User Interface
            //MetaEditorHandler.LoadPluginUI(xmlParser.ValueList, this.panelEx1, showInvisibles);

            //Load the values
            //MetaEditorHandler.LoadPluginValues(Map, panelEx1, Map.IndexItems[TagIndex].Offset);

            //Close our IO
            Map.CloseIO();

            //Resume our layout
            ResumeLayout(true);


            //Set our thread priority
            Thread.CurrentThread.Priority = ThreadPriority.Normal;
        }

        public void SwitchToTag(int tagIndex)
        {
            //If our new tag isn't the same class as the old.
            if (Map.IndexItems[tagIndex].Class != Map.IndexItems[TagIndex].Class)
            {
                //Load a new meta editor
                LoadMetaEditor(tagIndex, menuButtonItem5.Checked);
                //Stop processing code in this stub
                return;
            }
            //Otherwise..


            //Set our thread priority
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            //Set our instance of tag index
            TagIndex = tagIndex;

            //Reload the values
            MetaEditorHandler.LoadPluginValues(Map, panelEx1, Map.IndexItems[TagIndex].Offset);

            //Close our IO
            Map.CloseIO();


            //Set our thread priority
            Thread.CurrentThread.Priority = ThreadPriority.Normal;
        }

        public void UnloadMetaEditor()
        {
            //Collect our garbage
            GC.Collect();

            //For each control we have..
            foreach (Control control in panelEx1.Controls)
            {
                //Dispose of it.
                control.Dispose();
            }

            //Collect our garbage
            GC.Collect();

            //Clear our controls
            panelEx1.Controls.Clear();
        }

        private void menuButtonItem5_Activate(object sender, EventArgs e)
        {
            //Inverse our check
            menuButtonItem5.Checked = !menuButtonItem5.Checked;

            //Reload our meta editor with the tag index and the flag indicating our Show Invisible value.
            LoadMetaEditor(TagIndex, menuButtonItem5.Checked);
        }

        private void menuBarItem1_BeforePopup(object sender, TD.SandBar.MenuPopupEventArgs e)
        {
            //Save the plugin values.
            MetaEditorHandler.SaveChangedValues(Map, panelEx1, Map.IndexItems[TagIndex].Offset);
        }

        private void menuButtonItem1_Activate(object sender, EventArgs e)
        {
            //Poke our changes
            PokeChanges();
        }

        public void PokeChanges()
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
                HaloDeveloper.IO.EndianIO IO = new HaloDeveloper.IO.EndianIO(xbms,
                    HaloDeveloper.IO.EndianType.BigEndian);
                IO.Open();
                //Poke Values
                MetaEditorHandler.PokeValues(IO, panelEx1, Map.IndexItems[TagIndex].Offset, Map.MapHeader.mapMagic,
                                             true);

                IO.Close();
                xbms.Close();
                //Disconnect
                Xbox_Debug_Communicator.Disconnect();
                MessageBox.Show("Done.");
            }
            else
            {
                MessageBox.Show(
                    "The XDK Name is not set in the settings. Please click the button on the right to show the settings and indicate your IP/Name for your Xenon Development Kit.");
            }
        }
        public void PokeAll()
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
                //Disconnect
                Xbox_Debug_Communicator.Disconnect();
                //Endian IO
                HaloDeveloper.IO.EndianIO IO = new HaloDeveloper.IO.EndianIO(xbms,
                    HaloDeveloper.IO.EndianType.BigEndian);
                IO.Open();
                //Poke Values
                MetaEditorHandler.PokeValues(IO, panelEx1, Map.IndexItems[TagIndex].Offset, Map.MapHeader.mapMagic,
                                             false);
                IO.Close();
                xbms.Close();
                MessageBox.Show("Done.");
            }
            else
            {
                MessageBox.Show(
                    "The XDK Name is not set in the settings. Please click the button on the right to show the settings and indicate your IP/Name for your Xenon Development Kit.");
            }
        }
        private void menuButtonItem2_Activate(object sender, EventArgs e)
        {
            PokeAll();
        }

        private void menuButtonItem4_Activate(object sender, EventArgs e)
        {
            //Initialize our new RTH Data
            RTHData RTH_Data = new RTHData();
            //Get our RTH Change blocks
            MetaEditorHandler.BuildRTHData(RTH_Data, panelEx1, Map.IndexItems[TagIndex].Offset, Map.MapHeader.mapMagic,
                                           false);
            //Initialize our RTH Data form
            BuildRTHDataForm Build_RTH_Data_Form = new BuildRTHDataForm(RTH_Data);
            //Show the form
            Build_RTH_Data_Form.Show();
        }

        private void menuButtonItem3_Activate(object sender, EventArgs e)
        {
            //Initialize our new RTH Data
            RTHData RTH_Data = new RTHData();
            //Get our RTH Change blocks
            MetaEditorHandler.BuildRTHData(RTH_Data, panelEx1, Map.IndexItems[TagIndex].Offset, Map.MapHeader.mapMagic,
                                           true);
            //Initialize our RTH Data form
            BuildRTHDataForm Build_RTH_Data_Form = new BuildRTHDataForm(RTH_Data);
            //Show the form
            Build_RTH_Data_Form.Show();
        }

        /*
                private void menuButtonItem6_Activate(object sender, EventArgs e)
                {
                    //Save the plugin values.
                    MetaEditorHandler.SaveChangedValues(Map, panelEx1, Map.IndexItems[TagIndex].Offset);
                    MessageBox.Show("Done.");
                }
        */

        public void RefreshBaseItems(iStruct istruct, int change)
        {
            int c = panelEx1.Controls.Count - 1;
            bool now = false;
            //Loop for every control
            for (int i = 0; i < c + 1; i++)
            {
                if (panelEx1.Controls[c - i] == istruct)
                {
                    now = true;
                }
                else
                {
                    if (now)
                    {
                        panelEx1.Controls[c - i].Location = new Point(0, panelEx1.Controls[c - i].Location.Y + change);
                    }
                }
            }
        }

        private void menuButtonItem6_Activate(object sender, EventArgs e)
        {

            //Reload our meta editor with the tag index and the flag indicating our Show Invisible value.
            LoadMetaEditor(TagIndex, menuButtonItem5.Checked);

        }


    }
}
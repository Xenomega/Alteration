using System;
using System.Collections.Generic;
using System.Text;
using HaloDeveloper.Map;
using Alteration.Halo_3.Plugins;
using System.Windows.Forms;

namespace Alteration.Halo_3.Map_File.Meta_Editor.Lite
{
    /// <summary>
    /// This class loads a [Lite] version of the meta editor that is superfast and uses webbrowser classes.
    /// </summary>
    public class MetaEditorLiteHandler
    {

        #region Values

        private HaloMap _map;
        /// <summary>
        /// This instance of the HaloMap class will be used for handling values.
        /// </summary>
        public HaloMap Map
        {
            get { return _map; }
            set { _map = value; }
        }

        private HaloMap.TagItem _meta;
        /// <summary>
        /// The meta instance we'll be handling.
        /// </summary>
        public HaloMap.TagItem Meta
        {
            get { return _meta; }
            set { _meta = value; }
        }

        private XmlParser _xmlparser;
        /// <summary>
        /// Out Xml_Parser instance for this meta class.
        /// </summary>
        public XmlParser Xml_Parser
        {
            get { return _xmlparser; }
            set { _xmlparser = value; }
        }

        #endregion

        #region Initialization
        public MetaEditorLiteHandler(HaloMap map, int Tag_Index)
        {
            //Set our Map instance
            Map = map;

            //Set our meta instance
            Meta = Map.IndexItems[Tag_Index];

            //Get our plugin path.
            string pluginPath = "";

            //If it's retail halo 3
            if (Map.Halo_Map_Version == HaloMap.HaloMapVersion.Halo3Retail)
                pluginPath = Application.StartupPath + "\\plugins\\halo 3\\" +
                                     Map.IndexItems[Tag_Index].Class.Replace(" ", "").Replace("<", "_").Replace(">", "_") +
                                     ".alt";
            else if (Map.Halo_Map_Version == HaloMap.HaloMapVersion.Halo3ODST)
                pluginPath = Application.StartupPath + "\\plugins\\odst\\" +
                     Map.IndexItems[Tag_Index].Class.Replace(" ", "").Replace("<", "_").Replace(">", "_") +
                     ".alt";

            //Create a new instance of our plugin parser.
            XmlParser xmlParser = new XmlParser();
            //Parse the plugin
            xmlParser.ParsePlugin(pluginPath);
        }
        #endregion

    }
}

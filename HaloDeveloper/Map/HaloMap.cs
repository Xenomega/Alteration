using System;
using System.Collections.Generic;
using HaloDeveloper.IO;
using HaloDeveloper.Helpers;
using System.Windows.Forms;
using HaloDeveloper.Raw;

namespace HaloDeveloper.Map
{
    /// <summary>
    /// A Halo 3 Map Class Developed by Xenon.7 in Visual C# 2005.
    /// </summary>
    public class HaloMap
    {
        #region Values
        private HaloMapVersion _halomapversion;
        /// <summary>
        /// Our Halo Map Version.
        /// </summary>
        public HaloMapVersion Halo_Map_Version
        {
            get { return _halomapversion; }
            set { _halomapversion = value; }
        }

        private string fileName;
        /// <summary>
        /// The FileName or Location of the Halo 3 Map File
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public string MapDirectory { get { return FileName.Substring(0, FileName.LastIndexOf('\\') + 1); } }

        private H3MapHeader mapHeader;
        /// <summary>
        /// The Map Header Containing Map Header Information for this instance of HaloMap.
        /// </summary>
        public H3MapHeader MapHeader
        {
            get { return mapHeader; }
            set { mapHeader = value; }
        }

        private H3IndexHeader indexHeader;
        /// <summary>
        /// The Index Header Containing Index Header Information for this instance of HaloMap.
        /// </summary>
        public H3IndexHeader IndexHeader
        {
            get { return indexHeader; }
            set { indexHeader = value; }
        }

        private RawInformation rawInformation;
        /// <summary>
        /// Our raw information class containing information about raw.
        /// </summary>
        public RawInformation RawInformation
        {
            get
            {
                //Create our bool for IO opened
                bool IO_OPENED = true;
                //If our IO isn't open
                if (!IOisOpen)
                {
                    OpenIO();
                    IO_OPENED = false;
                }
                //If our string count is valid.
                if (IndexItems.Count > 0)
                    if (rawInformation == null)
                        rawInformation = new RawInformation(this);
                //If our IO was closed before, close it
                if (IO_OPENED == false)
                    CloseIO();

                return rawInformation;
            }
            set { rawInformation = value; }
        }

        private List<TagItem> indexItems;
        /// <summary>
        /// The Index Items Containing Each Tags' Information for this instance of HaloMap.
        /// </summary>
        public List<TagItem> IndexItems
        {
            get { return indexItems; }
            set { indexItems = value; }
        }

        private H3StringTable stringTable;
        /// <summary>
        /// The String Table Containing Each String's Information for this instance of HaloMap.
        /// </summary>
        public H3StringTable StringTable
        {
            get { return stringTable; }
            set { stringTable = value; }
        }


        private TagHierarchy tagHierarchy;
        /// <summary>
        /// Our tag hierarchy value used to load ident swappers and the map layout much quicker.
        /// </summary>
        public TagHierarchy TagHierarchy
        {
            get { return tagHierarchy; }
            set { tagHierarchy = value; }
        }

        private EndianIO endianIo;
        /// <summary>
        /// This is a binary tool used to read and write with specific endian styles, and extended functions, to streams.
        /// 
        /// It may be opened using the OpenIO() or CloseIO() functions within this class.
        /// </summary>
        public EndianIO IO
        {
            get { return endianIo; }
            set { endianIo = value; }
        }

        private bool ioIsOpen;
        /// <summary>
        /// This is a boolean which determined if the IO is open.
        /// </summary>
        public bool IOisOpen
        {
            get { return ioIsOpen; }
        }
        #endregion
        #region Initialization
        public HaloMap(string FileName)
        {
            //Set our FileName based off the given parameter.
            fileName = FileName;
            //Reload
            Reload();
        }
        #endregion
        #region Functions
        /// <summary>
        /// This function will open the IO in which you can read and write to the map file.
        /// </summary>
        public void OpenIO()
        {
            IO = new EndianIO(FileName, EndianType.BigEndian);
            IO.Open();
            ioIsOpen = true;
        }
        /// <summary>
        /// This function will close the IO in which you can read and write to the map file.
        /// </summary>
        public void CloseIO()
        {
            if (IO != null)
            {
                IO.Close();
            }
            IO = null;
            ioIsOpen = false;
        }
        /// <summary>
        /// This will reload the HaloMap class and all of it's values.
        /// </summary>
        public void Reload()
        {
            //Open our IO
            OpenIO();
            //Initialize our Map Header
            mapHeader = new H3MapHeader(this);
            //Initialize our Index Header
            indexHeader = new H3IndexHeader(this);
            //Initialize our string table to load Strings.
            stringTable = new H3StringTable(this);
            //Initialize our tag hierarchy
            tagHierarchy = new TagHierarchy(this);
            //Close our IO Now that we are finished
            CloseIO();
        }
        /// <summary>
        /// This function will load the appropriate tags into a treeView to view.
        /// </summary>
        /// <param name="treeView"></param>
        public void LoadTagsIntoTreeview(TreeView treeView)
        {
            //Clear our treeview
            treeView.Nodes.Clear();
            //Loop through our tag hierarchy classes
            for (int i = 0; i < TagHierarchy.TagClasses.Count; i++)
            {
                //Initialize a new treenode with the tagclass name
                TreeNode parentTagClass = new TreeNode(TagHierarchy.TagClasses[i].TagClass);
                //Assign it's tag as the tagclass
                parentTagClass.Tag = TagHierarchy.TagClasses[i].TagClass;
                //Loop through the tagnames
                for (int z = 0; z < TagHierarchy.TagClasses[i].Tags.Count; z++)
                {
                    //Create a new node for our tag
                    TreeNode tagNode = new TreeNode(TagHierarchy.TagClasses[i].Tags[z].TagName);
                    //Set it's tag
                    tagNode.Tag = TagHierarchy.TagClasses[i].Tags[z].TagInstance.Ident;
                    //Add it to our classNode.
                    parentTagClass.Nodes.Add(tagNode);
                }
                //Add our treenode to the treeview
                treeView.Nodes.Add(parentTagClass);
            }

            //Then sort the treeView.
            treeView.Sort();
        }
        public void LoadClassesIntoTreeview(TreeView treeView, string[] Classs)
        {
            //Create a list for easy look up
            List<string> tagClassList = new List<string>();
            tagClassList.AddRange(Classs);

            //Clear our treeview
            treeView.Nodes.Clear();

            //Loop through our tag hierarchy classes
            for (int i = 0; i < TagHierarchy.TagClasses.Count; i++)
            {
                //Check to see if its on the list, if not lets bounce
                if (!tagClassList.Contains(TagHierarchy.TagClasses[i].TagClass))
                    continue;

                //Initialize a new treenode with the tagclass name
                TreeNode parentTagClass = new TreeNode(TagHierarchy.TagClasses[i].TagClass);
                parentTagClass.Tag = TagHierarchy.TagClasses[i].TagClass;

                for (int z = 0; z < TagHierarchy.TagClasses[i].Tags.Count; z++)
                {
                    TreeNode tagNode = new TreeNode(TagHierarchy.TagClasses[i].Tags[z].TagName);
                    tagNode.Tag = TagHierarchy.TagClasses[i].Tags[z].TagInstance.Ident;
                    parentTagClass.Nodes.Add(tagNode);
                }

                treeView.Nodes.Add(parentTagClass);
            }

            //Then sort the treeView.
            treeView.Sort();
        }
        /// <summary>
        /// This function, when given the parameters of tagClass and tag Name, will find the index within the tag list, of which tag name matches those specifications.
        /// </summary>
        /// <param name="tagClass"></param>
        /// <param name="tagName"></param>
        public int GetTagIndexByClassAndName(string tagClass, string tagName)
        {
            //Initialize our index integer
            int index = -1;
            //Obtain our hierarchy class
            TagHierarchy.TagHClass hierarchyClass = TagHierarchy.TagClasses.ReturnTagHClass(tagClass);
            //If it isn't null.
            if (hierarchyClass != null)
            {
                //Obtain our hierarchy name
                TagHierarchy.TagHName hierarchyName = hierarchyClass.ReturnTagHName(tagName);
                //If it isn't null.
                if (hierarchyName != null)
                {
                    //Assign our index
                    index = hierarchyName.TagInstance.metaIndex;
                }
            }
            //Return the index
            return index;
        }
        /// <summary>
        /// This function, when given the parameters of tag Ident, will find the index within the tag list, of which tag ident matches.
        /// </summary>
        /// <param name="ident"></param>
        /// <returns></returns>
        public int GetTagIndexByIdent(int ident)
        {
            //Start off with a null index
            int index = -1;
            //Loop through all the tag
            for (int i = 0; i < IndexItems.Count; i++)
            {
                //If the indexed tag's ID matches the provided ID...
                if (IndexItems[i].Ident == ident)
                {
                    //...Change the index to the index of this tag
                    index = i;
                    //Break out of the loop
                    break;
                }
            }
            //Return the index
            return index;
            //Return our index
            //return (Ident == -1) ? -1 : (Ident & 0xFFFF);
        }
        #endregion
        #region Classes
        /// <summary>
        /// A class that, when initialized, will read all currently found Map Header Values.
        /// The Map Header Size is 0x3000.
        /// </summary>
        public class H3MapHeader
        {
            #region Values
            private string _header;
            /// <summary>
            /// The header string in the Map Header, indicating the start of the Map Header. It is read "head".
            /// Offset = 0
            /// </summary>
            public string header
            {
                get { return _header; }
                set { _header = value; }
            }
            private int _haloversion;
            /// <summary>
            /// This value, when read, indicates what version of the Halo series this map file was built for.
            /// Offset = 4.
            /// </summary>
            public int haloVersion
            {
                get { return _haloversion; }
                set { _haloversion = value; }
            }
            private int _filesize;
            /// <summary>
            /// This value indicates the size of the map file.
            /// Offset = 8
            /// </summary>
            public int fileSize
            {
                get { return _filesize; }
                set { _filesize = value; }
            }
         
            private int _indexoffset;
            /// <summary>
            /// The Pointer which, once the memory address modifier is applied, will indicate where the Index Header is in the Map File.
            /// Offset = 16
            /// </summary>
            public int indexOffset
            {
                get { return _indexoffset; }
                set { _indexoffset = value; }
            }

            /// <summary>
            /// The offset of the meta data/raw data start(is the same as RawTableOffset) once headerMagic is applied.
            /// Offset = 20
            /// </summary>
            private int _virtSegmentStart;
            public int virtSegmentStart
            {
                get { return _virtSegmentStart; }
                set { _virtSegmentStart = value; }
            }
            private int _virtSegmentSize;
            public int virtSegmentSize
            {
                get { return _virtSegmentSize; }
                set { _virtSegmentSize = value; }
            }
            private string _buildinfo;
            /// <summary>
            /// This value is a string which indicates the date that the map was built, along with other build details.
            /// Offset 284
            /// </summary>
            public string buildInfo
            {
                get { return _buildinfo; }
                set { _buildinfo = value; }
            }
            private MapSizeType _mapsizetype;
            /// <summary>
            /// This value indicates what type of map this is.
            /// Enum16:
            /// 0=Single Player
            /// 1=Multiplayer
            /// 2=Mainmenu
            /// 3=Shared
            /// 
            /// Offset = 316
            /// </summary>
            public MapSizeType Map_Size_Type
            {
                get { return _mapsizetype; }
                set { _mapsizetype = value; }
            }

            private MapType _maptype;
            /// <summary>
            /// Indicates the type of map for the game engine to handle.
            /// </summary>
            public MapType Map_Type
            {
                get { return _maptype; }
                set { _maptype = value; }
            }

            private int _stringTableCount;
            /// <summary>
            /// Indicates how many Strings there are in the string table.
            /// Offset = 344
            /// </summary>
            public int stringTableCount
            {
                get { return _stringTableCount; }
                set { _stringTableCount = value; }
            }
            private int _stringTableSize;
            /// <summary>
            /// Indicates the size of the string table in bytes.
            /// Offset = 348
            /// </summary>
            public int stringTableSize
            {
                get { return _stringTableSize; }
                set { _stringTableSize = value; }
            }
            private int _stringTableIndexOffset;
            /// <summary>
            /// Indicates the offset(once headerMagic is applied) of the String Index Table, which indicates the offset of each string.
            /// Offset = 352
            /// </summary>
            public int stringTableIndexOffset
            {
                get { return _stringTableIndexOffset; }
                set { _stringTableIndexOffset = value; }
            }
            private int _stringTableOffset;
            /// <summary>
            /// Indicates the offset of the string table(once headerMagic is applied), containing all strings for StringID's.
            /// Offset = 356
            /// </summary>
            public int stringTableOffset
            {
                get { return _stringTableOffset; }
                set { _stringTableOffset = value; }
            }
            private string _internalname;
            /// <summary>
            /// Indicates the internal name of the map.
            /// Offset = 396
            /// </summary>
            public string internalName
            {
                get { return _internalname; }
                set { _internalname = value; }
            }
            private string _scenarioname;
            /// <summary>
            /// Indicates the scenario of the map. This will match the name of the [scnr] tag.
            /// Offset = 432
            /// </summary>
            public string scenarioName
            {
                get { return _scenarioname; }
                set { _scenarioname = value; }
            }
            private int _fileTableCount;
            /// <summary>
            /// Indicates the Count of Tags in the FileName's Table
            /// Offset = 692
            /// </summary>
            public int fileTableCount
            {
                get { return _fileTableCount; }
                set { _fileTableCount = value; }
            }
            private int _fileTableOffset;
            /// <summary>
            /// Indicates the offset of the Table containing each Tags' name (once headerMagic is applied).
            /// Offset = 696
            /// </summary>
            public int fileTableOffset
            {
                get { return _fileTableOffset; }
                set { _fileTableOffset = value; }
            }
            private int _fileTableSize;
            /// <summary>
            /// Indicates the size of the fileTable, containing all Tags' names.
            /// Offset = 700
            /// </summary>
            public int fileTableSize
            {
                get { return _fileTableSize; }
                set { _fileTableSize = value; }
            }
            private int _fileTableIndexOffset;
            /// <summary>
            /// Indicates the offset of the File Index table, which indicates the offset of each tag's name string in the fileTable.
            /// Offset = 704
            /// </summary>
            public int fileTableIndexOffset
            {
                get { return _fileTableIndexOffset; }
                set { _fileTableIndexOffset = value; }
            }
            private int _signature;
            /// <summary>
            /// The CRC Signature Check
            /// Offset = 708
            /// </summary>
            public int Signature
            {
                get { return _signature; }
                set { _signature = value; }
            }

            private int _mapMagicBaseAddress;
            /// <summary>
            /// This is the map magic base address, which is used to calculate the map magic.
            /// Offset = 744
            /// </summary>
            public int mapMagicBaseAddress
            {
                get { return _mapMagicBaseAddress; }
                set { _mapMagicBaseAddress = value; }
            }

            private int _xdkversion;
            /// <summary>
            /// Our XDK version the maps were optimized for.
            /// </summary>
            public int XDK_Version
            {
                get { return _xdkversion; }
                set { _xdkversion = value; }
            }

            private byte[] _hash;
            /// <summary>
            /// This is a hash written, using a special algorithm to verify the map has not been editted.
            /// Offset = 876
            /// Size = 256
            /// </summary>
            public byte[] hash
            {
                get { return _hash; }
                set { _hash = value; }
            }
            private int _rawtableoffset;
            /// <summary>
            /// The offset of the beginning of the Raw Table in the map.
            /// Offset = 1136
            /// </summary>
            public int RawTableOffset
            {
                get { return _rawtableoffset; }
                set { _rawtableoffset = value; }
            }
            private int _localeTableAddressModifier;
            /// <summary>
            /// This is the modifier applied to the localeTable to tell it where to load, and can be used to figure out the file offset of the locale table.
            /// Offset = 1144
            /// </summary>
            public int localeTableAddressModifier
            {
                get { return _localeTableAddressModifier; }
                set { _localeTableAddressModifier = value; }
            }
            private int _rawtablesize;
            /// <summary>
            /// The size of the Raw Table
            /// Offset = 1160
            /// </summary>
            public int RawTableSize
            {
                get { return _rawtablesize; }
                set { _rawtablesize = value; }
            }
            private string _footer;
            /// <summary>
            /// This string indicates the end of the map header. It is read "foot".
            /// Offset = 12284
            /// </summary>
            public string footer
            {
                get { return _footer; }
                set { _footer = value; }
            }
            private int _headermagic;
            /// <summary>
            /// This is a memory address modifier used to calculate the file Offset of certain tables:
            ///        headerMagic = stringTableIndexOffset - Length; (how to calculate. Length always is 12288)
            ///        virtSegmentStart -= headerMagic; (applying the modifier to get file offset)
            ///        stringTableIndexOffset -= headerMagic; (applying the modifier to get file offset)
            ///        stringTableOffset -= headerMagic; (applying the modifier to get file offset)
            ///        fileTableOffset -= headerMagic; (applying the modifier to get file offset)
            ///        fileTableIndexOffset -= headerMagic; (applying the modifier to get file offset)
            /// 
            /// Offset = None. Calculate as shown above.
            /// </summary>
            public int headerMagic
            {
                get { return _headermagic; }
                set { _headermagic = value; }
            }
            private int _mapmagic;
            /// <summary>
            /// This value is a memory address modifier which can be applied to certain values such as tagOffsets, to get the offset in the file.
            /// 
            /// mapMagic = mapMagicBaseAddress - (mapMagicAddressModifier1 + mapMagicAddressModifier2);
            /// 
            /// Offset = None. Calculated as shown above.
            /// </summary>
            public int mapMagic
            {
                get { return _mapmagic; }
                set { _mapmagic = value; }
            }
            private Partition[] _memoryPartitions;
            /// <summary>
            /// The partitions where tag data is stored.
            /// </summary>
            public Partition[] Memory_Partitions
            {
                get { return _memoryPartitions; }
                set { _memoryPartitions = value; }
            }
            #endregion
            #region Initialization
            public H3MapHeader(HaloMap map)
            {
                //Set our IO to use the map's IO, so we don't need to type as much.
                EndianIO IO = map.IO;
                //Set Basestream position to start reading values.
                IO.In.BaseStream.Position = 0;
                //Read Header String "head"
                header = ExtraFunctions.BytesToString(IO.In.ReadBytes(4)); //0
                //Read the Halo Game Version Enum.
                haloVersion = IO.In.ReadInt32(); //4
                //Read the map fileSize.
                fileSize = IO.In.ReadInt32(); //8
                //Jump 4 bytes forward.
                IO.In.BaseStream.Position = 16;
                //Read the Index Offset(memaddr modifier will be applied later)
                indexOffset = IO.In.ReadInt32();

                //Read the virtSegmentStart
                virtSegmentStart = IO.In.ReadInt32();
                //Read the virtSegmentSize
                virtSegmentSize = IO.In.ReadInt32();

                //Skip to the Build Info Offset
                IO.In.BaseStream.Position = 284;
                //Read the build info as a ASCII string, 32 bytes long
                buildInfo = ExtraFunctions.BytesToString(IO.In.ReadBytes(32));

                //If our buildinfo contains "atlas"
                if (buildInfo.Contains("atlas"))
                    map.Halo_Map_Version = HaloMapVersion.Halo3ODST;
                else
                    map.Halo_Map_Version = HaloMapVersion.Halo3Retail;

                //Read the Map Size Type
                Map_Size_Type = (MapSizeType)IO.In.ReadInt16();
                //Read the Map Type
                Map_Type = (MapType)IO.In.ReadInt16();

                //Jump to the String Table Count Offset
                IO.In.BaseStream.Position = 344;
                //Read the String Count(amount of strings in the stringTable)
                stringTableCount = IO.In.ReadInt32();
                //Read the size of the string table in bytes.
                stringTableSize = IO.In.ReadInt32();
                //Read the String Index Table Offset(memaddr modifier will be applied later)
                stringTableIndexOffset = IO.In.ReadInt32();
                //Read the String Table Offset(memaddr modifier will be applied later)
                stringTableOffset = IO.In.ReadInt32();

                //Jump to the internalName Offset.
                IO.In.BaseStream.Position = 396;
                //Read the internalName as an ASCII string with a length of 32
                internalName = ExtraFunctions.BytesToString(IO.In.ReadBytes(32));

                //Jump to the Scenario name Offset.
                IO.In.BaseStream.Position = 432;
                //Read the Scenario Name as an ASCII string with a length of 256.
                scenarioName = ExtraFunctions.BytesToString(IO.In.ReadBytes(256));

                //Jump to the fileTableCount offset to start reading that...
                IO.In.BaseStream.Position = 692;
                //Read the fileTableCount, indicating how many entries for meta names there are.
                fileTableCount = IO.In.ReadInt32();
                //Read the fileTableOffset, indicating the offset of the fileTable, which we will apply the memaddr modifier to later.
                fileTableOffset = IO.In.ReadInt32();
                //Read the fileTableSize, indicating the size of the file table in bytes.
                fileTableSize = IO.In.ReadInt32();
                //Read the fileTableIndexOffset, memaddr modifier will be applied later.
                fileTableIndexOffset = IO.In.ReadInt32();

                //Read the CRC Check Signature which is used to verify the map has not been editted.
                Signature = IO.In.ReadInt32();

                //Jump to the mapMagicBaseAddress.
                IO.In.BaseStream.Position = 744;
                //Read the mapMagicBaseAddress, which will be used to calculate map magic.
                mapMagicBaseAddress = IO.In.ReadInt32();
                //Read the XDK Version
                XDK_Version = IO.In.ReadInt32();

                // memory partitions
                Memory_Partitions = new Partition[6];
                Memory_Partitions[0].BaseAddress = IO.In.ReadUInt32(); // cache resource buffer
                Memory_Partitions[0].Size = IO.In.ReadInt32();

                // readonly
                Memory_Partitions[1].BaseAddress = IO.In.ReadUInt32(); // sound cache resource buffer
                Memory_Partitions[1].Size = IO.In.ReadInt32();

                Memory_Partitions[2].BaseAddress = IO.In.ReadUInt32(); // global tags buffer
                Memory_Partitions[2].Size = IO.In.ReadInt32();
                Memory_Partitions[3].BaseAddress = IO.In.ReadUInt32(); // shared tag blocks?
                Memory_Partitions[3].Size = IO.In.ReadInt32();
                Memory_Partitions[4].BaseAddress = IO.In.ReadUInt32(); // address
                Memory_Partitions[4].Size = IO.In.ReadInt32();

                // readonly
                Memory_Partitions[5].BaseAddress = IO.In.ReadUInt32(); // map tags buffer
                Memory_Partitions[5].Size = IO.In.ReadInt32();

                //Jump to the Written Hash Offset.
                IO.In.BaseStream.Position = 876;
                //Read the hash which is used to verify the map has not been editted.
                hash = IO.In.ReadBytes(256);

                //Jump to the RawTableOffset Offset.
                IO.In.BaseStream.Position = 1136;
                //Read the RawTableOffset which will also be used to calculate the map magic.
                RawTableOffset = IO.In.ReadInt32();

                //Jump to the localeTableAddressModifier Offset.
                IO.In.BaseStream.Position = 1144;
                //Read the localeTableAddressModifier which will be used to determine file offsets of the localeTables.
                localeTableAddressModifier = IO.In.ReadInt32();

                //Jump to the RawTableSize Offset.
                IO.In.BaseStream.Position = 1160;
                //Read the RawTableSize, which will also be used to calculate the map magic.
                RawTableSize = IO.In.ReadInt32();

                //Jump to the footer offset.
                IO.In.BaseStream.Position = 12284;
                //Read the footer, indicating the end of the Map Header.
                footer = ExtraFunctions.BytesToString(IO.In.ReadBytes(4));

                //If the stringTableIndexOffset is null, we can't calculate the memory modifiers properly.
                if (stringTableIndexOffset != 0 && stringTableIndexOffset != -1)
                {
                    //Calculate the headerMagic(memory address modifier) used for the values below to calculate file offset.
                    headerMagic = stringTableIndexOffset - 12288;
                    //Apply the headerMagic to get the fileOffset of the virtSegmentStart.
                    virtSegmentStart -= headerMagic;
                    //Apply the headerMagic to get the fileOffset of the stringTableIndexOffset
                    stringTableIndexOffset -= headerMagic;
                    //Apply the headerMagic to get the fileOffset of the stringTableOffset
                    stringTableOffset -= headerMagic;
                    //Apply the headerMagic to get the fileOffset of the fileTableOffset
                    fileTableOffset -= headerMagic;
                    //Apply the headerMagic to get the fileOffset of the fileTableIndexOffset
                    fileTableIndexOffset -= headerMagic;
                }

                //Calculate the mapMagic using the modifiers we read earlier.
                mapMagic = mapMagicBaseAddress - (RawTableOffset + RawTableSize);

                //Apply the memoryAddress modifier to the indexOffset to get the offset within the file.
                indexOffset -= mapMagic;

                //Apply the memoryAddress modifier to the memory partitions
                for (int i = 0; i < Memory_Partitions.Length; i++)
                {
                    Memory_Partitions[i].Offset = (int)(Memory_Partitions[i].BaseAddress - mapMagic);
                }
            }
            #endregion
            #region Enums
            public enum MapSizeType
            {
                SinglePlayer = 0,
                Multiplayer = 1,
                Mainmenu = 2,
                Shared = 3,
                Unknown = 4//apparently it goes up to 4..
            }
            public enum MapType
            {
                Playable = -1,
                Mainmenu = 0,
                Shared = 1,
                Campaign = 2
            }
            #endregion
            #region Classes
            /// <summary>
            /// File/Tag data partitions that divides the map.
            /// </summary>
            public struct Partition
            {
                private uint _baseaddress;
                public uint BaseAddress
                {
                    get { return _baseaddress; }
                    set { _baseaddress = value; }
                }

                private int _size;
                public int Size
                {
                    get { return _size; }
                    set { _size = value; }
                }

                private int _offset;
                public int Offset
                {
                    get { return _offset; }
                    set { _offset = value; }
                }
            }
            #endregion
        }
        /// <summary>
        /// This is the indexheader, a part of the map which contains general information needed for loading Tags.
        /// </summary>
        public class H3IndexHeader
        {
            #region Values
            private int _tagClassCount;
            /// <summary>
            /// The amount of tagClasses in the tagClass table.
            /// Offset = 0
            /// </summary>
            public int tagClassCount
            {
                get { return _tagClassCount; }
                set { _tagClassCount = value; }
            }
            private int _tagClassIndexOffset;
            /// <summary>
            /// The Offset of the tagClassIndexOffset. You must subtract mapMagic to get the file Offset.
            /// Offset = 4
            /// </summary>
            public int tagClassIndexOffset
            {
                get { return _tagClassIndexOffset; }
                set { _tagClassIndexOffset = value; }
            }
            private int _tagCount;
            /// <summary>
            /// This value indicates the amount of tags within the map file, shown in the indexItems table.
            /// Offset = 8
            /// </summary>
            public int tagCount
            {
                get { return _tagCount; }
                set { _tagCount = value; }
            }
            private int _tagInfoOffset;
            /// <summary>
            /// Indicates the offset of the tagInfoTable(indexItems)
            /// </summary>
            public int tagInfoOffset
            {
                get { return _tagInfoOffset; }
                set { _tagInfoOffset = value; }
            }
            private int _tagInfoHeaderCount;
            public int tagInfoHeaderCount
            {
                get { return _tagInfoHeaderCount; }
                set { _tagInfoHeaderCount = value; }
            }
            private int _tagInfoHeaderOffset;
            public int tagInfoHeaderOffset
            {
                get { return _tagInfoHeaderOffset; }
                set { _tagInfoHeaderOffset = value; }
            }
            private int _tagInfoHeaderCount2;
            public int tagInfoHeaderCount2
            {
                get { return _tagInfoHeaderCount2; }
                set { _tagInfoHeaderCount2 = value; }
            }
            private int _tagInfoHeaderOffset2;
            public int tagInfoHeaderOffset2
            {
                get { return _tagInfoHeaderOffset2; }
                set { _tagInfoHeaderOffset2 = value; }
            }
            public int unknown1;
            private string _tagsString;
            /// <summary>
            /// The end of the IndexHeader, with a 4-byte long string "tags" indicating the beginning of the indexItems.
            /// </summary>
            public string tagsString
            {
                get { return _tagsString; }
                set { _tagsString = value; }
            }
            private List<TagInfoItem> _tagInfoEntries;
            public List<TagInfoItem> TagInfoEntries
            {
                get { return _tagInfoEntries; }
                set { _tagInfoEntries = value; }
            }
            #endregion
            #region Initialize
            public H3IndexHeader(HaloMap map)
            {
                //Initialize our Index_Items
                map.IndexItems = new List<TagItem>();

                //If our index offset is invalid, exit
                if (map.MapHeader.indexOffset == 0)
                    return;

                //Set our IO to use the map's IO, so we don't need to type as much.
                EndianIO IO = map.IO;

                //Jump to the indexHeader offset, so we can begin reading values.
                IO.In.BaseStream.Position = map.MapHeader.indexOffset;
                //Read the tagClassCount, indicating the amount of different tagclasses within this map.
                tagClassCount = IO.In.ReadInt32();
                //Read the tagClassIndexOffset so we can retrieve all tag classes for tags later.
                tagClassIndexOffset = IO.In.ReadInt32() - map.MapHeader.mapMagic;
                //Read the tagCount, indicating the amount of tags in this map.
                tagCount = IO.In.ReadInt32();
                //Read tagInfoOffset, so we can retreive information for each tag later.
                tagInfoOffset = IO.In.ReadInt32() - map.MapHeader.mapMagic;

                //Read some more values...
                tagInfoHeaderCount = IO.In.ReadInt32();
                tagInfoHeaderOffset = IO.In.ReadInt32() - map.MapHeader.mapMagic;
                tagInfoHeaderCount2 = IO.In.ReadInt32();
                tagInfoHeaderOffset2 = IO.In.ReadInt32() - map.MapHeader.mapMagic;

                //Read the unknown.
                unknown1 = IO.In.ReadInt32();
                //Read the tagsString, indicating the end of the indexHeader
                tagsString = ExtraFunctions.BytesToString(IO.In.ReadBytes(4));
                LoadIndexItems(map);
                LoadTagInfoHeader(map);
            }
            public void LoadIndexItems(HaloMap map)
            {
                //Set our IO so we don't need to type map. each time we want to use it.
                EndianIO IO = map.IO;

                //Initialize our tagClasses to start adding
                List<string> tagClasses = new List<string>();
                //Initialize our parentClasses to start adding
                List<string> parentClasses = new List<string>();
                //Initialize our parentClasses to start adding
                List<string> grandParentClasses = new List<string>();

                //Move our IO to the tagClassIndexOffset to start reading tagClasses.
                IO.In.BaseStream.Position = tagClassIndexOffset;
                //Loop through all the tagClasses, reading them, using the tagClass count.
                for (int i = 0; i < tagClassCount; i++)
                {
                    //Read our main tagClass
                    tagClasses.Add(ExtraFunctions.BytesToString(IO.In.ReadBytes(4)));
                    //Read the tagClass which the above tagClass belongs to.
                    parentClasses.Add(ExtraFunctions.BytesToString(IO.In.ReadBytes(4)));
                    //Read the tagClass which the above parentTagClass belongs to.
                    grandParentClasses.Add(ExtraFunctions.BytesToString(IO.In.ReadBytes(4)));
                    //Ignore the next four bytes and jump forward.
                    IO.In.BaseStream.Position += 4;
                }

                //Jump to the tagInfoOffset to start reading each tag's information.
                IO.In.BaseStream.Position = tagInfoOffset;
                //Loop through for each tag, and read the appropriate information.
                for (int i = 0; i < tagCount; i++)
                {
                    //Initialize our instance of a tag, in which we will read the data into.
                    TagItem tag = new TagItem();
                    //Set the HaloMap this tag belongs to.
                    tag.Map = map;
                    //Read the class index, which will be used to determine what class this tag is.
                    tag.ClassIndex = IO.In.ReadInt16();
                    //Determine the Class From the Class Index
                    if (tag.ClassIndex >= 0 && tag.ClassIndex < tagClassCount)
                    {
                        //If the ClassIndex is valid, assign it as it should be
                        tag.Class = tagClasses[tag.ClassIndex];
                    }
                    else
                    {
                        //Otherwise, we will throw an exception
                        tag.Class = "____";
                        //throw new Exception("A problem has occured. When reading tagClasses, it has been shown that one or more meta's has an invalid tagClass Index.");
                    }
                    //Read the Ident
                    tag.Ident = IO.In.ReadInt16();
                    //Apply its modifiers to it to make it a Valid Int32 as it should be and as it is written in the map file.
                    tag.Ident <<= 16;
                    tag.Ident |= i;
                    //Read the tag offset, and apply the mapMagic to get the offset within the file.
                    tag.Offset = IO.In.ReadInt32() - map.MapHeader.mapMagic;
                    //Assign our meta index
                    tag.metaIndex = i;
                    //Add it to the tag List.
                    map.IndexItems.Add(tag);
                }


                //Now we must finally read the fileName, the name of the tag.
                //So we jump to the fileTableIndexOffset, to get the offsets of the fileNames in the fileNameTable.
                IO.In.BaseStream.Position = map.MapHeader.fileTableIndexOffset;
                //Initialize our array of offsets, which we will read each fileName's offset into.
                int[] fileNameOffsets = new int[tagCount];
                //Loop through every tag to read the fileNameOffset.
                for (int i = 0; i < tagCount; i++)
                {
                    //Read our fileNameOffset, the offset is fileNameTable + readOffset.
                    fileNameOffsets[i] = IO.In.ReadInt32();
                }

                //Finally we read the fileNames.


                //Loop through each meta and read the appropriate filenames.
                //We will be getting the length by doing (Next Offset) - (This offset). 
                //Meaning the last we will get by (End of FileNames) - (This offset).
                //So we skip the last one and do that after.
                for (int i = 0; i < fileNameOffsets.Length; i++)
                {
                    //Go to the tagName offset.
                    IO.In.BaseStream.Position = map.MapHeader.fileTableOffset + fileNameOffsets[i];

                    //Determine which method to use.
                    map.IndexItems[i].Name = i != fileNameOffsets.Length - 1 ? IO.In.ReadAsciiString(fileNameOffsets[i + 1] - fileNameOffsets[i]) : IO.In.ReadAsciiString(map.MapHeader.fileTableSize - fileNameOffsets[i]);

                    //If the tag name is blank...
                    if (map.IndexItems[i].Name == "")
                    {
                        //...We change it to <<null>>
                        map.IndexItems[i].Name = "<blank name>";
                    }
                }
                //To do later: get header sizes.
            }
            public void LoadTagInfoHeader(HaloMap map)
            {
                //Go to our selected position
                map.IO.In.BaseStream.Position = tagInfoHeaderOffset;
                //Initialize our list
                TagInfoEntries = new List<TagInfoItem>();
                //Loop through all the chunks
                for (int i = 0; i < tagInfoHeaderCount; i++)
                {
                    //Initialize our tagInfoItem
                    TagInfoItem tagInfoitem = new TagInfoItem();
                    //Read our class
                    tagInfoitem.Class = map.IO.In.ReadAsciiString(4);
                    //Read our ident
                    tagInfoitem.Identifier = map.IO.In.ReadInt32();
                    //Add it to our list.
                    TagInfoEntries.Add(tagInfoitem);
                }
            }
            #endregion
            #region Classes
            public class TagInfoItem
            {
                private string _class;
                public string Class
                {
                    get { return _class; }
                    set { _class = value; }
                }
                private int _identifier;
                public int Identifier
                {
                    get { return _identifier; }
                    set { _identifier = value; }
                }
            }
            #endregion
        }
        /// <summary>
        /// This is an instance of a TagItem, which is used to store our Tag Values.
        /// </summary>
        public class TagItem
        {
            private HaloMap _map;
            /// <summary>
            /// This value indicates the instance of HaloMap the tag belongs to.
            /// </summary>
            public HaloMap Map
            {
                get { return _map; }
                set { _map = value; }
            }
            /// <summary>
            /// This is the index that the meta is in the metaList of the IndexItems.
            /// </summary>
            public int metaIndex;
            /// <summary>
            /// The index of the Class used. You can use this index to locate which class is used by looking at the TagClassTable
            /// </summary>
            public short ClassIndex;
            private string _class;
            /// <summary>
            /// This is the string of the class used. Classes are 4 characters in length, and indicate what type of object the tag is.
            /// </summary>
            public string Class
            {
                get { return _class; }
                set { _class = value; }
            }
            private string _name;
            /// <summary>
            /// This is the full tag name, indicating what tag this is. For example, "objects\multi\vehicles\warthog\warthog".
            /// </summary>
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }
            private int _offset;
            /// <summary>
            /// This is the offset of the tag in the map file.
            /// </summary>
            public int Offset
            {
                get { return _offset; }
                set { _offset = value; }
            }
            private int _ident;
            /// <summary>
            /// This is the unique identifier of the tag, in which it could be used as a reference from other tags.
            /// </summary>
            public int Ident
            {
                get { return _ident; }
                set { _ident = value; }
            }
            private int _headersize;
            /// <summary>
            /// This is a rough calculated size of the meta, which is horribly inaccurate.
            /// </summary>
            public int HeaderSize
            {
                get { return _headersize; }
                set { _headersize = value; }
            }
        }
        /// <summary>
        /// This is an instance of our StringTable, that when initialized, will read all strings.
        /// </summary>
        public class H3StringTable
        {
            #region Values
            private List<StringItem> _stringitems;
            /// <summary>
            /// This is a list containing all the StringItems are that are loaded from the StringTable.
            /// </summary>
            public List<StringItem> StringItems
            {
                get { return _stringitems; }
                set { _stringitems = value; }
            }
            private int _stringcount;
            /// <summary>
            /// This is the amount of stringItems in the string Table.
            /// </summary>
            public int StringCount
            {
                get { return _stringcount; }
                set { _stringcount = value; }
            }
            #endregion
            #region Initialization
            public H3StringTable(HaloMap map)
            {
                //If our string table offsets are not existant.
                if (map.MapHeader.stringTableOffset == 0 | map.MapHeader.stringTableIndexOffset == 0)
                    return;

                //StringItem count, lets set that.
                StringCount = map.MapHeader.stringTableCount;
                //Initialize our String List.
                StringItems = new List<StringItem>();
                //Assign this instance of IO so we don't need to say map.
                EndianIO IO = map.IO;
                //Move to the StringIndexTable.
                IO.In.BaseStream.Position = map.MapHeader.stringTableIndexOffset;
                //Loop through all the strings and read their offsets
                for (int i = 0; i < map.MapHeader.stringTableCount; i++)
                {
                    //Initialize our instance of the string
                    StringItem stringItem = new StringItem();
                    //Assign the index to this instance.
                    stringItem.Index = i;
                    //We read our offset
                    stringItem.Offset = IO.In.ReadInt32();
                    //Add our instance of the string to the stringList.
                    StringItems.Add(stringItem);
                }

                //Loop through all the strings and read them with the same method as the fileNames
                for (int i = 0; i < map.MapHeader.stringTableCount; i++)
                {
                    //Jump to the string offset.
                    IO.In.BaseStream.Position = map.MapHeader.stringTableOffset + StringItems[i].Offset;
                    //Read the string
                    if (i != map.MapHeader.stringTableCount - 1)
                    {
                        //Set our length of the string
                        StringItems[i].Length = StringItems[i + 1].Offset - (StringItems[i].Offset + 1);
                        //Read with the below method
                        StringItems[i].Name = IO.In.ReadAsciiString(StringItems[i].Length);
                    }
                    else
                    {
                        //Set our length of the string
                        StringItems[i].Length = map.MapHeader.stringTableSize - (StringItems[i].Offset + 1);
                        //Read with the below method
                        StringItems[i].Name = IO.In.ReadAsciiString(StringItems[i].Length);
                    }
                }

                //Loop through all the strings and read them with the same method as the fileNames
                for (int i = 0; i < map.MapHeader.stringTableCount; i++)
                {
                    //CALCULATAAA OUR SPARTAAAAA
                    StringItems[i].ID = i;

                }
            }
            #endregion
            #region Functions
            /// <summary>
            /// This function takes an identifier and gets the index of the StringItem that uses it.
            /// </summary>
            /// <param name="ID">The identifier of the string item</param>
            /// <returns>Returns the string item index.</returns>
            public int GetStringItemIndexByID(HaloMap map, int ID)
            {
                if (map.Halo_Map_Version == HaloMapVersion.Halo3Retail)
                {
                    if (ID > 0xFB49)
                        ID -= 0xFB49;
                    else if (ID > 0x4b0)
                        ID += 0x75A;
                }
                else if (map.Halo_Map_Version == HaloMapVersion.Halo3ODST)
                {
                    if (ID > 0xFAE7)
                        ID -= 0xFAE7;
                    else if (ID > 0x4b0)
                        ID += 0x832;
                }
                return ID;
                //Our index value
                int index = -1;

                //Loop for each sid
                for (int i = 0; i < StringItems.Count; i++)
                {
                    //Check if the ID matches
                    if (StringItems[i].ID == ID)
                    {
                        //Set the index
                        index = i;

                        //Break out of the loop
                        break;
                    }
                }

                //Return the index
                return index;
            }
            #endregion
            #region Classes
            /// <summary>
            /// This is an instance of a StringItem, which indicates a String's information retreived from the StringTable.
            /// </summary>
            public class StringItem
            {
                private string _name;
                /// <summary>
                /// This is the name of the String read from the stringTable, as an ASCII string.
                /// </summary>
                public string Name
                {
                    get { return _name; }
                    set { _name = value; }
                }
                private int _index;
                /// <summary>
                /// This is the index within the stringTable in which this string is stored.
                /// </summary>
                public int Index
                {
                    get { return _index; }
                    set { _index = value; }
                }
                private int _offset;
                /// <summary>
                /// This is the offset within the StringTable in which the string is located.
                /// </summary>
                public int Offset
                {
                    get { return _offset; }
                    set { _offset = value; }
                }
                private int _length;
                /// <summary>
                /// This value indicates the length of the string which is read.
                /// </summary>
                public int Length
                {
                    get { return _length; }
                    set { _length = value; }
                }
                private int _id;
                /// <summary>
                /// The identifier for the string ID
                /// </summary>
                public int ID
                {
                    get { return _id; }
                    set { _id = value; }
                }
            }
            #endregion
        }
        public enum HaloMapVersion
        {
            Halo3Retail,
            Halo3ODST
        }
        #endregion
    }
}
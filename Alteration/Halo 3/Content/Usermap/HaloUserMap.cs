using System;
using System.Collections.Generic;
using Alteration.Helpers;
using HaloDeveloper.IO;

namespace Alteration.Halo_3.Content.Usermap
{
    /// <summary>
    /// When initialized with a stream, this class will read the map variant and its information so it may be editted accordingly.
    /// </summary>
    public class HaloUserMap
    {
        #region Values

        private string _filename;

        private EndianIO _io;
        private MapVariantHeader _mapvariantheader;
        private MapVariantInformation _mapvariantinformation;
        private PalletteBlocks _palletteblocks;
        private PlacementBlocks _placementblocks;

        private int _sandboxoffset = -1;

        /// <summary>
        /// This is the filename of our CON containing the sandbox, or sandbox.
        /// </summary>
        public string FileName
        {
            get { return _filename; }
            set { _filename = value; }
        }

        /// <summary>
        /// This is the IO which can be opened with OpenIO() and closed with CloseIO() and can be used to read or write values within the file.
        /// </summary>
        public EndianIO IO
        {
            get { return _io; }
            set { _io = value; }
        }

        /// <summary>
        /// This is the offset of the sandbox.map file within the file at the specified filename.
        /// </summary>
        public int SandboxOffset
        {
            get { return _sandboxoffset; }
            set { _sandboxoffset = value; }
        }

        /// <summary>
        /// This is our Map Variant Header for this usermap, containing general information on the variant such as Author, Title, Description.
        /// </summary>
        public MapVariantHeader Map_Variant_Header
        {
            get { return _mapvariantheader; }
            set { _mapvariantheader = value; }
        }

        /// <summary>
        /// This part, when initialized, will read our MapVariantInformation such as Budgets, World Bounds, etc.
        /// </summary>
        public MapVariantInformation Map_Variant_Information
        {
            get { return _mapvariantinformation; }
            set { _mapvariantinformation = value; }
        }

        /// <summary>
        /// This is our list containing the placement blocks, which when initialized, will load all Placement Blocks as a list, and can save them aswell.
        /// </summary>
        public PlacementBlocks Placement_Blocks
        {
            get { return _placementblocks; }
            set { _placementblocks = value; }
        }

        /// <summary>
        /// This is our list of pallete blocks, which when initialized, will read all pallete blocks in the usermap/sandbox, and can write them aswell.
        /// </summary>
        public PalletteBlocks Pallette_Blocks
        {
            get { return _palletteblocks; }
            set { _palletteblocks = value; }
        }

        #endregion

        #region Initialization

        public HaloUserMap(string filename)
        {
            //Set our instance of the FileName
            FileName = filename;

            //Load our usermap
            Refresh();
        }

        #endregion

        #region Functions

        public void OpenIO()
        {
            //Close our IO first incase.
            CloseIO();
            //Initialize our IO
            IO = new EndianIO(FileName, EndianType.BigEndian);
            //Open our IO
            IO.Open();
        }

        public void CloseIO()
        {
            //If our IO is initialized...
            if (IO != null)
            {
                //If its open...
                if (IO.Opened)
                {
                    //Then close it.
                    IO.Close();
                }
                //And null it if its initialized.
                IO = null;
            }
        }

        public void Refresh()
        {
            //Open our IO
            OpenIO();

            //Find our sandbox.map
            //Loop through every 0x1000 block.
            for (int i = 0; i < (int) IO.In.BaseStream.Length - 0x1000; i += 0x1000)
            {
                //Go to the 0x1000 block
                IO.In.BaseStream.Position = i;
                //If the first 4 bytes are _blf
                if (IO.In.ReadAsciiString(4) == "_blf")
                {
                    //Then we found the sandbox offset
                    SandboxOffset = i;
                    //Break out of the loop
                    break;
                }
            }

            //If our sandboxOffset is null, it means we couldnt find the sandbox
            if (SandboxOffset == -1)
            {
                //So throw our exception
                throw new Exception(
                    "The sandbox data was not found when the IO looped through every 0x1000 block looking for the _blf header. The file could not be opened.");
            }

            //Initialize our Map Variant Header
            Map_Variant_Header = new MapVariantHeader(this);
            //Initialize our Map Variant Information
            Map_Variant_Information = new MapVariantInformation(this);
            //Initialize our Pallette Blocks.
            Pallette_Blocks = new PalletteBlocks(this);
            //Initialize our Placement Blocks
            Placement_Blocks = new PlacementBlocks(this);

            //Close our IO
            CloseIO();
        }

        public void SaveAll()
        {
            //If the IO is initialized..
            if (IO != null)
            {
                //If its open..
                if (IO.Opened)
                {
                    //Close it
                    CloseIO();
                }
            }

            //[Re]Open our IO
            OpenIO();

            //Save all tables
            Map_Variant_Header.SaveMapVariantHeader(this);
            Map_Variant_Information.SaveMapVariantInformation(this);
            Placement_Blocks.SavePlacementBlocks(this);
            Pallette_Blocks.SavePalleteBlocks(this);

            //Close our IO
            CloseIO();
        }

        #endregion

        #region Classes/Parts

        #region Nested type: MapVariantHeader

        /// <summary>
        /// This part of the Usermap or Map Variant will contain information about the variant in general such as the author, name, description, etc.
        /// </summary>
        public class MapVariantHeader
        {
            #region Values

            private int _confilename1;
            private int _confilename2;
            private int _entrysize;
            private int _mapid;
            private string _mapvstring;

            private int _size;

            private string _variantauthor;
            private string _variantdescription;
            private string _variantname;
            public int null1;
            public int null2;
            public int null3;
            public short unknown_10;

            public int unknown_200;
            public int unknown_204;
            public int unknown_208;
            public int unknown_212;
            public int unknown_216;
            public int unknown_232;
            public int unknown_244;
            public int unknown_254;
            public short unknown_8;

            public string MapVString
            {
                get { return _mapvstring; }
                set { _mapvstring = value; }
            }

            public int Size
            {
                get { return _size; }
                set { _size = value; }
            }

            public string Variant_Name
            {
                get { return _variantname; }
                set { _variantname = value; }
            }

            public string Variant_Description
            {
                get { return _variantdescription; }
                set { _variantdescription = value; }
            }

            public string Variant_Author
            {
                get { return _variantauthor; }
                set { _variantauthor = value; }
            }

            public int Entry_Size
            {
                get { return _entrysize; }
                set { _entrysize = value; }
            }

            public int CON_File_Name_1
            {
                get { return _confilename1; }
                set { _confilename1 = value; }
            }

            public int CON_File_Name_2
            {
                get { return _confilename2; }
                set { _confilename2 = value; }
            }

            public int Map_ID
            {
                get { return _mapid; }
                set { _mapid = value; }
            }

            #endregion

            #region Initialization

            public MapVariantHeader(HaloUserMap User_Map)
            {
                //Go to our sandbox offset
                User_Map.IO.In.BaseStream.Position = User_Map.SandboxOffset + 0x138;
                //Read our mapV string
                MapVString = User_Map.IO.In.ReadAsciiString(4);
                //Read our sandbox size
                Size = User_Map.IO.In.ReadInt32();
                //Read our unknowns
                unknown_8 = User_Map.IO.In.ReadInt16();
                unknown_10 = User_Map.IO.In.ReadInt16();
                //Skip unused data
                User_Map.IO.In.BaseStream.Position += 0xC;
                //Read our variant data
                Variant_Name = User_Map.IO.In.ReadUnicodeString(0x10);
                Variant_Description = User_Map.IO.In.ReadAsciiString(0x80);
                Variant_Author = User_Map.IO.In.ReadAsciiString(0x10);
                //Read more unknowns
                unknown_200 = User_Map.IO.In.ReadInt32();
                unknown_204 = User_Map.IO.In.ReadInt32();
                unknown_208 = User_Map.IO.In.ReadInt32();
                unknown_212 = User_Map.IO.In.ReadInt32();
                unknown_216 = User_Map.IO.In.ReadInt32();
                //Read our entry size
                Entry_Size = User_Map.IO.In.ReadInt32();
                //Read our CON File Name int's
                CON_File_Name_1 = User_Map.IO.In.ReadInt32();
                CON_File_Name_2 = User_Map.IO.In.ReadInt32();
                unknown_232 = User_Map.IO.In.ReadInt32();
                null1 = User_Map.IO.In.ReadInt32();
                //Read our map id
                Map_ID = User_Map.IO.In.ReadInt32();
                //Read our unknowns and nulls remaining
                unknown_244 = User_Map.IO.In.ReadInt32();
                null2 = User_Map.IO.In.ReadInt32();
                null3 = User_Map.IO.In.ReadInt32();
                unknown_254 = User_Map.IO.In.ReadInt32();
            }

            #endregion

            #region Functions

            public void SaveMapVariantHeader(HaloUserMap User_Map)
            {
                //Go to our sandbox offset
                User_Map.IO.Out.BaseStream.Position = User_Map.SandboxOffset + 0x138;
                //Write our mapV string
                User_Map.IO.Out.WriteAsciiString(MapVString, 4);
                //Write our sandbox size
                User_Map.IO.Out.Write(Size);
                //Write our unknowns
                User_Map.IO.Out.Write(unknown_8);
                User_Map.IO.Out.Write(unknown_10);
                //Skip unused data
                User_Map.IO.Out.BaseStream.Position += 0xC;
                //Write our variant data
                User_Map.IO.Out.WriteUnicodeString(Variant_Name, 0x10);
                User_Map.IO.Out.WriteAsciiString(Variant_Description, 0x80);
                User_Map.IO.Out.WriteAsciiString(Variant_Author, 0x10);
                //Write more unknowns
                User_Map.IO.Out.Write(unknown_200);
                User_Map.IO.Out.Write(unknown_204);
                User_Map.IO.Out.Write(unknown_208);
                User_Map.IO.Out.Write(unknown_212);
                User_Map.IO.Out.Write(unknown_216);
                //Read our entry size
                User_Map.IO.Out.Write(Entry_Size);
                //Read our CON File Name int's
                User_Map.IO.Out.Write(CON_File_Name_1);
                User_Map.IO.Out.Write(CON_File_Name_2);
                //Write our unknown and null
                User_Map.IO.Out.Write(unknown_232);
                User_Map.IO.Out.Write(null1);
                //Write our map id
                User_Map.IO.Out.Write(Map_ID);
                //Write our unknowns and nulls remaining
                User_Map.IO.Out.Write(unknown_244);
                User_Map.IO.Out.Write(null2);
                User_Map.IO.Out.Write(null3);
                User_Map.IO.Out.Write(unknown_254);
            }

            #endregion
        }

        #endregion

        #region Nested type: MapVariantInformation

        /// <summary>
        /// This part, when initialized, will read our MapVariantInformation such as Budgets, World Bounds, etc.
        /// </summary>
        public class MapVariantInformation
        {
            #region Values

            private float _currentbudget;
            private int _mapid;
            private float _maximumbudget;
            private byte _spawnedobjectcount;
            private float _worldboundsxmax;
            private float _worldboundsxmin;
            private float _worldboundsymax;
            private float _worldboundsymin;
            private float _worldboundszmax;
            private float _worldboundszmin;
            public short unknown;
            public short unknown_2;
            public int unknown_36;
            public byte unknown_4;
            public int unknown_48;
            public int unknown_52;

            public short unknown_6;

            public byte Spawned_Object_Count
            {
                get { return _spawnedobjectcount; }
                set { _spawnedobjectcount = value; }
            }

            public int Map_ID
            {
                get { return _mapid; }
                set { _mapid = value; }
            }

            public float World_Bounds_X_Min
            {
                get { return _worldboundsxmin; }
                set { _worldboundsxmin = value; }
            }

            public float World_Bounds_X_Max
            {
                get { return _worldboundsxmax; }
                set { _worldboundsxmax = value; }
            }

            public float World_Bounds_Y_Min
            {
                get { return _worldboundsymin; }
                set { _worldboundsymin = value; }
            }

            public float World_Bounds_Y_Max
            {
                get { return _worldboundsymax; }
                set { _worldboundsymax = value; }
            }

            public float World_Bounds_Z_Min
            {
                get { return _worldboundszmin; }
                set { _worldboundszmin = value; }
            }

            public float World_Bounds_Z_Max
            {
                get { return _worldboundszmax; }
                set { _worldboundszmax = value; }
            }

            public float Maximum_Budget
            {
                get { return _maximumbudget; }
                set { _maximumbudget = value; }
            }

            public float Current_Budget
            {
                get { return _currentbudget; }
                set { _currentbudget = value; }
            }

            #endregion

            #region Initialization

            public MapVariantInformation(HaloUserMap User_Map)
            {
                //Go to the Map Variant Information Offset
                User_Map.IO.In.BaseStream.Position = User_Map.SandboxOffset + 0x238;
                //Read our unknown values.
                unknown = User_Map.IO.In.ReadInt16();
                unknown_2 = User_Map.IO.In.ReadInt16();
                unknown_4 = User_Map.IO.In.ReadByte();
                //Read the spawned object count
                Spawned_Object_Count = User_Map.IO.In.ReadByte();
                //Read our unknown value.
                unknown_6 = User_Map.IO.In.ReadInt16();
                //Read the map ID
                Map_ID = User_Map.IO.In.ReadInt32();
                //Read our world bounds values
                World_Bounds_X_Min = User_Map.IO.In.ReadSingle();
                World_Bounds_X_Max = User_Map.IO.In.ReadSingle();
                World_Bounds_Y_Min = User_Map.IO.In.ReadSingle();
                World_Bounds_Y_Max = User_Map.IO.In.ReadSingle();
                World_Bounds_Z_Min = User_Map.IO.In.ReadSingle();
                World_Bounds_Z_Max = User_Map.IO.In.ReadSingle();
                //Read our unknown value.
                unknown_36 = User_Map.IO.In.ReadInt32();
                //Read the maximum budget
                Maximum_Budget = User_Map.IO.In.ReadSingle();
                //Read the current budget
                Current_Budget = User_Map.IO.In.ReadSingle();
                //Read our unknown values.
                unknown_48 = User_Map.IO.In.ReadInt32();
                unknown_52 = User_Map.IO.In.ReadInt32();
            }

            #endregion

            #region Functions

            public void SaveMapVariantInformation(HaloUserMap User_Map)
            {
                //Go to the Map Variant Information Offset
                User_Map.IO.Out.BaseStream.Position = User_Map.SandboxOffset + 0x238;
                //Write our unknown values.
                User_Map.IO.Out.Write(unknown);
                User_Map.IO.Out.Write(unknown_2);
                User_Map.IO.Out.Write(unknown_4);
                //Read the spawned object count
                User_Map.IO.Out.Write(Spawned_Object_Count);
                //Read our unknown value.
                User_Map.IO.Out.Write(unknown_6);
                //Read the map ID
                User_Map.IO.Out.Write(Map_ID);
                //Read our world bounds values
                User_Map.IO.Out.Write(World_Bounds_X_Min);
                User_Map.IO.Out.Write(World_Bounds_X_Max);
                User_Map.IO.Out.Write(World_Bounds_Y_Min);
                User_Map.IO.Out.Write(World_Bounds_Y_Max);
                User_Map.IO.Out.Write(World_Bounds_Z_Min);
                User_Map.IO.Out.Write(World_Bounds_Z_Max);
                //Read our unknown value.
                User_Map.IO.Out.Write(unknown_36);
                //Read the maximum budget
                User_Map.IO.Out.Write(Maximum_Budget);
                //Read the current budget
                User_Map.IO.Out.Write(Current_Budget);
                //Read our unknown values.
                User_Map.IO.Out.Write(unknown_48);
                User_Map.IO.Out.Write(unknown_52);
            }

            #endregion
        }

        #endregion

        #region Nested type: PalletteBlock

        /// <summary>
        /// This class, will contain information about an item's pallete, which says what object is to be spawned when this pallete is referenced, and some of its properties.
        /// </summary>
        public class PalletteBlock
        {
            public PalletteBlock()
            {
                //Initialize our list of placement blocks for this pallete.
                Placement_Blocks = new List<PlacementBlock>();
            }

            #region Values

            private byte _designtimemaximum;
            private int _indexoffset;
            private byte _numberonmap;
            private List<PlacementBlock> _placementblocks;
            private byte _runtimemaximum;
            private byte _runtimeminimum;
            private int _tagident;
            private float _totalcost;
            public int Index;

            public int Tag_Ident
            {
                get { return _tagident; }
                set { _tagident = value; }
            }

            public byte Run_Time_Minimum
            {
                get { return _runtimeminimum; }
                set { _runtimeminimum = value; }
            }

            public byte Run_Time_Maximum
            {
                get { return _runtimemaximum; }
                set { _runtimemaximum = value; }
            }

            public byte Number_On_Map
            {
                get { return _numberonmap; }
                set { _numberonmap = value; }
            }

            public byte Design_Time_Maximum
            {
                get { return _designtimemaximum; }
                set { _designtimemaximum = value; }
            }

            public float Total_Cost
            {
                get { return _totalcost; }
                set { _totalcost = value; }
            }

            //Not a MapV-Value

            public int Index_Offset
            {
                get { return _indexoffset; }
                set { _indexoffset = value; }
            }

            public List<PlacementBlock> Placement_Blocks
            {
                get { return _placementblocks; }
                set { _placementblocks = value; }
            }

            #endregion
        }

        #endregion

        #region Nested type: PalletteBlocks

        /// <summary>
        /// This is our list of pallete blocks, which when initialized, will read all pallete blocks in the usermap/sandbox, and can write them aswell.
        /// </summary>
        public class PalletteBlocks : List<PalletteBlock>
        {
            #region Initialization

            public PalletteBlocks(HaloUserMap User_Map)
            {
                //Loop for each chunk.
                for (int i = 0; i < 256; i++)
                {
                    //Initialize our pallete block instance
                    PalletteBlock pallete_Block = new PalletteBlock();
                    //Go to our pallete block offset
                    User_Map.IO.In.BaseStream.Position = User_Map.SandboxOffset + 0xD494 + (i*12);
                    //Set its offset in the usermap
                    pallete_Block.Index_Offset = (int) User_Map.IO.In.BaseStream.Position - User_Map.SandboxOffset;
                    //Read the tag Ident
                    pallete_Block.Tag_Ident = User_Map.IO.In.ReadInt32();
                    //Read the runtimes.
                    pallete_Block.Run_Time_Minimum = User_Map.IO.In.ReadByte();
                    pallete_Block.Run_Time_Maximum = User_Map.IO.In.ReadByte();
                    //Read the number on the map
                    pallete_Block.Number_On_Map = User_Map.IO.In.ReadByte();
                    //Read the design time maximum
                    pallete_Block.Design_Time_Maximum = User_Map.IO.In.ReadByte();
                    //Read the total cost
                    pallete_Block.Total_Cost = User_Map.IO.In.ReadSingle();
                    //Set our block's index
                    pallete_Block.Index = i;
                    //Add it to the list
                    Add(pallete_Block);
                }
            }

            #endregion

            #region Functions

            public void SavePalleteBlocks(HaloUserMap User_Map)
            {
                //Loop for each chunk.
                for (int i = 0; i < Count; i++)
                {
                    //Go to our pallete block offset
                    User_Map.IO.Out.BaseStream.Position = User_Map.SandboxOffset + this[i].Index_Offset;
                    //Write the tag Ident
                    User_Map.IO.Out.Write(this[i].Tag_Ident);
                    //Write the runtimes.
                    User_Map.IO.Out.Write(this[i].Run_Time_Minimum);
                    User_Map.IO.Out.Write(this[i].Run_Time_Maximum);
                    //Write the number on the map
                    User_Map.IO.Out.Write(this[i].Number_On_Map);
                    //Write the design time maximum
                    User_Map.IO.Out.Write(this[i].Design_Time_Maximum);
                    //Write the total cost
                    User_Map.IO.Out.Write(this[i].Total_Cost);
                }
            }

            #endregion
        }

        #endregion

        #region Nested type: PlacementBlock

        /// <summary>
        /// This class will contain information for a placement block which contains information like spawns, and how the item is spawned.
        /// </summary>
        public class PlacementBlock
        {
            #region Values

            public enum BlockType : short
            {
                Player_Spawn = 9,
                Reserved = 41,
                Added = 131,
                Original = 137,
                Edited = 139,
                NULL = 0,
            }

            private BlockType _blocktype;
            private bool _flagasymmetrical;
            private bool _flagplaceatstart;
            private bool _flagsymmetrical;
            private bool _flagunknown;

            private int _indexoffset;
            private float _pitch;
            private byte _respawntime;
            private float _roll;
            private byte _spareclips;
            private int _tagsindex;
            private byte _team;
            private float _x;
            private float _y;
            private float _yaw;
            private float _z;
            public byte unknown_60; //60 
            public byte unknown_61; //61   
            public float unknown_Rot_1; //40
            public float unknown_Rot_2; //44
            public float unknown_Rot_3; //48
            public short unknown66; //66
            public byte[] unused_4; //4

            #region Unused Data

            public byte[] unused_68; //68

            #endregion

            #region Unused Data

            public byte[] unused_52; //52

            #endregion

            public int Index_Offset
            {
                get { return _indexoffset; }
                set { _indexoffset = value; }
            }

            public BlockType Block_Type //0
            {
                get { return _blocktype; }
                set { _blocktype = value; }
            }

            public int Tags_Index //12
            {
                get { return _tagsindex; }
                set { _tagsindex = value; }
            }

            public float X //16
            {
                get { return _x; }
                set { _x = value; }
            }

            public float Y //20
            {
                get { return _y; }
                set { _y = value; }
            }

            public float Z //24
            {
                get { return _z; }
                set { _z = value; }
            }

            public float Yaw //28
            {
                get { return _yaw; }
                set { _yaw = value; }
            }

            public float Pitch //32
            {
                get { return _pitch; }
                set { _pitch = value; }
            }

            public float Roll //36
            {
                get { return _roll; }
                set { _roll = value; }
            }

            public bool Flag_Unknown //62 bit0
            {
                get { return _flagunknown; }
                set { _flagunknown = value; }
            }

            public bool Flag_Place_At_Start //62 bit1
            {
                get { return _flagplaceatstart; }
                set { _flagplaceatstart = value; }
            }

            public bool Flag_Asymmetrical //62 bit2
            {
                get { return _flagasymmetrical; }
                set { _flagasymmetrical = value; }
            }

            public bool Flag_Symmetrical //62 bit3
            {
                get { return _flagsymmetrical; }
                set { _flagsymmetrical = value; }
            }

            public byte Team //63
            {
                get { return _team; }
                set { _team = value; }
            }

            public byte Spare_Clips //64
            {
                get { return _spareclips; }
                set { _spareclips = value; }
            }

            public byte Respawn_Time //65
            {
                get { return _respawntime; }
                set { _respawntime = value; }
            }

            #endregion
        }

        #endregion

        #region Nested type: PlacementBlocks

        /// <summary>
        /// This is our list containing the placement blocks, which when initialized, will load all Placement Blocks as a list, and can save them aswell.
        /// </summary>
        public class PlacementBlocks : List<PlacementBlock>
        {
            #region Initialization

            public PlacementBlocks(HaloUserMap User_Map)
            {
                //Loop through all the placement blocks.
                for (int i = 0; i < 640; i++)
                {
                    //Initialize our placement block
                    PlacementBlock placement_block = new PlacementBlock();
                    //Go to that placement block's offset.
                    User_Map.IO.In.BaseStream.Position = User_Map.SandboxOffset + 0x278 + (i*0x54);
                    //Set our index offset which is the offset of the placement block within the sandbox
                    placement_block.Index_Offset = (int) User_Map.IO.In.BaseStream.Position - User_Map.SandboxOffset;
                    //Read our blockType
                    placement_block.Block_Type = (PlacementBlock.BlockType) User_Map.IO.In.ReadInt32();
                    //Read our unused
                    placement_block.unused_4 = User_Map.IO.In.ReadBytes(8);

                    //Read the tags index which is the index of the pallete to use.
                    placement_block.Tags_Index = User_Map.IO.In.ReadInt32();

                    //Read our X Coordinate
                    placement_block.X = User_Map.IO.In.ReadSingle();
                    //Read our Y Coordinate
                    placement_block.Y = User_Map.IO.In.ReadSingle();
                    //Read our Z Coordinate
                    placement_block.Z = User_Map.IO.In.ReadSingle();
                    //Read our Yaw Rotation Coordinate
                    placement_block.Yaw = User_Map.IO.In.ReadSingle();
                    //Read our Pitch Rotation Coordinate
                    placement_block.Pitch = User_Map.IO.In.ReadSingle();
                    //Read our Roll Rotation Coordinate
                    placement_block.Roll = User_Map.IO.In.ReadSingle();

                    //Read the three unknown rotation values.
                    placement_block.unknown_Rot_1 = User_Map.IO.In.ReadSingle();
                    placement_block.unknown_Rot_2 = User_Map.IO.In.ReadSingle();
                    placement_block.unknown_Rot_3 = User_Map.IO.In.ReadSingle();

                    //Read our 16 bytes of unused
                    placement_block.unused_52 = User_Map.IO.In.ReadBytes(16);
                    //Read two unknowns.
                    placement_block.unknown_60 = User_Map.IO.In.ReadByte();
                    placement_block.unknown_61 = User_Map.IO.In.ReadByte();

                    //Load our byte value for our flags
                    List<bool> flags = BitHelper.LoadValue(int.Parse(User_Map.IO.In.ReadByte().ToString()), 8);
                    //Set the unknown flag as the first bit
                    placement_block.Flag_Unknown = flags[0];
                    //Set the place at start as the second bit
                    placement_block.Flag_Place_At_Start = flags[1];
                    //Set the asymmetrical flag as the third bit
                    placement_block.Flag_Asymmetrical = flags[2];
                    //Set the symmetrical flag as the fourth bit.
                    placement_block.Flag_Symmetrical = flags[3];

                    //Read our team index
                    placement_block.Team = User_Map.IO.In.ReadByte();
                    //Read our spare clips
                    placement_block.Spare_Clips = User_Map.IO.In.ReadByte();
                    //Read our respawn time.
                    placement_block.Respawn_Time = User_Map.IO.In.ReadByte();

                    //Read our unused.
                    placement_block.unused_68 = User_Map.IO.In.ReadBytes(16);

                    //Add the placement block to the list.
                    Add(placement_block);
                    //If our placement block's pallete index isn't null
                    if (placement_block.Tags_Index != -1)
                    {
                        //Add the placement block to the parent pallete block's list
                        User_Map.Pallette_Blocks[placement_block.Tags_Index].Placement_Blocks.Add(placement_block);
                    }
                }
            }

            #endregion

            #region Functions

            public void SavePlacementBlocks(HaloUserMap User_Map)
            {
                //Loop through all the placement blocks.
                for (int i = 0; i < Count; i++)
                {
                    //Go to that placement block's offset.
                    User_Map.IO.Out.BaseStream.Position = this[i].Index_Offset + User_Map.SandboxOffset;
                    //Write our blockType
                    User_Map.IO.Out.Write((int) this[i].Block_Type);
                    //Write our unused
                    User_Map.IO.Out.Write(this[i].unused_4);

                    //Write the tags index which is the index of the pallete to use.
                    User_Map.IO.Out.Write(this[i].Tags_Index);

                    //Write our X Coordinate
                    User_Map.IO.Out.Write(this[i].X);
                    //Write our Y Coordinate
                    User_Map.IO.Out.Write(this[i].Y);
                    //Write our Z Coordinate
                    User_Map.IO.Out.Write(this[i].Z);
                    //Write our Yaw Rotation Coordinate
                    User_Map.IO.Out.Write(this[i].Yaw);
                    //Write our Pitch Rotation Coordinate
                    User_Map.IO.Out.Write(this[i].Pitch);
                    //Write our Roll Rotation Coordinate
                    User_Map.IO.Out.Write(this[i].Roll);

                    //Write the three unknown rotation values.
                    User_Map.IO.Out.Write(this[i].unknown_Rot_1);
                    User_Map.IO.Out.Write(this[i].unknown_Rot_2);
                    User_Map.IO.Out.Write(this[i].unknown_Rot_3);

                    //Write our 16 bytes of unused
                    User_Map.IO.Out.Write(this[i].unused_52);

                    //Write two unknowns.
                    User_Map.IO.Out.Write(this[i].unknown_60);
                    User_Map.IO.Out.Write(this[i].unknown_61);

                    //Initialize our flags list
                    List<bool> flags = new List<bool>();
                    //Set the unknown flag as the first bit
                    flags.Add(this[i].Flag_Unknown);
                    //Set the place at start as the second bit
                    flags.Add(this[i].Flag_Place_At_Start);
                    //Set the asymmetrical flag as the third bit
                    flags.Add(this[i].Flag_Asymmetrical);
                    //Set the symmetrical flag as the fourth bit.
                    flags.Add(this[i].Flag_Symmetrical);
                    //Add the 0 bits at the end
                    flags.Add(false);
                    flags.Add(false);
                    flags.Add(false);
                    flags.Add(false);

                    //Convert it to a byte and write it
                    User_Map.IO.Out.Write(byte.Parse(BitHelper.ConvertToWriteableInteger(flags).ToString()));

                    //Write our team index
                    User_Map.IO.Out.Write(this[i].Team);
                    //Write our spare clips
                    User_Map.IO.Out.Write(this[i].Spare_Clips);
                    //Write our respawn time.
                    User_Map.IO.Out.Write(this[i].Respawn_Time);

                    //Write our unused.
                    User_Map.IO.Out.Write(this[i].unused_68);
                }
            }

            #endregion
        }

        #endregion

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using HaloDeveloper.IO;

namespace Alteration.Patches.Memory_Patch.RTH_Data
{
    /// <summary>
    /// This class is used to read and build RTH data which can be poked to an Xbox 360 Development kit later on.
    /// </summary>
    public class RTHData
    {
        #region Values

        private RTHDataBlocks _rthdatablocks;
        private RTHDataHeader _rthdataheader;

        /// <summary>
        /// This class will contain information such as Author, Description, AuthorIP.
        /// </summary>
        public RTHDataHeader RTH_Data_Header
        {
            get { return _rthdataheader; }
            set { _rthdataheader = value; }
        }

        /// <summary>
        /// This class is a list containing all RTH Data blocks, indicating the offset of where to apply the change, the size of the data change, and the data itself to change to.
        /// </summary>
        public RTHDataBlocks RTH_Data_Blocks
        {
            get { return _rthdatablocks; }
            set { _rthdatablocks = value; }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// This way of initializing an instance of RTH Data will not read an existing RTH Data, but allow you to build a new one.
        /// </summary>
        public RTHData()
        {
            //Initialize our classes
            RTH_Data_Header = new RTHDataHeader();
            RTH_Data_Blocks = new RTHDataBlocks();
        }

        /// <summary>
        /// This way of initializing an instance of RTH Data would allow you to read an existing RTH Data file, and rebuild it, or apply it.
        /// </summary>
        /// <param name="stream">This is the stream to read the existing RTH Data from</param>
        public RTHData(Stream stream)
        {
            //Initialize our IO
            EndianIO IO = new EndianIO(stream, EndianType.BigEndian);
            //Open our IO
            IO.Open();
            //Initialize and read our RTHDataHeader
            RTH_Data_Header = new RTHDataHeader(IO);
            //Initialize RTH Blocks
            RTH_Data_Blocks = new RTHDataBlocks(IO);
            //Close our IO
            IO.Close();
        }

        #endregion

        #region Functions

        public void SaveData(Stream stream)
        {
            //Initialize our IO
            EndianIO IO = new EndianIO(stream, EndianType.BigEndian);
            //Open our IO
            IO.Open();
            //Write our header string
            IO.Out.WriteAsciiString(RTHDataHeader.HeaderString, 4);
            //Write our header version
            IO.Out.Write(RTHDataHeader.Version);
            //Write our block count
            IO.Out.Write(RTH_Data_Blocks.Count);
            //Write our patch type
            IO.Out.Write((int) RTH_Data_Header.Patch_Type);
            //Write our author name
            IO.Out.WriteAsciiString(RTH_Data_Header.Author_Name, 32);
            //Write our patch description
            IO.Out.WriteAsciiString(RTH_Data_Header.Patch_Description, 256);
            //Write our author IP
            IO.Out.WriteAsciiString(RTH_Data_Header.Watermark, 16);
            //Loop through our blocks
            for (int index = 0; index < RTH_Data_Blocks.Count; index++)
            {
                //Write our memory offset
                IO.Out.Write(RTH_Data_Blocks[index].Memory_Offset);
                //Write our block size
                IO.Out.Write(RTH_Data_Blocks[index].Block_Size);
                //Write our data block
                IO.Out.Write(RTH_Data_Blocks[index].Data_Block);
            }
            //Close our IO
            IO.Close();
        }

        #endregion

        #region Classes

        #region Nested type: RTHDataBlock

        public class RTHDataBlock
        {
            private int _blocksize;
            private byte[] _datablock;
            private uint _memoryoffset;

            /// <summary>
            /// This value will indicate the offset to start editting at in memory.
            /// </summary>
            public uint Memory_Offset
            {
                get { return _memoryoffset; }
                set { _memoryoffset = value; }
            }

            /// <summary>
            /// This value indicates the size of the block of data that will be written at the given offset.
            /// </summary>
            public int Block_Size
            {
                get { return _blocksize; }
                set { _blocksize = value; }
            }

            /// <summary>
            /// This value contains the information to be written as the specific memory offset.
            /// </summary>
            public byte[] Data_Block
            {
                get { return _datablock; }
                set { _datablock = value; }
            }
        }

        #endregion

        #region Nested type: RTHDataBlocks

        /// <summary>
        /// This class is a list containing all RTH Data blocks, indicating the offset of where to apply the change, the size of the data change, and the data itself to change to.
        /// </summary>
        public class RTHDataBlocks : List<RTHDataBlock>
        {
            /// <summary>
            /// This form of initialization will initialize a new instance of the List of RTH Data blocks.
            /// </summary>
            public RTHDataBlocks()
            {
            }

            /// <summary>
            /// This form of initialization will open an already existing RTH Data file, and read the RTH Data Blocks.
            /// </summary>
            /// <param name="IO"></param>
            public RTHDataBlocks(EndianIO IO)
            {
                //While we are not at the end of the file
                while (IO.In.BaseStream.Position != IO.In.BaseStream.Length)
                {
                    //Initialize a new RTH Data block
                    RTHDataBlock RTH_Data_Block = new RTHDataBlock();
                    //Read the memory offset
                    RTH_Data_Block.Memory_Offset = IO.In.ReadUInt32();
                    //Read our block size
                    RTH_Data_Block.Block_Size = IO.In.ReadInt32();
                    //Read our data block
                    RTH_Data_Block.Data_Block = IO.In.ReadBytes(RTH_Data_Block.Block_Size);
                    //Add it to our list
                    Add(RTH_Data_Block);
                }
            }
        }

        #endregion

        #region Nested type: RTHDataHeader

        /// <summary>
        /// This class will contain information such as Author, Description, AuthorIP.
        /// </summary>
        public class RTHDataHeader
        {
            #region Values

            public enum PatchType
            {
                NotSaveable = 0x00000000,
                Saveable = 0x01071992,
            }

            public const string HeaderString = "RTHP";
            public const float Version = 1;
            private readonly string _watermark;
            private readonly int _datablockcount;
            private string _authorname;
            private string _patchdescription;

            private PatchType _patchtype;

            /// <summary>
            /// This value will contain the count of RTH Data Blocks to read.
            /// </summary>
            public int Data_Block_Count
            {
                get { return _datablockcount; }
            }

            /// <summary>
            /// This value contains flags for the possible ways to handle the patch.
            /// </summary>
            public PatchType Patch_Type
            {
                get { return _patchtype; }
                set { _patchtype = value; }
            }

            /// <summary>
            /// This value can be read or assigned, and will be in RTH Data build files, indicating the authors name.
            /// </summary>
            public string Author_Name
            {
                get { return _authorname; }
                set
                {
                    if (value.Length > 32)
                    {
                        _authorname = value.Substring(0, 32);
                    }
                    else
                    {
                        _authorname = value;
                    }
                }
            }

            /// <summary>
            /// This value will be within a build file of RTH Data, indicating what the patch's use is for.
            /// </summary>
            public string Patch_Description
            {
                get { return _patchdescription; }
                set
                {
                    if (value.Length > 256)
                    {
                        _patchdescription = value.Substring(0, 256);
                    }
                    else
                    {
                        _patchdescription = value;
                    }
                }
            }

            /// <summary>
            /// This value may be read from a build file indicating the IP you received the file from.
            /// </summary>
            public string Watermark
            {
                get { return _watermark; }
            }

            #endregion

            #region Initialization

            /// <summary>
            /// This form of initialization will initialize a new instance of the List of RTH Data Header.
            /// </summary>
            public RTHDataHeader()
            {
                _watermark = DateTime.Now.Day.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\" + DateTime.Now.Year.ToString();
            }

            /// <summary>
            /// This form of initialization for the RTH Data Header will read an existing RTH Data file.
            /// </summary>
            /// <param name="IO">The stream from which to read the RTH Data Header</param>
            public RTHDataHeader(EndianIO IO)
            {
                //Go to offset 0
                IO.In.BaseStream.Position = 0;
                //Verify its an RTH Patch
                if (IO.In.ReadAsciiString(4) != HeaderString)
                {
                    throw new Exception(
                        "The following RTH Patch is an invalid patch and therefore will not be read/applied/handled.");
                }
                //Verify our version
                if (IO.In.ReadSingle() != Version)
                {
                    throw new Exception(
                        "The following RTH Patch is of a different version and it's architecture has most likely been updated, therefore the patch will and most likely cannot be applied.");
                }
                //Read our datablock count
                _datablockcount = IO.In.ReadInt32();
                //Read our patch type
                Patch_Type = (PatchType) IO.In.ReadInt32();
                //Read our Author Name
                Author_Name = IO.In.ReadAsciiString(32);
                //Read our patch description
                Patch_Description = IO.In.ReadAsciiString(256);
                //Read our author IP
                _watermark = IO.In.ReadAsciiString(16);
            }

            #endregion
        }

        #endregion

        #endregion
    }
}
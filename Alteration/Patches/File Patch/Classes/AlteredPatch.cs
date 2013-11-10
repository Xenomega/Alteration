using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using HaloDeveloper.IO;
using System.IO;
using HaloDeveloper.Map;
using HaloDeveloper.Locale;
using System.Windows.Forms;
using Alteration.Details;

namespace Alteration.Patches.File_Patch.Classes
{
    public class AlteredPatch
    {
        public class AlteredPatchHeader
        {
            #region Values
            private string _authorname;
            private string _patchdescription;
            private int _originalmapchecksum;
            private Image _previewimage;

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
            /// Our constant header value to indicate our type of file.
            /// </summary>
            public const string HeaderString = "ALTP";
            /// <summary>
            /// Our version to know what type of Altered Patch we are handling.
            /// </summary>
            public const float Version = 1;
            /// <summary>
            /// Our value indicating the CRC32 of the original .map file.
            /// </summary>
            public int Original_Map_Checksum
            {
                get { return _originalmapchecksum; }
                set { _originalmapchecksum = value; }
            }
            /// <summary>
            /// Our preview image for the file.
            /// </summary>
            public Image Preview_Image
            {
                get { return _previewimage; }
                set { _previewimage = value; }
            }
            #endregion

            #region Initialization
            public AlteredPatchHeader()
            {
                //Set our authorname.
                _authorname = Alteration.Details.AlterationDetails.Username;
                //Set our image as a blank image.
                _previewimage = (Image)new Bitmap(128, 128);
            }
            public AlteredPatchHeader(EndianIO IO)
            {
                //Go to offset 0
                IO.In.BaseStream.Position = 0;
                //Verify its an RTH Patch
                if (IO.In.ReadAsciiString(4) != HeaderString)
                {
                    throw new Exception(
                        "The following Patch is an invalid patch and therefore will not be read/applied/handled.");
                }
                //Verify our version
                if (IO.In.ReadSingle() != Version)
                {
                    throw new Exception(
                        "The following RTH Patch is of a different version and it's architecture has most likely been updated, therefore the patch will and most likely cannot be applied.");
                }
                //Read our Author Name
                Author_Name = IO.In.ReadAsciiString(32);
                //Read our patch description
                Patch_Description = IO.In.ReadAsciiString(256);
                //Read our original map checksum
                Original_Map_Checksum = IO.In.ReadInt32();
                //Read our image
                Preview_Image = Image.FromStream(new MemoryStream(IO.In.ReadBytes(0xA000)));
            }
            #endregion
        }
        public class AlteredDataBlock
        {
            private int _blocksize;
            private byte[] _datablock;
            private uint _memoryoffset;

            /// <summary>
            /// This value will indicate the offset to start editting at.
            /// </summary>
            public uint Offset
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
            /// This value contains the information to be written as the specific offset.
            /// </summary>
            public byte[] Data_Block
            {
                get { return _datablock; }
                set { _datablock = value; }
            }
        }
        public class AlteredDataBlocks : List<AlteredDataBlock>
        {

        }

        public void CreatePatch(HaloMap Orig, string ModdedMap, string PatchLocation, string Author, string Description)
        {

            //Write our patch file
            EndianIO pio = new EndianIO(PatchLocation, EndianType.LittleEndian);
            EndianIO mio = new EndianIO(ModdedMap, EndianType.LittleEndian);

            #region Old Code

            //Get our tag table starts for both maps
            int origTagTableStart = Orig.IndexItems[0].Offset;

            //Loop through all tags
            for (int index = 0; index < Orig.IndexHeader.tagCount; index++)
            {
                //If our indexed tag has a smaller offset than the current
                if (Orig.IndexItems[index].Offset < origTagTableStart)
                {
                    //Set that offset as our starting point.
                    origTagTableStart = Orig.IndexItems[index].Offset;
                }
            }

            /*
            //Get our tag table starts for both maps
            int modTagTableStart = Modded.IndexItems[0].Offset;

            //Loop through all tags
            for (int index = 0; index < Modded.IndexHeader.tagCount; index++)
            {
                //If our indexed tag has a smaller offset than the current
                if (Modded.IndexItems[index].Offset < modTagTableStart)
                {
                    //Set that offset as our starting point.
                    modTagTableStart = Modded.IndexItems[index].Offset;
                }
            }
            */
            //Get our table ends for both maps
            //int origTagTableEnd = new LocaleHandler(Orig).LocaleTables[0].LocaleTableIndexOffset;
            //int modTagTableEnd = new LocaleHandler(Modded).LocaleTables[0].LocaleTableIndexOffset;

            //Make sure our starts and ends match or else it won't be a good compare.
            //if (origTagTableStart != modTagTableStart && origTagTableEnd != modTagTableEnd)
            //{
            //Show our error message
            //    MessageBox.Show(
            //        "The following two maps didn't not have the same meta table sizes and therefore... a patch cannot be properly made of these maps. The patching process will be discontinued.");
            //Stop processing code
            //    return;
            // }

            #endregion

            //Open both map's IOs
            Orig.OpenIO();
            pio.Open();
            mio.Open();

            pio.Out.Write(Author);
            pio.Out.Write(Description);

            //Setup our size and buffer size
            int perferdBuffSize = 0x1000000;
            int size = (int)Orig.IO.Stream.Length;

            //Create our 3 buffers, this way we don't have to mess around later
            byte[] obuf = new byte[perferdBuffSize],
                mbuf = new byte[perferdBuffSize],
                cbuf = new byte[perferdBuffSize];

            //Set our positions
            Orig.IO.In.BaseStream.Position = origTagTableStart;
            mio.In.BaseStream.Position = origTagTableStart;

            //Now do our main compare loop :)
            unsafe
            {
                fixed (byte* pObuf = obuf, pMbuf = mbuf, pPbuf = cbuf)
                {
                    int* ip1 = (int*)pObuf;
                    int* ip2 = (int*)pMbuf;
                    int* ip3 = (int*)pPbuf;

                    int bufferSize = perferdBuffSize;
                    int patchStart = 0;
                    int patchSize = 0;
                    for (int x = origTagTableStart; x < size; x += perferdBuffSize)
                    {
                        //Read our data
                        bufferSize = Math.Min(perferdBuffSize, size - x);
                        Orig.IO.In.Read(obuf, 0, bufferSize);
                        mio.In.Read(mbuf, 0, bufferSize);

                        //Lets compare
                        for (int i = 0; i < bufferSize / 4; i++)
                        {
                            if (ip1[i] != ip2[i])
                            {
                                //Are we just starting this patch?
                                patchStart = x + (i * 4);
                                patchSize = 0;

                                while (i < (bufferSize / 4) && ip1[i] != ip2[i])
                                {
                                    ip3[patchSize / 4] = ip2[i];
                                    i++;
                                    patchSize += 4;
                                }

                                //Lets write out this data
                                pio.Out.Write(patchStart);
                                pio.Out.Write(patchSize);
                                pio.Out.Write(cbuf, 0, patchSize);
                            }
                        }
                    }
                }
            }

            //Close up our patch
            pio.Close();
            mio.Close();

        }

        public void ApplyPatch(string PatchLocation, string MapLocation)
        {


        }

    }
}
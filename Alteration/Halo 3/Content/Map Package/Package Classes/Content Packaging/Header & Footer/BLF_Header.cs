using HaloDeveloper.Helpers;
using HaloDeveloper.IO;

namespace Alteration.Halo_3.Map_Package.Info
{
    /// <summary>
    /// This is an instance of BLF Header, which will store our header information for various halo related files.
    /// </summary>
    public class BLF_Header
    {
        private string _blfstring;
        private int _filecontentsize;
        private string _filedescription;
        private int _filesize;

        private int _headersize;
        private int _mapid;
        private short _unknown10;
        private short _unknown12;
        private byte[] _unknown52;

        private short _unknown8;

        public BLF_Header(EndianIO IO, int position)
        {
            //Go to our selected position
            IO.In.BaseStream.Position = position;
            //Read the _blf head string
            BLF_String = IO.In.ReadAsciiString(4);
            //Read the headersize value
            HeaderSize = IO.In.ReadInt32();
            //Read our unknown values
            Unknown8 = IO.In.ReadInt16();
            Unknown10 = IO.In.ReadInt16();
            Unknown12 = IO.In.ReadInt16();
            //Read our file description
            FileDescription = ExtraFunctions.RemoveWhiteSpacingFromString(IO.In.ReadAsciiString(34));
            //Read our file size
            FileSize = IO.In.ReadInt32();
            //Read our unknown array
            Unknown52 = IO.In.ReadBytes(8);
            //Read our mapID
            Map_ID = IO.In.ReadInt32();
            //Read our fileContentSize value.
            FileContentSize = IO.In.ReadInt32();
        }

        public BLF_Header()
        {
        }

        public string BLF_String
        {
            get { return _blfstring; }
            set { _blfstring = value; }
        }

        public int HeaderSize
        {
            get { return _headersize; }
            set { _headersize = value; }
        }

        public short Unknown8
        {
            get { return _unknown8; }
            set { _unknown8 = value; }
        }

        public short Unknown10
        {
            get { return _unknown10; }
            set { _unknown10 = value; }
        }

        public short Unknown12
        {
            get { return _unknown12; }
            set { _unknown12 = value; }
        }

        public string FileDescription
        {
            get { return _filedescription; }
            set { _filedescription = value; }
        }

        public int FileSize
        {
            get { return _filesize; }
            set { _filesize = value; }
        }

        public byte[] Unknown52
        {
            get { return _unknown52; }
            set { _unknown52 = value; }
        }

        public int Map_ID
        {
            get { return _mapid; }
            set { _mapid = value; }
        }

        public int FileContentSize
        {
            get { return _filecontentsize; }
            set { _filecontentsize = value; }
        }
    }
}
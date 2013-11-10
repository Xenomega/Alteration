using HaloDeveloper.IO;

namespace Alteration.Halo_3.Map_Package.Package_Classes
{
    public class EOF_Footer
    {
        private string _eofstring;

        private int _footersize;

        private byte[] _unknown8;

        public EOF_Footer(EndianIO IO, int position)
        {
            //Go to our selected position
            IO.In.BaseStream.Position = position;
            //Read the _blf head string
            EOF_String = IO.In.ReadAsciiString(4);
            //Read our FooterSize value which indicates the size of the entire EOF_Footer
            FooterSize = IO.In.ReadInt32();
            //Read our remaining data
            Unknown8 = IO.In.ReadBytes(FooterSize - 8);
        }

        public string EOF_String
        {
            get { return _eofstring; }
            set { _eofstring = value; }
        }

        public int FooterSize
        {
            get { return _footersize; }
            set { _footersize = value; }
        }

        public byte[] Unknown8
        {
            get { return _unknown8; }
            set { _unknown8 = value; }
        }
    }
}
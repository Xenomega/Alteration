using System.IO;
using Alteration.Halo_3.Map_Package.Package_Classes;
using HaloDeveloper.IO;

namespace Alteration.Halo_3.Map_Package.Info
{
    /// <summary>
    /// This class should be initialized to view, edit, or create .info files for Map Packages.
    /// </summary>
    public class InfoFile
    {
        private BLF_Header _blfheader;
        private string _chinesedescription;
        private string _chinesename;
        private string _englishdescription;

        private string _englishname;
        private EOF_Footer _eoffooter;
        private string _frenchdescription;
        private string _frenchname;
        private string _germandescription;
        private string _germanname;
        private string _italiandescription;
        private string _italianname;
        private string _japanesedescription;

        private string _japanesename;
        private string _koreandescription;
        private string _koreanname;
        private string _latinamericanricaspanishdescription;
        private string _latinamericaspanishname;
        private string _mapfilename;
        private string _mapimagefilename;
        private string _portuguesedescription;
        private string _portuguesename;
        private string _spanishdescription;
        private string _spanishname;

        public InfoFile(Stream stream)
        {
            //Initialize our IO.
            EndianIO IO = new EndianIO(stream, EndianType.BigEndian);

            //Open the IO
            IO.Open();

            //Read our blf header, which is at offset 0
            BLFHeader = new BLF_Header(IO, 0);

            //Go to our entry table
            IO.In.BaseStream.Position = 0x44;

            //Read our Names
            EnglishName = IO.In.ReadUnicodeString(0x20);
            JapaneseName = IO.In.ReadUnicodeString(0x20);
            GermanName = IO.In.ReadUnicodeString(0x20);
            FrenchName = IO.In.ReadUnicodeString(0x20);
            SpanishName = IO.In.ReadUnicodeString(0x20);
            LatinAmericaSpanishName = IO.In.ReadUnicodeString(0x20);
            ItalianName = IO.In.ReadUnicodeString(0x20);
            KoreanName = IO.In.ReadUnicodeString(0x20);
            ChineseName = IO.In.ReadUnicodeString(0x20);

            //Skip unimportant
            IO.In.ReadBytes(0x40);

            PortugueseName = IO.In.ReadUnicodeString(0x20);

            //Skip unimportant
            IO.In.ReadBytes(0x40);

            //Read our descriptions
            EnglishDescription = IO.In.ReadUnicodeString(0x80);
            JapaneseDescription = IO.In.ReadUnicodeString(0x80);
            GermanDescription = IO.In.ReadUnicodeString(0x80);
            FrenchDescription = IO.In.ReadUnicodeString(0x80);
            SpanishDescription = IO.In.ReadUnicodeString(0x80);
            LatinAmericanSpanishDescription = IO.In.ReadUnicodeString(0x80);
            ItalianDescription = IO.In.ReadUnicodeString(0x80);
            KoreanDescription = IO.In.ReadUnicodeString(0x80);
            ChineseDescription = IO.In.ReadUnicodeString(0x80);

            //Skip unimportant
            IO.In.ReadBytes(0x100);

            PortugueseDescription = IO.In.ReadUnicodeString(0x80);

            //Skip unimportant
            IO.In.ReadBytes(0x100);

            //Read the resource filenames
            MapImageFileName = IO.In.ReadAsciiString(0x100);
            MapFileName = IO.In.ReadAsciiString(0x100);

            //Skip unimportant
            IO.In.ReadBytes(0x3C3C);

            //Read our End of file footer
            EOFFooter = new EOF_Footer(IO, (int) IO.In.BaseStream.Position);

            //Close our IO
            IO.Close();
        }

        public InfoFile()
        {
        }

        public BLF_Header BLFHeader
        {
            get { return _blfheader; }
            set { _blfheader = value; }
        }

        public string EnglishName
        {
            get { return _englishname; }
            set { _englishname = value; }
        }

        public string JapaneseName
        {
            get { return _japanesename; }
            set { _japanesename = value; }
        }

        public string GermanName
        {
            get { return _germanname; }
            set { _germanname = value; }
        }

        public string FrenchName
        {
            get { return _frenchname; }
            set { _frenchname = value; }
        }

        public string SpanishName
        {
            get { return _spanishname; }
            set { _spanishname = value; }
        }

        public string LatinAmericaSpanishName
        {
            get { return _latinamericaspanishname; }
            set { _latinamericaspanishname = value; }
        }

        public string ItalianName
        {
            get { return _italianname; }
            set { _italianname = value; }
        }

        public string KoreanName
        {
            get { return _koreanname; }
            set { _koreanname = value; }
        }

        public string ChineseName
        {
            get { return _chinesename; }
            set { _chinesename = value; }
        }

        public string PortugueseName
        {
            get { return _portuguesename; }
            set { _portuguesename = value; }
        }

        public string EnglishDescription
        {
            get { return _englishdescription; }
            set { _englishdescription = value; }
        }

        public string JapaneseDescription
        {
            get { return _japanesedescription; }
            set { _japanesedescription = value; }
        }

        public string GermanDescription
        {
            get { return _germandescription; }
            set { _germandescription = value; }
        }

        public string FrenchDescription
        {
            get { return _frenchdescription; }
            set { _frenchdescription = value; }
        }

        public string SpanishDescription
        {
            get { return _spanishdescription; }
            set { _spanishdescription = value; }
        }

        public string LatinAmericanSpanishDescription
        {
            get { return _latinamericanricaspanishdescription; }
            set { _latinamericanricaspanishdescription = value; }
        }

        public string ItalianDescription
        {
            get { return _italiandescription; }
            set { _italiandescription = value; }
        }

        public string KoreanDescription
        {
            get { return _koreandescription; }
            set { _koreandescription = value; }
        }

        public string ChineseDescription
        {
            get { return _chinesedescription; }
            set { _chinesedescription = value; }
        }

        public string PortugueseDescription
        {
            get { return _portuguesedescription; }
            set { _portuguesedescription = value; }
        }

        public string MapImageFileName
        {
            get { return _mapimagefilename; }
            set { _mapimagefilename = value; }
        }

        public string MapFileName
        {
            get { return _mapfilename; }
            set { _mapfilename = value; }
        }

        public EOF_Footer EOFFooter
        {
            get { return _eoffooter; }
            set { _eoffooter = value; }
        }

        public void SaveInfoFile(Stream stream)
        {
            //Create our instance of our IO
            EndianIO IO = new EndianIO(stream, EndianType.BigEndian);
            //Open our IO
            IO.Open();

            //BLF Header

            #region BLF Header

            //Write our _blf string
            IO.Out.WriteAsciiString("_blf", 0x4);
            //Write the headersize
            IO.Out.Write(48);
            //Write the unknown values
            IO.Out.Write((short) 1);
            IO.Out.Write((short) 2);
            IO.Out.Write((short) -2);
            //Write the whitespace of blank values
            IO.Out.Write(new byte[0x22]);
            //Write our ending portion
            IO.Out.WriteAsciiString("levl", 0x4);
            IO.Out.Write((short) 0);
            IO.Out.Write(new byte[0x4] {0x4D, 0x50, 0x00, 0x03});
            IO.Out.Write((short) 1);
            IO.Out.Write(BLFHeader.Map_ID);
            IO.Out.Write(76);

            #endregion

            //Write entires

            #region Entries

            //Go to our entry table
            IO.Out.BaseStream.Position = 0x44;

            //Write our names.
            IO.Out.WriteUnicodeString(EnglishName, 0x20);
            IO.Out.WriteUnicodeString(JapaneseName, 0x20);
            IO.Out.WriteUnicodeString(GermanName, 0x20);
            IO.Out.WriteUnicodeString(FrenchName, 0x20);
            IO.Out.WriteUnicodeString(SpanishName, 0x20);
            IO.Out.WriteUnicodeString(LatinAmericaSpanishName, 0x20);
            IO.Out.WriteUnicodeString(ItalianName, 0x20);
            IO.Out.WriteUnicodeString(KoreanName, 0x20);
            IO.Out.WriteUnicodeString(ChineseName, 0x20);

            //Write our blank
            IO.Out.Write(new byte[0x40]);

            //Finish reading our names
            IO.Out.WriteUnicodeString(PortugueseName, 0x20);

            //Write our blank
            IO.Out.Write(new byte[0x40]);

            //Write our descriptions
            IO.Out.WriteUnicodeString(EnglishDescription, 0x80);
            IO.Out.WriteUnicodeString(JapaneseDescription, 0x80);
            IO.Out.WriteUnicodeString(GermanDescription, 0x80);
            IO.Out.WriteUnicodeString(FrenchDescription, 0x80);
            IO.Out.WriteUnicodeString(SpanishDescription, 0x80);
            IO.Out.WriteUnicodeString(LatinAmericanSpanishDescription, 0x80);
            IO.Out.WriteUnicodeString(ItalianDescription, 0x80);
            IO.Out.WriteUnicodeString(KoreanDescription, 0x80);
            IO.Out.WriteUnicodeString(ChineseDescription, 0x80);

            //Write our blank
            IO.Out.Write(new byte[0x100]);

            //Finish reading descriptions
            IO.Out.WriteUnicodeString(PortugueseDescription, 0x80);

            //Write our blank
            IO.Out.Write(new byte[0x100]);

            //Write our resource filenames
            IO.Out.WriteAsciiString(MapImageFileName, 0x100);
            IO.Out.WriteAsciiString(MapFileName, 0x100);

            //Write unimportant
            IO.Out.Write(new byte[0x3C3C]);

            #endregion

            //TODO: Write the End of File Footer

            #region EOF Footer

            int totalSize = (int) IO.Stream.Position;
            IO.Out.WriteAsciiString("_eof", 4);
            IO.Out.Write(0x111);
            IO.Out.Write(0x10001);
            IO.Out.Write(totalSize);
            IO.Out.Write((byte) 0x03);
            IO.Out.Write(new byte[0x100]);

            #endregion

            //Close our IO
            IO.Close();
        }
    }
}
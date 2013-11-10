using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Alteration.Halo_3.Map_Package.Info;
using HaloDeveloper.IO;

namespace Alteration.Halo_3.Map_Package.Package_Classes
{
    /// <summary>
    /// This class, when initialized, will provide specific image information for a map file.
    /// </summary>
    public class BLFImageFile
    {
        #region BLFImageType enum

        public enum BLFImageType
        {
            BaseImage = 0, //JFIF
            Clip = 1, //PNG
            Film = 1, //PNG
            Sm = 0, //JFIF
            Variant = 1 //PNG
        }

        #endregion

        private BLF_Header _blfheader;

        private Image _blfimage;

        private EOF_Footer _eoffooter;

        public BLFImageFile(Stream stream)
        {
            //Create our instance of the IO
            EndianIO IO = new EndianIO(stream, EndianType.BigEndian);
            //Open our IO
            IO.Open();
            //Read our BLF Header
            BLFHeader = new BLF_Header(IO, 0);
            //Read our image
            BLFImage = Image.FromStream(new MemoryStream(IO.In.ReadBytes(BLFHeader.FileContentSize)));
            //Read our End of File
            EOFFooter = new EOF_Footer(IO, (int) IO.In.BaseStream.Position);
            //CloseIO
            IO.Close();
        }

        public BLFImageFile()
        {
        }

        public BLF_Header BLFHeader
        {
            get { return _blfheader; }
            set { _blfheader = value; }
        }

        public Image BLFImage
        {
            get { return _blfimage; }
            set { _blfimage = value; }
        }

        public EOF_Footer EOFFooter
        {
            get { return _eoffooter; }
            set { _eoffooter = value; }
        }

        public void SaveBLFImage(Stream stream, BLFImageType imgType)
        {
            //Create our instance of our IO
            EndianIO IO = new EndianIO(stream, EndianType.BigEndian);
            //Open our IO
            IO.Open();
            //Write our BLF Header

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
            IO.Out.WriteAsciiString("mapi", 0x4);
            //Create our image stream.
            MemoryStream imageStream = new MemoryStream();
            //Write our image to the stream
            if (imgType == BLFImageType.BaseImage | imgType == BLFImageType.Sm)
            {
                BLFImage.Save(imageStream, ImageFormat.Jpeg);
            }
            else
            {
                BLFImage.Save(imageStream, ImageFormat.Png);
            }
            //Get a useable byte array from it
            byte[] imageData = imageStream.ToArray();
            //Close our imageStream
            imageStream.Close();
            //Write imageData.length+20
            IO.Out.Write(imageData.Length + 20);
            IO.Out.Write((short) 1);
            IO.Out.Write((short) 1);
            //Write our enum for image type.
            //JFIF = 0
            //PNG = 1
            IO.Out.Write((int) imgType);
            //Write our content File Size(image length)
            IO.Out.Write(imageData.Length);

            #endregion

            //Write our image data
            IO.Out.Write(imageData);
            //Write our End of File

            #region EOF Footer

            //get our current location for a size
            int totalSize = (int) IO.Stream.Position;
            //Write our EOF Data
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
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using HaloDeveloper.IO;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.IO.Compression;
using HaloDeveloper.Map;

namespace HaloDeveloper.Raw.BitmapRaw
{
    public struct BitmapSubmap
    {
        public int Width;
        public int Height;
        public int Depth;
        public TextureType Type;
        public TextureFormat Format;
        public int RawLength;
        public byte Index1;
        public byte Index2;

        public int vWidth { get { return GetVirtualDimension(Width); } }
        public int vHeight { get { return GetVirtualDimension(Height); } }

        public BitmapSubmap(EndianReader er)
        {
            er.ReadInt32();//bitm

            Width = er.ReadInt16();
            Height = er.ReadInt16();
            Depth = er.ReadInt16();//maybe?
            Type = (TextureType)er.ReadInt16();
            Format = (TextureFormat)er.ReadInt16();

            er.ReadInt16();//flags
            er.ReadInt16();//regx
            er.ReadInt16();//regy
            er.ReadByte();// Unknown 1
            er.ReadByte();// Unknown 2
            Index1 = er.ReadByte();// Index 1?
            Index2 = er.ReadByte();// Index 2?
            er.ReadInt32();// size or offset mb?
            er.ReadInt32();
            er.ReadInt32();
            RawLength = er.ReadInt32();
            er.ReadInt32();
            er.ReadInt32();
        }

        public bool IsDefined { get { return Enum.IsDefined(typeof(TextureFormat), Format); } }
        public bool IsSupported
        {
            get
            {
                if (Format == TextureFormat.DXT1 || Format == TextureFormat.DXT3 ||
                    Format == TextureFormat.DXT5 || Format == TextureFormat.CTX1 ||
                    Format == TextureFormat.DXN || Format == TextureFormat.A8R8G8B8)
                    return true;

                return false;
            }
        }

        public enum TextureType : short
        {
            Texture2D = 0x0000,
            Texture3D = 0x0001,
            CubeMap = 0x0002,
            Sprite = 0x0003,
            UIBitmap = 0x0004
        }

        public int GetVirtualDimension(int Size)
        {
            if (Size % 128 != 0)
                Size += (128 - Size % 128);
            return Size;
        }
    }

    public struct BitmapInfo
    {
        HaloMap map;
        public List<BitmapSubmap> bitmapList;
        public List<int> rawIdTable1;
        public List<int> rawIdTable2;

        public BitmapInfo(HaloMap Map, int TagIndex)
        {
            map = Map;
            map.OpenIO();
            // Get our bitmap info first
            map.IO.SeekTo(map.IndexItems[TagIndex].Offset + 96);
            int count = map.IO.In.ReadInt32();
            int pointer = map.IO.In.ReadInt32() - map.MapHeader.mapMagic;
            bitmapList = new List<BitmapSubmap>();
            map.IO.SeekTo(pointer);
            for (int x = 0; x < count; x++)
                bitmapList.Add(new BitmapSubmap(map.IO.In));

            // Get our raw ID now
            map.IO.SeekTo(map.IndexItems[TagIndex].Offset + 140);
            count = map.IO.In.ReadInt32();
            pointer = map.IO.In.ReadInt32() - map.MapHeader.mapMagic;
            rawIdTable1 = new List<int>();
            map.IO.SeekTo(pointer);
            for (int x = 0; x < count; x++)
            {
                rawIdTable1.Add(map.IO.In.ReadInt32());
                map.IO.In.ReadInt32();
            }

            // Get our raw ID now
            map.IO.SeekTo(map.IndexItems[TagIndex].Offset + 152);
            count = map.IO.In.ReadInt32();
            pointer = map.IO.In.ReadInt32() - map.MapHeader.mapMagic;
            rawIdTable2 = new List<int>();
            map.IO.SeekTo(pointer);
            for (int x = 0; x < count; x++)
            {
                rawIdTable2.Add(map.IO.In.ReadInt32());
                map.IO.In.ReadInt32();
            }
            map.CloseIO();
        }

        public byte[] GetBitmapData(int BitmapDataIndex)
        {
            int ID = -1;
            int rawLength = 0;
            int offset = 0;

            // Get our raw id
            if (rawIdTable1.Count != 0)
            {
                ID = rawIdTable1[0];
                rawLength = bitmapList[BitmapDataIndex].RawLength;
            }
            else
            {
                if (rawIdTable2.Count != 0)
                {
                    ID = rawIdTable2[bitmapList[BitmapDataIndex].Index1];

                    for (int x = 0; x < bitmapList.Count; x++)
                    {
                        if (bitmapList[x].Index1 == bitmapList[BitmapDataIndex].Index1)
                        {
                            rawLength += bitmapList[x].RawLength;

                            if (x == BitmapDataIndex)
                                break;

                            offset += bitmapList[x].RawLength;
                        }
                    }
                }
            }

            if (ID == -1)
                return new byte[0];

            // We have our data
            byte[] data = map.RawInformation.GetRawDataFromID(ID, rawLength);

            // Now lets copy over to a new buffer if our size isnt zero and our length is bigger
            if (offset != 0)
            {
                byte[] newBuffer = new byte[bitmapList[BitmapDataIndex].RawLength];
                Array.Copy(data, offset, newBuffer, 0, newBuffer.Length);
                data = newBuffer;
            }

            // Lets return our data now
            return data;
        }

        public Bitmap GeneratePreview(int BitmapIndex)
        {
            BitmapSubmap bitmapData = bitmapList[BitmapIndex];

            // Make sure this format can be previewed
            if (!bitmapData.IsSupported)
            {
                Bitmap bitmap = new Bitmap(200, 200);
                Graphics g = Graphics.FromImage(bitmap);
                g.Clear(Color.CornflowerBlue);
                string drawString = "No Preview Available";
                SizeF size = g.MeasureString(drawString, new Font(FontFamily.GenericSerif, 15f));
                g.DrawString(drawString, new Font(FontFamily.GenericSerif, 15f),
                    Brushes.Black,
                    new PointF(100 - (size.Width / 2), 100 - (size.Height / 2)));
                return bitmap;
            }

            // Setup our new sizes for this level
            int height = bitmapData.vHeight;
            int width = bitmapData.vWidth;

            // Convert it to a linear texture
            byte[] data = GetBitmapData(BitmapIndex);
            //File.WriteAllBytes("C:\\normal2.bin", data);

            if (bitmapData.IsDefined)
                data = DXTDecoder.ConvertToLinearTexture(data,
                   width, height, bitmapData.Format);

            // Decode the dxt
            switch (bitmapData.Format)
            {
                case TextureFormat.A8:
                    data = DXTDecoder.DecodeA8(data, width, height);
                    break;
                case TextureFormat.Y8:
                    data = DXTDecoder.DecodeY8(data, width, height);
                    break;
                case TextureFormat.AY8:
                    data = DXTDecoder.DecodeAY8(data, width, height);
                    break;
                case TextureFormat.A8Y8:
                    data = DXTDecoder.DecodeA8Y8(data, width, height);
                    break;
                case TextureFormat.R5G6B5:
                    data = DXTDecoder.DecodeR5G6B5(data, width, height);
                    break;
                case TextureFormat.A1R5G5B5:
                    data = DXTDecoder.DecodeA1R5G5B5(data, width, height);
                    break;
                case TextureFormat.A4R4G4B4:
                    data = DXTDecoder.DecodeA4R4G4B4(data, width, height);
                    break;
                case TextureFormat.X8R8G8B8:
                case TextureFormat.A8R8G8B8:
                    // Do nothing its fine how it is
                    break;
                case TextureFormat.DXT1:
                    data = DXTDecoder.DecodeDXT1(data, width, height);
                    break;
                case TextureFormat.DXT3:
                    data = DXTDecoder.DecodeDXT3(data, width, height);
                    break;
                case TextureFormat.DXT5:
                    data = DXTDecoder.DecodeDXT5(data, width, height);
                    break;
                case TextureFormat.DXN:
                    data = DXTDecoder.DecodeDXN(data, width, height);
                    break;
                case TextureFormat.CTX1:
                    data = DXTDecoder.DecodeCTX1(data, width, height);
                    break;
                default:
                    return null;
                // throw new ArgumentOutOfRangeException();
            }

            // Now lets create a bitmap from it
            Bitmap bmp = new Bitmap(bitmapData.Width, bitmapData.Height, PixelFormat.Format32bppArgb);
            Rectangle rect = new Rectangle(0, 0, bitmapData.Width, bitmapData.Height);
            BitmapData bmpdata = bmp.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            // Loop and copy what we have removing any padding
            byte[] newData1 = new byte[bitmapData.Width * bitmapData.Height * 4];
            for (int x = 0; x < bitmapData.Height; x++)
                Array.Copy(data, x * width * 4, newData1, x * bitmapData.Width * 4, bitmapData.Width * 4);

            Marshal.Copy(newData1, 0, bmpdata.Scan0, newData1.Length);
            bmp.UnlockBits(bmpdata);

            // Return our image in our pic box now
            return bmp;
        }

        public void ExtractRaw(string FilePath, int BitmapIndex)
        {
            // Create a new raw file
            FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);

            // Now lets write it out
            bw.Write(GetBitmapData(BitmapIndex));

            // Close our new raw
            bw.Close();
        }

        public void Extract(string FilePath, int BitmapIndex)
        {
            // Create a new dds file
            FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);

            BitmapSubmap sm = bitmapList[BitmapIndex];

            // Create a new dds header with our texture info and write it
            new DDS(sm).Write(bw);

            // Setup our new sizes for this level
            int height = sm.vHeight;
            int width = sm.vWidth;

            // Untile the data
            byte[] data = DXTDecoder.ConvertToLinearTexture(GetBitmapData(BitmapIndex),
                width, height, sm.Format);

            // Now endian swap all the data if its DXT
            if (sm.Format != TextureFormat.A8R8G8B8)
                for (int i = 0; i < data.Length; i += 2)
                    Array.Reverse(data, i, 2);

            // Now lets write it out
            bw.Write(data);

            // Close our new dds
            bw.Close();
        }
    }

    public class BitmapFunctions
    {
        public static BitmapInfo GetBitmapInfo(HaloMap Map, int TagIndex)
        {
            return new BitmapInfo(Map, TagIndex);
        }
    }
}
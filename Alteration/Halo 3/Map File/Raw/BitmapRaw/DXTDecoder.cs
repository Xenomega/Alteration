using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;

namespace HaloDeveloper.Raw.BitmapRaw
{
    public enum TextureFormat : short
    {
        A8 = 0x0000,
        Y8 = 0x0001,
        AY8 = 0x0002,
        A8Y8 = 0x0003,
        R5G6B5 = 0x0006,
        A1R5G5B5 = 0x0008,
        A4R4G4B4 = 0x0009,
        X8R8G8B8 = 0x000A,
        A8R8G8B8 = 0x000B,
        DXT1 = 0x000E,
        DXT3 = 0x000F,
        DXT5 = 0x0010,
        P8 = 0x11,
        Lightmap = 0x12,
        U8V8 = 0x16,
        DXN = 0x0021,
        CTX1 = 0x0022,
        //Bitch = 0x0026
    }

    public struct DDS
    {
        public int dwMagic;

        // DDSURFACEDESC2
        public int dwSize;
        public DDSD dwFlags;
        public int dwHeight;
        public int dwWidth;
        public int dwPitchOrLinearSize;
        public int dwDepth;
        public int dwMipMapCount;
        public int[] dwReserved1;

        // DDPIXELFORMAT
        public int dwSize2;
        public DDPF dwFlags2;
        public FourCC dwFourCC;
        public int dwRGBBitCount;
        public int dwRBitMask, dwGBitMask, dwBBitMask;
        public uint dwRGBAlphaBitMask;

        // DDSCAPS2;
        public DDSCAPS dwCaps1;
        public DDSCAPS2 dwCaps2;
        public int[] Reserved2;

        public int dwReserved3;

        public DDS(HaloDeveloper.Raw.BitmapRaw.BitmapSubmap BitmapInfo)
        {
            dwMagic = 0x20534444; // "DDS "
            dwSize = 0x7C; // Size of DDSURFACEDESC2 struct

            // Setup the basic flags that we know we will always have
            dwFlags = 0;
            dwFlags |= DDSD.CAPS;
            dwFlags |= DDSD.HEIGHT;
            dwFlags |= DDSD.WIDTH;
            dwFlags |= DDSD.PIXELFORMAT;

            // Set our height and width
            dwHeight = BitmapInfo.vHeight;
            dwWidth = BitmapInfo.vWidth;

            // We will provide a pitch or linear size
            if (BitmapInfo.Format == TextureFormat.A8R8G8B8)
            {
                dwFlags |= DDSD.PITCH;
                dwPitchOrLinearSize = dwWidth * 4;
            }
            else
            {
                dwFlags |= DDSD.LINEARSIZE;
                dwPitchOrLinearSize = BitmapInfo.RawLength;// Size of first mip
            }
            dwDepth = 0; // I havent seen this used yet in xtd's

            // We will provide a mipmap count if theres more then 1 level
            //if (BitmapInfo.Levels > 1)
            //{
            //    dwFlags |= DDSD.MIPMAPCOUNT;
            //    dwMipMapCount = BitmapInfo.Levels;
            //}
            //else
            dwMipMapCount = 0;

            dwReserved1 = new int[11]; // Reserved

            dwSize2 = 0x00000020; // Size of DDPIXELFORMAT struct

            // Fill out our flags and fourcc
            dwFlags2 = 0;
            dwFourCC = FourCC.None;
            switch (BitmapInfo.Format)
            {
                case TextureFormat.DXT1:
                    dwFlags2 |= DDPF.FOURCC;
                    dwFourCC = FourCC.DXT1;
                    break;
                case TextureFormat.DXT3:
                    dwFlags2 |= DDPF.FOURCC;
                    dwFourCC = FourCC.DXT3;
                    break;
                case TextureFormat.DXT5:
                    dwFlags2 |= DDPF.FOURCC;
                    dwFourCC = FourCC.DXT5;
                    break;
                case TextureFormat.A8R8G8B8:
                    dwFlags2 |= DDPF.RGB;
                    dwFlags2 |= DDPF.ALPHAPIXELS;
                    break;
            }

            // Fill in the rest of the struct
            if (BitmapInfo.Format == TextureFormat.A8R8G8B8)
            {
                dwRGBBitCount = 0x20;
                dwRBitMask = 0x00ff0000;
                dwGBitMask = 0x0000ff00;
                dwBBitMask = 0x000000ff;
                dwRGBAlphaBitMask = 0xff000000;
            }
            else
            {
                dwRGBBitCount = 0x00000000;
                dwRBitMask = 0x00000000;
                dwGBitMask = 0x00000000;
                dwBBitMask = 0x00000000;
                dwRGBAlphaBitMask = 0x00000000;
            }

            // Fill in our DDSCAPS2 info
            dwCaps1 = 0;
            dwCaps1 |= DDSCAPS.TEXTURE;
            //if (BitmapInfo.Levels > 1)
            //{
            //    dwCaps1 |= DDSCAPS.COMPLEX;
            //    dwCaps1 |= DDSCAPS.MIPMAP;
            //}
            dwCaps2 = 0;
            Reserved2 = new int[2];

            dwReserved3 = 0x00000000;
        }

        public void Read(BinaryReader br)
        {
            dwMagic = br.ReadInt32();

            // DDSURFACEDESC2
            dwSize = br.ReadInt32();
            dwFlags = (DDSD)br.ReadInt32();
            dwHeight = br.ReadInt32();
            dwWidth = br.ReadInt32();
            dwPitchOrLinearSize = br.ReadInt32();
            dwDepth = br.ReadInt32();
            dwMipMapCount = br.ReadInt32();
            dwReserved1 = new int[11];
            for (int x = 0; x < dwReserved1.Length; x++)
                dwReserved1[x] = br.ReadInt32();

            // DDPIXELFORMAT
            dwSize2 = br.ReadInt32();
            dwFlags2 = (DDPF)br.ReadInt32();
            dwFourCC = (FourCC)br.ReadInt32();
            dwRGBBitCount = br.ReadInt32();
            dwRBitMask = br.ReadInt32();
            dwGBitMask = br.ReadInt32();
            dwBBitMask = br.ReadInt32();
            dwRGBAlphaBitMask = br.ReadUInt32();

            // DDSCAPS2;
            dwCaps1 = (DDSCAPS)br.ReadInt32();
            dwCaps2 = (DDSCAPS2)br.ReadInt32();
            Reserved2 = new int[2];
            for (int x = 0; x < Reserved2.Length; x++)
                Reserved2[x] = br.ReadInt32();

            dwReserved3 = br.ReadInt32();
        }

        public void Write(BinaryWriter bw)
        {
            bw.Write(dwMagic);

            // DDSURFACEDESC2
            bw.Write(dwSize);
            bw.Write((int)dwFlags);
            bw.Write(dwHeight);
            bw.Write(dwWidth);
            bw.Write(dwPitchOrLinearSize);
            bw.Write(dwDepth);
            bw.Write(dwMipMapCount);
            for (int x = 0; x < dwReserved1.Length; x++)
                bw.Write(dwReserved1[x]);

            // DDPIXELFORMAT
            bw.Write(dwSize2);
            bw.Write((int)dwFlags2);
            bw.Write((int)dwFourCC);
            bw.Write(dwRGBBitCount);
            bw.Write(dwRBitMask);
            bw.Write(dwGBitMask);
            bw.Write(dwBBitMask);
            bw.Write(dwRGBAlphaBitMask);

            // DDSCAPS2;
            bw.Write((int)dwCaps1);
            bw.Write((int)dwCaps2);
            for (int x = 0; x < Reserved2.Length; x++)
                bw.Write(Reserved2[x]);

            bw.Write(dwReserved3);
        }

        public enum FourCC : int
        {
            None = 0x00000000,
            DXT1 = 0x31545844,
            DXT3 = 0x33545844,
            DXT5 = 0x35545844
        }

        [Flags]
        public enum DDSD : int
        {
            CAPS = 0x00000001,
            HEIGHT = 0x00000002,
            WIDTH = 0x00000004,
            PITCH = 0x00000008,
            PIXELFORMAT = 0x00001000,
            MIPMAPCOUNT = 0x00020000,
            LINEARSIZE = 0x00080000,
            DEPTH = 0x00800000
        }

        [Flags]
        public enum DDPF : int
        {
            ALPHAPIXELS = 0x00000001,
            FOURCC = 0x00000004,
            RGB = 0x00000040
        }

        [Flags]
        public enum DDSCAPS : int
        {
            COMPLEX = 0x00000008,
            TEXTURE = 0x00001000,
            MIPMAP = 0x00400000
        }

        [Flags]
        public enum DDSCAPS2 : int
        {
            CUBEMAP = 0x00000200,
            CUBEMAP_POSITIVEX = 0x00000400,
            CUBEMAP_NEGATIVEX = 0x00000800,
            CUBEMAP_POSITIVEY = 0x00001000,
            CUBEMAP_NEGATIVEY = 0x00002000,
            CUBEMAP_POSITIVEZ = 0x00004000,
            CUBEMAP_NEGATIVEZ = 0x00008000,
            VOLUME = 0x00200000
        }
    }

    internal static class DXTDecoder
    {
        public static byte[] DecodeA8(byte[] data, int width, int height)
        {
            byte[] buffer2 = new byte[height * width * 4];
            for (int x = 0; x < height * width; x++)
            {
                int index = x * 4;
                buffer2[index] = 0xff;
                buffer2[index + 1] = 0xff;
                buffer2[index + 2] = 0xff;
                buffer2[index + 3] = data[x];
            }
            return buffer2;
        }

        public static byte[] DecodeY8(byte[] data, int width, int height)
        {
            byte[] buffer2 = new byte[height * width * 4];
            for (int x = 0; x < height * width; x++)
            {
                int index = x * 4;
                buffer2[index] = data[x];
                buffer2[index + 1] = data[x];
                buffer2[index + 2] = data[x];
                buffer2[index + 3] = 0xff;
            }
            return buffer2;
        }

        public static byte[] DecodeAY8(byte[] data, int width, int height)
        {
            byte[] buffer2 = new byte[height * width * 4];
            for (int x = 0; x < height * width; x++)
            {
                int index = x * 4;
                buffer2[index] = data[x];
                buffer2[index + 1] = data[x];
                buffer2[index + 2] = data[x];
                buffer2[index + 3] = data[x];
            }
            return buffer2;
        }

        public static byte[] DecodeA8Y8(byte[] data, int width, int height)
        {
            byte[] buffer2 = new byte[height * width * 4];
            for (int x = 0; x < height * width * 2; x += 2)
            {
                buffer2[x * 2] = data[x + 1];
                buffer2[(x * 2) + 1] = data[x + 1];
                buffer2[(x * 2) + 2] = data[x + 1];
                buffer2[(x * 2) + 3] = data[x];
            }
            return buffer2;
        }

        public static byte[] DecodeR5G6B5(byte[] data, int width, int height)
        {
            byte[] decoded = new byte[width * height * 4];
            for (int x = 0; x < width * height * 2; x += 2)
            {
                short temp = (short)(data[x] | (data[x + 1] << 8));

                decoded[(x * 2) + 0] = (byte)(temp & 0x1F);
                decoded[(x * 2) + 1] = (byte)((temp >> 5) & 0x3F);
                decoded[(x * 2) + 2] = (byte)((temp >> 11) & 0x1F);
                decoded[(x * 2) + 3] = 0xFF;
            }
            return decoded;
        }

        public static byte[] DecodeA1R5G5B5(byte[] data, int width, int height)
        {
            byte[] decoded = new byte[width * height * 4];
            for (int x = 0; x < width * height * 2; x += 2)
            {
                short temp = (short)(data[x] | (data[x + 1] << 8));

                decoded[(x * 2) + 0] = (byte)(temp & 0x1F);
                decoded[(x * 2) + 1] = (byte)((temp >> 5) & 0x3F);
                decoded[(x * 2) + 2] = (byte)((temp >> 11) & 0x1F);
                decoded[(x * 2) + 3] = 0xFF;
            }
            return decoded;
        }

        public static byte[] DecodeA4R4G4B4(byte[] data, int width, int height)
        {
            byte[] buffer2 = new byte[width * height * 4];
            for (int x = 0; x < width * height * 2; x += 2)
            {
                buffer2[(x * 2) + 3] = (byte)(data[x] & 0xF0);
                buffer2[(x * 2) + 2] = (byte)(data[x] & 0x0F);
                buffer2[(x * 2) + 1] = (byte)(data[x + 1] & 0xF0);
                buffer2[x * 2] = (byte)(data[x + 1] & 0x0F);
            }
            return buffer2;
        }

        public static byte[] DecodeDXT1(byte[] data, int width, int height)
        {
            byte[] pixData = new byte[width * height * 4];
            int xBlocks = width / 4;
            int yBlocks = height / 4;
            for (int y = 0; y < yBlocks; y++)
            {
                for (int x = 0; x < xBlocks; x++)
                {
                    int blockDataStart = ((y * xBlocks) + x) * 8;

                    uint color0 = ((uint)data[blockDataStart + 0] << 8) + data[blockDataStart + 1];
                    uint color1 = ((uint)data[blockDataStart + 2] << 8) + data[blockDataStart + 3];

                    uint code = BitConverter.ToUInt32(data, blockDataStart + 4);

                    ushort r0 = 0, g0 = 0, b0 = 0, r1 = 0, g1 = 0, b1 = 0;
                    r0 = (ushort)(8 * (color0 & 31));
                    g0 = (ushort)(4 * ((color0 >> 5) & 63));
                    b0 = (ushort)(8 * ((color0 >> 11) & 31));

                    r1 = (ushort)(8 * (color1 & 31));
                    g1 = (ushort)(4 * ((color1 >> 5) & 63));
                    b1 = (ushort)(8 * ((color1 >> 11) & 31));

                    for (int k = 0; k < 4; k++)
                    {
                        int j = k ^ 1;
                        for (int i = 0; i < 4; i++)
                        {
                            int pixDataStart = (width * (y * 4 + j) * 4) + ((x * 4 + i) * 4);
                            uint codeDec = code & 0x3;

                            switch (codeDec)
                            {
                                case 0:
                                    pixData[pixDataStart + 0] = (byte)r0;
                                    pixData[pixDataStart + 1] = (byte)g0;
                                    pixData[pixDataStart + 2] = (byte)b0;
                                    pixData[pixDataStart + 3] = 255;
                                    break;
                                case 1:
                                    pixData[pixDataStart + 0] = (byte)r1;
                                    pixData[pixDataStart + 1] = (byte)g1;
                                    pixData[pixDataStart + 2] = (byte)b1;
                                    pixData[pixDataStart + 3] = 255;
                                    break;
                                case 2:
                                    pixData[pixDataStart + 3] = 255;
                                    if (color0 > color1)
                                    {
                                        pixData[pixDataStart + 0] = (byte)((2 * r0 + r1) / 3);
                                        pixData[pixDataStart + 1] = (byte)((2 * g0 + g1) / 3);
                                        pixData[pixDataStart + 2] = (byte)((2 * b0 + b1) / 3);
                                    }
                                    else
                                    {
                                        pixData[pixDataStart + 0] = (byte)((r0 + r1) / 2);
                                        pixData[pixDataStart + 1] = (byte)((g0 + g1) / 2);
                                        pixData[pixDataStart + 2] = (byte)((b0 + b1) / 2);
                                    }
                                    break;
                                case 3:
                                    if (color0 > color1)
                                    {
                                        pixData[pixDataStart + 0] = (byte)((r0 + 2 * r1) / 3);
                                        pixData[pixDataStart + 1] = (byte)((g0 + 2 * g1) / 3);
                                        pixData[pixDataStart + 2] = (byte)((b0 + 2 * b1) / 3);
                                        pixData[pixDataStart + 3] = 255;
                                    }
                                    else
                                    {
                                        pixData[pixDataStart + 0] = 0;
                                        pixData[pixDataStart + 1] = 0;
                                        pixData[pixDataStart + 2] = 0;
                                        pixData[pixDataStart + 3] = 0;
                                    }
                                    break;
                            }

                            code >>= 2;
                        }
                    }
                }
            }
            return pixData;
        }

        public static byte[] DecodeDXT3(byte[] data, int width, int height)
        {
            byte[] pixData = new byte[width * height * 4];
            int xBlocks = width / 4;
            int yBlocks = height / 4;
            for (int y = 0; y < yBlocks; y++)
            {
                for (int x = 0; x < xBlocks; x++)
                {
                    int blockDataStart = ((y * xBlocks) + x) * 16;
                    ushort[] alphaData = new ushort[4];

                    alphaData[0] = (ushort)((data[blockDataStart + 0] << 8) + data[blockDataStart + 1]);
                    alphaData[1] = (ushort)((data[blockDataStart + 2] << 8) + data[blockDataStart + 3]);
                    alphaData[2] = (ushort)((data[blockDataStart + 4] << 8) + data[blockDataStart + 5]);
                    alphaData[3] = (ushort)((data[blockDataStart + 6] << 8) + data[blockDataStart + 7]);

                    byte[,] alpha = new byte[4, 4];
                    for (int j = 0; j < 4; j++)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            alpha[i, j] = (byte)((alphaData[j] & 0xF) * 16);
                            alphaData[j] >>= 4;
                        }
                    }

                    ushort color0 = (ushort)((data[blockDataStart + 8] << 8) + data[blockDataStart + 9]);
                    ushort color1 = (ushort)((data[blockDataStart + 10] << 8) + data[blockDataStart + 11]);

                    uint code = BitConverter.ToUInt32(data, blockDataStart + 8 + 4);

                    ushort r0 = 0, g0 = 0, b0 = 0, r1 = 0, g1 = 0, b1 = 0;
                    r0 = (ushort)(8 * (color0 & 31));
                    g0 = (ushort)(4 * ((color0 >> 5) & 63));
                    b0 = (ushort)(8 * ((color0 >> 11) & 31));

                    r1 = (ushort)(8 * (color1 & 31));
                    g1 = (ushort)(4 * ((color1 >> 5) & 63));
                    b1 = (ushort)(8 * ((color1 >> 11) & 31));

                    for (int k = 0; k < 4; k++)
                    {
                        int j = k ^ 1;
                        for (int i = 0; i < 4; i++)
                        {
                            int pixDataStart = (width * (y * 4 + j) * 4) + ((x * 4 + i) * 4);
                            uint codeDec = code & 0x3;

                            pixData[pixDataStart + 3] = alpha[i, j];

                            switch (codeDec)
                            {
                                case 0:
                                    pixData[pixDataStart + 0] = (byte)r0;
                                    pixData[pixDataStart + 1] = (byte)g0;
                                    pixData[pixDataStart + 2] = (byte)b0;
                                    break;
                                case 1:
                                    pixData[pixDataStart + 0] = (byte)r1;
                                    pixData[pixDataStart + 1] = (byte)g1;
                                    pixData[pixDataStart + 2] = (byte)b1;
                                    break;
                                case 2:
                                    if (color0 > color1)
                                    {
                                        pixData[pixDataStart + 0] = (byte)((2 * r0 + r1) / 3);
                                        pixData[pixDataStart + 1] = (byte)((2 * g0 + g1) / 3);
                                        pixData[pixDataStart + 2] = (byte)((2 * b0 + b1) / 3);
                                    }
                                    else
                                    {
                                        pixData[pixDataStart + 0] = (byte)((r0 + r1) / 2);
                                        pixData[pixDataStart + 1] = (byte)((g0 + g1) / 2);
                                        pixData[pixDataStart + 2] = (byte)((b0 + b1) / 2);
                                    }
                                    break;
                                case 3:
                                    if (color0 > color1)
                                    {
                                        pixData[pixDataStart + 0] = (byte)((r0 + 2 * r1) / 3);
                                        pixData[pixDataStart + 1] = (byte)((g0 + 2 * g1) / 3);
                                        pixData[pixDataStart + 2] = (byte)((b0 + 2 * b1) / 3);
                                    }
                                    else
                                    {
                                        pixData[pixDataStart + 0] = 0;
                                        pixData[pixDataStart + 1] = 0;
                                        pixData[pixDataStart + 2] = 0;
                                    }
                                    break;
                            }

                            code >>= 2;
                        }
                    }
                }
            }
            return pixData;
        }

        public static byte[] DecodeDXT5(byte[] data, int width, int height)
        {
            byte[] pixData = new byte[width * height * 4];
            int xBlocks = width / 4;
            int yBlocks = height / 4;
            for (int y = 0; y < yBlocks; y++)
            {
                for (int x = 0; x < xBlocks; x++)
                {
                    int blockDataStart = ((y * xBlocks) + x) * 16;
                    uint[] alphas = new uint[8];
                    ulong alphaMask = 0;

                    alphas[0] = data[blockDataStart + 1];
                    alphas[1] = data[blockDataStart + 0];

                    alphaMask |= data[blockDataStart + 6];
                    alphaMask <<= 8;
                    alphaMask |= data[blockDataStart + 7];
                    alphaMask <<= 8;
                    alphaMask |= data[blockDataStart + 4];
                    alphaMask <<= 8;
                    alphaMask |= data[blockDataStart + 5];
                    alphaMask <<= 8;
                    alphaMask |= data[blockDataStart + 2];
                    alphaMask <<= 8;
                    alphaMask |= data[blockDataStart + 3];

                    // 8-alpha or 6-alpha block
                    if (alphas[0] > alphas[1])
                    {
                        // 8-alpha block: derive the other 6
                        // Bit code 000 = alpha_0, 001 = alpha_1, others are interpolated.
                        alphas[2] = (byte)((6 * alphas[0] + 1 * alphas[1] + 3) / 7);    // bit code 010
                        alphas[3] = (byte)((5 * alphas[0] + 2 * alphas[1] + 3) / 7);    // bit code 011
                        alphas[4] = (byte)((4 * alphas[0] + 3 * alphas[1] + 3) / 7);    // bit code 100
                        alphas[5] = (byte)((3 * alphas[0] + 4 * alphas[1] + 3) / 7);    // bit code 101
                        alphas[6] = (byte)((2 * alphas[0] + 5 * alphas[1] + 3) / 7);    // bit code 110
                        alphas[7] = (byte)((1 * alphas[0] + 6 * alphas[1] + 3) / 7);    // bit code 111
                    }
                    else
                    {
                        // 6-alpha block.
                        // Bit code 000 = alpha_0, 001 = alpha_1, others are interpolated.
                        alphas[2] = (byte)((4 * alphas[0] + 1 * alphas[1] + 2) / 5);    // Bit code 010
                        alphas[3] = (byte)((3 * alphas[0] + 2 * alphas[1] + 2) / 5);    // Bit code 011
                        alphas[4] = (byte)((2 * alphas[0] + 3 * alphas[1] + 2) / 5);    // Bit code 100
                        alphas[5] = (byte)((1 * alphas[0] + 4 * alphas[1] + 2) / 5);    // Bit code 101
                        alphas[6] = 0x00;                                               // Bit code 110
                        alphas[7] = 0xFF;                                               // Bit code 111
                    }

                    byte[,] alpha = new byte[4, 4];

                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            alpha[j, i] = (byte)alphas[alphaMask & 7];
                            alphaMask >>= 3;
                        }
                    }

                    ushort color0 = (ushort)((data[blockDataStart + 8] << 8) + data[blockDataStart + 9]);
                    ushort color1 = (ushort)((data[blockDataStart + 10] << 8) + data[blockDataStart + 11]);

                    uint code = BitConverter.ToUInt32(data, blockDataStart + 8 + 4);

                    ushort r0 = 0, g0 = 0, b0 = 0, r1 = 0, g1 = 0, b1 = 0;
                    r0 = (ushort)(8 * (color0 & 31));
                    g0 = (ushort)(4 * ((color0 >> 5) & 63));
                    b0 = (ushort)(8 * ((color0 >> 11) & 31));

                    r1 = (ushort)(8 * (color1 & 31));
                    g1 = (ushort)(4 * ((color1 >> 5) & 63));
                    b1 = (ushort)(8 * ((color1 >> 11) & 31));

                    for (int k = 0; k < 4; k++)
                    {
                        int j = k ^ 1;
                        for (int i = 0; i < 4; i++)
                        {
                            int pixDataStart = (width * (y * 4 + j) * 4) + ((x * 4 + i) * 4);
                            uint codeDec = code & 0x3;

                            pixData[pixDataStart + 3] = alpha[i, j];

                            switch (codeDec)
                            {
                                case 0:
                                    pixData[pixDataStart + 0] = (byte)r0;
                                    pixData[pixDataStart + 1] = (byte)g0;
                                    pixData[pixDataStart + 2] = (byte)b0;
                                    break;
                                case 1:
                                    pixData[pixDataStart + 0] = (byte)r1;
                                    pixData[pixDataStart + 1] = (byte)g1;
                                    pixData[pixDataStart + 2] = (byte)b1;
                                    break;
                                case 2:
                                    if (color0 > color1)
                                    {
                                        pixData[pixDataStart + 0] = (byte)((2 * r0 + r1) / 3);
                                        pixData[pixDataStart + 1] = (byte)((2 * g0 + g1) / 3);
                                        pixData[pixDataStart + 2] = (byte)((2 * b0 + b1) / 3);
                                    }
                                    else
                                    {
                                        pixData[pixDataStart + 0] = (byte)((r0 + r1) / 2);
                                        pixData[pixDataStart + 1] = (byte)((g0 + g1) / 2);
                                        pixData[pixDataStart + 2] = (byte)((b0 + b1) / 2);
                                    }
                                    break;
                                case 3:
                                    if (color0 > color1)
                                    {
                                        pixData[pixDataStart + 0] = (byte)((r0 + 2 * r1) / 3);
                                        pixData[pixDataStart + 1] = (byte)((g0 + 2 * g1) / 3);
                                        pixData[pixDataStart + 2] = (byte)((b0 + 2 * b1) / 3);
                                    }
                                    else
                                    {
                                        pixData[pixDataStart + 0] = 0;
                                        pixData[pixDataStart + 1] = 0;
                                        pixData[pixDataStart + 2] = 0;
                                    }
                                    break;
                            }

                            code >>= 2;
                        }
                    }
                }
            }
            return pixData;
        }

        public static byte[] DecodeCTX1(byte[] data, int width, int height)
        {
            byte[] DestData = new Byte[(width * height) * 4];

            int dptr = 0;
            for (int i = 0; i < width * height; i += 16)
            {
                int c1 = data[dptr + 1] << 8 | data[dptr];
                int c2 = data[dptr + 3] << 8 | data[dptr + 2];

                RGBAColor[] colorTable = new RGBAColor[4];
                colorTable[0].R = data[dptr];
                colorTable[0].G = data[dptr + 1];
                colorTable[1].R = data[dptr + 2];
                colorTable[1].G = data[dptr + 3];

                if (c1 > c2)
                {
                    colorTable[2] = GradientColors(colorTable[0], colorTable[1]);
                    colorTable[3] = GradientColors(colorTable[1], colorTable[0]);
                }
                else
                {
                    colorTable[2] = GradientColorsHalf(colorTable[0], colorTable[1]);
                    colorTable[3] = colorTable[0];
                }

                int CData = (data[dptr + 5]) |
                    (data[dptr + 4] << 8) |
                    (data[dptr + 7] << 16) |
                    (data[dptr + 6] << 24);

                int ChunkNum = i / 16;
                int XPos = ChunkNum % (width / 4);
                int YPos = (ChunkNum - XPos) / (width / 4);

                int sizeh = (height < 4) ? height : 4;
                int sizew = (width < 4) ? width : 4;

                for (int x = 0; x < sizeh; x++)
                    for (int y = 0; y < sizew; y++)
                    {
                        RGBAColor CColor = colorTable[CData & 3];
                        CData >>= 2;

                        int tmp1 = ((YPos * 4 + x) * width + XPos * 4 + y) * 4;

                        float cx = ((CColor.R / 255.0f) * 2.0f) - 1.0f;
                        float cy = ((CColor.G / 255.0f) * 2.0f) - 1.0f;
                        float cz = (float)Math.Sqrt(Math.Max(0.0f, Math.Min(1.0f, 1.0f - cx * cx - cy * cy)));

                        DestData[tmp1] = (byte)(((cz + 1.0f) / 2.0f) * 255.0f);//(255 - (Math.Abs(CColor.R - CColor.G)));
                        DestData[tmp1 + 1] = (byte)CColor.G;
                        DestData[tmp1 + 2] = (byte)CColor.R;
                        DestData[tmp1 + 3] = 255;
                    }
                dptr += 8;
            }

            return DestData;
        }

        public static byte[] DecodeDXN(byte[] data, int width, int height)
        {
            byte[] destData = new byte[height * width * 4];
            RGBAColor cColor;
            int chunksPerHLine = width / 4;
            if (chunksPerHLine == 0)
                chunksPerHLine = 1;

            for (int i = 0; i < (width * height); i += 16)
            {
                byte XMin = data[i + 1];
                byte XMax = data[i + 0];

                byte[] XIndices = new byte[16];
                int temp = (data[i + 5] << 16) | (data[i + 2] << 8) | data[i + 3];
                int indicies = 0;
                for (; indicies < 8; indicies++)
                {
                    XIndices[indicies] = (byte)(temp & 7);
                    temp >>= 3;
                }
                temp = (data[i + 6] << 16) | (data[i + 7] << 8) | data[i + 4];
                for (; indicies < 16; indicies++)
                {
                    XIndices[indicies] = (byte)(temp & 7);
                    temp >>= 3;
                }

                byte YMin = data[i + 9];
                byte YMax = data[i + 8];

                byte[] YIndices = new byte[16];
                temp = (data[i + 13] << 16) | (data[i + 10] << 8) | data[i + 11];
                indicies = 0;
                for (; indicies < 8; indicies++)
                {
                    YIndices[indicies] = (byte)(temp & 7);
                    temp >>= 3;
                }
                temp = (data[i + 14] << 16) | (data[i + 15] << 8) | data[i + 12];
                for (; indicies < 16; indicies++)
                {
                    YIndices[indicies] = (byte)(temp & 7);
                    temp >>= 3;
                }

                byte[] XTable = new byte[8];
                XTable[0] = XMin;
                XTable[1] = XMax;
                if (XTable[0] > XTable[1])
                {
                    XTable[2] = (byte)(((XMax - XMin) * (1 / 7.0f)) + XMin);
                    XTable[3] = (byte)(((XMax - XMin) * (2 / 7.0f)) + XMin);
                    XTable[4] = (byte)(((XMax - XMin) * (3 / 7.0f)) + XMin);
                    XTable[5] = (byte)(((XMax - XMin) * (4 / 7.0f)) + XMin);
                    XTable[6] = (byte)(((XMax - XMin) * (5 / 7.0f)) + XMin);
                    XTable[7] = (byte)(((XMax - XMin) * (6 / 7.0f)) + XMin);
                }
                else
                {
                    XTable[2] = (byte)(((XMax - XMin) * (1 / 5.0f)) + XMin);
                    XTable[3] = (byte)(((XMax - XMin) * (2 / 5.0f)) + XMin);
                    XTable[4] = (byte)(((XMax - XMin) * (3 / 5.0f)) + XMin);
                    XTable[5] = (byte)(((XMax - XMin) * (4 / 5.0f)) + XMin);
                    XTable[6] = XMin;
                    XTable[7] = XMax;
                }

                byte[] YTable = new byte[8];
                YTable[0] = YMin;
                YTable[1] = YMax;
                if (YTable[0] > YTable[1])
                {
                    YTable[2] = (byte)(((YMax - YMin) * (1 / 7.0f)) + YMin);
                    YTable[3] = (byte)(((YMax - YMin) * (2 / 7.0f)) + YMin);
                    YTable[4] = (byte)(((YMax - YMin) * (3 / 7.0f)) + YMin);
                    YTable[5] = (byte)(((YMax - YMin) * (4 / 7.0f)) + YMin);
                    YTable[6] = (byte)(((YMax - YMin) * (5 / 7.0f)) + YMin);
                    YTable[7] = (byte)(((YMax - YMin) * (6 / 7.0f)) + YMin);
                }
                else
                {
                    YTable[2] = (byte)(((YMax - YMin) * (1 / 5.0f)) + YMin);
                    YTable[3] = (byte)(((YMax - YMin) * (2 / 5.0f)) + YMin);
                    YTable[4] = (byte)(((YMax - YMin) * (3 / 5.0f)) + YMin);
                    YTable[5] = (byte)(((YMax - YMin) * (4 / 5.0f)) + YMin);
                    YTable[6] = YMin;
                    YTable[7] = YMax;
                }

                int chunkNum = i / 16;
                int xPos = chunkNum % chunksPerHLine;
                int yPos = (chunkNum - xPos) / chunksPerHLine;
                int sizeh = height < 4 ? height : 4;
                int sizew = width < 4 ? width : 4;
                for (int x = 0; x < sizeh; x++)
                {
                    for (int y = 0; y < sizew; y++)
                    {
                        cColor.R = (byte)XTable[XIndices[(x * sizeh) + y]];
                        cColor.G = (byte)YTable[YIndices[(x * sizeh) + y]];
                        float cx = ((cColor.R / 255.0f) * 2.0f) - 1.0f;
                        float cy = ((cColor.G / 255.0f) * 2.0f) - 1.0f;
                        float cz = (float)Math.Sqrt(Math.Max(0.0f, Math.Min(1.0f, 1.0f - cx * cx - cy * cy)));
                        cColor.B = (byte)(((cz + 1.0f) / 2.0f) * 255.0f);
                        cColor.A = 0xFF;

                        temp = ((yPos * 4 + x) * width + xPos * 4 + y) * 4;
                        destData[temp] = (byte)cColor.B;
                        destData[temp + 1] = (byte)cColor.G;
                        destData[temp + 2] = (byte)cColor.R;
                        destData[temp + 3] = (byte)cColor.A;
                    }
                }
            }
            return destData;
        }

        private struct RGBAColor
        {
            public int R, G, B, A;
        }

        private static RGBAColor GradientColors(RGBAColor Color1, RGBAColor Color2)
        {
            RGBAColor newColor;
            newColor.R = (byte)(((Color1.R * 2 + Color2.R)) / 3);
            newColor.G = (byte)(((Color1.G * 2 + Color2.G)) / 3);
            newColor.B = (byte)(((Color1.B * 2 + Color2.B)) / 3);
            newColor.A = 0xFF;
            return newColor;
        }

        private static RGBAColor GradientColorsHalf(RGBAColor Color1, RGBAColor Color2)
        {
            RGBAColor newColor;
            newColor.R = (byte)(Color1.R / 2 + Color2.R / 2);
            newColor.G = (byte)(Color1.G / 2 + Color2.G / 2);
            newColor.B = (byte)(Color1.B / 2 + Color2.B / 2);
            newColor.A = 0xFF;
            return newColor;
        }

        public static byte[] ConvertToLinearTexture(byte[] data, int width, int height, TextureFormat texture)
        {
            return ModifyLinearTexture(data, width, height, texture, true);
        }

        public static byte[] ConvertFromLinearTexture(byte[] data, int width, int height, TextureFormat texture)
        {
            return ModifyLinearTexture(data, width, height, texture, false);
        }

        private static byte[] ModifyLinearTexture(byte[] data, int width, int height, TextureFormat texture, bool toLinear)
        {
            // Set up our destination buffer
            byte[] destData = new byte[data.Length];

            // Figure out our block size and texel pitch
            int blockSizeRow, blockSizeColumn;
            int texelPitch;
            switch (texture)
            {
                case TextureFormat.DXT1:
                case TextureFormat.CTX1:
                    blockSizeRow = 4;
                    blockSizeColumn = 4;
                    texelPitch = 8;
                    break;
                case TextureFormat.DXT3:
                case TextureFormat.DXT5:
                case TextureFormat.DXN:
                    blockSizeRow = 4;
                    blockSizeColumn = 4;
                    texelPitch = 16;
                    break;
                case TextureFormat.AY8:
                    blockSizeRow = 4;
                    blockSizeColumn = 4;
                    texelPitch = 2;
                    break;
                case TextureFormat.A8R8G8B8:
                default:
                    blockSizeRow = 1;
                    blockSizeColumn = 1;
                    texelPitch = 2;
                    break;
                //throw new ArgumentOutOfRangeException("Bad type!");
            }

            // Figure out our block height and width
            int blockWidth = width / blockSizeRow;
            int blockHeight = height / blockSizeColumn;

            // Loop through the height and width and copy our data
            try
            {
                for (int j = 0; j < blockHeight; j++)
                    for (int i = 0; i < blockWidth; i++)
                    {
                        int blockOffset = j * blockWidth + i;

                        int x = XGAddress2DTiledX(blockOffset, blockWidth, texelPitch);
                        int y = XGAddress2DTiledY(blockOffset, blockWidth, texelPitch);

                        int srcOffset = j * blockWidth * texelPitch + i * texelPitch;
                        int destOffset = y * blockWidth * texelPitch + x * texelPitch;

                        if (toLinear)
                            Array.Copy(data, srcOffset, destData, destOffset, texelPitch);
                        else
                            Array.Copy(data, destOffset, destData, srcOffset, texelPitch);
                    }
            }
            catch
            { }

            return destData;
        }

        private static int XGAddress2DTiledX(int Offset, int Width, int TexelPitch)
        {
            int AlignedWidth = (Width + 31) & ~31;

            int LogBpp = (TexelPitch >> 2) + ((TexelPitch >> 1) >> (TexelPitch >> 2));
            int OffsetB = Offset << LogBpp;
            int OffsetT = ((OffsetB & ~4095) >> 3) + ((OffsetB & 1792) >> 2) + (OffsetB & 63);
            int OffsetM = OffsetT >> (7 + LogBpp);

            int MacroX = ((OffsetM % (AlignedWidth >> 5)) << 2);
            int Tile = ((((OffsetT >> (5 + LogBpp)) & 2) + (OffsetB >> 6)) & 3);
            int Macro = (MacroX + Tile) << 3;
            int Micro = ((((OffsetT >> 1) & ~15) + (OffsetT & 15)) & ((TexelPitch << 3) - 1)) >> LogBpp;

            return Macro + Micro;
        }

        private static int XGAddress2DTiledY(int Offset, int Width, int TexelPitch)
        {
            int AlignedWidth = (Width + 31) & ~31;

            int LogBpp = (TexelPitch >> 2) + ((TexelPitch >> 1) >> (TexelPitch >> 2));
            int OffsetB = Offset << LogBpp;
            int OffsetT = ((OffsetB & ~4095) >> 3) + ((OffsetB & 1792) >> 2) + (OffsetB & 63);
            int OffsetM = OffsetT >> (7 + LogBpp);

            int MacroY = ((OffsetM / (AlignedWidth >> 5)) << 2);
            int Tile = ((OffsetT >> (6 + LogBpp)) & 1) + (((OffsetB & 2048) >> 10));
            int Macro = (MacroY + Tile) << 3;
            int Micro = ((((OffsetT & (((TexelPitch << 6) - 1) & ~31)) + ((OffsetT & 15) << 1)) >> (3 + LogBpp)) & ~1);

            return Macro + Micro + ((OffsetT & 16) >> 4);
        }
    }
}
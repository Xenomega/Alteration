using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using HaloDevelopmentExtender;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using Alteration.Settings;
using HaloDeveloper.Helpers;

namespace Alteration.Halo_3
{
    public abstract class H3Screenshot
    {
        public static Image TakeScreenshot(XboxDebugCommunicator xdc)
        {
            //Set our temporary DDS fileName
            string tempPath = Application.StartupPath + "\\TempScreenshot.dds";
            //Save a screenshot
            xdc.Screenshot(tempPath);
            //Decode our image
            Image img = Deswizzle(tempPath);
            //Delete our tempFile
            File.Delete(tempPath);
            return img;
        }

        public static unsafe void GammaCorrect(double gamma, BitmapData imageData)
        {
            gamma = Math.Max(0.1, Math.Min(5.0, gamma));
            double y = 1.0 / gamma;
            byte[] table = new byte[0x100];
            for (int x = 0; x < 0x100; x++)
            {
                table[x] = (byte)Math.Min(0xff,
                    (int)((Math.Pow(((double)x) / 255.0, y) * 255.0) + 0.5));
            }

            int width = imageData.Width;
            int height = imageData.Height;
            int num3 = width * ((imageData.PixelFormat == PixelFormat.Format8bppIndexed) ? 1 : 3);
            int num4 = imageData.Stride - num3;
            byte* numPtr = (byte*)imageData.Scan0.ToPointer();
            for (int i = 0; i < height; i++)
            {
                int num6 = 0;
                while (num6 < num3)
                {
                    numPtr[0] = table[numPtr[0]];
                    num6++;
                    numPtr++;
                }
                numPtr += num4;
            }
        }

        public static Bitmap ResizeImage(Bitmap Orig)
        {
            // If we don't want to resize just return our orig
            if (!AppSettings.Settings.ResizeScreenshots)
                return Orig;

            // Create a new image
            Bitmap newImg = new Bitmap(AppSettings.Settings.ScreenshotWidth,
                AppSettings.Settings.ScreenshotHeight);

            // Draw our new image
            using (Graphics g = Graphics.FromImage((Image)newImg))
                g.DrawImage(Orig, 0, 0, AppSettings.Settings.ScreenshotWidth,
                    AppSettings.Settings.ScreenshotHeight);

            // Now return our image
            return newImg;
        }

        [DllImport("kernel32.dll")]
        private static extern void RtlMoveMemory(IntPtr src, byte[] temp, int cb);

        private static Image Deswizzle(string FilePath)
        {
            //Open the temp dds
            FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            EndianReader br = new EndianReader(fs, EndianType.LittleEndian);

            //Read the dds header
            br.BaseStream.Position = 0x0C;
            int height = br.ReadInt32();
            int width = br.ReadInt32();

            //Read our random bytes
            br.BaseStream.Position = 0x5C;
            string randomBuf = ExtraFunctions.BytesToHexString(br.ReadBytes(12));

            //Read the buffer
            br.BaseStream.Position = 0x80;
            int size = width * height * 4;
            byte[] buffer = br.ReadBytes(size);
            br.Close();

            //A2R10G10B10
            if (randomBuf == "FF03000000FC0F000000F03F")
            {
                Image img = DeswizzleA2R10G10B10(buffer, width, height);
                //See if we have to fix the gamma
                if (AppSettings.Settings.AdjustGamma)
                {
                    BitmapData imageData = ((Bitmap)img).LockBits(
                        new Rectangle(0, 0, img.Width, img.Height),
                        ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                    GammaCorrect(AppSettings.Settings.GammaValue, imageData);
                    ((Bitmap)img).UnlockBits(imageData);
                }
                return img;
            }
            //A8R8G8B8?
            else if (randomBuf == "0000FF0000FF0000FF000000")
                return DeswizzleA8R8G8B8(buffer, width, height);
            return null;
        }
        private static Image DeswizzleA2R10G10B10(byte[] buffer, int width, int height)
        {
            //Loop through and convert each pixle
            int index = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    uint argb = BitConverter.ToUInt32(buffer, index);

                    uint r = ((argb & 0x3FF00000) >> 22) /*<< 16*/;
                    uint g = ((argb & 0xFFC00) >> 12) /*<< 8*/;
                    uint b = (argb & 0x3FF) >> 2;
                    //uint final = a | r | g | b;

                    buffer[index++] = (byte)b;
                    buffer[index++] = (byte)g;
                    buffer[index++] = (byte)r;
                    buffer[index++] = 255; //Set the alpha as 255
                }
            }

            //Now create a image from the buffer
            IntPtr ptr = new IntPtr();
            Marshal.FreeHGlobal(ptr);
            ptr = Marshal.AllocHGlobal(buffer.Length);
            RtlMoveMemory(ptr, buffer, buffer.Length);

            //Create the final image
            Bitmap final = new Bitmap(width, height, width * 4,
                                      PixelFormat.Format32bppArgb, ptr);

            //Return our done image
            return final;
        }
        private static Image DeswizzleA8R8G8B8(byte[] buffer, int width, int height)
        {
            //Loop through and convert each pixle
            int index = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    uint argb = BitConverter.ToUInt32(buffer, index);

                    uint b = buffer[index];
                    uint g = buffer[index + 1];
                    uint r = buffer[index + 2];

                    buffer[index++] = (byte)b;
                    buffer[index++] = (byte)g;
                    buffer[index++] = (byte)r;
                    buffer[index++] = (byte)255; //Set the alpha as 255
                }
            }

            //Now create a image from the buffer
            IntPtr ptr = new IntPtr();
            Marshal.FreeHGlobal(ptr);
            ptr = Marshal.AllocHGlobal(buffer.Length);
            RtlMoveMemory(ptr, buffer, buffer.Length);

            //Create the final image
            Bitmap final = new Bitmap(width, height, width * 4,
                                      PixelFormat.Format32bppArgb, ptr);

            //Return our done image
            return final;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Drawing;
using HaloDeveloper.Map;
namespace HaloDeveloper.Raw.ModelRaw
{
    public class H3Shader
    {
        int bitmapid;
        int bitmap_index;
        public Texture texture;
        public H3Shader(HaloMap map, int id, Renderer renderer)
        {
            int index = map.GetTagIndexByIdent(id);
            map.IO.SeekTo(map.IndexItems[index].Offset + 0x2c);
            int pointer = map.IO.In.ReadInt32() - map.MapHeader.mapMagic;
            map.IO.SeekTo(pointer + 0x14);
            pointer = map.IO.In.ReadInt32() - map.MapHeader.mapMagic;
            map.IO.SeekTo(pointer + 0xC);
            bitmapid = map.IO.In.ReadInt32();

            bitmap_index = map.GetTagIndexByIdent(bitmapid);
            BitmapRaw.BitmapInfo bi = BitmapRaw.BitmapFunctions.GetBitmapInfo(map, bitmap_index);
            Bitmap b = bi.GeneratePreview(0);
            if (b != null)
                texture = new Texture(renderer.Device, b, Usage.None, Pool.Managed);
        }
    }
}

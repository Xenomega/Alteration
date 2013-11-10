using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace HaloDeveloper.Raw.SoundRaw
{
    public class ugh
    {
        short[] nameIndexes;
        
        public class Permutations
        {
            //0x48
            short unk1;
            short unk2;
            byte unk3;
            byte chunk;
            short unk4;
            short optionsIndex;
            short chunkCount;
            short unk5;
        }
     
        public class Options
        {
            //0x9c
            short unk1;
            short unk2;
            int unk3;
            short unk4;
            short unk5;
            byte unk6;
            byte unk7;
            short unk8;
            short unk9;
            short unk10;
        }
    }
    public class SoundFunctions
    {
        public static void ExtractSoundNamesList(HaloDeveloper.Map.HaloMap map)
        {
            int ughIndex = 0;
            for (int x = 0; x < map.IndexItems.Count; x++) if (map.IndexItems[x].Class == "ugh!") ughIndex = x;
            if (ughIndex == -1) { System.Windows.Forms.MessageBox.Show("Error"); return; }
            int ughOffset = map.IndexItems[ughIndex].Offset;
            map.IO.Open();
            map.IO.In.BaseStream.Position = ughOffset+36;
            int count = map.IO.In.ReadInt32();
            int pointer = map.IO.In.ReadInt32()-map.MapHeader.mapMagic;
          
            map.IO.In.BaseStream.Position = pointer;
              FileStream fs=new FileStream("C:\\alteration\\stringnames.txt", FileMode.Create);

            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
            for (int x = 0; x < count; x++)
            {
                int unknown = map.IO.In.ReadInt16(); 
                int index=map.IO.In.ReadInt16();
                string t = "0x" + x.ToString("X") + "-" + map.StringTable.StringItems[index].Name;
                sw.WriteLine(t);
            }
          
       
            sw.Close();
            fs.Close();
            //tw.

        }
    }
}

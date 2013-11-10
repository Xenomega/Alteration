using System;
using System.Collections.Generic;

namespace Alteration.Helpers
{
    public abstract class BitHelper
    {
        public static List<bool> LoadValue(object value, int bitcount)
        {
            List<bool> flags = new List<bool>();
            int temp = 1;
            int tempv = Convert.ToInt32(value);
            for (int i = 0; i < bitcount; i++)
            {
                try
                {
                    bool b = (temp & tempv) == temp ? true : false;
                    flags.Add(b);
                    temp <<= 1;
                }
                catch
                {
                    break;
                }
            }
            return flags;
        }

        public static int ConvertToWriteableInteger(List<bool> flags)
        {
            int IntToWrite = 0;
            for (int z = 0; z < flags.Count; z++)
            {
                IntToWrite = setBit(IntToWrite, z, flags[z]);
            }
            return IntToWrite;
        }

        private static int setBit(int integer, int bitIndex, bool onOff)
        {
            if (onOff)
            {
                integer = (1 << bitIndex) | integer;
            }
            else
            {
                integer = ~(1 << bitIndex) & integer;
            }
            return integer;
        }
    }
}
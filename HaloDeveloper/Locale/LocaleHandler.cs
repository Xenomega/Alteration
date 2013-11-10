using System.Collections.Generic;
using HaloDeveloper.Map;
using System.Security.Cryptography;
using HaloDeveloper.Helpers;

namespace HaloDeveloper.Locale
{
    /// <summary>
    /// This class, when initialized, will load all Locale's into a list where they can easily be accessed.
    /// </summary>
    public class LocaleHandler
    {
        private HaloMap map;
        public HaloMap Map
        {
            get { return map; }
            set { map = value; }
        }
        private List<LocaleTable> localeTables;
        public List<LocaleTable> LocaleTables
        {
            get { return localeTables; }
            set { localeTables = value; }
        }
        public LocaleHandler(HaloMap map)
        {
            //Set our instance of our HaloMap class
            Map = map;
            //Open our IO
            Map.OpenIO();
            #region Get Matg Index
            //Create an integer to hold our matg Index
            int matgIndex = -1;
            //Loop through all the tags
            for (int i = 0; i < Map.IndexItems.Count; i++)
            {
                //If this tag's class is matg...
                if (Map.IndexItems[i].Class == "matg")
                {
                    //Then set the index
                    matgIndex = i;
                    //Break out of the loop.
                    break;
                }
            }
            #endregion
            #region Get Table Information
            //Initialuze our Locale Table list
            LocaleTables = new List<LocaleTable>();
            //Loop for each language...(there's 12)
            for (int i = 0; i < 12; i++)
            {
                //Create our table instance
                LocaleTable unicTable = new LocaleTable();
                //Set our language for this instance
                unicTable.Language = (LanguageType)i;
                //If we did find a matg tag.
                if (matgIndex != -1)
                {
                    //Make our IO go to the position to read our information for our table.
                    if(Map.Halo_Map_Version == HaloMap.HaloMapVersion.Halo3Retail)
                        unicTable.Offset = Map.IndexItems[matgIndex].Offset + 452 + ((int)unicTable.Language * 68);
                    else if (Map.Halo_Map_Version == HaloMap.HaloMapVersion.Halo3ODST)
                        unicTable.Offset = Map.IndexItems[matgIndex].Offset + 508 + ((int)unicTable.Language * 68);
                    map.IO.SeekTo(unicTable.Offset);
                    //Read our count
                    unicTable.LocaleCount = Map.IO.In.ReadInt32();
                    //Read our size
                    unicTable.LocaleTableSize = Map.IO.In.ReadInt32();
                    //Read our index offset
                    unicTable.LocaleTableIndexOffset = Map.IO.In.ReadInt32() + Map.MapHeader.localeTableAddressModifier;
                    //Read our locale table offset
                    unicTable.LocaleTableOffset = Map.IO.In.ReadInt32() + Map.MapHeader.localeTableAddressModifier;
                }
                //Add the language to our list...
                LocaleTables.Add(unicTable);
            }
            #endregion
            #region Load Strings
            //Loop through the localeTables
            for (int currentTableIndex = 0; currentTableIndex < LocaleTables.Count; currentTableIndex++)
            {
                //Get our LocaleTable instance
                LocaleTable currentTable = LocaleTables[currentTableIndex];
                //Go to the LocaleTableIndexOffset
                Map.IO.In.BaseStream.Position = currentTable.LocaleTableIndexOffset;
                //Initialize our LocaleStringList
                currentTable.LocaleStrings = new List<LocaleTable.LocaleString>();
                //Loop for each localeString
                for (int currentLocaleIndex = 0; currentLocaleIndex < currentTable.LocaleCount; currentLocaleIndex++)
                {
                    //Skip 4 bytes.
                    Map.IO.In.BaseStream.Position += 4;
                    //Initialize an instance of a locale string
                    LocaleTable.LocaleString currentLocaleString = new LocaleTable.LocaleString();
                    //Read it's offset
                    currentLocaleString.Offset = Map.IO.In.ReadInt32();
                    //Add it to the list of Locales
                    currentTable.LocaleStrings.Add(currentLocaleString);
                }

                //Read our strings

                //Loop for each string.
                for (int currentLocaleIndex = 0; currentLocaleIndex < currentTable.LocaleCount; currentLocaleIndex++)
                {
                    //Go to the offset to read the locale.
                    Map.IO.In.BaseStream.Position = currentTable.LocaleTableOffset + currentTable.LocaleStrings[currentLocaleIndex].Offset;
                    //If we aren't at the last string
                    if (currentLocaleIndex != currentTable.LocaleCount - 1)
                    {
                        //Calculate the length with (nextStringOffset - thisStringOffset)
                        currentTable.LocaleStrings[currentLocaleIndex].Length = currentTable.LocaleStrings[currentLocaleIndex + 1].Offset - (currentTable.LocaleStrings[currentLocaleIndex].Offset + 1);
                    }
                    //if we are at the last string
                    else
                    {
                        //Calculate the length with (endOfTable - thisStringOffset)
                        currentTable.LocaleStrings[currentLocaleIndex].Length = currentTable.LocaleTableSize - (currentTable.LocaleStrings[currentLocaleIndex].Offset + 1);
                    }
                    //Read the string according to our length.
                    currentTable.LocaleStrings[currentLocaleIndex].Name = Map.IO.In.ReadAsciiString(currentTable.LocaleStrings[currentLocaleIndex].Length);
                }
            }
            #endregion
            //Close our IO
            Map.CloseIO();
        }
        public void SaveLocale(int LocaleTableIndex, int StringIndex, string strName)
        {
            //Get our Locale Table
            LocaleHandler.LocaleTable selectedTable = LocaleTables[LocaleTableIndex];

            //Compare our string lengths.
            if (selectedTable.LocaleStrings[StringIndex].Name.Length == strName.Length)
            {
                //Open our IO
                Map.OpenIO();

                //Go to that locale's position
                Map.IO.Out.BaseStream.Position = selectedTable.LocaleTableOffset +
                                                 selectedTable.LocaleStrings[StringIndex].Offset;

                //Write our new locale.
                Map.IO.Out.WriteAsciiString(strName, strName.Length);

                //Close our IO
                Map.CloseIO();
            }
            else
            {
                //Get our variables
                int differenceStringLength = strName.Length - selectedTable.LocaleStrings[StringIndex].Name.Length;
                int oldTableSize = selectedTable.LocaleTableSize;
                int newTableSize = selectedTable.LocaleTableSize + differenceStringLength;
                int oldTableSizePadded = oldTableSize + ExtraFunctions.CalculatePadding(oldTableSize, 0x1000);
                int newTableSizePadded = newTableSize + ExtraFunctions.CalculatePadding(newTableSize, 0x1000);
                int differenceTableSize = newTableSizePadded - oldTableSizePadded;

                //Let's recalculate some variables.

                //Open our IO
                Map.OpenIO();

                //Go to our following string index's position
                Map.IO.Out.BaseStream.Position = selectedTable.LocaleTableIndexOffset + ((StringIndex + 1) * 8);

                //Loop for every string after.
                for (int i = StringIndex + 1; i < selectedTable.LocaleCount; i++)
                {
                    //Skip 4 bytes
                    Map.IO.Out.BaseStream.Position += 4;
                    //Write our new index
                    Map.IO.Out.Write(selectedTable.LocaleStrings[i].Offset + differenceStringLength);
                }

                //Let's shift our other tables

                //Loop for each table after this one.
                for (int i = LocaleTableIndex + 1; i < LocaleTables.Count; i++)
                {
                    //Go to that table's offset.
                    Map.IO.Out.BaseStream.Position = LocaleTables[i].Offset + 8;

                    //Shift our values
                    LocaleTables[i].LocaleTableOffset += differenceTableSize;
                    LocaleTables[i].LocaleTableIndexOffset += differenceTableSize;

                    //Write our values.
                    Map.IO.Out.Write((LocaleTables[i].LocaleTableIndexOffset - Map.MapHeader.localeTableAddressModifier));
                    Map.IO.Out.Write((LocaleTables[i].LocaleTableOffset - Map.MapHeader.localeTableAddressModifier));
                }

                byte[] restOfTable = new byte[0];
                try
                {
                    //Read the rest of our table after we'll insert
                    Map.IO.In.BaseStream.Position = selectedTable.LocaleTableOffset + selectedTable.LocaleStrings[StringIndex + 1].Offset;
                    restOfTable = Map.IO.In.ReadBytes(oldTableSize - selectedTable.LocaleStrings[StringIndex].Offset);
                }
                catch { }
                //Close our IO
                Map.CloseIO();

                //Check our difference.
                if (differenceTableSize > 0)
                {
                    //Insert
                    ExtraFunctions.InsertBytes(Map.FileName, selectedTable.LocaleTableOffset + oldTableSizePadded, differenceTableSize);
                }
                else if (differenceTableSize < 0)
                {
                    //Delete
                    ExtraFunctions.DeleteBytes(Map.FileName, selectedTable.LocaleTableOffset + newTableSizePadded, oldTableSizePadded - newTableSizePadded);
                }

                //Open our IO
                Map.OpenIO();

                //Let's write our new strings name.
                Map.IO.Out.BaseStream.Position = selectedTable.LocaleTableOffset + selectedTable.LocaleStrings[StringIndex].Offset;
                Map.IO.Out.WriteAsciiString(strName, strName.Length + 1);

                //Write the ending of our table
                Map.IO.Out.Write(restOfTable);

                Map.IO.Out.Write(new byte[newTableSizePadded - newTableSize]);

                //Lets go write our new locale table size
                Map.IO.Out.BaseStream.Position = selectedTable.Offset + 4;

                //Write our new size.
                Map.IO.Out.Write(newTableSize);

                //Close our IO
                Map.CloseIO();

                //Set our new table size
                selectedTable.LocaleTableSize = newTableSize;
            }



            //Open our IO
            Map.OpenIO();

            //Go back to the beginning of the table
            Map.IO.SeekTo(selectedTable.LocaleTableOffset);
            byte[] hash = SHA1.Create().ComputeHash(Map.IO.In.ReadBytes(
                selectedTable.LocaleTableSize));
            //Seek to where the hash is
            Map.IO.SeekTo(selectedTable.Offset + 0x24);
            //Write it
            Map.IO.Out.Write(hash);

            //Go back to the beginning of the index table
            Map.IO.SeekTo(selectedTable.LocaleTableIndexOffset);
            hash = SHA1.Create().ComputeHash(Map.IO.In.ReadBytes(0x08 * selectedTable.LocaleCount));
            //Seek to where the hash is
            Map.IO.SeekTo(selectedTable.Offset + 0x10);
            //Write it
            Map.IO.Out.Write(hash);
            //Close our IO
            Map.CloseIO();
        }
        public class LocaleTable
        {
            public int Offset;
            private LanguageType language;
            public LanguageType Language
            {
                get { return language; }
                set { language = value; }
            }
            private int localeTableOffset;
            public int LocaleTableOffset
            {
                get { return localeTableOffset; }
                set { localeTableOffset = value; }
            }
            private int localetableSize;
            public int LocaleTableSize
            {
                get { return localetableSize; }
                set { localetableSize = value; }
            }
            private int localeCount;
            public int LocaleCount
            {
                get { return localeCount; }
                set { localeCount = value; }
            }
            private int localeTableIndexOffset;
            public int LocaleTableIndexOffset
            {
                get { return localeTableIndexOffset; }
                set { localeTableIndexOffset = value; }
            }
            private List<LocaleString> localeStrings;
            public List<LocaleString> LocaleStrings
            {
                get { return localeStrings; }
                set { localeStrings = value; }
            }
            public class LocaleString
            {
                private string name;
                public string Name
                {
                    get { return name; }
                    set { name = value; }
                }
                private int offset;
                public int Offset
                {
                    get { return offset; }
                    set { offset = value; }
                }
                private int length;
                public int Length
                {
                    get { return length; }
                    set { length = value; }
                }
            }
        }
        public enum LanguageType
        {
            English = 0,
            Japanese = 1,
            German = 2,
            French = 3,
            Spanish = 4,
            LatinAmericanSpanish = 5,
            Italian = 6,
            Korean = 7,
            Chinese = 8,
            Unknown0 = 9,
            Portuguese = 10,
            Unknown1 = 11
        }
    }
}
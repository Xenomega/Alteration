using System;
using System.Collections.Generic;
using System.Text;
using HaloDeveloper.Map;
using HaloDeveloper.Helpers;

namespace Alteration.Halo_3.Map_File.Building
{
    public class MapBuilder
    {
        #region Values
        private HaloMap map;
        /// <summary>
        /// This is our map instance we'll be editting.
        /// </summary>
        public HaloMap Map
        {
            get { return map; }
            set { map = value; }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// This form of initializing sets our map instance.
        /// </summary>
        /// <param name="map">Our map instance to set.</param>
        public MapBuilder(HaloMap Map)
        {
            //Set our map instance
            map = Map;
        }
        #endregion

        #region Functions

        #region Raw Data

        public void InternalizeRawChunk(int rawPoolIndex)
        {
            //Get the raw chunk data
            byte[] rawChunk = Map.RawInformation.GetCompressedRawDataFromPoolIndex(rawPoolIndex);

            //Calculate the size to insert
            int paddingSize = ExtraFunctions.CalculatePadding((int)rawChunk.Length, 0x1000);

            //Block size to insert into the raw table
            int blockSize = paddingSize + (int)rawChunk.Length;

            //Open our IO
            Map.OpenIO();

            //Go to our external map index offset.
            Map.IO.Out.BaseStream.Position = Map.RawInformation.RawPools[rawPoolIndex].ChunkOffset + 4;

            //Write our value
            Map.IO.Out.Write((short)-1);

            //Skip 2 bytes
            Map.IO.Out.BaseStream.Position += 2;

            //Write our new raw data chunk offset.
            Map.IO.Out.Write((uint)Map.MapHeader.RawTableSize);

            //Close our IO
            Map.CloseIO();

            //Create our array for our bytes to insert
            byte[] dataToInsert = new byte[blockSize];
            for (int i = 0; i < dataToInsert.Length; i++)
                dataToInsert[i] = 0xFF;
            Array.Copy(rawChunk, dataToInsert, (int)rawChunk.Length);

            //Time to start inserting the block..
            ExtraFunctions.InsertBytes(Map.FileName, Map.MapHeader.RawTableOffset + Map.MapHeader.RawTableSize, dataToInsert);



            Map.OpenIO();
            Map.IO.Out.BaseStream.Position = 1160;
            Map.IO.Out.Write((map.MapHeader.RawTableSize + blockSize));
            Map.IO.Out.BaseStream.Position = 1144;
            Map.IO.Out.Write((map.MapHeader.localeTableAddressModifier + blockSize));
            Map.CloseIO();
        }

        #endregion

        #region Tag Names

        /// <summary>
        /// This function renames a tag.
        /// </summary>
        /// <param name="tagIndex">The index of the tag to rename.</param>
        /// <param name="newName">The new name of the tag to rename as.</param>
        public void RenameTag(int tagIndex, string newName)
        {
            //Get our tag instance
            HaloMap.TagItem tagItem = map.IndexItems[tagIndex];

            //Compare our string lengths..

            if (tagItem.Name.Length == newName.Length)
            {
                //They are the same length. Let's write our new string.

                //Open our IO
                map.OpenIO();

                //Go to our tag_name_index_entry
                map.IO.In.BaseStream.Position = map.MapHeader.fileTableIndexOffset + (tagIndex * 4);

                //Read our tagnameoffset
                int tagNameOffset = map.IO.In.ReadInt32() + map.MapHeader.fileTableOffset;

                //Go to this position
                map.IO.In.BaseStream.Position = tagNameOffset;

                //Write our tagname
                map.IO.Out.WriteAsciiString(newName, tagItem.Name.Length);

                //Close our IO
                map.CloseIO();

                //Set our tagname.
                map.IndexItems[tagIndex].Name = newName;
            }
            else
            {
                //Calculate our table difference.
                int differenceInStringLength = (tagItem.Name.Length - newName.Length);
                int newTableSizeUnpadded = map.MapHeader.fileTableSize - differenceInStringLength;
                int newTableSize = newTableSizeUnpadded + ExtraFunctions.CalculatePadding(newTableSizeUnpadded, 0x1000);
                int oldTableSize = map.MapHeader.fileTableSize + ExtraFunctions.CalculatePadding(map.MapHeader.fileTableSize, 0x1000);
                int differenceInSize = oldTableSize - newTableSize;

                //Open our IO
                map.OpenIO();

                //Go to our tag_name_index_entry
                map.IO.In.BaseStream.Position = map.MapHeader.fileTableIndexOffset + (tagIndex * 4);

                //Read our tagnameoffset
                int tagNameOffset = map.IO.In.ReadInt32() + map.MapHeader.fileTableOffset;

                //Loop for each tag_name_index after it.
                for (int i = tagIndex + 1; i < map.IndexHeader.tagCount; i++)
                {
                    //Go to our tag_name_index_entry
                    map.IO.In.BaseStream.Position = map.MapHeader.fileTableIndexOffset + (i * 4);

                    //Read our tagindex
                    int tagOff = map.IO.In.ReadInt32();
                    //Recalculate it.
                    tagOff -= differenceInStringLength;

                    //Go to our tag_name_index_entry
                    map.IO.Out.BaseStream.Position = map.MapHeader.fileTableIndexOffset + (i * 4);

                    //Write it
                    map.IO.Out.Write(tagOff);
                }

                //Close our IO
                map.CloseIO();

                //If it's any different.
                if (differenceInSize > 0)
                    ExtraFunctions.DeleteBytes(map.FileName, map.MapHeader.fileTableOffset + newTableSize, oldTableSize - newTableSize);
                else
                    ExtraFunctions.InsertBytes(map.FileName, map.MapHeader.fileTableOffset + oldTableSize, newTableSize - oldTableSize);

                //Open our IO
                map.OpenIO();

                //Go to this position
                map.IO.Out.BaseStream.Position = tagNameOffset;

                //Write our tagname
                map.IO.Out.WriteAsciiString(newName, newName.Length);
                map.IO.Out.Write((byte)0);

                for (int i = tagIndex + 1; i < map.IndexHeader.tagCount; i++)
                {
                    //Write the tagname
                    map.IO.Out.WriteAsciiString(map.IndexItems[i].Name, map.IndexItems[i].Name.Length);
                    map.IO.Out.Write((byte)0);
                }

                //For the rest of our table, write the padding.
                byte[] padding = new byte[newTableSize - newTableSizeUnpadded];

                //Write our padding
                map.IO.Out.Write(padding);

                //Recalculate some header values.
                map.IO.Out.BaseStream.Position = 20;
                map.IO.Out.Write(((map.MapHeader.virtSegmentStart - differenceInSize) + map.MapHeader.headerMagic));
                map.IO.Out.BaseStream.Position = 700;
                map.IO.Out.Write(newTableSizeUnpadded);
                map.IO.Out.BaseStream.Position = 704;
                map.IO.Out.Write(((map.MapHeader.fileTableIndexOffset - differenceInSize) + map.MapHeader.headerMagic));
                map.IO.Out.BaseStream.Position = 1136;
                map.IO.Out.Write((map.MapHeader.RawTableOffset - differenceInSize));
                map.IO.Out.BaseStream.Position = 1144;
                map.IO.Out.Write((map.MapHeader.localeTableAddressModifier - differenceInSize));

                //Close our IO
                map.CloseIO();

                //Reload our map
                map.Reload();
            }
        }
        /// <summary>
        /// This function protects a map file from being editted or ripped by another user.
        /// </summary>
        public void ProtectMap()
        {
            //Open our IO
            map.OpenIO();

            //Go to our filename index table
            map.IO.Out.BaseStream.Position = map.MapHeader.fileTableIndexOffset;

            //Loop for each filename
            for (int i = 0; i < map.MapHeader.fileTableCount; i++)
            {
                //Write our offset of our string(blank string)
                map.IO.Out.Write(i);
            }

            //Go to the filename table.
            map.IO.Out.BaseStream.Position = map.MapHeader.fileTableOffset;

            //Create our blank array of bytes
            byte[] blankData = new byte[map.MapHeader.fileTableSize];

            //Write it.
            map.IO.Out.Write(blankData);

            //Close our IO
            map.CloseIO();
        }

        #endregion

        #endregion
    }
}

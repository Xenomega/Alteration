using System.Collections.Generic;
using HaloDeveloper.Map;
using System.IO;
using System.IO.Compression;
using HaloDeveloper.IO;
using System;

namespace HaloDeveloper.Raw
{
    /// <summary>
    /// This class will supply information on Raw entries for tags, and etc. when utilized.
    /// </summary>
    public class RawInformation
    {
        #region Values

        private HaloMap map;
        public HaloMap Map
        {
            get { return map; }
            set { map = value; }
        }
        private RawEntryTable rawEntries;
        public RawEntryTable RawEntries
        {
            get { return rawEntries; }
            set { rawEntries = value; }
        }
        private ExternalMapsList externalMaps;
        public ExternalMapsList ExternalMaps
        {
            get { return externalMaps; }
            set { externalMaps = value; }
        }
        private RawPoolsList rawPools;
        public RawPoolsList RawPools
        {
            get { return rawPools; }
            set { rawPools = value; }
        }
        private RawLocationsList rawLocations;
        public RawLocationsList RawLocations
        {
            get { return rawLocations; }
            set { rawLocations = value; }
        }
        public Unknown3s unknowns;

        #endregion

        #region Initialization

        /// <summary>
        /// This form of initializing the RawInformation class will load all raw information in the class, from the given map.
        /// </summary>
        /// <param name="map">The map to load information from.</param>
        public RawInformation(HaloMap Map)
        {
            //Set our instance of the HaloMap class
            map = Map;

            //Initialize our rawBlock List
            rawEntries = new RawEntryTable(Map);

            //Get our bsp.
            RawEntry bsp = rawEntries[rawEntries.Count - 1];

            //Load our external maps list
            externalMaps = new ExternalMapsList(Map);

            //Load the raw pools
            rawPools = new RawPoolsList(Map);

            //Load the raw locations
            rawLocations = new RawLocationsList(Map);

            //Load our unknowns
            unknowns = new Unknown3s(Map);
        }

        #endregion

        #region Functions

        public int GetRawEntryIndexForTag(int ID)
        {
            for (int x = 0; x < rawEntries.Count; x++)
                if (rawEntries[x].TagID == ID)
                    return x;

            return -1;
        }
        public RawLocation GetRawLocationFromRawID(int RawID)
        {
            //Our raw entry we will grab.
            RawEntry entry = GetRawEntryFromRawID(RawID);

            //If our founded entry isn't null
            if (entry != null)
                if (entry.RawLocationsIndex <= rawLocations.Count)
                    return rawLocations[entry.RawLocationsIndex];

            return null;
        }
        public RawEntry GetRawEntryFromRawID(int RawID)
        {
            // Check to see if masking will get us the index faster
            if (rawEntries[(RawID & 0xFFFF)].RawID == RawID)
                return rawEntries[(RawID & 0xFFFF)];

            return null;
        }
        public RawLocation GetRawLocationForTag(int ID)
        {
            // Our raw entry we will grab.
            RawEntry entry = null;

            // Loop through each RawEntry
            for (int x = 0; x < rawEntries.Count; x++)
                if (rawEntries[x].TagID == ID)
                {
                    entry = rawEntries[x];
                    break;
                }

            // If our founded entry isn't null
            if (entry != null)
                if (entry.RawLocationsIndex <= rawLocations.Count)
                    return rawLocations[entry.RawLocationsIndex];

            // Dang..
            return null;
        }
        public RawLocation GetRawLocationForTag(string tagclass, string tagname)
        {
            //Get our ID for our tag
            int ID = Map.GetTagIndexByClassAndName(tagclass, tagname);

            //If our ID isn't -1
            if (ID != -1)
                return GetRawLocationForTag(ID);

            return null;
        }

        public byte[] GetRawDataFromID(int RawID)
        {
            return GetRawDataFromID(RawID, -1);
        }

        public byte[] GetRawDataFromID(int RawID, int DataLength)
        {
            map.OpenIO();

            // Get our raw info
            RawInformation.RawEntry entry = GetRawEntryFromRawID(RawID);
            if (entry == null) return null;
            RawInformation.RawLocation rl = rawLocations[entry.RawLocationsIndex];
            int index = (rl.RawPoolIndexSingle == -1) ? rl.RawPoolIndexMultiple : rl.RawPoolIndexSingle;
            int offsetInPool = (rl.RawPoolIndexSingle == -1) ? (int)rl.OffsetOfRawInPool : 0;
            int rawOffset = (int)rawPools[index].RawOffset;
            int compressedLen = (int)rawPools[index].CompressedSize;
            int decompressLen = (int)rawPools[index].DecompressedSize;

            // Load our IO
            EndianIO io = (RawPools[index].ExternalMapIndex == -1) ? map.IO :
                externalMaps[RawPools[index].ExternalMapIndex].io;

            // Open our IO and add our raw table offset
            io.In.SeekTo(0x470);
            int offset = rawOffset + io.In.ReadInt32();

            // Setup our output stream and deflater
            DeflateStream deflate = new DeflateStream(io.In.BaseStream,
                CompressionMode.Decompress);
            BinaryReader br = new BinaryReader(deflate);

            // Goto our data and decompress it
            io.SeekTo(offset);
            byte[] buffer = br.ReadBytes(decompressLen);
            byte[] dataNew = (DataLength == -1) ? new byte[decompressLen - offsetInPool] :
                new byte[DataLength];
            int size = 0;
            if (DataLength != -1)
                size = (offsetInPool + DataLength > buffer.Length) ? buffer.Length - offsetInPool : DataLength;
            else
                size = buffer.Length - offsetInPool;
            Array.Copy(
                buffer, offsetInPool,
                dataNew, 0, size);

            map.CloseIO();
            // Now lets return our data
            return dataNew;
        }

        public byte[] GetCompressedRawDataFromPoolIndex(int raw_pool_index)
        {
            externalMaps.OpenIOs();
            int index = raw_pool_index;
            int poolOffset = (int)rawPools[index].RawOffset;
            int size = (int)rawPools[index].CompressedSize;

            // Load our IO
            EndianIO io = (RawPools[index].ExternalMapIndex == -1) ? map.IO :
                externalMaps[RawPools[index].ExternalMapIndex].io;

            // Open our IO and add our raw table offset
            io.In.SeekTo(0x470);
            int offset = poolOffset + io.In.ReadInt32();

            //Go to our offset
            io.In.BaseStream.Position = offset;
            //Read our raw data.
            byte[] rawData = io.In.ReadBytes(size);

            externalMaps.CloseIOs();

            // Now lets return our data
            return rawData;
        }

        #endregion

        #region Classes

        public class RawEntry
        {
            //General information
            private int index;
            public int Index
            {
                get { return index; }
                set { index = value; }
            }
            //Tag information
            private string tagClass;
            public string TagClass
            {
                get { return tagClass; }
                set { tagClass = value; }
            }
            private string tagName;
            public string TagName
            {
                get { return tagName; }
                set { tagName = value; }
            }
            private int tagId;
            public int TagID
            {
                get { return tagId; }
                set { tagId = value; }
            }

            //Raw information
            private int rawId;
            public int RawID
            {
                get { return rawId; }
                set { rawId = value; }
            }

            private int zoneChunkOffset;
            /// <summary>
            /// Set our Zone chunk offset.
            /// </summary>
            public int ZoneChunkOffset
            {
                get { return zoneChunkOffset; }
                set { zoneChunkOffset = value; }
            }

            private uint offset;
            /// <summary>
            /// The offset of... something..
            /// </summary>
            public uint Offset
            {
                get { return offset; }
                set { offset = value; }
            }

            private short rawLocationsIndex;
            /// <summary>
            /// The index of the chunk of our raw pool from which we extract.
            /// </summary>
            public short RawLocationsIndex
            {
                get { return rawLocationsIndex; }
                set { rawLocationsIndex = value; }
            }

            private uint size;
            /// <summary>
            /// The size of... something..
            /// </summary>
            public uint Size
            {
                get { return size; }
                set { size = value; }
            }

            private int count0;
            public int Count0
            {
                get { return count0; }
                set { count0 = value; }
            }
            private int pointer0;
            public int Pointer0
            {
                get { return pointer0; }
                set { pointer0 = value; }
            }
            private int count1;
            public int Count1
            {
                get { return count1; }
                set { count1 = value; }
            }
            private int pointer1;
            public int Pointer1
            {
                get { return pointer1; }
                set { pointer1 = value; }
            }

            public override string ToString()
            {
                return tagName + "." + tagClass;
            }
        }
        /// <summary>
        /// This table located on the [zone] tag, lists a tag id for the tag that uses a given raw. The given raw's Raw Locations chunk index is given from there.
        /// </summary>
        public class RawEntryTable : List<RawEntry>
        {
            public RawEntryTable(HaloMap Map)
            {
                //Create our variable to hold the zone index
                int zoneIndex = -1;
                //Loop through all the tags
                for (int i = 0; i < Map.IndexItems.Count; i++)
                {
                    //If the class is zone
                    if (Map.IndexItems[i].Class == "zone")
                    {
                        //Set the zone index as this index
                        zoneIndex = i;
                        //Break out of the loop
                        break;
                    }
                }

                //If we didn't find a zone tag
                if (zoneIndex == -1)
                    return;

                //Get our zone Offset
                int zoneOffset = Map.IndexItems[zoneIndex].Offset;

                //Go to the structure offset to read our count and pointer
                Map.IO.In.BaseStream.Position = zoneOffset + 88;
                int rawTableCount = Map.IO.In.ReadInt32();
                int rawTablePointer = Map.IO.In.ReadInt32() - Map.MapHeader.mapMagic;

                //Loop through every RawBlock
                for (int i = 0; i < rawTableCount; i++)
                {
                    //Go to our rawBlock offset                  //ChunkSize=0x40
                    Map.IO.In.BaseStream.Position = rawTablePointer + (i * 0x40);
                    //Initialize a new RawBlock instance
                    RawEntry rawBlock = new RawEntry();
                    //Set our chunk offset
                    rawBlock.ZoneChunkOffset = (int)Map.IO.In.BaseStream.Position;
                    //Set our index
                    rawBlock.Index = i;
                    //Read our tagref
                    rawBlock.TagClass = Map.IO.In.ReadAsciiString(4);
                    //Skip the 8 byte gap.
                    Map.IO.In.BaseStream.Position += 8;
                    //Read our tagident
                    rawBlock.TagID = Map.IO.In.ReadInt32();

                    //Get our tag information from ident, so if its not null
                    if (rawBlock.TagID != -1)
                        rawBlock.TagName = Map.IndexItems[Map.GetTagIndexByIdent(rawBlock.TagID)].Name;
                    else
                    {
                        rawBlock.TagName = "null";
                        rawBlock.TagClass = "null";
                    }

                    //Read our raw identifier
                    rawBlock.RawID = (Map.IO.In.ReadInt16() << 16) + i;

                    //Go to our rawBlock offset                  //ChunkSize=0x40
                    Map.IO.In.BaseStream.Position = rawTablePointer + (i * 0x40) + 20;

                    //Read our offset
                    rawBlock.Offset = Map.IO.In.ReadUInt32();
                    rawBlock.Size = Map.IO.In.ReadUInt32();

                    //Skip the 6 byte gap.
                    Map.IO.In.BaseStream.Position += 6;

                    //Read our index
                    rawBlock.RawLocationsIndex = Map.IO.In.ReadInt16();

                    //Jump 0x04 bytes forward to our Count0 offset
                    Map.IO.In.BaseStream.Position += 0x04;
                    rawBlock.Count0 = Map.IO.In.ReadInt32();
                    if (rawBlock.Count0 != 0)
                        rawBlock.Pointer0 = Map.IO.In.ReadInt32() - Map.MapHeader.mapMagic;

                    //Skip the 4 bytes of blank space after a reflexive.
                    Map.IO.In.BaseStream.Position += 4;
                    rawBlock.Count1 = Map.IO.In.ReadInt32();
                    if (rawBlock.Count1 != 0)
                        rawBlock.Pointer1 = Map.IO.In.ReadInt32() - Map.MapHeader.mapMagic;

                    //Add it to our rawblock list
                    this.Add(rawBlock);
                }
            }
        }

        /// <summary>
        /// This class represents a chunk of the structure at offset 12 in the [play] tag.
        /// </summary>
        public class ExternalMap
        {
            private string mapReference;
            /// <summary>
            /// This string indicates an external map container for raw data.
            /// </summary>
            public string MapReference
            {
                get { return mapReference; }
                set { mapReference = value; }
            }

            public string MapName
            {
                get
                {
                    string[] split = MapReference.Split('\\');
                    return split[split.Length - 1];
                }
            }
            public EndianIO io;
        }
        /// <summary>
        /// This class representsthe structure at offset 12 in the [play] tag.
        /// </summary>
        public class ExternalMapsList : List<ExternalMap>
        {
            HaloMap map;

            /// <summary>
            /// This form of initialization loads the external maps.
            /// </summary>
            /// <param name="Map">The map instance to read external map paths from.</param>
            public ExternalMapsList(HaloMap Map)
            {
                map = Map;

                //Create our variable to hold the play index
                int playIndex = -1;
                for (int i = 0; i < Map.IndexItems.Count; i++)
                    if (Map.IndexItems[i].Class == "play")
                    {
                        playIndex = i;
                        break;
                    }

                //If we found the play tag..
                if (playIndex != -1)
                {
                    //Obtain the tag instance of the play tag
                    HaloMap.TagItem play_tag = Map.IndexItems[playIndex];

                    //Go to the tag offset + 12
                    Map.IO.In.BaseStream.Position = play_tag.Offset + 12;
                    int externCount = Map.IO.In.ReadInt32();
                    int externPointer = Map.IO.In.ReadInt32() - Map.MapHeader.mapMagic;

                    //Loop for each chunk
                    for (int i = 0; i < externCount; i++)
                    {
                        //Initialize our external chunk
                        ExternalMap externMap = new ExternalMap();

                        //Go to the map string reference location
                        Map.IO.In.BaseStream.Position = externPointer + (i * 264);
                        externMap.MapReference = Map.IO.In.ReadAsciiString(32);

                        //Add it to our list
                        this.Add(externMap);
                    }
                }
            }

            public void CreateIOs()
            {
                for (int x = 0; x < this.Count; x++)
                    if (map.MapDirectory + this[x].MapName != map.FileName)
                        this[x].io = new EndianIO(map.MapDirectory + this[x].MapName,
                        EndianType.BigEndian);
                    else
                        this[x].io = map.IO;
            }

            public void OpenIOs()
            {
                for (int x = 0; x < this.Count; x++)
                    this[x].io.Open();
            }

            public void CloseIOs()
            {
                for (int x = 0; x < this.Count; x++)
                    this[x].io.Close();
            }
        }

        public struct listEntry
        {
            public int offsetIntoRaw;
            public int size;
            public RawEntry entry;

            public override string ToString()
            {
                return entry.ToString();
            }
        }

        /// <summary>
        /// This class represents a chunk of the Raw Pool structure located at offset 24 in the [play] tag.
        /// </summary>
        public class RawPool
        {
            public int ChunkOffset;
            private short externalMapIndex;
            /// <summary>
            /// [Offset: 4] This value is an index of the ExternalMaps class to indicate where the raw data is located. -1 for internal.
            /// </summary>
            public short ExternalMapIndex
            {
                get { return externalMapIndex; }
                set { externalMapIndex = value; }
            }

            private uint rawOffset;
            /// <summary>
            /// [Offset: 8] This value indicates the offset of the raw pool relative to the Raw Table Offset.
            /// </summary>
            public uint RawOffset
            {
                get { return rawOffset; }
                set { rawOffset = value; }
            }

            private uint compressedSize;
            /// <summary>
            /// [Offset: 12] This value indicates the size of the raw while it is compressed. This size value includes padding, which is padded by an interval of 0x1000.
            /// </summary>
            public uint CompressedSize
            {
                get { return compressedSize; }
                set { compressedSize = value; }
            }

            private uint decompressedSize;
            /// <summary>
            /// [Offset: 16] This value indicates the size of the raw while it is compressed. This size value includes padding, which is padded by an interval of 0x1000.
            /// </summary>
            public uint DecompressedSize
            {
                get { return decompressedSize; }
                set { decompressedSize = value; }
            }

            private short rawChunkCount;
            /// <summary>
            /// [Offset: 84] This value indicates how many raw chunks are within this pool.
            /// </summary>
            public short RawChunkCount
            {
                get { return rawChunkCount; }
                set { rawChunkCount = value; }
            }

            public List<listEntry> poolFor = new List<listEntry>();
        }
        /// <summary>
        /// This class represents the Raw Pool structure located at offset 24 in the [play] tag.
        /// </summary>
        public class RawPoolsList : List<RawPool>
        {
            /// <summary>
            /// This form of initializing loads the RawPools.
            /// </summary>
            /// <param name="Map">The map to load the raw pools for</param>
            public RawPoolsList(HaloMap Map)
            {
                //Create our variable to hold the play index
                int playIndex = -1;
                for (int i = 0; i < Map.IndexItems.Count; i++)
                    if (Map.IndexItems[i].Class == "play")
                    {
                        playIndex = i;
                        break;
                    }

                //If we found the play tag..
                if (playIndex != -1)
                {
                    //Obtain the tag instance of the play tag
                    HaloMap.TagItem play_tag = Map.IndexItems[playIndex];

                    //Go to the tag offset + 24
                    Map.IO.In.BaseStream.Position = play_tag.Offset + 24;
                    int poolCount = Map.IO.In.ReadInt32();
                    int poolPointer = Map.IO.In.ReadInt32() - Map.MapHeader.mapMagic;

                    //Loop for each chunk
                    for (int i = 0; i < poolCount; i++)
                    {
                        //Initialize our chunk
                        RawPool rawPool = new RawPool();

                        //Set our offset of our raw pool.
                        rawPool.ChunkOffset = poolPointer + (i * 88);

                        //Go to the chunk location.
                        Map.IO.In.BaseStream.Position = poolPointer + (i * 88);
                        Map.IO.In.BaseStream.Position += 4;

                        //Read our external map index.
                        rawPool.ExternalMapIndex = Map.IO.In.ReadInt16();

                        //Go to our raw offset value's offset
                        Map.IO.In.BaseStream.Position += 2;
                        rawPool.RawOffset = Map.IO.In.ReadUInt32();
                        rawPool.CompressedSize = Map.IO.In.ReadUInt32();
                        rawPool.DecompressedSize = Map.IO.In.ReadUInt32();

                        //Go to our raw chunk count offset
                        Map.IO.In.BaseStream.Position = poolPointer + (i * 88) + 84;
                        rawPool.RawChunkCount = Map.IO.In.ReadInt16();

                        //Add it to our list
                        this.Add(rawPool);
                    }
                }
            }
        }

        /// <summary>
        /// This class represents a chunk of the structure located at offset 48 in the [play] tag.
        /// </summary>
        public class RawLocation
        {
            private short rawPoolIndexMultiple;
            /// <summary>
            /// Our index in the Raw pool. This reference is only used when single is -1, and this usually points to a pool with multiple references.
            /// </summary>
            public short RawPoolIndexMultiple
            {
                get { return rawPoolIndexMultiple; }
                set { rawPoolIndexMultiple = value; }
            }

            private short rawPoolIndexSingle;
            /// <summary>
            /// Our index in the Raw pool. This reference may be -1, and this usually points to a pool that is only referenced by this chunk.
            /// </summary>
            public short RawPoolIndexSingle
            {
                get { return rawPoolIndexSingle; }
                set { rawPoolIndexSingle = value; }
            }

            private uint offsetOfRawInPool;
            /// <summary>
            /// The offset of the raw chunk in the pool.
            /// </summary>
            public uint OffsetOfRawInPool
            {
                get { return offsetOfRawInPool; }
                set { offsetOfRawInPool = value; }
            }
        }
        /// <summary>
        /// This class represents all the chunks of the structure located at offset 48 in the [play] tag.
        /// </summary>
        public class RawLocationsList : List<RawLocation>
        {
            /// <summary>
            /// This form of initializing loads the raw locations.
            /// </summary>
            /// <param name="Map">The map file to load from.</param>
            public RawLocationsList(HaloMap Map)
            {
                //Create our variable to hold the play index
                int playIndex = -1;
                for (int i = 0; i < Map.IndexItems.Count; i++)
                    if (Map.IndexItems[i].Class == "play")
                    {
                        playIndex = i;
                        break;
                    }

                //If we found the play tag..
                if (playIndex != -1)
                {
                    //Obtain the tag instance of the play tag
                    HaloMap.TagItem play_tag = Map.IndexItems[playIndex];

                    //Go to the tag offset + 48
                    Map.IO.In.BaseStream.Position = play_tag.Offset + 48;
                    int locationsCount = Map.IO.In.ReadInt32();
                    int locationsPointer = Map.IO.In.ReadInt32() - Map.MapHeader.mapMagic;

                    //Loop for each chunk
                    for (int i = 0; i < locationsCount; i++)
                    {
                        //Initialize our chunk
                        RawLocation rawLoc = new RawLocation();

                        //Go to the chunk location.
                        Map.IO.In.BaseStream.Position = locationsPointer + (i * 16);

                        //Read our raw pool indexes
                        rawLoc.RawPoolIndexMultiple = Map.IO.In.ReadInt16();
                        rawLoc.RawPoolIndexSingle = Map.IO.In.ReadInt16();
                        rawLoc.OffsetOfRawInPool = Map.IO.In.ReadUInt32();

                        //Add it to our list
                        this.Add(rawLoc);
                    }
                }
            }
        }

        public class Unknown3
        {
            public int unknownInt;
        }
        public class Unknown3s : List<Unknown3>
        {
            public Unknown3s(HaloMap Map)
            {
                //Create our variable to hold the play index
                int playIndex = -1;
                for (int i = 0; i < Map.IndexItems.Count; i++)
                    if (Map.IndexItems[i].Class == "play")
                    {
                        playIndex = i;
                        break;
                    }

                //If we found the play tag..
                if (playIndex != -1)
                {
                    //Obtain the tag instance of the play tag
                    HaloMap.TagItem play_tag = Map.IndexItems[playIndex];

                    //Go to the tag offset + 48
                    Map.IO.In.BaseStream.Position = play_tag.Offset + 36;
                    int locationsCount = Map.IO.In.ReadInt32();
                    int locationsPointer = Map.IO.In.ReadInt32() - Map.MapHeader.mapMagic;

                    //Loop for each chunk
                    for (int i = 0; i < locationsCount; i++)
                    {
                        //Initialize our chunk
                        Unknown3 unkn = new Unknown3();

                        //Go to the chunk location.
                        Map.IO.In.BaseStream.Position = locationsPointer + (i * 16);
                        unkn.unknownInt = Map.IO.In.ReadInt32();

                        //Add it to our list
                        this.Add(unkn);
                    }
                }
            }
        }

        #endregion
    }
}
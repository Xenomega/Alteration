using System;
using System.Collections.Generic;
using System.Text;
using HaloDeveloper.Map;
using HaloDeveloper.Locale;
using HaloDeveloper.Helpers;
using System.Windows.Forms;
using System.Xml;
using Alteration.Halo_3.Values;

namespace Alteration.Halo_3.Plugins
{
    /// <summary>
    /// This class will create plugin layouts from a .map.
    /// </summary>
    public class PluginLayoutCreator
    {
        private HaloMap _map;
        /// <summary>
        /// Our instance of the HaloMap class.
        /// </summary>
        public HaloMap Map
        {
            get { return _map; }
            set { _map = value; }
        }

        private List<PluginDataBlock> _plugindatablocks;
        /// <summary>
        /// Our plugin data blocks to scan data for.
        /// </summary>
        public List<PluginDataBlock> Plugin_Data_Blocks
        {
            get { return _plugindatablocks; }
            set { _plugindatablocks = value; }
        }

        private List<MetaHeader_DataBlock> _metaheaderdatablocks;
        /// <summary>
        /// Our metaheader data blocks to scan data for.
        /// </summary>
        public List<MetaHeader_DataBlock> MetaHeader_Data_Blocks
        {
            get { return _metaheaderdatablocks; }
            set { _metaheaderdatablocks = value; }
        }

        private List<PluginScanningRegion> _pluginscanningregions;
        /// <summary>
        /// Our regions to scan through.
        /// </summary>
        public List<PluginScanningRegion> Plugin_Scanning_Regions
        {
            get { return _pluginscanningregions; }
            set { _pluginscanningregions = value; }
        }

        private List<Tag_Ident> _tagidents;
        /// <summary>
        /// A list of tag idents in this chunk.
        /// </summary>
        public List<Tag_Ident> Tag_Idents
        {
            get { return _tagidents; }
            set { _tagidents = value; }
        }

        /// <summary>
        /// This form of initializing sets our map instance.
        /// </summary>
        /// <param name="map">The map instance to set.</param>
        public PluginLayoutCreator(HaloMap map)
        {
            //Set our map instance
            Map = map;

            //Let's initialize our lists.
            Plugin_Scanning_Regions = new List<PluginScanningRegion>();
            Plugin_Data_Blocks = new List<PluginDataBlock>();
            MetaHeader_Data_Blocks = new List<MetaHeader_DataBlock>();
            Tag_Idents = new List<Tag_Ident>();
        }

        /// <summary>
        /// This void will generate plugins to a given path.
        /// </summary>
        public void GeneratePlugins(string outputFolder,bool remapMetaHeaders)
        {
            //If we are to remap the meta headers
            if(remapMetaHeaders)
                //Map our structures out.
                GetMetaHeaderDataBlocksMapped(true,true);

            //Initialize our temporary done list
            List<string> doneTagClasses = new List<string>();
            //Loop through every tag
            for (int i = 0; i < MetaHeader_Data_Blocks.Count; i++)
            {
                //If we don't got this tagclass.
                if (!doneTagClasses.Contains(MetaHeader_Data_Blocks[i].Tag_Class))
                {
                    //Add it to our list
                    doneTagClasses.Add(MetaHeader_Data_Blocks[i].Tag_Class);
                }
            }

            //Loop for each tagclass in our tag hierarchy
            foreach (string T_C in doneTagClasses)
            {
                //Initialize our new XmlParser
                XmlParser xmlP = new XmlParser();

                //Let's create our revision
                mRevision mRev = new mRevision();
                mRev.Author = "-DeToX-";
                mRev.Version = 0.1f;
                mRev.Description = "Mapped plugin structure a new.";

                //Add it
                xmlP.Revisions.Add(mRev);

                //Set our author.
                xmlP.Author = "-DeToX-";
                xmlP.Version = 0.1f;
                xmlP.TagClass = T_C;

                //Create our MetaHeader_DataBlock list
                List<PluginDataBlock> PDBList = new List<PluginDataBlock>();

                //Loop for each tag of this class
                foreach (MetaHeader_DataBlock db in MetaHeader_Data_Blocks)
                    //If it's the class we're looking for
                    if (db.Tag_Class == T_C)
                    {
                        //Add it to our list
                        PDBList.Add(db);
                    }

                //Set our header size
                xmlP.HeaderSize = FindMostLikelySizeForChunk(PDBList);

                //Map our plugin with our PDBList
                MapPluginWithPDBList(xmlP.ValueList, PDBList, xmlP.HeaderSize, 0);

                //Write our plugin
                xmlP.WritePlugin(outputFolder + "\\" + T_C.Replace('<', '_').Replace('>', '_').Replace(" ","") + ".alt");
            }
        }

        public List<MetaHeader_DataBlock> GetMetaHeaderDataBlocksMapped(bool FindIdents,bool FindChildren)
        {
            //Let's initialize our lists.
            Plugin_Scanning_Regions = new List<PluginScanningRegion>();
            Plugin_Data_Blocks = new List<PluginDataBlock>();
            MetaHeader_Data_Blocks = new List<MetaHeader_DataBlock>();
            Tag_Idents = new List<Tag_Ident>();

            //Let's make our first region
            PluginScanningRegion pluginScanRegion = new PluginScanningRegion();
            pluginScanRegion.Offset = Map.MapHeader.RawTableOffset + Map.MapHeader.RawTableSize;
            pluginScanRegion.Size = Map.IndexHeader.tagInfoHeaderOffset - pluginScanRegion.Offset;
            //Add it to our list
            Plugin_Scanning_Regions.Add(pluginScanRegion);

            //Let's make our second region
            PluginScanningRegion pluginScanRegion2 = new PluginScanningRegion();
            pluginScanRegion2.Offset = Map.MapHeader.indexOffset + 40 + ExtraFunctions.CalculatePadding(Map.MapHeader.indexOffset + 40, 0x1000);
            pluginScanRegion2.Size = new LocaleHandler(Map).LocaleTables[0].LocaleTableIndexOffset - pluginScanRegion2.Offset;
            //Add it to our list
            Plugin_Scanning_Regions.Add(pluginScanRegion2);

            //Loop through each of our tags
            foreach (HaloMap.TagItem tagItem in Map.IndexItems)
            {
                //Initialize our metaheader_datablock
                MetaHeader_DataBlock mhdb = new MetaHeader_DataBlock(tagItem);

                //Add it to our list
                Plugin_Data_Blocks.Add(mhdb);
                MetaHeader_Data_Blocks.Add(mhdb);

                //Unfreeze
                Application.DoEvents();
            }

            //Open our IO
            Map.OpenIO();



            //Loop through each scan region
            for (int i = 0; i < Plugin_Scanning_Regions.Count; i++)
            {
                //Set our plugin scanning region
                PluginScanningRegion scanRegion = Plugin_Scanning_Regions[i];

                //Make our IO go to the scan regions position
                Map.IO.In.BaseStream.Position = scanRegion.Offset;

                while (Map.IO.In.BaseStream.Position < (scanRegion.Offset + scanRegion.Size))
                {
                    double percent = (((double)Map.IO.In.BaseStream.Position - scanRegion.Offset) / (double)scanRegion.Size) * 100;
                    //Possible count
                    int count = Map.IO.In.ReadInt32();
                    int pointer = Map.IO.In.ReadInt32() - Map.MapHeader.mapMagic;
                    int zeros = Map.IO.In.ReadInt32();

                    //If our zero data = 0
                    if (zeros == 0)
                    {
                        //If our count is valid.
                        if (count > 0 && count < 600000)
                        {
                            //If our pointer is valid.
                            if (PointerIsInRegions(count, pointer))
                            {
                                //Let's initialize our reflexive data block
                                Reflexive_DataBlock rdb = new Reflexive_DataBlock();
                                rdb.Count = count;
                                rdb.Offset = (int)(Map.IO.In.BaseStream.Position - 12);
                                rdb.Pointer = pointer;

                                //Add it to our list
                                Plugin_Data_Blocks.Add(rdb);

                                //Go to our FoundAReflexive location
                                goto FoundAReflexive;
                            }
                        }
                    }
                    //Determine how much to jump back
                    if (PointerIsInRegions(pointer + Map.MapHeader.mapMagic, zeros - Map.MapHeader.mapMagic) && pointer + Map.MapHeader.mapMagic > 0 && pointer + Map.MapHeader.mapMagic < 600000)
                    {
                        //There's a potential reflexive 8 bytes back!
                        Map.IO.In.BaseStream.Position -= 8;
                    }
                    else
                    {
                        //If we are to find idents.
                        if (FindIdents)
                        {
                            //Didn't find anything. Go back and scan for idents
                            Map.IO.In.BaseStream.Position -= 12;

                            //Read our 4 values
                            string tagClass = Map.IO.In.ReadAsciiString(4);

                            //Skip 4 bytes
                            Map.IO.In.BaseStream.Position += 8;

                            //Read our id
                            int ident = Map.IO.In.ReadInt32();

                            //Get our index
                            int index = Map.GetTagIndexByIdent(ident);

                            //If our index isn't -1
                            if (index != -1)
                            {
                                //If our tag's class == tagclass
                                if (Map.IndexItems[index].Class == tagClass)
                                {
                                    //Initialize our new ident
                                    Tag_Ident id = new Tag_Ident();
                                    //Set our offset.
                                    id.Offset = ((int)Map.IO.In.BaseStream.Position - 16);
                                    //Add it to our list
                                    Tag_Idents.Add(id);
                                    //Go to our found ident spot
                                    goto FoundIdent;
                                }
                            }
                            //We didn't find an ident. Go back
                            Map.IO.In.BaseStream.Position -= 12;

                        //Where we go if we found an ident
                        FoundIdent: ;
                        }
                        else
                        {
                            //If we aren't to find idents.
                            //Go 4 bytes back.
                            Map.IO.In.BaseStream.Position -= 4;
                        }
                    }
                //Where we'll jump to if we found a reflex, so we dont go back 8 bytes for no reason, wasting time.
                FoundAReflexive: ;
                }
            }

            //Close our IO
            Map.CloseIO();

            //Let's calculate our sizes.. Loop through each data block
            for (int i = 0; i < Plugin_Data_Blocks.Count; i++)
                CalculateStructureSize(Plugin_Data_Blocks[i]);

            //If we are to map children.
            if (FindChildren)
            {
                //Let's map our children. Loop through each plugin data block
                for (int i = 0; i < Plugin_Data_Blocks.Count; i++)
                    MapChildren(Plugin_Data_Blocks[i]);
            }

            //Return our meta blocks
            return MetaHeader_Data_Blocks;
        }

        public void MapPluginWithPDBList(List<mValue> mValueList, List<PluginDataBlock> PDBList, int chunkSize, int unknownStructCount)
        {
            //Loop until we hit our chunkSize
            for (int i = 0; i < chunkSize; i += 4)
            {
                //Create our reflexive list
                List<PluginDataBlock> PDBList2 = new List<PluginDataBlock>();

                //Loop through each PDBList
                foreach (PluginDataBlock PDB in PDBList)
                {
                    //Loop for each reflexive
                    foreach (Reflexive_DataBlock RDB in PDB.Reflexive_Data_Blocks)
                        if (RDB.Offset_In_Parent == i)
                        {
                            //Add it to our PDB list
                            PDBList2.Add(RDB);
                        }
                }

                //If we got reflexives
                if (PDBList2.Count != 0)
                {
                    //Initialize our reflexive
                    mReflexive mReflex = new mReflexive();
                    mReflex.Name = "Unknown " + unknownStructCount;
                    unknownStructCount++;
                    mReflex.Offset = i;
                    mReflex.Size = FindMostLikelySizeForChunk(PDBList2);
                    mReflex.Visible = false;
                    //Add it to our list
                    mValueList.Add(mReflex);
                    //Map more.
                    MapPluginWithPDBList(mReflex.Values, PDBList2, mReflex.Size, unknownStructCount);
                    //Add 8 to our i.
                    i += 8;
                }
                else
                {
                    //Our bool that indicates if we found an ident.
                    bool foundIdent = false;
                    //Loop through each PDBList
                    foreach (PluginDataBlock PDB in PDBList)
                    {
                        //Loop for each ident
                        foreach (Tag_Ident ident in PDB.Tag_Idents)
                            if (ident.Offset_In_Parent == i)
                            {
                                //Initialize our ident parts
                                mTagRef tRef = new mTagRef();
                                tRef.Name = "Unknown";
                                tRef.Offset = i;
                                tRef.Visible = false;
                                mIdent tIdent = new mIdent();
                                tIdent.Name = "Unknown";
                                tIdent.Offset = i + 12;
                                tIdent.Visible = false;

                                //Add it to our list
                                mValueList.Add(tRef);
                                mValueList.Add(tIdent);

                                //Add 12 more to i
                                i += 12;

                                //We found our ident.
                                foundIdent = true;

                                //Leave.
                                break;
                            }

                        //If we found our ident
                        if (foundIdent)
                            //Leave
                            break;
                    }
                    if (!foundIdent)
                    {
                        //If we got 4 bytes left.
                        if (chunkSize - i >= 4)
                        {
                            //Let's make our unidentified
                            mUndefined mU = new mUndefined();
                            mU.Name = "Unknown";
                            mU.Offset = i;
                            mU.Visible = false;
                            //Add it to our list
                            mValueList.Add(mU);
                        }
                        else
                        {
                            //If we got 2 bytes left
                            if (chunkSize - i >= 2)
                            {
                                //Make our short
                                mInt16 m16 = new mInt16();
                                m16.Name = "Unknown";
                                m16.Offset = i;
                                m16.Visible = false;
                                //Add it to our list
                                mValueList.Add(m16);
                                //Take 2 away from i, since it'll add 4, it makes it progress 2
                                i -= 2;
                            }
                            else
                            {
                                if (chunkSize - i == 1)
                                {
                                    //Make our byte
                                    mByte mB = new mByte();
                                    mB.Name = "Unknown";
                                    mB.Offset = i;
                                    mB.Visible = false;
                                    //Add it to our list
                                    mValueList.Add(mB);
                                    //We're done, we dont need to take away 3 to make i increment 1.
                                }
                            }
                        }
                    }
                }

            }
        }

        public int FindMostLikelySizeForChunk(List<PluginDataBlock> PDBList)
        {
            
            //Initialize our int list
            int[] Sizes = new int[PDBList.Count];

            //Loop for each pdb
            for (int i = 0; i < PDBList.Count; i++)
                Sizes[i] = PDBList[i].Size;
            try
            {
                Array.Sort(Sizes);
                return Sizes[Sizes.Length / 2];
            }
            catch
            {
                return PDBList[0].Size;
            }
        }

        /// <summary>
        /// This function checks if a pointer is in any of the Plugin_Scanning_Regions
        /// </summary>
        /// <param name="count">The count of the reflex</param>
        /// <param name="pointer">The pointer of the reflex</param>
        /// <returns>Returns a bool indicating whether the pointer is in the regions.</returns>
        public bool PointerIsInRegions(int count, int pointer)
        {
            //Loop through both regions
            for (int i = 0; i < Plugin_Scanning_Regions.Count; i++)
            {
                //Check if its in this region
                if (pointer >= Plugin_Scanning_Regions[i].Offset && pointer < (Plugin_Scanning_Regions[i].Offset + Plugin_Scanning_Regions[i].Size) && (Plugin_Scanning_Regions[i].Offset + Plugin_Scanning_Regions[i].Size) - pointer >= count)
                {
                    //it is, so return true
                    return true;
                }
            }

            //We didn't find anything. Return false
            return false;
        }

        /// <summary>
        /// This function calculates the size of the given datablock
        /// </summary>
        /// <param name="dataBlock">The datablock to calculate size for.</param>
        public void CalculateStructureSize(PluginDataBlock dataBlock)
        {
            //Create our integer for the closest number after the datablock
            int closestNumber = 999999999;

            //Loop through each plugin data block
            for (int i = 0; i < Plugin_Data_Blocks.Count; i++)
                //If we find a closer number, set it.
                if (Plugin_Data_Blocks[i].Pointer > dataBlock.Pointer && Plugin_Data_Blocks[i].Pointer < closestNumber)
                    closestNumber = Plugin_Data_Blocks[i].Pointer;

            //Loop through each scan region
            for (int i = 0; i < Plugin_Scanning_Regions.Count; i++)
                //If we find a closer number, set it.
                if (Plugin_Scanning_Regions[i].Offset + Plugin_Scanning_Regions[i].Size > dataBlock.Pointer && Plugin_Scanning_Regions[i].Offset + Plugin_Scanning_Regions[i].Size < closestNumber)
                    closestNumber = Plugin_Scanning_Regions[i].Offset + Plugin_Data_Blocks[i].Size;

            //If we didn't find anything, throw our exception.
            if (closestNumber == 999999999)
                dataBlock.Size = 0;
            else
                //Set our size
                dataBlock.Size = (closestNumber - dataBlock.Pointer) / dataBlock.Count;
        }

        /// <summary>
        /// This function maps our children reflexives and idents for the given datablock
        /// </summary>
        /// <param name="dataBlock">The datablock to map children reflexives/idents for.</param>
        public void MapChildren(PluginDataBlock dataBlock)
        {
            //Loop through each plugin data block
            foreach (PluginDataBlock db in Plugin_Data_Blocks)
                if (db.Data_Block_Type == PluginDataBlock.DataBlockType.Reflexive)
                    //If its in our chunks for this block
                    if (db.Offset >= dataBlock.Pointer && db.Offset < dataBlock.Pointer + (dataBlock.Count * dataBlock.Size))
                    {
                        //Set our offset in parent
                        ((Reflexive_DataBlock)db).Offset_In_Parent = (db.Offset - dataBlock.Pointer) % dataBlock.Size;
                        //Add it to our list
                        dataBlock.Reflexive_Data_Blocks.Add((Reflexive_DataBlock)db);
                    }

            //Loop for each ident
            foreach (Tag_Ident ident in Tag_Idents)
                //If its in our chunks for this block
                if (ident.Offset >= dataBlock.Pointer && ident.Offset < dataBlock.Pointer + (dataBlock.Count * dataBlock.Size))
                {
                    //Set our offset in parent
                    ident.Offset_In_Parent = (ident.Offset - dataBlock.Pointer) % dataBlock.Size;

                    //Add it to our list
                    dataBlock.Tag_Idents.Add(ident);
                }
        }

        /// <summary>
        /// This class represents a data block(reflexive or meta header)
        /// </summary>
        public class PluginDataBlock
        {
            /// <summary>
            /// The type of data block we are dealing with.
            /// </summary>
            public enum DataBlockType
            {
                Meta_Header,
                Reflexive
            }

            private DataBlockType _datablocktype;
            /// <summary>
            /// The type of block we are dealing with.
            /// </summary>
            public DataBlockType Data_Block_Type
            {
                get { return _datablocktype; }
                set { _datablocktype = value; }
            }

            private int _offset;
            /// <summary>
            /// The offset the reflexive/pointer is at.
            /// </summary>
            public int Offset
            {
                get { return _offset; }
                set { _offset = value; }
            }

            private int _count;
            /// <summary>
            /// The count for how many chunks we have at our pointer.
            /// </summary>
            public int Count
            {
                get { return _count; }
                set { _count = value; }
            }

            private int _size;
            /// <summary>
            /// The size a chunk.
            /// </summary>
            public int Size
            {
                get { return _size; }
                set { _size = value; }
            }

            private int _pointer;
            /// <summary>
            /// The pointer for the data block(i.e: where the data for the block is located)
            /// </summary>
            public int Pointer
            {
                get { return _pointer; }
                set { _pointer = value; }
            }

            private List<Reflexive_DataBlock> _reflexivedatablocks;
            /// <summary>
            /// Our list of reflexives for this PluginDataBlock.
            /// </summary>
            public List<Reflexive_DataBlock> Reflexive_Data_Blocks
            {
                get { return _reflexivedatablocks; }
                set { _reflexivedatablocks = value; }
            }

            private List<Tag_Ident> _tagidents;
            /// <summary>
            /// A list of tag idents in this chunk.
            /// </summary>
            public List<Tag_Ident> Tag_Idents
            {
                get { return _tagidents; }
                set { _tagidents = value; }
            }

            /// <summary>
            /// This function checks if our block contains a given ident at an offset.
            /// </summary>
            /// <param name="offset">The offset of our ident to search for</param>
            /// <returns>Returns a bool indicating if we found the ident.</returns>
            public bool ContainsIdent(int offset)
            {
                //Loop through each ident
                foreach (Tag_Ident ident in Tag_Idents)
                    //If our offset is found.
                    if (ident.Offset_In_Parent == offset)
                        //Return true.
                        return true;

                //Otherwise return false.
                return false;
            }

        }

        public class MetaHeader_DataBlock : PluginDataBlock
        {

            private string _tagclass;
            /// <summary>
            /// The tag class we are dealing with for our Data_Block_Type of Meta_Header.
            /// </summary>
            public string Tag_Class
            {
                get { return _tagclass; }
                set { _tagclass = value; }
            }

            public MetaHeader_DataBlock(HaloMap.TagItem tag)
            {
                //Initialize our list
                Reflexive_Data_Blocks = new List<Reflexive_DataBlock>();
                Tag_Idents = new List<Tag_Ident>();

                //Set the type
                Data_Block_Type = DataBlockType.Meta_Header;

                //Set our count as 1
                Count = 1;

                //Set our pointer
                Pointer = tag.Offset;

                //Set our class
                Tag_Class = tag.Class;
            }

        }

        public class Reflexive_DataBlock : PluginDataBlock
        {
            private int _offsetinparent;
            /// <summary>
            /// The offset the reflexive/pointer is at in it's parent.
            /// </summary>
            public int Offset_In_Parent
            {
                get { return _offsetinparent; }
                set { _offsetinparent = value; }
            }

            public Reflexive_DataBlock()
            {

                //Initialize our list
                Reflexive_Data_Blocks = new List<Reflexive_DataBlock>();
                Tag_Idents = new List<Tag_Ident>();

                //Set the type
                Data_Block_Type = DataBlockType.Reflexive;
            }
        }

        public class Tag_Ident
        {
            private int _offset;
            /// <summary>
            /// The offset the reflexive/pointer is at.
            /// </summary>
            public int Offset
            {
                get { return _offset; }
                set { _offset = value; }
            }

            private int _offsetinparent;
            /// <summary>
            /// The offset the reflexive/pointer is at in it's parent.
            /// </summary>
            public int Offset_In_Parent
            {
                get { return _offsetinparent; }
                set { _offsetinparent = value; }
            }
        }

        /// <summary>
        /// This class indicates where in the map file reflexive's and pointers should be scanned in.
        /// </summary>
        public class PluginScanningRegion
        {
            private int _offset;
            /// <summary>
            /// The offset of our region to scan.
            /// </summary>
            public int Offset
            {
                get { return _offset; }
                set { _offset = value; }
            }

            private int _size;
            /// <summary>
            /// The size of our region to scan.
            /// </summary>
            public int Size
            {
                get { return _size; }
                set { _size = value; }
            }
        }
    }
}

using System.Collections.Generic;
using Alteration.Halo_3.Plugins;
using Alteration.Halo_3.Values;
using HaloDeveloper.Map;

namespace Alteration.Halo_3.Meta_Parser
{
    public class MetaParser
    {

        #region Values

        private List<Ident> _ident;
        private HaloMap _map;
        private List<Structure> _structures;
        private List<StringIdentifier> _strings;
        private int _tagindex;

        public HaloMap Map
        {
            get { return _map; }
            set { _map = value; }
        }

        public int TagIndex
        {
            get { return _tagindex; }
            set { _tagindex = value; }
        }

        public List<Structure> Structures
        {
            get { return _structures; }
            set { _structures = value; }
        }

        public List<Ident> Idents
        {
            get { return _ident; }
            set { _ident = value; }
        }

        public List<StringIdentifier> Strings
        {
            get { return _strings; }
            set { _strings = value; }
        }

        #endregion

        #region Classes

        #region Nested type: Ident

        public class Ident
        {
            private int _ident;
            private string _name;

            private int _offset;
            private string _tagclass;
            private string _tagname;

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            public int Offset
            {
                get { return _offset; }
                set { _offset = value; }
            }

            public int ID
            {
                get { return _ident; }
                set { _ident = value; }
            }

            public string TagClass
            {
                get { return _tagclass; }
                set { _tagclass = value; }
            }

            public string TagName
            {
                get { return _tagname; }
                set { _tagname = value; }
            }
        }

        #endregion

        #region Nested type: Structure

        public class Structure
        {
            private int _count;
            private string _name;
            private int _offset;
            private int _pointer;
            private int _size;

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            public int Count
            {
                get { return _count; }
                set { _count = value; }
            }

            public int Pointer
            {
                get { return _pointer; }
                set { _pointer = value; }
            }

            public int Offset
            {
                get { return _offset; }
                set { _offset = value; }
            }

            public int Size
            {
                get { return _size; }
                set { _size = value; }
            }
        }

        #endregion

        #region Nested type: Strings

        public class StringIdentifier
        {
            private int _ident;
            private string _name;

            private int _offset;
            private string _stringname;
            private int _stringindex;

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            public int Offset
            {
                get { return _offset; }
                set { _offset = value; }
            }

            public int Identifier
            {
                get { return _ident; }
                set { _ident = value; }
            }

            public string StringName
            {
                get { return _stringname; }
                set { _stringname = value; }
            }

            public int StringIndex
            {
                get { return _stringindex; }
                set { _stringindex = value; }
            }
        }

        #endregion

        #endregion

        #region Initialization

        public MetaParser(HaloMap map, int tagIndex)
        {
            //Set our instance of map to the parameter
            Map = map;
            //Set our instance of tag index to the parameter
            TagIndex = tagIndex;
        }

        public void ParseMeta(XmlParser xmlParser)
        {
            //Initialize our structure list
            Structures = new List<Structure>();
            //Initialize our ident list
            Idents = new List<Ident>();
            //Initialize our string list
            Strings = new List<StringIdentifier>();
            //Open the IO
            Map.OpenIO();
            //Parse the meta recursively
            ParseRecursively(xmlParser.ValueList, Map.IndexItems[TagIndex].Offset, -1);
            //Close IO
            Map.CloseIO();
        }

        private void ParseRecursively(List<mValue> ValueList, int chunkOffset, int chunkNumber)
        {
            //Loop through values
            for (int i = 0; i < ValueList.Count; i++)
            {
                //Determine the value type
                switch (ValueList[i].Attributes)
                {
                    case mValue.ObjectAttributes.Reflexive:
                        {
                            //If the offset is rediculous..
                            if (chunkOffset + ValueList[i].Offset > (int) Map.IO.In.BaseStream.Length)
                            {
                                break;
                            }
                            //Go to the reflexive offset
                            Map.IO.In.BaseStream.Position = chunkOffset + ValueList[i].Offset;

                            //Get the reflexive
                            mReflexive mreflexive = (mReflexive) ValueList[i];
                            //Initialize our instance of structure
                            Structure structure = new Structure();
                            //Assign the name
                            structure.Name = mreflexive.Name;
                            //Add chunk modifier
                            if (chunkNumber != -1)
                            {
                                structure.Name += " [" + chunkNumber + "]";
                            }
                            //Assign the size
                            structure.Size = mreflexive.Size;
                            //Read the count
                            structure.Count = Map.IO.In.ReadInt32();
                            //Read the pointer
                            structure.Pointer = Map.IO.In.ReadInt32() - Map.MapHeader.mapMagic;

                            //If the count or pointer are bad, break out and dont add it to the list
                            if (structure.Count <= 0 | structure.Pointer <= 0 | structure.Count > 100000)
                            {
                                break;
                            }

                            //Assign the offset
                            structure.Offset = chunkOffset + mreflexive.Offset;
                            //Add it to the list
                            Structures.Add(structure);
                            //Loop through all the chunks
                            for (int z = 0; z < structure.Count; z++)
                            {
                                //Parse recursively
                                ParseRecursively(mreflexive.Values, structure.Pointer + (z*structure.Size), z);
                            }
                            break;
                        }
                    case mValue.ObjectAttributes.Ident:
                        {
                            //If the offset is rediculous..
                            if (chunkOffset + ValueList[i].Offset > (int) Map.IO.In.BaseStream.Length)
                            {
                                break;
                            }

                            //Go to the ident offset
                            Map.IO.In.BaseStream.Position = chunkOffset + ValueList[i].Offset;

                            //Get the ident instance
                            mIdent mident = (mIdent) ValueList[i];
                            //Initialize our instance of the ident
                            Ident ident = new Ident();
                            //Assign the name
                            ident.Name = mident.Name;
                            if (chunkNumber != -1)
                            {
                                ident.Name += " [" + chunkNumber + "]";
                            }
                            //Assign the offset
                            ident.Offset = chunkOffset + mident.Offset - 12;
                            //Assign the Ident
                            ident.ID = Map.IO.In.ReadInt32();
                            //Get the tag index
                            int index = Map.GetTagIndexByIdent(ident.ID);
                            //Assign the class
                            if (index != -1)
                            {
                                ident.TagClass = Map.IndexItems[index].Class;
                            }
                            else
                            {
                                ident.TagClass = "Null";
                            }
                            //Assign the name
                            if (index != -1)
                            {
                                ident.TagName = Map.IndexItems[index].Name;
                            }
                            else
                            {
                                ident.TagName = "Null";
                            }
                            //Add to the ident to the list
                            Idents.Add(ident);
                            break;
                        }
                    case mValue.ObjectAttributes.StringID:
                        {
                            //If the offset is rediculous..
                            if (chunkOffset + ValueList[i].Offset > (int)Map.IO.In.BaseStream.Length)
                            {
                                break;
                            }

                            //Go to the ident offset
                            Map.IO.In.BaseStream.Position = chunkOffset + ValueList[i].Offset;

                            //Get the ident instance
                            mStringID mstringid = (mStringID)ValueList[i];
                            //Initialize our instance of the string id.
                            StringIdentifier str = new StringIdentifier();
                            //Assign the name
                            str.Name = mstringid.Name;
                            if (chunkNumber != -1)
                            {
                                str.Name += " [" + chunkNumber + "]";
                            }
                            //Assign the offset
                            str.Offset = chunkOffset + mstringid.Offset;
                            //Assign the Ident
                            str.Identifier = Map.IO.In.ReadInt32();
                            //Get the string index
                            int index = Map.StringTable.GetStringItemIndexByID(Map,str.Identifier);
                            //Assign the string index
                            str.StringIndex = index;
                            //Try to..
                            try
                            {
                                //Assign the string name
                                str.StringName = Map.StringTable.StringItems[index].Name;
                            }
                            catch
                            {
                                //Assign our invalid name
                                str.StringName = "<<invalid sid>>";
                            }
                            //Add to the string to the list
                            Strings.Add(str);
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
using System.Collections.Generic;
using System.Xml;
using Alteration.Halo_3.Values;
using System.IO;

namespace Alteration.Halo_3.Plugins
{
    public class XmlParser
    {
        private string _author;
        private int _headersize;
        private List<mRevision> _revisions;
        private string _tagclass;
        private List<mValue> _valuelist;
        private float _version;

        public string TagClass
        {
            get { return _tagclass; }
            set { _tagclass = value; }
        }

        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }

        public float Version
        {
            get { return _version; }
            set { _version = value; }
        }

        public int HeaderSize
        {
            get { return _headersize; }
            set { _headersize = value; }
        }

        public List<mRevision> Revisions
        {
            get { return _revisions; }
            set { _revisions = value; }
        }

        public List<mValue> ValueList
        {
            get { return _valuelist; }
            set { _valuelist = value; }
        }

        public XmlParser()
        {
            ValueList = new List<mValue>();
            Revisions = new List<mRevision>();
        }

        public void ParsePlugin(string fileName)
        {
            //Initialize our Value List.
            List<mValue> valueList = new List<mValue>();

            //Initialize our XmlDocument for parsing
            XmlDocument xmlDoc = new XmlDocument();
            //Load our plugin
            xmlDoc.Load(fileName);

            //Get the root to read the header information from
            XmlElement root = xmlDoc.DocumentElement;

            //Parse our basic information. Plugin Class first.
            TagClass = root.Attributes["class"].Value;
            //Now we parse the author.
            Author = root.Attributes["author"].Value;
            //Then we parse the version of the plugin.
            Version = float.Parse(root.Attributes["version"].Value);
            //Parse the headersize, which will indicate how big the Tag is without any reflexives.
            HeaderSize = int.Parse(root.Attributes["headersize"].Value);

            //Initialize our revision list incase there are revisions.
            Revisions = new List<mRevision>();
            //Initialize our value list.
            ValueList = new List<mValue>();
            //Loop through all the nodes
            for (int i = 0; i < root.ChildNodes.Count; i++)
            {
                //If the name isnt revision...
                if (root.ChildNodes[i].Name != "revision")
                {
                    //Then get the returned value.
                    mValue returnedVal = ReadNode(root.ChildNodes[i]);
                    //And if it isn't an unknown value
                    if (returnedVal.Attributes != mValue.ObjectAttributes.None)
                    {
                        //Then add it to the list.
                        ValueList.Add(returnedVal);
                    }
                }
                else
                {
                    //Otherwise...
                    //Initialize a new instance of our revision.
                    mRevision revision = new mRevision();
                    //Set the author for this revision.
                    revision.Author = root.ChildNodes[i].Attributes["author"].Value;
                    //Set the version for this revision.
                    revision.Version = float.Parse(root.ChildNodes[i].Attributes["version"].Value);
                    //Set the description for this revision.
                    revision.Description = root.ChildNodes[i].InnerText;
                    //Add the revision to the Revisions list.
                    Revisions.Add(revision);
                }
            }
        }

        private mValue ReadNode(XmlNode xmlNode)
        {
            //Indiciate what type of value this is.
            switch (xmlNode.Name.ToLower())
            {
                case "reflexive":
                case "structure":
                case "reflex":
                case "struct":
                    {
                        //Initialize our instance of a reflexive.
                        mReflexive reflexive = new mReflexive();
                        //Retrieve the structure's name.
                        reflexive.Name = xmlNode.Attributes["name"].Value;
                        //Retrieve its offset within its parent.
                        reflexive.Offset = int.Parse(xmlNode.Attributes["offset"].Value);
                        //Retrieve its chunk size.
                        reflexive.Size = int.Parse(xmlNode.Attributes["size"].Value);
                        //Retrieve its visibility flag.
                        reflexive.Visible = bool.Parse(xmlNode.Attributes["visible"].Value);

                        //Initialize the value list for the reflexive
                        reflexive.Values = new List<mValue>();

                        //Recursively add any nodes that belong to it.
                        for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
                        {
                            //Get our returned value.
                            mValue returnedVal = ReadNode(xmlNode.ChildNodes[i]);

                            //If its not some unknown node that couldnt be read...
                            if (returnedVal.Attributes != mValue.ObjectAttributes.None)
                            {
                                //Then add it to this reflexive value list.
                                reflexive.Values.Add(returnedVal);
                            }
                        }
                        //Return our reflexive
                        return reflexive;
                    }
                case "tagref":
                case "tag":
                    {
                        //Initialize our instance of TagRef
                        mTagRef tagRef = new mTagRef();
                        //Assign the name
                        tagRef.Name = xmlNode.Attributes["name"].Value;
                        //Assign the offset
                        tagRef.Offset = int.Parse(xmlNode.Attributes["offset"].Value);
                        //Assign the visibility flag
                        tagRef.Visible = bool.Parse(xmlNode.Attributes["visible"].Value);
                        //return the value
                        return tagRef;
                    }
                case "ident":
                case "id":
                case "tagid":
                    {
                        //Initialize our instance of ident
                        mIdent ident = new mIdent();
                        //Assign the name
                        ident.Name = xmlNode.Attributes["name"].Value;
                        //Assign the offset
                        ident.Offset = int.Parse(xmlNode.Attributes["offset"].Value);
                        //Assign the visibility flag
                        ident.Visible = bool.Parse(xmlNode.Attributes["visible"].Value);
                        //return the value
                        return ident;
                    }
                case "sid":
                case "stringid":
                case "stringidentifier":
                    {
                        //Initialize our instance of string
                        mStringID stringID = new mStringID();
                        //Assign the name
                        stringID.Name = xmlNode.Attributes["name"].Value;
                        //Assign the offset
                        stringID.Offset = int.Parse(xmlNode.Attributes["offset"].Value);
                        //Assign the visibility flag
                        stringID.Visible = bool.Parse(xmlNode.Attributes["visible"].Value);
                        //return the value
                        return stringID;
                    }
                case "bitmask8":
                case "bit8":
                    {
                        //Initialize our instance of bitmask8
                        mBitmask8 bitmask8 = new mBitmask8();
                        //Assign the name
                        bitmask8.Name = xmlNode.Attributes["name"].Value;
                        //Assign the offset
                        bitmask8.Offset = int.Parse(xmlNode.Attributes["offset"].Value);
                        //Assign the visibility flag
                        bitmask8.Visible = bool.Parse(xmlNode.Attributes["visible"].Value);
                        //Initialize the bitOption list.
                        bitmask8.Options = new List<mBitOption>();
                        //Loop through the children nodes for options.
                        for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
                        {
                            //if its an option...
                            if (xmlNode.ChildNodes[i].Name.ToLower() == "option")
                            {
                                //then initialize a new instance of option
                                mBitOption bitOption = new mBitOption();
                                //Get the name of the bit
                                bitOption.Name = xmlNode.ChildNodes[i].Attributes["name"].Value;
                                //Get the bit value
                                bitOption.BitIndex = int.Parse(xmlNode.ChildNodes[i].Attributes["value"].Value);
                                //Add it to the bitList
                                bitmask8.Options.Add(bitOption);
                            }
                        }
                        //return the value
                        return bitmask8;
                    }
                case "bitmask16":
                case "bit16":
                    {
                        //Initialize our instance of bitmask16
                        mBitmask16 bitmask16 = new mBitmask16();
                        //Assign the name
                        bitmask16.Name = xmlNode.Attributes["name"].Value;
                        //Assign the offset
                        bitmask16.Offset = int.Parse(xmlNode.Attributes["offset"].Value);
                        //Assign the visibility flag
                        bitmask16.Visible = bool.Parse(xmlNode.Attributes["visible"].Value);
                        //Initialize the bitOption list.
                        bitmask16.Options = new List<mBitOption>();
                        //Loop through the children nodes for options.
                        for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
                        {
                            //if its an option...
                            if (xmlNode.ChildNodes[i].Name.ToLower() == "option")
                            {
                                //then initialize a new instance of option
                                mBitOption bitOption = new mBitOption();
                                //Get the name of the bit
                                bitOption.Name = xmlNode.ChildNodes[i].Attributes["name"].Value;
                                //Get the bit value
                                bitOption.BitIndex = int.Parse(xmlNode.ChildNodes[i].Attributes["value"].Value);
                                //Add it to the bitList
                                bitmask16.Options.Add(bitOption);
                            }
                        }
                        //return the value
                        return bitmask16;
                    }
                case "bitmask32":
                case "bit32":
                    {
                        //Initialize our instance of bitmask32
                        mBitmask32 bitmask32 = new mBitmask32();
                        //Assign the name
                        bitmask32.Name = xmlNode.Attributes["name"].Value;
                        //Assign the offset
                        bitmask32.Offset = int.Parse(xmlNode.Attributes["offset"].Value);
                        //Assign the visibility flag
                        bitmask32.Visible = bool.Parse(xmlNode.Attributes["visible"].Value);
                        //Initialize the bitOption list.
                        bitmask32.Options = new List<mBitOption>();
                        //Loop through the children nodes for options.
                        for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
                        {
                            //if its an option...
                            if (xmlNode.ChildNodes[i].Name.ToLower() == "option")
                            {
                                //then initialize a new instance of option
                                mBitOption bitOption = new mBitOption();
                                //Get the name of the bit
                                bitOption.Name = xmlNode.ChildNodes[i].Attributes["name"].Value;
                                //Get the bit value
                                bitOption.BitIndex = int.Parse(xmlNode.ChildNodes[i].Attributes["value"].Value);
                                //Add it to the bitList
                                bitmask32.Options.Add(bitOption);
                            }
                        }
                        //return the value
                        return bitmask32;
                    }
                case "enum8":
                    {
                        //Initialize our instance of enum8
                        mEnum8 enum8 = new mEnum8();
                        //Assign the name
                        enum8.Name = xmlNode.Attributes["name"].Value;
                        //Assign the offset
                        enum8.Offset = int.Parse(xmlNode.Attributes["offset"].Value);
                        //Assign the visibility flag
                        enum8.Visible = bool.Parse(xmlNode.Attributes["visible"].Value);
                        //Initialize the enumOption list.
                        enum8.Options = new List<mEnumOption>();
                        //Loop through the children nodes for options.
                        for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
                        {
                            //if its an option...
                            if (xmlNode.ChildNodes[i].Name.ToLower() == "option")
                            {
                                //then initialize a new instance of option
                                mEnumOption enumOption = new mEnumOption();
                                //Get the name of the bit
                                enumOption.Name = xmlNode.ChildNodes[i].Attributes["name"].Value;
                                //Get the bit value
                                enumOption.Value = int.Parse(xmlNode.ChildNodes[i].Attributes["value"].Value);
                                //Add it to the bitList
                                enum8.Options.Add(enumOption);
                            }
                        }
                        //return the value
                        return enum8;
                    }
                case "enum16":
                    {
                        //Initialize our instance of enum16
                        mEnum16 enum16 = new mEnum16();
                        //Assign the name
                        enum16.Name = xmlNode.Attributes["name"].Value;
                        //Assign the offset
                        enum16.Offset = int.Parse(xmlNode.Attributes["offset"].Value);
                        //Assign the visibility flag
                        enum16.Visible = bool.Parse(xmlNode.Attributes["visible"].Value);
                        //Initialize the enumOption list.
                        enum16.Options = new List<mEnumOption>();
                        //Loop through the children nodes for options.
                        for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
                        {
                            //if its an option...
                            if (xmlNode.ChildNodes[i].Name.ToLower() == "option")
                            {
                                //then initialize a new instance of option
                                mEnumOption enumOption = new mEnumOption();
                                //Get the name of the bit
                                enumOption.Name = xmlNode.ChildNodes[i].Attributes["name"].Value;
                                //Get the bit value
                                enumOption.Value = int.Parse(xmlNode.ChildNodes[i].Attributes["value"].Value);
                                //Add it to the bitList
                                enum16.Options.Add(enumOption);
                            }
                        }
                        //return the value
                        return enum16;
                    }
                case "enum32":
                    {
                        //Initialize our instance of enum32
                        mEnum32 enum32 = new mEnum32();
                        //Assign the name
                        enum32.Name = xmlNode.Attributes["name"].Value;
                        //Assign the offset
                        enum32.Offset = int.Parse(xmlNode.Attributes["offset"].Value);
                        //Assign the visibility flag
                        enum32.Visible = bool.Parse(xmlNode.Attributes["visible"].Value);
                        //Initialize the enumOption list.
                        enum32.Options = new List<mEnumOption>();
                        //Loop through the children nodes for options.
                        for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
                        {
                            //if its an option...
                            if (xmlNode.ChildNodes[i].Name.ToLower() == "option")
                            {
                                //then initialize a new instance of option
                                mEnumOption enumOption = new mEnumOption();
                                //Get the name of the bit
                                enumOption.Name = xmlNode.ChildNodes[i].Attributes["name"].Value;
                                //Get the bit value
                                enumOption.Value = int.Parse(xmlNode.ChildNodes[i].Attributes["value"].Value);
                                //Add it to the bitList
                                enum32.Options.Add(enumOption);
                            }
                        }
                        //return the value
                        return enum32;
                    }
                case "int8":
                case "byte":
                    {
                        //Initialize our instance of byte
                        mByte byteVal = new mByte();
                        //Assign the name
                        byteVal.Name = xmlNode.Attributes["name"].Value;
                        //Assign the offset
                        byteVal.Offset = int.Parse(xmlNode.Attributes["offset"].Value);
                        //Assign the visibility flag
                        byteVal.Visible = bool.Parse(xmlNode.Attributes["visible"].Value);
                        //return the value
                        return byteVal;
                    }
                case "int16":
                case "short":
                    {
                        //Initialize our instance of int16
                        mInt16 int16Val = new mInt16();
                        //Assign the name
                        int16Val.Name = xmlNode.Attributes["name"].Value;
                        //Assign the offset
                        int16Val.Offset = int.Parse(xmlNode.Attributes["offset"].Value);
                        //Assign the visibility flag
                        int16Val.Visible = bool.Parse(xmlNode.Attributes["visible"].Value);
                        //return the value
                        return int16Val;
                    }
                case "uint16":
                case "ushort":
                    {
                        //Initialize our instance of uint16
                        mUInt16 uint16Val = new mUInt16();
                        //Assign the name
                        uint16Val.Name = xmlNode.Attributes["name"].Value;
                        //Assign the offset
                        uint16Val.Offset = int.Parse(xmlNode.Attributes["offset"].Value);
                        //Assign the visibility flag
                        uint16Val.Visible = bool.Parse(xmlNode.Attributes["visible"].Value);
                        //return the value
                        return uint16Val;
                    }
                case "int32":
                case "long":
                case "int":
                    {
                        //Initialize our instance of int32
                        mInt32 int32Val = new mInt32();
                        //Assign the name
                        int32Val.Name = xmlNode.Attributes["name"].Value;
                        //Assign the offset
                        int32Val.Offset = int.Parse(xmlNode.Attributes["offset"].Value);
                        //Assign the visibility flag
                        int32Val.Visible = bool.Parse(xmlNode.Attributes["visible"].Value);
                        //return the value
                        return int32Val;
                    }
                case "uint32":
                case "ulong":
                case "uint":
                    {
                        //Initialize our instance of uint32
                        mUInt32 uint32Val = new mUInt32();
                        //Assign the name
                        uint32Val.Name = xmlNode.Attributes["name"].Value;
                        //Assign the offset
                        uint32Val.Offset = int.Parse(xmlNode.Attributes["offset"].Value);
                        //Assign the visibility flag
                        uint32Val.Visible = bool.Parse(xmlNode.Attributes["visible"].Value);
                        //return the value
                        return uint32Val;
                    }
                case "single":
                case "float":
                    {
                        //Initialize our instance of float
                        mFloat floatVal = new mFloat();
                        //Assign the name
                        floatVal.Name = xmlNode.Attributes["name"].Value;
                        //Assign the offset
                        floatVal.Offset = int.Parse(xmlNode.Attributes["offset"].Value);
                        //Assign the visibility flag
                        floatVal.Visible = bool.Parse(xmlNode.Attributes["visible"].Value);
                        //return the value
                        return floatVal;
                    }
                case "string32":
                case "32string":
                    {
                        //Initialize our instance of string32
                        mString32 string32 = new mString32();
                        //Assign the name
                        string32.Name = xmlNode.Attributes["name"].Value;
                        //Assign the offset
                        string32.Offset = int.Parse(xmlNode.Attributes["offset"].Value);
                        //Assign the visibility flag
                        string32.Visible = bool.Parse(xmlNode.Attributes["visible"].Value);
                        //return the value
                        return string32;
                    }
                case "string256":
                case "256string":
                    {
                        //Initialize our instance of string32
                        mString256 string256 = new mString256();
                        //Assign the name
                        string256.Name = xmlNode.Attributes["name"].Value;
                        //Assign the offset
                        string256.Offset = int.Parse(xmlNode.Attributes["offset"].Value);
                        //Assign the visibility flag
                        string256.Visible = bool.Parse(xmlNode.Attributes["visible"].Value);
                        //return the value
                        return string256;
                    }
                case "unic64":
                case "unicode64":
                case "64unic":
                case "64unicode":
                    {
                        //Initialize our instance of unicode64
                        mUnicode64 unicode64 = new mUnicode64();
                        //Assign the name
                        unicode64.Name = xmlNode.Attributes["name"].Value;
                        //Assign the offset
                        unicode64.Offset = int.Parse(xmlNode.Attributes["offset"].Value);
                        //Assign the visibility flag
                        unicode64.Visible = bool.Parse(xmlNode.Attributes["visible"].Value);
                        //return the value
                        return unicode64;
                    }
                case "unic256":
                case "unicode256":
                case "256unic":
                case "256unicode":
                    {
                        //Initialize our instance of unicode64
                        mUnicode256 unicode256 = new mUnicode256();
                        //Assign the name
                        unicode256.Name = xmlNode.Attributes["name"].Value;
                        //Assign the offset
                        unicode256.Offset = int.Parse(xmlNode.Attributes["offset"].Value);
                        //Assign the visibility flag
                        unicode256.Visible = bool.Parse(xmlNode.Attributes["visible"].Value);
                        //return the value
                        return unicode256;
                    }
                case "undefined":
                case "unknown":
                    {
                        //Initialize our instance of undefined
                        mUndefined undefined = new mUndefined();
                        //Assign the name
                        undefined.Name = xmlNode.Attributes["name"].Value;
                        //Assign the offset
                        undefined.Offset = int.Parse(xmlNode.Attributes["offset"].Value);
                        //Assign the visibility flag
                        undefined.Visible = bool.Parse(xmlNode.Attributes["visible"].Value);
                        //return the value
                        return undefined;
                    }
                case "unused":
                case "blank":
                    {
                        //Initialize our instance of undefined
                        mUnused unused = new mUnused();
                        //Assign the offset
                        unused.Offset = int.Parse(xmlNode.Attributes["offset"].Value);
                        //Assign the visibility flag
                        unused.Size = int.Parse(xmlNode.Attributes["size"].Value);
                        //return the value
                        return unused;
                    }
            }
            //If all else fails, make a 'none' value.
            mValue blankVal = new mValue();
            blankVal.Attributes = mValue.ObjectAttributes.None;
            return blankVal;
        }

        public void WritePlugin(string fileName)
        {
            //Initialize our XmlTextWriter
            XmlTextWriter xtw = new XmlTextWriter(new FileStream(fileName, FileMode.Create),System.Text.Encoding.ASCII);

            //Set our formatting to indent
            xtw.Formatting = Formatting.Indented;

            //Write our plugin header start attribute
            xtw.WriteStartElement("plugin");

            //Write our attribute strings
            xtw.WriteAttributeString("class", TagClass);

            //Write our author
            xtw.WriteAttributeString("author", Author);

            //Write our version
            xtw.WriteAttributeString("version", Version.ToString());

            //Write our headersize
            xtw.WriteAttributeString("headersize", HeaderSize.ToString());

            //Loop for each revision
            foreach (mRevision revision in Revisions)
            {
                //Write our start element
                xtw.WriteStartElement("revision");

                //Write our author
                xtw.WriteAttributeString("author", revision.Author);

                //Write our version
                xtw.WriteAttributeString("version", revision.Version.ToString());

                //Write our description
                xtw.WriteString(revision.Description);

                //Write our end element
                xtw.WriteEndElement();
            }

            //Loop for each value in the ValueList
            foreach (mValue val in ValueList)
                //Write our node
                WriteNode(xtw, val);

            //Write our end element
            xtw.WriteEndElement();

            //Close our writer
            xtw.Close();
        }

        public void WriteNode(XmlTextWriter xtw, mValue mVal)
        {
            //Determine our value type
            switch (mVal.Attributes)
            {
                case mValue.ObjectAttributes.Bitmask8:
                    {
                        //Write our values
                        xtw.WriteStartElement("bitmask8");
                        xtw.WriteAttributeString("name", mVal.Name);
                        xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                        xtw.WriteAttributeString("visible", mVal.Visible.ToString());

                        //Get our value as a bitmask8
                        foreach (mBitOption bit in ((mBitmask8)mVal).Options)
                        {
                            //Write our bit start
                            xtw.WriteStartElement("option");

                            //Write our name
                            xtw.WriteAttributeString("name", bit.Name);

                            //Write our value
                            xtw.WriteAttributeString("value", bit.BitIndex.ToString());

                            //Write our end element
                            xtw.WriteEndElement();
                        }

                        //Write our end element
                        xtw.WriteEndElement();
                        break;
                    }
                case mValue.ObjectAttributes.Bitmask16:
                    {
                        //Write our values
                        xtw.WriteStartElement("bitmask16");
                        xtw.WriteAttributeString("name", mVal.Name);
                        xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                        xtw.WriteAttributeString("visible", mVal.Visible.ToString());

                        //Get our value as a bitmask16
                        foreach (mBitOption bit in ((mBitmask16)mVal).Options)
                        {
                            //Write our bit start
                            xtw.WriteStartElement("option");

                            //Write our name
                            xtw.WriteAttributeString("name", bit.Name);

                            //Write our value
                            xtw.WriteAttributeString("value", bit.BitIndex.ToString());

                            //Write our end element
                            xtw.WriteEndElement();
                        }

                        //Write our end element
                        xtw.WriteEndElement();
                        break;
                    }
                case mValue.ObjectAttributes.Bitmask32:
                    {
                        //Write our values
                        xtw.WriteStartElement("bitmask32");
                        xtw.WriteAttributeString("name", mVal.Name);
                        xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                        xtw.WriteAttributeString("visible", mVal.Visible.ToString());

                        //Get our value as a bitmask32
                        foreach (mBitOption bit in ((mBitmask32)mVal).Options)
                        {
                            //Write our bit start
                            xtw.WriteStartElement("option");

                            //Write our name
                            xtw.WriteAttributeString("name", bit.Name);

                            //Write our value
                            xtw.WriteAttributeString("value", bit.BitIndex.ToString());

                            //Write our end element
                            xtw.WriteEndElement();
                        }

                        //Write our end element
                        xtw.WriteEndElement();
                        break;
                    }
                case mValue.ObjectAttributes.Enum8:
                    {
                        //Write our values
                        xtw.WriteStartElement("enum8");
                        xtw.WriteAttributeString("name", mVal.Name);
                        xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                        xtw.WriteAttributeString("visible", mVal.Visible.ToString());

                        //Get our value as a enum
                        foreach (mEnumOption opt in ((mEnum8)mVal).Options)
                        {
                            //Write our bit start
                            xtw.WriteStartElement("option");

                            //Write our name
                            xtw.WriteAttributeString("name", opt.Name);

                            //Write our value
                            xtw.WriteAttributeString("value", opt.Value.ToString());

                            //Write our end element
                            xtw.WriteEndElement();
                        }

                        //Write our end element
                        xtw.WriteEndElement();
                        break;
                    }
                case mValue.ObjectAttributes.Enum16:
                    {
                        //Write our values
                        xtw.WriteStartElement("enum16");
                        xtw.WriteAttributeString("name", mVal.Name);
                        xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                        xtw.WriteAttributeString("visible", mVal.Visible.ToString());

                        //Get our value as a enum
                        foreach (mEnumOption opt in ((mEnum16)mVal).Options)
                        {
                            //Write our bit start
                            xtw.WriteStartElement("option");

                            //Write our name
                            xtw.WriteAttributeString("name", opt.Name);

                            //Write our value
                            xtw.WriteAttributeString("value", opt.Value.ToString());

                            //Write our end element
                            xtw.WriteEndElement();
                        }

                        //Write our end element
                        xtw.WriteEndElement();
                        break;
                    }
                case mValue.ObjectAttributes.Enum32:
                    {
                        //Write our values
                        xtw.WriteStartElement("enum32");
                        xtw.WriteAttributeString("name", mVal.Name);
                        xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                        xtw.WriteAttributeString("visible", mVal.Visible.ToString());

                        //Get our value as a enum
                        foreach (mEnumOption opt in ((mEnum32)mVal).Options)
                        {
                            //Write our bit start
                            xtw.WriteStartElement("option");

                            //Write our name
                            xtw.WriteAttributeString("name", opt.Name);

                            //Write our value
                            xtw.WriteAttributeString("value", opt.Value.ToString());

                            //Write our end element
                            xtw.WriteEndElement();
                        }

                        //Write our end element
                        xtw.WriteEndElement();
                        break;
                    }
                case mValue.ObjectAttributes.Byte:
                    {
                        //Write our values
                        xtw.WriteStartElement("byte");
                        xtw.WriteAttributeString("name", mVal.Name);
                        xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                        xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                        //Write our end element
                        xtw.WriteEndElement();
                        break;
                    }
                case mValue.ObjectAttributes.Float:
                    {
                        //Write our values
                        xtw.WriteStartElement("float");
                        xtw.WriteAttributeString("name", mVal.Name);
                        xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                        xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                        //Write our end element
                        xtw.WriteEndElement();
                        break;
                    }
                case mValue.ObjectAttributes.Int16:
                    {
                        //Write our values
                        xtw.WriteStartElement("short");
                        xtw.WriteAttributeString("name", mVal.Name);
                        xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                        xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                        //Write our end element
                        xtw.WriteEndElement();
                        break;
                    }
                case mValue.ObjectAttributes.Int32:
                    {
                        //Write our values
                        xtw.WriteStartElement("int");
                        xtw.WriteAttributeString("name", mVal.Name);
                        xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                        xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                        //Write our end element
                        xtw.WriteEndElement();
                        break;
                    }
                case mValue.ObjectAttributes.String32:
                    {
                        //Write our values
                        xtw.WriteStartElement("string32");
                        xtw.WriteAttributeString("name", mVal.Name);
                        xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                        xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                        //Write our end element
                        xtw.WriteEndElement();
                        break;
                    }
                case mValue.ObjectAttributes.String256:
                    {
                        //Write our values
                        xtw.WriteStartElement("string256");
                        xtw.WriteAttributeString("name", mVal.Name);
                        xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                        xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                        //Write our end element
                        xtw.WriteEndElement();
                        break;
                    }
                case mValue.ObjectAttributes.StringID:
                    {
                        //Write our values
                        xtw.WriteStartElement("stringid");
                        xtw.WriteAttributeString("name", mVal.Name);
                        xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                        xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                        //Write our end element
                        xtw.WriteEndElement();
                        break;
                    }
                case mValue.ObjectAttributes.TagRef:
                    {
                        //Write our values
                        xtw.WriteStartElement("tag");
                        xtw.WriteAttributeString("name", mVal.Name);
                        xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                        xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                        //Write our end element
                        xtw.WriteEndElement();
                        break;
                    }
                case mValue.ObjectAttributes.Ident:
                    {
                        //Write our values
                        xtw.WriteStartElement("id");
                        xtw.WriteAttributeString("name", mVal.Name);
                        xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                        xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                        //Write our end element
                        xtw.WriteEndElement();
                        break;
                    }
                case mValue.ObjectAttributes.UInt16:
                    {
                        //Write our values
                        xtw.WriteStartElement("ushort");
                        xtw.WriteAttributeString("name", mVal.Name);
                        xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                        xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                        //Write our end element
                        xtw.WriteEndElement();
                        break;
                    }
                case mValue.ObjectAttributes.UInt32:
                    {
                        //Write our values
                        xtw.WriteStartElement("uint");
                        xtw.WriteAttributeString("name", mVal.Name);
                        xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                        xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                        //Write our end element
                        xtw.WriteEndElement();
                        break;
                    }
                case mValue.ObjectAttributes.Undefined:
                    {
                        //Write our values
                        xtw.WriteStartElement("undefined");
                        xtw.WriteAttributeString("name", mVal.Name);
                        xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                        xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                        //Write our end element
                        xtw.WriteEndElement();
                        break;
                    }
                case mValue.ObjectAttributes.Unicode64:
                    {
                        //Write our values
                        xtw.WriteStartElement("unicode64");
                        xtw.WriteAttributeString("name", mVal.Name);
                        xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                        xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                        //Write our end element
                        xtw.WriteEndElement();
                        break;
                    }
                case mValue.ObjectAttributes.Unicode256:
                    {
                        //Write our values
                        xtw.WriteStartElement("unicode256");
                        xtw.WriteAttributeString("name", mVal.Name);
                        xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                        xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                        //Write our end element
                        xtw.WriteEndElement();
                        break;
                    }
                case mValue.ObjectAttributes.Unused:
                    {
                        //Write our values
                        xtw.WriteStartElement("unused");
                        xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                        xtw.WriteAttributeString("size", ((mUnused)mVal).Size.ToString());
                        //Write our end element
                        xtw.WriteEndElement();
                        break;
                    }
                case mValue.ObjectAttributes.Reflexive:
                    {
                        //Write our values
                        xtw.WriteStartElement("struct");
                        xtw.WriteAttributeString("name", mVal.Name);
                        xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                        xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                        xtw.WriteAttributeString("size", ((mReflexive)mVal).Size.ToString());
                        //Loop for each child
                        foreach (mValue val2 in ((mReflexive)mVal).Values)
                            //Write our node
                            WriteNode(xtw, val2);

                        //Write our end element
                        xtw.WriteEndElement();
                        break;
                    }
            }
        }
    }
}
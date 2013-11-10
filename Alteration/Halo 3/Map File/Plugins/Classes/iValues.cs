using System.Collections.Generic;

namespace Alteration.Halo_3.Values
{
    public class mRevision
    {
        private string _author;
        private string _description;

        private float _version;

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

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }

    public class mReflexive : mValue
    {
        private int _chunkcount;
        private int _memorypointer;
        private int _pointer;
        private int _size;
        private List<mValue> _values;

        public mReflexive()
        {
            Attributes = ObjectAttributes.Reflexive;
            Values = new List<mValue>();
        }

        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public int ChunkCount
        {
            get { return _chunkcount; }
            set { _chunkcount = value; }
        }

        public int MemoryPointer
        {
            get { return _memorypointer; }
            set { _memorypointer = value; }
        }

        public int Pointer
        {
            get { return _pointer; }
            set { _pointer = value; }
        }

        public List<mValue> Values
        {
            get { return _values; }
            set { _values = value; }
        }
    }

    public class mTagRef : mValue
    {
        public mTagRef()
        {
            Attributes = ObjectAttributes.TagRef;
        }
    }

    public class mIdent : mValue
    {
        public mIdent()
        {
            Attributes = ObjectAttributes.Ident;
        }
    }

    public class mStringID : mValue
    {
        public mStringID()
        {
            Attributes = ObjectAttributes.StringID;
        }
    }

    public class mBitmask8 : mBitmask
    {
        public mBitmask8()
        {
            Attributes = ObjectAttributes.Bitmask8;
        }
    }

    public class mBitmask16 : mBitmask
    {
        public mBitmask16()
        {
            Attributes = ObjectAttributes.Bitmask16;
        }
    }

    public class mBitmask32 : mBitmask
    {
        public mBitmask32()
        {
            Attributes = ObjectAttributes.Bitmask32;
        }
    }

    public class mBitmask : mValue
    {
        private List<mBitOption> _options;

        public List<mBitOption> Options
        {
            get { return _options; }
            set { _options = value; }
        }
    }

    public class mBitOption
    {
        private int _bitindex;
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int BitIndex
        {
            get { return _bitindex; }
            set { _bitindex = value; }
        }
    }

    public class mEnum8 : mEnum
    {
        public mEnum8()
        {
            Attributes = ObjectAttributes.Enum8;
        }
    }

    public class mEnum16 : mEnum
    {
        public mEnum16()
        {
            Attributes = ObjectAttributes.Enum16;
        }
    }

    public class mEnum32 : mEnum
    {
        public mEnum32()
        {
            Attributes = ObjectAttributes.Enum32;
        }
    }

    public class mEnum : mValue
    {
        private List<mEnumOption> _options;

        public List<mEnumOption> Options
        {
            get { return _options; }
            set { _options = value; }
        }
    }

    public class mEnumOption
    {
        private string _name;

        private int _value;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    public class mByte : mValue
    {
        public mByte()
        {
            Attributes = ObjectAttributes.Byte;
        }
    }

    public class mInt16 : mValue
    {
        public mInt16()
        {
            Attributes = ObjectAttributes.Int16;
        }
    }

    public class mUInt16 : mValue
    {
        public mUInt16()
        {
            Attributes = ObjectAttributes.UInt16;
        }
    }

    public class mInt32 : mValue
    {
        public mInt32()
        {
            Attributes = ObjectAttributes.Int32;
        }
    }

    public class mUInt32 : mValue
    {
        public mUInt32()
        {
            Attributes = ObjectAttributes.UInt32;
        }
    }

    public class mFloat : mValue
    {
        public mFloat()
        {
            Attributes = ObjectAttributes.Float;
        }
    }

    public class mString32 : mValue
    {
        public mString32()
        {
            Attributes = ObjectAttributes.String32;
        }
    }

    public class mString256 : mValue
    {
        public mString256()
        {
            Attributes = ObjectAttributes.String256;
        }
    }

    public class mUnicode64 : mValue
    {
        public mUnicode64()
        {
            Attributes = ObjectAttributes.Unicode64;
        }
    }

    public class mUnicode256 : mValue
    {
        public mUnicode256()
        {
            Attributes = ObjectAttributes.Unicode256;
        }
    }

    public class mUndefined : mValue
    {
        public mUndefined()
        {
            Attributes = ObjectAttributes.Undefined;
        }
    }

    public class mUnused : mValue
    {
        private int _size;

        public mUnused()
        {
            Attributes = ObjectAttributes.Unused;
        }

        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }
    }

    public class mValue
    {
        #region ObjectAttributes enum

        public enum ObjectAttributes
        {
            Reflexive,
            TagRef,
            Ident,
            StringID,
            Unicode64,
            Unicode256,
            String32,
            String256,
            Bitmask8,
            Bitmask16,
            Bitmask32,
            Float,
            Int16,
            UInt16,
            Int32,
            UInt32,
            Byte,
            Enum8,
            Enum16,
            Enum32,
            Undefined,
            Unused,
            None
        }

        #endregion

        private ObjectAttributes _attributes;

        private string _name;

        private int _offset;

        private bool _visible;

        public ObjectAttributes Attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }

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

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }
    }
}
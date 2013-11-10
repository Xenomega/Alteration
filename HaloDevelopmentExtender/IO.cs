using System;
using System.IO;

namespace HaloDevelopmentExtender
{
    public enum EndianType
    {
        BigEndian,
        LittleEndian
    }

    public class EndianIO
    {

        private readonly bool isfile;
        private bool isOpen;
        private Stream stream;
        private readonly string filepath = "";
        private readonly EndianType endiantype = EndianType.LittleEndian;

        private EndianReader _in;
        private EndianWriter _out;

        public bool Opened { get { return isOpen; } }
        public bool Closed { get { return !isOpen; } }
        public EndianReader In { get { return _in; } }
        public EndianWriter Out { get { return _out; } }
        public Stream Stream { get { return stream; } }

        public EndianIO(string FilePath, EndianType EndianStyle)
        {
            endiantype = EndianStyle;
            filepath = FilePath;
            isfile = true;
        }

        public EndianIO(Stream Stream, EndianType EndianStyle)
        {
            endiantype = EndianStyle;
            stream = Stream;
            isfile = false;
        }
        public EndianIO(byte[] Buffer, EndianType EndianStyle)
        {
            endiantype = EndianStyle;
            stream = new MemoryStream(Buffer);
            isfile = false;
        }

        public void SeekTo(int offset)
        {
            SeekTo(offset, SeekOrigin.Begin);
        }
        public void SeekTo(uint offset)
        {
            SeekTo((int)offset, SeekOrigin.Begin);
        }
        public void SeekTo(int offset, SeekOrigin SeekOrigin)
        {
            stream.Seek(offset, SeekOrigin);
        }

        public void Open()
        {
            if (isOpen)
                return;

            if (isfile)
                stream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            _in = new EndianReader(stream, endiantype);
            _out = new EndianWriter(stream, endiantype);

            isOpen = true;
        }
        public void Close()
        {
            if (isOpen == false)
                return;

            stream.Close();
            _in.Close();
            _out.Close();

            isOpen = false;
        }

    }

    public class EndianReader : BinaryReader
    {

        public EndianType endianstyle;

        public EndianReader(Stream stream, EndianType endianstyle)
            : base(stream)
        {
            this.endianstyle = endianstyle;
        }

        public void SeekTo(int offset)
        {
            SeekTo(offset, SeekOrigin.Begin);
        }

        public void SeekTo(uint offset)
        {
            SeekTo((int)offset, SeekOrigin.Begin);
        }

        public void SeekTo(long offset)
        {
            SeekTo((int)offset, SeekOrigin.Begin);
        }

        public void SeekTo(int offset, SeekOrigin SeekOrigin)
        {
            BaseStream.Seek(offset, SeekOrigin);
        }

        public override short ReadInt16()
        {
            return ReadInt16(endianstyle);
        }
        public short ReadInt16(EndianType EndianType)
        {
            byte[] buffer = base.ReadBytes(2);

            if (EndianType == EndianType.BigEndian)
                Array.Reverse(buffer);

            return BitConverter.ToInt16(buffer, 0);
        }

        public override ushort ReadUInt16()
        {
            return ReadUInt16(endianstyle);
        }
        public ushort ReadUInt16(EndianType EndianType)
        {
            byte[] buffer = base.ReadBytes(2);

            if (EndianType == EndianType.BigEndian)
                Array.Reverse(buffer);

            return BitConverter.ToUInt16(buffer, 0);
        }

        public override int ReadInt32()
        {
            return ReadInt32(endianstyle);
        }
        public int ReadInt32(EndianType EndianType)
        {
            byte[] buffer = base.ReadBytes(4);

            if (EndianType == EndianType.BigEndian)
                Array.Reverse(buffer);

            return BitConverter.ToInt32(buffer, 0);
        }

        public override uint ReadUInt32()
        {
            return ReadUInt32(endianstyle);
        }
        public uint ReadUInt32(EndianType EndianType)
        {
            byte[] buffer = base.ReadBytes(4);

            if (EndianType == EndianType.BigEndian)
                Array.Reverse(buffer);

            return BitConverter.ToUInt32(buffer, 0);
        }

        public override long ReadInt64()
        {
            return ReadInt64(endianstyle);
        }
        public long ReadInt64(EndianType EndianType)
        {
            byte[] buffer = base.ReadBytes(8);

            if (EndianType == EndianType.BigEndian)
                Array.Reverse(buffer);

            return BitConverter.ToInt64(buffer, 0);
        }

        public override ulong ReadUInt64()
        {
            return ReadUInt64(endianstyle);
        }
        public ulong ReadUInt64(EndianType EndianType)
        {
            byte[] buffer = base.ReadBytes(8);

            if (EndianType == EndianType.BigEndian)
                Array.Reverse(buffer);

            return BitConverter.ToUInt64(buffer, 0);
        }

        public override float ReadSingle()
        {
            return ReadSingle(endianstyle);
        }
        public float ReadSingle(EndianType EndianType)
        {
            byte[] buffer = base.ReadBytes(4);

            if (EndianType == EndianType.BigEndian)
                Array.Reverse(buffer);

            return BitConverter.ToSingle(buffer, 0);
        }

        public override double ReadDouble()
        {
            return ReadDouble(endianstyle);
        }
        public double ReadDouble(EndianType EndianType)
        {
            byte[] buffer = base.ReadBytes(4);

            if (EndianType == EndianType.BigEndian)
                Array.Reverse(buffer);

            return BitConverter.ToDouble(buffer, 0);
        }

        public string ReadNullTerminatedString()
        {
            string newString = "";
            char temp;
            while ((temp = ReadChar()) != '\0')
            {
                if (temp != '\0') newString += temp;
                else break;
            }
            return newString;
        }

        public string ReadAsciiString(int Length)
        {
            return ReadAsciiString(Length, endianstyle);
        }
        public string ReadAsciiString(int Length, EndianType EndianType)
        {
            string newString = "";
            int howMuch = 0;
            for (int x = 0; x < Length; x++)
            {
                char tempChar = (char)ReadByte();
                howMuch++;
                if (tempChar != '\0')
                    newString += tempChar;
                else
                    break;
            }

            int size = (Length - howMuch) * sizeof(byte);
            BaseStream.Seek(size, SeekOrigin.Current);

            return newString;
        }

        public string ReadUnicodeString(int Length)
        {
            return ReadUnicodeString(Length, endianstyle);
        }
        public string ReadUnicodeString(int Length, EndianType EndianType)
        {
            string newString = "";
            int howMuch = 0;
            for (int x = 0; x < Length; x++)
            {
                char tempChar = (char)ReadUInt16(EndianType);
                howMuch++;
                if (tempChar != '\0')
                    newString += tempChar;
                else
                    break;
            }

            int size = (Length - howMuch) * sizeof(UInt16);
            BaseStream.Seek(size, SeekOrigin.Current);

            return newString;
        }

        public override string ReadString()
        {
            string newString = "";
            int howMuch = 0;
            while (true)
            {
                char tempChar = (char)ReadByte();
                howMuch++;
                if (tempChar != '\0')
                    newString += tempChar;
                else
                    break;
            }

            int size = (newString.Length - howMuch) * sizeof(byte);
            BaseStream.Seek(size + 1, SeekOrigin.Current);

            return newString;
        }
        public string ReadString(int Length)
        {
            return ReadAsciiString(Length);
        }

        public int ReadInt24()
        {
            return ReadInt24(endianstyle);
        }
        public int ReadInt24(EndianType EndianType)
        {
            byte[] buffer = base.ReadBytes(3);
            byte[] dest = new byte[0x04];

            Array.Copy(buffer, 0, dest, 0, 3);

            if (EndianType == EndianType.BigEndian)
                Array.Reverse(dest);

            return BitConverter.ToInt32(dest, 0);
        }

    }

    public class EndianWriter : BinaryWriter
    {
        readonly EndianType endianstyle;

        public EndianWriter(Stream stream, EndianType endianstyle)
            : base(stream)
        {
            this.endianstyle = endianstyle;
        }

        public void SeekTo(int offset)
        {
            SeekTo(offset, SeekOrigin.Begin);
        }

        public void SeekTo(uint offset)
        {
            SeekTo((int)offset, SeekOrigin.Begin);
        }

        public void SeekTo(long offset)
        {
            SeekTo((int)offset, SeekOrigin.Begin);
        }

        public void SeekTo(int offset, SeekOrigin SeekOrigin)
        {
            BaseStream.Seek(offset, SeekOrigin);
        }

        public override void Write(short value)
        {
            Write(value, endianstyle);
        }
        public void Write(short value, EndianType EndianType)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            if (EndianType == EndianType.BigEndian)
                Array.Reverse(buffer);

            base.Write(buffer);
        }

        public override void Write(ushort value)
        {
            Write(value, endianstyle);
        }
        public void Write(ushort value, EndianType EndianType)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            if (EndianType == EndianType.BigEndian)
                Array.Reverse(buffer);

            base.Write(buffer);
        }

        public override void Write(int value)
        {
            Write(value, endianstyle);
        }
        public void Write(int value, EndianType EndianType)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            if (EndianType == EndianType.BigEndian)
                Array.Reverse(buffer);

            base.Write(buffer);
        }

        public override void Write(uint value)
        {
            Write(value, endianstyle);
        }
        public void Write(uint value, EndianType EndianType)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            if (EndianType == EndianType.BigEndian)
                Array.Reverse(buffer);

            base.Write(buffer);
        }

        public override void Write(long value)
        {
            Write(value, endianstyle);
        }
        public void Write(long value, EndianType EndianType)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            if (EndianType == EndianType.BigEndian)
                Array.Reverse(buffer);

            base.Write(buffer);
        }

        public override void Write(ulong value)
        {
            Write(value, endianstyle);
        }
        public void Write(ulong value, EndianType EndianType)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            if (EndianType == EndianType.BigEndian)
                Array.Reverse(buffer);

            base.Write(buffer);
        }

        public override void Write(float value)
        {
            Write(value, endianstyle);
        }
        public void Write(float value, EndianType EndianType)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            if (EndianType == EndianType.BigEndian)
                Array.Reverse(buffer);

            base.Write(buffer);
        }

        public override void Write(double value)
        {
            Write(value, endianstyle);
        }
        public void Write(double value, EndianType EndianType)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            if (EndianType == EndianType.BigEndian)
                Array.Reverse(buffer);

            base.Write(buffer);
        }

        public void WriteAsciiString(string String, int Length)
        {
            WriteAsciiString(String, Length, endianstyle);
        }
        public void WriteAsciiString(string String, int Length, EndianType EndianType)
        {
            int strLen = String.Length;
            for (int x = 0; x < strLen; x++)
            {
                if (x > Length)
                    break;//just incase they pass a huge string

                byte val = (byte)String[x];
                Write(val);
            }

            int nullSize = (Length - strLen) * sizeof(byte);
            if (nullSize > 0)
                Write(new byte[nullSize]);
        }

        public void WriteUnicodeString(string String, int Length)
        {
            WriteUnicodeString(String, Length, endianstyle);
        }
        public void WriteUnicodeString(string String, int Length, EndianType EndianType)
        {
            int strLen = String.Length;
            for (int x = 0; x < strLen; x++)
            {
                if (x > Length)
                    break;//just incase they pass a huge string

                ushort val = String[x];
                Write(val, EndianType);
            }

            int nullSize = (Length - strLen) * sizeof(ushort);
            if (nullSize > 0)
                Write(new byte[nullSize]);
        }

    }
}
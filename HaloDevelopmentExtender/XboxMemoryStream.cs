using System;

namespace HaloDevelopmentExtender
{
    public class XboxMemoryStream : System.IO.Stream
    {

        #region Position
        protected uint position;
        /// <summary>
        /// 
        /// </summary>
        public override long Position
        {
            get { return position; }
            set { position = (uint)value; }
        }
        #endregion

        readonly XDevkit.IXboxDebugTarget target;

        /// <summary>
        /// Creates a new stream using a client connection to a debug xbox
        /// </summary>
        /// <param name="Target">Connection to use</param>
        /// <remarks>
        /// Defaults the position to the address of the title id.
        /// </remarks>
        public XboxMemoryStream(XDevkit.IXboxDebugTarget Target)
        {
            target = Target;
            position = 0x10114U;
        }

        public override bool CanRead { get { return true; } }

        public override bool CanSeek { get { return true; } }

        public override bool CanWrite { get { return true; } }

        public override void Flush() { /*throw new Exception("The method or operation is not implemented.");*/ }

        public override long Length
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {

            uint read;
            if (offset == 0)
            {
                target.GetMemory(position, (uint)count, buffer, out read);
            }
            else
            {
                byte[] temp = new byte[count];
                target.GetMemory(position, (uint)count, temp, out read);
                temp.CopyTo(buffer, offset);
            }
            position += read;
            return (int)read;

        }

        /// <summary>
        /// Seeks to a point in memory
        /// </summary>
        /// <param name="offset">Offset in memory</param>
        /// <param name="origin">origin to perform the seek</param>
        /// <returns>new position in memory</returns>
        /// <remarks>
        /// If you pass <c>System.IO.SeekOrigin.End</c> then it will
        /// subtract <paramref name="offset"/> from the current position
        /// (due to the explicit use of unsigned numbers)
        /// </remarks>
        public override long Seek(long offset, System.IO.SeekOrigin origin)
        {

            switch (origin)
            {
                case System.IO.SeekOrigin.Begin:
                    return position = (uint)offset;

                case System.IO.SeekOrigin.Current:
                    return position += (uint)offset;

                case System.IO.SeekOrigin.End:
                    return position -= (uint)offset;

                default:
                    throw new Exception("Invalid SeekOrigin.");
            }

        }

        public override void SetLength(long value)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            uint bytesWritten;
            target.SetMemory(position, (uint)count, buffer, out bytesWritten);
            position += bytesWritten;
        }

    }
}
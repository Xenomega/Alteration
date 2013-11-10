using System;
using System.Collections.Generic;
using System.Text;

namespace Alteration.Halo_3.Map_File.Meta_Editor.Chunk_Clipboard
{
    /// <summary>
    /// Our class to contain chunk data we copy and paste.
    /// </summary>
    public class ChunkClipboard
    {
        private static string _name;
        /// <summary>
        /// The name of the chunk/reflexive.
        /// </summary>
        public static string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private static byte[] _chunk;
        /// <summary>
        /// The data of the chunk.
        /// </summary>
        public static byte[] Chunk
        {
            get { return _chunk; }
            set { _chunk = value; }
        }

        private static int _size;
        /// <summary>
        /// The size of each chunk.
        /// </summary>
        public static int Size
        {
            get { return _size; }
            set { _size = value; }
        }
    }
}

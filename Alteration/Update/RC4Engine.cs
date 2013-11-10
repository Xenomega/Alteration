/* Title    : RC4Engine.cs
 * Author   : Simone Spagna
 * Purpose  : Encrypt/Decrypt strings using RC4 Alghoritm
 * */
using System.Text;

namespace Alteration.Update
{
    /// <summary>
    /// Summary description for RC4Engine.
    /// </summary>
    public abstract class RC4Engine
    {
        #region Constructor

        #endregion

        #region Public Method

        /// <summary>
        /// Encript InClear Text using RC4 method using EncriptionKey
        /// Put the result into CryptedText 
        /// </summary>
        /// <returns>true if success, else false</returns>
        public static byte[] Encrypt(byte[] data)
        {
            //
            // indexes used below
            //
            long i = 0;
            long j = 0;

            //
            // Put input string in temporary byte array
            //
            byte[] input = data;

            // 
            // Output byte array
            //
            byte[] output = new byte[input.Length];

            //
            // Local copy of m_nBoxLen
            //
            byte[] n_LocBox = new byte[m_nBoxLen];
            m_nBox.CopyTo(n_LocBox, 0);

            //
            // Run Alghoritm
            //
            for (long offset = 0; offset < input.Length; offset++)
            {
                i = (i + 1) % m_nBoxLen;
                j = (j + n_LocBox[i]) % m_nBoxLen;
                byte temp = n_LocBox[i];
                n_LocBox[i] = n_LocBox[j];
                n_LocBox[j] = temp;
                byte a = input[offset];
                byte b = n_LocBox[(n_LocBox[i] + n_LocBox[j]) % m_nBoxLen];
                output[offset] = (byte)(a ^ b);
            }

            //
            // Put result into output string ( CryptedText )
            //
            return output;

        }

        /// <summary>
        /// Decript CryptedText into InClearText using EncriptionKey
        /// </summary>
        /// <returns>true if success else false</returns>
        public static byte[] Decrypt(byte[] data)
        {
            return Encrypt(data);
        }

        public static string Encrypt(string Key, string Data)
        {
            //Set our key
            EncryptionKey = Key;

            //Now lets get our data
            byte[] bdata = Encoding.ASCII.GetBytes(Data);

            //Now lets encrypt and return
            return Encoding.ASCII.GetString(Encrypt(bdata));
        }

        public static string Decrypt(string Key, string Data)
        {
            return Encrypt(Key, Data);
        }

        #endregion

        #region Prop definitions
        /// <summary>
        /// Get or set Encryption Key
        /// </summary>
        public static string EncryptionKey
        {
            get
            {
                return (m_sEncryptionKey);
            }
            set
            {
                //
                // assign value only if it is a new value
                //
                if (m_sEncryptionKey != value)
                {
                    m_sEncryptionKey = value;

                    //
                    // Used to populate m_nBox
                    //
                    long index2 = 0;

                    //
                    // Create two different encoding 
                    //
                    Encoding ascii = Encoding.ASCII;
                    Encoding unicode = Encoding.Unicode;

                    //
                    // Perform the conversion of the encryption key from unicode to ansi
                    //
                    byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicode.GetBytes(m_sEncryptionKey));

                    //
                    // Convert the new byte[] into a char[] and then to string
                    //

                    char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
                    ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);

                    //
                    // Populate m_nBox
                    //
                    long KeyLen = m_sEncryptionKey.Length;

                    //
                    // First Loop
                    //
                    m_nBox = new byte[m_nBoxLen];
                    for (long count = 0; count < m_nBoxLen; count++)
                    {
                        m_nBox[count] = (byte)count;
                    }

                    //
                    // Second Loop
                    //
                    for (long count = 0; count < m_nBoxLen; count++)
                    {
                        index2 = (index2 + m_nBox[count] + asciiChars[count % KeyLen]) % m_nBoxLen;
                        byte temp = m_nBox[count];
                        m_nBox[count] = m_nBox[index2];
                        m_nBox[index2] = temp;
                    }

                }
            }
        }
        public static byte[] BinaryEncryptionKey
        {
            set
            {
                int KeyLen = value.Length;

                m_nBox = new byte[m_nBoxLen];
                for (int count = 0; count < m_nBoxLen; count++)
                    m_nBox[count] = (byte)count;

                int index2 = 0;
                for (int count = 0; count < m_nBoxLen; count++)
                {
                    index2 = (index2 + m_nBox[count] + value[count % KeyLen]) % (int)m_nBoxLen;
                    byte temp = m_nBox[count];
                    m_nBox[count] = m_nBox[index2];
                    m_nBox[index2] = temp;
                }
            }
        }
        #endregion

        #region Private Fields

        //
        // Encryption Key  - Unicode & Ascii version
        //
        private static string m_sEncryptionKey = "";
        //
        // It is related to Encryption Key
        //
        protected static byte[] m_nBox = new byte[m_nBoxLen];
        //
        // Len of nBox
        //
        static public long m_nBoxLen = 255;

        #endregion

    }
}
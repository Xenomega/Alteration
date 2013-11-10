using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using HaloDeveloper.Helpers;
using HaloDeveloper.IO;
using System.Collections.Specialized;
using System.Xml;
using System.Collections.Generic;
using System.Threading;
using Alteration.Details;

namespace Alteration.Security
{
    public abstract class Security
    {


        public static string GetUserKey()
        {
            return ExtraFunctions.BytesToHexString(GetHash(GetOperatingSystemInfo()));
        }

        public static byte[] GetUserKeyBin()
        {
            return GetHash(GetOperatingSystemInfo());
        }


        private static string GetOperatingSystemInfo()
        {
            //Initialize our string with our information
            string operatingInfo = "";
            //Add our machine name
            operatingInfo += Environment.MachineName + Environment.NewLine;
            //Add our processor count
            operatingInfo += Environment.ProcessorCount + Environment.NewLine;
            //Add our system directory
            operatingInfo += Environment.SystemDirectory + Environment.NewLine;
            //Add our OS version
            operatingInfo += Environment.OSVersion.Version + Environment.NewLine;
            //Add our username
            operatingInfo += Environment.UserName + Environment.NewLine;
            //Return the information
            return operatingInfo;
        }

        private static byte[] GetHash(string str)
        {
            //create our hasher
            SHA1 sha1 = SHA1.Create();
            //Get our hash
            byte[] hash = sha1.ComputeHash(Encoding.ASCII.GetBytes(str));
            //Loop through the bytes
            for (int i = 0; i < hash.Length - 1; i++)
            {
                //Xor it with the next byte
                hash[i] ^= hash[i + 1];
            }
            //Return the hash
            return hash;
        }

    }
}
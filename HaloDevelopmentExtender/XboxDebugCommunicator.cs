using System;
using System.Collections.Generic;
using System.IO;

namespace HaloDevelopmentExtender
{
    /// <summary>
    /// This class, when initialized, may perform many function for Xbox 360 Development Kits.
    /// This class was created by Xenon.7 & Anthony.
    /// </summary>
    public class XboxDebugCommunicator
    {
        #region Values
        public const float Version = 1.0f;

        private bool _connected;
        public bool Connected
        {
            get { return _connected; }
            set { _connected = value; }
        }
        private uint _connectioncode;
        public uint Connection_Code
        {
            get { return _connectioncode; }
            set { _connectioncode = value; }
        }
        private string _xboxname;
        public string XboxName
        {
            get { return _xboxname; }
            set { _xboxname = value; }
        }
        private XDevkit.XboxConsole _xboxconsole;
        private XDevkit.XboxConsole Xbox_Console
        {
            get { return _xboxconsole; }
            set { _xboxconsole = value; }
        }
        private XDevkit.XboxManagerClass _xboxmanager;
        private XDevkit.XboxManagerClass Xbox_Manager
        {
            get { return _xboxmanager; }
            set { _xboxmanager = value; }
        }
        private FileSystem _filesystem;
        public FileSystem File_System
        {
            get { return _filesystem; }
            set { _filesystem = value; }
        }
        private string _xboxtype;
        public string Xbox_Type
        {
            get { return _xboxtype; }
            set { _xboxtype = value; }
        }

        #endregion
        #region Initialization
        public XboxDebugCommunicator(string xboxName)
        {
            //Set our XboxName instance.
            XboxName = xboxName;
        }
        #endregion
        #region Functions
        /// <summary>
        /// This function, when called upon, will connect to the provided Xbox Console, making it ready to initialize other functions.
        /// </summary>
        public void Connect()
        {
            //If we are not connected..
            if (!Connected)
            {
                //Initialize a new Xbox_Manager
                Xbox_Manager = new XDevkit.XboxManagerClass();
                //Initialize a new Xbox Console by Opening it with our Xbox Manager
                Xbox_Console = Xbox_Manager.OpenConsole(XboxName);
                //Open our connection to the xbox, set our connection code.
                Connection_Code = Xbox_Console.OpenConnection(null);


                try
                {
                    //Assign our Kit type
                    Xbox_Type = Xbox_Console.ConsoleType.ToString();
                }
                catch { }
                //Initialize our FileSystem class
                File_System = new FileSystem(Xbox_Console);
                //Set our bool indicating we are now connected
                Connected = true;
            }
        }
        /// <summary>
        /// This function, when initialized, will disconnect from the connected xbox.
        /// </summary>
        public void Disconnect()
        {
            //If we are connected
            if (Connected)
            {
                //Send our text command
                SendTextCommand("bye");
                //Close our connection with our connection code
                Xbox_Console.CloseConnection(Connection_Code);
                //Close our xbdm connection.
                //Xbdm.CloseConnection(dmSession);
                //Null our Xbox FileSystem
                File_System = null;
                //Set our bool as disconnected.
                Connected = false;
            }
        }

        /// <summary>
        /// Our function that will reload the Xbox FileSystem class.
        /// </summary>
        public void RefreshFilesystem()
        {
            //Initialize our FileSystem class
            File_System = new FileSystem(Xbox_Console);
        }

        /// <summary>
        /// This function, when called upon, will send a text command to the Xbox, and the Xbox will process this command, and send a response back.
        /// </summary>
        /// <param name="Command">The command to use.</param>
        /// <returns>Returns the response.</returns>
        public string SendTextCommand(string Command)
        {
            //Initialize our response string.
            string Response = "";

                //Send the text command and retrieve the response.
                Xbox_Console.SendTextCommand(Connection_Code, Command, out Response);

                if (Response.Contains("202") | Response.Contains("203"))
                {
                    try
                    {
                        //Loop.
                        while (true)
                        {
                            //Create our temporary string.
                            string tmp = "";

                            //Receive our line of text.
                            Xbox_Console.ReceiveSocketLine(Connection_Code, out tmp);

                            //If  the line isn't blank.
                            if (tmp.Length > 0)
                            {
                                //If the first character isn't .
                                if (tmp[0] != '.')
                                {
                                    //Add it to our response.
                                    Response += Environment.NewLine + tmp;
                                }
                                //Otherwise.
                                else
                                {
                                    //Break.
                                    break;
                                }
                            }
                        }
                    }
                    catch { }
                }

            //Return the response
            return Response;
        }

        /// <summary>
        /// This function, when called upon, will return the Xbox Memory Stream, which is read-write access, allowing editting of Xbox Memory.
        /// </summary>
        /// <returns></returns>
        public XboxMemoryStream ReturnXboxMemoryStream()
        {
            //Return our newely initialized XboxMemoryStream
            return new XboxMemoryStream(Xbox_Console.DebugTarget);
        }

        /// <summary>
        /// This command, when called upon, will freeze the Xbox, until unfrozen by the Unfreeze() function.
        /// </summary>
        public void Freeze()
        {
            //Send the stop processing command.
            SendTextCommand("stop");
        }

        /// <summary>
        /// This function, when called upon, will resume Xbox Processes when the Freeze() command has stopped them.
        /// </summary>
        public void Unfreeze()
        {
            //Send the continue processing command.
            SendTextCommand("go");
        }
        /// <summary>
        /// This function, when called upon, will reboot the Xbox Development Console.
        /// </summary>
        public void Reboot(RebootFlags reboottype)
        {
            //Determine our reboot type.
            switch (reboottype)
            {
                case RebootFlags.Warm:
                    {
                        SendTextCommand("reboot");
                        break;
                    }
                case RebootFlags.Cold:
                    {
                        SendTextCommand("reboot");
                        break;
                    }
            }
            //If we're cold booting.
            if (reboottype == RebootFlags.Cold)
            {
                //And try to disconnect.
                try { Disconnect(); }
                catch
                {
                    Console.WriteLine("Error Disconnecting");
                }
            }
        }

        public enum RebootFlags
        {
            Warm,
            Cold
        }

        /// <summary>
        /// This function, when called upon, will capture a .dds screencapture from your Xbox Development Kit and save it to the specified location
        /// </summary>
        /// <param name="savePath"></param>
        public void Screenshot(string savePath)
        {
            //Capture our screenshot
            Xbox_Console.ScreenShot(savePath);
        }
        #endregion
        #region Classes
        /// <summary>
        /// This class, when initialized, can be used to read the Xbox 360 FileSystem.
        /// </summary>
        public class FileSystem
        {
            #region Values
            private XDevkit.XboxConsole _xboxconsole;
            public XDevkit.XboxConsole Xbox_Console
            {
                get { return _xboxconsole; }
                set { _xboxconsole = value; }
            }
            private string[] _drives;
            public string[] Drives
            {
                get { return _drives; }
                set { _drives = value; }
            }
            #endregion
            #region Initialization
            public FileSystem(XDevkit.XboxConsole xboxConsole)
            {
                //Set our Xbox Console Instance.
                Xbox_Console = xboxConsole;

                //Obtain our drives as a single string
                string drivesString = Xbox_Console.Drives;
                //Split the string to obtain drives as an array.
                Drives = drivesString.Split(',');
            }
            #endregion
            #region Functions
            /// <summary>
            /// This function, when called upon, will return all files in a directory with their full filename.
            /// </summary>
            /// <param name="directory"></param>
            /// <returns></returns>
            public string[] GetFiles(string directory)
            {
                //Obtain our Xbox Files and Directories.
                XDevkit.IXboxFiles xboxFiles = Xbox_Console.DirectoryFiles(directory);
                //Initialize a string list.
                List<string> fileNames = new List<string>();
                //Loop through all the "xboxFiles"
                foreach (XDevkit.IXboxFile xboxFile in xboxFiles)
                {
                    //If the file is not a directory
                    if (!xboxFile.IsDirectory)
                    {
                        //Add it to the list
                        fileNames.Add(xboxFile.Name);
                    }
                }
                //Return the list as a string[]
                return fileNames.ToArray();
            }
            /// <summary>
            ///  This function, when called upon, will return all directories in a directory with their full filename.
            /// </summary>
            /// <param name="directory"></param>
            /// <returns></returns>
            public string[] GetDirectories(string directory)
            {
                //Obtain our Xbox Files and Directories.
                XDevkit.IXboxFiles xboxFiles = Xbox_Console.DirectoryFiles(directory);
                //Initialize a string list.
                List<string> directoryNames = new List<string>();
                //Loop through all the "xboxFiles"
                foreach (XDevkit.IXboxFile xboxFile in xboxFiles)
                {
                    //If the file is a directory
                    if (xboxFile.IsDirectory)
                    {
                        //Add it to the list
                        directoryNames.Add(xboxFile.Name);
                    }
                }
                //Return the list as a string[]
                return directoryNames.ToArray();
            }
            /// <summary>
            ///  This function, when called upon, will upload a file from your local harddrive, to the remote(Xbox) harddrive.
            /// </summary>
            public void UploadFile(string localName, string remoteName)
            {
                //Upload our file
                Xbox_Console.SendFile(localName, remoteName);
            }
            /// <summary>
            /// This function, when called upon, will rename a file on the Xbox 360 Development Kit
            /// </summary>
            /// <param name="remoteName"></param>
            /// <param name="newRemoteName"></param>
            public void RenameFile(string remoteName, string newRemoteName)
            {
                //Rename our file to the new filename
                Xbox_Console.RenameFile(remoteName, newRemoteName);
            }
            /// <summary>
            /// This function, when called upon, will delete a file at the remote location.
            /// </summary>
            /// <param name="remoteName"></param>
            public void DeleteFile(string remoteName)
            {
                //Delete the file at the remote name.
                Xbox_Console.DeleteFile(remoteName);
            }
            /// <summary>
            /// This function, when called upon, will remove a directory at the path given.
            /// </summary>
            /// <param name="directory"></param>
            public void DeleteDirectory(string directory)
            {
                //Get our list of files for this folder
                string[] files = GetFiles(directory);
                //For each file in this folder
                foreach (string file in files)
                {
                    //Delete the file.
                    DeleteFile(file);
                }

                //Get our list of directories for this folder
                string[] folders = GetDirectories(directory);
                //For each folder in this folder
                foreach (string folder in folders)
                {
                    //Delete the folder
                    DeleteDirectory(folder);
                }

                //Remove the directory.
                Xbox_Console.RemoveDirectory(directory);
            }
            /// <summary>
            /// This function, when called upon, will create the directory specified.
            /// </summary>
            /// <param name="directory"></param>
            public void CreateDirectory(string directory)
            {
                //Make the directory specified.
                Xbox_Console.MakeDirectory(directory);
            }
            /// <summary>
            ///  This function, when called upon, will download a file to your local harddrive, from the remote(Xbox) harddrive.
            /// </summary>
            public void DownloadFile(string localName, string remoteName)
            {
                //Download our file
                Xbox_Console.ReceiveFile(localName, remoteName);
            }
            /// <summary>
            /// This function will download a directory from the Xbox.
            /// </summary>
            /// <param name="localFolderToSaveIn">The local folder to save the remote folder in.</param>
            /// <param name="remoteFolderPath">The remote folder to download.</param>
            public void DownloadDirectory(string localFolderToSaveIn, string remoteFolderPath)
            {
                //Get our files and directories
                string[] fileList = GetFiles(remoteFolderPath);
                string[] folderList = GetDirectories(remoteFolderPath);
                
                //If the last character is \\ remove it.
                if (remoteFolderPath[remoteFolderPath.Length - 1] == '\\')
                    remoteFolderPath = remoteFolderPath.Substring(0, remoteFolderPath.Length - 1);
                if (localFolderToSaveIn[localFolderToSaveIn.Length - 1] == '\\')
                    localFolderToSaveIn = localFolderToSaveIn.Substring(0, localFolderToSaveIn.Length - 1);

                //Create our temporary string
                string[] temporaryString = remoteFolderPath.Split('\\');

                //Get our short folder name
                string shortFolderName = temporaryString[temporaryString.Length - 1];

                //Get our local folder path.
                string localFolderPath = localFolderToSaveIn + "\\" + shortFolderName + "\\";
                
                //If our folder doesn't exist, create it
                if (!Directory.Exists(localFolderPath))
                {
                    //Create it.
                    Directory.CreateDirectory(localFolderPath);
                }

                //Loop for each file
                foreach (string file in fileList)
                {
                    //Create our temporary string
                    temporaryString = file.Split('\\');

                    //Get our short filename
                    string shortFileName = temporaryString[temporaryString.Length - 1];

                    //Download it.
                    DownloadFile(localFolderPath + shortFileName, file);
                }

                //Loop for each folder
                foreach (string directory in folderList)
                {
                    //Create our temporary string
                    temporaryString = directory.Split('\\');

                    //Get our short filename
                    string shortFileName = temporaryString[temporaryString.Length - 1];

                    //Download the directory
                    DownloadDirectory(localFolderPath,directory);
                }
            }
            /// <summary>
            /// This function when called upon will upload a folder to the Xbox 360 Development Console.
            /// </summary>
            /// <param name="localFolder">The local folder to upload.</param>
            /// <param name="remoteFolderToSaveIn">The remote folder to upload to.</param>
            public void UploadDirectory(string localFolder, string remoteFolderToSaveIn)
            {
                //Get our files and directories
                string[] fileList = Directory.GetFiles(localFolder);
                string[] folderList = Directory.GetDirectories(localFolder);

                //If the last character is \\ remove it.
                if (localFolder[localFolder.Length - 1] == '\\')
                    localFolder = localFolder.Substring(0, localFolder.Length - 1);
                if (remoteFolderToSaveIn[remoteFolderToSaveIn.Length - 1] == '\\')
                    remoteFolderToSaveIn = remoteFolderToSaveIn.Substring(0, remoteFolderToSaveIn.Length - 1);

                //Create our temporary string
                string[] temporaryString = localFolder.Split('\\');

                //Get our short folder name
                string shortFolderName = temporaryString[temporaryString.Length - 1];

                //Get our local folder path.
                string remoteFolderPath = remoteFolderToSaveIn + "\\" + shortFolderName + "\\";

                //Create our folder
                CreateDirectory(remoteFolderPath);

                //Loop for each file
                foreach (string file in fileList)
                {
                    //Create our temporary string
                    temporaryString = file.Split('\\');

                    //Get our short filename
                    string shortFileName = temporaryString[temporaryString.Length - 1];

                    //Upload it.
                    UploadFile(file, remoteFolderPath + shortFileName);
                }

                //Loop for each folder
                foreach (string directory in folderList)
                {
                    //Create our temporary string
                    temporaryString = directory.Split('\\');

                    //Get our short filename
                    string shortFileName = temporaryString[temporaryString.Length - 1];

                    //Download the directory
                    UploadDirectory(directory, remoteFolderPath);
                }
            }
            #endregion
        }
        #endregion
    }
}
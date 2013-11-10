using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Alteration.Forms.Dialog;
using HaloDevelopmentExtender;
using Alteration.Settings;
using System.IO;
using Alteration.Halo_3;
using System.Threading;
using Alteration.Forms.Xbox_Debug_Communicator.Dialog.File_System;

namespace Alteration.Forms.Xbox_Debug_Communicator
{
    public partial class XboxDebugCommunicatorForm : Form
    {

        private XboxDebugCommunicator _xboxdebugcommunicator;
        /// <summary>
        /// Our class to communicate with the Xbox 360 Developers console.
        /// </summary>
        public XboxDebugCommunicator Xbox_Debug_Communicator
        {
            get { return _xboxdebugcommunicator; }
            set { _xboxdebugcommunicator = value; }
        }


        public XboxDebugCommunicatorForm()
        {
            InitializeComponent();
            comboBoxEx1.SelectedIndex = 0;
            AddCommandsToComboBox();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            //Initialize our screenshot form
            ScreenshotForm ssf = new ScreenshotForm();

            //Set it's mdi parent
            ssf.MdiParent = this.MdiParent;

            //Show it
            ssf.Show();
        }


        private void buttonX1_Click(object sender, EventArgs e)
        {
            //If the Xbox_Debug_Communicator isn't null
            if (Xbox_Debug_Communicator != null)
                //If it is connected..
                if (Xbox_Debug_Communicator.Connected)
                {
                    //Show our message
                    MessageBox.Show("You are already connected.");

                    //Stop processing code
                    return;
                }

            //If the XDK Name isn't blank
            if (AppSettings.Settings.XDKName != "")
            {
                //Initialize our Xbox Debug Communicator, with our Xbox Name
                Xbox_Debug_Communicator = new XboxDebugCommunicator(AppSettings.Settings.XDKName);
                //Connect
                Xbox_Debug_Communicator.Connect();

                //Load our filesystem drives
                LoadFileSystemDrives();

                //Show our done message.
                MessageBox.Show("Connected.");
            }
            else
            {
                MessageBox.Show(
                    "The XDK Name is not set in the settings. Please click the button on the right to show the settings and indicate your IP/Name for your Xenon Development Kit.");
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            //If the Xbox_Debug_Communicator isn't null
            if (Xbox_Debug_Communicator != null)
                //If it is connected..
                if (Xbox_Debug_Communicator.Connected)
                {
                    //Disconnect
                    Xbox_Debug_Communicator.Disconnect();
                }

            //Null our communicator
            Xbox_Debug_Communicator = null;

            //Clear our treeview & listview
            treeView1.Nodes.Clear();
            listView1.Items.Clear();

            //Show our disconnected message
            MessageBox.Show("Disconnected.");
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            //If the Xbox_Debug_Communicator isn't null
            if (Xbox_Debug_Communicator != null)
                //If it is connected..
                if (Xbox_Debug_Communicator.Connected)
                {
                    //Freeze our xbox
                    Xbox_Debug_Communicator.Freeze();

                    //Return
                    return;
                }

            //Show our message
            MessageBox.Show("You are not connected.");
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            //If the Xbox_Debug_Communicator isn't null
            if (Xbox_Debug_Communicator != null)
                //If it is connected..
                if (Xbox_Debug_Communicator.Connected)
                {
                    //Freeze our xbox
                    Xbox_Debug_Communicator.Unfreeze();

                    //Return
                    return;
                }

            //Show our message
            MessageBox.Show("You are not connected.");
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            //If the Xbox_Debug_Communicator isn't null
            if (Xbox_Debug_Communicator != null)
                //If it is connected..
                if (Xbox_Debug_Communicator.Connected)
                {
                    //Switch through our choices
                    switch (comboBoxEx1.SelectedItem.ToString())
                    {

                        case "Warm":
                            {
                                //Reboot warm.
                                Xbox_Debug_Communicator.Reboot(XboxDebugCommunicator.RebootFlags.Warm);
                                break;
                            }
                        case "Cold":
                            {
                                //Reboot to cold
                                Xbox_Debug_Communicator.Reboot(XboxDebugCommunicator.RebootFlags.Cold);
                                break;
                            }
                    }

                    //Clear our treeview & listview
                    treeView1.Nodes.Clear();
                    listView1.Items.Clear();

                    //Set our xbox debug communicator as null
                    try { Xbox_Debug_Communicator.Disconnect(); }
                    catch { }
                    Xbox_Debug_Communicator = null;

                    //Return
                    return;
                }

            //Show our message
            MessageBox.Show("You are not connected.");
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            //If the Xbox_Debug_Communicator isn't null
            if (Xbox_Debug_Communicator != null)
                //If it is connected..
                if (Xbox_Debug_Communicator.Connected)
                {
                    try
                    {
                        //Send our data and show our message.
                        richTextCommandBox.Text = Xbox_Debug_Communicator.SendTextCommand(comboBoxEx2.Text);
                    }
                    catch
                    {
                        //Show our error.
                        richTextCommandBox.Text = "[Alteration Error] An error occurred. This may be due to a bad command.";
                    }

                    //Select our response tab
                    tabControl1.SelectedTab = tabItem3;

                    //Return
                    return;
                }

            //Show our message
            MessageBox.Show("You are not connected.");
        }

        private void AddCommandsToComboBox()
        {
           //Our command list.
            string commandMasterStr = @"xexfield
xbeinfo
writefile
walkmem
verbosevd
userlist
user
title
threads
threadinfo
tdecomp
systime
systeminfo
sysfileupd
suspend
stopon
stop
setuserpriv
setsystime
setserver
setreset
setmem
setfileattributes
setcontext
setconfig
sendfile
screenshot
runoobe
resume
rename
reboot
querypc
pogoctrl
pixpreview
pixcmd
perfstat
pdbinfo
pclist
oddlog
notifyport
nostopon
nicstats
netcap
modules
modsections
mkdir
mdetect
magicboot
lopctrl
lockmode
keyxchg
kdlite
kd
isstopped
isdebugger
isbreak
irtsweep
iptvsupportinfo
iptvprovisioningurl
iptvprovidername
iptvremote
iptvdvr
iptv
hddhalt
gpucount
go
getxnsecassocinfo
getxnqoslookupinfo
getxnkeyinfo
getuserpriv
getsum
getsockinfo
getpid
getmemex
getmem
getfileattributes
getfile
getcontext
fmtfat
flash
fileevent
fileeof
eventdefer
dvdperf
dvdeject
dvdblk
dumpsettings
dumpmode
drivelist
drivefreespace
dmversion
dirlist
detour
delete
deftitle
dedicate
debugmode
debugger
dbgprint
dbgname
dbgextld
crashdump
continue
consoletype
capctrl
bye
boxid
bootanim
bkgdmodetest
autoprof
autoinput
authuser
altaddr
adminpw";
            //Get our commands as an array.
            string[] commands = commandMasterStr.Split('\n');

            //Clear all combobox items.
            comboBoxEx2.Items.Clear();

            //Loop and add to our combobox.
            for (int i = 0; i < commands.Length; i++)
            {
                comboBoxEx2.Items.Insert(0,commands[i].Replace("\r", ""));
            }
        }

        private void LoadFileSystemDrives()
        {
            //Clear our nodes
            treeView1.Nodes.Clear();

            //For each drive in our collection.
            foreach (string drive in Xbox_Debug_Communicator.File_System.Drives)
            {
                //Create a treenode for this drive
                TreeNode driveNode = new TreeNode(drive);
                
                //Set the imagekey
                driveNode.ImageIndex = 1;
                driveNode.SelectedImageIndex = 1;

                //Set the node's tag
                driveNode.Tag = drive + ":\\";
                
                //Add it to our treeview
                treeView1.Nodes.Add(driveNode);
            }

            //Sort our treeview
            treeView1.Sort();

        }

        private void LoadFileSystemFolders(string folderPath)
        {
            //Clear our nodes for our selected node
            treeView1.SelectedNode.Nodes.Clear();

            //Obtain our directories for this folder.
            string[] Directories = Xbox_Debug_Communicator.File_System.GetDirectories(folderPath);

            //Sort our array
            Array.Sort(Directories);

            //Loop through each directory
            foreach (string Directory in Directories)
            {
                //Obtain our directory name
                string shortDirName = Directory.Substring(treeView1.SelectedNode.Tag.ToString().Length);

                //Create a treenode for this folder
                TreeNode folderNode = new TreeNode(shortDirName);

                //Set the imagekey
                folderNode.ImageIndex = 11;
                folderNode.SelectedImageIndex = 12;

                //Set the node's tag
                folderNode.Tag = Directory + "\\";

                //Add it to our selected node.
                treeView1.SelectedNode.Nodes.Add(folderNode);

                //Create a ListViewItem for this file
                ListViewItem lvi = new ListViewItem(shortDirName);

                //Set the imagekey
                lvi.ImageIndex = 11;

                //Set the node's tag
                lvi.Tag = Directory + "\\";

                //Add it to our listview
                listView1.Items.Add(lvi);
            }

            //Expand this node.
            treeView1.SelectedNode.Expand();

        }

        private void LoadFileSystemFiles(string folderpath)
        {
            //Get our list of files.
            string[] Files = Xbox_Debug_Communicator.File_System.GetFiles(folderpath);

            //Sort our array
            Array.Sort(Files);

            //Loop through each file
            foreach (string file in Files)
            {
                //Get our filename
                string shortFileName = file.Substring(treeView1.SelectedNode.Tag.ToString().Length);

                //Create a ListViewItem for this file
                ListViewItem lvi = new ListViewItem(shortFileName);

                //Set the imagekey
                lvi.ImageIndex = 14;

                //Set the node's tag
                lvi.Tag = file;

                //Add it to our listview
                listView1.Items.Add(lvi);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //If our treeview selected node isn't null
            if(treeView1.SelectedNode != null)
                //If the tag for it isn't null
                if (treeView1.SelectedNode.Tag != null)
                {
                    //Clear our listview
                    listView1.Items.Clear();

                    //Add our folders.
                    LoadFileSystemFolders(treeView1.SelectedNode.Tag.ToString());

                    //Add our files
                    LoadFileSystemFiles(treeView1.SelectedNode.Tag.ToString());
                }
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //If the Xbox_Debug_Communicator isn't null
            if (Xbox_Debug_Communicator != null)
                //If it is connected..
                if (Xbox_Debug_Communicator.Connected)
                {
                    //If our treeview selected node isn't null
                    if (treeView1.SelectedNode != null)
                        //If the tag for it isn't null
                        if (treeView1.SelectedNode.Tag != null)
                        {
                            //Create our new folder dialog.
                            FSNewFolder newFolderDialog = new FSNewFolder(Xbox_Debug_Communicator, treeView1.SelectedNode.Tag.ToString());

                            //Show our dialog
                            newFolderDialog.ShowDialog();

                            //Once it's done processing, reload this folder.
                            LoadFileSystemFolders(treeView1.SelectedNode.Tag.ToString());
                        }
                    return;
                }

            //Show our message
            MessageBox.Show("You are not connected.");
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //If the Xbox_Debug_Communicator isn't null
            if (Xbox_Debug_Communicator != null)
                //If it is connected..
                if (Xbox_Debug_Communicator.Connected)
                {
                    //If our treeview selected node isn't null
                    if (treeView1.SelectedNode != null)
                        //If the tag for it isn't null
                        if (treeView1.SelectedNode.Tag != null)
                        {
                            //Send our delete command
                            Xbox_Debug_Communicator.File_System.DeleteDirectory(treeView1.SelectedNode.Tag.ToString());

                            //Select our parent.
                            treeView1.SelectedNode = treeView1.SelectedNode.Parent;
                        }
                    return;
                }

            //Show our message
            MessageBox.Show("You are not connected.");
        }

        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //If the Xbox_Debug_Communicator isn't null
            if (Xbox_Debug_Communicator != null)
                //If it is connected..
                if (Xbox_Debug_Communicator.Connected)
                {
                    //If our treeview selected node isn't null
                    if (treeView1.SelectedNode != null)
                        //If the tag for it isn't null
                        if (treeView1.SelectedNode.Tag != null)
                        {
                            //Create our open file dialog
                            OpenFileDialog ofd = new OpenFileDialog();

                            //If our result of us showing it is ok..
                            if (ofd.ShowDialog() == DialogResult.OK)
                            {
                                //Create our temporary string
                                string[] temporaryString = ofd.FileName.Split('\\');
                                //Get the short filename
                                string shortFilename = temporaryString[temporaryString.Length - 1];

                                //Start uploading our file
                                Xbox_Debug_Communicator.File_System.UploadFile(ofd.FileName, treeView1.SelectedNode.Tag.ToString() + shortFilename);


                                //Clear our listview
                                listView1.Items.Clear();

                                //Load our directories
                                LoadFileSystemFolders(treeView1.SelectedNode.Tag.ToString());
                                
                                //Reload our file list
                                LoadFileSystemFiles(treeView1.SelectedNode.Tag.ToString());
                            }

                        }
                    return;
                }

            //Show our message
            MessageBox.Show("You are not connected.");
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //If the Xbox_Debug_Communicator isn't null
            if (Xbox_Debug_Communicator != null)
                //If it is connected..
                if (Xbox_Debug_Communicator.Connected)
                {
                    //If our treeview selected node isn't null
                    if (treeView1.SelectedNode != null)
                        if (listView1.SelectedItems.Count > 0)
                            //If the tag for it isn't null
                            if (treeView1.SelectedNode.Tag != null)
                            {
                                //Loop for each selected item
                                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                                {  
                                    //If our selected item is a file
                                    if (listView1.SelectedItems[i].ImageIndex == 14)
                                    {
                                        //Delete the file
                                        Xbox_Debug_Communicator.File_System.DeleteFile(listView1.SelectedItems[i].Tag.ToString());
                                    }
                                    else if (listView1.SelectedItems[i].ImageIndex == 11)
                                    {
                                        //Delete the directory
                                        Xbox_Debug_Communicator.File_System.DeleteDirectory(listView1.SelectedItems[i].Tag.ToString());
                                    }
                                }

                                //Clear our listview
                                listView1.Items.Clear();

                                //Load our directories
                                LoadFileSystemFolders(treeView1.SelectedNode.Tag.ToString());

                                //Reload our file list
                                LoadFileSystemFiles(treeView1.SelectedNode.Tag.ToString());

                            }
                    return;
                }

            //Show our message
            MessageBox.Show("You are not connected.");
        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //If the Xbox_Debug_Communicator isn't null
            if (Xbox_Debug_Communicator != null)
                //If it is connected..
                if (Xbox_Debug_Communicator.Connected)
                {
                    //If our treeview selected node isn't null
                    if (treeView1.SelectedNode != null)
                        if (listView1.SelectedItems.Count > 0)
                            //If the tag for it isn't null
                            if (treeView1.SelectedNode.Tag != null)
                            {
                                //If we got more than one file.
                                if (listView1.SelectedItems.Count > 1 | listView1.SelectedItems[0].ImageIndex == 11)
                                {
                                    //Create our folderbrowserdialog
                                    FolderBrowserDialog fbd = new FolderBrowserDialog();

                                    //If our result of us showing it is ok..
                                    if (fbd.ShowDialog() == DialogResult.OK)
                                    {

                                        //Loop for each selected file
                                        for (int i = 0; i < listView1.SelectedItems.Count; i++)
                                        {
                                            //If it is a file
                                            if (listView1.SelectedItems[0].ImageIndex == 14)
                                            {
                                                //Get the short filename
                                                string shortFilename = listView1.SelectedItems[i].Text;

                                                //Start uploading our file
                                                Xbox_Debug_Communicator.File_System.DownloadFile(fbd.SelectedPath + "\\" + shortFilename, listView1.SelectedItems[i].Tag.ToString());
                                            }
                                            else
                                            {
                                                //Get the short filename
                                                string shortFilename = listView1.SelectedItems[i].Text;

                                                //Start uploading our file
                                                Xbox_Debug_Communicator.File_System.DownloadDirectory(fbd.SelectedPath, listView1.SelectedItems[i].Tag.ToString());
                                            }
                                        }

                                        //Show our done message
                                        MessageBox.Show("Done.");
                                    }
                                }
                                else
                                {
                                    //Create a savefiledialog for our one file.
                                    SaveFileDialog sfd = new SaveFileDialog();

                                    //Set our filename
                                    sfd.FileName = listView1.SelectedItems[0].Text;

                                    //If the result of us showing the dialog is OK
                                    if (sfd.ShowDialog() == DialogResult.OK)
                                    {
                                        //Get the short filename
                                        string shortFilename = listView1.SelectedItems[0].Text;

                                        //Start uploading our file
                                        Xbox_Debug_Communicator.File_System.DownloadFile(sfd.FileName, listView1.SelectedItems[0].Tag.ToString());
                                        
                                        //Show our done message
                                        MessageBox.Show("Done.");
                                    }
                                }
                            }
                    return;
                }

            //Show our message
            MessageBox.Show("You are not connected.");
        }

        private void buttonX9_Click(object sender, EventArgs e)
        {
           //If the Xbox_Debug_Communicator isn't null
            if (Xbox_Debug_Communicator != null)
                //If it is connected..
                if (Xbox_Debug_Communicator.Connected)
                {
                    //Get our memory stream.
                    XboxMemoryStream xms = Xbox_Debug_Communicator.ReturnXboxMemoryStream();

                    //Create our IO
                    EndianIO IO = new EndianIO(xms, EndianType.BigEndian);

                    //Open our IO
                    IO.Open();

                    //Go to our offset
                    IO.In.BaseStream.Position = 0x3A00C000 + 0x1A874;

                    //Read our macbin
                    byte[] macbin = IO.In.ReadBytes(0x1D0);

                    //Close our IO
                    IO.Close();

                    //Create our savefiledialog
                    SaveFileDialog sfd = new SaveFileDialog();

                    //Set our filter
                    sfd.Filter = "Mac Bin(.bin)|*.bin";

                    //Show our dialog, if the result is OK
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        //Create our IO
                        IO = new EndianIO(new FileStream(sfd.FileName, FileMode.Create), EndianType.BigEndian);

                        //Open our IO
                        IO.Open();

                        //Write our macbin data.
                        IO.Out.Write(macbin);

                        //Close our IO
                        IO.Close();

                        //Show our messagebox
                        MessageBox.Show("Done.");
                    }
                    return;
                }

            //We aren't connected
            MessageBox.Show("You are not connected.");
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            //If we got a selected item
            if (listView1.SelectedItems.Count == 1)
            {
                //If it is a directory.
                if (listView1.SelectedItems[0].ImageIndex == 11)
                {
                    //Get the path of our directory
                    string dirPath = listView1.SelectedItems[0].Tag.ToString();

                    //Loop through our nodes
                    foreach (TreeNode node in treeView1.SelectedNode.Nodes)
                    {
                        //If the tag matches
                        if (node.Tag.ToString() == dirPath)
                        {
                            //Select it
                            treeView1.SelectedNode = node;
                            
                            //Break
                            break;
                        }
                    }
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void renameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //If the Xbox_Debug_Communicator isn't null
            if (Xbox_Debug_Communicator != null)
                //If it is connected..
                if (Xbox_Debug_Communicator.Connected)
                {
                    //If our treeview selected node isn't null
                    if (treeView1.SelectedNode != null)
                        if (listView1.SelectedItems.Count > 0)
                            //If the tag for it isn't null
                            if (treeView1.SelectedNode.Tag != null)
                            {
                                //If the selected item is a file.
                                if (listView1.SelectedItems[0].ImageIndex == 14)
                                {
                                    //Create our rename dialog form
                                    FSRenameFile fsrf = new FSRenameFile(Xbox_Debug_Communicator, listView1.SelectedItems[0].Tag.ToString());

                                    //Show the dialog
                                    fsrf.ShowDialog();

                                    //Clear our listview
                                    listView1.Items.Clear();

                                    //Load our directories
                                    LoadFileSystemFolders(treeView1.SelectedNode.Tag.ToString());

                                    //Load the files
                                    LoadFileSystemFiles(treeView1.SelectedNode.Tag.ToString());

                                    //Show our done message
                                    MessageBox.Show("Done.");
                                }
                            }

                    return;
                }
            //Show our message
            MessageBox.Show("You are not connected.");

        }

        private void newFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //If the Xbox_Debug_Communicator isn't null
            if (Xbox_Debug_Communicator != null)
                //If it is connected..
                if (Xbox_Debug_Communicator.Connected)
                {
                    //If our treeview selected node isn't null
                    if (treeView1.SelectedNode != null)
                        //If the tag for it isn't null
                        if (treeView1.SelectedNode.Tag != null)
                        {
                            //Create our new folder dialog.
                            FSNewFolder newFolderDialog = new FSNewFolder(Xbox_Debug_Communicator, treeView1.SelectedNode.Tag.ToString());

                            //Show our dialog
                            newFolderDialog.ShowDialog();

                            //Clear our listview
                            listView1.Items.Clear();

                            //Load our directories
                            LoadFileSystemFolders(treeView1.SelectedNode.Tag.ToString());

                            //Load the files
                            LoadFileSystemFiles(treeView1.SelectedNode.Tag.ToString());
                        }
                    return;
                }

            //Show our message
            MessageBox.Show("You are not connected.");
        }

        private void uploadFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //If the Xbox_Debug_Communicator isn't null
            if (Xbox_Debug_Communicator != null)
                //If it is connected..
                if (Xbox_Debug_Communicator.Connected)
                {
                    //If our treeview selected node isn't null
                    if (treeView1.SelectedNode != null)
                        //If the tag for it isn't null
                        if (treeView1.SelectedNode.Tag != null)
                        {
                            //Create our folderbrowserdialog
                            FolderBrowserDialog fbd = new FolderBrowserDialog();

                            //If our result of us showing it is ok..
                            if (fbd.ShowDialog() == DialogResult.OK)
                            {
                                //Upload our directory
                                Xbox_Debug_Communicator.File_System.UploadDirectory(fbd.SelectedPath, treeView1.SelectedNode.Tag.ToString());

                                //Clear our listview
                                listView1.Items.Clear();

                                //Load our directories
                                LoadFileSystemFolders(treeView1.SelectedNode.Tag.ToString());

                                //Load the files
                                LoadFileSystemFiles(treeView1.SelectedNode.Tag.ToString());
                            }
                        }
                    return;
                }

            //Show our message
            MessageBox.Show("You are not connected.");
        }
    }
}
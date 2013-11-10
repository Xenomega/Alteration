using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HaloDevelopmentExtender;

namespace Alteration.Forms.Xbox_Debug_Communicator.Dialog.File_System
{
    public partial class FSRenameFile : Form
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

        private string _filename;
        /// <summary>
        /// The name of the file to rename
        /// </summary>
        public string File_Name
        {
            get { return _filename; }
            set { _filename = value; }
        }

        private string _parentfolder;
        /// <summary>
        /// The parent of the folder we will create.
        /// </summary>
        public string Parent_Folder
        {
            get { return _parentfolder; }
            set { _parentfolder = value; }
        }


        public FSRenameFile(XboxDebugCommunicator xdc, string filename)
        {
            InitializeComponent();

            //Set our Xbox Debug Communicator
            Xbox_Debug_Communicator = xdc;

            //Get our temporary string
            string[] temporaryString = filename.Split('\\');

            //Set our filename
            File_Name = temporaryString[temporaryString.Length - 1];

            //Set our parent folder
            Parent_Folder = filename.Substring(0, filename.Length - temporaryString[temporaryString.Length - 1].Length);

            //Set the textbox's text
            textBox1.Text = File_Name;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

            //-------------
            //Check invaliditity.
            //-------------
            string invalidFilenameCharacters = @"""'~`!@#$%^&*=+\|/?><,;:[]{}";
            foreach (char character in invalidFilenameCharacters)
            {
                if (textBox1.Text.Contains(character.ToString()))
                    return;
            }
            if (textBox1.Text == "")
                return;

            //Create our new folder
            if (new List<string>(Xbox_Debug_Communicator.File_System.GetFiles(Parent_Folder)).Contains(textBox1.Text))
            {
                //Show our error message.
                MessageBox.Show("You have entered a name of an already existing file. Please enter another name for your file.");
                return;
            }

            //If we made it through all of that, rename
            Xbox_Debug_Communicator.File_System.RenameFile(Parent_Folder + File_Name, Parent_Folder + textBox1.Text);

            //Close this dialog
            this.Close();
        }
    }
}
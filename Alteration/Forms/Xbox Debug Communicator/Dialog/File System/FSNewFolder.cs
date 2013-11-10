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
    public partial class FSNewFolder : Form
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

        private string _parentfolder;
        /// <summary>
        /// The parent of the folder we will create.
        /// </summary>
        public string Parent_Folder
        {
            get { return _parentfolder; }
            set { _parentfolder = value; }
        }

        public FSNewFolder(XboxDebugCommunicator xdc, string parentfolder)
        {
            InitializeComponent();

            //Set our Xbox Debug Communicator instance
            Xbox_Debug_Communicator = xdc;

            //Set our parent folder
            Parent_Folder = parentfolder;
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
            if (new List<string>(Xbox_Debug_Communicator.File_System.GetDirectories(Parent_Folder)).Contains(textBox1.Text))
            {
                //Show our error message.
                MessageBox.Show("You have entered a name of an already existing folder. Please enter another name for your folder.");
                return;
            }

            //If we got past all of that, let's create our folder
            Xbox_Debug_Communicator.File_System.CreateDirectory(Parent_Folder + textBox1.Text + "\\");

            //Close this form.
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //If we clicked enter.
            if (e.KeyCode == Keys.Enter)
            {
                //Click the go button.
                this.buttonX1_Click(new object(), new EventArgs());
            }
        }
    }
}
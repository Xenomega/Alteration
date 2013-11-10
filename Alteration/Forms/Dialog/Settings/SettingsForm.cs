using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Alteration.Settings;

namespace Alteration.Forms.Dialog
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            propertyGrid1.SelectedObject = AppSettings.Settings;
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            AppSettings.SaveSettings();
        }
    }
}
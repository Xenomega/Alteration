using System;
using System.Threading;
using System.Windows.Forms;
using Alteration.Forms;
using Alteration.Settings;

namespace Alteration
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {

            //Initialize program...
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Load our settings
            AppSettings.settingsDirectory = Application.StartupPath;
            AppSettings.LoadSettings();
          

            Application.Run(new Form1());
        }
    }
}
using System.Windows.Forms;
using HaloDeveloper.Map;

namespace AlterationLib
{
    public class AlterationLibrary : UserControl
    {
        public string Map_Location;
        public int Selected_Tag_Index;
        /// <summary>
        /// This will be our tagClass filter in which can
        /// </summary>
        public string TagClassFilter = "";
        /// <summary>
        /// This is our library name which will be shown in Alteration when you can select the Library.
        /// </summary>
        public string LibraryName;
        /// <summary>
        /// This is the library Author which Alteration will read
        /// </summary>
        public string LibraryAuthor;
        // ReSharper disable UnusedPrivateMember
        private void InitializeComponent()
        // ReSharper restore UnusedPrivateMember
        {
            SuspendLayout();
            // 
            // AlterationLibrary
            // 
            Name = "AlterationLibrary";
            ResumeLayout(false);
        }
    }
}
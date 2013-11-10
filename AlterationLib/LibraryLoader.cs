using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AlterationLib
{
    public class LibraryLoader
    {
        public List<LibraryClass> Libraries;
        public void LoadLibraries(string directory)
        {
            return;
            //Loop through each file in the given directory
            foreach (FileInfo fi in new DirectoryInfo(directory).GetFiles())
            {
                //Initialize our reflection assembly
                Assembly asm;
                //Attempt to load the file.
                try
                {
                    asm = Assembly.LoadFile(fi.FullName);
                }
                catch { continue; }
                //Get our types in our assembly
                foreach (Type t in asm.GetTypes())
                {
                    //If theres the type of AlterationLibrary...
                    if (t.BaseType == typeof(AlterationLibrary))
                    {
                        //Initialize our LibraryClass
                        LibraryClass Library_Class = new LibraryClass();
                        //Set our library instance
                        Library_Class.Library = (AlterationLibrary)Activator.CreateInstance(t);
                        //Set our FileName instance
                        Library_Class.FileName = fi.FullName;
                        //Add it to the list
                        Libraries.Add(Library_Class);
                    }
                }
            }
            //Loop through the directories
            foreach (DirectoryInfo di in new DirectoryInfo(directory).GetDirectories())
            {
                //Load any libraries recursively
                LoadLibraries(di.FullName);
            }
        }
        public class LibraryClass
        {
            public AlterationLibrary Library;
            public string FileName;

        }
    }
}

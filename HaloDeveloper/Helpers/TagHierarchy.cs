using System.Collections.Generic;
using HaloDeveloper.Map;

namespace HaloDeveloper.Helpers
{
    public class TagHierarchy
    {
        private HaloMap map;
        /// <summary>
        /// Our Map class value in which we retreived the tag hierarchy from.
        /// </summary>
        public HaloMap Map
        {
            get { return map; }
            set { map = value; }
        }
        private TagHClasses tagClasses;
        /// <summary>
        /// Our tagclasses parent list.
        /// </summary>
        public TagHClasses TagClasses
        {
            get { return tagClasses; }
            set { tagClasses = value; }
        }
        public TagHierarchy(HaloMap map)
        {
            //Assign our instance of our map class
            Map = map;
            //Initialize our temporary done list
            List<string> doneTagClasses = new List<string>();
            //Initialize our tagclasses list
            TagClasses = new TagHClasses();
            //Loop through every tag
            for (int i = 0; i < Map.IndexItems.Count; i++)
            {
                if (!doneTagClasses.Contains(Map.IndexItems[i].Class))
                {
                    //Initialize our tagClass
                    TagHClass tagClass = new TagHClass();
                    //Assign our class
                    tagClass.TagClass = Map.IndexItems[i].Class;
                    //Loop through all of our tags
                    for (int z = 0; z < Map.IndexItems.Count; z++)
                    {
                        //If this indexed tag's our tagclass we're looking for
                        if (Map.IndexItems[z].Class == tagClass.TagClass)
                        {
                            //Initialize our tagname
                            TagHName tagName = new TagHName();
                            //Assign our tagname
                            tagName.TagName = Map.IndexItems[z].Name;
                            //Assign our taginstance
                            tagName.TagInstance = Map.IndexItems[z];
                            //Add our tagname to the parent
                            tagClass.Tags.Add(tagName);
                        }
                    }
                    //Add our tagclass to the list
                    TagClasses.Add(tagClass);
                    //Add our now done tagclass
                    doneTagClasses.Add(Map.IndexItems[i].Class);
                }
            }
        }
        public class TagHClasses : List<TagHClass>
        {
            public TagHClass ReturnTagHClass(string tagClass)
            {
                for (int i = 0; i < Count; i++)
                    if (this[i].TagClass == tagClass)
                        return this[i];

                return null;
            }
        }
        public class TagHClass
        {
            public TagHClass()
            {
                Tags = new List<TagHName>();
            }
            private string tagClass;
            /// <summary>
            /// Assign our instance of our tagclass item.
            /// </summary>
            public string TagClass
            {
                get { return tagClass; }
                set { tagClass = value; }
            }
            private List<TagHName> tags;
            /// <summary>
            /// Our instance of our tagname list.
            /// </summary>
            public List<TagHName> Tags
            {
                get { return tags; }
                set { tags = value; }
            }
            public TagHName ReturnTagHName(string tagName)
            {
                for (int i = 0; i < Tags.Count; i++)
                    if (Tags[i].TagName == tagName)
                        return Tags[i];

                return null;
            }
        }
        public class TagHName
        {
            private string tagName;
            /// <summary>
            /// Assign our instance of our tagname item.
            /// </summary>
            public string TagName
            {
                get { return tagName; }
                set { tagName = value; }
            }
            private HaloMap.TagItem tagInstance;
            /// <summary>
            /// Our tag instance value in which we can grab values from.
            /// </summary>
            public HaloMap.TagItem TagInstance
            {
                get { return tagInstance; }
                set { tagInstance = value; }
            }
        }
    }
}

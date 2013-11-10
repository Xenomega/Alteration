using System;
using System.Collections.Generic;
using System.Text;
using Alteration.Halo_3.Meta_Editor;
using Alteration.Halo_3.Meta_Editor.Controls;
using System.Windows.Forms;

namespace Alteration.Halo_3.Map_File.Meta_Editor.Full.Controls
{
    public abstract class ControlParentPoker
    {
        public static void PokeByParent(UserControl controlThis, bool changes)
        {
            //Set the first control
            Control parent = controlThis;
            //Continue to loop
            while (true)
            {
                //Set the parent, this will keep setting and looping
                parent = parent.Parent;
                //...Until the parent is null
                if (parent == null)
                {
                    //...Then it'll exit the loop
                    break;
                }
                //...Or until we hit the meta editor container
                if (parent.Name == "MetaEditorContainer")
                {
                    //Poke appropriately
                    if (changes)
                        ((MetaEditorContainer)parent).PokeChanges();
                    else
                        ((MetaEditorContainer)parent).PokeChanges();

                    //Break
                    break;
                }
            }
        }
    }
}

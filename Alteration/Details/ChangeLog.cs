using System;
using System.Collections.Generic;
using System.Text;

namespace Alteration.Details
{
    public static class ChangeLogs
    {
        /// <summary>
        /// Our array of changes for our change log.
        /// </summary>
        public static Change[] ChangeLog = new Change[]{
            
            //Alteration Version 0.045
            new Change( 0.045f, ChangeType.Add, "Added patch file support" ),
            new Change( 0.045f, ChangeType.Add, "Added new Settings Dialog" ),
            new Change( 0.045f, ChangeType.Add, "Added ablity to resize screenshots" ),
            new Change( 0.045f, ChangeType.Add, "Added ablity to save screenshots to a different format" ),
            new Change( 0.045f, ChangeType.Add, "Added encryption to sensitive setting data" ),
            new Change( 0.045f, ChangeType.Remove, "Removed settings from the right" ),
            new Change( 0.045f, ChangeType.Update, "Updated the main menu" ),
            new Change( 0.045f, ChangeType.Update, "Update SandBar.dll" ),
            new Change( 0.045f, ChangeType.Update, "Updated Login Screen With Matching Controls" ),
            new Change( 0.045f, ChangeType.Fix, "Fixed many bugs" ),

            //Alteration Version 0.046
            new Change( 0.046f, ChangeType.Add, "Added Change Log" ),
            new Change( 0.046f, ChangeType.Add, "Added new refresh option for the meta editor" ),
            new Change( 0.046f, ChangeType.Add, "Added automatic recalculating of unicode table hash" ),
            new Change( 0.046f, ChangeType.Add, "Added DirectX Fill Mode modification" ),
            new Change( 0.046f, ChangeType.Remove, "Removed view from the main menu" ),            
            new Change( 0.046f, ChangeType.Fix, "Fixed the create patch function to make it not miss anything" ),
            new Change( 0.046f, ChangeType.Update, "Made PNG default screenshot type" ),
            new Change( 0.046f, ChangeType.Update, "Updated create patch function to make it alot faster" ),

            //Alteration Version 0.047
            new Change( 0.047f, ChangeType.Update, "Updated comboboxes in meta editor so you cannot type in them, reducing error." ),

            //Alteration Version 0.048
            new Change( 0.048f, ChangeType.Add, "Added right click->View value as... In Meta Editor for benefit in research." ),
            new Change( 0.048f, ChangeType.Add, "Added right click->Copy & Paste->Chunk Data on Reflexives/Structures in the meta editor." ),
            new Change( 0.048f, ChangeType.Add, "Added right click->Copy & Paste->Chunk Data For all Chunks on Reflexives/Structures in the meta editor." ),
            new Change( 0.048f, ChangeType.Add, "Added disable reflexive in Meta Editor." ),
            new Change( 0.048f, ChangeType.Fix, "Fixed the Meta Grid Docking. No longer cuts off." ),

            //Alteration Version 0.049
            new Change( 0.049f, ChangeType.Add, "Added right click->Swap Meta Header. This allows you to overwrite tags." ),
            new Change( 0.049f, ChangeType.Remove, "Removed the feature to not be able to type in a combobox in the meta editor for idents. This is better so you can copy and paste and save time." ),
            new Change( 0.049f, ChangeType.Add, "Added Xbox Debug Communicator." ),
            new Change( 0.049f, ChangeType.Update, "Updated security." ),

            //Alteration Version 0.050
            new Change( 0.050f, ChangeType.Fix, "Fixed the Halo Development Extender so it doesn't throw errors anymore after too many connections." ),
            new Change( 0.050f, ChangeType.Fix, "Made the Xbox Debug Communicator unload the filesystem and etc. when rebooting, to prevent error." ),

            //Alteration Version 0.051
            new Change( 0.051f, ChangeType.Add, "Added tag renaming. A new special XEX is needed. Talk to Xenon about it." ),
            new Change( 0.051f, ChangeType.Add, "Added map protecting. Protect your maps before handing them out, so people don't mess with them." ),
            new Change( 0.051f, ChangeType.Add, "Added 'Go To In New Tab' for idents in the Meta Grid." ),
            new Change( 0.051f, ChangeType.Add, "Added locale renaming of any length. Shorter and longer strings should now work." ),
            new Change( 0.051f, ChangeType.Update, "Map Package under the edit menu became Maps and Package, which will now have functions for Maps, and the packages." ),
            new Change( 0.051f, ChangeType.Update, "Made it so View Value As.. in the Meta Editor now displays all values at once, for convenience." ),

            //Alteration Version 0.052
            new Change( 0.052f, ChangeType.Add, "Added more to Rightclick->Copy in the Meta Editor." ),
            new Change( 0.052f, ChangeType.Fix, "Fixed Libraries. You may need a new Library folder from Xenon." ),
            new Change( 0.052f, ChangeType.Add, "Added Edit->Plugins->Generate Plugins (will appear frozen and take a LONG time, not recommended)" ),
            new Change( 0.052f, ChangeType.Add, "Switched from .ent to .alt extension. Use Edit->Plugins->Rename Plugins to rename your plugins" ),

            //Alteration Version 0.06
            new Change( 0.06f, ChangeType.Add, "Added more formats for Screenshot support, most, if not all games should be supported now." ),
            new Change( 0.06f, ChangeType.Add, "Added StringID support, and a strings view in the Meta Grid." ),
        };

        /// <summary>
        /// This function gets our change log as a string.
        /// </summary>
        /// <returns>Returns a string of our changes for this current build.</returns>
        public static string GetChangeLogString()
        {
            //Return a log of our current changes for this revision.
            return GetChangeLogString(AlterationDetails.Version);
        }

        public static string GetChangeLogString(float Version)
        {
            //This is our output string
            string outputString = "";

            //Lets loop through all our changes
            for (int x = 0; x < ChangeLog.Length; x++)
            {
                if (ChangeLog[x].Version == Version || Version == 0)
                    outputString += ChangeLog[x].ToString() + Environment.NewLine;
            }

            //Return our string now
            return outputString;
        }

    }

    /// <summary>
    /// The type of change made in the update.
    /// </summary>
    public enum ChangeType
    {
        None,
        Add,
        Remove,
        Fix,
        Update,
    }

    public class Change
    {
        private ChangeType _changeType = ChangeType.None;
        public ChangeType ChangeType { get { return _changeType; } }

        private string _text = "";
        public string Text { get { return _text; } }

        private float _version = 0.0f;
        public float Version { get { return _version; } }

        public Change(float Version, ChangeType Type, string Text)
        {
            _version = Version;
            _changeType = Type;
            _text = Text;
        }

        public override string ToString()
        {
            string outPutString = "";
            switch (_changeType)
            {
                case ChangeType.Add:
                    outPutString += "+";
                    break;
                case ChangeType.Remove:
                    outPutString += "-";
                    break;
                case ChangeType.Fix:
                    outPutString += "*";
                    break;
                case ChangeType.Update:
                    outPutString += "*";
                    break;
                case ChangeType.None:
                default:
                    break;
            }
            return outPutString + " " + Text;
        }
    }

}
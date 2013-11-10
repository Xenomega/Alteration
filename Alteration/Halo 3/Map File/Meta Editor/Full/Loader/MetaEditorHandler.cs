using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Alteration.Halo_3.Meta_Editor.Controls;
using Alteration.Halo_3.Values;
using Alteration.Patches.Memory_Patch.RTH_Data;
using HaloDeveloper.IO;
using HaloDeveloper.Map;

namespace Alteration.Halo_3.Meta_Editor.Loader
{
    public abstract class MetaEditorHandler
    {
        public static void LoadPluginUI(List<mValue> ValueList, Panel parentPanel, bool showInvisibles)
        {
            int locationY = 0;
            //Loop through all the values
            for (int i = 0; i < ValueList.Count; i++)
            {
                //Unfreeze our form
                Application.DoEvents();
                //Lets determine our valueType by its attribute enum.
                switch (ValueList[i].Attributes)
                {
                        //If its a...

                        #region Reflexives

                    case mValue.ObjectAttributes.Reflexive:
                        {
                            //Obtain our instance of the value.
                            mReflexive reflexive = (mReflexive) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (reflexive.Visible == false)
                                {
                                    //If its not supposed to be shown, skip to the next value.
                                    break;
                                }
                            }
                            //Initialize our interface component using our value as a parameter.
                            iStruct istruct = new iStruct(reflexive);
                            //Load the UserInteface recursively into the struct's panel.
                            LoadPluginUI(reflexive.Values, istruct.returnValuePanel(), showInvisibles);
                            //Resize the height
                            istruct.ResizeHeight();
                            //Set our items location
                            istruct.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += istruct.Height;
                            //Add the structure to the parent panel
                            parentPanel.Controls.Add(istruct);
                            //Bring it to the top of where it should be.
                            istruct.BringToFront();
                            break;
                        }

                        #endregion

                        #region Bitmasks

                    case mValue.ObjectAttributes.Bitmask8:
                        {
                            //Obtain our bitmask value
                            mBitmask8 bitmask8 = (mBitmask8) ValueList[i];
                            //Check our visibility flags
                            if (!showInvisibles)
                            {
                                if (bitmask8.Visible == false)
                                {
                                    //If its not supposed to be shown, skip to the next value.
                                    break;
                                }
                            }
                            //Initialize our interface component
                            iBitmask ibitmask = new iBitmask(bitmask8);
                            //Set our items location
                            ibitmask.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ibitmask.Height;
                            //Add it to the parent panel
                            parentPanel.Controls.Add(ibitmask);
                            //Bring it to its appropriate location
                            ibitmask.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.Bitmask16:
                        {
                            //Obtain our bitmask value
                            mBitmask16 bitmask16 = (mBitmask16) ValueList[i];
                            //Check our visibility flags
                            if (!showInvisibles)
                            {
                                if (bitmask16.Visible == false)
                                {
                                    //If its not supposed to be shown, skip to the next value.
                                    break;
                                }
                            }
                            //Initialize our interface component
                            iBitmask ibitmask = new iBitmask(bitmask16);
                            //Set our items location
                            ibitmask.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ibitmask.Height;
                            //Add it to the parent panel
                            parentPanel.Controls.Add(ibitmask);
                            //Bring it to its appropriate location
                            ibitmask.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.Bitmask32:
                        {
                            //Obtain our bitmask value
                            mBitmask32 bitmask32 = (mBitmask32) ValueList[i];
                            //Check our visibility flags
                            if (!showInvisibles)
                            {
                                if (bitmask32.Visible == false)
                                {
                                    //If its not supposed to be shown, skip to the next value.
                                    break;
                                }
                            }
                            //Initialize our interface component
                            iBitmask ibitmask = new iBitmask(bitmask32);
                            //Set our items location
                            ibitmask.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ibitmask.Height;
                            //Add it to the parent panel
                            parentPanel.Controls.Add(ibitmask);
                            //Bring it to its appropriate location
                            ibitmask.BringToFront();
                            break;
                        }

                        #endregion

                        #region Enums

                    case mValue.ObjectAttributes.Enum8:
                        {
                            //Obtain our enum properties.
                            mEnum8 enum8 = (mEnum8) ValueList[i];
                            //Check its visibility flags.
                            if (!showInvisibles)
                            {
                                if (enum8.Visible == false)
                                {
                                    //If its not supposed to be shown, skip to the next value
                                    break;
                                }
                            }
                            //Initialize our interface component
                            iEnum ienum = new iEnum(enum8);
                            //Set our items location
                            ienum.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ienum.Height;
                            //Add it to the parent panel
                            parentPanel.Controls.Add(ienum);
                            //Bring it to its appropriate location
                            ienum.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.Enum16:
                        {
                            //Obtain our enum properties.
                            mEnum16 enum16 = (mEnum16) ValueList[i];
                            //Check its visibility flags.
                            if (!showInvisibles)
                            {
                                if (enum16.Visible == false)
                                {
                                    //If its not supposed to be shown, skip to the next value
                                    break;
                                }
                            }
                            //Initialize our interface component
                            iEnum ienum = new iEnum(enum16);
                            //Set our items location
                            ienum.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ienum.Height;
                            //Add it to the parent panel
                            parentPanel.Controls.Add(ienum);
                            //Bring it to its appropriate location
                            ienum.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.Enum32:
                        {
                            //Obtain our enum properties.
                            mEnum32 enum32 = (mEnum32) ValueList[i];
                            //Check its visibility flags.
                            if (!showInvisibles)
                            {
                                if (enum32.Visible == false)
                                {
                                    //If its not supposed to be shown, skip to the next value
                                    break;
                                }
                            }
                            //Initialize our interface component
                            iEnum ienum = new iEnum(enum32);
                            //Set our items location
                            ienum.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ienum.Height;
                            //Add it to the parent panel
                            parentPanel.Controls.Add(ienum);
                            //Bring it to its appropriate location
                            ienum.BringToFront();
                            break;
                        }

                        #endregion

                        #region Integers

                    case mValue.ObjectAttributes.Byte:
                        {
                            //Obtain our value properties
                            mByte mbyte = (mByte) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (mbyte.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(mbyte);
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.Int16:
                        {
                            //Obtain our value properties
                            mInt16 int16 = (mInt16) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (int16.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(int16);
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.UInt16:
                        {
                            //Obtain our value properties
                            mUInt16 uint16 = (mUInt16) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (uint16.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(uint16);
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.Int32:
                        {
                            //Obtain our value properties
                            mInt32 int32 = (mInt32) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (int32.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(int32);
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.UInt32:
                        {
                            //Obtain our value properties
                            mUInt32 uint32 = (mUInt32) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (uint32.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(uint32);
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.Float:
                        {
                            //Obtain our value properties
                            mFloat mfloat = (mFloat) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (mfloat.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(mfloat);
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.Undefined:
                        {
                            //Obtain our value properties
                            mUndefined undefined = (mUndefined) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (undefined.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(undefined);
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }

                        #endregion

                        #region Unused

                    case mValue.ObjectAttributes.Unused:
                        {
                            //Obtain our value properties
                            mUnused unused = (mUnused) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                //Skip to the next value if it isn't to be shown.
                                break;
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iUnused iunused = new iUnused(unused);
                            //Set our items location
                            iunused.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += iunused.Height;
                            parentPanel.Controls.Add(iunused);
                            iunused.BringToFront();
                            break;
                        }

                        #endregion

                        #region Tag Attributes

                    case mValue.ObjectAttributes.TagRef:
                        {
                            #region TagRef (disabled)

                            /*
                            //Obtain our value properties
                            mTagRef tagref = (mTagRef)ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (tagref.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(tagref);
                            ivalue.Dock = DockStyle.Top;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();*/
                            break;

                            #endregion
                        }
                    case mValue.ObjectAttributes.Ident:
                        {
                            //Obtain our value properties
                            mIdent ident = (mIdent) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (ident.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iIdent ivalue = new iIdent(ident);
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.StringID:
                        {
                            //Obtain our value properties
                            mStringID stringID = (mStringID) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (stringID.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(stringID);
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }

                        #endregion

                        #region Strings

                    case mValue.ObjectAttributes.String32:
                        {
                            //Obtain our value properties
                            mString32 string32 = (mString32) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (string32.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(string32);
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.String256:
                        {
                            //Obtain our value properties
                            mString256 string256 = (mString256) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (string256.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(string256);
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.Unicode64:
                        {
                            //Obtain our value properties
                            mUnicode64 unicode64 = (mUnicode64) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (unicode64.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(unicode64);
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.Unicode256:
                        {
                            //Obtain our value properties
                            mUnicode256 unicode256 = (mUnicode256) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (unicode256.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(unicode256);
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }

                        #endregion
                }
            }
        }

        public static void LoadPluginValues(HaloMap Map, Panel parentPanel, int parentOffset)
        {
            //If the parentOffset is impossible
            if (parentOffset <= 0 | parentOffset > Map.MapHeader.fileSize)
            {
                //Return and stop the code
                return;
            }
            EndianIO IO = Map.IO;
            //Loop through all the controls.
            for (int i = 0; i < parentPanel.Controls.Count; i++)
            {
                //Make sure the IO is open
                if (!Map.IOisOpen)
                {
                    //If it isn't. Then open it
                    Map.OpenIO();
                }
                //Determine the type.
                switch (parentPanel.Controls[i].Name)
                {
                        //If its a...

                        #region Structure

                    case "iStruct":
                        {
                            //Get it as a value
                            iStruct istruct = (iStruct) parentPanel.Controls[i];
                            //Load the structure accordingly, and pass it the HaloMap class, and parentOffset
                            istruct.LoadStructure(Map, parentOffset);
                            break;
                        }

                        #endregion

                        #region Bitmask

                    case "iBitmask":
                        {
                            //Get it as a value
                            iBitmask ibitmask = (iBitmask) parentPanel.Controls[i];
                            //Load the value
                            ibitmask.LoadValue(Map, parentOffset);
                            break;
                        }

                        #endregion

                        #region Value

                    case "iValue":
                        {
                            iValue ival = (iValue) parentPanel.Controls[i];
                            ival.LoadValue(Map, parentOffset);
                            break;
                        }

                        #endregion

                        #region Enum

                    case "iEnum":
                        {
                            iEnum ienum = (iEnum) parentPanel.Controls[i];
                            ienum.LoadValue(Map, parentOffset);
                            break;
                        }

                        #endregion

                        #region Ident

                    case "iIdent":
                        {
                            iIdent iident = (iIdent) parentPanel.Controls[i];
                            iident.LoadValue(Map, parentOffset);
                            break;
                        }

                        #endregion
                }
            }
            //Close the IO
            Map.CloseIO();
        }

        public static void LoadNewPlugin(HaloMap Map, List<mValue> ValueList, Panel parentPanel, int parentOffset,
                                         bool showInvisibles)
        {
            int locationY = 0;
            //Loop through all the values
            for (int i = 0; i < ValueList.Count; i++)
            {
                //Make sure the IO is open
                if (!Map.IOisOpen)
                {
                    //If it isn't. Then open it
                    Map.OpenIO();
                }
                //Unfreeze our form
                //Application.DoEvents();
                //Lets determine our valueType by its attribute enum.
                switch (ValueList[i].Attributes)
                {
                        //If its a...

                        #region Reflexives

                    case mValue.ObjectAttributes.Reflexive:
                        {
                            //Obtain our instance of the value.
                            mReflexive reflexive = (mReflexive) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (reflexive.Visible == false)
                                {
                                    //If its not supposed to be shown, skip to the next value.
                                    break;
                                }
                            }
                            //Initialize our interface component using our value as a parameter.
                            iStruct istruct = new iStruct(reflexive);
                            if (parentPanel.Enabled)
                            {
                                //Load our structure.
                                istruct.LoadStructure(Map, parentOffset);
                            }
     
                                //Load the UserInteface recursively into the struct's panel.
                                LoadNewPlugin(Map, reflexive.Values, istruct.returnValuePanel(),
                                              istruct.ReflexiveData.Pointer, showInvisibles);
                            
                            //Resize the height
                            istruct.ResizeHeight();
                            //Set our items location
                            istruct.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += istruct.Height;
                            //Add the structure to the parent panel
                            parentPanel.Controls.Add(istruct);
                            //Bring it to the top of where it should be.
                            istruct.BringToFront();
                            break;
                        }

                        #endregion

                        #region Bitmasks

                    case mValue.ObjectAttributes.Bitmask8:
                        {
                            //Obtain our bitmask value
                            mBitmask8 bitmask8 = (mBitmask8) ValueList[i];
                            //Check our visibility flags
                            if (!showInvisibles)
                            {
                                if (bitmask8.Visible == false)
                                {
                                    //If its not supposed to be shown, skip to the next value.
                                    break;
                                }
                            }
                            //Initialize our interface component
                            iBitmask ibitmask = new iBitmask(bitmask8);
                            if (parentPanel.Enabled)
                            {
                                //Load our value
                                ibitmask.LoadValue(Map, parentOffset);
                            }
                            //Set our items location
                            ibitmask.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ibitmask.Height;
                            //Add it to the parent panel
                            parentPanel.Controls.Add(ibitmask);
                            //Bring it to its appropriate location
                            ibitmask.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.Bitmask16:
                        {
                            //Obtain our bitmask value
                            mBitmask16 bitmask16 = (mBitmask16) ValueList[i];
                            //Check our visibility flags
                            if (!showInvisibles)
                            {
                                if (bitmask16.Visible == false)
                                {
                                    //If its not supposed to be shown, skip to the next value.
                                    break;
                                }
                            }
                            //Initialize our interface component
                            iBitmask ibitmask = new iBitmask(bitmask16);
                            if (parentPanel.Enabled)
                            {
                                //Load our value
                                ibitmask.LoadValue(Map, parentOffset);
                            }
                            //Set our items location
                            ibitmask.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ibitmask.Height;
                            //Add it to the parent panel
                            parentPanel.Controls.Add(ibitmask);
                            //Bring it to its appropriate location
                            ibitmask.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.Bitmask32:
                        {
                            //Obtain our bitmask value
                            mBitmask32 bitmask32 = (mBitmask32) ValueList[i];
                            //Check our visibility flags
                            if (!showInvisibles)
                            {
                                if (bitmask32.Visible == false)
                                {
                                    //If its not supposed to be shown, skip to the next value.
                                    break;
                                }
                            }
                            //Initialize our interface component
                            iBitmask ibitmask = new iBitmask(bitmask32);
                            if (parentPanel.Enabled)
                            {
                                //Load our value
                                ibitmask.LoadValue(Map, parentOffset);
                            }
                            //Set our items location
                            ibitmask.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ibitmask.Height;
                            //Add it to the parent panel
                            parentPanel.Controls.Add(ibitmask);
                            //Bring it to its appropriate location
                            ibitmask.BringToFront();
                            break;
                        }

                        #endregion

                        #region Enums

                    case mValue.ObjectAttributes.Enum8:
                        {
                            //Obtain our enum properties.
                            mEnum8 enum8 = (mEnum8) ValueList[i];
                            //Check its visibility flags.
                            if (!showInvisibles)
                            {
                                if (enum8.Visible == false)
                                {
                                    //If its not supposed to be shown, skip to the next value
                                    break;
                                }
                            }
                            //Initialize our interface component
                            iEnum ienum = new iEnum(enum8);
                            if (parentPanel.Enabled)
                            {
                                //Load our value
                                ienum.LoadValue(Map, parentOffset);
                            }
                            //Set our items location
                            ienum.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ienum.Height;
                            //Add it to the parent panel
                            parentPanel.Controls.Add(ienum);
                            //Bring it to its appropriate location
                            ienum.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.Enum16:
                        {
                            //Obtain our enum properties.
                            mEnum16 enum16 = (mEnum16) ValueList[i];
                            //Check its visibility flags.
                            if (!showInvisibles)
                            {
                                if (enum16.Visible == false)
                                {
                                    //If its not supposed to be shown, skip to the next value
                                    break;
                                }
                            }
                            //Initialize our interface component
                            iEnum ienum = new iEnum(enum16);
                            if (parentPanel.Enabled)
                            {
                                //Load our value
                                ienum.LoadValue(Map, parentOffset);
                            }
                            //Set our items location
                            ienum.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ienum.Height;
                            //Add it to the parent panel
                            parentPanel.Controls.Add(ienum);
                            //Bring it to its appropriate location
                            ienum.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.Enum32:
                        {
                            //Obtain our enum properties.
                            mEnum32 enum32 = (mEnum32) ValueList[i];
                            //Check its visibility flags.
                            if (!showInvisibles)
                            {
                                if (enum32.Visible == false)
                                {
                                    //If its not supposed to be shown, skip to the next value
                                    break;
                                }
                            }
                            //Initialize our interface component
                            iEnum ienum = new iEnum(enum32);
                            if (parentPanel.Enabled)
                            {
                                //Load our value
                                ienum.LoadValue(Map, parentOffset);
                            }
                            //Set our items location
                            ienum.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ienum.Height;
                            //Add it to the parent panel
                            parentPanel.Controls.Add(ienum);
                            //Bring it to its appropriate location
                            ienum.BringToFront();
                            break;
                        }

                        #endregion

                        #region Integers

                    case mValue.ObjectAttributes.Byte:
                        {
                            //Obtain our value properties
                            mByte mbyte = (mByte) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (mbyte.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(mbyte);
                            if (parentPanel.Enabled)
                            {
                                //Load our value
                                ivalue.LoadValue(Map, parentOffset);
                            }
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.Int16:
                        {
                            //Obtain our value properties
                            mInt16 int16 = (mInt16) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (int16.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(int16);
                            if (parentPanel.Enabled)
                            {
                                //Load our value
                                ivalue.LoadValue(Map, parentOffset);
                            }
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.UInt16:
                        {
                            //Obtain our value properties
                            mUInt16 uint16 = (mUInt16) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (uint16.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(uint16);
                            if (parentPanel.Enabled)
                            {
                                //Load our value
                                ivalue.LoadValue(Map, parentOffset);
                            }
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.Int32:
                        {
                            //Obtain our value properties
                            mInt32 int32 = (mInt32) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (int32.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(int32);
                            if (parentPanel.Enabled)
                            {
                                //Load our value
                                ivalue.LoadValue(Map, parentOffset);
                            }
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.UInt32:
                        {
                            //Obtain our value properties
                            mUInt32 uint32 = (mUInt32) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (uint32.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(uint32);
                            if (parentPanel.Enabled)
                            {
                                //Load our value
                                ivalue.LoadValue(Map, parentOffset);
                            }
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.Float:
                        {
                            //Obtain our value properties
                            mFloat mfloat = (mFloat) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (mfloat.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(mfloat);
                            if (parentPanel.Enabled)
                            {
                                //Load our value
                                ivalue.LoadValue(Map, parentOffset);
                            }
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.Undefined:
                        {
                            //Obtain our value properties
                            mUndefined undefined = (mUndefined) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (undefined.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(undefined);
                            if (parentPanel.Enabled)
                            {
                                //Load our value
                                ivalue.LoadValue(Map, parentOffset);
                            }
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }

                        #endregion

                        #region Unused

                    case mValue.ObjectAttributes.Unused:
                        {
                            //Obtain our value properties
                            mUnused unused = (mUnused) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                //Skip to the next value if it isn't to be shown.
                                break;
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iUnused iunused = new iUnused(unused);
                            //Set our items location
                            iunused.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += iunused.Height;
                            parentPanel.Controls.Add(iunused);
                            iunused.BringToFront();
                            break;
                        }

                        #endregion

                        #region Tag Attributes

                    case mValue.ObjectAttributes.TagRef:
                        {
                            #region TagRef (disabled)

                            /*
                            //Obtain our value properties
                            mTagRef tagref = (mTagRef)ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (tagref.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(tagref);
                            ivalue.Dock = DockStyle.Top;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();*/
                            break;

                            #endregion
                        }
                    case mValue.ObjectAttributes.Ident:
                        {
                            //Obtain our value properties
                            mIdent ident = (mIdent) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (ident.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iIdent ivalue = new iIdent(ident);
                            if (parentPanel.Enabled)
                            {
                                //Load our value
                                ivalue.LoadValue(Map, parentOffset);
                            }
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.StringID:
                        {
                            //Obtain our value properties
                            mStringID stringID = (mStringID) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (stringID.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(stringID);
                            if (parentPanel.Enabled)
                            {
                                //Load our value
                                ivalue.LoadValue(Map, parentOffset);
                            }
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }

                        #endregion

                        #region Strings

                    case mValue.ObjectAttributes.String32:
                        {
                            //Obtain our value properties
                            mString32 string32 = (mString32) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (string32.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(string32);
                            if (parentPanel.Enabled)
                            {
                                //Load our value
                                ivalue.LoadValue(Map, parentOffset);
                            }
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.String256:
                        {
                            //Obtain our value properties
                            mString256 string256 = (mString256) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (string256.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(string256);
                            if (parentPanel.Enabled)
                            {
                                //Load our value
                                ivalue.LoadValue(Map, parentOffset);
                            }
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.Unicode64:
                        {
                            //Obtain our value properties
                            mUnicode64 unicode64 = (mUnicode64) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (unicode64.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(unicode64);
                            if (parentPanel.Enabled)
                            {
                                //Load our value
                                ivalue.LoadValue(Map, parentOffset);
                            }
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }
                    case mValue.ObjectAttributes.Unicode256:
                        {
                            //Obtain our value properties
                            mUnicode256 unicode256 = (mUnicode256) ValueList[i];
                            //Check the visibility flags
                            if (!showInvisibles)
                            {
                                if (unicode256.Visible == false)
                                {
                                    //Skip to the next value if it isn't to be shown.
                                    break;
                                }
                            }
                            //Initialize our value instance, dock it, add it to the parent panel, and bring it to its appropriate location
                            iValue ivalue = new iValue(unicode256);
                            if (parentPanel.Enabled)
                            {
                                //Load our value
                                ivalue.LoadValue(Map, parentOffset);
                            }
                            //Set our items location
                            ivalue.Location = new Point(0, locationY);
                            //Add to our location
                            locationY += ivalue.Height;
                            parentPanel.Controls.Add(ivalue);
                            ivalue.BringToFront();
                            break;
                        }

                        #endregion
                }
            }
        }

        public static void SaveChangedValues(HaloMap Map, Panel parentPanel, int parentOffset)
        {
            EndianIO IO = Map.IO;
            //Loop through all the controls.
            for (int i = 0; i < parentPanel.Controls.Count; i++)
            {
                //Make sure the IO is open
                if (!Map.IOisOpen)
                {
                    //If it isn't. Then open it
                    Map.OpenIO();
                }
                //Determine the type.
                switch (parentPanel.Controls[i].Name)
                {
                        //If its a...

                        #region Structure

                    case "iStruct":
                        {
                            //Get it as a value
                            iStruct istruct = (iStruct) parentPanel.Controls[i];
                            //Save the structure accordingly, and pass it the HaloMap class, and parentOffset
                            istruct.SaveStructure(Map, parentOffset);
                            break;
                        }

                        #endregion

                        #region Bitmask

                    case "iBitmask":
                        {
                            //Get it as a value
                            iBitmask ibitmask = (iBitmask) parentPanel.Controls[i];
                            //Save the value
                            if (ibitmask.Editted)
                            {
                                ibitmask.SaveValue(Map.IO, parentOffset);
                            }
                            break;
                        }

                        #endregion

                        #region Value

                    case "iValue":
                        {
                            iValue ival = (iValue) parentPanel.Controls[i];
                            if (ival.Editted)
                            {
                                ival.SaveValue(Map.IO, parentOffset);
                            }
                            break;
                        }

                        #endregion

                        #region Enum

                    case "iEnum":
                        {
                            iEnum ienum = (iEnum) parentPanel.Controls[i];
                            if (ienum.Editted)
                            {
                                ienum.SaveValue(Map.IO, parentOffset);
                            }
                            break;
                        }

                        #endregion

                        #region Ident

                    case "iIdent":
                        {
                            iIdent iident = (iIdent) parentPanel.Controls[i];
                            if (iident.Editted)
                            {
                                iident.SaveValue(Map.IO, parentOffset);
                            }
                            break;
                        }

                        #endregion
                }
            }
            Map.CloseIO();
        }

        public static void PokeValues(EndianIO IO, Panel parentPanel, int parentOffset, int magic, bool onlyChanged)
        {
            //Loop through all the controls.
            for (int i = 0; i < parentPanel.Controls.Count; i++)
            {
                //Determine the type.
                switch (parentPanel.Controls[i].Name)
                {
                        //If its a...

                        #region Structure

                    case "iStruct":
                        {
                            //Get it as a value
                            iStruct istruct = (iStruct) parentPanel.Controls[i];
                            //Save the structure accordingly, and pass it the HaloMap class, and parentOffset
                            istruct.PokeStructure(IO, magic, onlyChanged);
                            break;
                        }

                        #endregion

                        #region Bitmask

                    case "iBitmask":
                        {
                            //Get it as a value
                            iBitmask ibitmask = (iBitmask) parentPanel.Controls[i];
                            //Save the value
                            if (onlyChanged)
                            {
                                if (ibitmask.Editted)
                                {
                                    ibitmask.SaveValue(IO, parentOffset + magic);
                                }
                            }
                            else
                            {
                                ibitmask.SaveValue(IO, parentOffset + magic);
                            }
                            break;
                        }

                        #endregion

                        #region Value

                    case "iValue":
                        {
                            iValue ival = (iValue) parentPanel.Controls[i];
                            if (onlyChanged)
                            {
                                if (ival.Editted)
                                {
                                    ival.SaveValue(IO, parentOffset + magic);
                                }
                            }
                            else
                            {
                                ival.SaveValue(IO, parentOffset + magic);
                            }
                            break;
                        }

                        #endregion

                        #region Enum

                    case "iEnum":
                        {
                            iEnum ienum = (iEnum) parentPanel.Controls[i];
                            if (onlyChanged)
                            {
                                if (ienum.Editted)
                                {
                                    ienum.SaveValue(IO, parentOffset + magic);
                                }
                            }
                            else
                            {
                                ienum.SaveValue(IO, parentOffset + magic);
                            }
                            break;
                        }

                        #endregion

                        #region Ident

                    case "iIdent":
                        {
                            iIdent iident = (iIdent) parentPanel.Controls[i];
                            if (onlyChanged)
                            {
                                if (iident.Editted)
                                {
                                    iident.SaveValue(IO, parentOffset + magic);
                                }
                            }
                            else
                            {
                                iident.SaveValue(IO, parentOffset + magic);
                            }
                            break;
                        }

                        #endregion
                }
            }
        }

        public static void BuildRTHData(RTHData RTH_Data, Panel parentPanel, int parentOffset, int magic,
                                        bool onlyChanged)
        {
            //Loop through all the controls.
            for (int i = 0; i < parentPanel.Controls.Count; i++)
            {
                //Determine the type.
                switch (parentPanel.Controls[i].Name)
                {
                        //If its a...

                        #region Structure

                    case "iStruct":
                        {
                            //Get it as a value
                            iStruct istruct = (iStruct) parentPanel.Controls[i];
                            //Save the structure accordingly, and pass it the HaloMap class, and parentOffset
                            istruct.BuildRTHData(RTH_Data, magic, onlyChanged);
                            break;
                        }

                        #endregion

                        #region Bitmask

                    case "iBitmask":
                        {
                            //Get it as a value
                            iBitmask ibitmask = (iBitmask) parentPanel.Controls[i];
                            //Save the value
                            if (onlyChanged)
                            {
                                if (ibitmask.Editted)
                                {
                                    RTH_Data.RTH_Data_Blocks.Add(ibitmask.ReturnRTHDataBlock(parentOffset + magic));
                                }
                            }
                            else
                            {
                                RTH_Data.RTH_Data_Blocks.Add(ibitmask.ReturnRTHDataBlock(parentOffset + magic));
                            }
                            break;
                        }

                        #endregion

                        #region Value

                    case "iValue":
                        {
                            iValue ival = (iValue) parentPanel.Controls[i];
                            if (onlyChanged)
                            {
                                if (ival.Editted)
                                {
                                    RTH_Data.RTH_Data_Blocks.Add(ival.ReturnRTHDataBlock(parentOffset + magic));
                                }
                            }
                            else
                            {
                                RTH_Data.RTH_Data_Blocks.Add(ival.ReturnRTHDataBlock(parentOffset + magic));
                            }
                            break;
                        }

                        #endregion

                        #region Enum

                    case "iEnum":
                        {
                            iEnum ienum = (iEnum) parentPanel.Controls[i];
                            if (onlyChanged)
                            {
                                if (ienum.Editted)
                                {
                                    RTH_Data.RTH_Data_Blocks.Add(ienum.ReturnRTHDataBlock(parentOffset + magic));
                                }
                            }
                            else
                            {
                                RTH_Data.RTH_Data_Blocks.Add(ienum.ReturnRTHDataBlock(parentOffset + magic));
                            }
                            break;
                        }

                        #endregion

                        #region Ident

                    case "iIdent":
                        {
                            iIdent iident = (iIdent) parentPanel.Controls[i];
                            if (onlyChanged)
                            {
                                if (iident.Editted)
                                {
                                    RTH_Data.RTH_Data_Blocks.Add(iident.ReturnRTHDataBlock(parentOffset + magic));
                                }
                            }
                            else
                            {
                                RTH_Data.RTH_Data_Blocks.Add(iident.ReturnRTHDataBlock(parentOffset + magic));
                            }
                            break;
                        }

                        #endregion
                }
            }
        }

        public static void SetAllAsEditted(HaloMap Map, Panel parentPanel, int parentOffset)
        {
            //If the parentOffset is impossible
            if (parentOffset <= 0 | parentOffset > Map.MapHeader.fileSize)
            {
                //Return and stop the code
                return;
            }
            EndianIO IO = Map.IO;
            //Loop through all the controls.
            for (int i = 0; i < parentPanel.Controls.Count; i++)
            {
                //Make sure the IO is open
                if (!Map.IOisOpen)
                {
                    //If it isn't. Then open it
                    Map.OpenIO();
                }
                //Determine the type.
                switch (parentPanel.Controls[i].Name)
                {
                    //If its a...


                    #region Bitmask

                    case "iBitmask":
                        {
                            //Get it as a value
                            iBitmask ibitmask = (iBitmask)parentPanel.Controls[i];
                           
                            //Set it's editted value
                            ibitmask.Editted = true;
                            break;
                        }

                    #endregion

                    #region Value

                    case "iValue":
                        {
                            iValue ival = (iValue)parentPanel.Controls[i];

                            //Set it's editted value
                            ival.Editted = true;
                            break;
                        }

                    #endregion

                    #region Enum

                    case "iEnum":
                        {
                            iEnum ienum = (iEnum)parentPanel.Controls[i];
                            //Set it's editted value
                            ienum.Editted = true;
                            break;
                        }

                    #endregion

                    #region Ident

                    case "iIdent":
                        {
                            iIdent iident = (iIdent)parentPanel.Controls[i];
                            //Set it's editted value
                            iident.Editted = true;
                            break;
                        }

                    #endregion
                }
            }
            //Close the IO
            Map.CloseIO();
        }

    }
}
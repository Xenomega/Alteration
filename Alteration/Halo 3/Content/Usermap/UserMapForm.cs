using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using HaloDeveloper.Map;

namespace Alteration.Halo_3.Content.Usermap
{
    public partial class UserMapForm : Form
    {
        private HaloMap _map;
        private HaloUserMap.PalletteBlock _selectedblock;
        private HaloUserMap _usermap;

        public UserMapForm(HaloUserMap usermap, HaloMap map)
        {
            InitializeComponent();
            //Set our instance of the usermap
            Usermap = usermap;
            //Set our instance of the HaloMap class
            Map = map;
            //Load our variant information
            LoadVariantInformation();
            //Load the tag tree
            LoadTagTree();
            //Load classes
            LoadClasses();
            //Set the forms text
            Text = Usermap.FileName;
        }

        public HaloUserMap Usermap
        {
            get { return _usermap; }
            set { _usermap = value; }
        }

        public HaloMap Map
        {
            get { return _map; }
            set { _map = value; }
        }

        public HaloUserMap.PalletteBlock Selected_Block
        {
            get { return _selectedblock; }
            set { _selectedblock = value; }
        }

        private void LoadTagTree()
        {
            //Clear all treenodes
            tvTagReferences.Nodes.Clear();
            //Create our pallete Node
            TreeNode palleteNode = new TreeNode(Usermap.Map_Variant_Header.Variant_Name);
            //Loop through all the palletes
            for (int i = 0; i < Usermap.Pallette_Blocks.Count; i++)
            {
                //Get our tag index for this pallete
                int tagIndex = Map.GetTagIndexByIdent(Usermap.Pallette_Blocks[i].Tag_Ident);
                //Get our tag name and class
                string tagName = "Empty Slot";
                string tagClass = "";
                //If our tagindex isnt null
                if (tagIndex != -1)
                {
                    //Assign our tagName
                    tagName = Map.IndexItems[tagIndex].Name;
                    //Assign our tagClass
                    tagClass = Map.IndexItems[tagIndex].Class;
                }

                //If its a blank slot
                if (tagIndex == -1)
                {
                    //Initialize our new treeNode
                    TreeNode blankSlotNode = new TreeNode(tagName);
                    //Set its tag as its index in the palletes
                    blankSlotNode.Tag = i;
                    //Add it to the palletes
                    palleteNode.Nodes.Add(blankSlotNode);
                }
                    //Otherwise
                else
                {
                    //Loop through the current nodes
                    int nodeIndex = -1;
                    //For each treenode in the child nodes
                    foreach (TreeNode node in palleteNode.Nodes)
                    {
                        //If the node's text is that of the tagclass
                        if (node.Text == "~" + tagClass)
                        {
                            //Set our index
                            nodeIndex = node.Index;
                        }
                    }

                    //Create an instance of the treeNode to add to
                    TreeNode parentClassNode = null;
                    //If we found a node that exists
                    if (nodeIndex != -1)
                    {
                        //Assign it
                        parentClassNode = palleteNode.Nodes[nodeIndex];
                    }
                        //Otherwise
                    else
                    {
                        //Initialize it
                        parentClassNode = new TreeNode("~" + tagClass);
                        //Set its tag
                        parentClassNode.Tag = "ParentClassNode";
                        //Add it to the list
                        palleteNode.Nodes.Add(parentClassNode);
                    }

                    //Initialize our new treeNode to add to it.
                    TreeNode itemNode = new TreeNode(tagName);
                    //Set its tag
                    itemNode.Tag = i;
                    //Add it to the parentNode
                    parentClassNode.Nodes.Add(itemNode);
                }
            }
            //Add the pallete node to the tree
            tvTagReferences.Nodes.Add(palleteNode);
            //Sort the tree
            tvTagReferences.Sort();
            //Expand the first node
            palleteNode.Expand();
        }

        private void LoadVariantInformation()
        {
            //Assign our Variant name
            txtVariantName.Text = Usermap.Map_Variant_Header.Variant_Name;
            //Assign our Variant Description
            txtVariantDescription.Text = Usermap.Map_Variant_Header.Variant_Description;
            //Assign our Variant Author
            txtVariantAuthor.Text = Usermap.Map_Variant_Header.Variant_Author;
        }

        private void SaveVariantInformation()
        {
            //Assign the variant name
            Usermap.Map_Variant_Header.Variant_Name = txtVariantName.Text;
            //Assign the variant description
            Usermap.Map_Variant_Header.Variant_Description = txtVariantDescription.Text;
            //Assign the variant author
            Usermap.Map_Variant_Header.Variant_Author = txtVariantAuthor.Text;

            //Save The Map Variant Header
            Usermap.Map_Variant_Header.SaveMapVariantHeader(Usermap);
        }

        private void UserMapForm_Load(object sender, EventArgs e)
        {
        }

        private void LoadSelectedPallete()
        {
            //If we have an item selected
            if (tvTagReferences.SelectedNode != null)
            {
                //If the item has a tag
                if (tvTagReferences.SelectedNode.Tag != null)
                {
                    //If its not a tagclass parent node
                    if (tvTagReferences.SelectedNode.Tag.ToString() != "ParentClassNode")
                    {
                        //Get the index for the tag
                        int palleteIndex = (int) tvTagReferences.SelectedNode.Tag;

                        #region Tag Ident

                        //Get our tag index
                        int tagIndex = Map.GetTagIndexByIdent(Usermap.Pallette_Blocks[palleteIndex].Tag_Ident);
                        //If our tagIndex isnt -1
                        if (tagIndex != -1)
                        {
                            //Get our tag item instance
                            HaloMap.TagItem tagItem = Map.IndexItems[tagIndex];
                            //Select our class
                            SelectItem(cmbxTagClass, tagItem.Class);
                            //Select our tagName
                            SelectItem(cmbxTagName, tagItem.Name);
                        }
                            //Otherwise..
                        else
                        {
                            //Select our first item.
                            cmbxTagClass.SelectedIndex = 0;
                        }

                        #endregion

                        #region Pallete Values

                        //Set our runtimes text.
                        txtRuntimeMinimum.Text = Usermap.Pallette_Blocks[palleteIndex].Run_Time_Minimum.ToString();
                        txtRuntimeMaximum.Text = Usermap.Pallette_Blocks[palleteIndex].Run_Time_Maximum.ToString();
                        //Set our total cost
                        txtTotalCost.Text = Usermap.Pallette_Blocks[palleteIndex].Total_Cost.ToString();

                        #endregion

                        #region Load Our Placements Into the Combobox

                        //Clear our combobox items
                        cmbxPlacementChunk.Items.Clear();
                        cmbxPlacementChunk.Text = "";
                        //Disable our gpnl for placements
                        gpnlPlacements.Enabled = false;
                        //If our tagIndex isnt -1
                        if (palleteIndex != -1)
                        {
                            //Get our currently selected pallete block
                            Selected_Block = Usermap.Pallette_Blocks[palleteIndex];
                            //Loop for each placement
                            for (int i = 0; i < Selected_Block.Placement_Blocks.Count; i++)
                            {
                                //Add our chunk item.
                                cmbxPlacementChunk.Items.Add(i + " : Chunk");
                            }
                            //If we have any items
                            if (cmbxPlacementChunk.Items.Count > 0)
                            {
                                //Select the first item.
                                cmbxPlacementChunk.SelectedIndex = 0;
                                //Enable our gpnl for placements
                                gpnlPlacements.Enabled = true;
                            }
                        }

                        #endregion
                    }
                }
            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            //Save Map Variant Header information
            SaveVariantInformation();
            //If we have an item selected
            if (Selected_Block != null)
            {
                //Set our selected blocks information
                Selected_Block.Run_Time_Minimum = byte.Parse(txtRuntimeMinimum.Text);
                Selected_Block.Run_Time_Maximum = byte.Parse(txtRuntimeMaximum.Text);
                Selected_Block.Total_Cost = byte.Parse(txtTotalCost.Text);
                //Save all our palletes
                Usermap.Pallette_Blocks.SavePalleteBlocks(Usermap);
                //If our placement block panel is enabled
                if (gpnlPlacements.Enabled)
                {
                    //Get our selected placement
                    HaloUserMap.PlacementBlock selectedPlacementBlock =
                        Selected_Block.Placement_Blocks[cmbxPlacementChunk.SelectedIndex];
                    //Set it's information
                    selectedPlacementBlock.X = float.Parse(txtCordX.Text);
                    selectedPlacementBlock.Y = float.Parse(txtCordY.Text);
                    selectedPlacementBlock.Z = float.Parse(txtCordZ.Text);
                    selectedPlacementBlock.Yaw = float.Parse(txtCordYaw.Text);
                    selectedPlacementBlock.Pitch = float.Parse(txtCordPitch.Text);
                    selectedPlacementBlock.Roll = float.Parse(txtCordRoll.Text);
                    selectedPlacementBlock.Flag_Asymmetrical = chkAsymmetrical.Checked;
                    selectedPlacementBlock.Flag_Place_At_Start = chkPlaceAtStart.Checked;
                    selectedPlacementBlock.Flag_Symmetrical = chkSymmetrical.Checked;
                    //Assign our block type
                    switch (cmbxChunkType.SelectedItem.ToString())
                    {
                        case "Player Spawn":
                            {
                                selectedPlacementBlock.Block_Type = HaloUserMap.PlacementBlock.BlockType.Player_Spawn;
                                break;
                            }
                        case "Reserved":
                            {
                                selectedPlacementBlock.Block_Type = HaloUserMap.PlacementBlock.BlockType.Reserved;
                                break;
                            }
                        case "Added":
                            {
                                selectedPlacementBlock.Block_Type = HaloUserMap.PlacementBlock.BlockType.Added;
                                break;
                            }
                        case "Original":
                            {
                                selectedPlacementBlock.Block_Type = HaloUserMap.PlacementBlock.BlockType.Original;
                                break;
                            }
                        case "Editted":
                            {
                                selectedPlacementBlock.Block_Type = HaloUserMap.PlacementBlock.BlockType.Edited;
                                break;
                            }
                        case "Null":
                            {
                                selectedPlacementBlock.Block_Type = HaloUserMap.PlacementBlock.BlockType.NULL;
                                break;
                            }
                    }
                    //Save all our placement blocks
                    Usermap.Placement_Blocks.SavePlacementBlocks(Usermap);
                }
            }
            //Messagebox
            MessageBox.Show("Done.");
        }

        private void LoadClasses()
        {
            //Initialize our list of treeNodes
            List<string> tagClasses = new List<string>();
            for (int i = 0; i < Map.IndexItems.Count; i++)
            {
                //Search to see if this tagClass is already initialized in a treenode.

                //So we start with an index of -1(null)
                int index = -1;
                //Loop through all the tagClasses
                for (int z = 0; z < tagClasses.Count; z++)
                {
                    //If the tagClass is that of the tagClass we're looking for
                    if (tagClasses[z] == Map.IndexItems[i].Class)
                    {
                        //Set our index of the tagClass
                        index = z;
                        //Break out of the loop.
                        break;
                    }
                }
                //If the treeNode doesn't exist...
                if (index == -1)
                {
                    //Add the tag to it.
                    tagClasses.Add(Map.IndexItems[i].Class);
                }
            }
            //Clear the tagClasscomboBox
            cmbxTagClass.Items.Clear();
            //Loop through the tagClasses
            for (int i = 0; i < tagClasses.Count; i++)
            {
                //Add the tagClass to the comboBox
                cmbxTagClass.Items.Add(tagClasses[i]);
            }
            //Sort the items
            cmbxTagClass.Sorted = true;
        }

        private void SelectItem(ComboBoxEx comboBox, string text)
        {
            //Loop through the items of the combobox
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                //If the indexed item's text is that of the provided text...
                if (comboBox.Items[i].ToString() == text)
                {
                    //...Then select it
                    comboBox.SelectedIndex = i;
                    //Break out of the loop
                    break;
                }
            }
        }

        private void tvTagReferences_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Load our selected item if any
            LoadSelectedPallete();
        }

        private void cmbxTagClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clear the tag name comboBox
            cmbxTagName.Items.Clear();
            //Loop through all the tags of the HaloMap class
            for (int i = 0; i < Map.IndexHeader.tagCount; i++)
            {
                //If the tag's name matches the class selected...
                if (Map.IndexItems[i].Class == cmbxTagClass.Text)
                {
                    //...Add the tag name instance
                    cmbxTagName.Items.Add(Map.IndexItems[i].Name);
                }
            }
            //Sort the comboBox
            cmbxTagName.Sorted = true;
            //Set it to not sort anymore so we can add null to the bottom.
            cmbxTagName.Sorted = false;
            //Add the null instance.
            cmbxTagName.Items.Add("Null");
            //Select the last item
            cmbxTagName.SelectedIndex = cmbxTagName.Items.Count - 1;
        }

        private void RefreshTags()
        {
            //Initialize our integer to hold the selectedplacement index
            int selectedPlaceIndex = -1;
            //If our placements is enabled.
            if (gpnlPlacements.Enabled)
            {
                //Assign our placement index.
                selectedPlaceIndex = cmbxPlacementChunk.SelectedIndex;
            }
            //Load the tag tree
            LoadTagTree();
            //if our most recently selected block isn't null
            if (Selected_Block != null)
            {
                //Loop through all treenodes..
                for (int i = 0; i < tvTagReferences.Nodes.Count; i++)
                {
                    //If it's the node we're looking for..
                    if (int.Parse(tvTagReferences.Nodes[i].Tag.ToString()) == Selected_Block.Index)
                    {
                        //Select it.
                        tvTagReferences.SelectedNode = tvTagReferences.Nodes[i];
                        //And break out of the loop
                        break;
                    }
                    //Loop through all treenodes' treenodes...
                    for (int z = 0; z < tvTagReferences.Nodes[i].Nodes.Count; z++)
                    {
                        //If it's the node we're looking for..
                        if (int.Parse(tvTagReferences.Nodes[i].Nodes[z].Tag.ToString()) == Selected_Block.Index)
                        {
                            //Select it.
                            tvTagReferences.SelectedNode = tvTagReferences.Nodes[i].Nodes[z];
                            //And break out of the loop
                            break;
                        }
                    }
                }

                //If our placement block index isn't null
                if (selectedPlaceIndex != -1 && cmbxPlacementChunk.Items.Count != 0)
                {
                    //Select our selected chunk.
                    cmbxPlacementChunk.SelectedIndex = selectedPlaceIndex;
                }
            }
        }

        private void cmbxPlacementChunk_SelectedIndexChanged(object sender, EventArgs e)
        {
            //If we have a selected block
            if (Selected_Block != null)
            {
                //Get our placement block
                HaloUserMap.PlacementBlock selectedPlacementBlock =
                    Selected_Block.Placement_Blocks[cmbxPlacementChunk.SelectedIndex];
                //Load our information into the appropriate boxes
                txtCordX.Text = selectedPlacementBlock.X.ToString();
                txtCordY.Text = selectedPlacementBlock.Y.ToString();
                txtCordZ.Text = selectedPlacementBlock.Z.ToString();
                txtCordYaw.Text = selectedPlacementBlock.Yaw.ToString();
                txtCordPitch.Text = selectedPlacementBlock.Pitch.ToString();
                txtCordRoll.Text = selectedPlacementBlock.Roll.ToString();
                chkAsymmetrical.Checked = selectedPlacementBlock.Flag_Asymmetrical;
                chkSymmetrical.Checked = selectedPlacementBlock.Flag_Symmetrical;
                chkPlaceAtStart.Checked = selectedPlacementBlock.Flag_Place_At_Start;
                //Load our block type
                switch (selectedPlacementBlock.Block_Type)
                {
                    case HaloUserMap.PlacementBlock.BlockType.Player_Spawn:
                        {
                            cmbxChunkType.SelectedIndex = 0;
                            break;
                        }
                    case HaloUserMap.PlacementBlock.BlockType.Reserved:
                        {
                            cmbxChunkType.SelectedIndex = 1;
                            break;
                        }
                    case HaloUserMap.PlacementBlock.BlockType.Added:
                        {
                            cmbxChunkType.SelectedIndex = 2;
                            break;
                        }
                    case HaloUserMap.PlacementBlock.BlockType.Original:
                        {
                            cmbxChunkType.SelectedIndex = 3;
                            break;
                        }
                    case HaloUserMap.PlacementBlock.BlockType.Edited:
                        {
                            cmbxChunkType.SelectedIndex = 4;
                            break;
                        }
                    case HaloUserMap.PlacementBlock.BlockType.NULL:
                        {
                            cmbxChunkType.SelectedIndex = 5;
                            break;
                        }
                    default:
                        {
                            cmbxChunkType.SelectedIndex = 4;
                            break;
                        }
                }
            }
        }
    }
}
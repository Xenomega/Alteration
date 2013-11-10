using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HaloDeveloper.Map;

namespace HaloDeveloper.Raw.ModelRaw
{
    public partial class ModelExtractor : UserControl
    {
        ModelInfo mi;
        HaloMap map;
        int tagIndex;

        public ModelExtractor()
        {
            InitializeComponent();
        }

        public void LoadModelTag(HaloMap Map, int TagIndex)
        {
            map = Map;
            tagIndex = TagIndex;

            // Load our model info for this tag
            mi = ModelFunctions.GetModelInfo(map, tagIndex);

            label1.Text = mi.Name;

            // Now lets clear our treeview
            treeView1.Nodes.Clear();

            // Now lets add our submaps to it
            for (int x = 0; x < mi.Regions.Count; x++)
            {
                TreeNode region = new TreeNode(mi.Regions[x].Name);
                for (int y = 0; y < mi.Regions[x].Permutations.Count; y++)
                {
                    TreeNode perm = new TreeNode(mi.Regions[x].Permutations[y].Name);
                    perm.Checked = true;
                    region.Nodes.Add(perm);
                }
                region.Checked = true;
                treeView1.Nodes.Add(region);
            }
        }

        bool isWorking = false;
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (isWorking)
                return;

            isWorking = true;

            TreeNode node = e.Node;

            // This is a region
            if (node.Parent == null)
            {
                if (!node.Checked)
                {
                    for (int x = 0; x < node.Nodes.Count; x++)
                    {
                        node.Nodes[x].Checked = false;
                    }
                }
                else
                {
                    if (node.Nodes.Count > 0)
                        node.Nodes[0].Checked = true;
                }
            }
            else
            {
                // This is a perm
                if (node.Checked)
                    node.Parent.Checked = true;
                else
                {
                    // We are unchecked so lets see if we even have any selected
                    bool has1 = false;
                    for (int x = 0; x < node.Parent.Nodes.Count; x++)
                    {
                        if (node.Parent.Nodes[x].Checked)
                        {
                            has1 = true;
                            break;
                        }
                    }
                    node.Parent.Checked = has1;
                }
            }
            isWorking = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = mi.Name;
            //sfd.Filter = "EMF (*.emf)|*.emf|OBJ (*.obj)|*.obj";
            sfd.Filter = "EMF (*.emf)|*.emf|Text (*.txt)|*.txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (sfd.FilterIndex == 1)
                {
                    ModelFunctions.WriteEMF(sfd.FileName,
                        mi, Permutation.LOD.Highest, checkBox1.Checked,
                        checkBox2.Checked, treeView1);
                }
                else if (sfd.FilterIndex == 2)
                {
                    ModelFunctions.WriteTxt(sfd.FileName,
                        mi, Permutation.LOD.Highest);
                }
                else
                {
                    // See if they have any boxes checked
                    if (checkBox1.Checked || checkBox2.Checked)
                        MessageBox.Show("You have selected OBJ and no Makers or Nodes will be exported.");

                }
                MessageBox.Show("Done!");
            }
        }
    }
}

using System.Windows.Forms;
using Alteration.Halo_3.Values;
using DevComponents.DotNetBar;
using System;

namespace Alteration.Halo_3.Meta_Editor.Controls
{
    public partial class iUnused : UserControl
    {
        private mUnused _unuseddata;

        public iUnused()
        {
            InitializeComponent();
        }

        public iUnused(mUnused unuseddata)
        {
            InitializeComponent();
            //Set our unused data
            UnusedData = unuseddata;

            //Set our tooltip name.
            ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText =
                "Type: " + UnusedData.Attributes.ToString() + Environment.NewLine +
                "Offset: " + UnusedData.Offset.ToString() + Environment.NewLine +
                "Size: " + UnusedData.Size.ToString();

            //Loop through each control..
            for (int i = 0; i < this.Controls.Count; i++)
            {
                //Set the super tool tip info.
                try
                {
                    ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this.Controls[i]]).HeaderText = ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).HeaderText;
                    ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this.Controls[i]]).BodyText = ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText;
                    ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this.Controls[i]]).FooterVisible = false;
                }
                catch
                {
                    this.superTooltip1.SetSuperTooltip(this.Controls[i], new DevComponents.DotNetBar.SuperTooltipInfo(((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).HeaderText, "", ((SuperTooltipInfo)superTooltip1.SuperTooltipInfoTable[this]).BodyText, null, null, DevComponents.DotNetBar.eTooltipColor.System, true, false, new System.Drawing.Size(0, 0)));

                }
            }


            //Change our unused description on the UI to display offset and size
            lblUnusedDescription.Text = "unused data {offset =" + UnusedData.Offset + ", size=" + UnusedData.Size + "}";
        }

        public mUnused UnusedData
        {
            get { return _unuseddata; }
            set { _unuseddata = value; }
        }
    }
}
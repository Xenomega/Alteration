namespace Alteration.Halo_3.Meta_Editor.Controls
{
    partial class iEnum
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblValueType = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewValueAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
            this.cmbxSelections = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblValueType
            // 
            this.lblValueType.AutoSize = true;
            this.lblValueType.Location = new System.Drawing.Point(365, 8);
            this.lblValueType.Name = "lblValueType";
            this.lblValueType.Size = new System.Drawing.Size(33, 13);
            this.lblValueType.TabIndex = 5;
            this.lblValueType.Text = "enum";
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(3, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(200, 23);
            this.superTooltip1.SetSuperTooltip(this.lblName, new DevComponents.DotNetBar.SuperTooltipInfo("Label", "", "The label/name of the value.", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, true, false, new System.Drawing.Size(0, 0)));
            this.lblName.TabIndex = 3;
            this.lblName.Text = "label1";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewValueAsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(164, 26);
            // 
            // viewValueAsToolStripMenuItem
            // 
            this.viewValueAsToolStripMenuItem.Name = "viewValueAsToolStripMenuItem";
            this.viewValueAsToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.viewValueAsToolStripMenuItem.Text = "View Value As...";
            this.viewValueAsToolStripMenuItem.Click += new System.EventHandler(this.viewValueAsToolStripMenuItem_Click);
            // 
            // cmbxSelections
            // 
            this.cmbxSelections.DisplayMember = "Text";
            this.cmbxSelections.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbxSelections.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxSelections.FormattingEnabled = true;
            this.cmbxSelections.ItemHeight = 14;
            this.cmbxSelections.Location = new System.Drawing.Point(209, 5);
            this.cmbxSelections.Name = "cmbxSelections";
            this.cmbxSelections.Size = new System.Drawing.Size(150, 20);
            this.cmbxSelections.TabIndex = 6;
            this.cmbxSelections.SelectedIndexChanged += new System.EventHandler(this.cmbxSelections_SelectedIndexChanged);
            // 
            // iEnum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.cmbxSelections);
            this.Controls.Add(this.lblValueType);
            this.Controls.Add(this.lblName);
            this.Name = "iEnum";
            this.Size = new System.Drawing.Size(401, 28);
            this.superTooltip1.SetSuperTooltip(this, new DevComponents.DotNetBar.SuperTooltipInfo("Enum", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, true, false, new System.Drawing.Size(0, 0)));
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblValueType;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewValueAsToolStripMenuItem;
        private DevComponents.DotNetBar.SuperTooltip superTooltip1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbxSelections;
    }
}

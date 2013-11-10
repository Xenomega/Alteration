namespace Alteration.Halo_3.Meta_Editor.Controls
{
    partial class iIdent
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
            this.lblSeperator = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.cmbxClass = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbxName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewValueAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSeperator
            // 
            this.lblSeperator.AutoSize = true;
            this.lblSeperator.Location = new System.Drawing.Point(217, 6);
            this.lblSeperator.Name = "lblSeperator";
            this.lblSeperator.Size = new System.Drawing.Size(16, 13);
            this.lblSeperator.TabIndex = 7;
            this.lblSeperator.Text = " - ";
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(3, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(133, 23);
            this.superTooltip1.SetSuperTooltip(this.lblName, new DevComponents.DotNetBar.SuperTooltipInfo("Label", "", "The label/name of the value.", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
            this.lblName.TabIndex = 5;
            this.lblName.Text = "label1";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblName.Click += new System.EventHandler(this.lblName_Click);
            // 
            // cmbxClass
            // 
            this.cmbxClass.DisplayMember = "Text";
            this.cmbxClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbxClass.FormattingEnabled = true;
            this.cmbxClass.Location = new System.Drawing.Point(142, 3);
            this.cmbxClass.Name = "cmbxClass";
            this.cmbxClass.Size = new System.Drawing.Size(71, 21);
            this.cmbxClass.TabIndex = 9;
            this.cmbxClass.SelectedIndexChanged += new System.EventHandler(this.cmbxClass_SelectedIndexChanged);
            // 
            // cmbxName
            // 
            this.cmbxName.DisplayMember = "Text";
            this.cmbxName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbxName.FormattingEnabled = true;
            this.cmbxName.Location = new System.Drawing.Point(239, 3);
            this.cmbxName.Name = "cmbxName";
            this.cmbxName.Size = new System.Drawing.Size(274, 21);
            this.cmbxName.TabIndex = 10;
            this.cmbxName.SelectedIndexChanged += new System.EventHandler(this.cmbxName_SelectedIndexChanged);
            this.cmbxName.TextChanged += new System.EventHandler(this.cmbxName_TextChanged);
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
            // iIdent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.cmbxName);
            this.Controls.Add(this.cmbxClass);
            this.Controls.Add(this.lblSeperator);
            this.Controls.Add(this.lblName);
            this.Name = "iIdent";
            this.Size = new System.Drawing.Size(516, 27);
            this.superTooltip1.SetSuperTooltip(this, new DevComponents.DotNetBar.SuperTooltipInfo("Ident", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, true, false, new System.Drawing.Size(0, 0)));
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSeperator;
        private System.Windows.Forms.Label lblName;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbxClass;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbxName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewValueAsToolStripMenuItem;
        private DevComponents.DotNetBar.SuperTooltip superTooltip1;
    }
}

namespace Alteration.Halo_3.Meta_Editor.Controls
{
    partial class iValue
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
            this.lblType = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewValueAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(365, 8);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(35, 13);
            this.lblType.TabIndex = 5;
            this.lblType.Text = "label2";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(209, 5);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(150, 20);
            this.txtValue.TabIndex = 4;
            this.txtValue.TextChanged += new System.EventHandler(this.txtValue_TextChanged);
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(3, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(200, 23);
            this.superTooltip1.SetSuperTooltip(this.lblName, new DevComponents.DotNetBar.SuperTooltipInfo("Label", "", "The label/name of the value.", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
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
            // iValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.lblName);
            this.Name = "iValue";
            this.Size = new System.Drawing.Size(403, 28);
            this.superTooltip1.SetSuperTooltip(this, new DevComponents.DotNetBar.SuperTooltipInfo("Value", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewValueAsToolStripMenuItem;
        private DevComponents.DotNetBar.SuperTooltip superTooltip1;
    }
}

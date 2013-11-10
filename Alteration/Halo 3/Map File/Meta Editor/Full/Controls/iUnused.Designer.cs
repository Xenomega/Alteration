namespace Alteration.Halo_3.Meta_Editor.Controls
{
    partial class iUnused
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lblUnusedDescription = new System.Windows.Forms.Label();
            this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
            this.SuspendLayout();
            // 
            // lblUnusedDescription
            // 
            this.lblUnusedDescription.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblUnusedDescription.Location = new System.Drawing.Point(3, 3);
            this.lblUnusedDescription.Name = "lblUnusedDescription";
            this.lblUnusedDescription.Size = new System.Drawing.Size(321, 25);
            this.lblUnusedDescription.TabIndex = 1;
            this.lblUnusedDescription.Text = "unused data";
            this.lblUnusedDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // iUnused
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblUnusedDescription);
            this.Name = "iUnused";
            this.Size = new System.Drawing.Size(327, 28);
            this.superTooltip1.SetSuperTooltip(this, new DevComponents.DotNetBar.SuperTooltipInfo("Unused", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, true, false, new System.Drawing.Size(0, 0)));
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lblUnusedDescription;
        private DevComponents.DotNetBar.SuperTooltip superTooltip1;
    }
}

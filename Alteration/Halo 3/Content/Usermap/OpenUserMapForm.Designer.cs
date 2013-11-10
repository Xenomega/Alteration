namespace Alteration.Halo_3.Content.Usermap
{
    partial class OpenUserMapForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.lblOpenUsermap = new System.Windows.Forms.Label();
            this.txtOpenUsermap = new System.Windows.Forms.TextBox();
            this.btnOpenUsermap = new DevComponents.DotNetBar.ButtonX();
            this.lblOpenMapFile = new System.Windows.Forms.Label();
            this.txtOpenMapFile = new System.Windows.Forms.TextBox();
            this.btnOpenMapFile = new DevComponents.DotNetBar.ButtonX();
            this.btnDone = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorScheme.ItemDesignTimeBorder = System.Drawing.Color.Black;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx1.Controls.Add(this.btnDone);
            this.panelEx1.Controls.Add(this.btnOpenMapFile);
            this.panelEx1.Controls.Add(this.txtOpenMapFile);
            this.panelEx1.Controls.Add(this.lblOpenMapFile);
            this.panelEx1.Controls.Add(this.btnOpenUsermap);
            this.panelEx1.Controls.Add(this.txtOpenUsermap);
            this.panelEx1.Controls.Add(this.lblOpenUsermap);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(456, 126);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            this.panelEx1.Click += new System.EventHandler(this.panelEx1_Click);
            // 
            // lblOpenUsermap
            // 
            this.lblOpenUsermap.AutoSize = true;
            this.lblOpenUsermap.Location = new System.Drawing.Point(12, 9);
            this.lblOpenUsermap.Name = "lblOpenUsermap";
            this.lblOpenUsermap.Size = new System.Drawing.Size(166, 13);
            this.lblOpenUsermap.TabIndex = 0;
            this.lblOpenUsermap.Text = "Open a usermap or sandbox.map:";
            // 
            // txtOpenUsermap
            // 
            this.txtOpenUsermap.BackColor = System.Drawing.SystemColors.Window;
            this.txtOpenUsermap.Location = new System.Drawing.Point(15, 25);
            this.txtOpenUsermap.Name = "txtOpenUsermap";
            this.txtOpenUsermap.ReadOnly = true;
            this.txtOpenUsermap.Size = new System.Drawing.Size(381, 20);
            this.txtOpenUsermap.TabIndex = 1;
            // 
            // btnOpenUsermap
            // 
            this.btnOpenUsermap.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenUsermap.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOpenUsermap.Location = new System.Drawing.Point(402, 25);
            this.btnOpenUsermap.Name = "btnOpenUsermap";
            this.btnOpenUsermap.Size = new System.Drawing.Size(42, 20);
            this.btnOpenUsermap.TabIndex = 2;
            this.btnOpenUsermap.Text = "...";
            this.btnOpenUsermap.Click += new System.EventHandler(this.btnOpenUsermap_Click);
            // 
            // lblOpenMapFile
            // 
            this.lblOpenMapFile.AutoSize = true;
            this.lblOpenMapFile.Location = new System.Drawing.Point(12, 48);
            this.lblOpenMapFile.Name = "lblOpenMapFile";
            this.lblOpenMapFile.Size = new System.Drawing.Size(166, 13);
            this.lblOpenMapFile.TabIndex = 3;
            this.lblOpenMapFile.Text = "Open the corresponding .map File";
            // 
            // txtOpenMapFile
            // 
            this.txtOpenMapFile.BackColor = System.Drawing.SystemColors.Window;
            this.txtOpenMapFile.Location = new System.Drawing.Point(15, 64);
            this.txtOpenMapFile.Name = "txtOpenMapFile";
            this.txtOpenMapFile.ReadOnly = true;
            this.txtOpenMapFile.Size = new System.Drawing.Size(381, 20);
            this.txtOpenMapFile.TabIndex = 4;
            // 
            // btnOpenMapFile
            // 
            this.btnOpenMapFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenMapFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOpenMapFile.Location = new System.Drawing.Point(402, 64);
            this.btnOpenMapFile.Name = "btnOpenMapFile";
            this.btnOpenMapFile.Size = new System.Drawing.Size(42, 20);
            this.btnOpenMapFile.TabIndex = 5;
            this.btnOpenMapFile.Text = "...";
            this.btnOpenMapFile.Click += new System.EventHandler(this.btnOpenMapFile_Click);
            // 
            // btnDone
            // 
            this.btnDone.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDone.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDone.Location = new System.Drawing.Point(15, 90);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(429, 23);
            this.btnDone.TabIndex = 6;
            this.btnDone.Text = "Done";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // OpenUserMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 126);
            this.Controls.Add(this.panelEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpenUserMapForm";
            this.Text = "Open a Usermap...";
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.Label lblOpenUsermap;
        private DevComponents.DotNetBar.ButtonX btnDone;
        private DevComponents.DotNetBar.ButtonX btnOpenMapFile;
        private System.Windows.Forms.TextBox txtOpenMapFile;
        private System.Windows.Forms.Label lblOpenMapFile;
        private DevComponents.DotNetBar.ButtonX btnOpenUsermap;
        private System.Windows.Forms.TextBox txtOpenUsermap;
    }
}
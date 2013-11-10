namespace Alteration.Halo_3.Plugins
{
    partial class PluginGeneratorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginGeneratorForm));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnGeneratePlugins = new DevComponents.DotNetBar.ButtonX();
            this.btnOutputFolder = new DevComponents.DotNetBar.ButtonX();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pbxLogo = new System.Windows.Forms.PictureBox();
            this.btnOpenMap = new DevComponents.DotNetBar.ButtonX();
            this.txtOpenMap = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxX1 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx1.Controls.Add(this.checkBoxX1);
            this.panelEx1.Controls.Add(this.btnGeneratePlugins);
            this.panelEx1.Controls.Add(this.btnOutputFolder);
            this.panelEx1.Controls.Add(this.txtOutputFolder);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.pbxLogo);
            this.panelEx1.Controls.Add(this.btnOpenMap);
            this.panelEx1.Controls.Add(this.txtOpenMap);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(292, 256);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 2;
            // 
            // btnGeneratePlugins
            // 
            this.btnGeneratePlugins.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnGeneratePlugins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGeneratePlugins.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnGeneratePlugins.Location = new System.Drawing.Point(12, 221);
            this.btnGeneratePlugins.Name = "btnGeneratePlugins";
            this.btnGeneratePlugins.Size = new System.Drawing.Size(274, 24);
            this.btnGeneratePlugins.TabIndex = 15;
            this.btnGeneratePlugins.Text = "Generate Plugins";
            this.btnGeneratePlugins.Click += new System.EventHandler(this.btnGeneratePlugins_Click);
            // 
            // btnOutputFolder
            // 
            this.btnOutputFolder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutputFolder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOutputFolder.Location = new System.Drawing.Point(254, 198);
            this.btnOutputFolder.Name = "btnOutputFolder";
            this.btnOutputFolder.Size = new System.Drawing.Size(32, 20);
            this.btnOutputFolder.TabIndex = 14;
            this.btnOutputFolder.Text = "...";
            this.btnOutputFolder.Click += new System.EventHandler(this.btnOutputFolder_Click);
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputFolder.Location = new System.Drawing.Point(12, 198);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.ReadOnly = true;
            this.txtOutputFolder.Size = new System.Drawing.Size(236, 20);
            this.txtOutputFolder.TabIndex = 13;
            this.txtOutputFolder.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Output Plugins Folder:";
            // 
            // pbxLogo
            // 
            this.pbxLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbxLogo.Image")));
            this.pbxLogo.Location = new System.Drawing.Point(89, 12);
            this.pbxLogo.Name = "pbxLogo";
            this.pbxLogo.Size = new System.Drawing.Size(100, 100);
            this.pbxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxLogo.TabIndex = 11;
            this.pbxLogo.TabStop = false;
            // 
            // btnOpenMap
            // 
            this.btnOpenMap.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenMap.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOpenMap.Location = new System.Drawing.Point(254, 155);
            this.btnOpenMap.Name = "btnOpenMap";
            this.btnOpenMap.Size = new System.Drawing.Size(32, 20);
            this.btnOpenMap.TabIndex = 2;
            this.btnOpenMap.Text = "...";
            this.btnOpenMap.Click += new System.EventHandler(this.btnOpenMap_Click);
            // 
            // txtOpenMap
            // 
            this.txtOpenMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOpenMap.Location = new System.Drawing.Point(12, 155);
            this.txtOpenMap.Name = "txtOpenMap";
            this.txtOpenMap.ReadOnly = true;
            this.txtOpenMap.Size = new System.Drawing.Size(236, 20);
            this.txtOpenMap.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Open a map file:";
            // 
            // checkBoxX1
            // 
            this.checkBoxX1.Location = new System.Drawing.Point(126, 116);
            this.checkBoxX1.Name = "checkBoxX1";
            this.checkBoxX1.Size = new System.Drawing.Size(154, 23);
            this.checkBoxX1.TabIndex = 16;
            this.checkBoxX1.Text = "Use all maps in map folder";
            // 
            // PluginGeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 256);
            this.Controls.Add(this.panelEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PluginGeneratorForm";
            this.Text = "Plugin Generator";
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX btnOpenMap;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbxLogo;
        private DevComponents.DotNetBar.ButtonX btnOutputFolder;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOpenMap;
        private DevComponents.DotNetBar.ButtonX btnGeneratePlugins;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX1;
    }
}
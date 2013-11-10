namespace Alteration.Forms.Dialog
{
    partial class MapProtectorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapProtectorForm));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnStartProtect = new DevComponents.DotNetBar.ButtonX();
            this.btnFindProtected = new DevComponents.DotNetBar.ButtonX();
            this.btnFindUnprotected = new DevComponents.DotNetBar.ButtonX();
            this.txtProtectedLocation = new System.Windows.Forms.TextBox();
            this.lblProtectedMap = new System.Windows.Forms.Label();
            this.txtUnprotectedLocation = new System.Windows.Forms.TextBox();
            this.lblUnprotectedMap = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblWhatIsThis = new System.Windows.Forms.Label();
            this.pbxLogo = new System.Windows.Forms.PictureBox();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx1.Controls.Add(this.btnStartProtect);
            this.panelEx1.Controls.Add(this.btnFindProtected);
            this.panelEx1.Controls.Add(this.btnFindUnprotected);
            this.panelEx1.Controls.Add(this.txtProtectedLocation);
            this.panelEx1.Controls.Add(this.lblProtectedMap);
            this.panelEx1.Controls.Add(this.txtUnprotectedLocation);
            this.panelEx1.Controls.Add(this.lblUnprotectedMap);
            this.panelEx1.Controls.Add(this.lblWelcome);
            this.panelEx1.Controls.Add(this.lblWhatIsThis);
            this.panelEx1.Controls.Add(this.pbxLogo);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(516, 245);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 2;
            // 
            // btnStartProtect
            // 
            this.btnStartProtect.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnStartProtect.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnStartProtect.Location = new System.Drawing.Point(12, 211);
            this.btnStartProtect.Name = "btnStartProtect";
            this.btnStartProtect.Size = new System.Drawing.Size(492, 20);
            this.btnStartProtect.TabIndex = 20;
            this.btnStartProtect.Text = "Protect Map!";
            this.btnStartProtect.Click += new System.EventHandler(this.btnStartProtect_Click);
            // 
            // btnFindProtected
            // 
            this.btnFindProtected.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFindProtected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindProtected.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnFindProtected.Location = new System.Drawing.Point(478, 185);
            this.btnFindProtected.Name = "btnFindProtected";
            this.btnFindProtected.Size = new System.Drawing.Size(26, 20);
            this.btnFindProtected.TabIndex = 19;
            this.btnFindProtected.Text = "...";
            this.btnFindProtected.Click += new System.EventHandler(this.btnFindProtected_Click);
            // 
            // btnFindUnprotected
            // 
            this.btnFindUnprotected.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFindUnprotected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindUnprotected.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnFindUnprotected.Location = new System.Drawing.Point(478, 141);
            this.btnFindUnprotected.Name = "btnFindUnprotected";
            this.btnFindUnprotected.Size = new System.Drawing.Size(26, 20);
            this.btnFindUnprotected.TabIndex = 18;
            this.btnFindUnprotected.Text = "...";
            this.btnFindUnprotected.Click += new System.EventHandler(this.btnFindUnprotected_Click);
            // 
            // txtProtectedLocation
            // 
            this.txtProtectedLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProtectedLocation.Location = new System.Drawing.Point(12, 185);
            this.txtProtectedLocation.Name = "txtProtectedLocation";
            this.txtProtectedLocation.ReadOnly = true;
            this.txtProtectedLocation.Size = new System.Drawing.Size(460, 20);
            this.txtProtectedLocation.TabIndex = 17;
            // 
            // lblProtectedMap
            // 
            this.lblProtectedMap.AutoSize = true;
            this.lblProtectedMap.Location = new System.Drawing.Point(9, 169);
            this.lblProtectedMap.Name = "lblProtectedMap";
            this.lblProtectedMap.Size = new System.Drawing.Size(108, 13);
            this.lblProtectedMap.TabIndex = 16;
            this.lblProtectedMap.Text = "Save Protected Map:";
            // 
            // txtUnprotectedLocation
            // 
            this.txtUnprotectedLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUnprotectedLocation.Location = new System.Drawing.Point(12, 141);
            this.txtUnprotectedLocation.Name = "txtUnprotectedLocation";
            this.txtUnprotectedLocation.ReadOnly = true;
            this.txtUnprotectedLocation.Size = new System.Drawing.Size(460, 20);
            this.txtUnprotectedLocation.TabIndex = 15;
            // 
            // lblUnprotectedMap
            // 
            this.lblUnprotectedMap.AutoSize = true;
            this.lblUnprotectedMap.Location = new System.Drawing.Point(9, 125);
            this.lblUnprotectedMap.Name = "lblUnprotectedMap";
            this.lblUnprotectedMap.Size = new System.Drawing.Size(129, 13);
            this.lblUnprotectedMap.TabIndex = 14;
            this.lblUnprotectedMap.Text = "Locate Unprotected Map:";
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(118, 12);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(242, 13);
            this.lblWelcome.TabIndex = 13;
            this.lblWelcome.Text = "Welcome to the Alteration Map Protector.";
            // 
            // lblWhatIsThis
            // 
            this.lblWhatIsThis.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWhatIsThis.Location = new System.Drawing.Point(118, 29);
            this.lblWhatIsThis.Name = "lblWhatIsThis";
            this.lblWhatIsThis.Size = new System.Drawing.Size(386, 93);
            this.lblWhatIsThis.TabIndex = 12;
            this.lblWhatIsThis.Text = resources.GetString("lblWhatIsThis.Text");
            // 
            // pbxLogo
            // 
            this.pbxLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbxLogo.Image")));
            this.pbxLogo.Location = new System.Drawing.Point(12, 12);
            this.pbxLogo.Name = "pbxLogo";
            this.pbxLogo.Size = new System.Drawing.Size(100, 100);
            this.pbxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxLogo.TabIndex = 11;
            this.pbxLogo.TabStop = false;
            // 
            // MapProtectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 245);
            this.Controls.Add(this.panelEx1);
            this.Name = "MapProtectorForm";
            this.Text = "Alteration Map Protector";
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.Label lblWhatIsThis;
        private System.Windows.Forms.PictureBox pbxLogo;
        private System.Windows.Forms.Label lblUnprotectedMap;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.TextBox txtProtectedLocation;
        private System.Windows.Forms.Label lblProtectedMap;
        private System.Windows.Forms.TextBox txtUnprotectedLocation;
        private DevComponents.DotNetBar.ButtonX btnFindUnprotected;
        private DevComponents.DotNetBar.ButtonX btnStartProtect;
        private DevComponents.DotNetBar.ButtonX btnFindProtected;
    }
}
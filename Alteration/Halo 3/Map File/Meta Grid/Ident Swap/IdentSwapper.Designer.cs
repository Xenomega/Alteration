namespace Alteration.Halo_3.Meta_Grid.Ident_Swap
{
    partial class IdentSwapper
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.cmbxName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbxClass = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.buttonX1);
            this.panel1.Controls.Add(this.cmbxName);
            this.panel1.Controls.Add(this.cmbxClass);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(475, 77);
            this.panel1.TabIndex = 0;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(13, 42);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(457, 23);
            this.buttonX1.TabIndex = 2;
            this.buttonX1.Text = "Swap";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // cmbxName
            // 
            this.cmbxName.DisplayMember = "Text";
            this.cmbxName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbxName.FormattingEnabled = true;
            this.cmbxName.Location = new System.Drawing.Point(116, 15);
            this.cmbxName.Name = "cmbxName";
            this.cmbxName.Size = new System.Drawing.Size(354, 21);
            this.cmbxName.TabIndex = 1;
            this.cmbxName.SelectedIndexChanged += new System.EventHandler(this.cmbxName_SelectedIndexChanged);
            // 
            // cmbxClass
            // 
            this.cmbxClass.DisplayMember = "Text";
            this.cmbxClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbxClass.FormattingEnabled = true;
            this.cmbxClass.Location = new System.Drawing.Point(13, 15);
            this.cmbxClass.Name = "cmbxClass";
            this.cmbxClass.Size = new System.Drawing.Size(97, 21);
            this.cmbxClass.TabIndex = 0;
            this.cmbxClass.SelectedIndexChanged += new System.EventHandler(this.cmbx_SelectedIndexChanged_1);
            // 
            // IdentSwapper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(499, 103);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IdentSwapper";
            this.Text = "Ident Swapper";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbxName;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbxClass;
    }
}
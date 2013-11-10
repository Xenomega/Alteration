namespace Alteration.Halo_3.Meta_Editor
{
    partial class MetaEditorContainer
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
            try
            {
                UnloadMetaEditor();
            }
            catch { }
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
            this.menuBar1 = new TD.SandBar.MenuBar();
            this.menuBarItem1 = new TD.SandBar.MenuBarItem();
            this.menuBarItem2 = new TD.SandBar.MenuBarItem();
            this.menuButtonItem1 = new TD.SandBar.MenuButtonItem();
            this.menuButtonItem2 = new TD.SandBar.MenuButtonItem();
            this.menuBarItem3 = new TD.SandBar.MenuBarItem();
            this.menuButtonItem3 = new TD.SandBar.MenuButtonItem();
            this.menuButtonItem4 = new TD.SandBar.MenuButtonItem();
            this.menuBarItem4 = new TD.SandBar.MenuBarItem();
            this.menuButtonItem5 = new TD.SandBar.MenuButtonItem();
            this.menuButtonItem6 = new TD.SandBar.MenuButtonItem();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.SuspendLayout();
            // 
            // menuBar1
            // 
            this.menuBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuBar1.Guid = new System.Guid("df697c1f-13d5-4c45-a508-b28740a98cb5");
            this.menuBar1.Items.AddRange(new TD.SandBar.ToolbarItemBase[] {
            this.menuBarItem1,
            this.menuBarItem2,
            this.menuBarItem3,
            this.menuBarItem4});
            this.menuBar1.Location = new System.Drawing.Point(0, 339);
            this.menuBar1.Name = "menuBar1";
            this.menuBar1.OwnerForm = null;
            this.menuBar1.Size = new System.Drawing.Size(482, 22);
            this.menuBar1.TabIndex = 0;
            this.menuBar1.Text = "menuBar1";
            // 
            // menuBarItem1
            // 
            this.menuBarItem1.Text = "Save";
            this.menuBarItem1.BeforePopup += new TD.SandBar.MenuItemBase.BeforePopupEventHandler(this.menuBarItem1_BeforePopup);
            // 
            // menuBarItem2
            // 
            this.menuBarItem2.Items.AddRange(new TD.SandBar.ToolbarItemBase[] {
            this.menuButtonItem1,
            this.menuButtonItem2});
            this.menuBarItem2.Text = "Poke Options";
            // 
            // menuButtonItem1
            // 
            this.menuButtonItem1.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.menuButtonItem1.Text = "Poke Changes";
            this.menuButtonItem1.Activate += new System.EventHandler(this.menuButtonItem1_Activate);
            // 
            // menuButtonItem2
            // 
            this.menuButtonItem2.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftP;
            this.menuButtonItem2.Text = "Poke All";
            this.menuButtonItem2.Activate += new System.EventHandler(this.menuButtonItem2_Activate);
            // 
            // menuBarItem3
            // 
            this.menuBarItem3.Items.AddRange(new TD.SandBar.ToolbarItemBase[] {
            this.menuButtonItem3,
            this.menuButtonItem4});
            this.menuBarItem3.Text = "Build RTH Data";
            // 
            // menuButtonItem3
            // 
            this.menuButtonItem3.Text = "Build RTH Data from Changes";
            this.menuButtonItem3.Activate += new System.EventHandler(this.menuButtonItem3_Activate);
            // 
            // menuButtonItem4
            // 
            this.menuButtonItem4.Text = "Build RTH Data from All";
            this.menuButtonItem4.Activate += new System.EventHandler(this.menuButtonItem4_Activate);
            // 
            // menuBarItem4
            // 
            this.menuBarItem4.Items.AddRange(new TD.SandBar.ToolbarItemBase[] {
            this.menuButtonItem5,
            this.menuButtonItem6});
            this.menuBarItem4.Text = "Options";
            // 
            // menuButtonItem5
            // 
            this.menuButtonItem5.Checked = true;
            this.menuButtonItem5.Text = "Show Invisibles";
            this.menuButtonItem5.Activate += new System.EventHandler(this.menuButtonItem5_Activate);
            // 
            // menuButtonItem6
            // 
            this.menuButtonItem6.Text = "Refresh";
            this.menuButtonItem6.Activate += new System.EventHandler(this.menuButtonItem6_Activate);
            // 
            // panelEx1
            // 
            this.panelEx1.AutoScroll = true;
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(482, 339);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // MetaEditorContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.menuBar1);
            this.Name = "MetaEditorContainer";
            this.Size = new System.Drawing.Size(482, 361);
            this.ResumeLayout(false);

        }

        #endregion

        private TD.SandBar.MenuBar menuBar1;
        private TD.SandBar.MenuBarItem menuBarItem1;
        private TD.SandBar.MenuBarItem menuBarItem2;
        private TD.SandBar.MenuButtonItem menuButtonItem1;
        private TD.SandBar.MenuButtonItem menuButtonItem2;
        private TD.SandBar.MenuBarItem menuBarItem3;
        private TD.SandBar.MenuButtonItem menuButtonItem3;
        private TD.SandBar.MenuButtonItem menuButtonItem4;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private TD.SandBar.MenuBarItem menuBarItem4;
        private TD.SandBar.MenuButtonItem menuButtonItem5;
        private TD.SandBar.MenuButtonItem menuButtonItem6;



    }
}

namespace Alteration
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuBar1 = new TD.SandBar.MenuBar();
            this.menuBarItem1 = new TD.SandBar.MenuBarItem();
            this.menuButtonItem1 = new TD.SandBar.MenuButtonItem();
            this.menuBarItem2 = new TD.SandBar.MenuBarItem();
            this.menuCreateMapPackage = new TD.SandBar.MenuButtonItem();
            this.menuScreenshot = new TD.SandBar.MenuButtonItem();
            this.menuBarItem3 = new TD.SandBar.MenuBarItem();
            this.menuBarItem4 = new TD.SandBar.MenuBarItem();
            this.menuBarItem5 = new TD.SandBar.MenuBarItem();
            this.menuButtonItem2 = new TD.SandBar.MenuButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panel2 = new System.Windows.Forms.Panel();
            this.explorerBar1 = new DevComponents.DotNetBar.ExplorerBar();
            this.explorerBarGroupItem1 = new DevComponents.DotNetBar.ExplorerBarGroupItem();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.txtXDKName = new DevComponents.DotNetBar.TextBoxItem();
            this.explorerBarGroupItem2 = new DevComponents.DotNetBar.ExplorerBarGroupItem();
            this.chkShowInvisibles = new DevComponents.DotNetBar.CheckBoxItem();
            this.explorerBarGroupItem3 = new DevComponents.DotNetBar.ExplorerBarGroupItem();
            this.btnLoadSettings = new DevComponents.DotNetBar.ButtonItem();
            this.btnSaveSettings = new DevComponents.DotNetBar.ButtonItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCollapse = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.explorerBar1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuBar1
            // 
            this.menuBar1.Guid = new System.Guid("224373b3-fa79-42d5-b7f1-fe9fd2cd9a49");
            this.menuBar1.Items.AddRange(new TD.SandBar.ToolbarItemBase[] {
            this.menuBarItem1,
            this.menuBarItem2,
            this.menuBarItem3,
            this.menuBarItem4,
            this.menuBarItem5});
            this.menuBar1.Location = new System.Drawing.Point(0, 0);
            this.menuBar1.Name = "menuBar1";
            this.menuBar1.OwnerForm = this;
            this.menuBar1.Size = new System.Drawing.Size(820, 22);
            this.menuBar1.TabIndex = 0;
            this.menuBar1.Text = "menuBar1";
            this.menuBar1.ButtonClick += new TD.SandBar.ToolBar.ButtonClickEventHandler(this.menuBar1_ButtonClick);
            // 
            // menuBarItem1
            // 
            this.menuBarItem1.Items.AddRange(new TD.SandBar.ToolbarItemBase[] {
            this.menuButtonItem1});
            this.menuBarItem1.Text = "&File";
            // 
            // menuButtonItem1
            // 
            this.menuButtonItem1.Text = "Open Map";
            this.menuButtonItem1.Activate += new System.EventHandler(this.menuButtonItem1_Activate);
            // 
            // menuBarItem2
            // 
            this.menuBarItem2.Items.AddRange(new TD.SandBar.ToolbarItemBase[] {
            this.menuCreateMapPackage,
            this.menuScreenshot});
            this.menuBarItem2.Text = "&Edit";
            // 
            // menuCreateMapPackage
            // 
            this.menuCreateMapPackage.Text = "Create Map Package";
            this.menuCreateMapPackage.Activate += new System.EventHandler(this.menuButtonItem3_Activate);
            // 
            // menuScreenshot
            // 
            this.menuScreenshot.BeginGroup = true;
            this.menuScreenshot.Text = "Screenshot";
            this.menuScreenshot.Activate += new System.EventHandler(this.menuScreenshot_Activate);
            // 
            // menuBarItem3
            // 
            this.menuBarItem3.Text = "&View";
            // 
            // menuBarItem4
            // 
            this.menuBarItem4.MdiWindowList = true;
            this.menuBarItem4.Text = "&Window";
            // 
            // menuBarItem5
            // 
            this.menuBarItem5.Items.AddRange(new TD.SandBar.ToolbarItemBase[] {
            this.menuButtonItem2});
            this.menuBarItem5.Text = "&Help";
            // 
            // menuButtonItem2
            // 
            this.menuButtonItem2.Text = "About";
            this.menuButtonItem2.Activate += new System.EventHandler(this.menuButtonItem2_Activate);
            // 
            // buttonItem1
            // 
            this.buttonItem1.Checked = true;
            this.buttonItem1.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem1.Image")));
            this.buttonItem1.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.OptionGroup = "navBar";
            this.buttonItem1.Text = "buttonItem1";
            // 
            // buttonItem2
            // 
            this.buttonItem2.Checked = true;
            this.buttonItem2.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem2.Image")));
            this.buttonItem2.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.OptionGroup = "navBar";
            this.buttonItem2.Text = "buttonItem2";
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorScheme.ItemDesignTimeBorder = System.Drawing.Color.Black;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx1.Controls.Add(this.panel2);
            this.panelEx1.Controls.Add(this.panel1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelEx1.Location = new System.Drawing.Point(599, 22);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(221, 520);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.explorerBar1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(12, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(209, 520);
            this.panel2.TabIndex = 3;
            // 
            // explorerBar1
            // 
            this.explorerBar1.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.explorerBar1.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.explorerBar1.BackStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(149)))), ((int)(((byte)(237)))));
            this.explorerBar1.BackStyle.BackColorGradientAngle = 90;
            this.explorerBar1.BackStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ExplorerBarBackground;
            this.explorerBar1.Cursor = System.Windows.Forms.Cursors.Default;
            this.explorerBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.explorerBar1.GroupImages = null;
            this.explorerBar1.Groups.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.explorerBarGroupItem1,
            this.explorerBarGroupItem2,
            this.explorerBarGroupItem3});
            this.explorerBar1.Images = null;
            this.explorerBar1.Location = new System.Drawing.Point(0, 0);
            this.explorerBar1.Name = "explorerBar1";
            this.explorerBar1.Size = new System.Drawing.Size(209, 520);
            this.explorerBar1.TabIndex = 0;
            this.explorerBar1.Text = "explorerBar1";
            this.explorerBar1.ThemeAware = true;
            // 
            // explorerBarGroupItem1
            // 
            // 
            // 
            // 
            this.explorerBarGroupItem1.BackStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(223)))), ((int)(((byte)(247)))));
            this.explorerBarGroupItem1.BackStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem1.BackStyle.BorderBottomWidth = 1;
            this.explorerBarGroupItem1.BackStyle.BorderColor = System.Drawing.Color.White;
            this.explorerBarGroupItem1.BackStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem1.BackStyle.BorderLeftWidth = 1;
            this.explorerBarGroupItem1.BackStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem1.BackStyle.BorderRightWidth = 1;
            this.explorerBarGroupItem1.Cursor = System.Windows.Forms.Cursors.Default;
            this.explorerBarGroupItem1.Expanded = true;
            this.explorerBarGroupItem1.Name = "explorerBarGroupItem1";
            this.explorerBarGroupItem1.StockStyle = DevComponents.DotNetBar.eExplorerBarStockStyle.SystemColors;
            this.explorerBarGroupItem1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem1,
            this.txtXDKName});
            this.explorerBarGroupItem1.Text = "Xbox Development Kit Information";
            // 
            // 
            // 
            this.explorerBarGroupItem1.TitleHotStyle.BackColor = System.Drawing.Color.White;
            this.explorerBarGroupItem1.TitleHotStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(211)))), ((int)(((byte)(247)))));
            this.explorerBarGroupItem1.TitleHotStyle.CornerDiameter = 3;
            this.explorerBarGroupItem1.TitleHotStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem1.TitleHotStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem1.TitleHotStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.explorerBarGroupItem1.TitleStyle.BackColor = System.Drawing.Color.White;
            this.explorerBarGroupItem1.TitleStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(211)))), ((int)(((byte)(247)))));
            this.explorerBarGroupItem1.TitleStyle.CornerDiameter = 3;
            this.explorerBarGroupItem1.TitleStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem1.TitleStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem1.TitleStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            // 
            // labelItem1
            // 
            this.labelItem1.BorderType = DevComponents.DotNetBar.eBorderType.None;
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "XDK Name:";
            this.labelItem1.ThemeAware = true;
            // 
            // txtXDKName
            // 
            this.txtXDKName.AlwaysShowCaption = false;
            this.txtXDKName.ControlText = "";
            this.txtXDKName.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.txtXDKName.Name = "txtXDKName";
            this.txtXDKName.RecentlyUsed = false;
            this.txtXDKName.SelectedText = "";
            this.txtXDKName.SelectionLength = 0;
            this.txtXDKName.SelectionStart = 0;
            this.txtXDKName.Text = "XDK360";
            this.txtXDKName.TextBoxWidth = 64;
            this.txtXDKName.ThemeAware = true;
            // 
            // explorerBarGroupItem2
            // 
            // 
            // 
            // 
            this.explorerBarGroupItem2.BackStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(223)))), ((int)(((byte)(247)))));
            this.explorerBarGroupItem2.BackStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem2.BackStyle.BorderBottomWidth = 1;
            this.explorerBarGroupItem2.BackStyle.BorderColor = System.Drawing.Color.White;
            this.explorerBarGroupItem2.BackStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem2.BackStyle.BorderLeftWidth = 1;
            this.explorerBarGroupItem2.BackStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem2.BackStyle.BorderRightWidth = 1;
            this.explorerBarGroupItem2.Cursor = System.Windows.Forms.Cursors.Default;
            this.explorerBarGroupItem2.Expanded = true;
            this.explorerBarGroupItem2.Name = "explorerBarGroupItem2";
            this.explorerBarGroupItem2.StockStyle = DevComponents.DotNetBar.eExplorerBarStockStyle.SystemColors;
            this.explorerBarGroupItem2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.chkShowInvisibles});
            this.explorerBarGroupItem2.Text = "Meta Editor Settings";
            // 
            // 
            // 
            this.explorerBarGroupItem2.TitleHotStyle.BackColor = System.Drawing.Color.White;
            this.explorerBarGroupItem2.TitleHotStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(211)))), ((int)(((byte)(247)))));
            this.explorerBarGroupItem2.TitleHotStyle.CornerDiameter = 3;
            this.explorerBarGroupItem2.TitleHotStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem2.TitleHotStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem2.TitleHotStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.explorerBarGroupItem2.TitleStyle.BackColor = System.Drawing.Color.White;
            this.explorerBarGroupItem2.TitleStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(211)))), ((int)(((byte)(247)))));
            this.explorerBarGroupItem2.TitleStyle.CornerDiameter = 3;
            this.explorerBarGroupItem2.TitleStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem2.TitleStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem2.TitleStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            // 
            // chkShowInvisibles
            // 
            this.chkShowInvisibles.Name = "chkShowInvisibles";
            this.chkShowInvisibles.Text = "Show Invisibles by Default";
            this.chkShowInvisibles.ThemeAware = true;
            // 
            // explorerBarGroupItem3
            // 
            // 
            // 
            // 
            this.explorerBarGroupItem3.BackStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(223)))), ((int)(((byte)(247)))));
            this.explorerBarGroupItem3.BackStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem3.BackStyle.BorderBottomWidth = 1;
            this.explorerBarGroupItem3.BackStyle.BorderColor = System.Drawing.Color.White;
            this.explorerBarGroupItem3.BackStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem3.BackStyle.BorderLeftWidth = 1;
            this.explorerBarGroupItem3.BackStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem3.BackStyle.BorderRightWidth = 1;
            this.explorerBarGroupItem3.Cursor = System.Windows.Forms.Cursors.Default;
            this.explorerBarGroupItem3.Expanded = true;
            this.explorerBarGroupItem3.Name = "explorerBarGroupItem3";
            this.explorerBarGroupItem3.StockStyle = DevComponents.DotNetBar.eExplorerBarStockStyle.SystemColors;
            this.explorerBarGroupItem3.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnLoadSettings,
            this.btnSaveSettings});
            this.explorerBarGroupItem3.Text = "Settings";
            // 
            // 
            // 
            this.explorerBarGroupItem3.TitleHotStyle.BackColor = System.Drawing.Color.White;
            this.explorerBarGroupItem3.TitleHotStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(211)))), ((int)(((byte)(247)))));
            this.explorerBarGroupItem3.TitleHotStyle.CornerDiameter = 3;
            this.explorerBarGroupItem3.TitleHotStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem3.TitleHotStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem3.TitleHotStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.explorerBarGroupItem3.TitleStyle.BackColor = System.Drawing.Color.White;
            this.explorerBarGroupItem3.TitleStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(211)))), ((int)(((byte)(247)))));
            this.explorerBarGroupItem3.TitleStyle.CornerDiameter = 3;
            this.explorerBarGroupItem3.TitleStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem3.TitleStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem3.TitleStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            // 
            // btnLoadSettings
            // 
            this.btnLoadSettings.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnLoadSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.btnLoadSettings.HotFontUnderline = true;
            this.btnLoadSettings.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.btnLoadSettings.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.btnLoadSettings.Name = "btnLoadSettings";
            this.btnLoadSettings.Text = "Load Settings";
            this.btnLoadSettings.Click += new System.EventHandler(this.btnLoadSettings_Click);
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnSaveSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.btnSaveSettings.HotFontUnderline = true;
            this.btnSaveSettings.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.btnSaveSettings.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Text = "Save Settings";
            this.btnSaveSettings.Click += new System.EventHandler(this.buttonItem4_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCollapse);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(12, 520);
            this.panel1.TabIndex = 2;
            // 
            // btnCollapse
            // 
            this.btnCollapse.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCollapse.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCollapse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCollapse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCollapse.Location = new System.Drawing.Point(0, 0);
            this.btnCollapse.Name = "btnCollapse";
            this.btnCollapse.Size = new System.Drawing.Size(12, 520);
            this.btnCollapse.TabIndex = 1;
            this.btnCollapse.Text = "+";
            this.btnCollapse.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 542);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.menuBar1);
            this.IsMdiContainer = true;
            this.Name = "Form1";
            this.Text = "Alteration";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panelEx1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.explorerBar1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TD.SandBar.MenuBar menuBar1;
        private TD.SandBar.MenuBarItem menuBarItem1;
        private TD.SandBar.MenuButtonItem menuButtonItem1;
        private TD.SandBar.MenuBarItem menuBarItem2;
        private TD.SandBar.MenuBarItem menuBarItem3;
        private TD.SandBar.MenuBarItem menuBarItem4;
        private TD.SandBar.MenuBarItem menuBarItem5;
        private TD.SandBar.MenuButtonItem menuButtonItem2;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.ButtonX btnCollapse;
        private DevComponents.DotNetBar.ExplorerBar explorerBar1;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.ExplorerBarGroupItem explorerBarGroupItem1;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.TextBoxItem txtXDKName;
        private DevComponents.DotNetBar.ExplorerBarGroupItem explorerBarGroupItem2;
        private DevComponents.DotNetBar.CheckBoxItem chkShowInvisibles;
        private DevComponents.DotNetBar.ExplorerBarGroupItem explorerBarGroupItem3;
        private DevComponents.DotNetBar.ButtonItem btnLoadSettings;
        private DevComponents.DotNetBar.ButtonItem btnSaveSettings;
        private TD.SandBar.MenuButtonItem menuCreateMapPackage;
        private TD.SandBar.MenuButtonItem menuScreenshot;


    }
}


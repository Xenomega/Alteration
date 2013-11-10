namespace Alteration.Halo_3.Raw
{
    partial class RawViewerControl
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
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.rawGrid = new System.Windows.Forms.ListView();
            this.colIndex = new System.Windows.Forms.ColumnHeader();
            this.colRawIdentifier = new System.Windows.Forms.ColumnHeader();
            this.colClass = new System.Windows.Forms.ColumnHeader();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colRawLocationsIndex = new System.Windows.Forms.ColumnHeader();
            this.colOffset = new System.Windows.Forms.ColumnHeader();
            this.colSize = new System.Windows.Forms.ColumnHeader();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.lblValuesInHex = new System.Windows.Forms.Label();
            this.cbxShowNull = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnSaveChanges = new DevComponents.DotNetBar.ButtonX();
            this.txtSize = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblSize = new System.Windows.Forms.Label();
            this.txtOffset = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblOffset = new System.Windows.Forms.Label();
            this.txtRawPoolIndex = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblRawLocationsIndex = new System.Windows.Forms.Label();
            this.cmbxTagName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblSeperator = new System.Windows.Forms.Label();
            this.cmbxTagClass = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblTagIdent = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblFilter = new System.Windows.Forms.Label();
            this.txtFilter = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnRefreshFilter = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1.SuspendLayout();
            this.pnlDetails.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx1.Controls.Add(this.panel1);
            this.panelEx1.Controls.Add(this.rawGrid);
            this.panelEx1.Controls.Add(this.pnlDetails);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(594, 506);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // rawGrid
            // 
            this.rawGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rawGrid.BackColor = System.Drawing.Color.White;
            this.rawGrid.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIndex,
            this.colRawIdentifier,
            this.colClass,
            this.colName,
            this.colRawLocationsIndex,
            this.colOffset,
            this.colSize});
            this.rawGrid.ForeColor = System.Drawing.Color.Black;
            this.rawGrid.FullRowSelect = true;
            this.rawGrid.GridLines = true;
            this.rawGrid.Location = new System.Drawing.Point(0, 47);
            this.rawGrid.MultiSelect = false;
            this.rawGrid.Name = "rawGrid";
            this.rawGrid.Size = new System.Drawing.Size(594, 348);
            this.rawGrid.TabIndex = 10;
            this.rawGrid.UseCompatibleStateImageBehavior = false;
            this.rawGrid.View = System.Windows.Forms.View.Details;
            this.rawGrid.SelectedIndexChanged += new System.EventHandler(this.rawGrid_SelectedIndexChanged);
            // 
            // colIndex
            // 
            this.colIndex.Text = "Index";
            this.colIndex.Width = 59;
            // 
            // colRawIdentifier
            // 
            this.colRawIdentifier.Text = "Raw Identifer";
            this.colRawIdentifier.Width = 69;
            // 
            // colClass
            // 
            this.colClass.Text = "Class";
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 223;
            // 
            // colRawLocationsIndex
            // 
            this.colRawLocationsIndex.Text = "Raw Locations Index";
            this.colRawLocationsIndex.Width = 59;
            // 
            // colOffset
            // 
            this.colOffset.Text = "Offset";
            // 
            // colSize
            // 
            this.colSize.Text = "Size";
            // 
            // pnlDetails
            // 
            this.pnlDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlDetails.Controls.Add(this.btnRefreshFilter);
            this.pnlDetails.Controls.Add(this.lblValuesInHex);
            this.pnlDetails.Controls.Add(this.cbxShowNull);
            this.pnlDetails.Controls.Add(this.btnSaveChanges);
            this.pnlDetails.Controls.Add(this.txtSize);
            this.pnlDetails.Controls.Add(this.lblSize);
            this.pnlDetails.Controls.Add(this.txtOffset);
            this.pnlDetails.Controls.Add(this.lblOffset);
            this.pnlDetails.Controls.Add(this.txtRawPoolIndex);
            this.pnlDetails.Controls.Add(this.lblRawLocationsIndex);
            this.pnlDetails.Controls.Add(this.cmbxTagName);
            this.pnlDetails.Controls.Add(this.lblSeperator);
            this.pnlDetails.Controls.Add(this.cmbxTagClass);
            this.pnlDetails.Controls.Add(this.lblTagIdent);
            this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDetails.Location = new System.Drawing.Point(0, 395);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(594, 111);
            this.pnlDetails.TabIndex = 0;
            this.pnlDetails.Paint += new System.Windows.Forms.PaintEventHandler(this.lblCount0_Paint);
            // 
            // lblValuesInHex
            // 
            this.lblValuesInHex.ForeColor = System.Drawing.Color.Silver;
            this.lblValuesInHex.Location = new System.Drawing.Point(421, 1);
            this.lblValuesInHex.Name = "lblValuesInHex";
            this.lblValuesInHex.Size = new System.Drawing.Size(122, 28);
            this.lblValuesInHex.TabIndex = 15;
            this.lblValuesInHex.Text = "(Notice these values are in hexadecimal)";
            // 
            // cbxShowNull
            // 
            this.cbxShowNull.Location = new System.Drawing.Point(424, 30);
            this.cbxShowNull.Name = "cbxShowNull";
            this.cbxShowNull.Size = new System.Drawing.Size(79, 23);
            this.cbxShowNull.TabIndex = 14;
            this.cbxShowNull.Text = "Show Null";
            this.cbxShowNull.CheckedChanged += new System.EventHandler(this.checkBoxX1_CheckedChanged);
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSaveChanges.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSaveChanges.Location = new System.Drawing.Point(0, 82);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(590, 25);
            this.btnSaveChanges.TabIndex = 13;
            this.btnSaveChanges.Text = "Save Changes";
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // txtSize
            // 
            // 
            // 
            // 
            this.txtSize.Border.Class = "TextBoxBorder";
            this.txtSize.Location = new System.Drawing.Point(299, 54);
            this.txtSize.Name = "txtSize";
            this.txtSize.ReadOnly = true;
            this.txtSize.Size = new System.Drawing.Size(119, 20);
            this.txtSize.TabIndex = 12;
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(227, 56);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(30, 13);
            this.lblSize.TabIndex = 11;
            this.lblSize.Text = "Size:";
            // 
            // txtOffset
            // 
            // 
            // 
            // 
            this.txtOffset.Border.Class = "TextBoxBorder";
            this.txtOffset.Location = new System.Drawing.Point(299, 33);
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.ReadOnly = true;
            this.txtOffset.Size = new System.Drawing.Size(119, 20);
            this.txtOffset.TabIndex = 10;
            // 
            // lblOffset
            // 
            this.lblOffset.AutoSize = true;
            this.lblOffset.Location = new System.Drawing.Point(227, 35);
            this.lblOffset.Name = "lblOffset";
            this.lblOffset.Size = new System.Drawing.Size(38, 13);
            this.lblOffset.TabIndex = 9;
            this.lblOffset.Text = "Offset:";
            // 
            // txtRawPoolIndex
            // 
            // 
            // 
            // 
            this.txtRawPoolIndex.Border.Class = "TextBoxBorder";
            this.txtRawPoolIndex.Location = new System.Drawing.Point(6, 54);
            this.txtRawPoolIndex.Name = "txtRawPoolIndex";
            this.txtRawPoolIndex.ReadOnly = true;
            this.txtRawPoolIndex.Size = new System.Drawing.Size(215, 20);
            this.txtRawPoolIndex.TabIndex = 6;
            // 
            // lblRawLocationsIndex
            // 
            this.lblRawLocationsIndex.AutoSize = true;
            this.lblRawLocationsIndex.Location = new System.Drawing.Point(3, 40);
            this.lblRawLocationsIndex.Name = "lblRawLocationsIndex";
            this.lblRawLocationsIndex.Size = new System.Drawing.Size(110, 13);
            this.lblRawLocationsIndex.TabIndex = 5;
            this.lblRawLocationsIndex.Text = "Raw Locations Index:";
            // 
            // cmbxTagName
            // 
            this.cmbxTagName.DisplayMember = "Text";
            this.cmbxTagName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbxTagName.FormattingEnabled = true;
            this.cmbxTagName.Location = new System.Drawing.Point(150, 3);
            this.cmbxTagName.Name = "cmbxTagName";
            this.cmbxTagName.Size = new System.Drawing.Size(258, 21);
            this.cmbxTagName.TabIndex = 4;
            // 
            // lblSeperator
            // 
            this.lblSeperator.AutoSize = true;
            this.lblSeperator.Location = new System.Drawing.Point(134, 5);
            this.lblSeperator.Name = "lblSeperator";
            this.lblSeperator.Size = new System.Drawing.Size(10, 13);
            this.lblSeperator.TabIndex = 3;
            this.lblSeperator.Text = "-";
            // 
            // cmbxTagClass
            // 
            this.cmbxTagClass.DisplayMember = "Text";
            this.cmbxTagClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbxTagClass.FormattingEnabled = true;
            this.cmbxTagClass.Location = new System.Drawing.Point(65, 3);
            this.cmbxTagClass.Name = "cmbxTagClass";
            this.cmbxTagClass.Size = new System.Drawing.Size(63, 21);
            this.cmbxTagClass.TabIndex = 2;
            this.cmbxTagClass.SelectedIndexChanged += new System.EventHandler(this.cmbxTagClass_SelectedIndexChanged);
            // 
            // lblTagIdent
            // 
            this.lblTagIdent.AutoSize = true;
            this.lblTagIdent.Location = new System.Drawing.Point(3, 5);
            this.lblTagIdent.Name = "lblTagIdent";
            this.lblTagIdent.Size = new System.Drawing.Size(56, 13);
            this.lblTagIdent.TabIndex = 1;
            this.lblTagIdent.Text = "Tag Ident:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.txtFilter);
            this.panel1.Controls.Add(this.lblFilter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(594, 47);
            this.panel1.TabIndex = 11;
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(3, 3);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(77, 13);
            this.lblFilter.TabIndex = 0;
            this.lblFilter.Text = "Filter by Name:";
            // 
            // txtFilter
            // 
            // 
            // 
            // 
            this.txtFilter.Border.Class = "TextBoxBorder";
            this.txtFilter.Location = new System.Drawing.Point(6, 19);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(571, 20);
            this.txtFilter.TabIndex = 7;
            // 
            // btnRefreshFilter
            // 
            this.btnRefreshFilter.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRefreshFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshFilter.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRefreshFilter.Location = new System.Drawing.Point(428, 54);
            this.btnRefreshFilter.Name = "btnRefreshFilter";
            this.btnRefreshFilter.Size = new System.Drawing.Size(149, 23);
            this.btnRefreshFilter.TabIndex = 12;
            this.btnRefreshFilter.Text = "Refresh";
            this.btnRefreshFilter.Click += new System.EventHandler(this.btnRefreshFilter_Click);
            // 
            // RawViewerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx1);
            this.Name = "RawViewerControl";
            this.Size = new System.Drawing.Size(594, 506);
            this.panelEx1.ResumeLayout(false);
            this.pnlDetails.ResumeLayout(false);
            this.pnlDetails.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.Label lblRawLocationsIndex;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbxTagName;
        private System.Windows.Forms.Label lblSeperator;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbxTagClass;
        private System.Windows.Forms.Label lblTagIdent;
        private DevComponents.DotNetBar.Controls.TextBoxX txtRawPoolIndex;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbxShowNull;
        private DevComponents.DotNetBar.ButtonX btnSaveChanges;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSize;
        private System.Windows.Forms.Label lblSize;
        private DevComponents.DotNetBar.Controls.TextBoxX txtOffset;
        private System.Windows.Forms.Label lblOffset;
        internal System.Windows.Forms.ListView rawGrid;
        internal System.Windows.Forms.ColumnHeader colIndex;
        internal System.Windows.Forms.ColumnHeader colRawIdentifier;
        internal System.Windows.Forms.ColumnHeader colClass;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colRawLocationsIndex;
        private System.Windows.Forms.Label lblValuesInHex;
        private System.Windows.Forms.ColumnHeader colOffset;
        private System.Windows.Forms.ColumnHeader colSize;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtFilter;
        private System.Windows.Forms.Label lblFilter;
        private DevComponents.DotNetBar.ButtonX btnRefreshFilter;
    }
}

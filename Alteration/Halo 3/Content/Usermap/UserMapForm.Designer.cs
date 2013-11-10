namespace Alteration.Halo_3.Content.Usermap
{
    partial class UserMapForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvTagReferences = new System.Windows.Forms.TreeView();
            this.pnlBackGroundEditor = new DevComponents.DotNetBar.PanelEx();
            this.pnlVariantInfoContainer = new System.Windows.Forms.Panel();
            this.gpnlVariantInformation = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtVariantDescription = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblVariantDescription = new System.Windows.Forms.Label();
            this.txtVariantAuthor = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblVariantAuthor = new System.Windows.Forms.Label();
            this.txtVariantName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblVariantName = new System.Windows.Forms.Label();
            this.pnlSelectedPalleteContainer = new System.Windows.Forms.Panel();
            this.gpnlPallete = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.gpnlPlacements = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtCordZ = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtCordY = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtCordX = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.gpnlFlags = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.chkSymmetrical = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkAsymmetrical = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkPlaceAtStart = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cmbxChunkType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtCordRoll = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblCordRoll = new System.Windows.Forms.Label();
            this.lblChunkType = new System.Windows.Forms.Label();
            this.txtCordPitch = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblCordPitch = new System.Windows.Forms.Label();
            this.txtCordYaw = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblCordYaw = new System.Windows.Forms.Label();
            this.lblCordZ = new System.Windows.Forms.Label();
            this.lblCordY = new System.Windows.Forms.Label();
            this.lblCordX = new System.Windows.Forms.Label();
            this.btnPastePlacement = new DevComponents.DotNetBar.ButtonX();
            this.btnCopyPlacement = new DevComponents.DotNetBar.ButtonX();
            this.btnDeletePlacement = new DevComponents.DotNetBar.ButtonX();
            this.btnAddPlacement = new DevComponents.DotNetBar.ButtonX();
            this.cmbxPlacementChunk = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtTotalCost = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblTotalCost = new System.Windows.Forms.Label();
            this.txtRuntimeMaximum = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblRuntimeMaximum = new System.Windows.Forms.Label();
            this.txtRuntimeMinimum = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblRuntimeMinimum = new System.Windows.Forms.Label();
            this.cmbxTagName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblSeperator = new System.Windows.Forms.Label();
            this.cmbxTagClass = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblTagIdent = new System.Windows.Forms.Label();
            this.pnlButtonContainer = new System.Windows.Forms.Panel();
            this.btnSaveChanges = new DevComponents.DotNetBar.ButtonX();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.comboItem6 = new DevComponents.Editors.ComboItem();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlBackGroundEditor.SuspendLayout();
            this.pnlVariantInfoContainer.SuspendLayout();
            this.gpnlVariantInformation.SuspendLayout();
            this.pnlSelectedPalleteContainer.SuspendLayout();
            this.gpnlPallete.SuspendLayout();
            this.gpnlPlacements.SuspendLayout();
            this.gpnlFlags.SuspendLayout();
            this.pnlButtonContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvTagReferences);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlBackGroundEditor);
            this.splitContainer1.Size = new System.Drawing.Size(844, 521);
            this.splitContainer1.SplitterDistance = 297;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvTagReferences
            // 
            this.tvTagReferences.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTagReferences.Location = new System.Drawing.Point(0, 0);
            this.tvTagReferences.Name = "tvTagReferences";
            this.tvTagReferences.Size = new System.Drawing.Size(297, 521);
            this.tvTagReferences.TabIndex = 0;
            this.tvTagReferences.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvTagReferences_AfterSelect);
            // 
            // pnlBackGroundEditor
            // 
            this.pnlBackGroundEditor.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlBackGroundEditor.ColorScheme.ItemDesignTimeBorder = System.Drawing.Color.Black;
            this.pnlBackGroundEditor.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.pnlBackGroundEditor.Controls.Add(this.pnlVariantInfoContainer);
            this.pnlBackGroundEditor.Controls.Add(this.pnlSelectedPalleteContainer);
            this.pnlBackGroundEditor.Controls.Add(this.pnlButtonContainer);
            this.pnlBackGroundEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackGroundEditor.Location = new System.Drawing.Point(0, 0);
            this.pnlBackGroundEditor.Name = "pnlBackGroundEditor";
            this.pnlBackGroundEditor.Size = new System.Drawing.Size(543, 521);
            this.pnlBackGroundEditor.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlBackGroundEditor.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlBackGroundEditor.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pnlBackGroundEditor.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlBackGroundEditor.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlBackGroundEditor.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlBackGroundEditor.Style.GradientAngle = 90;
            this.pnlBackGroundEditor.TabIndex = 0;
            // 
            // pnlVariantInfoContainer
            // 
            this.pnlVariantInfoContainer.Controls.Add(this.gpnlVariantInformation);
            this.pnlVariantInfoContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVariantInfoContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlVariantInfoContainer.Name = "pnlVariantInfoContainer";
            this.pnlVariantInfoContainer.Size = new System.Drawing.Size(543, 194);
            this.pnlVariantInfoContainer.TabIndex = 14;
            // 
            // gpnlVariantInformation
            // 
            this.gpnlVariantInformation.CanvasColor = System.Drawing.SystemColors.Control;
            this.gpnlVariantInformation.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpnlVariantInformation.Controls.Add(this.txtVariantDescription);
            this.gpnlVariantInformation.Controls.Add(this.lblVariantDescription);
            this.gpnlVariantInformation.Controls.Add(this.txtVariantAuthor);
            this.gpnlVariantInformation.Controls.Add(this.lblVariantAuthor);
            this.gpnlVariantInformation.Controls.Add(this.txtVariantName);
            this.gpnlVariantInformation.Controls.Add(this.lblVariantName);
            this.gpnlVariantInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpnlVariantInformation.Location = new System.Drawing.Point(0, 0);
            this.gpnlVariantInformation.Name = "gpnlVariantInformation";
            this.gpnlVariantInformation.Size = new System.Drawing.Size(543, 194);
            // 
            // 
            // 
            this.gpnlVariantInformation.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpnlVariantInformation.Style.BackColorGradientAngle = 90;
            this.gpnlVariantInformation.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpnlVariantInformation.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpnlVariantInformation.Style.BorderBottomWidth = 1;
            this.gpnlVariantInformation.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpnlVariantInformation.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpnlVariantInformation.Style.BorderLeftWidth = 1;
            this.gpnlVariantInformation.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpnlVariantInformation.Style.BorderRightWidth = 1;
            this.gpnlVariantInformation.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpnlVariantInformation.Style.BorderTopWidth = 1;
            this.gpnlVariantInformation.Style.CornerDiameter = 4;
            this.gpnlVariantInformation.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gpnlVariantInformation.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gpnlVariantInformation.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpnlVariantInformation.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.gpnlVariantInformation.TabIndex = 0;
            this.gpnlVariantInformation.Text = "Variant Information";
            // 
            // txtVariantDescription
            // 
            this.txtVariantDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtVariantDescription.Border.Class = "TextBoxBorder";
            this.txtVariantDescription.Location = new System.Drawing.Point(72, 55);
            this.txtVariantDescription.MaxLength = 128;
            this.txtVariantDescription.Multiline = true;
            this.txtVariantDescription.Name = "txtVariantDescription";
            this.txtVariantDescription.Size = new System.Drawing.Size(456, 115);
            this.txtVariantDescription.TabIndex = 5;
            // 
            // lblVariantDescription
            // 
            this.lblVariantDescription.AutoSize = true;
            this.lblVariantDescription.Location = new System.Drawing.Point(3, 57);
            this.lblVariantDescription.Name = "lblVariantDescription";
            this.lblVariantDescription.Size = new System.Drawing.Size(63, 13);
            this.lblVariantDescription.TabIndex = 4;
            this.lblVariantDescription.Text = "Description:";
            // 
            // txtVariantAuthor
            // 
            this.txtVariantAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtVariantAuthor.Border.Class = "TextBoxBorder";
            this.txtVariantAuthor.Location = new System.Drawing.Point(72, 29);
            this.txtVariantAuthor.MaxLength = 16;
            this.txtVariantAuthor.Name = "txtVariantAuthor";
            this.txtVariantAuthor.Size = new System.Drawing.Size(456, 20);
            this.txtVariantAuthor.TabIndex = 3;
            // 
            // lblVariantAuthor
            // 
            this.lblVariantAuthor.AutoSize = true;
            this.lblVariantAuthor.Location = new System.Drawing.Point(3, 31);
            this.lblVariantAuthor.Name = "lblVariantAuthor";
            this.lblVariantAuthor.Size = new System.Drawing.Size(41, 13);
            this.lblVariantAuthor.TabIndex = 2;
            this.lblVariantAuthor.Text = "Author:";
            // 
            // txtVariantName
            // 
            this.txtVariantName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtVariantName.Border.Class = "TextBoxBorder";
            this.txtVariantName.Location = new System.Drawing.Point(72, 3);
            this.txtVariantName.MaxLength = 16;
            this.txtVariantName.Name = "txtVariantName";
            this.txtVariantName.Size = new System.Drawing.Size(456, 20);
            this.txtVariantName.TabIndex = 1;
            // 
            // lblVariantName
            // 
            this.lblVariantName.AutoSize = true;
            this.lblVariantName.Location = new System.Drawing.Point(3, 5);
            this.lblVariantName.Name = "lblVariantName";
            this.lblVariantName.Size = new System.Drawing.Size(38, 13);
            this.lblVariantName.TabIndex = 0;
            this.lblVariantName.Text = "Name:";
            // 
            // pnlSelectedPalleteContainer
            // 
            this.pnlSelectedPalleteContainer.Controls.Add(this.gpnlPallete);
            this.pnlSelectedPalleteContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSelectedPalleteContainer.Location = new System.Drawing.Point(0, 194);
            this.pnlSelectedPalleteContainer.Name = "pnlSelectedPalleteContainer";
            this.pnlSelectedPalleteContainer.Size = new System.Drawing.Size(543, 300);
            this.pnlSelectedPalleteContainer.TabIndex = 13;
            // 
            // gpnlPallete
            // 
            this.gpnlPallete.CanvasColor = System.Drawing.SystemColors.Control;
            this.gpnlPallete.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpnlPallete.Controls.Add(this.gpnlPlacements);
            this.gpnlPallete.Controls.Add(this.txtTotalCost);
            this.gpnlPallete.Controls.Add(this.lblTotalCost);
            this.gpnlPallete.Controls.Add(this.txtRuntimeMaximum);
            this.gpnlPallete.Controls.Add(this.lblRuntimeMaximum);
            this.gpnlPallete.Controls.Add(this.txtRuntimeMinimum);
            this.gpnlPallete.Controls.Add(this.lblRuntimeMinimum);
            this.gpnlPallete.Controls.Add(this.cmbxTagName);
            this.gpnlPallete.Controls.Add(this.lblSeperator);
            this.gpnlPallete.Controls.Add(this.cmbxTagClass);
            this.gpnlPallete.Controls.Add(this.lblTagIdent);
            this.gpnlPallete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpnlPallete.Location = new System.Drawing.Point(0, 0);
            this.gpnlPallete.Name = "gpnlPallete";
            this.gpnlPallete.Size = new System.Drawing.Size(543, 300);
            // 
            // 
            // 
            this.gpnlPallete.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpnlPallete.Style.BackColorGradientAngle = 90;
            this.gpnlPallete.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpnlPallete.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpnlPallete.Style.BorderBottomWidth = 1;
            this.gpnlPallete.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpnlPallete.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpnlPallete.Style.BorderLeftWidth = 1;
            this.gpnlPallete.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpnlPallete.Style.BorderRightWidth = 1;
            this.gpnlPallete.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpnlPallete.Style.BorderTopWidth = 1;
            this.gpnlPallete.Style.CornerDiameter = 4;
            this.gpnlPallete.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gpnlPallete.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gpnlPallete.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpnlPallete.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.gpnlPallete.TabIndex = 1;
            this.gpnlPallete.Text = "Selected Pallete Information";
            // 
            // gpnlPlacements
            // 
            this.gpnlPlacements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gpnlPlacements.CanvasColor = System.Drawing.SystemColors.Control;
            this.gpnlPlacements.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpnlPlacements.Controls.Add(this.txtCordZ);
            this.gpnlPlacements.Controls.Add(this.txtCordY);
            this.gpnlPlacements.Controls.Add(this.txtCordX);
            this.gpnlPlacements.Controls.Add(this.gpnlFlags);
            this.gpnlPlacements.Controls.Add(this.cmbxChunkType);
            this.gpnlPlacements.Controls.Add(this.txtCordRoll);
            this.gpnlPlacements.Controls.Add(this.lblCordRoll);
            this.gpnlPlacements.Controls.Add(this.lblChunkType);
            this.gpnlPlacements.Controls.Add(this.txtCordPitch);
            this.gpnlPlacements.Controls.Add(this.lblCordPitch);
            this.gpnlPlacements.Controls.Add(this.txtCordYaw);
            this.gpnlPlacements.Controls.Add(this.lblCordYaw);
            this.gpnlPlacements.Controls.Add(this.lblCordZ);
            this.gpnlPlacements.Controls.Add(this.lblCordY);
            this.gpnlPlacements.Controls.Add(this.lblCordX);
            this.gpnlPlacements.Controls.Add(this.btnPastePlacement);
            this.gpnlPlacements.Controls.Add(this.btnCopyPlacement);
            this.gpnlPlacements.Controls.Add(this.btnDeletePlacement);
            this.gpnlPlacements.Controls.Add(this.btnAddPlacement);
            this.gpnlPlacements.Controls.Add(this.cmbxPlacementChunk);
            this.gpnlPlacements.Location = new System.Drawing.Point(6, 107);
            this.gpnlPlacements.Name = "gpnlPlacements";
            this.gpnlPlacements.Size = new System.Drawing.Size(522, 165);
            // 
            // 
            // 
            this.gpnlPlacements.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpnlPlacements.Style.BackColorGradientAngle = 90;
            this.gpnlPlacements.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpnlPlacements.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpnlPlacements.Style.BorderBottomWidth = 1;
            this.gpnlPlacements.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpnlPlacements.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpnlPlacements.Style.BorderLeftWidth = 1;
            this.gpnlPlacements.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpnlPlacements.Style.BorderRightWidth = 1;
            this.gpnlPlacements.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpnlPlacements.Style.BorderTopWidth = 1;
            this.gpnlPlacements.Style.CornerDiameter = 4;
            this.gpnlPlacements.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gpnlPlacements.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gpnlPlacements.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpnlPlacements.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.gpnlPlacements.TabIndex = 10;
            this.gpnlPlacements.Text = "Placements";
            // 
            // txtCordZ
            // 
            // 
            // 
            // 
            this.txtCordZ.Border.Class = "TextBoxBorder";
            this.txtCordZ.Location = new System.Drawing.Point(24, 111);
            this.txtCordZ.Name = "txtCordZ";
            this.txtCordZ.Size = new System.Drawing.Size(114, 20);
            this.txtCordZ.TabIndex = 22;
            // 
            // txtCordY
            // 
            // 
            // 
            // 
            this.txtCordY.Border.Class = "TextBoxBorder";
            this.txtCordY.Location = new System.Drawing.Point(24, 85);
            this.txtCordY.Name = "txtCordY";
            this.txtCordY.Size = new System.Drawing.Size(114, 20);
            this.txtCordY.TabIndex = 21;
            // 
            // txtCordX
            // 
            // 
            // 
            // 
            this.txtCordX.Border.Class = "TextBoxBorder";
            this.txtCordX.Location = new System.Drawing.Point(24, 59);
            this.txtCordX.Name = "txtCordX";
            this.txtCordX.Size = new System.Drawing.Size(114, 20);
            this.txtCordX.TabIndex = 20;
            // 
            // gpnlFlags
            // 
            this.gpnlFlags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gpnlFlags.CanvasColor = System.Drawing.SystemColors.Control;
            this.gpnlFlags.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpnlFlags.Controls.Add(this.chkSymmetrical);
            this.gpnlFlags.Controls.Add(this.chkAsymmetrical);
            this.gpnlFlags.Controls.Add(this.chkPlaceAtStart);
            this.gpnlFlags.Location = new System.Drawing.Point(319, 30);
            this.gpnlFlags.Name = "gpnlFlags";
            this.gpnlFlags.Size = new System.Drawing.Size(194, 103);
            // 
            // 
            // 
            this.gpnlFlags.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpnlFlags.Style.BackColorGradientAngle = 90;
            this.gpnlFlags.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpnlFlags.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpnlFlags.Style.BorderBottomWidth = 1;
            this.gpnlFlags.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpnlFlags.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpnlFlags.Style.BorderLeftWidth = 1;
            this.gpnlFlags.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpnlFlags.Style.BorderRightWidth = 1;
            this.gpnlFlags.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpnlFlags.Style.BorderTopWidth = 1;
            this.gpnlFlags.Style.CornerDiameter = 4;
            this.gpnlFlags.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gpnlFlags.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gpnlFlags.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpnlFlags.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.gpnlFlags.TabIndex = 19;
            this.gpnlFlags.Text = "Flags";
            // 
            // chkSymmetrical
            // 
            this.chkSymmetrical.Location = new System.Drawing.Point(3, 52);
            this.chkSymmetrical.Name = "chkSymmetrical";
            this.chkSymmetrical.Size = new System.Drawing.Size(172, 23);
            this.chkSymmetrical.TabIndex = 2;
            this.chkSymmetrical.Text = "Symmetrical";
            // 
            // chkAsymmetrical
            // 
            this.chkAsymmetrical.Location = new System.Drawing.Point(3, 27);
            this.chkAsymmetrical.Name = "chkAsymmetrical";
            this.chkAsymmetrical.Size = new System.Drawing.Size(172, 23);
            this.chkAsymmetrical.TabIndex = 1;
            this.chkAsymmetrical.Text = "Asymmetrical";
            // 
            // chkPlaceAtStart
            // 
            this.chkPlaceAtStart.Location = new System.Drawing.Point(3, 3);
            this.chkPlaceAtStart.Name = "chkPlaceAtStart";
            this.chkPlaceAtStart.Size = new System.Drawing.Size(172, 23);
            this.chkPlaceAtStart.TabIndex = 0;
            this.chkPlaceAtStart.Text = "Place at Start";
            // 
            // cmbxChunkType
            // 
            this.cmbxChunkType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbxChunkType.DisplayMember = "Text";
            this.cmbxChunkType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbxChunkType.FormattingEnabled = true;
            this.cmbxChunkType.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3,
            this.comboItem4,
            this.comboItem5,
            this.comboItem6});
            this.cmbxChunkType.Location = new System.Drawing.Point(78, 30);
            this.cmbxChunkType.Name = "cmbxChunkType";
            this.cmbxChunkType.Size = new System.Drawing.Size(235, 21);
            this.cmbxChunkType.TabIndex = 18;
            // 
            // txtCordRoll
            // 
            this.txtCordRoll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtCordRoll.Border.Class = "TextBoxBorder";
            this.txtCordRoll.Location = new System.Drawing.Point(182, 111);
            this.txtCordRoll.Name = "txtCordRoll";
            this.txtCordRoll.Size = new System.Drawing.Size(130, 20);
            this.txtCordRoll.TabIndex = 16;
            // 
            // lblCordRoll
            // 
            this.lblCordRoll.AutoSize = true;
            this.lblCordRoll.Location = new System.Drawing.Point(144, 114);
            this.lblCordRoll.Name = "lblCordRoll";
            this.lblCordRoll.Size = new System.Drawing.Size(28, 13);
            this.lblCordRoll.TabIndex = 15;
            this.lblCordRoll.Text = "Roll:";
            // 
            // lblChunkType
            // 
            this.lblChunkType.AutoSize = true;
            this.lblChunkType.Location = new System.Drawing.Point(4, 33);
            this.lblChunkType.Name = "lblChunkType";
            this.lblChunkType.Size = new System.Drawing.Size(68, 13);
            this.lblChunkType.TabIndex = 17;
            this.lblChunkType.Text = "Chunk Type:";
            // 
            // txtCordPitch
            // 
            this.txtCordPitch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtCordPitch.Border.Class = "TextBoxBorder";
            this.txtCordPitch.Location = new System.Drawing.Point(182, 85);
            this.txtCordPitch.Name = "txtCordPitch";
            this.txtCordPitch.Size = new System.Drawing.Size(130, 20);
            this.txtCordPitch.TabIndex = 14;
            // 
            // lblCordPitch
            // 
            this.lblCordPitch.AutoSize = true;
            this.lblCordPitch.Location = new System.Drawing.Point(144, 87);
            this.lblCordPitch.Name = "lblCordPitch";
            this.lblCordPitch.Size = new System.Drawing.Size(34, 13);
            this.lblCordPitch.TabIndex = 13;
            this.lblCordPitch.Text = "Pitch:";
            // 
            // txtCordYaw
            // 
            this.txtCordYaw.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtCordYaw.Border.Class = "TextBoxBorder";
            this.txtCordYaw.Location = new System.Drawing.Point(182, 59);
            this.txtCordYaw.Name = "txtCordYaw";
            this.txtCordYaw.Size = new System.Drawing.Size(130, 20);
            this.txtCordYaw.TabIndex = 12;
            // 
            // lblCordYaw
            // 
            this.lblCordYaw.AutoSize = true;
            this.lblCordYaw.Location = new System.Drawing.Point(144, 62);
            this.lblCordYaw.Name = "lblCordYaw";
            this.lblCordYaw.Size = new System.Drawing.Size(31, 13);
            this.lblCordYaw.TabIndex = 11;
            this.lblCordYaw.Text = "Yaw:";
            // 
            // lblCordZ
            // 
            this.lblCordZ.AutoSize = true;
            this.lblCordZ.Location = new System.Drawing.Point(1, 113);
            this.lblCordZ.Name = "lblCordZ";
            this.lblCordZ.Size = new System.Drawing.Size(17, 13);
            this.lblCordZ.TabIndex = 9;
            this.lblCordZ.Text = "Z:";
            // 
            // lblCordY
            // 
            this.lblCordY.AutoSize = true;
            this.lblCordY.Location = new System.Drawing.Point(1, 87);
            this.lblCordY.Name = "lblCordY";
            this.lblCordY.Size = new System.Drawing.Size(17, 13);
            this.lblCordY.TabIndex = 7;
            this.lblCordY.Text = "Y:";
            // 
            // lblCordX
            // 
            this.lblCordX.AutoSize = true;
            this.lblCordX.Location = new System.Drawing.Point(1, 61);
            this.lblCordX.Name = "lblCordX";
            this.lblCordX.Size = new System.Drawing.Size(17, 13);
            this.lblCordX.TabIndex = 5;
            this.lblCordX.Text = "X:";
            // 
            // btnPastePlacement
            // 
            this.btnPastePlacement.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPastePlacement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPastePlacement.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPastePlacement.Location = new System.Drawing.Point(463, 1);
            this.btnPastePlacement.Name = "btnPastePlacement";
            this.btnPastePlacement.Size = new System.Drawing.Size(50, 23);
            this.btnPastePlacement.TabIndex = 4;
            this.btnPastePlacement.Text = "Paste";
            // 
            // btnCopyPlacement
            // 
            this.btnCopyPlacement.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCopyPlacement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyPlacement.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCopyPlacement.Location = new System.Drawing.Point(407, 1);
            this.btnCopyPlacement.Name = "btnCopyPlacement";
            this.btnCopyPlacement.Size = new System.Drawing.Size(50, 23);
            this.btnCopyPlacement.TabIndex = 3;
            this.btnCopyPlacement.Text = "Copy";
            // 
            // btnDeletePlacement
            // 
            this.btnDeletePlacement.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDeletePlacement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeletePlacement.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDeletePlacement.Location = new System.Drawing.Point(351, 1);
            this.btnDeletePlacement.Name = "btnDeletePlacement";
            this.btnDeletePlacement.Size = new System.Drawing.Size(50, 23);
            this.btnDeletePlacement.TabIndex = 2;
            this.btnDeletePlacement.Text = "Delete";
            // 
            // btnAddPlacement
            // 
            this.btnAddPlacement.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddPlacement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPlacement.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAddPlacement.Location = new System.Drawing.Point(295, 1);
            this.btnAddPlacement.Name = "btnAddPlacement";
            this.btnAddPlacement.Size = new System.Drawing.Size(50, 23);
            this.btnAddPlacement.TabIndex = 1;
            this.btnAddPlacement.Text = "Add";
            // 
            // cmbxPlacementChunk
            // 
            this.cmbxPlacementChunk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbxPlacementChunk.DisplayMember = "Text";
            this.cmbxPlacementChunk.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbxPlacementChunk.FormattingEnabled = true;
            this.cmbxPlacementChunk.Location = new System.Drawing.Point(4, 3);
            this.cmbxPlacementChunk.Name = "cmbxPlacementChunk";
            this.cmbxPlacementChunk.Size = new System.Drawing.Size(285, 21);
            this.cmbxPlacementChunk.TabIndex = 0;
            this.cmbxPlacementChunk.SelectedIndexChanged += new System.EventHandler(this.cmbxPlacementChunk_SelectedIndexChanged);
            // 
            // txtTotalCost
            // 
            this.txtTotalCost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtTotalCost.Border.Class = "TextBoxBorder";
            this.txtTotalCost.Location = new System.Drawing.Point(102, 81);
            this.txtTotalCost.Name = "txtTotalCost";
            this.txtTotalCost.Size = new System.Drawing.Size(426, 20);
            this.txtTotalCost.TabIndex = 9;
            // 
            // lblTotalCost
            // 
            this.lblTotalCost.AutoSize = true;
            this.lblTotalCost.Location = new System.Drawing.Point(3, 83);
            this.lblTotalCost.Name = "lblTotalCost";
            this.lblTotalCost.Size = new System.Drawing.Size(58, 13);
            this.lblTotalCost.TabIndex = 8;
            this.lblTotalCost.Text = "Total Cost:";
            // 
            // txtRuntimeMaximum
            // 
            this.txtRuntimeMaximum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtRuntimeMaximum.Border.Class = "TextBoxBorder";
            this.txtRuntimeMaximum.Location = new System.Drawing.Point(102, 55);
            this.txtRuntimeMaximum.Name = "txtRuntimeMaximum";
            this.txtRuntimeMaximum.Size = new System.Drawing.Size(426, 20);
            this.txtRuntimeMaximum.TabIndex = 7;
            // 
            // lblRuntimeMaximum
            // 
            this.lblRuntimeMaximum.AutoSize = true;
            this.lblRuntimeMaximum.Location = new System.Drawing.Point(3, 57);
            this.lblRuntimeMaximum.Name = "lblRuntimeMaximum";
            this.lblRuntimeMaximum.Size = new System.Drawing.Size(96, 13);
            this.lblRuntimeMaximum.TabIndex = 6;
            this.lblRuntimeMaximum.Text = "Runtime Maximum:";
            // 
            // txtRuntimeMinimum
            // 
            this.txtRuntimeMinimum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtRuntimeMinimum.Border.Class = "TextBoxBorder";
            this.txtRuntimeMinimum.Location = new System.Drawing.Point(102, 29);
            this.txtRuntimeMinimum.Name = "txtRuntimeMinimum";
            this.txtRuntimeMinimum.Size = new System.Drawing.Size(426, 20);
            this.txtRuntimeMinimum.TabIndex = 5;
            // 
            // lblRuntimeMinimum
            // 
            this.lblRuntimeMinimum.AutoSize = true;
            this.lblRuntimeMinimum.Location = new System.Drawing.Point(3, 31);
            this.lblRuntimeMinimum.Name = "lblRuntimeMinimum";
            this.lblRuntimeMinimum.Size = new System.Drawing.Size(93, 13);
            this.lblRuntimeMinimum.TabIndex = 4;
            this.lblRuntimeMinimum.Text = "Runtime Minimum:";
            // 
            // cmbxTagName
            // 
            this.cmbxTagName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbxTagName.DisplayMember = "Text";
            this.cmbxTagName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbxTagName.FormattingEnabled = true;
            this.cmbxTagName.Location = new System.Drawing.Point(156, 3);
            this.cmbxTagName.Name = "cmbxTagName";
            this.cmbxTagName.Size = new System.Drawing.Size(372, 21);
            this.cmbxTagName.TabIndex = 3;
            // 
            // lblSeperator
            // 
            this.lblSeperator.AutoSize = true;
            this.lblSeperator.Location = new System.Drawing.Point(140, 7);
            this.lblSeperator.Name = "lblSeperator";
            this.lblSeperator.Size = new System.Drawing.Size(10, 13);
            this.lblSeperator.TabIndex = 2;
            this.lblSeperator.Text = "-";
            // 
            // cmbxTagClass
            // 
            this.cmbxTagClass.DisplayMember = "Text";
            this.cmbxTagClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbxTagClass.FormattingEnabled = true;
            this.cmbxTagClass.Location = new System.Drawing.Point(65, 3);
            this.cmbxTagClass.Name = "cmbxTagClass";
            this.cmbxTagClass.Size = new System.Drawing.Size(69, 21);
            this.cmbxTagClass.TabIndex = 1;
            this.cmbxTagClass.SelectedIndexChanged += new System.EventHandler(this.cmbxTagClass_SelectedIndexChanged);
            // 
            // lblTagIdent
            // 
            this.lblTagIdent.AutoSize = true;
            this.lblTagIdent.Location = new System.Drawing.Point(3, 7);
            this.lblTagIdent.Name = "lblTagIdent";
            this.lblTagIdent.Size = new System.Drawing.Size(56, 13);
            this.lblTagIdent.TabIndex = 0;
            this.lblTagIdent.Text = "Tag Ident:";
            // 
            // pnlButtonContainer
            // 
            this.pnlButtonContainer.Controls.Add(this.btnSaveChanges);
            this.pnlButtonContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtonContainer.Location = new System.Drawing.Point(0, 494);
            this.pnlButtonContainer.Name = "pnlButtonContainer";
            this.pnlButtonContainer.Size = new System.Drawing.Size(543, 27);
            this.pnlButtonContainer.TabIndex = 12;
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSaveChanges.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSaveChanges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveChanges.Location = new System.Drawing.Point(0, 0);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(543, 27);
            this.btnSaveChanges.TabIndex = 11;
            this.btnSaveChanges.Text = "Save Changes";
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "Player Spawn";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "Reserved";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "Added";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "Original";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "Editted";
            // 
            // comboItem6
            // 
            this.comboItem6.Text = "Null";
            // 
            // UserMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 521);
            this.Controls.Add(this.splitContainer1);
            this.Name = "UserMapForm";
            this.Text = "UserMapForm";
            this.Load += new System.EventHandler(this.UserMapForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.pnlBackGroundEditor.ResumeLayout(false);
            this.pnlVariantInfoContainer.ResumeLayout(false);
            this.gpnlVariantInformation.ResumeLayout(false);
            this.gpnlVariantInformation.PerformLayout();
            this.pnlSelectedPalleteContainer.ResumeLayout(false);
            this.gpnlPallete.ResumeLayout(false);
            this.gpnlPallete.PerformLayout();
            this.gpnlPlacements.ResumeLayout(false);
            this.gpnlPlacements.PerformLayout();
            this.gpnlFlags.ResumeLayout(false);
            this.pnlButtonContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvTagReferences;
        private DevComponents.DotNetBar.PanelEx pnlBackGroundEditor;
        private DevComponents.DotNetBar.Controls.GroupPanel gpnlPallete;
        private DevComponents.DotNetBar.Controls.GroupPanel gpnlVariantInformation;
        private DevComponents.DotNetBar.Controls.TextBoxX txtVariantName;
        private System.Windows.Forms.Label lblVariantName;
        private DevComponents.DotNetBar.Controls.TextBoxX txtVariantDescription;
        private System.Windows.Forms.Label lblVariantDescription;
        private DevComponents.DotNetBar.Controls.TextBoxX txtVariantAuthor;
        private System.Windows.Forms.Label lblVariantAuthor;
        private System.Windows.Forms.Label lblSeperator;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbxTagClass;
        private System.Windows.Forms.Label lblTagIdent;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbxTagName;
        private System.Windows.Forms.Label lblRuntimeMinimum;
        private DevComponents.DotNetBar.Controls.TextBoxX txtRuntimeMaximum;
        private System.Windows.Forms.Label lblRuntimeMaximum;
        private DevComponents.DotNetBar.Controls.TextBoxX txtRuntimeMinimum;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTotalCost;
        private System.Windows.Forms.Label lblTotalCost;
        private DevComponents.DotNetBar.Controls.GroupPanel gpnlPlacements;
        private DevComponents.DotNetBar.ButtonX btnAddPlacement;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbxPlacementChunk;
        private DevComponents.DotNetBar.ButtonX btnCopyPlacement;
        private DevComponents.DotNetBar.ButtonX btnDeletePlacement;
        private DevComponents.DotNetBar.ButtonX btnPastePlacement;
        private System.Windows.Forms.Label lblCordY;
        private System.Windows.Forms.Label lblCordX;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCordRoll;
        private System.Windows.Forms.Label lblCordRoll;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCordPitch;
        private System.Windows.Forms.Label lblCordPitch;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCordYaw;
        private System.Windows.Forms.Label lblCordYaw;
        private System.Windows.Forms.Label lblCordZ;
        private DevComponents.DotNetBar.Controls.GroupPanel gpnlFlags;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkPlaceAtStart;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbxChunkType;
        private System.Windows.Forms.Label lblChunkType;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkAsymmetrical;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkSymmetrical;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCordZ;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCordY;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCordX;
        private DevComponents.DotNetBar.ButtonX btnSaveChanges;
        private System.Windows.Forms.Panel pnlVariantInfoContainer;
        private System.Windows.Forms.Panel pnlSelectedPalleteContainer;
        private System.Windows.Forms.Panel pnlButtonContainer;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem5;
        private DevComponents.Editors.ComboItem comboItem6;
    }
}
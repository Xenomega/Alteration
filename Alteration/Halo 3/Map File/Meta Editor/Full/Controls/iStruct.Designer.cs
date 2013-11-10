namespace Alteration.Halo_3.Meta_Editor.Controls
{
    partial class iStruct
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
            try { UnloadMetaEditor(); }
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offsetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedChunkOffsetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pointerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.chunkDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chunkDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chunkDataForAllChunksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.disableReflexiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableChunkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlValues = new System.Windows.Forms.Panel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnResize = new DevComponents.DotNetBar.ButtonX();
            this.comboChunks = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblName = new System.Windows.Forms.Label();
            this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
            this.contextMenuStrip1.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator1,
            this.disableReflexiveToolStripMenuItem,
            this.disableChunkToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(168, 98);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.offsetToolStripMenuItem,
            this.sizeToolStripMenuItem,
            this.selectedChunkOffsetToolStripMenuItem,
            this.pointerToolStripMenuItem,
            this.toolStripSeparator2,
            this.chunkDToolStripMenuItem});
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // offsetToolStripMenuItem
            // 
            this.offsetToolStripMenuItem.Name = "offsetToolStripMenuItem";
            this.offsetToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.offsetToolStripMenuItem.Text = "Offset";
            this.offsetToolStripMenuItem.Click += new System.EventHandler(this.offsetToolStripMenuItem_Click);
            // 
            // sizeToolStripMenuItem
            // 
            this.sizeToolStripMenuItem.Name = "sizeToolStripMenuItem";
            this.sizeToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.sizeToolStripMenuItem.Text = "Size";
            this.sizeToolStripMenuItem.Click += new System.EventHandler(this.copySizeToolStripMenuItem_Click);
            // 
            // selectedChunkOffsetToolStripMenuItem
            // 
            this.selectedChunkOffsetToolStripMenuItem.Name = "selectedChunkOffsetToolStripMenuItem";
            this.selectedChunkOffsetToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.selectedChunkOffsetToolStripMenuItem.Text = "Selected Chunk Offset";
            this.selectedChunkOffsetToolStripMenuItem.Click += new System.EventHandler(this.copySelectedChunkOffsetToolStripMenuItem_Click);
            // 
            // pointerToolStripMenuItem
            // 
            this.pointerToolStripMenuItem.Name = "pointerToolStripMenuItem";
            this.pointerToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.pointerToolStripMenuItem.Text = "Pointer";
            this.pointerToolStripMenuItem.Click += new System.EventHandler(this.copyPointerToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(190, 6);
            // 
            // chunkDToolStripMenuItem
            // 
            this.chunkDToolStripMenuItem.Name = "chunkDToolStripMenuItem";
            this.chunkDToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.chunkDToolStripMenuItem.Text = "Chunk Data";
            this.chunkDToolStripMenuItem.Click += new System.EventHandler(this.chunkDToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chunkDataToolStripMenuItem,
            this.chunkDataForAllChunksToolStripMenuItem});
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // chunkDataToolStripMenuItem
            // 
            this.chunkDataToolStripMenuItem.Name = "chunkDataToolStripMenuItem";
            this.chunkDataToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.chunkDataToolStripMenuItem.Text = "Chunk Data";
            this.chunkDataToolStripMenuItem.Click += new System.EventHandler(this.chunkDataToolStripMenuItem_Click);
            // 
            // chunkDataForAllChunksToolStripMenuItem
            // 
            this.chunkDataForAllChunksToolStripMenuItem.Name = "chunkDataForAllChunksToolStripMenuItem";
            this.chunkDataForAllChunksToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.chunkDataForAllChunksToolStripMenuItem.Text = "Chunk Data for All Chunks";
            this.chunkDataForAllChunksToolStripMenuItem.Click += new System.EventHandler(this.chunkDataForAllChunksToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(164, 6);
            // 
            // disableReflexiveToolStripMenuItem
            // 
            this.disableReflexiveToolStripMenuItem.Name = "disableReflexiveToolStripMenuItem";
            this.disableReflexiveToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.disableReflexiveToolStripMenuItem.Text = "Disable Reflexive";
            this.disableReflexiveToolStripMenuItem.Click += new System.EventHandler(this.disableReflexiveToolStripMenuItem_Click);
            // 
            // disableChunkToolStripMenuItem
            // 
            this.disableChunkToolStripMenuItem.Name = "disableChunkToolStripMenuItem";
            this.disableChunkToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.disableChunkToolStripMenuItem.Text = "Disable Chunk";
            this.disableChunkToolStripMenuItem.Visible = false;
            this.disableChunkToolStripMenuItem.Click += new System.EventHandler(this.disableChunkToolStripMenuItem_Click);
            // 
            // pnlValues
            // 
            this.pnlValues.Location = new System.Drawing.Point(14, 32);
            this.pnlValues.Name = "pnlValues";
            this.pnlValues.Size = new System.Drawing.Size(791, 125);
            this.pnlValues.TabIndex = 2;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlHeader.ContextMenuStrip = this.contextMenuStrip1;
            this.pnlHeader.Controls.Add(this.btnResize);
            this.pnlHeader.Controls.Add(this.comboChunks);
            this.pnlHeader.Controls.Add(this.lblName);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(450, 218);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnResize
            // 
            this.btnResize.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnResize.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnResize.Location = new System.Drawing.Point(8, 5);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(28, 21);
            this.btnResize.TabIndex = 3;
            this.btnResize.Text = "-";
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // comboChunks
            // 
            this.comboChunks.ContextMenuStrip = this.contextMenuStrip1;
            this.comboChunks.DisplayMember = "Text";
            this.comboChunks.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboChunks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboChunks.FormattingEnabled = true;
            this.comboChunks.Location = new System.Drawing.Point(243, 6);
            this.comboChunks.Name = "comboChunks";
            this.comboChunks.Size = new System.Drawing.Size(190, 21);
            this.comboChunks.TabIndex = 2;
            this.comboChunks.SelectedIndexChanged += new System.EventHandler(this.comboChunks_SelectedIndexChanged);
            // 
            // lblName
            // 
            this.lblName.ContextMenuStrip = this.contextMenuStrip1;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(44, 4);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(193, 23);
            this.superTooltip1.SetSuperTooltip(this.lblName, new DevComponents.DotNetBar.SuperTooltipInfo("Label", "", "The label/name of the value.", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
            this.lblName.TabIndex = 1;
            this.lblName.Text = "STRUCTURE";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // iStruct
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pnlValues);
            this.Controls.Add(this.pnlHeader);
            this.Name = "iStruct";
            this.Size = new System.Drawing.Size(1193, 157);
            this.superTooltip1.SetSuperTooltip(this, new DevComponents.DotNetBar.SuperTooltipInfo("Structure", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, true, false, new System.Drawing.Size(0, 0)));
            this.contextMenuStrip1.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Panel pnlValues;
        private System.Windows.Forms.Panel pnlHeader;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboChunks;
        private System.Windows.Forms.Label lblName;
        private DevComponents.DotNetBar.ButtonX btnResize;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem disableReflexiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectedChunkOffsetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pointerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem chunkDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chunkDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableChunkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chunkDataForAllChunksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offsetToolStripMenuItem;
        private DevComponents.DotNetBar.SuperTooltip superTooltip1;

    }
}

namespace Alteration.Halo_3.Meta_Grid
{
    partial class MetaGrid
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
            this.components = new System.ComponentModel.Container();
            this.identGrid = new System.Windows.Forms.ListView();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.ctxIdentGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.swapSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.goToSelectedTagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inCurrentTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inNewTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.showToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.structuresToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.identsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.stringsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.reflexiveGrid = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.ctxReflexiveGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.structuresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.identsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stringsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBar1 = new TD.SandBar.MenuBar();
            this.menuBarItem2 = new TD.SandBar.MenuBarItem();
            this.menuBarItem1 = new TD.SandBar.MenuBarItem();
            this.menuButtonItem1 = new TD.SandBar.MenuButtonItem();
            this.menuButtonItem2 = new TD.SandBar.MenuButtonItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.stringGrid = new System.Windows.Forms.ListView();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
            this.menuButtonItem3 = new TD.SandBar.MenuButtonItem();
            this.ctxIdentGrid.SuspendLayout();
            this.ctxReflexiveGrid.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // identGrid
            // 
            this.identGrid.BackColor = System.Drawing.Color.White;
            this.identGrid.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.identGrid.ContextMenuStrip = this.ctxIdentGrid;
            this.identGrid.ForeColor = System.Drawing.Color.Black;
            this.identGrid.GridLines = true;
            this.identGrid.Location = new System.Drawing.Point(31, 23);
            this.identGrid.Name = "identGrid";
            this.identGrid.Size = new System.Drawing.Size(494, 321);
            this.identGrid.TabIndex = 8;
            this.identGrid.UseCompatibleStateImageBehavior = false;
            this.identGrid.View = System.Windows.Forms.View.Details;
            this.identGrid.SelectedIndexChanged += new System.EventHandler(this.identGrid_SelectedIndexChanged);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Name";
            this.columnHeader6.Width = 119;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Offset";
            this.columnHeader7.Width = 84;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Tag Class";
            this.columnHeader8.Width = 92;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Tag Name";
            this.columnHeader9.Width = 237;
            // 
            // ctxIdentGrid
            // 
            this.ctxIdentGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.swapSelectedToolStripMenuItem,
            this.toolStripSeparator1,
            this.goToSelectedTagToolStripMenuItem,
            this.toolStripSeparator2,
            this.showToolStripMenuItem1});
            this.ctxIdentGrid.Name = "ctxIdentGrid";
            this.ctxIdentGrid.Size = new System.Drawing.Size(185, 82);
            // 
            // swapSelectedToolStripMenuItem
            // 
            this.swapSelectedToolStripMenuItem.Name = "swapSelectedToolStripMenuItem";
            this.swapSelectedToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.swapSelectedToolStripMenuItem.Text = "Swap Selected";
            this.swapSelectedToolStripMenuItem.Click += new System.EventHandler(this.swapSelectedToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // goToSelectedTagToolStripMenuItem
            // 
            this.goToSelectedTagToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inCurrentTabToolStripMenuItem,
            this.inNewTabToolStripMenuItem});
            this.goToSelectedTagToolStripMenuItem.Name = "goToSelectedTagToolStripMenuItem";
            this.goToSelectedTagToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.goToSelectedTagToolStripMenuItem.Text = "Go to Selected Tag..";
            // 
            // inCurrentTabToolStripMenuItem
            // 
            this.inCurrentTabToolStripMenuItem.Name = "inCurrentTabToolStripMenuItem";
            this.inCurrentTabToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.inCurrentTabToolStripMenuItem.Text = "In Current Tab";
            this.inCurrentTabToolStripMenuItem.Click += new System.EventHandler(this.goToSelectedTagToolStripMenuItem_Click);
            // 
            // inNewTabToolStripMenuItem
            // 
            this.inNewTabToolStripMenuItem.Name = "inNewTabToolStripMenuItem";
            this.inNewTabToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.inNewTabToolStripMenuItem.Text = "In New Tab";
            this.inNewTabToolStripMenuItem.Click += new System.EventHandler(this.inNewTabToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(181, 6);
            // 
            // showToolStripMenuItem1
            // 
            this.showToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.structuresToolStripMenuItem1,
            this.identsToolStripMenuItem1,
            this.stringsToolStripMenuItem1});
            this.showToolStripMenuItem1.Name = "showToolStripMenuItem1";
            this.showToolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            this.showToolStripMenuItem1.Text = "Show";
            // 
            // structuresToolStripMenuItem1
            // 
            this.structuresToolStripMenuItem1.Name = "structuresToolStripMenuItem1";
            this.structuresToolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
            this.structuresToolStripMenuItem1.Text = "Structures";
            this.structuresToolStripMenuItem1.Click += new System.EventHandler(this.structuresToolStripMenuItem1_Click);
            // 
            // identsToolStripMenuItem1
            // 
            this.identsToolStripMenuItem1.Name = "identsToolStripMenuItem1";
            this.identsToolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
            this.identsToolStripMenuItem1.Text = "Idents";
            this.identsToolStripMenuItem1.Click += new System.EventHandler(this.identsToolStripMenuItem1_Click);
            // 
            // stringsToolStripMenuItem1
            // 
            this.stringsToolStripMenuItem1.Name = "stringsToolStripMenuItem1";
            this.stringsToolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
            this.stringsToolStripMenuItem1.Text = "Strings";
            this.stringsToolStripMenuItem1.Click += new System.EventHandler(this.stringsToolStripMenuItem1_Click);
            // 
            // reflexiveGrid
            // 
            this.reflexiveGrid.BackColor = System.Drawing.Color.White;
            this.reflexiveGrid.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.reflexiveGrid.ContextMenuStrip = this.ctxReflexiveGrid;
            this.reflexiveGrid.ForeColor = System.Drawing.Color.Black;
            this.reflexiveGrid.GridLines = true;
            this.reflexiveGrid.Location = new System.Drawing.Point(3, 23);
            this.reflexiveGrid.Name = "reflexiveGrid";
            this.reflexiveGrid.Size = new System.Drawing.Size(625, 321);
            this.reflexiveGrid.TabIndex = 9;
            this.reflexiveGrid.UseCompatibleStateImageBehavior = false;
            this.reflexiveGrid.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 119;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Offset";
            this.columnHeader2.Width = 84;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Chunk Count";
            this.columnHeader3.Width = 92;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Pointer";
            this.columnHeader4.Width = 141;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Chunk Size";
            this.columnHeader5.Width = 164;
            // 
            // ctxReflexiveGrid
            // 
            this.ctxReflexiveGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem});
            this.ctxReflexiveGrid.Name = "ctxReflexiveGrid";
            this.ctxReflexiveGrid.Size = new System.Drawing.Size(112, 26);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.structuresToolStripMenuItem,
            this.identsToolStripMenuItem,
            this.stringsToolStripMenuItem});
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.showToolStripMenuItem.Text = "Show";
            // 
            // structuresToolStripMenuItem
            // 
            this.structuresToolStripMenuItem.Name = "structuresToolStripMenuItem";
            this.structuresToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.structuresToolStripMenuItem.Text = "Structures";
            this.structuresToolStripMenuItem.Click += new System.EventHandler(this.structuresToolStripMenuItem_Click);
            // 
            // identsToolStripMenuItem
            // 
            this.identsToolStripMenuItem.Name = "identsToolStripMenuItem";
            this.identsToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.identsToolStripMenuItem.Text = "Idents";
            this.identsToolStripMenuItem.Click += new System.EventHandler(this.identsToolStripMenuItem_Click);
            // 
            // stringsToolStripMenuItem
            // 
            this.stringsToolStripMenuItem.Name = "stringsToolStripMenuItem";
            this.stringsToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.stringsToolStripMenuItem.Text = "Strings";
            this.stringsToolStripMenuItem.Click += new System.EventHandler(this.stringsToolStripMenuItem_Click);
            // 
            // menuBar1
            // 
            this.menuBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuBar1.Guid = new System.Guid("df697c1f-13d5-4c45-a508-b28740a98cb5");
            this.menuBar1.Items.AddRange(new TD.SandBar.ToolbarItemBase[] {
            this.menuBarItem2,
            this.menuBarItem1});
            this.menuBar1.Location = new System.Drawing.Point(0, 379);
            this.menuBar1.Name = "menuBar1";
            this.menuBar1.OwnerForm = null;
            this.menuBar1.Size = new System.Drawing.Size(528, 22);
            this.menuBar1.TabIndex = 10;
            this.menuBar1.Text = "menuBar1";
            // 
            // menuBarItem2
            // 
            this.menuBarItem2.Text = "Poke Idents";
            this.menuBarItem2.BeforePopup += new TD.SandBar.MenuItemBase.BeforePopupEventHandler(this.menuBarItem2_BeforePopup);
            // 
            // menuBarItem1
            // 
            this.menuBarItem1.Items.AddRange(new TD.SandBar.ToolbarItemBase[] {
            this.menuButtonItem1,
            this.menuButtonItem2,
            this.menuButtonItem3});
            this.menuBarItem1.Text = "Show";
            // 
            // menuButtonItem1
            // 
            this.menuButtonItem1.Text = "Structures";
            this.menuButtonItem1.Activate += new System.EventHandler(this.menuButtonItem1_Activate);
            // 
            // menuButtonItem2
            // 
            this.menuButtonItem2.Text = "Idents";
            this.menuButtonItem2.Activate += new System.EventHandler(this.menuButtonItem2_Activate);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.stringGrid);
            this.panel1.Controls.Add(this.reflexiveGrid);
            this.panel1.Controls.Add(this.identGrid);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(528, 379);
            this.panel1.TabIndex = 11;
            // 
            // stringGrid
            // 
            this.stringGrid.BackColor = System.Drawing.Color.White;
            this.stringGrid.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14});
            this.stringGrid.ContextMenuStrip = this.ctxIdentGrid;
            this.stringGrid.ForeColor = System.Drawing.Color.Black;
            this.stringGrid.GridLines = true;
            this.stringGrid.Location = new System.Drawing.Point(14, 3);
            this.stringGrid.Name = "stringGrid";
            this.stringGrid.Size = new System.Drawing.Size(459, 321);
            this.stringGrid.TabIndex = 10;
            this.stringGrid.UseCompatibleStateImageBehavior = false;
            this.stringGrid.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Name";
            this.columnHeader10.Width = 119;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Offset";
            this.columnHeader11.Width = 84;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Index";
            this.columnHeader12.Width = 68;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "ID";
            this.columnHeader13.Width = 63;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "String";
            this.columnHeader14.Width = 112;
            // 
            // menuButtonItem3
            // 
            this.menuButtonItem3.Text = "Strings";
            this.menuButtonItem3.Activate += new System.EventHandler(this.menuButtonItem3_Activate);
            // 
            // MetaGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuBar1);
            this.Name = "MetaGrid";
            this.Size = new System.Drawing.Size(528, 401);
            this.ctxIdentGrid.ResumeLayout(false);
            this.ctxReflexiveGrid.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListView identGrid;
        internal System.Windows.Forms.ColumnHeader columnHeader6;
        internal System.Windows.Forms.ColumnHeader columnHeader7;
        internal System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        internal System.Windows.Forms.ListView reflexiveGrid;
        internal System.Windows.Forms.ColumnHeader columnHeader1;
        internal System.Windows.Forms.ColumnHeader columnHeader2;
        internal System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ContextMenuStrip ctxReflexiveGrid;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem structuresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem identsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ctxIdentGrid;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem identsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem structuresToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem swapSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem goToSelectedTagToolStripMenuItem;
        private TD.SandBar.MenuBar menuBar1;
        private TD.SandBar.MenuBarItem menuBarItem2;
        private TD.SandBar.MenuBarItem menuBarItem1;
        private TD.SandBar.MenuButtonItem menuButtonItem1;
        private TD.SandBar.MenuButtonItem menuButtonItem2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem inCurrentTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inNewTabToolStripMenuItem;
        internal System.Windows.Forms.ListView stringGrid;
        internal System.Windows.Forms.ColumnHeader columnHeader10;
        internal System.Windows.Forms.ColumnHeader columnHeader11;
        internal System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ToolStripMenuItem stringsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem stringsToolStripMenuItem;
        private TD.SandBar.MenuButtonItem menuButtonItem3;
    }
}

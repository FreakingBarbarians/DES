namespace DESEditor
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newActionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadActionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCurrentActionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportCurrentActionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tv = new System.Windows.Forms.TreeView();
            this.DynamicSpace = new System.Windows.Forms.Panel();
            this.AddEffectContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddEffectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveActionDialog = new System.Windows.Forms.SaveFileDialog();
            this.AddRequirementContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addRequirementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EffectContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportActionDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.AddEffectContext.SuspendLayout();
            this.AddRequirementContext.SuspendLayout();
            this.EffectContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(858, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newActionToolStripMenuItem,
            this.loadActionToolStripMenuItem,
            this.saveCurrentActionToolStripMenuItem,
            this.exportCurrentActionToolStripMenuItem});
            this.toolStripMenuItem1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // newActionToolStripMenuItem
            // 
            this.newActionToolStripMenuItem.Name = "newActionToolStripMenuItem";
            this.newActionToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.newActionToolStripMenuItem.Text = "New Action";
            this.newActionToolStripMenuItem.Click += new System.EventHandler(this.NewAction);
            // 
            // loadActionToolStripMenuItem
            // 
            this.loadActionToolStripMenuItem.Name = "loadActionToolStripMenuItem";
            this.loadActionToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.loadActionToolStripMenuItem.Text = "Load Action From File";
            this.loadActionToolStripMenuItem.Click += new System.EventHandler(this.loadActionToolStripMenuItem_Click);
            // 
            // saveCurrentActionToolStripMenuItem
            // 
            this.saveCurrentActionToolStripMenuItem.Name = "saveCurrentActionToolStripMenuItem";
            this.saveCurrentActionToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.saveCurrentActionToolStripMenuItem.Text = "Save Current Action";
            this.saveCurrentActionToolStripMenuItem.Click += new System.EventHandler(this.saveCurrentActionToolStripMenuItem_Click);
            // 
            // exportCurrentActionToolStripMenuItem
            // 
            this.exportCurrentActionToolStripMenuItem.Name = "exportCurrentActionToolStripMenuItem";
            this.exportCurrentActionToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.exportCurrentActionToolStripMenuItem.Text = "Export Current Action";
            this.exportCurrentActionToolStripMenuItem.Click += new System.EventHandler(this.exportCurrentAction);
            // 
            // tv
            // 
            this.tv.Dock = System.Windows.Forms.DockStyle.Left;
            this.tv.Location = new System.Drawing.Point(0, 24);
            this.tv.Name = "tv";
            this.tv.Size = new System.Drawing.Size(136, 396);
            this.tv.TabIndex = 1;
            this.tv.Click += new System.EventHandler(this.TreeViewMouseClick);
            // 
            // DynamicSpace
            // 
            this.DynamicSpace.AutoSize = true;
            this.DynamicSpace.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DynamicSpace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DynamicSpace.Location = new System.Drawing.Point(136, 24);
            this.DynamicSpace.Name = "DynamicSpace";
            this.DynamicSpace.Size = new System.Drawing.Size(722, 396);
            this.DynamicSpace.TabIndex = 3;
            this.DynamicSpace.Paint += new System.Windows.Forms.PaintEventHandler(this.DynamicSpace_Paint);
            // 
            // AddEffectContext
            // 
            this.AddEffectContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddEffectToolStripMenuItem});
            this.AddEffectContext.Name = "contextMenuStrip1";
            this.AddEffectContext.Size = new System.Drawing.Size(127, 26);
            // 
            // AddEffectToolStripMenuItem
            // 
            this.AddEffectToolStripMenuItem.Name = "AddEffectToolStripMenuItem";
            this.AddEffectToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.AddEffectToolStripMenuItem.Text = "AddEffect";
            this.AddEffectToolStripMenuItem.Click += new System.EventHandler(this.AddEffectToolstripItem);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveActionDialog
            // 
            this.saveActionDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // AddRequirementContext
            // 
            this.AddRequirementContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRequirementToolStripMenuItem});
            this.AddRequirementContext.Name = "AddRequirementContext";
            this.AddRequirementContext.Size = new System.Drawing.Size(165, 26);
            // 
            // addRequirementToolStripMenuItem
            // 
            this.addRequirementToolStripMenuItem.Name = "addRequirementToolStripMenuItem";
            this.addRequirementToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.addRequirementToolStripMenuItem.Text = "AddRequirement";
            this.addRequirementToolStripMenuItem.Click += new System.EventHandler(this.AddRequirementToolstripItem);
            // 
            // EffectContext
            // 
            this.EffectContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.EffectContext.Name = "EffectContext";
            this.EffectContext.Size = new System.Drawing.Size(108, 70);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteEffectToolstripItem);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // exportActionDialog
            // 
            this.exportActionDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.exportActionDialog_FileOk);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(858, 420);
            this.Controls.Add(this.DynamicSpace);
            this.Controls.Add(this.tv);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "DESEditor";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.AddEffectContext.ResumeLayout(false);
            this.AddRequirementContext.ResumeLayout(false);
            this.EffectContext.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.Panel DynamicSpace;
        private System.Windows.Forms.ContextMenuStrip AddEffectContext;
        private System.Windows.Forms.ToolStripMenuItem AddEffectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newActionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadActionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCurrentActionToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveActionDialog;
        private System.Windows.Forms.ContextMenuStrip AddRequirementContext;
        private System.Windows.Forms.ToolStripMenuItem addRequirementToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip EffectContext;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exportCurrentActionToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog exportActionDialog;
    }
}


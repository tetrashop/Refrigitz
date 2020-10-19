using System;
namespace Refrigtz
{
    partial class FormTXT
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
            this.TextBoxTXT = new System.Windows.Forms.TextBox();
            this.treeViewRefregitzDraw = new System.Windows.Forms.TreeView();
            this.contextMenuStripRefrigitzTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorkertreeView = new System.ComponentModel.BackgroundWorker();
            this.contextMenuStripRefrigitzTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBoxTXT
            // 
            this.TextBoxTXT.Location = new System.Drawing.Point(13, 13);
            this.TextBoxTXT.Multiline = true;
            this.TextBoxTXT.Name = "TextBoxTXT";
            this.TextBoxTXT.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextBoxTXT.Size = new System.Drawing.Size(1307, 563);
            this.TextBoxTXT.TabIndex = 0;
            this.TextBoxTXT.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // treeViewRefregitzDraw
            // 
            this.treeViewRefregitzDraw.Location = new System.Drawing.Point(12, 13);
            this.treeViewRefregitzDraw.Name = "treeViewRefregitzDraw";
            this.treeViewRefregitzDraw.Size = new System.Drawing.Size(1308, 563);
            this.treeViewRefregitzDraw.TabIndex = 85;
            this.treeViewRefregitzDraw.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewRefregitzDraw_AfterSelect);
            this.treeViewRefregitzDraw.BindingContextChanged += new System.EventHandler(this.treeViewRefregitzDraw_BindingContextChanged);
            this.treeViewRefregitzDraw.ContextMenuStripChanged += new System.EventHandler(this.treeViewRefregitzDraw_ContextMenuStripChanged);
            this.treeViewRefregitzDraw.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.treeViewRefregitzDraw_ControlAdded);
            this.treeViewRefregitzDraw.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeViewRefregitzDraw_MouseClick);
            // 
            // contextMenuStripRefrigitzTree
            // 
            this.contextMenuStripRefrigitzTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showTreeToolStripMenuItem});
            this.contextMenuStripRefrigitzTree.Name = "contextMenuStripRefrigitzTree";
            this.contextMenuStripRefrigitzTree.Size = new System.Drawing.Size(126, 26);
            // 
            // showTreeToolStripMenuItem
            // 
            this.showTreeToolStripMenuItem.Name = "showTreeToolStripMenuItem";
            this.showTreeToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.showTreeToolStripMenuItem.Text = "ShowTree";
            // 
            // backgroundWorkertreeView
            // 
            this.backgroundWorkertreeView.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkertreeView_DoWork);
            // 
            // FormTXT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1332, 588);
            this.Controls.Add(this.treeViewRefregitzDraw);
            this.Controls.Add(this.TextBoxTXT);
            this.Name = "FormTXT";
            this.Text = "FormTXT";
            this.Load += new System.EventHandler(this.FormTXT_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormTXT_MouseClick);
            this.contextMenuStripRefrigitzTree.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        [field: NonSerialized]
        private System.Windows.Forms.TextBox TextBoxTXT;
        private System.Windows.Forms.TreeView treeViewRefregitzDraw;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripRefrigitzTree;
        private System.Windows.Forms.ToolStripMenuItem showTreeToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorkertreeView;
    }
}
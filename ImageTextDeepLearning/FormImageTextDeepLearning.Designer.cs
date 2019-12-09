namespace ImageTextDeepLearning
{
    partial class FormImageTextDeepLearning
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImageTextDeepLearning));
            this.textBoxImageTextDeepLearning = new System.Windows.Forms.TextBox();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.openFileDialogImageTextDeepLearning = new System.Windows.Forms.OpenFileDialog();
            this.buttonSplitationConjunction = new System.Windows.Forms.Button();
            this.pictureBoxTest = new System.Windows.Forms.PictureBox();
            this.progressBarCompleted = new System.Windows.Forms.ProgressBar();
            this.buttonTxtDetect = new System.Windows.Forms.Button();
            this.panelImageTextDeepLearning = new System.Windows.Forms.Panel();
            this.PictureBoxImageTextDeepLearning = new System.Windows.Forms.PictureBox();
            this.checkBoxDisablePaintOnAligns = new System.Windows.Forms.CheckBox();
            this.CreateConSha = new System.Windows.Forms.Button();
            this.labelMonitor = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createConjunctionShapesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtDetectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonTxtTemplates = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTest)).BeginInit();
            this.panelImageTextDeepLearning.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxImageTextDeepLearning)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxImageTextDeepLearning
            // 
            this.textBoxImageTextDeepLearning.Location = new System.Drawing.Point(564, -6);
            this.textBoxImageTextDeepLearning.Multiline = true;
            this.textBoxImageTextDeepLearning.Name = "textBoxImageTextDeepLearning";
            this.textBoxImageTextDeepLearning.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxImageTextDeepLearning.Size = new System.Drawing.Size(224, 402);
            this.textBoxImageTextDeepLearning.TabIndex = 1;
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(51, 415);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(75, 23);
            this.buttonOpen.TabIndex = 2;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // openFileDialogImageTextDeepLearning
            // 
            this.openFileDialogImageTextDeepLearning.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogImageTextDeepLearning_FileOk);
            // 
            // buttonSplitationConjunction
            // 
            this.buttonSplitationConjunction.Location = new System.Drawing.Point(132, 415);
            this.buttonSplitationConjunction.Name = "buttonSplitationConjunction";
            this.buttonSplitationConjunction.Size = new System.Drawing.Size(75, 23);
            this.buttonSplitationConjunction.TabIndex = 3;
            this.buttonSplitationConjunction.Text = "Splitation";
            this.buttonSplitationConjunction.UseVisualStyleBackColor = true;
            this.buttonSplitationConjunction.Click += new System.EventHandler(this.buttonSplitationConjunction_Click);
            // 
            // pictureBoxTest
            // 
            this.pictureBoxTest.Location = new System.Drawing.Point(294, 407);
            this.pictureBoxTest.Name = "pictureBoxTest";
            this.pictureBoxTest.Size = new System.Drawing.Size(30, 30);
            this.pictureBoxTest.TabIndex = 4;
            this.pictureBoxTest.TabStop = false;
            // 
            // progressBarCompleted
            // 
            this.progressBarCompleted.Location = new System.Drawing.Point(530, 415);
            this.progressBarCompleted.Name = "progressBarCompleted";
            this.progressBarCompleted.Size = new System.Drawing.Size(258, 23);
            this.progressBarCompleted.TabIndex = 5;
            this.progressBarCompleted.Click += new System.EventHandler(this.progressBarCompleted_Click);
            // 
            // buttonTxtDetect
            // 
            this.buttonTxtDetect.Location = new System.Drawing.Point(331, 415);
            this.buttonTxtDetect.Name = "buttonTxtDetect";
            this.buttonTxtDetect.Size = new System.Drawing.Size(75, 23);
            this.buttonTxtDetect.TabIndex = 6;
            this.buttonTxtDetect.Text = "TxtDetect";
            this.buttonTxtDetect.UseVisualStyleBackColor = true;
            this.buttonTxtDetect.Click += new System.EventHandler(this.buttonTxtDetect_Click);
            // 
            // panelImageTextDeepLearning
            // 
            this.panelImageTextDeepLearning.AutoScroll = true;
            this.panelImageTextDeepLearning.Controls.Add(this.PictureBoxImageTextDeepLearning);
            this.panelImageTextDeepLearning.Location = new System.Drawing.Point(24, 27);
            this.panelImageTextDeepLearning.Name = "panelImageTextDeepLearning";
            this.panelImageTextDeepLearning.Size = new System.Drawing.Size(534, 369);
            this.panelImageTextDeepLearning.TabIndex = 7;
            // 
            // PictureBoxImageTextDeepLearning
            // 
            this.PictureBoxImageTextDeepLearning.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PictureBoxImageTextDeepLearning.Image = ((System.Drawing.Image)(resources.GetObject("PictureBoxImageTextDeepLearning.Image")));
            this.PictureBoxImageTextDeepLearning.Location = new System.Drawing.Point(3, 3);
            this.PictureBoxImageTextDeepLearning.Name = "PictureBoxImageTextDeepLearning";
            this.PictureBoxImageTextDeepLearning.Size = new System.Drawing.Size(1013, 619);
            this.PictureBoxImageTextDeepLearning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PictureBoxImageTextDeepLearning.TabIndex = 1;
            this.PictureBoxImageTextDeepLearning.TabStop = false;
            this.PictureBoxImageTextDeepLearning.Click += new System.EventHandler(this.PictureBoxImageTextDeepLearning_Click);
            this.PictureBoxImageTextDeepLearning.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBoxImageTextDeepLearning_Paint);
            // 
            // checkBoxDisablePaintOnAligns
            // 
            this.checkBoxDisablePaintOnAligns.AutoSize = true;
            this.checkBoxDisablePaintOnAligns.Location = new System.Drawing.Point(356, 392);
            this.checkBoxDisablePaintOnAligns.Name = "checkBoxDisablePaintOnAligns";
            this.checkBoxDisablePaintOnAligns.Size = new System.Drawing.Size(136, 17);
            this.checkBoxDisablePaintOnAligns.TabIndex = 2;
            this.checkBoxDisablePaintOnAligns.Text = "Disable Paint On Aligns";
            this.checkBoxDisablePaintOnAligns.UseVisualStyleBackColor = true;
            this.checkBoxDisablePaintOnAligns.CheckedChanged += new System.EventHandler(this.checkBoxDisablePaintOnAligns_CheckedChanged);
            // 
            // CreateConSha
            // 
            this.CreateConSha.Location = new System.Drawing.Point(213, 415);
            this.CreateConSha.Name = "CreateConSha";
            this.CreateConSha.Size = new System.Drawing.Size(75, 23);
            this.CreateConSha.TabIndex = 8;
            this.CreateConSha.Text = "CreateConSha";
            this.CreateConSha.UseVisualStyleBackColor = true;
            this.CreateConSha.Click += new System.EventHandler(this.CreateConSha_Click);
            // 
            // labelMonitor
            // 
            this.labelMonitor.AutoSize = true;
            this.labelMonitor.Location = new System.Drawing.Point(413, 415);
            this.labelMonitor.Name = "labelMonitor";
            this.labelMonitor.Size = new System.Drawing.Size(0, 13);
            this.labelMonitor.TabIndex = 9;
            this.labelMonitor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStripImageTextDeepLearning";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.splitationToolStripMenuItem,
            this.createConjunctionShapesToolStripMenuItem,
            this.txtDetectionToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // splitationToolStripMenuItem
            // 
            this.splitationToolStripMenuItem.Name = "splitationToolStripMenuItem";
            this.splitationToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.splitationToolStripMenuItem.Text = "Splitation";
            this.splitationToolStripMenuItem.Click += new System.EventHandler(this.splitationToolStripMenuItem_Click);
            // 
            // createConjunctionShapesToolStripMenuItem
            // 
            this.createConjunctionShapesToolStripMenuItem.Name = "createConjunctionShapesToolStripMenuItem";
            this.createConjunctionShapesToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.createConjunctionShapesToolStripMenuItem.Text = "Create Conjunction Shapes";
            this.createConjunctionShapesToolStripMenuItem.Click += new System.EventHandler(this.createConjunctionShapesToolStripMenuItem_Click);
            // 
            // txtDetectionToolStripMenuItem
            // 
            this.txtDetectionToolStripMenuItem.Name = "txtDetectionToolStripMenuItem";
            this.txtDetectionToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.txtDetectionToolStripMenuItem.Text = "TxtDetection";
            this.txtDetectionToolStripMenuItem.Click += new System.EventHandler(this.txtDetectionToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // buttonTxtTemplates
            // 
            this.buttonTxtTemplates.Location = new System.Drawing.Point(420, 415);
            this.buttonTxtTemplates.Name = "buttonTxtTemplates";
            this.buttonTxtTemplates.Size = new System.Drawing.Size(82, 23);
            this.buttonTxtTemplates.TabIndex = 11;
            this.buttonTxtTemplates.Text = "TxtTemplatets";
            this.buttonTxtTemplates.UseVisualStyleBackColor = true;
            this.buttonTxtTemplates.Click += new System.EventHandler(this.buttonTxtTemplates_Click);
            // 
            // FormImageTextDeepLearning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonTxtTemplates);
            this.Controls.Add(this.labelMonitor);
            this.Controls.Add(this.CreateConSha);
            this.Controls.Add(this.checkBoxDisablePaintOnAligns);
            this.Controls.Add(this.panelImageTextDeepLearning);
            this.Controls.Add(this.buttonTxtDetect);
            this.Controls.Add(this.progressBarCompleted);
            this.Controls.Add(this.pictureBoxTest);
            this.Controls.Add(this.buttonSplitationConjunction);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.textBoxImageTextDeepLearning);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormImageTextDeepLearning";
            this.Text = "FormImageTextDeepLearning";
            this.Load += new System.EventHandler(this.FormImageTextDeepLearning_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTest)).EndInit();
            this.panelImageTextDeepLearning.ResumeLayout(false);
            this.panelImageTextDeepLearning.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxImageTextDeepLearning)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxImageTextDeepLearning;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialogImageTextDeepLearning;
        private System.Windows.Forms.Button buttonSplitationConjunction;
        private System.Windows.Forms.PictureBox pictureBoxTest;
        private System.Windows.Forms.ProgressBar progressBarCompleted;
        private System.Windows.Forms.Button buttonTxtDetect;
        private System.Windows.Forms.Panel panelImageTextDeepLearning;
        private System.Windows.Forms.PictureBox PictureBoxImageTextDeepLearning;
        private System.Windows.Forms.CheckBox checkBoxDisablePaintOnAligns;
        private System.Windows.Forms.Button CreateConSha;
        public System.Windows.Forms.Label labelMonitor;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem splitationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createConjunctionShapesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem txtDetectionToolStripMenuItem;
        private System.Windows.Forms.Button buttonTxtTemplates;
    }
}


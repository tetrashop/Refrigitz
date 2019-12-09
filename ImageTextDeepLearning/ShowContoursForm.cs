namespace ContourAnalysisDemo
{
    using ContourAnalysisNS;
    using Emgu.CV;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class ShowContoursForm : Form
    {
        private Templates templates;
        private Templates samples;
        public Template selectedTemplate;
        private Bitmap bmp;
        private IContainer components = null;
        private DataGridView dgvContours;
        private TextBox tbTemplateName;
        private Label label1;
        private Button button1;
        private DataGridViewTextBoxColumn Column;
        private Label label2;

        public ShowContoursForm(Templates templates, Templates samples, IImage image)
        {
            bool flag = !ReferenceEquals(image, null);
            if (flag)
            {
                this.InitializeComponent();
                this.templates = templates;
                this.samples = samples;
                this.samples = new Templates();
                using (List<Template>.Enumerator enumerator = samples.GetEnumerator())
                {
                    while (true)
                    {
                        flag = enumerator.MoveNext();
                        if (!flag)
                        {
                            break;
                        }
                        Template current = enumerator.Current;
                        this.samples.Add(current);
                    }
                }
                this.dgvContours.RowCount = samples.Count;
                base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
                string fileName = Path.GetTempPath() + @"\temp.bmp";
                image.Save(fileName);
                this.bmp = (Bitmap) Image.FromFile(fileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.tbTemplateName.Text == "<template name>")
            {
                MessageBox.Show("Enter template name");
            }
            else
            {
                try
                {
                    int rowIndex = this.dgvContours.SelectedCells[0].RowIndex;
                    this.samples[rowIndex].name = this.tbTemplateName.Text;
                    this.templates.Add(this.samples[rowIndex]);
                }
                catch (Exception exception1)
                {
                    MessageBox.Show(exception1.Message);
                }
            }
        }

        private void dgvContours_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
            e.Handled = true;
            if (e.RowIndex >= 0)
            {
                Template template = this.samples[e.RowIndex];
                if (e.ColumnIndex == -1)
                {
                    e.Graphics.DrawString(e.RowIndex.ToString(), this.Font, Brushes.Black, (PointF) e.CellBounds.Location);
                }
                else
                {
                    if (e.ColumnIndex == 0)
                    {
                        Rectangle rectangle = new Rectangle(e.CellBounds.X, e.CellBounds.Y, (e.CellBounds.Width - 0x18) / 2, e.CellBounds.Height);
                        rectangle.Inflate(-20, -20);
                        Rectangle sourceBoundingRect = template.contour.SourceBoundingRect;
                        float num3 = Math.Min((float) ((1f * rectangle.Width) / ((float) sourceBoundingRect.Width)), (float) ((1f * rectangle.Height) / ((float) sourceBoundingRect.Height)));
                        e.Graphics.DrawImage(this.bmp, new Rectangle(rectangle.X, rectangle.Y, (int) (sourceBoundingRect.Width * num3), (int) (sourceBoundingRect.Height * num3)), sourceBoundingRect, GraphicsUnit.Pixel);
                    }
                    if (e.ColumnIndex == 0)
                    {
                        template.Draw(e.Graphics, e.CellBounds);
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!(!disposing || ReferenceEquals(this.components, null)))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvContours = new System.Windows.Forms.DataGridView();
            this.Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbTemplateName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContours)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvContours
            // 
            this.dgvContours.AllowUserToAddRows = false;
            this.dgvContours.AllowUserToDeleteRows = false;
            this.dgvContours.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvContours.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvContours.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvContours.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvContours.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column});
            this.dgvContours.Location = new System.Drawing.Point(12, 12);
            this.dgvContours.Name = "dgvContours";
            this.dgvContours.ReadOnly = true;
            this.dgvContours.RowTemplate.Height = 200;
            this.dgvContours.Size = new System.Drawing.Size(500, 409);
            this.dgvContours.TabIndex = 0;
            this.dgvContours.VirtualMode = true;
            this.dgvContours.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvContours_CellContentClick);
            this.dgvContours.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvContours_CellPainting);
            // 
            // Column
            // 
            this.Column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column.HeaderText = "Contour";
            this.Column.Name = "Column";
            this.Column.ReadOnly = true;
            // 
            // tbTemplateName
            // 
            this.tbTemplateName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbTemplateName.ForeColor = System.Drawing.Color.Gray;
            this.tbTemplateName.Location = new System.Drawing.Point(205, 431);
            this.tbTemplateName.Name = "tbTemplateName";
            this.tbTemplateName.Size = new System.Drawing.Size(114, 20);
            this.tbTemplateName.TabIndex = 1;
            this.tbTemplateName.Text = "<template name>";
            this.tbTemplateName.Enter += new System.EventHandler(this.tbTemplateName_Enter);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 434);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(18, 429);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(181, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Add selected contour as Template:";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(325, 431);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 29);
            this.label2.TabIndex = 4;
            this.label2.Text = "enter template name or image file name (*.png, *.jpg)";
            // 
            // ShowContoursForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 459);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbTemplateName);
            this.Controls.Add(this.dgvContours);
            this.Name = "ShowContoursForm";
            this.Text = "Create templates";
            ((System.ComponentModel.ISupportInitialize)(this.dgvContours)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void tbTemplateName_Enter(object sender, EventArgs e)
        {
            this.tbTemplateName.ForeColor = Color.Black;
            if (this.tbTemplateName.Text == "<template name>")
            {
                this.tbTemplateName.Text = "";
            }
        }

        private void dgvContours_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}


namespace ContourAnalysisDemo
{
    using ImageTextDeepLearning;
    using ContourAnalysisNS;
    using Emgu.CV;
    using Emgu.CV.CvEnum;
    using Emgu.CV.UI;
    using Emgu.CV.Structure;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Windows.Forms;
    [Serializable]
    public class MainForm : Form
    {
        public bool DisablePaintOnAligns = false;
        private Capture _capture;
        public Image<Bgr, byte> frame;
        public ImageProcessor processor;
        private Dictionary<string, Image> AugmentedRealityImages = new Dictionary<string, Image>();
        private bool captureFromCam = true;
        private int frameCount = 0;
        private int oldFrameCount = 0;
        public bool showAngle;
        private int camWidth = 640;
        private int camHeight = 480;
        private string templateFile;
        private IContainer components = null;
        private ImageBox ibMain;
        private Panel pnSettings;
        private StatusStrip ssMain;
        private Splitter splitter1;
        public  ToolStripStatusLabel lbFPS;
        private ToolStripStatusLabel lbContoursCount;
        private Timer tmUpdateState;
        private ToolStripStatusLabel lbRecognized;
        private CheckBox cbShowAngle;
        private CheckBox cbAutoContrast;
        private Button btLoadImage;
        private Label label1;
        private ComboBox cbCamResolution;
        private CheckBox cbCaptureFromCam;
        private CheckBox cbAllowAngleMore45;
        private Label label3;
        private NumericUpDown nudMinContourArea;
        private Label label2;
        private NumericUpDown nudMinContourLength;
        private Label label4;
        private NumericUpDown nudMinICF;
        private Label label5;
        private NumericUpDown nudMinACF;
        private Label label6;
        private NumericUpDown nudMaxACFdesc;
        private GroupBox groupBox3;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private CheckBox cbBlur;
        private CheckBox cbNoiseFilter;
        private NumericUpDown nudMinDefinition;
        private NumericUpDown nudAdaptiveThBlockSize;
        private Label label7;
        private ToolStrip toolStrip1;
        private ToolStripButton btCreateTemplate;
        private ToolStripButton btNewTemplates;
        private ToolStripButton btOpenTemplates;
        private ToolStripButton btSaveTemplates;
        private ToolStripSeparator toolStripSeparator;
        private CheckBox cbShowContours;
        private ToolStripButton btTemplateEditor;
        private ToolStripButton btAutoGenerate;
        private PanAndZoomPictureBox panAndZoomPictureBox1;
        private ToolStripLabel toolStripLabel1;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripComboBox toolStripComboBox1;
        private ToolStripTextBox toolStripTextBox1;
        private ToolStripProgressBar toolStripProgressBar1;
        private CheckBox cbShowBinarized;

        public MainForm()
        {
            this.InitializeComponent();
            this.processor = new ImageProcessor();
            this.templateFile = AppDomain.CurrentDomain.BaseDirectory + @"\Tahoma.bin";
            this.LoadTemplates(this.templateFile);
            this.StartCapture();
            this.ApplySettings();
            Application.Idle += new EventHandler(this.Application_Idle);
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            this.ProcessFrame();
        }

        private void ApplyCamSettings()
        {
            try
            {
                this._capture.SetCaptureProperty(CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, (double) this.camWidth);
                this._capture.SetCaptureProperty(CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, (double) this.camHeight);
                this.cbCamResolution.Text = this.camWidth + "x" + this.camHeight;
            }
            catch (NullReferenceException exception1)
            {
                MessageBox.Show(exception1.Message);
            }
        }

        private void ApplySettings()
        {
            try
            {
                this.processor.equalizeHist = this.cbAutoContrast.Checked;
                this.showAngle = this.cbShowAngle.Checked;
                this.captureFromCam = this.cbCaptureFromCam.Checked;
                this.btLoadImage.Enabled = !this.captureFromCam;
                this.cbCamResolution.Enabled = this.captureFromCam;
                this.processor.finder.maxRotateAngle = this.cbAllowAngleMore45.Checked ? 3.1415926535897931 : 0.78539816339744828;
                this.processor.minContourArea = (int) this.nudMinContourArea.Value;
                this.processor.minContourLength = (int) this.nudMinContourLength.Value;
                this.processor.finder.maxACFDescriptorDeviation = (int) this.nudMaxACFdesc.Value;
                this.processor.finder.minACF = (double) this.nudMinACF.Value;
                this.processor.finder.minICF = (double) this.nudMinICF.Value;
                this.processor.blur = this.cbBlur.Checked;
                this.processor.noiseFilter = this.cbNoiseFilter.Checked;
                this.processor.cannyThreshold = (int) this.nudMinDefinition.Value;
                this.nudMinDefinition.Enabled = this.processor.noiseFilter;
                this.processor.adaptiveThresholdBlockSize = (int) this.nudAdaptiveThBlockSize.Value;
                string[] strArray = this.cbCamResolution.Text.ToLower().Split(new char[] { 'x' });
                if (strArray.Length == 2)
                {
                    int num = int.Parse(strArray[0]);
                    int num2 = int.Parse(strArray[1]);
                    if ((this.camHeight != num2) || (this.camWidth != num))
                    {
                        this.camWidth = num;
                        this.camHeight = num2;
                        this.ApplyCamSettings();
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message);
            }
        }

        private void btAutoGenerate_Click(object sender, EventArgs e)
        {
            new AutoGenerateForm(this.processor).ShowDialog();
        }

        private void btCreateTemplate_Click(object sender, EventArgs e)
        {
            if (!ReferenceEquals(this.frame, null))
            {
                new ShowContoursForm(this.processor.templates, this.processor.samples, this.frame).ShowDialog();
            }
        }

        private void btLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "Image|*.bmp;*.png;*.jpg;*.jpeg"
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    this.frame = new Image<Bgr, byte>((Bitmap) Image.FromFile(dialog.FileName));
                }
                catch (Exception exception1)
                {
                    MessageBox.Show(exception1.Message);
                }
            }
        }

        private void btNewTemplates_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to create new template database?", "Create new template database", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.processor.templates.Clear();
            }
        }

        private void btOpenTemplates_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "Templates(*.bin)|*.bin"
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                this.templateFile = dialog.FileName;
                this.LoadTemplates(this.templateFile);
            }
        }

        private void btSaveTemplates_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog {
                Filter = "Templates(*.bin)|*.bin"
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                this.templateFile = dialog.FileName;
                this.SaveTemplates(this.templateFile);
            }
        }

        private void btTemplateEditor_Click(object sender, EventArgs e)
        {
            new TemplateEditor(this.processor.templates).Show();
        }

        private void cbAutoContrast_CheckedChanged(object sender, EventArgs e)
        {
            this.ApplySettings();
        }

        protected override void Dispose(bool disposing)
        {
            if (!(!disposing || ReferenceEquals(this.components, null)))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void DrawAugmentedReality(FoundTemplateDesc found, Graphics gr)
        {
            string key = Path.GetDirectoryName(this.templateFile) + @"\" + found.template.name;
            if (!this.AugmentedRealityImages.ContainsKey(key))
            {
                if (File.Exists(key))
                {
                    this.AugmentedRealityImages[key] = Image.FromFile(key);
                }
                else
                {
                    return;
                }
            }
            Image image = this.AugmentedRealityImages[key];
            Point point = found.sample.contour.SourceBoundingRect.Center();
            GraphicsState gstate = gr.Save();
            gr.TranslateTransform((float) point.X, (float) point.Y);
            gr.RotateTransform((float) ((180.0 * found.angle) / 3.1415926535897931));
            gr.ScaleTransform((float) found.scale, (float) found.scale);
            gr.DrawImage(image, new Point(-image.Width / 2, -image.Height / 2));
            gr.Restore(gstate);
        }

        private void ibMain_Paint(object sender, PaintEventArgs e)
        {
            if (!DisablePaintOnAligns)
            {
                Font font;
                Brush brush;
                Brush brush2;
                Pen pen;
                bool flag2;
                if (!ReferenceEquals(this.frame, null))
                {
                    font = new Font(this.Font.FontFamily, 24f);
                    e.Graphics.DrawString(this.lbFPS.Text, new Font(this.Font.FontFamily, 16f), Brushes.Yellow, new PointF(1f, 1f));
                    brush = new SolidBrush(Color.FromArgb(0xff, 0, 0, 0));
                    brush2 = new SolidBrush(Color.FromArgb(0xff, 0xff, 0xff, 0xff));
                    pen = new Pen(Color.FromArgb(150, 0, 0xff, 0));
                    flag2 = !this.cbShowContours.Checked;
                    if (!flag2)
                    {
                        using (List<Contour<Point>>.Enumerator enumerator = this.processor.contours.GetEnumerator())
                        {
                            while (true)
                            {
                                flag2 = enumerator.MoveNext();
                                if (!flag2)
                                {
                                    break;
                                }
                                Contour<Point> current = enumerator.Current;
                                if (current.Total > 1)
                                {
                                    e.Graphics.DrawLines(Pens.Red, current.ToArray());
                                }

                            }

                        }
                    }
                }
                else
                {
                    return;
                }
                lock (this.processor.foundTemplates)
                {
                    using (List<FoundTemplateDesc>.Enumerator enumerator2 = this.processor.foundTemplates.GetEnumerator())
                    {
                        while (true)
                        {
                            flag2 = enumerator2.MoveNext();
                            if (!flag2)
                            {
                                break;
                            }
                            FoundTemplateDesc current = enumerator2.Current;
                            if (current.template.name.EndsWith(".png") || current.template.name.EndsWith(".jpg"))
                            {
                                this.DrawAugmentedReality(current, e.Graphics);
                                continue;
                            }
                            Rectangle sourceBoundingRect = current.sample.contour.SourceBoundingRect;
                            Point point = new Point((sourceBoundingRect.Left + sourceBoundingRect.Right) / 2, sourceBoundingRect.Top);
                            string name = current.template.name;
                            if (this.showAngle)
                            {
                                name = name + $"angle={((180.0 * current.angle) / 3.1415926535897931):000}°scale={current.scale:0.0}";

                            }

                            e.Graphics.DrawRectangle(pen, sourceBoundingRect);
                            e.Graphics.DrawString(name, font, brush, new PointF((float)((point.X + 1) - (font.Height / 3)), (float)((point.Y + 1) - font.Height)));
                            e.Graphics.DrawString(name, font, brush2, new PointF((float)(point.X - (font.Height / 3)), (float)(point.Y - font.Height)));
                        }
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ibMain = new Emgu.CV.UI.ImageBox();
            this.pnSettings = new System.Windows.Forms.Panel();
            this.panAndZoomPictureBox1 = new Emgu.CV.UI.PanAndZoomPictureBox();
            this.cbShowBinarized = new System.Windows.Forms.CheckBox();
            this.cbShowContours = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.nudMaxACFdesc = new System.Windows.Forms.NumericUpDown();
            this.nudMinACF = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.cbAllowAngleMore45 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.nudMinICF = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nudMinDefinition = new System.Windows.Forms.NumericUpDown();
            this.cbNoiseFilter = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudMinContourLength = new System.Windows.Forms.NumericUpDown();
            this.nudMinContourArea = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudAdaptiveThBlockSize = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.cbBlur = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbCaptureFromCam = new System.Windows.Forms.CheckBox();
            this.cbCamResolution = new System.Windows.Forms.ComboBox();
            this.btLoadImage = new System.Windows.Forms.Button();
            this.cbAutoContrast = new System.Windows.Forms.CheckBox();
            this.cbShowAngle = new System.Windows.Forms.CheckBox();
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.lbFPS = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbContoursCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbRecognized = new System.Windows.Forms.ToolStripStatusLabel();
            this.btNewTemplates = new System.Windows.Forms.ToolStripButton();
            this.btOpenTemplates = new System.Windows.Forms.ToolStripButton();
            this.btSaveTemplates = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btCreateTemplate = new System.Windows.Forms.ToolStripButton();
            this.btAutoGenerate = new System.Windows.Forms.ToolStripButton();
            this.btTemplateEditor = new System.Windows.Forms.ToolStripButton();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.tmUpdateState = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.ibMain)).BeginInit();
            this.pnSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panAndZoomPictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxACFdesc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinACF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinICF)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinDefinition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinContourLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinContourArea)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdaptiveThBlockSize)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ibMain
            // 
            this.ibMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ibMain.Location = new System.Drawing.Point(0, 25);
            this.ibMain.Name = "ibMain";
            this.ibMain.Size = new System.Drawing.Size(499, 405);
            this.ibMain.TabIndex = 2;
            this.ibMain.TabStop = false;
            this.ibMain.Paint += new System.Windows.Forms.PaintEventHandler(this.ibMain_Paint);
            // 
            // pnSettings
            // 
            this.pnSettings.AutoScroll = true;
            this.pnSettings.Controls.Add(this.panAndZoomPictureBox1);
            this.pnSettings.Controls.Add(this.cbShowBinarized);
            this.pnSettings.Controls.Add(this.cbShowContours);
            this.pnSettings.Controls.Add(this.groupBox3);
            this.pnSettings.Controls.Add(this.groupBox2);
            this.pnSettings.Controls.Add(this.groupBox1);
            this.pnSettings.Controls.Add(this.cbShowAngle);
            this.pnSettings.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnSettings.Location = new System.Drawing.Point(504, 25);
            this.pnSettings.Name = "pnSettings";
            this.pnSettings.Size = new System.Drawing.Size(209, 405);
            this.pnSettings.TabIndex = 3;
            // 
            // panAndZoomPictureBox1
            // 
            this.panAndZoomPictureBox1.Location = new System.Drawing.Point(145, 190);
            this.panAndZoomPictureBox1.Name = "panAndZoomPictureBox1";
            this.panAndZoomPictureBox1.Size = new System.Drawing.Size(8, 8);
            this.panAndZoomPictureBox1.TabIndex = 22;
            this.panAndZoomPictureBox1.TabStop = false;
            // 
            // cbShowBinarized
            // 
            this.cbShowBinarized.AutoSize = true;
            this.cbShowBinarized.Location = new System.Drawing.Point(6, 178);
            this.cbShowBinarized.Name = "cbShowBinarized";
            this.cbShowBinarized.Size = new System.Drawing.Size(101, 17);
            this.cbShowBinarized.TabIndex = 21;
            this.cbShowBinarized.Text = "Show binarized ";
            this.cbShowBinarized.UseVisualStyleBackColor = true;
            // 
            // cbShowContours
            // 
            this.cbShowContours.AutoSize = true;
            this.cbShowContours.Location = new System.Drawing.Point(95, 155);
            this.cbShowContours.Name = "cbShowContours";
            this.cbShowContours.Size = new System.Drawing.Size(97, 17);
            this.cbShowContours.TabIndex = 20;
            this.cbShowContours.Text = "Show contours";
            this.cbShowContours.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.nudMaxACFdesc);
            this.groupBox3.Controls.Add(this.nudMinACF);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.cbAllowAngleMore45);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.nudMinICF);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(6, 317);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(182, 128);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Recognition parameters";
            // 
            // nudMaxACFdesc
            // 
            this.nudMaxACFdesc.Location = new System.Drawing.Point(6, 36);
            this.nudMaxACFdesc.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudMaxACFdesc.Name = "nudMaxACFdesc";
            this.nudMaxACFdesc.Size = new System.Drawing.Size(78, 20);
            this.nudMaxACFdesc.TabIndex = 15;
            this.nudMaxACFdesc.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudMaxACFdesc.ValueChanged += new System.EventHandler(this.cbAutoContrast_CheckedChanged);
            // 
            // nudMinACF
            // 
            this.nudMinACF.DecimalPlaces = 2;
            this.nudMinACF.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudMinACF.Location = new System.Drawing.Point(6, 75);
            this.nudMinACF.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMinACF.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudMinACF.Name = "nudMinACF";
            this.nudMinACF.Size = new System.Drawing.Size(78, 20);
            this.nudMinACF.TabIndex = 11;
            this.nudMinACF.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudMinACF.ValueChanged += new System.EventHandler(this.cbAutoContrast_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Min. ACF";
            // 
            // cbAllowAngleMore45
            // 
            this.cbAllowAngleMore45.AutoSize = true;
            this.cbAllowAngleMore45.Location = new System.Drawing.Point(7, 105);
            this.cbAllowAngleMore45.Name = "cbAllowAngleMore45";
            this.cbAllowAngleMore45.Size = new System.Drawing.Size(109, 17);
            this.cbAllowAngleMore45.TabIndex = 6;
            this.cbAllowAngleMore45.Text = "Allow angles > 45";
            this.cbAllowAngleMore45.UseVisualStyleBackColor = true;
            this.cbAllowAngleMore45.CheckedChanged += new System.EventHandler(this.cbAutoContrast_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Max. ACF descriptor deviation";
            // 
            // nudMinICF
            // 
            this.nudMinICF.DecimalPlaces = 2;
            this.nudMinICF.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudMinICF.Location = new System.Drawing.Point(101, 75);
            this.nudMinICF.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMinICF.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudMinICF.Name = "nudMinICF";
            this.nudMinICF.Size = new System.Drawing.Size(78, 20);
            this.nudMinICF.TabIndex = 13;
            this.nudMinICF.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudMinICF.ValueChanged += new System.EventHandler(this.cbAutoContrast_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(100, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Min. ICF";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nudMinDefinition);
            this.groupBox2.Controls.Add(this.cbNoiseFilter);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.nudMinContourLength);
            this.groupBox2.Controls.Add(this.nudMinContourArea);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(3, 220);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(182, 91);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Contour filtration";
            // 
            // nudMinDefinition
            // 
            this.nudMinDefinition.Enabled = false;
            this.nudMinDefinition.Location = new System.Drawing.Point(100, 63);
            this.nudMinDefinition.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudMinDefinition.Name = "nudMinDefinition";
            this.nudMinDefinition.Size = new System.Drawing.Size(78, 20);
            this.nudMinDefinition.TabIndex = 12;
            this.nudMinDefinition.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudMinDefinition.ValueChanged += new System.EventHandler(this.cbAutoContrast_CheckedChanged);
            // 
            // cbNoiseFilter
            // 
            this.cbNoiseFilter.AutoSize = true;
            this.cbNoiseFilter.Location = new System.Drawing.Point(6, 64);
            this.cbNoiseFilter.Name = "cbNoiseFilter";
            this.cbNoiseFilter.Size = new System.Drawing.Size(75, 17);
            this.cbNoiseFilter.TabIndex = 11;
            this.cbNoiseFilter.Text = "Noise filter";
            this.cbNoiseFilter.UseVisualStyleBackColor = true;
            this.cbNoiseFilter.CheckedChanged += new System.EventHandler(this.cbAutoContrast_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Min. contour length";
            // 
            // nudMinContourLength
            // 
            this.nudMinContourLength.Location = new System.Drawing.Point(5, 34);
            this.nudMinContourLength.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.nudMinContourLength.Name = "nudMinContourLength";
            this.nudMinContourLength.Size = new System.Drawing.Size(78, 20);
            this.nudMinContourLength.TabIndex = 7;
            this.nudMinContourLength.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nudMinContourLength.ValueChanged += new System.EventHandler(this.cbAutoContrast_CheckedChanged);
            // 
            // nudMinContourArea
            // 
            this.nudMinContourArea.Location = new System.Drawing.Point(100, 34);
            this.nudMinContourArea.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.nudMinContourArea.Name = "nudMinContourArea";
            this.nudMinContourArea.Size = new System.Drawing.Size(78, 20);
            this.nudMinContourArea.TabIndex = 9;
            this.nudMinContourArea.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudMinContourArea.ValueChanged += new System.EventHandler(this.cbAutoContrast_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(97, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Min. contour area";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudAdaptiveThBlockSize);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbBlur);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbCaptureFromCam);
            this.groupBox1.Controls.Add(this.cbCamResolution);
            this.groupBox1.Controls.Add(this.btLoadImage);
            this.groupBox1.Controls.Add(this.cbAutoContrast);
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 146);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source";
            // 
            // nudAdaptiveThBlockSize
            // 
            this.nudAdaptiveThBlockSize.Location = new System.Drawing.Point(11, 122);
            this.nudAdaptiveThBlockSize.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.nudAdaptiveThBlockSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAdaptiveThBlockSize.Name = "nudAdaptiveThBlockSize";
            this.nudAdaptiveThBlockSize.Size = new System.Drawing.Size(78, 20);
            this.nudAdaptiveThBlockSize.TabIndex = 11;
            this.nudAdaptiveThBlockSize.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudAdaptiveThBlockSize.ValueChanged += new System.EventHandler(this.cbAutoContrast_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 106);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(145, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Adaptive threshold block size";
            // 
            // cbBlur
            // 
            this.cbBlur.AutoSize = true;
            this.cbBlur.Checked = true;
            this.cbBlur.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBlur.Location = new System.Drawing.Point(106, 84);
            this.cbBlur.Name = "cbBlur";
            this.cbBlur.Size = new System.Drawing.Size(44, 17);
            this.cbBlur.TabIndex = 6;
            this.cbBlur.Text = "Blur";
            this.cbBlur.UseVisualStyleBackColor = true;
            this.cbBlur.CheckedChanged += new System.EventHandler(this.cbAutoContrast_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Cam resolution";
            // 
            // cbCaptureFromCam
            // 
            this.cbCaptureFromCam.AutoSize = true;
            this.cbCaptureFromCam.Checked = true;
            this.cbCaptureFromCam.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCaptureFromCam.Location = new System.Drawing.Point(11, 16);
            this.cbCaptureFromCam.Name = "cbCaptureFromCam";
            this.cbCaptureFromCam.Size = new System.Drawing.Size(124, 17);
            this.cbCaptureFromCam.TabIndex = 2;
            this.cbCaptureFromCam.Text = "Capture from camera";
            this.cbCaptureFromCam.UseVisualStyleBackColor = true;
            this.cbCaptureFromCam.CheckedChanged += new System.EventHandler(this.cbAutoContrast_CheckedChanged);
            // 
            // cbCamResolution
            // 
            this.cbCamResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCamResolution.FormattingEnabled = true;
            this.cbCamResolution.Items.AddRange(new object[] {
            "800x600",
            "640x480",
            "320x240"});
            this.cbCamResolution.Location = new System.Drawing.Point(11, 51);
            this.cbCamResolution.Name = "cbCamResolution";
            this.cbCamResolution.Size = new System.Drawing.Size(89, 21);
            this.cbCamResolution.TabIndex = 3;
            this.cbCamResolution.TextChanged += new System.EventHandler(this.cbAutoContrast_CheckedChanged);
            // 
            // btLoadImage
            // 
            this.btLoadImage.Enabled = false;
            this.btLoadImage.Location = new System.Drawing.Point(105, 38);
            this.btLoadImage.Name = "btLoadImage";
            this.btLoadImage.Size = new System.Drawing.Size(68, 35);
            this.btLoadImage.TabIndex = 5;
            this.btLoadImage.Text = "Recognize image ...";
            this.btLoadImage.UseVisualStyleBackColor = true;
            this.btLoadImage.Click += new System.EventHandler(this.btLoadImage_Click);
            // 
            // cbAutoContrast
            // 
            this.cbAutoContrast.AutoSize = true;
            this.cbAutoContrast.Location = new System.Drawing.Point(11, 84);
            this.cbAutoContrast.Name = "cbAutoContrast";
            this.cbAutoContrast.Size = new System.Drawing.Size(89, 17);
            this.cbAutoContrast.TabIndex = 1;
            this.cbAutoContrast.Text = "Auto contrast";
            this.cbAutoContrast.UseVisualStyleBackColor = true;
            this.cbAutoContrast.CheckedChanged += new System.EventHandler(this.cbAutoContrast_CheckedChanged);
            // 
            // cbShowAngle
            // 
            this.cbShowAngle.AutoSize = true;
            this.cbShowAngle.Location = new System.Drawing.Point(6, 155);
            this.cbShowAngle.Name = "cbShowAngle";
            this.cbShowAngle.Size = new System.Drawing.Size(87, 17);
            this.cbShowAngle.TabIndex = 0;
            this.cbShowAngle.Text = "Show angles";
            this.cbShowAngle.UseVisualStyleBackColor = true;
            this.cbShowAngle.CheckedChanged += new System.EventHandler(this.cbAutoContrast_CheckedChanged);
            // 
            // ssMain
            // 
            this.ssMain.Location = new System.Drawing.Point(0, 430);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(713, 22);
            this.ssMain.TabIndex = 4;
            this.ssMain.Text = "statusStrip1";
            this.ssMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ssMain_ItemClicked);
            // 
            // lbFPS
            // 
            this.lbFPS.AutoSize = false;
            this.lbFPS.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lbFPS.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lbFPS.Name = "lbFPS";
            this.lbFPS.Size = new System.Drawing.Size(52, 17);
            this.lbFPS.Text = "0 fps";
            // 
            // lbContoursCount
            // 
            this.lbContoursCount.AutoSize = false;
            this.lbContoursCount.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lbContoursCount.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lbContoursCount.Name = "lbContoursCount";
            this.lbContoursCount.Size = new System.Drawing.Size(120, 17);
            this.lbContoursCount.Text = "Total Contours: ";
            this.lbContoursCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbContoursCount.Click += new System.EventHandler(this.lbContoursCount_Click);
            // 
            // lbRecognized
            // 
            this.lbRecognized.AutoSize = false;
            this.lbRecognized.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lbRecognized.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lbRecognized.Name = "lbRecognized";
            this.lbRecognized.Size = new System.Drawing.Size(150, 17);
            this.lbRecognized.Text = "Recognized Contours: ";
            this.lbRecognized.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbRecognized.Click += new System.EventHandler(this.btAutoGenerate_Click);
            // 
            // btNewTemplates
            // 
            this.btNewTemplates.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btNewTemplates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btNewTemplates.Name = "btNewTemplates";
            this.btNewTemplates.Size = new System.Drawing.Size(23, 22);
            this.btNewTemplates.Text = "New templates";
            this.btNewTemplates.Click += new System.EventHandler(this.btNewTemplates_Click);
            // 
            // btOpenTemplates
            // 
            this.btOpenTemplates.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btOpenTemplates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btOpenTemplates.Name = "btOpenTemplates";
            this.btOpenTemplates.Size = new System.Drawing.Size(23, 22);
            this.btOpenTemplates.Text = "Open templates";
            this.btOpenTemplates.Click += new System.EventHandler(this.btOpenTemplates_Click);
            // 
            // btSaveTemplates
            // 
            this.btSaveTemplates.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btSaveTemplates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSaveTemplates.Name = "btSaveTemplates";
            this.btSaveTemplates.Size = new System.Drawing.Size(23, 22);
            this.btSaveTemplates.Text = "Save templates";
            this.btSaveTemplates.Click += new System.EventHandler(this.btSaveTemplates_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btCreateTemplate
            // 
            this.btCreateTemplate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btCreateTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btCreateTemplate.Name = "btCreateTemplate";
            this.btCreateTemplate.Size = new System.Drawing.Size(23, 22);
            this.btCreateTemplate.Text = "Create template";
            this.btCreateTemplate.Click += new System.EventHandler(this.btCreateTemplate_Click);
            // 
            // btAutoGenerate
            // 
            this.btAutoGenerate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btAutoGenerate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btAutoGenerate.Name = "btAutoGenerate";
            this.btAutoGenerate.Size = new System.Drawing.Size(23, 22);
            this.btAutoGenerate.Text = "Auto generate templates";
            this.btAutoGenerate.Click += new System.EventHandler(this.btAutoGenerate_Click);
            // 
            // btTemplateEditor
            // 
            this.btTemplateEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btTemplateEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btTemplateEditor.Name = "btTemplateEditor";
            this.btTemplateEditor.Size = new System.Drawing.Size(23, 22);
            this.btTemplateEditor.Text = "Template viewer";
            this.btTemplateEditor.Click += new System.EventHandler(this.btTemplateEditor_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(499, 25);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 405);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // tmUpdateState
            // 
            this.tmUpdateState.Enabled = true;
            this.tmUpdateState.Interval = 1000;
            this.tmUpdateState.Tick += new System.EventHandler(this.tmUpdateState_Tick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbFPS,
            this.lbContoursCount,
            this.lbRecognized,
            this.toolStripLabel1,
            this.toolStripDropDownButton1,
            this.toolStripComboBox1,
            this.toolStripTextBox1,
            this.toolStripProgressBar1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(713, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(86, 22);
            this.toolStripLabel1.Text = "toolStripLabel1";
            this.toolStripLabel1.Click += new System.EventHandler(this.toolStripLabel1_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 15);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 452);
            this.Controls.Add(this.ibMain);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnSettings);
            this.Controls.Add(this.ssMain);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainForm";
            this.Text = "Contour Analysis Demo";
            ((System.ComponentModel.ISupportInitialize)(this.ibMain)).EndInit();
            this.pnSettings.ResumeLayout(false);
            this.pnSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panAndZoomPictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxACFdesc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinACF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinICF)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinDefinition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinContourLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinContourArea)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdaptiveThBlockSize)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void LoadTemplates(string fileName)
        {
            try
            {
                FileStream serializationStream = new FileStream(fileName, FileMode.Open);
                try
                {
                    this.processor.templates = (Templates) new BinaryFormatter().Deserialize(serializationStream);
                }
                finally
                {
                    if (!ReferenceEquals(serializationStream, null))
                    {
                        serializationStream.Dispose();
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message);
            }
        }

        private void ProcessFrame()
        {
            try
            {
                if (this.captureFromCam)
                {
                    this.frame = this._capture.QueryFrame();
                }
                this.frameCount++;
                this.processor.ProcessImage(this.frame);
                this.ibMain.Image = !this.cbShowBinarized.Checked ? ((IImage) this.frame) : ((IImage) this.processor.binarizedFrame);
            }
            catch (Exception exception1)
            {
                Console.WriteLine(exception1.Message);
            }
        }

        private void SaveTemplates(string fileName)
        {
            try
            {
                FileStream serializationStream = new FileStream(fileName, FileMode.Create);
                try
                {
                    new BinaryFormatter().Serialize(serializationStream, this.processor.templates);
                }
                finally
                {
                    if (!ReferenceEquals(serializationStream, null))
                    {
                        serializationStream.Dispose();
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message);
            }
        }

        private void StartCapture()
        {
            try
            {
                this._capture = new Capture();
                this.ApplyCamSettings();
            }
            catch (NullReferenceException exception1)
            {
                MessageBox.Show(exception1.Message);
            }
        }

        private void tmUpdateState_Tick(object sender, EventArgs e)
        {
            this.lbFPS.Text = (this.frameCount - this.oldFrameCount) + " fps";
            this.oldFrameCount = this.frameCount;
            if (!ReferenceEquals(this.processor.contours, null))
            {
                this.lbContoursCount.Text = "Contours: " + this.processor.contours.Count;
            }
            if (!ReferenceEquals(this.processor.foundTemplates, null))
            {
                this.lbRecognized.Text = "Recognized contours: " + this.processor.foundTemplates.Count;
            }
        }

        private void ssMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void lbContoursCount_Click(object sender, EventArgs e)
        {

        }
    }
}


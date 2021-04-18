/***********************************************************************************
 * Ramin Edjlal*********************************************************************
 CopyRighted 1398/0802**************************************************************
 TetraShop.Ir***********************************************************************
 ***********************************************************************************/
using ContourAnalysisDemo;
//#pragma warning disable CS0246 // The type or namespace name 'ContourAnalysisNS' could not be found (are you missing a using directive or an assembly reference?)
using ContourAnalysisNS;
//#pragma warning restore CS0246 // The type or namespace name 'ContourAnalysisNS' could not be found (are you missing a using directive or an assembly reference?)
//#pragma warning disable CS0246 // The type or namespace name 'Emgu' could not be found (are you missing a using directive or an assembly reference?)
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//#pragma warning restore CS0246 // The type or namespace name 'Emgu' could not be found (are you missing a using directive or an assembly reference?)
//#pragma warning disable CS0246 // The type or namespace name 'Emgu' could not be found (are you missing a using directive or an assembly reference?)
//#pragma warning restore CS0246 // The type or namespace name 'Emgu' could not be found (are you missing a using directive or an assembly reference?)
//#pragma warning disable CS0246 // The type or namespace name 'Emgu' could not be found (are you missing a using directive or an assembly reference?)
//#pragma warning restore CS0246 // The type or namespace name 'Emgu' could not be found (are you missing a using directive or an assembly reference?)
//#pragma warning disable CS0246 // The type or namespace name 'Emgu' could not be found (are you missing a using directive or an assembly reference?)
//#pragma warning restore CS0246 // The type or namespace name 'Emgu' could not be found (are you missing a using directive or an assembly reference?)

namespace ImageTextDeepLearning
{
    //Constructor
    public partial class FormImageTextDeepLearning : Form
    {
        List<string> TextMined = new List<string>();
       List< List<string>> TextMinedLogics = new List<List<string>>();
        private bool Resum = false;
        private Task tf = null;
        private bool DisablePaint = false;
        public static bool test = false;
        public static bool comeng = false;
        public static bool fontsel = false;
        public static Font selfont = null;
        private bool Recognized = false;

        //Global vars
        private DetectionOfLitteral On = null;

        //#pragma warning disable CS0108 // 'FormImageTextDeepLearning.Width' hides inherited member 'Control.Width'. Use the new keyword if hiding was intended.
        //#pragma warning disable CS0108 // 'FormImageTextDeepLearning.Height' hides inherited member 'Control.Height'. Use the new keyword if hiding was intended.
        private readonly int Width = 10, Height = 10;

        //#pragma warning restore CS0108 // 'FormImageTextDeepLearning.Height' hides inherited member 'Control.Height'. Use the new keyword if hiding was intended.
        //#pragma warning restore CS0108 // 'FormImageTextDeepLearning.Width' hides inherited member 'Control.Width'. Use the new keyword if hiding was intended.
        private readonly List<ConjunctedShape> conShapes = new List<ConjunctedShape>();
        private SmallImageing t = null;
        private MainForm d = null;
        //Main Form constructor
        public FormImageTextDeepLearning()
        {
            InitializeComponent();
        }
        //Load form
        private void FormImageTextDeepLearning_Load(object sender, EventArgs e)
        {
            //Thread of load
            Thread t = new Thread(new ThreadStart(Progress));
            t.Start();
        }
        //click on open buttonb event
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            //determine file of image name
            openFileDialogImageTextDeepLearning.ShowDialog();
            //Assign content of image on main picture box
            PictureBoxImageTextDeepLearning.BackgroundImage = Image.FromFile(openFileDialogImageTextDeepLearning.FileName);
            //PictureBoxImageTextDeepLearning.Size = new Size(PictureBoxImageTextDeepLearning.BackgroundImage.Width, PictureBoxImageTextDeepLearning.BackgroundImage.Height);
            //set scale
            PictureBoxImageTextDeepLearning.BackgroundImageLayout = ImageLayout.Stretch;
            //refresh and update to pain event occured
            PictureBoxImageTextDeepLearning.Refresh();
            PictureBoxImageTextDeepLearning.Update();


        }

        private void openFileDialogImageTextDeepLearning_FileOk(object sender, CancelEventArgs e)
        {

        }
        //splitation and conjunction of one load image deterministic
        private void buttonSplitationConjunction_Click(object sender, EventArgs e)
        {
            //when there is no image
            if (PictureBoxImageTextDeepLearning.BackgroundImage == null)
            {
                //set image to back image
                PictureBoxImageTextDeepLearning.BackgroundImage = PictureBoxImageTextDeepLearning.Image;
                PictureBoxImageTextDeepLearning.Image = null;
            }
            //wen ready to splitation
            if (buttonSplitationConjunction.Text == "Splitation")
            {
                //create constructor image
                t = new SmallImageing(PictureBoxImageTextDeepLearning.BackgroundImage);
                //Do splitation
                bool Do = t.Splitation(PictureBoxTest);
                //wen successfull
                if (Do)
                {
                    //change operation recurve
                    buttonSplitationConjunction.Text = "Conjunction";
                    MessageBox.Show("Splited!");
                }
                else
                {
                    MessageBox.Show("Unsuccessfull splitation;");
                }
            }
            else//when ready to conjunction
if (buttonSplitationConjunction.Text == "Conjunction")
            {
                //Do conjunction
                bool Do = t.Conjunction(PictureBoxTest, PictureBoxImageTextDeepLearning);
                //when successfull
                if (Do)
                {
                    //assgin conjuncted image to back image and refresh and update to pain even occured
                    PictureBoxImageTextDeepLearning.BackgroundImage = t.RootConjuction;
                    PictureBoxImageTextDeepLearning.Refresh();
                    PictureBoxImageTextDeepLearning.Update();
                    buttonSplitationConjunction.Text = "Splitation";
                    MessageBox.Show("Conjuncted!");
                }
                else
                {
                    MessageBox.Show("Unsuccessfull conjunction;");
                }
            }


        }

        private void progressBarCompleted_Click(object sender, EventArgs e)
        {
            /*  if (t.SplitedBegin)
              {
                  int Total = t.RootWidth * t.RootWidth;
                  int Cuurent = t.i * t.j;
                  progressBarCompleted.Maximum = Total;
                  progressBarCompleted.Minimum = 0;
                  progressBarCompleted.Value = Cuurent;
              }
              else
  if (t.ConjuctedBegin)
              {
                  int Total = t.imgarray.Count;
                  int Cuurent = t.i;
                  progressBarCompleted.Maximum = Total;
                  progressBarCompleted.Minimum = 0;
                  progressBarCompleted.Value = Cuurent;
              }*/
        }

        private void backgroundWorkerProgress_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void Progress()
        {
            /*  bool init = false;
              do
              {
                  if (t != null)
                  {
                      if (t.SplitedBegin)
                      {
                          int Cuurent = t.i * t.j;
                          int Total = t.RootWidth * t.RootHeight;

                          d.Invoke((MethodInvoker)delegate ()
                          {
                               progressBarCompleted.Maximum = Total;
                              progressBarCompleted.Minimum = 0;

                              progressBarCompleted.Value = Cuurent;
                              progressBarCompleted.Update();
                          });
                      }
                      else
      if (t.ConjuctedBegin)
                      {
                          int Total = t.imgarray.Count;
                          int Cuurent = t.i;


                          d.Invoke((MethodInvoker)delegate ()
                          {
                              progressBarCompleted.Maximum = Total;
                              progressBarCompleted.Minimum = 0;

                              progressBarCompleted.Value = Cuurent;
                              progressBarCompleted.Update();
                          });
                      }
                  }
              } while (true);*/
        }
        //detect of literalson image to be text 
        private void buttonTxtDetect_Click(object sender, EventArgs e)
        {
            Recognized = false;
            textBoxImageTextDeepLearning.Text = "";

            //detection foregin unnkown app constructor
            d = new MainForm();
            d.ShowDialog();



            //textBoxImageTextDeepLearning.Refresh();
            //textBoxImageTextDeepLearning.Update();
            //d.Dispose();
            PictureBoxImageTextDeepLearning.Update();
            PictureBoxImageTextDeepLearning.Refresh();
            CreateConSha.Visible = true;
        }

        //delegates on lables
        private delegate void CallRefLable();
        public void RefCallSetLablr()
        {
            if (InvokeRequired)
            {
                CallRefLable t = new CallRefLable(RefCallSetLablr);

                Invoke(new Action(() => labelMonitor.Refresh()));

            }


        }

        private delegate void CallSetLable(string Text);
        public void SetCallSetLablr(string Text)
        {
            if (InvokeRequired)
            {
                CallSetLable t = new CallSetLable(SetCallSetLablr);

                Invoke(new Action(() => labelMonitor.Text = Text));

            }


        }

        //main picture boc pain event
        private void PictureBoxImageTextDeepLearning_Paint(object sender, PaintEventArgs e)
        {
            if (!GraphS.Drawn)
            {
                if (!DisablePaint)
                {
                    bool Re = false;
                    //when foregin is ready
                    if (d != null)
                    {
                        //initiate local vars
                        Font font;
                        Brush brush;
                        Brush brush2;
                        Pen pen;
                        bool flag2;
                        //when is ready top detected unconjuncted shapes set draw parameters
                        if (!ReferenceEquals(d.frame, null))
                        {
                            font = new Font(d.Font.FontFamily, 24f);
                            e.Graphics.DrawString(d.lbFPS.Text, new Font(d.Font.FontFamily, 16f), Brushes.Yellow, new PointF(1f, 1f));
                            brush = new SolidBrush(Color.FromArgb(0xff, 0, 0, 0));
                            brush2 = new SolidBrush(Color.FromArgb(0xff, 0xff, 0xff, 0xff));
                            pen = new Pen(Color.FromArgb(150, 0, 0xff, 0));
                            flag2 = false;
                            if (!flag2)
                            {
                                using (List<Contour<Point>>.Enumerator enumerator = d.processor.contours.GetEnumerator())
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
                        //lock (d.processor.foundTemplates)
                        {
                            using (List<FoundTemplateDesc>.Enumerator enumerator2 = d.processor.foundTemplates.GetEnumerator())
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
                                        d.DrawAugmentedReality(current, e.Graphics);
                                        continue;
                                    }
                                    Rectangle sourceBoundingRect = current.sample.contour.SourceBoundingRect;
                                    Point point = new Point((sourceBoundingRect.Left + sourceBoundingRect.Right) / 2, sourceBoundingRect.Top);
                                    string name = current.template.name;
                                    if (d.showAngle)
                                    {
                                        name = name + $"angle={((180.0 * current.angle) / 3.1415926535897931):000}°scale={current.scale:0.0}";

                                    }
                                    if (!Recognized)
                                    {
                                        textBoxImageTextDeepLearning.Text += name;
                                        textBoxImageTextDeepLearning.Refresh();
                                        textBoxImageTextDeepLearning.Update();
                                        Re = true;
                                    }
                                    e.Graphics.DrawRectangle(pen, sourceBoundingRect);
                                    e.Graphics.DrawString(name, font, brush, new PointF((point.X + 1) - (font.Height / 3), (point.Y + 1) - font.Height));
                                    e.Graphics.DrawString(name, font, brush2, new PointF(point.X - (font.Height / 3), point.Y - font.Height));
                                }
                            }
                        }

                    }
                    if (Re)
                    {
                        Recognized = true;
                    }

                    PictureBoxImageTextDeepLearning.Update();
                    PictureBoxImageTextDeepLearning.Refresh();
                }
            }
            else
            {/*
                  if (GraphS.Z != null)
                  {
                      Bitmap s = new Bitmap(PictureBoxImageTextDeepLearning.Width, PictureBoxImageTextDeepLearning.Height);
                      Graphics g = Graphics.FromImage(s);
                      if (ContourAnalysisNS.GraphS.Z.A != null)
                      {
                          if (ContourAnalysisNS.GraphS.Z.A.Xv != null)
                          {
                              for (int i = 0; i < ContourAnalysisNS.GraphS.Z.A.Xv.Count; i++)
                              {

                                  g.DrawString("*", new Font("Tahoma", 10F), new SolidBrush(Color.Red), new PointF(ContourAnalysisNS.GraphS.Z.A.Xv[i].X * 10, ContourAnalysisNS.GraphS.Z.A.Xv[i].Y * 10));

                              }
                          }
                      }
                      if (ContourAnalysisNS.GraphS.Z.B != null)
                      {
                          if (ContourAnalysisNS.GraphS.Z.B.Xv != null)
                          {
                              for (int i = 0; i < ContourAnalysisNS.GraphS.Z.B.Xv.Count; i++)
                              {

                                  g.DrawString("*", new Font("Tahoma", 10F), new SolidBrush(Color.Blue), new PointF(ContourAnalysisNS.GraphS.Z.B.Xv[i].X * 10, ContourAnalysisNS.GraphS.Z.B.Xv[i].Y * 10));

                              }
                          }
                      }
                      if (ContourAnalysisNS.GraphS.ZB != null)
                      {
                          if (ContourAnalysisNS.GraphS.ZB.Xv != null)
                          {
                              for (int i = 0; i < ContourAnalysisNS.GraphS.ZB.Xv.Count; i++)
                              {

                                  g.DrawString("*", new Font("Tahoma", 10F), new SolidBrush(Color.Green), new PointF(ContourAnalysisNS.GraphS.ZB.Xv[i].X * 10, ContourAnalysisNS.GraphS.ZB.Xv[i].Y * 10));

                              }
                          }
                      }
                      g.Dispose();
                      PictureBoxImageTextDeepLearning.BackgroundImage = s;
                      PictureBoxImageTextDeepLearning.Refresh();
                      PictureBoxImageTextDeepLearning.Update();
                      Thread.Sleep(100);
                      GraphS.Drawn = false;
                  }*/
            }
        }
        public void GraphsDrawn(GraphDivergenceMatrix A, GraphDivergenceMatrix B)
        {
            Bitmap ss = new Bitmap(PictureBoxImageTextDeepLearning.Width, PictureBoxImageTextDeepLearning.Height);
            PictureBoxImageTextDeepLearning.BackgroundImage = ss;
            Graphics g = Graphics.FromImage(PictureBoxImageTextDeepLearning.BackgroundImage);
            if (A != null)
            {
                if (A.Xv != null)
                {
                    for (int i = 0; i < A.Xv.Count; i++)
                    {
                        g.DrawString("*", new Font("Tahoma", 10F), new SolidBrush(Color.Red), new PointF(A.Xv[i].X * 10, A.Xv[i].Y * 10));

                        for (int j = 0; j < A.Xv.Count; j++)
                        {

                            if (A.d(A.Xv[i], A.Xv[j]) != null)
                                g.DrawLine(new Pen(Color.Red), new Point(A.Xv[i].X * 10, A.Xv[i].Y * 10), new Point(A.Xv[j].X * 10, A.Xv[j].Y * 10));

                        }
                    }
                }
            }
            if (B != null)
            {
                if (B.Xv != null)
                {
                    for (int i = 0; i < B.Xv.Count; i++)
                    {
                        g.DrawString("*", new Font("Tahoma", 10F), new SolidBrush(Color.Blue), new PointF(B.Xv[i].X * 10, B.Xv[i].Y * 10));

                        for (int j = 0; j < B.Xv.Count; j++)
                        {

                            if (B.d(B.Xv[i], B.Xv[j]) != null)
                                g.DrawLine(new Pen(Color.Blue), new Point(B.Xv[i].X * 10, B.Xv[i].Y * 10), new Point(B.Xv[j].X * 10, B.Xv[j].Y * 10));

                        }
                    }
                }
            }
            /*if (ContourAnalysisNS.GraphS.ZB != null)
            {
                if (ContourAnalysisNS.GraphS.ZB.Xv != null)
                {
                    for (int i = 0; i < ContourAnalysisNS.GraphS.ZB.Xv.Count; i++)
                    {

                        g.DrawString("*", new Font("Tahoma", 10F), new SolidBrush(Color.Green), new PointF(ContourAnalysisNS.GraphS.ZB.Xv[i].X * 10, ContourAnalysisNS.GraphS.ZB.Xv[i].Y * 10));

                    }
                }
            }*/
            g.Dispose();
            PictureBoxImageTextDeepLearning.Refresh();
            PictureBoxImageTextDeepLearning.Update();
        }
        private void PictureBoxImageTextDeepLearning_Click(object sender, EventArgs e)
        {

        }
        //disable algins paint on foregin form
        private void checkBoxDisablePaintOnAligns_CheckedChanged(object sender, EventArgs e)
        {
            if (d != null)
            {
                if (checkBoxDisablePaintOnAligns.Checked)
                {
                    d.DisablePaintOnAligns = true;
                }
                else
                {
                    d.DisablePaintOnAligns = false;
                }
            }
        }

        //main detection form
        private void CreateOneConShape()
        {

            //when cunsoming is ready
            if (!ReferenceEquals(d.frame, null))
            {
                lock (d.processor.foundTemplates)
                {
                    /* ConjunctedShape One = new ConjunctedShape(d);
                     One.CreateSAhapeFromConjucted(Width, Height);
                     AllKeyboardOfWorld Tow = new AllKeyboardOfWorld();
                     Tow.ConvertAllImageToMatrix(One.AllImage);
                     AllKeyboardOfWorld Three = new AllKeyboardOfWorld();
                     Three.ConvertAllStringToImage(d);

                 */
                    //call detection constructor
                    FormImageTextDeepLearning This = this;
                    On = new DetectionOfLitteral(ref This, d);
                }
            }

        }
        //about
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new AboutBoxImageTextDeepLearning()).Show();
        }
        //menue strip of open file 
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialogImageTextDeepLearning.ShowDialog();
            PictureBoxImageTextDeepLearning.BackgroundImage = Image.FromFile(openFileDialogImageTextDeepLearning.FileName);
            //PictureBoxImageTextDeepLearning.Size = new Size(PictureBoxImageTextDeepLearning.BackgroundImage.Width, PictureBoxImageTextDeepLearning.BackgroundImage.Height);

            PictureBoxImageTextDeepLearning.BackgroundImageLayout = ImageLayout.Stretch;

            PictureBoxImageTextDeepLearning.Refresh();
            PictureBoxImageTextDeepLearning.Update();


        }
        //Open file and load
        private void splitationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PictureBoxImageTextDeepLearning.BackgroundImage == null)
            {
                PictureBoxImageTextDeepLearning.BackgroundImage = PictureBoxImageTextDeepLearning.Image;
                PictureBoxImageTextDeepLearning.Image = null;
            }
            if (buttonSplitationConjunction.Text == "Splitation")
            {
                t = new SmallImageing(PictureBoxImageTextDeepLearning.BackgroundImage);

                bool Do = t.Splitation(PictureBoxTest);

                if (Do)
                {
                    buttonSplitationConjunction.Text = "Conjunction";
                    MessageBox.Show("Splited!");
                }
            }
            else
if (buttonSplitationConjunction.Text == "Conjunction")
            {
                bool Do = t.Conjunction(PictureBoxTest, PictureBoxImageTextDeepLearning);
                if (Do)
                {
                    PictureBoxImageTextDeepLearning.BackgroundImage = t.RootConjuction;
                    PictureBoxImageTextDeepLearning.Refresh();
                    PictureBoxImageTextDeepLearning.Update();
                    buttonSplitationConjunction.Text = "Splitation";
                    MessageBox.Show("Conjuncted!");
                }
            }
        }

        private void Draw()
        {
            for (int i = 0; i < On.tt.AllImage.Count; i++)
            {
                object O = new object();
                lock (O)
                {
                    try
                    {
                        PictureBoxTest.BackgroundImage = On.tt.AllImage[i];
                        PictureBoxTest.BackgroundImageLayout = ImageLayout.Zoom;
                        PictureBoxTest.Refresh();
                        PictureBoxTest.Update();

                    }
                    catch (System.Exception) { }
                }
            }
        }
        //conjuncton create shapes menue strip
        private void createConjunctionShapesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(CreateOneConShape));
            t.Start();
            t.Join();
            t = new Thread(new ThreadStart(Draw));
            t.Start();
            t.Join();

            for (int i = 0; i < On.Detected.Count; i++)
            {
                textBoxImageTextDeepLearning.AppendText(On.Detected[i]);

            }
            /* for (int i = 0; i < On.t.KeyboardAllImage.Count; i++)
             {
                 PictureBoxTest.BackgroundImage = On.t.KeyboardAllImage[i];
                 PictureBoxTest.BackgroundImageLayout = ImageLayout.Zoom;
                 PictureBoxTest.Refresh();
                 PictureBoxTest.Update();
                 
             }*/
        }
        //detection form munue strip
        private void txtDetectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            d = new MainForm();
            d.ShowDialog();



            //textBoxImageTextDeepLearning.Refresh();
            //textBoxImageTextDeepLearning.Update();
            //d.Dispose();
            PictureBoxImageTextDeepLearning.Update();
            PictureBoxImageTextDeepLearning.Refresh();
        }

        private void buttonTxtTemplates_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxUndetectiveFont_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                AllKeyboardOfWorld a = new AllKeyboardOfWorld();
                a.ListAllFonts();
                comboBoxUndetectiveFont.Items.AddRange(AllKeyboardOfWorld.fonts.ToArray());
                fontsel = true;
            }
        }

        private void comboBoxUndetectiveFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            AllKeyboardOfWorld.fonts.Clear();
            selfont = new System.Drawing.Font(comboBoxUndetectiveFont.Text, 10);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBoxUseCommonEnglish_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                comeng = true;
            }
        }

        private void PictureBoxTest_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                test = true;
                checkBoxUseCommonEnglish.Enabled = false;
            }
            else
            {
                test = false;
                checkBoxUseCommonEnglish.Enabled = true;
            }
        }
        public void Set()
        {
            do
            {
                try
                {
                    do { } while (!GraphS.Drawn);
                    PictureBoxImageTextDeepLearning.Invalidate();
                    PictureBoxImageTextDeepLearning.Refresh();
                }
                catch (Exception) { }
            } while (true);
        }

        private void c()
        {
            int len = 0;
            do
            {
                if (On != null)
                {
                    if (On.Detected != null)
                    {
                        if (len != On.Detected.Count)
                        {
                            Resum = true;

                            Invoke((MethodInvoker)delegate ()
                            {
                                textBoxImageTextDeepLearning.Text = "";
                                for (int i = 0; i < On.Detected.Count; i++)
                                {
                                    textBoxImageTextDeepLearning.AppendText(On.Detected[i]);
                                    textBoxImageTextDeepLearning.Update();
                                }
                            });
                            len = On.Detected.Count;

                            Resum = false;
                        }
                    }
                }
                Thread.Sleep(1000);

            } while (true);
        }
        int WordNumber(string s)
        {
            int no = 0;
            string c = s;
            do
            {
                if (c[0] == ' ')
                {
                    c = c.Remove(0, 1);
                }
                else
                    break;
            } while (true);
            int len = -1;
            do
            {
                len = c.IndexOf(" ");
                if (len > -1)
                {
                    no++;
                    c = c.Remove(0, len + 1);
                }
                else
                {
                    len = c.IndexOf(".");
                    if (len > -1)
                    {
                        no++;
                        c = c.Remove(0, len + 1);
                    }
                }
            } while (c.Length > 0);
            return no;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            do
            {

                string s = textBoxImageTextDeepLearning.Text.Substring(0, textBoxImageTextDeepLearning.Text.IndexOf(".") + 1);
                if (s.Contains("است.") && WordNumber(s) == 3)
                    TextMined.Add(s);
                textBoxImageTextDeepLearning.Text = textBoxImageTextDeepLearning.Text.Remove(0, textBoxImageTextDeepLearning.Text.IndexOf(".") + 1);
            } while (textBoxImageTextDeepLearning.Text.Contains("."));
            textBoxImageTextDeepLearning.Text = "";
            for (int i = 0; i < TextMined.Count; i++)
                textBoxImageTextDeepLearning.Text += TextMined[i];
            textBoxImageTextDeepLearning.Refresh();
            textBoxImageTextDeepLearning.Update();

            List<string> mined =new List<string>();
            for (int i = 0; i < TextMined.Count; i++)
                mined.Add(TextMined[i]);
            for (int i = 0; i < mined.Count; i++)
            {
                TextMinedLogics.Add(new List<string>());
                string s = mined[i];

                int no = 0;
                string c = s;
                do
                {
                    if (c[0] == ' ')
                    {
                        c = c.Remove(0, 1);
                    }
                    else
                        break;
                } while (true);
                int len = -1;
                do
                {
                    len = c.IndexOf(" ");
                    if (len > -1)
                    {
                        no++;
                        TextMinedLogics[TextMinedLogics.Count - 1].Add(c.Substring(0, len));
                        c = c.Remove(0, len + 1);
                    }
                    else
                    {
                        len = c.IndexOf(".");
                        if (len > -1)
                        {
                            no++;
                           //TextMinedLogics[TextMinedLogics.Count - 1].Add(c.Substring(0, len));
                            c = c.Remove(0, len + 1);
                        }
                    }
                } while (c.Length > 0);
                
            }
            textBoxImageTextDeepLearning.Text += "\r\n========================================";
            ResultsOfSupposed.MindedIsVerb(TextMined, TextMinedLogics);
            //MessageBox.Show("نتایج!");
            for (int i = 0; i < ResultsOfSupposed.mined.Count; i++)
                 textBoxImageTextDeepLearning.Text += ResultsOfSupposed.mined[i];
            textBoxImageTextDeepLearning.Refresh();
            textBoxImageTextDeepLearning.Update();

        }

        //create main detection button
        private void CreateConSha_Click(object sender, EventArgs e)
        {
            //var H = Task.Factory.StartNew(() => c());
            //tf = Task.Factory.StartNew(() => CreateOneConShape());
            //tf.Wait();
            CreateOneConShape();

            DisablePaint = true;
            MessageBox.Show("Samples!");
            for (int i = 0; i < On.ConjunctedShapeListRequired.KeyboardAllImage.Count; i++)
            {
                PictureBoxTest.BackgroundImage = On.ConjunctedShapeListRequired.KeyboardAllImage[i];
                PictureBoxTest.BackgroundImageLayout = ImageLayout.Zoom;
                PictureBoxTest.Refresh();
                PictureBoxTest.Update();
                MessageBox.Show("Next!");
                PictureBoxTest.BackgroundImage.Dispose();

            }
            if (!test)
            {
                MessageBox.Show("part of References!");
            }
            else
            {
                MessageBox.Show("References!");
            }

            for (int i = 0; i < On.t.KeyboardAllImage.Count; i++)
            {
                PictureBoxTest.BackgroundImage = On.t.KeyboardAllImage[i];
                PictureBoxTest.BackgroundImageLayout = ImageLayout.Zoom;
                PictureBoxTest.Refresh();
                PictureBoxTest.Update();
                MessageBox.Show("Next!");
                PictureBoxTest.BackgroundImage.Dispose();

            }
            DisablePaint = false;
            
            for (int i = 0; i < On.Detected.Count; i++)
            {
                textBoxImageTextDeepLearning.AppendText(On.Detected[i]);
                textBoxImageTextDeepLearning.Refresh();
                textBoxImageTextDeepLearning.Update();
            }
            buttonSplitationConjunction.Visible = true;
            /* for (int i = 0; i < On.t.KeyboardAllImage.Count; i++)
             {
                 PictureBoxTest.BackgroundImage = On.t.KeyboardAllImage[i];
                 PictureBoxTest.BackgroundImageLayout = ImageLayout.Zoom;
                 PictureBoxTest.Refresh();
                 PictureBoxTest.Update();

             }*/
        }
    }

}
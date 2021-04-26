using ContourAnalysisDemo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ImageTextDeepLearning
{
    //To Store All Keyboard literals
    [Serializable]
    internal class AllKeyboardOfWorld
    {
        bool Hollowed = false;
        public static List<string> fonts = new List<string>();
        public static char[] engsmal = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        public static char[] engbig = null;
        public static char[] engnum = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ' ' };
        
        private readonly char[] te = { 'v', '3', ' ' };
        public AllKeyboardOfWorld()
        {
            if (fonts.Count == 0)
            {
                fonts.Clear();
                bool Do = ListAllFonts();
                if (!Do)
                {
                    fonts.Clear();
                }
            }
        }
        //Initiate global vars
        private readonly int Width = 10, Height = 10;
        public List<string> KeyboardAllStringsWithfont = new List<string>();
        public List<string> KeyboardAllStrings = new List<string>();
        public List<Bitmap> KeyboardAllImage = new List<Bitmap>();
        public List<bool[,]> KeyboardAllConjunctionMatrix = new List<bool[,]>();
        //Crate all able chars on List indevidully
        public bool CreateString()
        {
            //when not existence
            if (KeyboardAllStrings.Count == 0)
            {
                //clear
                KeyboardAllStrings.Clear();
                try
                {
                    if (!FormImageTextDeepLearning.comeng && FormImageTextDeepLearning.test == false)
                    {   //for all possible
                        for (int i = 0; i < char.MaxValue; i++)
                        {
                            //get type of current
                            Type t = ((char)i).GetType();
                            //when is char and visible and is serializable
                            if (t.Equals(typeof(char)) && t.IsVisible && t.IsSerializable)
                            {
                                //if (((char)i).ToString().Contains("\\u"))
                                
                                //when existemnce of this conditions continue
                                int ch = i;
                                if ((ch >= 0x0020 && ch <= 0xD7FF) ||
                                        (ch >= 0xE000 && ch <= 0xFFFD) ||
                                        ch == 0x0009 ||
                                        ch == 0x000A ||
                                        ch == 0x000D)
                                {
                                    //sdetermine and Store
                                    if (!KeyboardAllStrings.Contains(((char)i).ToString()))
                                    {
                                        KeyboardAllStrings.Add(((char)i).ToString());
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (FormImageTextDeepLearning.test == false)
                        {
                            for (int i = 0; i < engsmal.Length; i++)
                            {
                                KeyboardAllStrings.Add(Convert.ToString(engsmal[i]));
                            }
                            for (int i = 0; i < engsmal.Length; i++)
                            {
                                KeyboardAllStrings.Add(Convert.ToString(engsmal[i]).ToUpper());
                            }
                            for (int i = 0; i < engnum.Length; i++)
                            {
                                KeyboardAllStrings.Add(Convert.ToString(engnum[i]));
                            }
                        }
                        else
                        {
                            try
                            {
                                if (File.Exists("KeyboardAllStrings.asd"))
                                {
                                    File.Delete("KeyboardAllStrings.asd");
                                }
                            }
                            catch (Exception)
                            {
                            }
                            for (int i = 0; i < te.Length; i++)
                            {
                                KeyboardAllStrings.Add(Convert.ToString(te[i]));
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
        public bool ListAllFonts()
        {
            try
            {
                if (FormImageTextDeepLearning.fontsel == false && FormImageTextDeepLearning.test == false)
                {
                    foreach (FontFamily font in System.Drawing.FontFamily.Families)
                    {
                        fonts.Add(font.Name);
                    }
                }
                else
                {
                    fonts.Add(FormImageTextDeepLearning.selfont.ToString());
                }
            }
            catch (Exception) { return false; }
            return true;
        }
        //Savle all
        private bool SaveAll()
        {
            try
            {
                //when file dos not exist
                if (!File.Exists("KeyboardAllStrings.asd"))
                {
                    
                    //serialized on take root
                    if (KeyboardAllImage.Count > 0)
                    {
                        Refrigtz.TakeRoot t = new Refrigtz.TakeRoot();
                        t.Save(this, "KeyboardAllStrings.asd");
                    }
                }
                else
                {//delete and serilized take root
                    File.Delete("KeyboardAllStrings.asd");
                    if (KeyboardAllImage.Count > 0)
                    {
                        Refrigtz.TakeRoot t = new Refrigtz.TakeRoot();
                        t.Save(this, "KeyboardAllStrings.asd");
                    }
                }
            }
            catch (Exception)
            {
                
            }
            return true;
        }
        //read all
        private bool ReadAll()
        {
            try
            {
                //when existence
                if (File.Exists("KeyboardAllStrings.asd"))
                {
                    //clear
                    KeyboardAllStrings.Clear();
                    KeyboardAllImage.Clear();
                    KeyboardAllConjunctionMatrix.Clear();

                    
                    //serilized
                    Refrigtz.TakeRoot tr = new Refrigtz.TakeRoot();
                    AllKeyboardOfWorld t = tr.Load("KeyboardAllStrings.asd");
                    KeyboardAllConjunctionMatrix = t.KeyboardAllConjunctionMatrix;
                    KeyboardAllConjunctionMatrix = t.KeyboardAllConjunctionMatrix;
                    KeyboardAllImage = t.KeyboardAllImage;
                    KeyboardAllStrings = t.KeyboardAllStrings;
                }
                else//others retiurn unsuccessfull
                {
                    return false;
                }
            }
            catch (Exception)
            {
                //when unsuccessfull return false
                return false;
            }
            //return true
            return true;
        }
        //Cropping and fitting image
        private Bitmap cropImage(Bitmap img, Rectangle cropArea)
        {
            int X = cropArea.X;
            int Y = cropArea.Y;
            int XX = cropArea.Width;
            int YY = cropArea.Height;


            Bitmap bmp = new Bitmap(Width, Height);
            using (Graphics gph = Graphics.FromImage(bmp))
            {
                gph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gph.DrawImage(img, new Rectangle(0, 0, Width, Height), new Rectangle(X, Y, XX, YY), GraphicsUnit.Pixel);
                gph.Dispose();
            }
            
            return bmp;
        }
        //Found of Min of X
        private int MinX(Bitmap Im)
        {
            int Mi = -1;
            for (int j = 0; j < Im.Width; j++)
            {
                for (int k = 0; k < Im.Height; k++)
                {
                    if ((Im.GetPixel(j, k).ToArgb()== Color.Black.ToArgb()))
                    {
                        Mi = j;
                        break;
                    }
                }
                if (Mi > -1)
                {
                    break;
                }
            }
            return Mi;
        }
        //Founf Min of Y
        private int MinY(Bitmap Im)
        {
            int Mi = -1;
            for (int k = 0; k < Im.Height; k++)
            {
                for (int j = 0; j < Im.Width; j++)
                {
                    if ((Im.GetPixel(j, k).ToArgb()== Color.Black.ToArgb()))
                    {
                        Mi = k;
                        break;
                    }
                }
                if (Mi > -1)
                {
                    break;
                }
            }
            return Mi;
        }
        //Found of Max Of Y
        private int MaxY(Bitmap Im)
        {
            int Ma = -1;
            for (int k = Im.Height - 1; k >= 0; k--)
            {
                for (int j = 0; j < Im.Width; j++)
                {
                    if ((Im.GetPixel(j, k).ToArgb()== Color.Black.ToArgb()))
                    {
                        Ma = k;
                        break;
                    }
                }
                if (Ma > -1)
                {
                    break;
                }
            }
            return Ma;
        }
        //Found of Max of X
        private int MaxX(Bitmap Im)
        {
            int Ma = -1;
            
            //{
            for (int j = Im.Width - 1; j >= 0; j--)
            {
                
                // {
                for (int k = 0; k < Im.Height; k++)
                {
                    if ((Im.GetPixel(j, k).ToArgb() == Color.Black.ToArgb()))
                    {
                        Ma = j;
                        break;
                    }
                }
                if (Ma > -1)
                {
                    break;
                }
            }
            return Ma;
        }
        //Colorized list of image
        private bool ColorizedCountreImageCommon(List<Bitmap> Im)
        {
            try
            {
                //for all list items
                
                ///{
                for (int i = 0; i < Im.Count; i++)
                {
                    //create graphics for current image
                    Graphics e = Graphics.FromImage(Im[i]);
                    //for all image width
                    
                    //{
                    for (int j = 0; j < Im[i].Width; j++)
                    {
                        //found of tow orthogonal detinated points
                        PointF[] Po = new PointF[2];
                        int nu = 0;
                        for (int k = 0; k < Im[i].Height; k++)
                        {
                            //first
                            if (nu == 0)
                            {
                                if ((Im[i].GetPixel(j, k).ToArgb() == Color.Black.ToArgb()))
                                {
                                    Po[0] = new PointF(j, k);
                                    nu++;
                                }
                            }
                            else//second
                            if (nu == 1)
                            {
                                if ((Im[i].GetPixel(j, k).ToArgb()==Color.Black.ToArgb()))
                                {
                                    Po[1] = new PointF(j, k);
                                    nu++;
                                    //draw linnes and free var to coninue
                                    e.DrawLines(Pens.Black, Po);
                                    nu = 0;
                                }
                            }
                        }
                    }
                    //Dispose
                    e.Dispose();
                }

            }
            catch (Exception t)
            {
                MessageBox.Show(t.ToString());
                //return unsuccessfull
                return false;
            }
            //return successfull
            return true;
        }
        //Colorized an image
        private bool ColorizedCountreImageCommmon(ref Bitmap Im)
        {
            try
            {
                //create graphics for current image
                Graphics e = Graphics.FromImage(Im);
                //for all image width
                for (int j = 0; j < Im.Width; j++)
                {
                    //found of tow orthogonal detinated points
                    Point[] Po = new Point[2];
                    int nu = 0;
                    for (int k = 0; k < Im.Height; k++)
                    {
                        //first
                        if (nu == 0)
                        {
                            if ((Im.GetPixel(j, k).ToArgb()== Color.Black.ToArgb()))
                            {
                                Po[0] = new Point(j, k);
                                nu++;
                            }
                        }
                        else//second
                        if (nu == 1)
                        {
                            if ((Im.GetPixel(j, k).ToArgb()== Color.Black.ToArgb()))
                            {
                                Po[1] = new Point(j, k);
                                nu++;
                                //draw linnes and free var to coninue
                                e.DrawLines(Pens.Black, Po);
                                nu = 0;
                            }
                        }
                    }
                }


            }
            catch (Exception t)
            {
                MessageBox.Show(t.ToString());
                return false;
            }
            return true;
        }
         private bool HollowCountreImageCommmon(ref Bitmap Img, bool[,] Ab)
        {
            try
            {
                int wi = Img.Width;
                int he = Img.Height;
                Bitmap Im = Img;
                List<Task> th = new List<Task>();
                Graphics e = Graphics.FromImage(Im);
                e.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //do
                // {
                Hollowed = false;
                var output = Task.Factory.StartNew(() =>
                {
                    
                    //{
                    ParallelOptions po = new ParallelOptions
                    {
                        MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                    }; Parallel.For(0, wi, x =>
                        {
                            
                            //{
                            ParallelOptions poo = new ParallelOptions
                            {
                                MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                            }; Parallel.For(0, wi, y =>
                            {
                                object o = new object();
                                lock (o)
                                {
                                    if (Ab[x, y])
                                    {
                                        var H = Task.Factory.StartNew(() => HollowCountreImageCommmonXY(ref Im, x, y, wi, he, x, y, Ab));
                                        
                                        
                                    }
                                }
                            });

                        });
                });
                output.Wait();
                
                e.Dispose();
                
                Img = Im;
            }
            catch (Exception t)
            {
                MessageBox.Show(t.ToString());
                return false;
            }
            return true;
        }
        private bool HollowCountreImageCommmonXY(ref Bitmap Img, int x, int y, int wi, int he, int X, int Y, bool[,] Ab)
        {
            Bitmap Im = Img;
            try
            {
                bool IsOut1 = false;
                bool IsOut2 = false;
                bool IsOut3 = false;
                bool IsOut4 = false;
                if (!(x >= 0 && y >= 0 && x < wi && y < he))
                    return false;
                bool Is = true;
                object o = new object();
                lock (o)
                {
                    if ((Im.GetPixel(X, Y).ToArgb() == Color.Black.ToArgb()))
                    {
                        if ((x != X && y != Y))
                        {
                            if ((Im.GetPixel(x, y).ToArgb() == Color.Black.ToArgb()))
                            {
                                return true;
                            }
                        }

                        var output = Task.Factory.StartNew(() =>
                        {
                            ParallelOptions po = new ParallelOptions
                            {
                                MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                            }; Parallel.Invoke(() =>
                            {
                                object ooo = new object();
                                lock (ooo)
                                {


                                    if (y + 1 < he)
                                    {
                                        if (y - 1 >= 0)
                                        {
                                            if (x + 1 < he)
                                            {
                                                if (x - 1 >= 0)
                                                {
                                                    if (((Ab[x + 1, y]) && (Ab[x + 1, y])) || ((!Ab[x, y + 1]) || (!Ab[x, y - 1])))
                                                        return;
                                                }
                                            }
                                        }
                                    }
                                    if (x + 1 < wi)
                                    {
                                        if (Ab[x + 1, y])
                                        {
                                            var H = Task.Factory.StartNew(() => Is = Is && HollowCountreImageCommmonXYRigthX(ref Im, x + 1, y, wi, he, X, Y, Ab, ref IsOut1));
                                        }
                                    }

                                }
                            }, () =>
                            {
                                object oioo = new object();
                                lock (oioo)
                                {
                                    if (y + 1 < he)
                                    {
                                        if (y - 1 >= 0)
                                        {
                                            if (x + 1 < he)
                                            {
                                                if (x - 1 >= 0)
                                                {
                                                    if (((Ab[x + 1, y]) && (Ab[x + 1, y])) || ((!Ab[x, y + 1]) || (!Ab[x, y - 1])))
                                                        return;
                                                }
                                            }
                                        }
                                    }


                                    if (x - 1 >= 0)
                                    {
                                        if (Ab[x - 1, y])
                                        {
                                            var H = Task.Factory.StartNew(() => Is = Is && HollowCountreImageCommmonXYLeftX(ref Im, x - 1, y, wi, he, X, Y, Ab, ref IsOut2));
                                        }
                                    }

                                }
                            }, () =>
                            {
                                object pooo = new object();
                                lock (pooo)
                                {
                                    if (x + 1 < he)
                                    {
                                        if (x - 1 >= 0)
                                        {
                                            if (y + 1 < he)
                                            {
                                                if (y - 1 >= 0)
                                                {
                                                    if (((Ab[x, y + 1]) && (Ab[x, y - 1])) || (((!Ab[x + 1, y]) || (!Ab[x + 1, y]))))
                                                        return;
                                                }
                                            }
                                        }
                                    }
                                    if (y + 1 < he)
                                    {
                                        if (Ab[x, y + 1])
                                        {
                                            var H = Task.Factory.StartNew(() => Is = Is && HollowCountreImageCommmonXYUpY(ref Im, x, y + 1, wi, he, X, Y, Ab, ref IsOut3));
                                        }
                                    }

                                }
                            }, () =>
                            {
                                object nooo = new object();
                                lock (nooo)
                                {
                                    if (x + 1 < he)
                                    {
                                        if (x - 1 >= 0)
                                        {
                                            if (y + 1 < he)
                                            {
                                                if (y - 1 >= 0)
                                                {
                                                    if (((Ab[x, y + 1]) && (Ab[x, y - 1])) || (((!Ab[x + 1, y]) || (!Ab[x + 1, y]))))
                                                        return;
                                                }
                                            }
                                        }
                                    }
                                    if (y - 1 >= 0)
                                    {
                                        if (Ab[x, y - 1])
                                        {
                                            var H = Task.Factory.StartNew(() => Is = Is && HollowCountreImageCommmonXYDowwnY(ref Im, x, y - 1, wi, he, X, Y, Ab, ref IsOut4));
                                        }
                                    }

                                }
                            });
                        });
                        output.Wait();
                        object oo = new object();
                        lock (oo)
                        {
                            if (Is)
                            {
                                Im.SetPixel(X, Y, Color.White);
                                Img = Im;
                                Hollowed = true;
                                return false;
                            }
                        }
                    }

                }
            }
            catch (Exception t)
            {
                Img = Im;
                //MessageBox.Show(t.ToString());
                return false;
            }
            Img = Im;
            return true;
        }


        private bool HollowCountreImageCommmonXYRigthX(ref Bitmap Img, int x, int y, int wi, int he, int X, int Y, bool[,] Ab, ref bool IsOut)
        {
            Bitmap Im = Img;
            bool Is = false;
            try
            {
                if (!(x >= 0 && y >= 0 && x < wi && y < he))
                    return false;
                object o = new object();
                lock (o)
                {
                    if ((Im.GetPixel(X, Y).ToArgb() == Color.Black.ToArgb()))
                    {
                        if ((x != X && y != Y))
                        {
                            if ((Im.GetPixel(x, y).ToArgb() == Color.Black.ToArgb()))
                            {
                                IsOut = true;
                                return true;
                            }
                        }
                        if (IsOut)
                            return IsOut;
                        object ooo = new object();
                        lock (ooo)
                        {
                            if (x + 1 < wi)
                            {
                                if (Ab[x + 1, y])
                                {
                                    var H = Task.Factory.StartNew(() => Is = Is || HollowCountreImageCommmonXYRigthX(ref Im, x + 1, y, wi, he, X, Y, Ab, ref Is));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception t)
            {
                Img = Im;
                
                return false;
            }
            Img = Im;
            return Is;
        }
        private bool HollowCountreImageCommmonXYLeftX(ref Bitmap Img, int x, int y, int wi, int he, int X, int Y, bool[,] Ab, ref bool IsOut)
        {
            Bitmap Im = Img;
            bool Is = false;
            try
            {
                if (!(x >= 0 && y >= 0 && x < wi && y < he))
                    return false;
                object o = new object();
                lock (o)
                {
                    if ((Im.GetPixel(X, Y).ToArgb() == Color.Black.ToArgb()))
                    {
                        if ((x != X && y != Y))
                        {
                            if ((Im.GetPixel(x, y).ToArgb() == Color.Black.ToArgb()))
                            {
                                IsOut = true;
                                return true;
                            }
                        }

                        if (IsOut)
                            return IsOut;
                        object oioo = new object();
                        lock (oioo)
                        {
                            if (x - 1 >= 0)
                            {
                                if (Ab[x - 1, y])
                                {
                                    var H = Task.Factory.StartNew(() => Is = Is || HollowCountreImageCommmonXYLeftX(ref Im, x - 1, y, wi, he, X, Y, Ab, ref Is));
                                }
                            }
                        }
                    }
                    object oo = new object();
                    lock (oo)
                    {
                    }
                }
            }
            catch (Exception t)
            {
                Img = Im;
                
                return false;
            }
            Img = Im;
            return Is;
        }
        private bool HollowCountreImageCommmonXYUpY(ref Bitmap Img, int x, int y, int wi, int he, int X, int Y, bool[,] Ab, ref bool IsOut)
        {
             Bitmap Im = Img;
            bool Is = false;
            try
            {
                if (!(x >= 0 && y >= 0 && x < wi && y < he))
                    return false;
                object o = new object();
                lock (o)
                {
                    if ((Im.GetPixel(X, Y).ToArgb() == Color.Black.ToArgb()))
                    {
                        if ((x != X && y != Y))
                        {
                            if ((Im.GetPixel(x, y).ToArgb() == Color.Black.ToArgb()))
                            {
                                IsOut = true;
                                return true;
                            }
                        }
                        if (IsOut)
                            return IsOut;

                        object pooo = new object();
                        lock (pooo)
                        {
                            if (y + 1 < he)
                            {
                                if (Ab[x, y + 1])
                                {
                                    var H = Task.Factory.StartNew(() => Is = Is || HollowCountreImageCommmonXYUpY(ref Im, x, y - 1, wi, he, X, Y, Ab, ref Is));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception t)
            {
                Img = Im;
                
                return false;
            }
            Img = Im;
            return Is;
        }
        private bool HollowCountreImageCommmonXYDowwnY(ref Bitmap Img, int x, int y, int wi, int he, int X, int Y, bool[,] Ab, ref bool IsOut)
        {     Bitmap Im = Img;
            bool Is = false;
            try
            {
                if (!(x >= 0 && y >= 0 && x < wi && y < he))
                    return false;
                object o = new object();
                lock (o)
                {
                    if ((Im.GetPixel(X, Y).ToArgb() == Color.Black.ToArgb()))
                    {
                        if ((x != X && y != Y))
                        {
                            if ((Im.GetPixel(x, y).ToArgb() == Color.Black.ToArgb()))
                            {
                                IsOut = true;
                                return true;
                            }
                        }
                        if (IsOut)
                            return IsOut;

                        object pooo = new object();
                        lock (pooo)
                        {
                            if (y + 1 < he)
                            {
                                if (Ab[x, y + 1])
                                {
                                    var H = Task.Factory.StartNew(() => Is = Is || HollowCountreImageCommmonXYDowwnY(ref Im, x, y + 1, wi, he, X, Y, Ab, ref Is));
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception t)
            {
                Img = Im;
                
                return false;
            }
            Img = Im;
            return Is;
        }
        //store all strings list to proper  images themselves list
        public bool ConvertAllStringToImage(MainForm d)
        {
            try
            {
                bool Do = false;
                Do = CreateString();
                //when is not ok
                
                {
                    Do = true;
                }
                //when existence os string list and empty od image list
                if (Do && KeyboardAllImage.Count == 0)
                {
                    //clear
                    KeyboardAllImage.Clear();
                    //for all lists items
                    
                    //{
                    for (int i = 0; i < KeyboardAllStrings.Count; i++)
                    {
                        if (KeyboardAllStrings[i] != " ")
                        {
                            //For all font prototype
                            if (fonts.Count > 0)
                            {
                                //Do literal Database for All fonts
                                
                                //{
                                for (int h = 0; h < fonts.Count; h++)
                                {   //proper empty image coinstruction object
                                    Bitmap Temp = new Bitmap(100, 100);
                                    //initate new root image empty
                                    //create proper image graphics
                                    Graphics e = Graphics.FromImage(Temp);
                                    e.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    //Draw fill white image
                                    e.FillRectangle(Brushes.White, new Rectangle(0, 0, 100, 100));

                                    //draw string
                                    e.DrawString(Convert.ToString(KeyboardAllStrings[i]), new Font(Convert.ToString(fonts[h].Substring(fonts[h].IndexOf("=") + 1, fonts[h].IndexOf(",") - (fonts[h].IndexOf("=") + 1))), (float)((Width + Height))
                                                                                          , FontStyle.Bold, GraphicsUnit.Point), new SolidBrush(Color.Black), new Rectangle(0, 0, 100, 100));
                                    //retrive min and max of tow X and Y
                                    int MiX = MinX(Temp), MiY = MinY(Temp), MaX = MaxX(Temp), MaY = MaxY(Temp);
                                    int MxM = (MaX - MiX) / 2;
                                    int MyM = (MaY - MiY) / 2;
                                    int Mx = MxM * 2;
                                    int My = MyM * 2;
                                    Bitmap Te = null;
                                    e.Dispose();
                                    if ((MaX - MiX) < (MaY - MiY))
                                    {
                                        if (MiX < MaX && MiY < MaY)
                                        {
                                            //crop to proper space
                                            Te = cropImage(Temp, new Rectangle(MiX, MiY, MaY - MiY, MaY - MiY));
                                        }
                                        else
                                        {
                                            Te = cropImage(Temp, new Rectangle(0, 0, Temp.Width, Temp.Height));
                                        }
                                    }
                                    else
                                    {
                                        if (MiX < MaX && MiY < MaY)
                                        {
                                            //crop to proper space
                                            Te = cropImage(Temp, new Rectangle(MiX, MiY, MaX - MiX, MaX - MiX));
                                        }
                                        else
                                        {
                                            Te = cropImage(Temp, new Rectangle(0, 0, Temp.Width, Temp.Height));
                                        }
                                    }
                                    e = Graphics.FromImage(Te);
                                    e.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    bool[,] TemB = new bool[Width, Height];
                                    for (int k = 0; k < Width; k++)
                                    {
                                        for (int p = 0; p < Height; p++)
                                        {
                                            object o = new object();
                                            lock (o)
                                            {  
                                                if ((Te.GetPixel(k, p).ToArgb() == Color.Black.ToArgb()))
                                                {
                                                    TemB[k, p] = true;
                                                }
                                                else
                                                {
                                                    TemB[k, p] = false;
                                                }
                                            }
                                        }
                                    }
                                    e.Dispose();
                                    Do = HollowCountreImageCommmon(ref Te, TemB);
                                    if (!Do)
                                    {
                                        MessageBox.Show("Hollowed Fatal Error");
                                        return false;
                                    }
                                    e = Graphics.FromImage(Te);
                                    e.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    //Add
                                    
                                    //create proper conjunction matrix
                                    bool[,] Tem = new bool[Width, Height];
                                    for (int k = 0; k < Width; k++)
                                    {
                                        for (int p = 0; p < Height; p++)
                                        {
                                            object o = new object();
                                            lock (o)
                                            {
                                                
                                                if ((Te.GetPixel(k, p).ToArgb() == Color.Black.ToArgb()))
                                                {
                                                    Tem[k, p] = true;
                                                }
                                                else
                                                {
                                                    Tem[k, p] = false;
                                                }
                                            }
                                        }
                                    }
                                    //Add
                                    e.Dispose();
                                    KeyboardAllImage.Add(Te);
                                    KeyboardAllConjunctionMatrix.Add(Tem);
                                    KeyboardAllStringsWithfont.Add(KeyboardAllStrings[i]);
                                    Temp.Dispose();
                                    
                                }
                            }
                            else//When font not installed
                            {
                                //proper empty image coinstruction object
                                Bitmap Temp = new Bitmap(100, 100);
                                //initate new root image empty
                                //create proper image graphics
                                Graphics e = Graphics.FromImage(Temp);
                                e.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                //Draw fill white image
                                e.FillRectangle(Brushes.White, new Rectangle(0, 0, 100, 100));
                                //draw string
                                e.DrawString(Convert.ToString(KeyboardAllStrings[i]), new Font(Convert.ToString(fonts[0].Substring(fonts[0].IndexOf("=") + 1, fonts[0].IndexOf(",") - (fonts[0].IndexOf("=") + 1))), (float)((Width + Height))
                                                                                      , FontStyle.Bold, GraphicsUnit.Point), new SolidBrush(Color.Black), new Rectangle(0, 0, 100, 100));
                                //retrive min and max of tow X and Y
                                int MiX = MinX(Temp), MiY = MinY(Temp), MaX = MaxX(Temp), MaY = MaxY(Temp);
                                int MxM = (MaX - MiX) / 2;
                                int MyM = (MaY - MiY) / 2;
                                int Mx = MxM * 2;
                                int My = MyM * 2;
                                Bitmap Te = null;
                                e.Dispose();
                                if ((MaX - MiX) < (MaY - MiY))
                                {
                                    if (MiX < MaX && MiY < MaY)
                                    {
                                        //crop to proper space
                                        Te = cropImage(Temp, new Rectangle(MiX, MiY, MaY - MiY, MaY - MiY));
                                    }
                                    else
                                    {
                                        Te = cropImage(Temp, new Rectangle(0, 0, Temp.Width, Temp.Height));
                                    }
                                }
                                else
                                {
                                    if (MiX < MaX && MiY < MaY)
                                    {
                                        //crop to proper space
                                        Te = cropImage(Temp, new Rectangle(MiX, MiY, MaX - MiX, MaX - MiX));
                                    }
                                    else
                                    {
                                        Te = cropImage(Temp, new Rectangle(0, 0, Temp.Width, Temp.Height));
                                    }
                                }
                                e.Dispose();
                                e = Graphics.FromImage(Te);
                                e.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                bool[,] TemB = new bool[Width, Height];
                                for (int k = 0; k < Width; k++)
                                {
                                    for (int p = 0; p < Height; p++)
                                    {
                                        object o = new object();
                                        lock (o)
                                        { 
                                            if ((Te.GetPixel(k, p).ToArgb() == Color.Black.ToArgb()))
                                            {
                                                TemB[k, p] = true;
                                            }
                                            else
                                            {
                                                TemB[k, p] = false;
                                            }
                                        }
                                    }
                                }
                                e.Dispose();
                                Do = HollowCountreImageCommmon(ref Te, TemB);
                                if (!Do)
                                {
                                    MessageBox.Show("Hollowed Fatal Error");
                                    return false;
                                }
                                //Add
                                
                                //create proper conjunction matrix
                                e.Dispose();
                                e = Graphics.FromImage(Te);
                                e.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                bool[,] Tem = new bool[Width, Height];
                                for (int k = 0; k < Width; k++)
                                {
                                    for (int p = 0; p < Height; p++)
                                    {
                                        object o = new object();
                                        lock (o)
                                        {    
                                            if ((Te.GetPixel(k, p).ToArgb() == Color.Black.ToArgb()))
                                            {
                                                Tem[k, p] = true;
                                            }
                                            else
                                            {
                                                Tem[k, p] = false;
                                            }
                                        }
                                    }
                                }
                                e.Dispose();
                                KeyboardAllImage.Add(Te);
                                KeyboardAllConjunctionMatrix.Add(Tem);
                                KeyboardAllStringsWithfont.Add(KeyboardAllStrings[i]);
                                Temp.Dispose();
                            }
                        }
                        else
                        {
                            Bitmap Temp = new Bitmap(Width, Height);
                            //initate new root image empty
                            //create proper image graphics
                            Graphics e = Graphics.FromImage(Temp);
                            //Draw fill white image
                            e.FillRectangle(Brushes.White, new Rectangle(0, 0, Width, Height));
                            e.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            e.Dispose();
                            bool[,] Tem = new bool[Width, Height];
                            for (int k = 0; k < Width; k++)
                            {
                                for (int p = 0; p < Height; p++)
                                {
                                    object o = new object();
                                    lock (o)
                                    {
                                        
                                            Tem[k, p] = false;
                                        
                                    }
                                }
                            }
                            KeyboardAllImage.Add(Temp);
                            KeyboardAllConjunctionMatrix.Add(Tem);
                            KeyboardAllStringsWithfont.Add(KeyboardAllStrings[i]);
                       
                        }
                    }
                    
                    //save all
                    
                    //if (!Do)
                    
                    //else
                    
                }
                //else
                
            }
            catch (Exception t)
            {
                
                //when existence of exeptio return false
                
                return false;
            }
            //return successfulll
            
            return true;
        }
        //Convert image list to conjunction matrix
        public bool ConvertAllTempageToMatrix(List<Bitmap> TempI)
        {
            try
            {
                //when list is empty
                KeyboardAllConjunctionMatrix.Clear();
                if (KeyboardAllConjunctionMatrix.Count == 0)
                {
                    //clear
                    //for all list count
                    
                    //{
                    for (int i = 0; i < TempI.Count; i++)
                    {
                        //matrix boolean object constructor list
                        List<bool[,]> Te = new List<bool[,]>();
                        //boolean object constructor
                        bool[,] Tem = new bool[TempI[i].Width, TempI[i].Height];
                    
                          Graphics e = Graphics.FromImage(TempI[i]);
                        //{
                        for (int k = 0; k < TempI[i].Width; k++)
                        {
                              for (int p = 0; p < TempI[i].Height; p++)
                            {
                                //assigne proper matrix
                                if ((TempI[i].GetPixel(k, p).ToArgb() == Color.Black.ToArgb()))
                                {
                                    Tem[k, p] = true;
                                }
                                else
                                {
                                    Tem[k, p] = false;
                                }
                            }
                        }
                        e.Dispose();
                        //add
                        KeyboardAllImage.Add((Bitmap)TempI[i].Clone());
                        KeyboardAllConjunctionMatrix.Add(Tem);
                     }
                }
                else//othewise return successfull
                {
                    return true;
                }
            }
            catch (Exception t)
            {
                
                //when is exeption return unsuccessfull
                return false;
            }
            //when save is not valid return successfull
            
            //return successfull
            return true;
        }
    }
}

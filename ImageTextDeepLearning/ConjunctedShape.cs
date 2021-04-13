/***********************************************************************************
 * Ramin Edjlal*********************************************************************
 CopyRighted 1398/0802**************************************************************
 TetraShop.Ir***********************************************************************
 https://www.codingdefined.com/2015/04/solved-bitmapclone-out-of-memory.html********
 ***********************************************************************************/
//#pragma warning restore CS0246 // The type or namespace name 'Emgu' could not be found (are you missing a using directive or an assembly reference?)
using ContourAnalysisDemo;
//#pragma warning disable CS0246 // The type or namespace name 'ContourAnalysisNS' could not be found (are you missing a using directive or an assembly reference?)
//#pragma warning restore CS0246 // The type or namespace name 'ContourAnalysisNS' could not be found (are you missing a using directive or an assembly reference?)
//#pragma warning disable CS0246 // The type or namespace name 'Emgu' could not be found (are you missing a using directive or an assembly reference?)
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageTextDeepLearning
{
    //Class for create conjuncted shape
    [Serializable]
    internal class ConjunctedShape
    {
        bool Hollowed = false;
        //initiate global vars
        private readonly int Width = 10, Height = 10;
        private readonly MainForm d = null;
        private readonly int Threashold = 5;
        public Contour<Point> Collection = null;

        public List<Contour<Point>> All = new List<Contour<Point>>();
        public List<Bitmap> AllImage = new List<Bitmap>();
        //Constructor
        public ConjunctedShape(MainForm dd)
        {

            d = dd;
        }

        //Max of list
        private int MaxX(Point[] Tem)
        {
            int te = 0;
            for (int j = 0; j < Tem.Length; j++)
            {
                if (Tem[j].X > te)
                {
                    te = Tem[j].X;
                }
            }
            return te;
        }

        //Max of list
        private int MaxY(Point[] Tem)
        {
            int te = 0;
            for (int j = 0; j < Tem.Length; j++)
            {
                if (Tem[j].Y > te)
                {
                    te = Tem[j].Y;
                }
            }

            return te;
        }

        //Min of list
        private int MinX(Point[] Tem)
        {
            int te = int.MaxValue;

            for (int j = 0; j < Tem.Length; j++)
            {
                if (Tem[j].X < te)
                {
                    te = Tem[j].X;
                }
            }
            return te;
        }

        //Min of list
        private int MinY(Point[] Tem)
        {
            int te = int.MaxValue;
            for (int j = 0; j < Tem.Length; j++)
            {
                if (Tem[j].Y < te)
                {
                    te = Tem[j].Y;
                }
            }
            return te;
        }

        //max of to object
        private int MaxMax(int maxx, int maxy)
        {
            if (maxx < maxy)
            {
                return maxy;
            }

            return maxx;

        }

        //min of tor object
        private int MinMin(int minx, int miny)
        {
            if (minx < miny)
            {
                return minx;
            }

            return miny;

        }

        //Cropping and fitting image to correcttly map
        private Bitmap cropImage(Bitmap img, Rectangle cropArea)
        {
            int X = cropArea.X;
            int Y = cropArea.Y;
            int XX = cropArea.Width;
            int YY = cropArea.Height;




            Bitmap bmp = new Bitmap(Width, Height);
            using (Graphics gph = Graphics.FromImage(bmp))
            {
                gph.DrawImage(img, new Rectangle(0, 0, Width, Height), new Rectangle(X, Y, XX, YY), GraphicsUnit.Pixel);
            }
            return bmp;
        }

        private int ImMinX(Bitmap Im)
        {
            int Mi = -1;
            for (int j = 0; j < Im.Width; j++)
            {
                for (int k = 0; k < Im.Height; k++)
                {

                    if ((Im.GetPixel(j, k).ToArgb() == Color.Black.ToArgb()))
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
        private int ImMinY(Bitmap Im)
        {
            int Mi = -1;
            for (int k = 0; k < Im.Height; k++)
            {
                for (int j = 0; j < Im.Width; j++)
                {

                    if ((Im.GetPixel(j, k).ToArgb() == Color.Black.ToArgb()))
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
        private int ImMaxY(Bitmap Im)
        {
            int Ma = -1;
            for (int k = Im.Height - 1; k >= 0; k--)
            {
                for (int j = 0; j < Im.Width; j++)
                {

                    if ((Im.GetPixel(j, k).ToArgb() == Color.Black.ToArgb()))
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
        private int ImMaxX(Bitmap Im)
        {
            int Ma = -1;
            // ParallelOptions po = new ParallelOptions(); po.MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount; Parallel.For(Im.Width - 1, 0, j =>

            //{
            for (int j = Im.Width - 1; j >= 0; j--)
            {
                //ParallelOptions poo = new ParallelOptions(); poo.MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount; Parallel.For(Im.Height - 1, 0, k =>

                // {
                for (int k = 0; k < Im.Height; k++)
                {

                    if ((Im.GetPixel(j, k).ToArgb() == Color.Black.ToArgb()))
                    {
                        Ma = j;
                        break;
                    }
                }//);
                if (Ma > -1)
                {
                    break;
                }
            }//);
            return Ma;

        }
        //Create shape of conjuncted countor poins
        public bool CreateSAhapeFromConjucted(int Wi, int Hei)
        {
            //create shapes at list
            bool Do = ConjunctedShapeCreate(d);

            try
            {
                //when is true
                if (Do)
                {
                    //clear list
                    AllImage.Clear();
                    //for all items
                    for (int i = 0; i < All.Count; i++)
                    {

                        //retrive min and max of tow X and Y
                        Bitmap Temp = new Bitmap(Width, Height);
                        Graphics e = Graphics.FromImage(Temp);
                        //e.FillRectangle(Brushes.White, new Rectangle(0, 0, Mx, Mx));
                        Point[] te = All[i].ToArray();
                        Point[] tec = new Point[All[i].ToArray().Length];
                        int MiX = MinX(te), MiY = MinY(te), MaX = MaxX(te), MaY = MaxY(te);


                        //centeralized
                        int MxM = (MaX - MiX) / 2;
                        int MyM = (MaY - MiY) / 2;
                        int Mx = MxM * 2;
                        int My = MyM * 2;
                        //initate new root image empty
                        if (!(MaX == MiX || MaY == MiY))
                        {
                            if ((MaX - MiX) < (MaY - MiY))
                            {
                                for (int o = 0; o < All[i].ToArray().Length; o++)
                                {
                                    tec[o] = new Point((int)((float)Width * (float)(te[o].X - MiX) / (float)(MaX - MiX)), (int)((float)Height * (float)(te[o].Y - MiY) / (float)(MaY - MiY)));
                                }
                            }
                            else
                            {
                                for (int o = 0; o < All[i].ToArray().Length; o++)
                                {
                                    tec[o] = new Point((int)((float)Width * (float)(te[o].X - MiX) / (float)(MaX - MiX)), (int)((float)Height * (float)(te[o].Y - MiY) / (float)(MaY - MiY)));
                                }
                            }
                            //e.DrawLines(Pens.Black, tec);
                            e.DrawPolygon(new Pen(Color.Black), tec);
                            //, System.Drawing.Drawing2D.FillMode.Alternate
                            e.Dispose();

                            MiX = ImMinX(Temp);
                            MiY = ImMinY(Temp);
                            MaX = ImMaxX(Temp);
                            MaY = ImMaxY(Temp);


                            //centeralized
                            MxM = (MaX - MiX) / 2;
                            MyM = (MaY - MiY) / 2;
                            Mx = MxM * 2;
                            My = MyM * 2;
                            //draw all points
                            if (Mx > My)
                            {
                                Bitmap Te = null;
                                if ((MaX - MiX) < (MaY - MiY))
                                {
                                    if (MiX < MaX && MiY < MaY)
                                    {
                                        //crop to proper space
                                        //Te = cropImage(Temp, new Rectangle(MiX, MiY, MaY - MiY, MaY - MiY));
                                        Te = cropImage(Temp, new Rectangle(0, 0, Temp.Width, Temp.Height));
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
                                        //Te = cropImage(Temp, new Rectangle(MiX, MiY, MaY - MiY, MaY - MiY));
                                        Te = cropImage(Temp, new Rectangle(0, 0, Temp.Width, Temp.Height));
                                    }
                                    else
                                    {
                                        Te = cropImage(Temp, new Rectangle(0, 0, Temp.Width, Temp.Height));

                                    }
                                }       //add image
                                /*bool[,] TemB = new bool[Width, Height];
                                e = Graphics.FromImage(Te);

                                for (int k = 0; k < Width; k++)
                                {
                                    for (int p = 0; p < Height; p++)
                                    {
                                        // Tem[k, p] = Temp.GetPixel(k, p).ToArgb();
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
                                e.Dispose();
                                Do = HollowCountreImageCommmon(ref Te, TemB);
                                if (!Do)
                                {
                                    MessageBox.Show("Hollowed Fatal Error");
                                    return false;
                                }*/

                                // AllImage.Add((Bitmap)Te.Clone());
                                AllImage.Add(Te);
                            }
                            else
                            {
                                Bitmap Te = null;
                                if ((MaX - MiX) < (MaY - MiY))
                                {
                                    if (MiX < MaX && MiY < MaY)
                                    {
                                        //crop to proper space
                                        //Te = cropImage(Temp, new Rectangle(MiX, MiY, MaY - MiY, MaY - MiY));
                                        Te = cropImage(Temp, new Rectangle(0, 0, Temp.Width, Temp.Height));
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
                                        //Te = cropImage(Temp, new Rectangle(MiX, MiY, MaY - MiY, MaY - MiY));
                                        Te = cropImage(Temp, new Rectangle(0, 0, Temp.Width, Temp.Height));
                                    }
                                    else
                                    {
                                        Te = cropImage(Temp, new Rectangle(0, 0, Temp.Width, Temp.Height));

                                    }
                                }
                               /* bool[,] TemB = new bool[Width, Height];
                                 e = Graphics.FromImage(Te);

                                 for (int k = 0; k < Width; k++)
                                 {
                                     for (int p = 0; p < Height; p++)
                                     {
                                         // Tem[k, p] = Temp.GetPixel(k, p).ToArgb();
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
                                 e.Dispose();
                                Do = HollowCountreImageCommmon(ref Te, TemB);
                                 if (!Do)
                                 {
                                     MessageBox.Show("Hollowed Fatal Error");
                                     return false;
                                 }*/
                                // AllImage.Add((Bitmap)Te.Clone());
                                AllImage.Add(Te);
                            }
                        }
                        else
                        {
                            Temp = new Bitmap(Width, Height);
                            e = Graphics.FromImage(Temp);
                            e.FillRectangle(Brushes.White, new Rectangle(0, 0, Width, Height));
                            e.Dispose();
                            AllImage.Add((Bitmap)Temp.Clone());

                        }
                    }

                }
                else
                {
                    //when is not ready return unsuccessfull
                    return false;
                }
            }
            catch (Exception)
            {
                //when existence of exeption return unsuccessfull
                //System.Windows.Forms.MessageBox.Show("Fatual Error!" + t.ToString());

                return false;
            }
            //clear unneed and free memmory
            //All.Clear();
            //Collection.Clear();
            return true;
        }

        //Colorized list of image
        private bool ColorizedCountreImageCommon(List<Bitmap> Im)
        {
            try
            {
                //for all list items
                for (int i = 0; i < Im.Count; i++)
                {
                    //create graphics for current image
                    Graphics e = Graphics.FromImage(Im[i]);
                    //for all image width
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
                                if ((Im[i].GetPixel(j, k).ToArgb() == Color.Black.ToArgb()))
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
                            if ((Im.GetPixel(j, k).ToArgb() == Color.Black.ToArgb()))
                            {
                                Po[0] = new Point(j, k);
                                nu++;
                            }
                        }
                        else//second
                        if (nu == 1)
                        {
                            if ((Im.GetPixel(j, k).ToArgb() == Color.Black.ToArgb()))
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
                //do
                // {
                Hollowed = false;

                var output = Task.Factory.StartNew(() =>
                {
                    // for (int x = 0; x < wi; x++)
                    //{

                    ParallelOptions po = new ParallelOptions
                    {
                        MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                    }; Parallel.For(0, wi, x =>
                    {
                        /// for (int y = 0; y < he; y++)
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
                                    //th.Add(H);
                                    //H.Wait();
                                }
                            }
                        });


                    });
                });
                output.Wait();
                //} while (Hollowed);
                e.Dispose();
                //Parallel.ForEach(th, item => Task.WaitAll(item));
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

                //MessageBox.Show(t.ToString());

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

                //MessageBox.Show(t.ToString());

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

                //MessageBox.Show(t.ToString());

                return false;
            }
            Img = Im;

            return Is;
        }
        private bool HollowCountreImageCommmonXYDowwnY(ref Bitmap Img, int x, int y, int wi, int he, int X, int Y, bool[,] Ab, ref bool IsOut)
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

                //MessageBox.Show(t.ToString());

                return false;
            }
            Img = Im;

            return Is;
        }


        //Create Conjuncted image
        private bool ConjunctedShapeCreate(MainForm d)
        {
            All.Clear();
            //Collection.Clear();
            try
            {
                //get counter points of foreign
                bool flag = true;
                using (List<Contour<Point>>.Enumerator enumerator1 = d.processor.contours.GetEnumerator())
                {
                    //do
                    do
                    {
                        //when is finished break
                        if (!flag)
                        {
                            break;
                        }
                        else
                        {
                            flag = false;
                        }

                        bool flag1 = false, flag2 = false;
                        //while
                        while (true)
                        {
                            //when there is not and is not empty at list
                            /*  if (!All.Contains(Collection) && Collection.Count > 1)
                              {
                                  //add collection
                                  All.Add(Collection);
                                  Collection = new List<Point[]>();

                              }*/
                            //next enumerator
                            flag1 = enumerator1.MoveNext();
                            //when is finished break 
                            if (!flag1)
                            {



                                break;
                            }
                            //current target
                            Contour<Point> current1 = enumerator1.Current;

                            //current points
                            //Point[] PointP1 = current1.ToArray();
                            All.Add(current1);


                            //Collection.Add(current1);
                            flag2 = true;
                        }
                        if (flag2)
                        {
                        }

                        //Collection = new Contour<Point>();
                    } while (true);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


    }
}

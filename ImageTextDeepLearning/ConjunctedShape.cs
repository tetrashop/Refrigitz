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
        public List<Point[]> Collection = new List<Point[]>();

        public List<List<Point[]>> All = new List<List<Point[]>>();
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

        //Found of Min of X
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
            for (int j = Im.Width - 1; j >= 0; j--)
            {
                for (int k = Im.Height - 1; k >= 0; k--)
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
                        for (int j = 0; j < All[i].Count; j++)
                        {
                            //initate constructor object and initate...
                            List<Bitmap> TempAllImage = new List<Bitmap>();



                            Point[] Tem = null;
                            //retrive current item
                            Tem = All[i][j];
                            //retrive min and max of tow X and Y
                            int MiX = MinX(Tem), MiY = MinY(Tem), MaX = MaxX(Tem), MaY = MaxY(Tem);


                            //centeralized
                            int MxM = (MaX - MiX) / 2;
                            int MyM = (MaY - MiY) / 2;
                            int Mx = MxM * 2;
                            int My = MyM * 2;
                            //initate new root image empty

                            PointF[] tec = new PointF[Tem.Length];
                            if ((MaX - MiX) < (MaY - MiY))
                            {
                                for (int o = 0; o < Tem.Length; o++)
                                {
                                    tec[o] = new PointF((Width * (Tem[o].X - MiX) / (float)(MaY - MiY)), Height * (Tem[o].Y - MiY) / (float)(MaY - MiY));
                                }
                            }
                            else
                            {
                                for (int o = 0; o < Tem.Length; o++)
                                {
                                    tec[o] = new PointF((Width * (Tem[o].X - MiX) / (float)(MaX - MiX)), Height * (Tem[o].Y - MiY) / (float)(MaX - MiX));
                                }
                            }
                            //draw all points
                            Bitmap Temp = new Bitmap(Width, Height);

                            //Draw fill white image
                            Graphics e = Graphics.FromImage(Temp);
                            e.FillRectangle(Brushes.White, new Rectangle(0, 0, Width, Height));
                            e.DrawLines(new Pen(Color.Black), tec
                                );
                            //, System.Drawing.Drawing2D.FillMode.Alternate
                            e.Dispose();
                            e.Dispose();
                            bool[,] TemB = new bool[Width, Height];
                            for (int k = 0; k < Width; k++)
                            {
                                for (int p = 0; p < Height; p++)
                                {
                                    // Tem[k, p] = Temp.GetPixel(k, p).ToArgb();
                                    if (!(Temp.GetPixel(k, p).ToArgb() == Color.White.ToArgb()))
                                    {
                                        TemB[k, p] = true;
                                    }
                                    else
                                    {
                                        TemB[k, p] = false;
                                    }
                                }
                            }
                            Do = HollowCountreImageCommmon(ref Temp, TemB);
                            if (!Do)
                            {
                                MessageBox.Show("Hollowed Fatal Error");
                                return false;
                            }
                            //Add

                            /*   MiX = ImMinX(Temp);
                               MiY = ImMinY(Temp);
                               MaX = ImMaxX(Temp);
                               MaY =ImMaxY(Temp );*/


                            Bitmap Te = null;
                            /*if (MiX < MaX && MiY < MaY)
                            {
                                //Rectangle cropArea = new Rectangle(MiX, MiY, MaX, MaY);
                                //crop to proper space
                                Te = cropImage(Temp, new Rectangle(MiXyy MiY, MaX - MiX, MaY - MiY));
                           
                            }
                            else*/
                            Te = Temp;
                             
                                
                            //add image
                            AllImage.Add(Te);
                            //e.Dispose();

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
            All.Clear();
            Collection.Clear();
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

        //Hollow an image
        private bool HollowCountreImageCommmon(ref Bitmap Img, bool[,] Ab)
        {
            try
            {
                int wi = Img.Width;
                int he = Img.Height;
                Bitmap Im = Img;
                List<Task> th = new List<Task>();
                do
                {
                    Hollowed = false;
                    for (int x = 0; x < wi; x++)
                    {
                        for (int y = 0; y < he; y++)
                        {
                            object o = new object();
                            lock (o)
                            {

                                var H = Task.Factory.StartNew(() => HollowCountreImageCommmonXY(ref Im, x, y, wi, he, x, y, Ab));
                                //th.Add(H);
                                H.Wait();

                            }
                        }

                    }
                } while (Hollowed);
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
                                            var H = Task.Factory.StartNew(() => Is = Is && HollowCountreImageCommmonXY(ref Im, x + 1, y, wi, he, X, Y, Ab));
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
                                            var H = Task.Factory.StartNew(() => Is = Is && HollowCountreImageCommmonXY(ref Im, x - 1, y, wi, he, X, Y, Ab));
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
                                            var H = Task.Factory.StartNew(() => Is = Is && HollowCountreImageCommmonXY(ref Im, x, y + 1, wi, he, X, Y, Ab));
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
                                            var H = Task.Factory.StartNew(() => Is = Is && HollowCountreImageCommmonXY(ref Im, x, y - 1, wi, he, X, Y, Ab));

                                        }
                                    }
                                }
                            });
                        });
                        output.Wait();
                    }
                    object oo = new object();
                    lock (oo)
                    {
                        if (Is)
                        {
                            if ((x != X && y != Y))
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


        //Create Conjuncted image
        private bool ConjunctedShapeCreate(MainForm d)
        {
            All.Clear();
            Collection.Clear();
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
                            Point[] PointP1 = current1.ToArray();


                            Collection.Add(PointP1);
                            flag2 = true;
                        }
                        if (flag2)
                        {
                            All.Add(Collection);
                        }

                        Collection = new List<Point[]>();
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

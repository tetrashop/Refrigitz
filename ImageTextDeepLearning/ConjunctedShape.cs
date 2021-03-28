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
using System.Windows.Forms;

namespace ImageTextDeepLearning
{
    //Class for create conjuncted shape
    [Serializable]
    class ConjunctedShape
    {
        //initiate global vars
        int Width = 10, Height = 10;
        MainForm d = null;
        int Threashold = 5;
        public List<Point[]> Collection = new List<Point[]>();

        public List<List<Point[]>> All = new List<List<Point[]>>();
        public List<Bitmap> AllImage = new List<Bitmap>();
        //Constructor
        public ConjunctedShape(MainForm dd)
        {

            d = dd;
        }

        //Max of list
        int MaxX(Point[] Tem)
        {
            int te = 0;
            for (int j = 0; j < Tem.Length; j++)
            {
                if (Tem[j].X > te)
                    te = Tem[j].X;
            }
            return te;
        }
        //Max of list
        int MaxY(Point[] Tem)
        {
            int te = 0;
            for (int j = 0; j < Tem.Length; j++)
            {
                if (Tem[j].Y > te)
                    te = Tem[j].Y;
            }

            return te;
        }
        //Min of list
        int MinX(Point[] Tem)
        {
            int te = Int32.MaxValue;

            for (int j = 0; j < Tem.Length; j++)
            {
                if (Tem[j].X < te)
                    te = Tem[j].X;
            }
            return te;
        }
        //Min of list
        int MinY(Point[] Tem)
        {
            int te = Int32.MaxValue;
            for (int j = 0; j < Tem.Length; j++)
            {
                if (Tem[j].Y < te)
                    te = Tem[j].Y;
            }
            return te;
        }
        //max of to object
        int MaxMax(int maxx, int maxy)
        {
            if (maxx < maxy)
                return maxy;
            return maxx;

        }
        //min of tor object
        int MinMin(int minx, int miny)
        {
            if (minx < miny)
                return minx;
            return miny;

        }
        //Cropping and fitting image to correcttly map
        Bitmap cropImage(Bitmap img, Rectangle cropArea)
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
        int ImMinX(Bitmap Im)
        {
            int Mi = -1;
            for (int j = 0; j < Im.Width; j++)
            {
                for (int k = 0; k < Im.Height; k++)
                {

                    if (!(Im.GetPixel(j, k).A == 255 && Im.GetPixel(j, k).R == 255 && Im.GetPixel(j, k).B == 255 && Im.GetPixel(j, k).G == 255))
                    {
                        Mi = j;
                        break;
                    }
                }
                if (Mi > -1)
                    break;
            }
            return Mi;

        }
        //Founf Min of Y
        int ImMinY(Bitmap Im)
        {
            int Mi = -1;
            for (int k = 0; k < Im.Height; k++)
            {
                for (int j = 0; j < Im.Width; j++)
                {

                    if (!(Im.GetPixel(j, k).A == 255 && Im.GetPixel(j, k).R == 255 && Im.GetPixel(j, k).B == 255 && Im.GetPixel(j, k).G == 255))
                    {
                        Mi = k;
                        break;
                    }
                }
                if (Mi > -1)
                    break;
            }
            return Mi;

        }
        //Found of Max Of Y
        int ImMaxY(Bitmap Im)
        {
            int Ma = -1;
            for (int j = 0; j < Im.Width; j++)
            {
                for (int k = Im.Height - 1; k >= 0; k--)
                {
                    if (!(Im.GetPixel(j, k).A == 255 && Im.GetPixel(j, k).R == 255 && Im.GetPixel(j, k).B == 255 && Im.GetPixel(j, k).G == 255))
                    {
                        Ma = k;
                        break;
                    }
                }
                if (Ma > -1)
                    break;
            }
            return Ma;

        }
        //Found of Max of X
        int ImMaxX(Bitmap Im)
        {
            int Ma = -1;
            for (int k = Im.Height - 1; k >= 0; k--)
            {
                for (int j = Im.Width - 1; j >= 0; j--)
                {
                    if (!(Im.GetPixel(j, k).A == 255 && Im.GetPixel(j, k).R == 255 && Im.GetPixel(j, k).B == 255 && Im.GetPixel(j, k).G == 255))
                    {
                        Ma = j;
                        break;
                    }
                }
                if (Ma > -1)
                    break;
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
                            Bitmap Temp = new Bitmap(MaX, MaY);

                            //Draw fill white image
                            Graphics e = Graphics.FromImage(Temp);
                            e.FillRectangle(Brushes.White, new Rectangle(0, 0, MaX, MaY));

                            PointF[] tec = new PointF[Tem.Length];
                            for (int o = 0; o < Tem.Length; o++)
                                tec[o] = new PointF(Tem[o].X, Tem[o].Y);
                            //draw all points
                            e.DrawPolygon(new Pen(Brushes.Black), tec
                                );
                                //, System.Drawing.Drawing2D.FillMode.Alternate
                              e.Dispose();
                            Do = ColorizedCountreImageCommmon(ref Temp);
                            if (!Do)
                            {
                                MessageBox.Show("Coloriezed Fatal Error");
                                return false;
                            }

                            /*   MiX = ImMinX(Temp);
                               MiY = ImMinY(Temp);
                               MaX = ImMaxX(Temp);
                               MaY =ImMaxY(Temp );*/


                            Bitmap Te = null;
                            if (MiX < MaX && MiY < MaY)
                            {
                                //Rectangle cropArea = new Rectangle(MiX, MiY, MaX, MaY);
                                //crop to proper space
                                Te = cropImage(Temp, new Rectangle(MiX, MiY, MaX - MiX, MaY - MiY));
                           
                            }
                            else
                                Te = Temp;
                            /* Do = ColorizedCountreImageCommmon(ref Te);
                                if (!Do)
                                {
                                    MessageBox.Show("Coloriezed Fatal Error");
                                    return false;
                                }
                                */
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
            catch (Exception t)
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
        bool ColorizedCountreImageCommon(List<Bitmap> Im)
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
                                if (!(Im[i].GetPixel(j, k).A == 255 && Im[i].GetPixel(j, k).R == 255 && Im[i].GetPixel(j, k).B == 255 && Im[i].GetPixel(j, k).G == 255))
                                {
                                    Po[0] = new PointF(j, k);
                                    nu++;
                                }
                            }
                            else//second
                            if (nu == 1)
                            {
                                if (!(Im[i].GetPixel(j, k).A == 255 && Im[i].GetPixel(j, k).R == 255 && Im[i].GetPixel(j, k).B == 255 && Im[i].GetPixel(j, k).G == 255))
                                {
                                    Po[1] = new PointF(j, k);
                                    nu++;
                                    //draw linnes and free var to coninue
                                    e.DrawLines(Pens.Black, Po);
                                    nu = 0;
                                    break;
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
        bool ColorizedCountreImageCommmon(ref Bitmap Im)
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
                            if (!(Im.GetPixel(j, k).A == 255 && Im.GetPixel(j, k).R == 255 && Im.GetPixel(j, k).B == 255 && Im.GetPixel(j, k).G == 255))
                            {
                                Po[0] = new Point(j, k);
                                nu++;
                            }
                        }
                        else//second
                        if (nu == 1)
                        {
                            if (!(Im.GetPixel(j, k).A == 255 && Im.GetPixel(j, k).R == 255 && Im.GetPixel(j, k).B == 255 && Im.GetPixel(j, k).G == 255))
                            {
                                Po[1] = new Point(j, k);
                                nu++;
                                //draw linnes and free var to coninue
                                e.DrawLines(Pens.Black, Po);
                                nu = 0;
                                break;
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
        //Create Conjuncted image
        bool ConjunctedShapeCreate(MainForm d)
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
                            flag = false;

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
                            All.Add(Collection);
                        Collection = new List<Point[]>();
                    } while (true);
                }
            }
            catch (Exception t)
            {
                return false;
            }
            return true;
        }


    }
}

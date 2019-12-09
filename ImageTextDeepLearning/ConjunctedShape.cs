/***********************************************************************************
 * Ramin Edjlal*********************************************************************
 CopyRighted 1398/0802**************************************************************
 TetraShop.Ir***********************************************************************
 https://www.codingdefined.com/2015/04/solved-bitmapclone-out-of-memory.html********
 ***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ContourAnalysisNS;
using Emgu.CV;
using ContourAnalysisDemo;
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
                            int MxM = (MaX + MiX) / 2;
                            int MyM = (MiY + MaY) / 2;
                            int Mx = MxM * 2;
                            int My = MyM * 2;
                            //initate new root image empty
                            Bitmap Temp = new Bitmap(Mx, My);

                            //Draw fill white image
                            Graphics e = Graphics.FromImage(Temp);
                            e.FillRectangle(Brushes.White, new Rectangle(0, 0, Mx, My));



                            //draw all points
                            e.FillPolygon(Brushes.Black, Tem,System.Drawing.Drawing2D.FillMode.Alternate);


                            //Rectangle cropArea = new Rectangle(MiX, MiY, MaX, MaY);
                            //crop to proper space
                            Bitmap Te = cropImage(Temp, new Rectangle(MiX, MiY, MaX - MiX, MaY - MiY));


                           /* Do = ColorizedCountreImageCommmon(ref Te);
                            if (!Do)
                            {
                                MessageBox.Show("Coloriezed Fatal Error");
                                return false;
                            }
                            */
                            //add image
                            AllImage.Add(Te);
                            e.Dispose();

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
                        if(flag2)                         
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

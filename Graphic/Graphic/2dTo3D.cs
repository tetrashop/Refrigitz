/*tetrashop.ir 1399/11/24 iran urmia
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace WindowsApplication1
{
    class _2dTo3D
    {
        Image a;
        public Image ar;
        //size
        int[] b = new int[3];
        int[,,] t;// zeros(b(1,1),b(1,2),3);
        int[,,] rr;// rr=zeros(b(1,1),b(1,2),3);
        int[,,] f;//     f=zeros(b(1,1),b(1,2),3);
        float[,,] c;
        public float[,,] e;
        int fg = 2;
        int minr = int.MaxValue;
        int minteta = int.MaxValue;
        int minfi = int.MaxValue;
        int maxr = int.MinValue;
        int maxteta = int.MinValue;
        int maxfi = int.MinValue;
        double[] cart2sph(float i, float j, float k)
        {
            double[] s = new double[3];
            s[2] = Math.Sqrt(i * i + j * j + k * k);
            if (s[2] == 0)
            {
                s[0] = 0;
                s[1] = 0;
            }
            else
            {
                s[0] = Math.Acos(k / s[2]);
                s[1] = Math.Atan2(j, i);
            }
            return s;
        }
        void Threaadcal(int i, int j, int k, int ii, int jj)
        {
            lock (c)
            {
                float dr = 0;
                double[] s = new double[3];
                //[teta, fi, r] = cart2sph(i, j, 0);
                s = cart2sph(i, j, 0);
                t[i, j, k] = (int)Math.Round((double)(s[0] * 180.0 / 3.1415 - (double)minteta + 1));
                rr[i, j, k] = (int)Math.Round((double)(s[1] * 180.0 / 3.1415 - (double)minr + 1));
                f[i, j, k] = (int)Math.Round((double)(s[2] - (double)minfi + 1.00));
                dr = (float)Math.Round(((-1 * (i + 1)) / (Math.Sqrt(Math.Pow(i + 1, 2) + Math.Pow(j + 1, 2) + Math.Pow(k + 1, 2)))) * 3 * 300 / (1 + System.Convert.ToInt32(GetK(a, i, j, 0)) + System.Convert.ToInt32(GetK(a, i, j, 1)) + System.Convert.ToInt32(GetK(a, i, j, 2))));
                if ((dr + maxr - minr < maxr - minr)// && (t[i, j, k] + 2 < maxteta - minteta) && (t[i, j, k] - 2 > minteta)
                    )
                {
                    try
                    {
                        if ((ii + jj) % 2 == 0)
                            c[ii, jj - 1, k] = (float)(System.Convert.ToInt32(GetK(a, i, j, k)) + dr);
                        else
                            c[ii, jj + 1, k] = (float)(System.Convert.ToInt32(GetK(a, i, j, k)) + dr);
                    }
                    catch (Exception t)
                    {

                    }
                }
            }
        }
        void Threaadfetch(int i, int j, int k, int ii, int jj)
        {
            lock (e)
            {
                lock (c)
                {
                    try
                    {
                        e[(int)(ii * b[0] + i), (int)(jj * b[1] + j), k] = c[(int)(ii * (maxr - minr) + rr[i, j, k]), (int)(jj * (maxteta - minteta) + t[i, j, k]), k];
                    }
                    catch (Exception t)
                    {

                    }
                }
            }
        }
        void Threaaddraw(int i, int j, ref Graphics g, ref Image ar)
        {
            lock (e)
            {
                lock (ar)
                {
                    lock (g)
                    {
                        try
                        {
                            (ar as Bitmap).SetPixel(i, j, Color.FromArgb((int)(e[i, j, 0]), (int)(e[i, j, 1]), (int)(e[i, j, 2])));
                        g.DrawImage(ar, 0, 0, ar.Width, ar.Height);
                        g.Save();
                        }
                        catch (Exception t)
                        {

                        }
                    }
                }
            }
        }
        void ContoObject()
        {
            int r = 0;
            int teta = 0;
            int fi = 0;
            c = new float[(int)((maxr - minr + 1) * fg + maxr), (int)Math.Round((double)(maxteta - minteta + 1) * fg + maxteta + 1), 3];
            t = new int[b[0], b[1], 3];
            rr = new int[b[0], b[1], 3];
            f = new int[b[0], b[1], 3];
            int v = 0;
            int n = 0;
            int q = 0;
            int robeta = 0;

            float rb = (float)0.20;// pixel;
            float Z = (float)0.100;// distance of eye form screen cm;
            float ra = 0;//varabale;
            var output = Task.Factory.StartNew(() =>
           {

               ParallelOptions po = new ParallelOptions(); po.MaxDegreeOfParallelism = 2; Parallel.For(0, fg, ii =>
               {

                   ParallelOptions poo = new ParallelOptions(); poo.MaxDegreeOfParallelism = 2; Parallel.For(0, fg, jj =>
                   {
                        //float[,,] cc = new float[(maxr - minr + 1), (maxteta - minteta + 1), 3];
                        ParallelOptions ppoio = new ParallelOptions(); ppoio.MaxDegreeOfParallelism = 2; Parallel.For(0, b[0], i =>
                 {
                     ParallelOptions pooo = new ParallelOptions(); pooo.MaxDegreeOfParallelism = 2; Parallel.For(0, b[1], j =>
                     {
                         ParallelOptions poooo = new ParallelOptions(); poooo.MaxDegreeOfParallelism = 2; Parallel.For(0, 3, k =>
                           {
                               var output1 = Task.Factory.StartNew(() => Threaadcal(i, j, k, (int)((maxr - minr + 1) * ii + maxr), (int)Math.Round((double)(maxteta - minteta + 1) * jj + maxteta + 1)));
                           });


                     });
                 });
                   });
               });
           });
            output.Wait();
        }
        byte GetK(Image a, int i, int j, int k)
        {
            lock (a)
            {
                if (k == 0)
                    return (a as Bitmap).GetPixel(i, j).R;
                if (k == 1)
                    return (a as Bitmap).GetPixel(i, j).G;

                return (a as Bitmap).GetPixel(i, j).B;

            }
        }
        void ConvTo3D()
        {
            int r = 0;
            int teta = 0;
            int fi = 0;
            e = new float[(int)(b[0] * fg), (int)((b[1]) * fg), 3];

            var output = Task.Factory.StartNew(() =>
            {
                ParallelOptions pop = new ParallelOptions(); pop.MaxDegreeOfParallelism = 2; Parallel.For(0, fg, ii =>
                {
                    ParallelOptions popp = new ParallelOptions(); popp.MaxDegreeOfParallelism = 2; Parallel.For(0, fg, jj =>
                    {
                        ParallelOptions poo = new ParallelOptions(); poo.MaxDegreeOfParallelism = 2; Parallel.For(0, b[0], i =>
                        {
                            ParallelOptions pon = new ParallelOptions(); pon.MaxDegreeOfParallelism = 2; Parallel.For(0, b[1], j =>
                            {
                                ParallelOptions pob = new ParallelOptions(); pob.MaxDegreeOfParallelism = 2; Parallel.For(0, 3, k =>
                                {
                                    var output1 = Task.Factory.StartNew(() => Threaadfetch(i, j, k, ii, jj));

                                });
                            });
                        });
                    });
                });
            });
            output.Wait();
        }
        void Initiate()
        {
            int r = 0;
            int teta = 0;
            int fi = 0;
            b[0] = a.Width;
            b[1] = a.Height;
            b[2] = 3;
            t = new int[b[0], b[1], 3];
            rr = new int[b[0], b[1], 3];
            f = new int[b[0], b[1], 3];

            for (int i = 0; i < b[0]; i++)
            {
                for (int j = 0; j < b[1]; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        double[] s = new double[3];
                        //[teta, fi, r] = cart2sph(i, j, 0);
                        s = cart2sph(i, j, 0);
                        s[2] = Math.Round(s[2]);
                        s[0] = Math.Round(s[0] * 180.0 / 3.1415);
                        s[1] = Math.Round(s[1] * 180.0 / 3.1415);
                        teta = (int)s[0];
                        fi = (int)s[1];
                        r = (int)s[2];
                        if (minr > r)
                            minr = r;

                        if (maxr < r)
                            maxr = r;

                        if (minfi > fi)
                            minfi = fi;

                        if (maxfi < fi)
                            maxfi = fi;

                        if (minteta > teta)
                            minteta = teta;

                        if (maxteta < teta)
                            maxteta = teta;

                    }


                }
            }
        }
        float[,,] uitn8(float[,,] p, int ii, int jj, int kk)
        {
            float min = float.MaxValue, max = float.MinValue;
            for (int i = 0; i < ii; i++)
            {
                for (int j = 0; j < jj; j++)
                {
                    for (int k = 0; k < kk; k++)
                    {
                        if (p[i, j, k] > max)
                            max = p[i, j, k];
                        if (p[i, j, k] < min)
                            min = p[i, j, k];
                    }
                }

            }
            float q = min;
            for (int i = 0; i < ii; i++)
            {
                for (int j = 0; j < jj; j++)
                {
                    for (int k = 0; k < kk; k++)
                    {
                        p[i, j, k] -= q;
                    }
                }

            }

            min = float.MaxValue; max = float.MinValue;
            for (int i = 0; i < ii; i++)
            {
                for (int j = 0; j < jj; j++)
                {
                    for (int k = 0; k < kk; k++)
                    {
                        if (p[i, j, k] > max)
                            max = p[i, j, k];
                        if (p[i, j, k] < min)
                            min = p[i, j, k];
                    }
                }

            }
            q = (float)255.0 / (max - min);
            for (int i = 0; i < ii; i++)
            {
                for (int j = 0; j < jj; j++)
                {
                    for (int k = 0; k < kk; k++)
                    {
                        p[i, j, k] *= q;
                    }
                }

            }
            return p;
        }
        //convert 2d image to 3d;
        public _2dTo3D(string ass)
        {
            a = Image.FromFile(ass);
            Initiate();
            MessageBox.Show("Initiate pass!");
            ContoObject();
            MessageBox.Show("ContoObject pass!");
            ConvTo3D();
            MessageBox.Show("ConvTo3D pass!");
            e = uitn8(e, (int)(b[0] * fg), (int)((b[1]) * fg), 3);
            ar = new Bitmap((int)(b[0] * fg), (int)((b[1]) * fg));
            MessageBox.Show("Graphic begin!!");
            Graphics g = Graphics.FromImage(ar);
            lock (ar)
            {
                var output = Task.Factory.StartNew(() =>
            {

                ParallelOptions po = new ParallelOptions(); po.MaxDegreeOfParallelism = 2; Parallel.For(0, ar.Width, i =>
            {


                ParallelOptions poo = new ParallelOptions(); poo.MaxDegreeOfParallelism = 2; Parallel.For(0, ar.Height, j =>
                {

                    var output1 = Task.Factory.StartNew(() => Threaaddraw(i, j, ref g, ref ar));

                });
            });

            });
                output.Wait();
            }
            MessageBox.Show("Graphic finished!!");
        }
    }
}

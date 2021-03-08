﻿/*tetrashop.ir 1399/11/24 iran urmia
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace WindowsApplication1
{
    public class _2dTo3D

    {
        public double x = 0;
        int cxC = -1, cyC = -1, czC = -1;
        public List<List<double[]>> st = new List<List<double[]>>();
        public bool _3DReady = false;
        Image a;
        public Image ar;
        //size
        int[] b = new int[3];
        int[,,] t;// zeros(b(1,1),b(1,2),3);
        int[,,] rr;// rr=zeros(b(1,1),b(1,2),3);
        int[,,] f;//     f=zeros(b(1,1),b(1,2),3);
        int[,,] ddr;//     f=zeros(b(1,1),b(1,2),3);
        public float[,,] c;
        public int cx = 0, cyp0 = 0, cyp1 = 0, cz = 3;
        public float[,,] e;
        int fg = 2;
        public int minr = int.MaxValue;
        int minteta = int.MaxValue;
        int minfi = int.MaxValue;
        public int maxr = int.MinValue;
        int maxteta = int.MinValue;
        int maxfi = int.MinValue;
        double[] cart2sph(float i, float j, float k)
        {
            double[] s = new double[3];
            s[2] = Math.Sqrt(i * i + j * j + k * k);
            if (s[2] == 0.0)
            {
                s[0] = 0.0;
                s[1] = 0.0;
            }
            else
            {
                s[0] = Math.Acos((double)k / s[2]);
                s[1] = Math.Atan2((double)j, (double)i);
            }
            return s;
        }
        bool SetneighboursSt(Image a, double cxr, double cyr, double i, double j, double maxx, double maxy)
        {
            lock (st)
            {
                lock (a)
                {
                    bool aa = false;
                    if (cxr > 0 && cyr> 0)
                    {
                        if (i >= 0 && j >= 0)
                        {
                            if (i < a.Width && j < a.Height)
                            {
                                if (cxr < maxx - 1 && cyr < maxy - 1)
                                {

                                    aa = System.Convert.ToDouble(GetK(a, (int)i, (int)j, 0)) + System.Convert.ToDouble(GetK(a, (int)i, (int)j, 1)) + System.Convert.ToDouble(GetK(a, (int)i, (int)j, 2)) > 0;

                                    if (System.Convert.ToDouble(GetK(a, (int)i, (int)j, 0)) + System.Convert.ToDouble(GetK(a, (int)i, (int)j, 1)) + System.Convert.ToDouble(GetK(a, (int)i, (int)j, 2)) > 0)
                                    {
                                        st[(int)cxr][(int)cyr][0] = i;
                                        st[(int)cxr][(int)cyr][1] = j;
                                        st[(int)cxr][(int)cyr][2] = (System.Convert.ToDouble(GetK(a, (int)i, (int)j, 0)) + System.Convert.ToDouble(GetK(a, (int)i, (int)j, 1)) + System.Convert.ToDouble(GetK(a, (int)i, (int)j, 2))) / 3.0;
                                    }
                                    if ((System.Convert.ToDouble(GetK(a, (int)(i - 1), (int)j, 0)) + System.Convert.ToDouble(GetK(a, (int)(i - 1), (int)j, 1)) + System.Convert.ToDouble(GetK(a, (int)(i - 1), (int)j, 2))) > 0)
                                    {
                                        st[(int)cxr][(int)cyr][3] = i - 1;
                                        st[(int)cxr][(int)cyr][4] = j;
                                        st[(int)cxr][(int)cyr][5] = (System.Convert.ToDouble(GetK(a, (int)(i - 1), (int)j, 0)) + System.Convert.ToDouble(GetK(a, (int)(i - 1), (int)j, 1)) + System.Convert.ToDouble(GetK(a, (int)(i - 1), (int)j, 2))) / 3.0;
                                    }
                                    if ((System.Convert.ToDouble(GetK(a, (int)(i - 1), (int)j, 0)) + System.Convert.ToDouble(GetK(a, (int)(i - 1), (int)j, 1)) + System.Convert.ToDouble(GetK(a, (int)(i - 1), (int)j, 2))) > 0)
                                    {
                                        st[(int)cxr][(int)cyr][6] = i + 1;
                                        st[(int)cxr][(int)cyr][7] = j;
                                        st[(int)cxr][(int)cyr][8] = (System.Convert.ToDouble(GetK(a, (int)(i + 1), (int)j, 0)) + System.Convert.ToDouble(GetK(a, (int)(i + 1), (int)j, 1)) + System.Convert.ToDouble(GetK(a, (int)(i + 1), (int)j, 2))) / 3.0;
                                    }
                                    if ((System.Convert.ToDouble(GetK(a, (int)(i), (int)(j - 1), 0)) + System.Convert.ToDouble(GetK(a, (int)(i), (int)(j - 1), 1)) + System.Convert.ToDouble(GetK(a, (int)(i), (int)(j - 1), 2))) > 0)
                                    {
                                        st[(int)cxr][(int)cyr][9] = i;
                                        st[(int)cxr][(int)cyr][10] = (j - 1);
                                        st[(int)cxr][(int)cyr][11] = (System.Convert.ToDouble(GetK(a, (int)(i), (int)(j - 1), 0)) + System.Convert.ToDouble(GetK(a, (int)(i), (int)(j - 1), 1)) + System.Convert.ToDouble(GetK(a, (int)(i), (int)(j - 1), 2))) / 3.0;
                                    }
                                    if ((System.Convert.ToDouble(GetK(a, (int)(i), (int)(j + 1), 0)) + System.Convert.ToDouble(GetK(a, (int)(i), (int)(j + 1), 1)) + System.Convert.ToDouble(GetK(a, (int)(i), (int)(j + 1), 2))) > 0)
                                    {
                                        st[(int)cxr][(int)cyr][12] = i;
                                        st[(int)cxr][(int)cyr][13] = (j + 1);
                                        st[(int)cxr][(int)cyr][14] = (System.Convert.ToDouble(GetK(a, (int)(i), (int)(j + 1), 0)) + System.Convert.ToDouble(GetK(a, (int)(i), (int)(j + 1), 1)) + System.Convert.ToDouble(GetK(a, (int)(i), (int)(j + 1), 2))) / 3.0;
                                    }
                                    if (System.Convert.ToDouble(GetK(a, (int)i, (int)(int)j, 0)) + System.Convert.ToDouble(GetK(a, (int)i, (int)(int)j, 1)) + System.Convert.ToDouble(GetK(a, (int)i, (int)(int)j, 2)) > 0)
                                    {
                                        st[(int)cxr][(int)cyr][15] = i - 1;
                                        st[(int)cxr][(int)cyr][16] = (j - 1);
                                        st[(int)cxr][(int)cyr][17] = (System.Convert.ToDouble(GetK(a, (int)(i - 1), (int)(j - 1), 0)) + System.Convert.ToDouble(GetK(a, (int)i, (int)j, 1)) + System.Convert.ToDouble(GetK(a, (int)i, (int)j, 2))) / 3.0;
                                    }
                                    if ((System.Convert.ToDouble(GetK(a, (int)(i - 1), (int)(j + 1), 0)) + System.Convert.ToDouble(GetK(a, (int)(i - 1), (int)(j + 1), 1)) + System.Convert.ToDouble(GetK(a, (int)(i - 1), (int)(j + 1), 2))) > 0)
                                    {
                                        st[(int)cxr][(int)cyr][18] = i - 1;
                                        st[(int)cxr][(int)cyr][19] = (j + 1);
                                        st[(int)cxr][(int)cyr][20] = (System.Convert.ToDouble(GetK(a, (int)(i - 1), (int)(j + 1), 0)) + System.Convert.ToDouble(GetK(a, (int)(i - 1), (int)(j + 1), 1)) + System.Convert.ToDouble(GetK(a, (int)(i - 1), (int)(j + 1), 2))) / 3.0;
                                    }
                                    if ((System.Convert.ToDouble(GetK(a, (int)(i - 1), (int)(j + 1), 0)) + System.Convert.ToDouble(GetK(a, (int)(i - 1), (int)(j + 1), 1)) + System.Convert.ToDouble(GetK(a, (int)(i - 1), (int)(j + 1), 2))) > 0)
                                    {
                                        st[(int)cxr][(int)cyr][21] = i + 1;
                                        st[(int)cxr][(int)cyr][22] = (j - 1);
                                        st[(int)cxr][(int)cyr][23] = (System.Convert.ToDouble(GetK(a, (int)(i + 1), (int)(j - 1), 0)) + System.Convert.ToDouble(GetK(a, (int)(i + 1), (int)(j - 1), 1)) + System.Convert.ToDouble(GetK(a, (int)(i + 1), (int)(j - 1), 2))) / 3.0;
                                    }
                                    if ((System.Convert.ToDouble(GetK(a, (int)(i + 1), (int)(j + 1), 0)) + System.Convert.ToDouble(GetK(a, (int)(i + 1), (int)(j + 1), 1)) + System.Convert.ToDouble(GetK(a, (int)(i + 1), (int)(j + 1), 2))) > 0)
                                    {
                                        st[(int)cxr][(int)cyr][24] = i + 1;
                                        st[(int)cxr][(int)cyr][25] = (j + 1);
                                        st[(int)cxr][(int)cyr][26] = (System.Convert.ToDouble(GetK(a, (int)(i + 1), (int)(j + 1), 0)) + System.Convert.ToDouble(GetK(a, (int)(i + 1), (int)(j + 1), 1)) + System.Convert.ToDouble(GetK(a, (int)(i + 1), (int)(j + 1), 2))) / 3.0;
                                    }
                                }
                            }
                        }
                    }
                    return aa;
                }
            }
        }
        void Threaadcal(int i, int j, int k, int ii, int jj)
        {
            lock (c)
            {
                if ((float)(System.Convert.ToInt32(GetK(a, i, j, k))) == 0)
                    return;
                float dr = 0;
                double[] s = new double[3];
                //[teta, fi, r] = cart2sph(i, j, 0);
                s = cart2sph(i, j, 1);
                t[i, j, k] = (int)Math.Round((double)(s[0] * 180.0 / 3.1415));
                f[i, j, k] = (int)Math.Round((double)(s[1] * 180.0 / 3.1415));
                rr[i, j, k] = (int)Math.Round((double)(s[2]));
                dr = (float)Math.Round((((maxr - minr) * -1.0 * ((double)(i + 1))) / (1 + Math.Sqrt(Math.Pow(i, 2) + Math.Pow(j, 2) + Math.Pow(k, 2)))) * 3.0 * 300.0 / (1.0 + System.Convert.ToDouble(GetK(a, i, j, 0)) + System.Convert.ToDouble(GetK(a, i, j, 1)) + System.Convert.ToDouble(GetK(a, i, j, 2)))); ;
                ddr[i, j, k] = (int)dr;
                if ((maxr - minr) * ii + dr >= 0// && (t[i, j, k + 1] + 2 < maxteta - minteta) && (t[i, j, k + 1] - 2 > minteta)
                     )
                {
                    try
                    {
                        int cxT = (maxr - minr) * ii + (int)dr;
                        int cyT1 = (int)Math.Round((double)((maxteta - minteta) * (double)jj + (double)t[i, j, k] + 2.0));
                        int cyT2 = (int)Math.Round((double)((maxteta - minteta) * (double)jj + (double)t[i, j, k] - 2.0));

                        if ((ii + jj) % 2 == 0)
                        {
                            c[cxT, cyT1, k] = (float)(System.Convert.ToInt32(GetK(a, i, j, k)));
                            //SetneighboursSt(a, cxT, cyT1, cxC, cyC);
                        }
                        else
                        {
                            c[cxT, cyT2, k] = (float)(System.Convert.ToInt32(GetK(a, i, j, k)));
                            //SetneighboursSt(a, cxT, cyT2, cxC, cyC);
                        }
                    }
                    catch (Exception t)
                    {

                    }
                }
            }
        }
        void ThreaadcalNeighbour(int i, int j, int k, int ii, int jj)
        {
            lock (c)
            {
                if ((float)(System.Convert.ToInt32(GetK(a, i, j, k))) == 0)
                    return;
                float dr = 0;
                double[] s = new double[3];
                //[teta, fi, r] = cart2sph(i, j, 0);
                s = cart2sph(i, j, 1);
                t[i, j, k] = (int)Math.Round((double)(s[0] * 180.0 / 3.1415));
                f[i, j, k] = (int)Math.Round((double)(s[1] * 180.0 / 3.1415));
                rr[i, j, k] = (int)Math.Round((double)(s[2]));
                dr = (float)Math.Round((((maxr - minr) * -1.0 * ((double)(i + 1))) / (1 + Math.Sqrt(Math.Pow(i, 2) + Math.Pow(j, 2) + Math.Pow(k, 2)))) * 3.0 * 300.0 / (1.0 + System.Convert.ToDouble(GetK(a, i, j, 0)) + System.Convert.ToDouble(GetK(a, i, j, 1)) + System.Convert.ToDouble(GetK(a, i, j, 2)))); ;
                ddr[i, j, k] = (int)dr;
                if ((maxr - minr) * ii + dr >= 0// && (t[i, j, k + 1] + 2 < maxteta - minteta) && (t[i, j, k + 1] - 2 > minteta)
                     )
                {
                    try
                    {
                        int cxT = (maxr - minr) * ii + (int)dr;
                        int cyT1 = (int)Math.Round((double)((maxteta - minteta) * (double)jj + (double)t[i, j, k] + 2.0));
                        int cyT2 = (int)Math.Round((double)((maxteta - minteta) * (double)jj + (double)t[i, j, k] - 2.0));

                        if ((ii + jj) % 2 == 0)
                        {
                            bool aa = SetneighboursSt(a, cxT, cyT1, i, j, cx, cyp1);
                            if (aa)
                                x++;
                        }
                        else
                        {
                            SetneighboursSt(a, cxT, cyT2, i, j, cx, cyp1);
                        }
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
                        if (c[(int)(ii * (maxr - minr) + ddr[i, j, k]), (int)(jj * (maxteta - minteta) + t[i, j, k] + 2), k] == 0)
                            return;
                        if ((ii + jj) % 2 == 0)
                            e[(int)(ii * b[0] + i), (int)(j), k] = c[(int)(ii * (maxr - minr) + ddr[i, j, k]), (int)(jj * (maxteta - minteta) + t[i, j, k] + 2), k];
                        else
                            e[(int)(ii * b[0] + i), (int)(j), k] = c[(int)(ii * (maxr - minr) + ddr[i, j, k]), (int)(jj * (maxteta - minteta) + t[i, j, k] - 2), k];
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
            cx = (int)((maxr - minr) * fg + (int)maxr + 1);

            cyp0 = (int)Math.Round((double)(maxteta - minteta) + (double)maxteta + 1.0);
            cyp1 = (int)Math.Round((double)(maxteta - minteta) * (double)2 + (double)maxteta + 1.0);
            //cy = (int)Math.Round((double)(maxteta - minteta) * (double)fg + (double)maxteta + 1.0);
            cxC = (int)((maxr - minr) * fg + (int)maxr + 1);
            cyC = (int)Math.Round((double)(maxteta - minteta) * (double)fg + (double)maxteta + 1.0);
            czC = 3;
            for (int i = 0; i < cxC; i++)
            {
                st.Add(new List<double[]>());
                for (int j = 0; j < cyp1; j++)
                {
                    st[i].Add(new double[27]);
                }
            }
            c = new float[cxC, cyp1, czC];
            t = new int[b[0], b[1], 3];
            rr = new int[b[0], b[1], 3];
            f = new int[b[0], b[1], 3];
            ddr = new int[b[0], b[1], 3];
            int v = 0;
            int n = 0;
            int q = 0;
            int robeta = 0;

            float rb = (float)0.20;// pixel;
            float Z = (float)0.100;// distance of eye form screen cm;
            float ra = 0;//varabale;
            List<Task> th = new List<Task>();

            var output = Task.Factory.StartNew(() =>
           {

               ParallelOptions po = new ParallelOptions(); po.MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, fg, delegate (int ii)
                {

                    ParallelOptions poo = new ParallelOptions(); poo.MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, fg, delegate (int jj)
                    {
                       //float[,,] cc = new float[(maxr - minr + 1), (maxteta - minteta + 1), 3];
                       ParallelOptions ppoio = new ParallelOptions(); ppoio.MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, b[0], delegate (int i)
                   {
                       ParallelOptions pooo = new ParallelOptions(); pooo.MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, b[1], delegate (int j)
                       {
                           ParallelOptions poooo = new ParallelOptions(); poooo.MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, 2, delegate (int k)
                             {
                                 var output1 = Task.Factory.StartNew(() => Threaadcal(i, j, k, ii, jj));
                                 lock (th) { th.Add(output1); }
                             });


                       });
                   });
                    });
                });
           });
            //output.Wait();
            th.Add(output);
            Parallel.ForEach(th, item => Task.WaitAll(item));
        }
        void ContoObjectNeighbour()
        {
            int r = 0;
            int teta = 0;
            int fi = 0;
            cx = (int)((maxr - minr) * fg + (int)maxr + 1);

            cyp0 = (int)Math.Round((double)(maxteta - minteta) + (double)maxteta + 1.0);
            cyp1 = (int)Math.Round((double)(maxteta - minteta) * (double)2 + (double)maxteta + 1.0);
            //cy = (int)Math.Round((double)(maxteta - minteta) * (double)fg + (double)maxteta + 1.0);
            cxC = (int)((maxr - minr) * fg + (int)maxr + 1);
            cyC = (int)Math.Round((double)(maxteta - minteta) * (double)fg + (double)maxteta + 1.0);
            czC = 3;
            for (int i = 0; i < cxC; i++)
            {
                st.Add(new List<double[]>());
                for (int j = 0; j < cyp1; j++)
                {
                    st[i].Add(new double[27]);
                }
            }
            c = new float[cxC, cyp1, czC];
            t = new int[b[0], b[1], 3];
            rr = new int[b[0], b[1], 3];
            f = new int[b[0], b[1], 3];
            ddr = new int[b[0], b[1], 3];
            int v = 0;
            int n = 0;
            int q = 0;
            int robeta = 0;

            float rb = (float)0.20;// pixel;
            float Z = (float)0.100;// distance of eye form screen cm;
            float ra = 0;//varabale;
            List<Task> th = new List<Task>();

            var output = Task.Factory.StartNew(() =>
            {

                ParallelOptions po = new ParallelOptions(); po.MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, fg, delegate (int ii)
                {

                    ParallelOptions poo = new ParallelOptions(); poo.MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, fg, delegate (int jj)
                    {
                        //float[,,] cc = new float[(maxr - minr + 1), (maxteta - minteta + 1), 3];
                        ParallelOptions ppoio = new ParallelOptions(); ppoio.MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, b[0], delegate (int i)
                        {
                            ParallelOptions pooo = new ParallelOptions(); pooo.MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, b[1], delegate (int j)
                            {
                                ParallelOptions poooo = new ParallelOptions(); poooo.MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, 3, delegate (int k)
                                {
                                    var output1 = Task.Factory.StartNew(() => ThreaadcalNeighbour(i, j, k, ii, jj));
                                    lock (th) { th.Add(output1); }
                                });


                            });
                        });
                    });
                });
            });
            //output.Wait();
           th.Add(output);
            Parallel.ForEach(th, item => Task.WaitAll(item));
        }
        byte GetK(Image a, int i, int j, int k)
        {
            object o = new object();
            lock (o)
            {
                if (i >= (a as Bitmap).Width || j >= (a as Bitmap).Height)
                    return 0;
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
            e = new float[(int)(b[0] * fg), (int)((b[1])), 3];
            List<Task> th = new List<Task>();

            var output = Task.Factory.StartNew(() =>
            {
                ParallelOptions pop = new ParallelOptions(); pop.MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, fg, delegate (int ii)
                 {
                     ParallelOptions popp = new ParallelOptions(); popp.MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, fg, delegate (int jj)
                     {
                         ParallelOptions poo = new ParallelOptions(); poo.MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, b[0], delegate (int i)
                           {
                               ParallelOptions pon = new ParallelOptions(); pon.MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, b[1], delegate (int j)
                               {
                                   ParallelOptions pob = new ParallelOptions(); pob.MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, 2, delegate (int k)
                                   {
                                       var output1 = Task.Factory.StartNew(() => Threaadfetch(i, j, k, ii, jj));

                                       lock (th) { th.Add(output1); }
                                   });
                               });
                           });
                     });
                 });
            });
           // output.Wait();
            th.Add(output);
            Parallel.ForEach(th, item => Task.WaitAll(item));
        }
        void Initiate()
        {
            minr = int.MaxValue;
            minteta = int.MaxValue;
            minfi = int.MaxValue;
            maxr = int.MinValue;
            maxteta = int.MinValue;
            maxfi = int.MinValue;
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
                        s = cart2sph(i, j, 1);
                        teta = (int)Math.Round(s[0] * 180.0 / 3.1415);
                        fi = (int)Math.Round(s[1] * 180.0 / 3.1415);
                        r = (int)Math.Round(s[2]);
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
        void InitiateIntelli()
        {
            minr = int.MaxValue;
            minteta = int.MaxValue;
            minfi = int.MaxValue;
            maxr = int.MinValue;
            maxteta = int.MinValue;
            maxfi = int.MinValue;
            int r = 0;
            int teta = 0;
            int fi = 0;
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
                        s = cart2sph(i, j, 1);
                        teta = (int)Math.Round(s[0] * 180.0 / 3.1415);
                        fi = (int)Math.Round(s[1] * 180.0 / 3.1415);
                        r = (int)Math.Round(s[2]);
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
            bool Donemax = false, Donemin = false;
            for (int i = 0; i < ii; i++)
            {
                for (int j = 0; j < jj; j++)
                {
                    for (int k = 0; k < kk; k++)
                    {
                        if (p[i, j, k] > max)
                        {
                            Donemax = true;
                            max = p[i, j, k];
                        }
                        if (p[i, j, k] < min)
                        {
                            Donemin = true;
                            min = p[i, j, k];
                        }
                    }
                }

            }
            float q = min;
            if (q < 0.0 && Donemin)
            {
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
            }
            min = float.MaxValue; max = float.MinValue;
            Donemax = false; Donemin = false;
            for (int i = 0; i < ii; i++)
            {
                for (int j = 0; j < jj; j++)
                {
                    for (int k = 0; k < kk; k++)
                    {
                        if (p[i, j, k] > max)
                        {
                            Donemax = true;
                            max = p[i, j, k];
                        }
                        if (p[i, j, k] < min)
                        {
                            Donemin = true;
                            min = p[i, j, k];
                        }
                    }
                }

            }
            q = (float)255.0 / (max);
            if (max > 255.0 && Donemax)
            {
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
            }
            return p;
        }

        public void reconstruct2dto3d(int succeed)
        {
            if (succeed > 0)
            {
                ConvTo3D();
                MessageBox.Show("ConvTo3D pass!");
                ar = new Bitmap((int)(b[0] * fg), (int)((b[1])));
                MessageBox.Show("Graphic begin!!");
                Graphics g = Graphics.FromImage(ar);



                for (int i = 0; i < ar.Width; i++)
                {
                    for (int j = 0; j < ar.Height; j++)
                    {
                        Threaaddraw(i, j, ref g, ref ar);
                    }
                }
                _3DReady = true;
                g.Dispose();
            }
            /*  List<Task> th = new List<Task>();
              var output = Task.Factory.StartNew(() =>
                  {
                  lock (ar)
                  {
                      ParallelOptions pop = new ParallelOptions(); pop.MaxDegreeOfParallelism =System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, ar.Width, delegate(int i)
                      {
                          ParallelOptions popp = new ParallelOptions(); popp.MaxDegreeOfParallelism =System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, ar.Height, delegate(int j) 
                          {
                              var output1 = Task.Factory.StartNew(() => Threaaddraw(i, j, ref g, ref ar));
                              lock (th) { th.Add(output1); }
                          });
                      });
                      }
                  });
              th.Add(output);
              Parallel.ForEach(th, item => Task.WaitAll(item));*/
            /* BitmapData bitmapData = (ar as Bitmap).LockBits(new Rectangle(0, 0, (ar as Bitmap).Width, (ar as Bitmap).Height), ImageLockMode.ReadWrite, (ar as Bitmap).PixelFormat);
            //http://csharpexamples.com/tag/parallel-bitmap-processing/
             int bytesPerPixel = Bitmap.GetPixelFormatSize((ar as Bitmap).PixelFormat) / 8;
             int byteCount = bitmapData.Stride * (ar as Bitmap).Height;
             byte[] pixels = new byte[byteCount];
             IntPtr ptrFirstPixel = bitmapData.Scan0;
             Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);
             int heightInPixels = bitmapData.Height;
             int widthInBytes = bitmapData.Width * bytesPerPixel;

             for (int y = 0; y < heightInPixels; y++)
             {
                 int currentLine = y * bitmapData.Stride;
                 for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                 {
                     int oldBlue = new int();
                     int oldGreen = new int();
                     int oldRed = new int();

                     lock (e)
                     {
                          oldBlue = (int)e[x / bytesPerPixel, y, 0];                        
                          oldGreen = (int)e[x / bytesPerPixel, y, 1];
                          oldRed = (int)e[x / bytesPerPixel, y, 2];
                     }

                     // calculate new pixel value
                     pixels[currentLine + x] = (byte)oldBlue;
                     pixels[currentLine + x + 1] = (byte)oldGreen;
                     pixels[currentLine + x + 2] = (byte)oldRed;
                 }
             }

             // copy modified bytes back
             Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
             (ar as Bitmap).UnlockBits(bitmapData);*/
            MessageBox.Show("Graphic finished!!");

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
            e = uitn8(e, (int)(b[0] * fg), (int)((b[1])), 3);
            ar = new Bitmap((int)(b[0] * fg), (int)((b[1])));
            MessageBox.Show("Graphic begin!!");
            Graphics g = Graphics.FromImage(ar);



            for (int i = 0; i < ar.Width; i++)
            {
                for (int j = 0; j < ar.Height; j++)
                {
                    Threaaddraw(i, j, ref g, ref ar);
                }
            }
            _3DReady = true;
            g.Dispose();
            /*  List<Task> th = new List<Task>();
              var output = Task.Factory.StartNew(() =>
                  {
                  lock (ar)
                  {
                      ParallelOptions pop = new ParallelOptions(); pop.MaxDegreeOfParallelism =System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, ar.Width, delegate(int i)
                      {
                          ParallelOptions popp = new ParallelOptions(); popp.MaxDegreeOfParallelism =System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, ar.Height, delegate(int j) 
                          {
                              var output1 = Task.Factory.StartNew(() => Threaaddraw(i, j, ref g, ref ar));
                              lock (th) { th.Add(output1); }
                          });
                      });
                      }
                  });
              th.Add(output);
              Parallel.ForEach(th, item => Task.WaitAll(item));*/
            /* BitmapData bitmapData = (ar as Bitmap).LockBits(new Rectangle(0, 0, (ar as Bitmap).Width, (ar as Bitmap).Height), ImageLockMode.ReadWrite, (ar as Bitmap).PixelFormat);
            //http://csharpexamples.com/tag/parallel-bitmap-processing/
             int bytesPerPixel = Bitmap.GetPixelFormatSize((ar as Bitmap).PixelFormat) / 8;
             int byteCount = bitmapData.Stride * (ar as Bitmap).Height;
             byte[] pixels = new byte[byteCount];
             IntPtr ptrFirstPixel = bitmapData.Scan0;
             Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);
             int heightInPixels = bitmapData.Height;
             int widthInBytes = bitmapData.Width * bytesPerPixel;

             for (int y = 0; y < heightInPixels; y++)
             {
                 int currentLine = y * bitmapData.Stride;
                 for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                 {
                     int oldBlue = new int();
                     int oldGreen = new int();
                     int oldRed = new int();

                     lock (e)
                     {
                          oldBlue = (int)e[x / bytesPerPixel, y, 0];                        
                          oldGreen = (int)e[x / bytesPerPixel, y, 1];
                          oldRed = (int)e[x / bytesPerPixel, y, 2];
                     }

                     // calculate new pixel value
                     pixels[currentLine + x] = (byte)oldBlue;
                     pixels[currentLine + x + 1] = (byte)oldGreen;
                     pixels[currentLine + x + 2] = (byte)oldRed;
                 }
             }

             // copy modified bytes back
             Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
             (ar as Bitmap).UnlockBits(bitmapData);*/
            MessageBox.Show("Graphic finished!!");

        }
        public _2dTo3D(Image ib, double percent)
        {
            a = ib;
            Graphics g = Graphics.FromImage(a);



            for (int i = 0; i < a.Width; i++)
            {
                for (int j = 0; j < a.Height; j++)
                {
                    if (((i + j) % ((int)(1.0 / percent))) == 0)
                    {
                    }
                    else
                    {
                        (a as Bitmap).SetPixel(i, j, Color.Black);
                        g.DrawImage(a, 0, 0, a.Width, a.Height);
                        g.Save();
                    }
                }
            }
            MessageBox.Show("Simplyified pass!");

            Initiate();
            MessageBox.Show("Initiate pass!");
            ContoObject();
            MessageBox.Show("ContoObject pass!");
            ConvTo3D();
            MessageBox.Show("ConvTo3D pass!");
            e = uitn8(e, (int)(b[0] * fg), (int)((b[1])), 3);
            ar = new Bitmap((int)(b[0] * fg), (int)((b[1])));
            MessageBox.Show("Graphic begin!!");
            g = Graphics.FromImage(ar);



            for (int i = 0; i < ar.Width; i++)
            {
                for (int j = 0; j < ar.Height; j++)
                {
                    Threaaddraw(i, j, ref g, ref ar);
                }
            }
            _3DReady = true;
            g.Dispose();
            /*  List<Task> th = new List<Task>();
              var output = Task.Factory.StartNew(() =>
                  {
                  lock (ar)
                  {
                      ParallelOptions pop = new ParallelOptions(); pop.MaxDegreeOfParallelism =System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, ar.Width, delegate(int i)
                      {
                          ParallelOptions popp = new ParallelOptions(); popp.MaxDegreeOfParallelism =System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, ar.Height, delegate(int j) 
                          {
                              var output1 = Task.Factory.StartNew(() => Threaaddraw(i, j, ref g, ref ar));
                              lock (th) { th.Add(output1); }
                          });
                      });
                      }
                  });
              th.Add(output);
              Parallel.ForEach(th, item => Task.WaitAll(item));*/
            /* BitmapData bitmapData = (ar as Bitmap).LockBits(new Rectangle(0, 0, (ar as Bitmap).Width, (ar as Bitmap).Height), ImageLockMode.ReadWrite, (ar as Bitmap).PixelFormat);
            //http://csharpexamples.com/tag/parallel-bitmap-processing/
             int bytesPerPixel = Bitmap.GetPixelFormatSize((ar as Bitmap).PixelFormat) / 8;
             int byteCount = bitmapData.Stride * (ar as Bitmap).Height;
             byte[] pixels = new byte[byteCount];
             IntPtr ptrFirstPixel = bitmapData.Scan0;
             Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);
             int heightInPixels = bitmapData.Height;
             int widthInBytes = bitmapData.Width * bytesPerPixel;

             for (int y = 0; y < heightInPixels; y++)
             {
                 int currentLine = y * bitmapData.Stride;
                 for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                 {
                     int oldBlue = new int();
                     int oldGreen = new int();
                     int oldRed = new int();

                     lock (e)
                     {
                          oldBlue = (int)e[x / bytesPerPixel, y, 0];                        
                          oldGreen = (int)e[x / bytesPerPixel, y, 1];
                          oldRed = (int)e[x / bytesPerPixel, y, 2];
                     }

                     // calculate new pixel value
                     pixels[currentLine + x] = (byte)oldBlue;
                     pixels[currentLine + x + 1] = (byte)oldGreen;
                     pixels[currentLine + x + 2] = (byte)oldRed;
                 }
             }

             // copy modified bytes back
             Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
             (ar as Bitmap).UnlockBits(bitmapData);*/
            MessageBox.Show("Graphic finished!!");

        }
        public _2dTo3D(Image ib, bool neighbour)
        {
            object o = new object();
            lock (o)
            {
                x = 0;
                a = ib;
                Graphics g = Graphics.FromImage(a);
                Initiate();
                //MessageBox.Show("Initiate pass!");
                ContoObjectNeighbour();
                //MessageBox.Show("Neighbour pass!");
            }
        }
        public _2dTo3D(Image ib)

        {
            a = ib;
            Graphics g = Graphics.FromImage(a);
            Initiate();
            MessageBox.Show("Initiate pass!");
            ContoObject();
            MessageBox.Show("ContoObject pass!");
            ConvTo3D();
            MessageBox.Show("ConvTo3D pass!");
            e = uitn8(e, (int)(b[0] * fg), (int)((b[1])), 3);
            ar = new Bitmap((int)(b[0] * fg), (int)((b[1])));
            MessageBox.Show("Graphic begin!!");
            g = Graphics.FromImage(ar);



            for (int i = 0; i < ar.Width; i++)
            {
                for (int j = 0; j < ar.Height; j++)
                {
                    Threaaddraw(i, j, ref g, ref ar);
                }
            }
            _3DReady = true;
            g.Dispose();
            /*  List<Task> th = new List<Task>();
              var output = Task.Factory.StartNew(() =>
                  {
                  lock (ar)
                  {
                      ParallelOptions pop = new ParallelOptions(); pop.MaxDegreeOfParallelism =System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, ar.Width, delegate(int i)
                      {
                          ParallelOptions popp = new ParallelOptions(); popp.MaxDegreeOfParallelism =System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, ar.Height, delegate(int j) 
                          {
                              var output1 = Task.Factory.StartNew(() => Threaaddraw(i, j, ref g, ref ar));
                              lock (th) { th.Add(output1); }
                          });
                      });
                      }
                  });
              th.Add(output);
              Parallel.ForEach(th, item => Task.WaitAll(item));*/
            /* BitmapData bitmapData = (ar as Bitmap).LockBits(new Rectangle(0, 0, (ar as Bitmap).Width, (ar as Bitmap).Height), ImageLockMode.ReadWrite, (ar as Bitmap).PixelFormat);
            //http://csharpexamples.com/tag/parallel-bitmap-processing/
             int bytesPerPixel = Bitmap.GetPixelFormatSize((ar as Bitmap).PixelFormat) / 8;
             int byteCount = bitmapData.Stride * (ar as Bitmap).Height;
             byte[] pixels = new byte[byteCount];
             IntPtr ptrFirstPixel = bitmapData.Scan0;
             Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);
             int heightInPixels = bitmapData.Height;
             int widthInBytes = bitmapData.Width * bytesPerPixel;

             for (int y = 0; y < heightInPixels; y++)
             {
                 int currentLine = y * bitmapData.Stride;
                 for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                 {
                     int oldBlue = new int();
                     int oldGreen = new int();
                     int oldRed = new int();

                     lock (e)
                     {
                          oldBlue = (int)e[x / bytesPerPixel, y, 0];                        
                          oldGreen = (int)e[x / bytesPerPixel, y, 1];
                          oldRed = (int)e[x / bytesPerPixel, y, 2];
                     }

                     // calculate new pixel value
                     pixels[currentLine + x] = (byte)oldBlue;
                     pixels[currentLine + x + 1] = (byte)oldGreen;
                     pixels[currentLine + x + 2] = (byte)oldRed;
                 }
             }

             // copy modified bytes back
             Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
             (ar as Bitmap).UnlockBits(bitmapData);*/
            MessageBox.Show("Graphic finished!!");

        }
        public void _2dTo3D_reconstructed(Image ib)

        {
            a = ib;
            Graphics g = Graphics.FromImage(a);
            Initiate();
            MessageBox.Show("Initiate pass!");
            ContoObject();
            MessageBox.Show("ContoObject pass!");
            ConvTo3D();
            MessageBox.Show("ConvTo3D pass!");
            e = uitn8(e, (int)(b[0] * fg), (int)((b[1])), 3);
            ar = new Bitmap((int)(b[0] * fg), (int)((b[1])));
            MessageBox.Show("Graphic begin!!");
            g = Graphics.FromImage(ar);



            for (int i = 0; i < ar.Width; i++)
            {
                for (int j = 0; j < ar.Height; j++)
                {
                    Threaaddraw(i, j, ref g, ref ar);
                }
            }
            _3DReady = true;
            g.Dispose();
            /*  List<Task> th = new List<Task>();
              var output = Task.Factory.StartNew(() =>
                  {
                  lock (ar)
                  {
                      ParallelOptions pop = new ParallelOptions(); pop.MaxDegreeOfParallelism =System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, ar.Width, delegate(int i)
                      {
                          ParallelOptions popp = new ParallelOptions(); popp.MaxDegreeOfParallelism =System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, ar.Height, delegate(int j) 
                          {
                              var output1 = Task.Factory.StartNew(() => Threaaddraw(i, j, ref g, ref ar));
                              lock (th) { th.Add(output1); }
                          });
                      });
                      }
                  });
              th.Add(output);
              Parallel.ForEach(th, item => Task.WaitAll(item));*/
            /* BitmapData bitmapData = (ar as Bitmap).LockBits(new Rectangle(0, 0, (ar as Bitmap).Width, (ar as Bitmap).Height), ImageLockMode.ReadWrite, (ar as Bitmap).PixelFormat);
            //http://csharpexamples.com/tag/parallel-bitmap-processing/
             int bytesPerPixel = Bitmap.GetPixelFormatSize((ar as Bitmap).PixelFormat) / 8;
             int byteCount = bitmapData.Stride * (ar as Bitmap).Height;
             byte[] pixels = new byte[byteCount];
             IntPtr ptrFirstPixel = bitmapData.Scan0;
             Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);
             int heightInPixels = bitmapData.Height;
             int widthInBytes = bitmapData.Width * bytesPerPixel;

             for (int y = 0; y < heightInPixels; y++)
             {
                 int currentLine = y * bitmapData.Stride;
                 for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                 {
                     int oldBlue = new int();
                     int oldGreen = new int();
                     int oldRed = new int();

                     lock (e)
                     {
                          oldBlue = (int)e[x / bytesPerPixel, y, 0];                        
                          oldGreen = (int)e[x / bytesPerPixel, y, 1];
                          oldRed = (int)e[x / bytesPerPixel, y, 2];
                     }

                     // calculate new pixel value
                     pixels[currentLine + x] = (byte)oldBlue;
                     pixels[currentLine + x + 1] = (byte)oldGreen;
                     pixels[currentLine + x + 2] = (byte)oldRed;
                 }
             }

             // copy modified bytes back
             Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
             (ar as Bitmap).UnlockBits(bitmapData);*/
            MessageBox.Show("Graphic finished!!");

        }
        public _2dTo3D(string ass, double percent)
        {
            a = Image.FromFile(ass);
            Graphics g = Graphics.FromImage(a);



            for (int i = 0; i < a.Width; i++)
            {
                for (int j = 0; j < a.Height; j++)
                {
                    if (((i + j) % ((int)(1.0 / percent))) == 0)
                    {
                        g.DrawImage(a, 0, 0, a.Width, a.Height);
                        g.Save();
                    }
                    else
                    {
                        (a as Bitmap).SetPixel(i, j, Color.Black);
                        g.DrawImage(a, 0, 0, a.Width, a.Height);
                        g.Save();
                    }
                }
            }
            MessageBox.Show("Simplyified pass!");

            Initiate();
            MessageBox.Show("Initiate pass!");
            ContoObject();
            MessageBox.Show("ContoObject pass!");
            ConvTo3D();
            MessageBox.Show("ConvTo3D pass!");
            e = uitn8(e, (int)(b[0] * fg), (int)((b[1])), 3);
            ar = new Bitmap((int)(b[0] * fg), (int)((b[1])));
            MessageBox.Show("Graphic begin!!");
            g = Graphics.FromImage(ar);



            for (int i = 0; i < ar.Width; i++)
            {
                for (int j = 0; j < ar.Height; j++)
                {
                    Threaaddraw(i, j, ref g, ref ar);
                }
            }
            _3DReady = true;
            g.Dispose();
            /*  List<Task> th = new List<Task>();
              var output = Task.Factory.StartNew(() =>
                  {
                  lock (ar)
                  {
                      ParallelOptions pop = new ParallelOptions(); pop.MaxDegreeOfParallelism =System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, ar.Width, delegate(int i)
                      {
                          ParallelOptions popp = new ParallelOptions(); popp.MaxDegreeOfParallelism =System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, ar.Height, delegate(int j) 
                          {
                              var output1 = Task.Factory.StartNew(() => Threaaddraw(i, j, ref g, ref ar));
                              lock (th) { th.Add(output1); }
                          });
                      });
                      }
                  });
              th.Add(output);
              Parallel.ForEach(th, item => Task.WaitAll(item));*/
            /* BitmapData bitmapData = (ar as Bitmap).LockBits(new Rectangle(0, 0, (ar as Bitmap).Width, (ar as Bitmap).Height), ImageLockMode.ReadWrite, (ar as Bitmap).PixelFormat);
            //http://csharpexamples.com/tag/parallel-bitmap-processing/
             int bytesPerPixel = Bitmap.GetPixelFormatSize((ar as Bitmap).PixelFormat) / 8;
             int byteCount = bitmapData.Stride * (ar as Bitmap).Height;
             byte[] pixels = new byte[byteCount];
             IntPtr ptrFirstPixel = bitmapData.Scan0;
             Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);
             int heightInPixels = bitmapData.Height;
             int widthInBytes = bitmapData.Width * bytesPerPixel;

             for (int y = 0; y < heightInPixels; y++)
             {
                 int currentLine = y * bitmapData.Stride;
                 for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                 {
                     int oldBlue = new int();
                     int oldGreen = new int();
                     int oldRed = new int();

                     lock (e)
                     {
                          oldBlue = (int)e[x / bytesPerPixel, y, 0];                        
                          oldGreen = (int)e[x / bytesPerPixel, y, 1];
                          oldRed = (int)e[x / bytesPerPixel, y, 2];
                     }

                     // calculate new pixel value
                     pixels[currentLine + x] = (byte)oldBlue;
                     pixels[currentLine + x + 1] = (byte)oldGreen;
                     pixels[currentLine + x + 2] = (byte)oldRed;
                 }
             }

             // copy modified bytes back
             Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
             (ar as Bitmap).UnlockBits(bitmapData);*/
            MessageBox.Show("Graphic finished!!");

        }
    }
}

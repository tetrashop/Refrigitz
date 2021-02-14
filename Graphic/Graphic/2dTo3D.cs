/*tetrashop.ir 1399/11/24 iran urmia
 */
using System;
using System.Drawing;
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
        float[] cart2sph(float i, float j, float k)
        {
            float[] s = new float[3];
            s[2] = (float)Math.Sqrt(i * i + j * j + k * k);
            if (s[2] == 0)
            {
                s[0] = 0;
                s[1] = 0;
            }
            else
            {
                s[0] = (float)Math.Acos(k / s[2]);
                s[1] = (float)Math.Atan2(j, i);
            }
            return s;
        }
        void ContoObject()
        {
            int r = 0;
            int teta = 0;
            int fi = 0;
            c = new float[(maxr - minr + 1) * fg, (maxteta - minteta + 1) * fg, 3];
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
            float dr = 0;
            for (int ii = 0; ii < fg ; ii++)
            {
                for (int jj = 0; jj < fg ; jj++)
                {
                    float[,,] cc = new float[(maxr - minr + 1), (maxteta - minteta + 1), 3];
                    for (int i = 0; i < b[0]; i++)
                    {
                        for (int j = 0; j < b[1]; j++)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                float[] s = new float[3];
                                //[teta, fi, r] = cart2sph(i, j, 0);
                                s = cart2sph(i, j, 0);
                                teta = (int)s[0];
                                fi = (int)s[1];
                                r = (int)s[2];

                                s[1] = (float)Math.Round(s[1] * 180 / 3.1415);
                                s[0] = (float)Math.Round(s[0] * 180 / 3.1415);
                                s[2] = (float)Math.Round(s[2] - minr);
                                fi = fi - minfi + 1;
                                teta = teta - minteta + 1;
                                r = (int)Math.Round((double)(r - minr + 1));
                                t[i, j, k] = teta;
                                rr[i, j, k] = r;
                                f[i, j, k] = fi;


                                dr = (float)Math.Round(((-1 * (i + 1)) / (1 + Math.Sqrt(Math.Pow(i + 1, 2) + Math.Pow(j + 1, 2) + Math.Pow(k + 1, 2)))) * 3 * 300 / (1 + System.Convert.ToInt32(GetK(a, i, j, 0)) + System.Convert.ToInt32(GetK(a, i, j, 1)) + System.Convert.ToInt32(GetK(a, i, j, 2))));
                                if ((dr + maxr - minr < maxr - minr) && (teta + 1 < maxteta - minteta) && (teta - 1 >= minteta))
                                {
                                    if ((ii + jj) % 2 == 0)
                                        c[(int)((maxr - minr + 1) * ii + r), (int)Math.Round((double)(maxteta - minteta + 1) * jj + teta + 1), k] = (float)(System.Convert.ToInt32(GetK(a, i, j, k)) + dr);
                                    else
                                        c[(int)((maxr - minr + 1) * ii + r), (int)Math.Round((double)(maxteta - minteta + 1) * jj + teta - 1), k] = (float)(System.Convert.ToInt32(GetK(a, i, j, k)) + dr);
                                }
                            }


                        }
                    }
                }
            }
        }
        byte GetK(Image a, int i, int j, int k)
        {
            if (k == 0)
                return (a as Bitmap).GetPixel(i, j).R;
            if (k == 1)
                return (a as Bitmap).GetPixel(i, j).G;

            return (a as Bitmap).GetPixel(i, j).B;

        }
        void ConvTo3D()
        {
            int r = 0;
            int teta = 0;
            int fi = 0;
            e = new float[(int)(b[0] * fg), (int)((b[1]) * fg), 3];

            for (int ii = 0; ii < fg ; ii++)
            {
                for (int jj = 0; jj < fg ; jj++)
                {
                    for (int i = 0; i < b[0]; i++)
                    {
                        for (int j = 0; j < b[1]; j++)
                        {
                            for (int k = 0; k < 3; k++)
                            {

                                e[(int)(ii * b[0] + i), (int)(jj * b[1] + j), k] = c[(int)(ii * (maxr - minr) + rr[i, j, k]), (int)(jj * (maxteta - minteta) + t[i, j, k]), k];
                            }
                        }
                    }
                }
            }
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
                        float[] s = new float[3];
                        //[teta, fi, r] = cart2sph(i, j, 0);
                        s = cart2sph(i, j, 0);
                        s[2] = (float)Math.Round(s[2]);
                        s[0] = (float)Math.Round(s[0] * 180 / 3.1415);
                        s[1] = (float)Math.Round(s[1] * 180 / 3.1415);
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
        float[,,] uitn8(float[,,] a,int ii,int jj,int kk)
        {
            float min = 10000000, max = -1000000;
            for (int i = 0; i < ii; i++)
            {
                for (int j = 0; j < jj; j++)
                {
                    for (int k = 0; k < kk; k++)
                    {
                        if (a[i, j, k] > max)
                            max = a[i, j, k];
                        if (a[i, j, k] < min)
                            min = a[i, j, k];
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
                        a[i, j, k] -= q;
                    }
                }

            }

            min = 10000000; max = -1000000;
            for (int i = 0; i < ii; i++)
            {
                for (int j = 0; j < jj; j++)
                {
                    for (int k = 0; k < kk; k++)
                    {
                        if (a[i, j, k] > max)
                            max = a[i, j, k];
                        if (a[i, j, k] < min)
                            min = a[i, j, k];
                    }
                }

            }
             q = 255 / (max - min);
            for (int i = 0; i < ii; i++)
            {
                for (int j = 0; j < jj; j++)
                {
                    for (int k = 0; k < kk; k++)
                    {
                        a[i, j, k] *= q;
                    }
                }

            }
            return a;
        }
        //convert 2d image to 3d;
        public _2dTo3D(string ass)
        {
            a = Image.FromFile(ass);
            Initiate();
            ContoObject();
            ConvTo3D();
            e = uitn8(e, (int)(b[0] * fg), (int)((b[1]) * fg), 3);
            ar = new Bitmap(a, (int)(b[0] * fg), (int)((b[1]) * fg));
            Graphics g = Graphics.FromImage(ar);
            for (int i = 0; i < ar.Width; i++)
            {
                for (int j = 0; j < ar.Height; j++)
                {
                    
                        (ar as Bitmap).SetPixel(i, j, Color.FromArgb((int)(e[i, j, 0]), (int)(e[i, j, 1]),(int)(e[i, j, 2])));
                        g.DrawImage(ar, 0, 0, ar.Width, ar.Height);
                        g.Save();
                    
                }
            }
        }
    }
}

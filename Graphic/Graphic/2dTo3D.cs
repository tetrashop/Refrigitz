/*tetrashop.ir 1399/11/24 iran urmia
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace WindowsApplication1
{
    class _2dTo3D
    {
        Bitmap a;
        //size
        int[] b =new int[3];
        int[,,] t;// zeros(b(1,1),b(1,2),3);
        int[,,] rr;// rr=zeros(b(1,1),b(1,2),3);
        int[,,] f;//     f=zeros(b(1,1),b(1,2),3);
        float[,,] c;
        float[,,] e;
        int fg = 2;
        int r = 0;
        int teta = 0;
        int fi = 0;
        int minr = +100000;
        int minteta = +100000;
        int minfi = +100000;
        int maxr = -1000000;
        int maxteta = -1000000;
        int maxfi = -1000000;
        float[] cart2sph(float i, float j, float k)
        {
            float[] s = new float[3];
            s[2] =(float) Math.Sqrt(i * i + j * j + k * k);
            if (s[2] == 0)
            {
                s[0] = 0;
                s[1] = 0;
            }
            else
            {
                s[0] = (float)Math.Acos(k / r);
                s[1] = (float)Math.Atan2(j, i);
            }
            return s;
        }
        void ContoObject(int[] b)
        {
            c = new float[(maxr - minr + 1) * fg, (maxteta - minteta + 1) * fg, 3];
            int v = 0;
            int n = 0;
            int q = 0;
            int robeta = 0;

            float rb = (float)0.20;// pixel;
            float Z = (float)0.100;// distance of eye form screen cm;
            float ra = 0;//varabale;
            float dr = 0;
            for (int ii = 0; ii < fg - 1; ii++)
            {
                for (int jj = 0; jj < fg - 1; jj++)
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
                                s[2] = (float)Math.Round(s[2] - minr + 1);
                                fi = fi - minfi + 1;
                                teta = teta - minteta + 1;
                                t[i, j, k] = teta;
                                rr[i, j, k] = r;
                                f[i, j, k] = fi;


                                dr = (float)Math.Round((-i / Math.Sqrt(Math.Pow(i, 2) + Math.Pow(j, 2) + Math.Pow(k, 2))) * 3 * 300 / (1 + System.Convert.ToInt32(a.GetPixel(i, j))));
                                if (dr + maxr - minr < maxr - minr && teta + 2 < maxteta - minteta && teta - 2 > minteta)
                                {
                                    if ((ii + jj) % 2 == 0)
                                        c[(int)((maxr - minr + 1) * ii + r),(int)Math.Round((double)(maxteta - minteta + 1) * jj + teta + 2), k] = System.Convert.ToInt32(a.GetPixel(i, j)) + dr;
                                    else
                                        c[(int)((maxr - minr + 1) * ii + r), (int)Math.Round((double)(maxteta - minteta + 1) * jj + teta - 2), k] = System.Convert.ToInt32(a.GetPixel(i, j)) + dr;
                                }
                            }


                        }
                    }
                }
            }
        }
        void ConvTo3D()
        {
            e = new float[b[0],(int)( b[1] * fg), 3];

            for (int ii = 0; ii < fg - 1; ii++)
            {
                for (int jj = 0; jj < fg - 1; jj++)
                {
                    for (int i = 0; i < b[0]; i++)
                    {
                        for (int j = 0; j < b[1]; j++)
                        {
                            for (int k = 1; k < 3; k++)
                            {

                                e[(int)(ii * b[0] + i),(int) (jj * b[1] + j), k] = c[(int)(ii * (maxr - minr + 1) + rr[i, j, k]),(int)( jj * (maxteta - minteta + 1) + t[i, j, k]), k];
                            }
                        }
                    }
                }
            }
        }
        void Initiate()
        {
            b[0] = a.Width;
            b[1] = a.Height;
            b[3] = 3;
            t = new int[b[0], b[1], 3];
            rr=new int[b[0], b[1], 3];
            f=new int[b[0], b[1], 3];

            for (int i = 0; i < b[0]; i++){
                for (int j = 0; j < b[1]; j++){
                    for (int k = 0; k < 3; k++)
                        {
                        float[] s = new float[3];
                        //[teta, fi, r] = cart2sph(i, j, 0);
                        s = cart2sph(i, j, 0);
                        s[2] = (float)Math.Round(s[2]);
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
    }
}

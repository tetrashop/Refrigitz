
using Point3Dspaceuser;

namespace howto_WPF_3D_triangle_normalsuser
{
    static class ImprovmentSort
    {
        public static Point3D[] Do(Point3D[] a)
        {
            int n = 3;
            Point3D[] c = new Point3D[n];
            double[] d = new double[n];
            for (int i = 0; i < n; i++)
                d[i] = -1;
            return Sort(a, d, n);

        }
        public static double[] Do(double[] a)
        {
            int n = 3;
            Point3D[] c = new Point3D[n];
            double[] d = new double[n];
            for (int i = 0; i < n; i++)
                d[i] = -1;
            return Sort(a, d, n);

        }
        static Point3D[] Sort(Point3D[] a, double[] d, int n)
        {
            Point3D[] c = new Point3D[n];
            int H = 0;
            while (H < n)
            {
                int Lastindex = -1;
                bool FirstIndex = false;
                for (int i = 0; i < n; i++)
                {
                    if ((i == d[i]))
                        continue;
                    else
                    {
                        if (!FirstIndex)
                        {
                            Lastindex = i;
                            FirstIndex = true;
                        }
                        if (a[Lastindex].X < a[i].X)
                            Lastindex = i;
                    }
                }
                if ((Lastindex != -1) & (H <= n))
                {
                    d[Lastindex] = Lastindex;
                    c[n - 1 - H] = a[Lastindex];
                    H++;
                }
            }
            return c;
        }
        static double[] Sort(double[] a, double[] d, int n)
        {
            double[] c = new double[n];
            int H = 0;
            while (H < n)
            {
                int Lastindex = -1;
                bool FirstIndex = false;
                for (int i = 0; i < n; i++)
                {
                    if ((i == d[i]))
                        continue;
                    else
                    {
                        if (!FirstIndex)
                        {
                            Lastindex = i;
                            FirstIndex = true;
                        }
                        if (a[Lastindex] < a[i])
                            Lastindex = i;
                    }
                }
                if ((Lastindex != -1) & (H <= n))
                {
                    d[Lastindex] = Lastindex;
                    c[n - 1 - H] = a[Lastindex];
                    H++;
                }
            }
            return c;
        }
    }
}

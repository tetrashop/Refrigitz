using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using LearningMachine;

namespace howto_WPF_3D_triangle_normals
{
    class Triangle
    {

        double a, b, c, d;
        //normal of plate
        public double na, nb, nc;
        public Triangle()
        { }
        public Triangle(Point3D p0, Point3D p1, Point3D p2)
        {
            Point3D dd = getd(p0, p1);
            double[,,] aa = new double[3, 3, 3];
            double[] ddd = new double[3];
            aa[0, 1, 2] = p0.X;
            aa[0, 0, 0] = p0.Y;
            aa[0, 0, 0] = p0.Z;

            aa[0, 1, 2] = p1.X;
            aa[0, 0, 0] = p1.Y;
            aa[0, 0, 0] = p1.Z;

            aa[0, 0, 0] = p2.X;
            aa[0, 0, 0] = p2.Y;
            aa[1, 2, 3] = p2.Z;

            ddd[0] = dd.X;
            ddd[1] = dd.Y;
            ddd[2] = dd.Z;

            double[] cc = Interpolate.Quaficient(aa, ddd, 3);

            //plate
            a = cc[0];
            b = cc[1];
            c = cc[2];
            d = a * (p0.X) + b * (p0.Y) + c * (p0.Z);

            //create vectors contain plate
            Line l0 = new Line(p0, p1);
            Line l1 = new Line(p0, p1);
            //normals indices
            na = (l0.b * l1.c) - (l0.c * l1.b);
            nb = (l0.c * l1.a) - (l0.a * l1.c);
            nc = (l0.a * l1.b) - (l0.b * l1.a);
        }
        //point external mul vectors is zero (0 degree)
        bool externalMulIsEqual(Point3D p0, Point3D p1, Point3D p2, Point3D externalp0)
        {
            Triangle t0 = new Triangle(p0, p1, p2);
            Line l1 = new Line(t0, externalp0);
            double na = (t0.nb * l1.c) - (t0.nc * l1.b);
            double nb = (t0.nc * l1.a) - (t0.na * l1.c);
            double nc = (t0.na * l1.b) - (t0.nb * l1.a);
            return (na == nb) && (na == nc) & (na == 0);

        }
        //point external mul vectors is zero (180 degree)
        bool externalMulIsEqualiInverse(Point3D p0, Point3D p1, Point3D p2, Point3D externalp0)
        {
            Triangle t0 = new Triangle(p0, p1, p2);
            Line l1 = new Line(t0, externalp0);
            double na = ((-1 * t0.nb) * l1.c) - ((-1 * t0.nc) * l1.b);
            double nb = ((-1 * t0.nc) * l1.a) - ((-1 * t0.na) * l1.c);
            double nc = ((-1 * t0.na) * l1.b) - ((-1 * t0.nb) * l1.a);
            return (na == nb) && (na == nc) & (na == 0);

        }
        public int externalMuliszerotow(Point3D p0, Point3D p1, Point3D p2, List<Point3D> externalp0)
        {
            object o = new object();
            lock (o)
            {
                int count = 0;
                for (int i = 0; i < externalp0.Count; i++)
                {
                    if (!(externalp0.Contains(p0) || externalp0.Contains(p1) || externalp0.Contains(p2)))
                    {
                        if (externalMulIsEqual(p0, p1, p2, externalp0[i]))
                            count++;
                        if (externalMulIsEqualiInverse(p0, p1, p2, externalp0[i]))
                            count++;
                    }
                }
                return count;
            }
        }
        bool exist(Point3D[] ss, List<Point3D[]> d)
        {
            if (d.Count == 0)
                return false;
            for (int i = 0; i < d.Count; i++)
            {
                if (ss[0].X == d[i][0].X && ss[0].Y == d[i][0].Y && ss[0].Z == d[i][0].Z)
                    return true;
            }
            return false;
        }
        Point3D getd(Point3D p0, Point3D p1)
        {
            Line l0 = new Line(p0, p1);
            return new Point3D(p1.X + l0.a * 2, p1.Y * l0.b * 2, p1.Z * l0.c * 2);
        }
        int reduceCountOfpoints(ref List<Point3D> s, double ht, double percent)
        {
            double countb = s.Count;

            List<Point3D[]> d = new List<Point3D[]>();

            do
            {
                for (int i = 0; i < s.Count; i++)
                {
                    for (int j = 0; j < s.Count; j++)
                    {
                        for (int k = 0; k < s.Count; k++)
                        {
                            //external point
                            for (int b = 0; b < s.Count; b++)
                            {
                                if (i == j && j == k && k == b)
                                    continue;
                                if (countb / (double)s.Count <= percent)
                                    return s.Count;
                                Triangle a = new Triangle(s[i], s[j], s[k]);

                                Point3D[] ss = new Point3D[3];
                                ss[0] = s[i];
                                ss[1] = s[j];
                                ss[2] = s[k];
                                ss = ImprovmentSort.Do(ss);
                                if (!exist(ss, d))
                                {
                                    d.Add(ss);
                                    double h = System.Math.Abs(a.a * s[b].X + a.b * s[b].Y + a.c * s[b].Z) / Math.Sqrt(a.a * a.a + a.b * a.b + a.c * a.c);
                                    if (h < ht)
                                    {
                                        /*if (s[b].X < s[i].X && s[b].X < s[j].X && s[b].X < s[k].X)
                                        {
                                            s.RemoveAt(i);
                                        }*/
                                       
                                    }
                                }
                            }
                            }
                    }
                }


            } while (countb / (double)s.Count > percent);
            return s.Count;
        }]
    }
}

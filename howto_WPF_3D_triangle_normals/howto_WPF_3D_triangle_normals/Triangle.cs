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
        Point3D getd(Point3D p0, Point3D p1)
        {
            Line l0 = new Line(p0, p1);
            return new Point3D(p1.X + l0.a * 2, p1.Y * l0.b * 2, p1.Z * l0.c * 2);
        }
    }
}

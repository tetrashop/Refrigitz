using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace howto_WPF_3D_triangle_normals
{
    class Line
    {
        public double a, b, c, x0, y0, z0;
        public Line(Point3D p0,Point3D p1)
        {
            x0 = p0.X;
            y0 = p0.Y;
            z0 = p0.Z;
            a = (p1.X - p1.X);
            b = (p1.Y - p1.Y);
            c = (p1.Z - p1.Z);
        }
        bool exist(Point3D p)
        {
            if (a == 0 || b == 0 || c == 0)
                return false;
            return (((p.X - x0) / a) == (p.Y - y0) / b) && ((p.X - x0) / a) == ((p.Z - z0) / c);
        }
    }
}

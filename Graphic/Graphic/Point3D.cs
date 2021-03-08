namespace Point3Dspaceuser
{
    public class Point3D
    {
        public double X, Y, Z;
        public int i, j, k;
        public Point3D(double x, double y, double z, int ii, int jj, int kk)
        {
            X = x;
            Y = y;
            Z = z;
            i = ii;
            j = jj;
            k = kk;
        }
        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Point3D()
        {
        }
    }
}

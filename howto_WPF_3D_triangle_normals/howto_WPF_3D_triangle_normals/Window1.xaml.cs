//#define SURFACE2

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using WindowsApplication1;


namespace howto_WPF_3D_triangle_normals
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        static void Log(Exception ex)
        {
            try
            {
                Object a = new Object();
                lock (a)
                {
                    string stackTrace = ex.ToString();
                    //Write to File.
                     File.AppendAllText( "ErrorProgramRun.txt", stackTrace + ": On" + DateTime.Now.ToString());
                }
            }
#pragma warning disable CS0168 // The variable 't' is declared but never used
            catch (Exception t) { }
#pragma warning restore CS0168 // The variable 't' is declared but never used
        }
        public Window1()
        {
            InitializeComponent();
        }
        WindowsApplication1.Form1 gr = null;
        // The main object model group.
        private Model3DGroup MainModel3Dgroup = new Model3DGroup();


        // The camera.
        private PerspectiveCamera TheCamera;

        // The camera's current location.
        private double CameraPhi = Math.PI / 6.0;       // 30 degrees
        private double CameraTheta = Math.PI / 6.0;     // 30 degrees
#if SURFACE2
        private double CameraR = 3.0;
#else
        private double CameraR = 13.0;
#endif

        // The change in CameraPhi when you press the up and down arrows.
        private const double CameraDPhi = 0.1;

        // The change in CameraTheta when you press the left and right arrows.
        private const double CameraDTheta = 0.1;

        // The change in CameraR when you press + or -.
        private const double CameraDR = 0.1;

        // The surface's model.
        private GeometryModel3D SurfaceModel;

        // The wireframe's model.
        private GeometryModel3D WireframeModel;

        // The normals model.
        private GeometryModel3D NormalsModel;

        // Create the scene.
        // MainViewport is the Viewport3D defined
        // in the XAML code that displays everything.
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Give the camera its initial position.
            TheCamera = new PerspectiveCamera();
            TheCamera.FieldOfView = 60;
            MainViewport.Camera = TheCamera;
            PositionCamera();

            // Define lights.
            DefineLights();

            // Create the model.
           //DefineModel(MainModel3Dgroup);

            // Add the group of models to a ModelVisual3D.
            ModelVisual3D model_visual = new ModelVisual3D();
            model_visual.Content = MainModel3Dgroup;

            // Display the main visual in the viewportt.
            MainViewport.Children.Add(model_visual);
        }

        // Define the lights.
        private void DefineLights()
        {
            AmbientLight ambient_light = new AmbientLight(Colors.Gray);
            DirectionalLight directional_light =
                new DirectionalLight(Colors.Gray, new Vector3D(-1.0, -3.0, -2.0));
            MainModel3Dgroup.Children.Add(ambient_light);
            MainModel3Dgroup.Children.Add(directional_light);
        }

        // Add the model to the Model3DGroup.
        private void DefineModel(Model3DGroup model_group)
        {
            // Make a mesh to hold the surface.
            MeshGeometry3D mesh = new MeshGeometry3D();

            // Make the surface's points and triangles.
#if SURFACE2
            const double xmin = -1.5;
            const double xmax = 1.5;
            const double dx = 0.3;
            const double zmin = -1.5;
            const double zmax = 1.5;
            const double dz = 0.3;
#else
            const double xmin = -5;
            const double xmax = 5;
            const double dx = 1;
            const double zmin = -5;
            const double zmax = 5;
            const double dz = 1;
#endif
            for (double x = xmin; x <= xmax - dx; x += dx)
            {
                for (double z = zmin; z <= zmax - dz; z += dx)
                {
                    // Make points at the corners of the surface
                    // over (x, z) - (x + dx, z + dz).
                    Point3D p00 = new Point3D(x, F(x, z), z);
                    Point3D p10 = new Point3D(x + dx, F(x + dx, z), z);
                    Point3D p01 = new Point3D(x, F(x, z + dz), z + dz);
                    Point3D p11 = new Point3D(x + dx, F(x + dx, z + dz), z + dz);

                    // Add the triangles.
                    AddTriangle(mesh, p00, p01, p11);
                    AddTriangle(mesh, p00, p11, p10);
                }
            }
            Console.WriteLine("Surface: ");
            Console.WriteLine("    " + mesh.Positions.Count + " points");
            Console.WriteLine("    " + mesh.TriangleIndices.Count / 3 + " triangles");
            Console.WriteLine();

            // Make the surface's material using a solid green brush.
            DiffuseMaterial surface_material = new DiffuseMaterial(Brushes.LightGreen);

            // Make the surface's model.
            SurfaceModel = new GeometryModel3D(mesh, surface_material);

            // Make the surface visible from both sides.
            SurfaceModel.BackMaterial = surface_material;

            // Add the model to the model groups.
            model_group.Children.Add(SurfaceModel);

            // Make a wireframe.
            double thickness = 0.03;
#if SURFACE2
            thickness = 0.01
#endif
            MeshGeometry3D wireframe = mesh.ToWireframe(thickness);
            DiffuseMaterial wireframe_material = new DiffuseMaterial(Brushes.Red);
            WireframeModel = new GeometryModel3D(wireframe, wireframe_material);
            model_group.Children.Add(WireframeModel);
            Console.WriteLine("Wireframe: ");
            Console.WriteLine("    " + wireframe.Positions.Count + " points");
            Console.WriteLine("    " + wireframe.TriangleIndices.Count / 3 + " triangles");
            Console.WriteLine();

            // Make the normals.
            MeshGeometry3D normals = mesh.ToTriangleNormals(0.5, thickness);
            DiffuseMaterial normals_material = new DiffuseMaterial(Brushes.Blue);
            NormalsModel = new GeometryModel3D(normals, normals_material);
            model_group.Children.Add(NormalsModel);
            Console.WriteLine("Normals: ");
            Console.WriteLine("    " + normals.Positions.Count + " points");
            Console.WriteLine("    " + normals.TriangleIndices.Count / 3 + " triangles");
            Console.WriteLine();
        }

        // The function that defines the surface we are drawing.
        private double F(double x, double z)
        {
#if SURFACE2
            const double two_pi = 2 * 3.14159265;
            double r2 = x * x + z * z;
            double r = Math.Sqrt(r2);
            double theta = Math.Atan2(z, x);
            return Math.Exp(-r2) * Math.Sin(two_pi * r) * Math.Cos(3 * theta);
#else
            double r2 = x * x + z * z;
            return 8 * Math.Cos(r2 / 2) / (2 + r2);
#endif
        }

        // Add a triangle to the indicated mesh.
        // If the triangle's points already exist, reuse them.
        private void AddTriangle(MeshGeometry3D mesh, Point3D point1, Point3D point2, Point3D point3)
        {
            object o = new object();
            lock (o)
            {
                // Get the points' indices.
                int index1 = AddPoint(mesh.Positions, point1);
                int index2 = AddPoint(mesh.Positions, point2);
                int index3 = AddPoint(mesh.Positions, point3);

                // Create the triangle.
                mesh.TriangleIndices.Add(index1);
                mesh.TriangleIndices.Add(index2);
                mesh.TriangleIndices.Add(index3);
            }
        }

        // A dictionary to hold points for fast lookup.
        private Dictionary<Point3D, int> PointDictionary =
            new Dictionary<Point3D, int>();

        // If the point already exists, return its index.
        // Otherwise create the point and return its new index.
        private int AddPoint(Point3DCollection points, Point3D point)
        {
            // If the point is in the point dictionary,
            // return its saved index.
            if (PointDictionary.ContainsKey(point))
                return PointDictionary[point];

            // We didn't find the point. Create it.
            points.Add(point);
            PointDictionary.Add(point, points.Count - 1);
            return points.Count - 1;
        }

        // Adjust the camera's position.
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    CameraPhi += CameraDPhi;
                    if (CameraPhi > Math.PI / 2.0) CameraPhi = Math.PI / 2.0;
                    break;
                case Key.Down:
                    CameraPhi -= CameraDPhi;
                    if (CameraPhi < -Math.PI / 2.0) CameraPhi = -Math.PI / 2.0;
                    break;
                case Key.Left:
                    CameraTheta += CameraDTheta;
                    break;
                case Key.Right:
                    CameraTheta -= CameraDTheta;
                    break;
                case Key.Add:
                case Key.OemPlus:
                    CameraR -= CameraDR;
                    if (CameraR < CameraDR) CameraR = CameraDR;
                    break;
                case Key.Subtract:
                case Key.OemMinus:
                    CameraR += CameraDR;
                    break;
            }

            // Update the camera's position.
            PositionCamera();
        }

        // Position the camera.
        private void PositionCamera()
        {
            // Calculate the camera's position in Cartesian coordinates.
            double y = CameraR * Math.Sin(CameraPhi);
            double hyp = CameraR * Math.Cos(CameraPhi);
            double x = hyp * Math.Cos(CameraTheta);
            double z = hyp * Math.Sin(CameraTheta);
            TheCamera.Position = new Point3D(x, y, z);

            // Look toward the origin.
            TheCamera.LookDirection = new Vector3D(-x, -y, -z);

            // Set the Up direction.
            TheCamera.UpDirection = new Vector3D(0, 1, 0);

            // Console.WriteLine("Camera.Position: (" + x + ", " + y + ", " + z + ")");
        }

        // Show and hide the appropriate GeometryModel3Ds.
        private void chkContents_Click(object sender, RoutedEventArgs e)
        {
            // Remove the GeometryModel3Ds.
            for (int i = MainModel3Dgroup.Children.Count - 1; i >= 0; i--)
            {
                if (MainModel3Dgroup.Children[i] is GeometryModel3D)
                    MainModel3Dgroup.Children.RemoveAt(i);
            }

            // Add the selected GeometryModel3Ds.
            if ((SurfaceModel != null) && ((bool)chkSurface.IsChecked))
                MainModel3Dgroup.Children.Add(SurfaceModel);
            if ((WireframeModel != null) && ((bool)chkWireframe.IsChecked))
                MainModel3Dgroup.Children.Add(WireframeModel);
            if ((NormalsModel != null) && ((bool)chkNormals.IsChecked))
                MainModel3Dgroup.Children.Add(NormalsModel);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gr = new Form1();
            WindowInteropHelper wih = new WindowInteropHelper(this);
            wih.Owner = gr.Handle;
            gr.Show();
        }
        bool exist(Point3D[] ss, List<Point3D[]> d)
        {
            if (d.Count == 0)
                return false;
            for (int i = 0; i < d.Count; i++)
            {
                if (ss[0].X == d[i][0].X && ss[0].Y == d[i][0].Y && ss[0].Z == d[i][0].Z)
                {
                    if (ss[1].X == d[i][1].X && ss[1].Y == d[i][1].Y && ss[1].Z == d[i][1].Z)
                    {
                        if (ss[2].X == d[i][2].X && ss[2].Y == d[i][2].Y && ss[2].Z == d[i][2].Z)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        double minraddpoints(List<Point3D> p0)
        {
            double r = double.MaxValue;
            for (int i = 0; i < p0.Count; i++)
            {
                for (int j = 0; j < p0.Count; j++)
                {

                    double a = Math.Sqrt((p0[i].X - p0[j].X) * (p0[i].X - p0[j].X) + (p0[i].Y - p0[j].Y) * (p0[i].Y - p0[j].Y) + (p0[i].Z - p0[j].Z) * (p0[i].Z - p0[j].Z));

                    if (a < r && a != 0)
                        r = a;
                }
            }
            return r;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try { if (gr != null)
                {
                    if (gr.a != null)
                    {
                        if (gr.a._3DReady)
                        {
                            //creation of target
                            //list found of 3d PointsAdd
                            List<Point3D> PointsAddp0 = new List<Point3D>();
                            List<Point3D> PointsAddp1 = new List<Point3D>();
                            for (int i = 0; i < gr.a.cx; i++)
                            {
                                for (int j = 0; j < gr.a.cyp0; j++)
                                {
                                    if (gr.a.c[i, j, 0] != 0 || gr.a.c[i, j, 1] != 0 || gr.a.c[i, j, 2] != 0)
                                    {
                                        Point3D s = new Point3D(i, j, (gr.a.c[i, j, 0] + gr.a.c[i, j, 1] + gr.a.c[i, j, 2]) / 3);
                                        PointsAddp0.Add(s);
                                    }
                                }
                            }
                            for (int i = 0; i < gr.a.cx; i++)
                            {
                                for (int j = gr.a.cyp0; j < gr.a.cyp1; j++)
                                {
                                    if (gr.a.c[i, j, 0] != 0 || gr.a.c[i, j, 1] != 0 || gr.a.c[i, j, 2] != 0)
                                    {
                                        Point3D s = new Point3D(i, j, (gr.a.c[i, j, 0] + gr.a.c[i, j, 1] + gr.a.c[i, j, 2]) / 3);
                                        PointsAddp1.Add(s);
                                    }
                                }
                            }
                            if (PointsAddp0.Count >= 3 || PointsAddp1.Count >= 3)
                            {
                                  double minrp0 = minraddpoints(PointsAddp0);
                                double minrp1 = minraddpoints(PointsAddp0);
                                MessageBox.Show("Add capable...p0! " + PointsAddp0.Count.ToString() + " p1! " + PointsAddp1.Count.ToString() + " points. with minrp0 " + minrp0.ToString() + " with minrp1 " + minrp1.ToString());
                                if (PointsAddp0.Count > 35 || PointsAddp1.Count > 35)
                                {
                                    List<Point3D> xxxp0 = new List<Point3D>();

                                    List<Point3D> xxxp1 = new List<Point3D>();

                                    int f = (new Triangle()).reduceCountOfpoints(ref PointsAddp0, minrp0 * 2, 35.0 / (double)PointsAddp0.Count, ref xxxp0, System.Convert.ToDouble(gr.textBox1.Text));
                                    f = f + (new Triangle()).reduceCountOfpoints(ref PointsAddp1, minrp1 * 2, 35.0 / (double)PointsAddp1.Count, ref xxxp1, System.Convert.ToDouble(gr.textBox1.Text));
                                    if (xxxp0.Count > 1)
                                    {
                                        PointsAddp0 = xxxp0;
                                    }
                                    if (xxxp1.Count > 1)
                                    {
                                        PointsAddp1 = xxxp1;
                                    }
                                    MessageBox.Show("reduced...p0! " + PointsAddp0.Count.ToString() + " points." + "reduced...p1! " + PointsAddp1.Count.ToString() + " points.");

                                }
                                CurvedSystems addpoint0 = new CurvedSystems(PointsAddp0);
                                List<double[]> p0 = addpoint0.CreateQuficientofCurved();
                                CurvedSystems addpoint1 = new CurvedSystems(PointsAddp1);
                                List<double[]> p1= addpoint1.CreateQuficientofCurved();
                                MessageBox.Show("queficients complete! p0: " + (p0 != null).ToString() + " p1: " + (p1 != null).ToString());

                                // Give the camera its initial position.
                                TheCamera = new PerspectiveCamera();
                                TheCamera.FieldOfView = 60;
                                MainViewport.Camera = TheCamera;
                                PositionCamera();

                                // Define lights.
                                DefineLights();
                                MeshGeometry3D mesh = new MeshGeometry3D();
                                Model3DGroup model_group = MainModel3Dgroup;
                                /*
                                List<Point3D> PointsAdd = PointsAddp0;
                                double minr = minraddpoints(PointsAdd);

                                List<Point3D[]> d = new List<Point3D[]>();
                                List<Point3D> dd = new List<Point3D>();

                                 var output = Task.Factory.StartNew(() =>
                                {

                                    ParallelOptions po = new ParallelOptions(); po.MaxDegreeOfParallelism =System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, PointsAdd.Count, i =>
                                   {

                                       ParallelOptions poo = new ParallelOptions(); poo.MaxDegreeOfParallelism =System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, PointsAdd.Count, j =>
                                         {//float[,,] cc = new float[(maxr - minr + 1), (maxteta - minteta + 1), 3];
                                             ParallelOptions ppoio = new ParallelOptions(); ppoioMaxDegreeOfParallelism =System.Threading.PlatformHelper.ProcessorCount; Parallel.For(0, PointsAdd.Count, k =>
                                              {
                                              
                                                 if ((new Triangle()).boundry(i, j, k))
                                                return;

                                            Point3D[] ss = new Point3D[3];
                                            ss[0] = PointsAdd[i];
                                            ss[1] = PointsAdd[j];
                                            ss[2] = PointsAdd[k];
                                            ss = ImprovmentSort.Do(ss);
                                            //if (!(new Triangle()).distancesaticfied(ss[0], ss[1], ss[2], minr))
                                            //continue;
                                            if (!exist(ss, d))
                                            {
                                                d.Add(ss);
                                                if ((new Triangle()).externalMuliszerotow(ss[0], ss[1], ss[2], PointsAdd, dd) == 0)
                                                {
                                                          this.Dispatcher.Invoke(() =>
                                                        {
                                                             dd.Add(ss[0]);
                                                    dd.Add(ss[1]);
                                                    dd.Add(ss[2]);
                                                  var output1 = Task.Factory.StartNew(() => AddTriangle(mesh, PointsAdd[i], PointsAdd[j], PointsAdd[k]));
                                                        });
                                                      }
                                                  }

                                              });
                                         });
                                   });
                                });
                                output.Wait();
                                */
                                List<Point3D> PointsAdd = PointsAddp0;

                                makeListCenteralized(ref PointsAdd);

                                double minr = minraddpoints(PointsAdd);
                                List<Point3D[]> d = new List<Point3D[]>();
                                List<Point3D> dd = new List<Point3D>();

                                for (int i = 0; i < PointsAdd.Count; i++)
                                {
                                    for (int j = 0; j < PointsAdd.Count; j++)
                                    {//float[,,] cc = new float[(maxr - minr + 1), (maxteta - minteta + 1), 3];
                                        for (int k = 0; k < PointsAdd.Count; k++)
                                        {
                                            if ((new Triangle()).boundry(i, j, k))
                                                continue;

                                            Point3D[] ss = new Point3D[3];
                                            ss[0] = PointsAdd[i];
                                            ss[1] = PointsAdd[j];
                                            ss[2] = PointsAdd[k];
                                            ss = ImprovmentSort.Do(ss);
                                            //if (!(new Triangle()).distancesaticfied(ss[0], ss[1], ss[2], minr))
                                            //continue;
                                            if (!exist(ss, d))
                                            {
                                                d.Add(ss);
                                                if ((new Triangle()).externalMuliszerotow(ss[0], ss[1], ss[2], PointsAdd, dd) == 0)
                                                {
                                                    dd.Add(ss[0]);
                                                    dd.Add(ss[1]);
                                                    dd.Add(ss[2]);
                                                    AddTriangle(mesh, ss[0], ss[1], ss[2]);
                                                }
                                            }

                                        }
                                    }
                                }
                                                        
                            // Make a mesh to hold the surface.
                            Console.WriteLine("Surface: ");
                                Console.WriteLine("    " + mesh.Positions.Count + " Points");
                                Console.WriteLine("    " + mesh.TriangleIndices.Count / 3 + " triangles");
                                Console.WriteLine();

                                // Make the surface's material using a solid green brush.
                                DiffuseMaterial surface_material = new DiffuseMaterial(Brushes.LightGreen);

                                // Make the surface's model.
                                SurfaceModel = new GeometryModel3D(mesh, surface_material);

                                // Make the surface visible from both sides.
                                SurfaceModel.BackMaterial = surface_material;

                                // Add the model to the model groups.
                                model_group.Children.Add(SurfaceModel);

                                // Make a wireframe.
                                double thickness = 0.03;
#if SURFACE2
            thickness = 0.01
#endif
                                MeshGeometry3D wireframe = mesh.ToWireframe(thickness);
                                DiffuseMaterial wireframe_material = new DiffuseMaterial(Brushes.Red);
                                WireframeModel = new GeometryModel3D(wireframe, wireframe_material);
                                model_group.Children.Add(WireframeModel);
                                Console.WriteLine("Wireframe: ");
                                Console.WriteLine("    " + wireframe.Positions.Count + " Points");
                                Console.WriteLine("    " + wireframe.TriangleIndices.Count / 3 + " triangles");
                                Console.WriteLine();

                                // Make the normals.
                                MeshGeometry3D normals = mesh.ToTriangleNormals(0.5, thickness);
                                DiffuseMaterial normals_material = new DiffuseMaterial(Brushes.Blue);
                                NormalsModel = new GeometryModel3D(normals, normals_material);
                                model_group.Children.Add(NormalsModel);
                                Console.WriteLine("Normals: ");
                                Console.WriteLine("    " + normals.Positions.Count + " Points");
                                Console.WriteLine("    " + normals.TriangleIndices.Count / 3 + " triangles");
                                Console.WriteLine();

                                // Add the group of models to a ModelVisual3D.
                                ModelVisual3D model_visual = new ModelVisual3D();
                                model_visual.Content = MainModel3Dgroup;

                                // Display the main visual in the viewportt.
                                MainViewport.Children.Add(model_visual);
                            }
                            // if (PointsAdd.Count > 0)
                            // Window_Loaded(sender, e);
                        }

                    }

                }
            } catch (Exception t) { MessageBox.Show(t.ToString()); Log(t); }
        }

        public static void makeListCenteralized(ref List<Point3D> non)
        {
            double maxx = maxGetListX(non);
            double maxy = maxGetListY(non);
            double maxz = maxGetListZ(non);

            double minx = minGetListX(non);
            double miny = minGetListY(non);
            double minz = minGetListZ(non);

            double disx = minx + (maxx - minx) / 2;
            double disy = miny + (maxy - miny) / 2;
            double disz = minz + (maxz - minz) / 2;
            for (int i = 0; i < non.Count; i++)
            {
                non[i] = new Point3D(non[i].X - disx, non[i].Y - disy, non[i].Z - disz);
            }

        }


        static double maxGetListX(List<Point3D> d)
        {
            int inex = -1;
            double max = float.MinValue;
            for (int i = 0; i < d.Count; i++)
            {
                if (max < d[i].X)
                {
                    max = d[i].X;
                    inex = i;
                }
            }
            return d[inex].X;
        }
        static double maxGetListY(List<Point3D> d)
        {
            int inex = -1;
            double max = float.MinValue;
            for (int i = 0; i < d.Count; i++)
            {
                if (max < d[i].Y)
                {
                    max = d[i].Y;
                    inex = i;
                }
            }
            return d[inex].Y;
        }
        static double maxGetListZ(List<Point3D> d)
        {
            int inex = -1;
            double max = float.MinValue;
            for (int i = 0; i < d.Count; i++)
            {
                if (max < d[i].Z)
                {
                    max = d[i].Z;
                    inex = i;
                }
            }
            return d[inex].Z;
        }
        static double minGetListX(List<Point3D> d)
        {
            int inex = -1;
            double min = float.MaxValue;
            for (int i = 0; i < d.Count; i++)
            {
                if (min > d[i].X)
                {
                    min = d[i].X;
                    inex = i;
                }
            }
            return d[inex].X;
        }
        static double minGetListY(List<Point3D> d)
        {
            int inex = -1;
            double min = float.MaxValue;
            for (int i = 0; i < d.Count; i++)
            {
                if (min > d[i].X)
                {
                    min = d[i].X;
                    inex = i;
                }
            }
            return d[inex].Y;
        }
        static double minGetListZ(List<Point3D> d)
        {
            int inex = -1;
            double min = float.MaxValue;
            for (int i = 0; i < d.Count; i++)
            {
                if (min > d[i].Z)
                {
                    min = d[i].Z;
                    inex = i;
                }
            }
            return d[inex].Z;
        }
    }
}

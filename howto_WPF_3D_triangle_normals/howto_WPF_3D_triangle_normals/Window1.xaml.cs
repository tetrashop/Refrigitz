﻿//#define SURFACE2

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
            DefineModel(MainModel3Dgroup);

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
                    return true;
            }
            return false;
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
                            List<Point3D> PointsAdd = new List<Point3D>();
                            for (int i = 0; i < gr.a.cx; i++)
                            {
                                for (int j = 0; j < gr.a.cy; j++)
                                {
                                    if (gr.a.c[i, j, 0] != 0)
                                    {
                                        Point3D s = new Point3D(i, j, gr.a.c[i, j, 0]);
                                        PointsAdd.Add(s);
                                    }
                                    else
                                        if (gr.a.c[i, j, 1] != 0)
                                    {
                                        Point3D s = new Point3D(i, j, gr.a.c[i, j, 1]);
                                        PointsAdd.Add(s);
                                    }
                                    else
                                        if (gr.a.c[i, j, 2] != 0)
                                    {
                                        Point3D s = new Point3D(i, j, gr.a.c[i, j, 2]);
                                        PointsAdd.Add(s);
                                    }



                                }
                            }
                            if (PointsAdd.Count >= 3)
                            {
                                MessageBox.Show("Add capable...! " + PointsAdd.Count.ToString() + " points.");
                                // Give the camera its initial position.
                                TheCamera = new PerspectiveCamera();
                                TheCamera.FieldOfView = 60;
                                MainViewport.Camera = TheCamera;
                                PositionCamera();

                                // Define lights.
                                DefineLights();
                                MeshGeometry3D mesh = new MeshGeometry3D();
                                Model3DGroup model_group = MainModel3Dgroup;

                                var output = Task.Factory.StartNew(() =>
                                {

                                    ParallelOptions po = new ParallelOptions(); po.MaxDegreeOfParallelism = 2; Parallel.For(0, PointsAdd.Count, i =>
                                   {

                                       ParallelOptions poo = new ParallelOptions(); poo.MaxDegreeOfParallelism = 2; Parallel.For(i + 1, PointsAdd.Count, j =>
                                         {//float[,,] cc = new float[(maxr - minr + 1), (maxteta - minteta + 1), 3];
                                             ParallelOptions ppoio = new ParallelOptions(); ppoio.MaxDegreeOfParallelism = 2; Parallel.For(j + 1, PointsAdd.Count, k =>
                                              {
                                                  List<Point3D[]> d = new List<Point3D[]>();

                                                  Point3D[] ss = new Point3D[3];
                                                  ss[0] = PointsAdd[i];
                                                  ss[1] = PointsAdd[j];
                                                  ss[2] = PointsAdd[k];
                                                  ss = ImprovmentSort.Do(ss);
                                                  if (!exist(ss, d))
                                                  {
                                                      d.Add(ss);
                                                      if ((new Triangle()).externalMuliszerotow(PointsAdd[i], PointsAdd[j], PointsAdd[k], PointsAdd) == 0)
                                                      {
                                                          this.Dispatcher.Invoke(() =>
                                                        {
                                                            var output1 = Task.Factory.StartNew(() => AddTriangle(mesh, PointsAdd[i], PointsAdd[j], PointsAdd[k]));
                                                        });
                                                      }
                                                  }

                                              });
                                         });
                                   });
                                });
                                output.Wait();

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
    }
}

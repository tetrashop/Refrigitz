using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
namespace Run
{
    public class Program
    { //Error Handling.
        private static readonly string Root = System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);

        private static void Log(Exception ex)
        {
            try
            {
                object a = new object();
                lock (a)
                {
                    string stackTrace = ex.ToString();
                    File.AppendAllText(Root + "\\ErrorProgramRun.txt", stackTrace + ": On" + DateTime.Now.ToString()); // path of file where stack trace will be stored.
                }
            }
            catch (Exception t) { Log(t); }
        }

        public static void Main(string[] args)
        {
            string Root = System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);

            List<Process> a = new List<Process>();
            a.AddRange(Process.GetProcessesByName("Refrigtz"));
            if (a.Count >= 1)
            {

                {
                    for (int i = 0; i < a.Count; i++)
                    {
                        try
                        {

                            a[i].Kill();
                        }
                        catch (Exception t) { Log(t); }

                    }

                }
            }


            string FolderLocation = Root;
            int exitCode = 0;

            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo
            {
                //TBeep.Start(); MessageBox.Show("running VisualBasicPowerPacks.Control Of the Program While 15 Second id By User.ClickOk and Finished While 20 Second");
                // Prepare the process to run
                // Enter in the command line arguments, everything you would enter after the executable name itself
                Arguments = "",
                // Enter the executable to run, including the complete path
                FileName = "\"" + FolderLocation + "\\" + "Refrigtz.exe" + "\"",
                // Do you want to show a console window?
                WindowStyle = ProcessWindowStyle.Normal,
                CreateNoWindow = true,
                UseShellExecute = true
            };

            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();
                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }

        }
    }
}

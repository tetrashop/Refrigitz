using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace QuantumRefrigiz
{


    static class Program
    {
        public static long SomeExtremelyLargeNumber { get; private set; }

        /// <summary>
        /// The main en

        static void Log(Exception ex)
        {

            Object a = new Object();
            lock (a)
            {
                string stackTrace = ex.ToString();
                Helper.WaitOnUsed(AllDraw.Root + "\\ErrorProgramRun.txt"); File.AppendAllText(AllDraw.Root + "\\ErrorProgramRun.txt", stackTrace + ": On" + DateTime.Now.ToString()); // path of file where stack trace will be stored.
            }

        }
        public static void IncreASingThreadPerformance()
        {
            Process.GetCurrentProcess().PriorityBoostEnabled = true;
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;

            // Of course this only affects the main thread rather than child threads.
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            Int64 seed = SomeExtremelyLargeNumber; // Millions of digits.
            int[] nums = Enumerable.Range(0, 1000000).ToArray();
            long total = 0;

            // Use type parameter to make subtotal a long, not an int
            ParallelOptions po = new ParallelOptions(); po.MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount; Parallel.For<long>(0, nums.Length, () => 0, (j, loop, subtotal) =>
 {
     subtotal += nums[j];
     return subtotal;
 },
 (x) => Interlocked.Add(ref total, x)
 );


        }
        public class StackOverflowDetector
        {
            static void CheckStackDepth()
            {
                if (new StackTrace().FrameCount > 60) // some arbitrary limit
                {
                    throw new StackOverflowException("Bad thread.");
                }
            }

            public static int Recur()
            {
                CheckStackDepth();
                return 0;
            }
        }


        //Main Programm.
        [STAThread]
        public static void Main()
        {
            // IncreASingThreadPerformance();
            //Intiate  Program Load and Calling.
            //Application.EnableVisualStyles();
            //

            //Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new Load());


        }
    }
}
//End of Documents.
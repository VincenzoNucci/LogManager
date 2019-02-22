using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using LogManager;
using System.Linq;
using System.Timers;

namespace Benchmarks
{
    class BenchmarkMaxTimeFullBuffers : BenchmarkTool
    {
        public Stopwatch s = new Stopwatch();
        public StreamWriter sw = null;
        public System.Timers.Timer timer = new System.Timers.Timer();
        public bool stopCriterionReached = false;
        public string path = @"D:\[]- Benchmark3NUOVO.txt";

        public long StartingDBSize { get; set; }
        public long TargetDBSize { get; set; }
        public long PreviousBatchSize { get; set; }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //timer.Stop();

            s.Restart();
            long batchSize = LogManager.TraceLog.Flush();
            s.Stop();
            double time = s.Elapsed.TotalMilliseconds;
            //string measure = "bytes";
            long t = LogManager.TraceLog.CurrentDBSize();
            PreviousBatchSize += batchSize;

            sw.WriteLine(batchSize + ", " + time.ToString().Replace(",", ".") + ", " + t);
            Console.WriteLine("flushed " + batchSize + " logs in " + time + " ms with size: " + t + " bytes");

            if (t > this.TargetDBSize)
            {
                stopCriterionReached = true;
            }

            //timer.Start();
        }

        /// <summary>
        /// Create a new instance of benchmark class and start with specified parameters
        /// </summary>
        /// <param name="threads"></param>
        /// <param name="nlogs"></param>
        /// <param name="resolution"></param>
        /// <param name="collectionName"></param>
        public BenchmarkMaxTimeFullBuffers(int threads, int nlogs, double timerspan, long startingdbsize, long targetdbsize, string collectionName) : base(threads, nlogs, timerspan)
        {
            this.StartingDBSize = startingdbsize;
            this.TargetDBSize = targetdbsize;
            PreviousBatchSize = 0;
            if (sw == null)
            {
                //sw = new StreamWriter(@"D:\[T-" + this.ThreadNumber + ", L-" + this.MaxLogs + ", R-" + this.Resolution + ", S-" + this.StartingDBSize + ", Ta-" + this.TargetDBSize + "]- BenchmarkFlushTimeSize.txt");
            }

            File.AppendAllText(path, "batchSize,time,Currentsize\n");

            Start(collectionName);
        }

        /// <summary>
        /// Starts the specified benchmark by this class
        /// </summary>
        /// <param name="collectionName"></param>
        public override void Start(string collectionName)
        {

            try
            {

                LogManager.TraceLog.Connect(collectionName);
            }
            catch (TraceLogStateException e)
            {

            }
            TraceLog.Write(new Log(LogLevel.CRITICAL, ""));
            TraceLog.Flush();
            //collection has not the preferred size to operate with
            if (TraceLog.CurrentDBSize() < StartingDBSize)
            {
                //Start 5 threads to write continuonsly until the desired size is reached
                List<Task> fillTasks = new List<Task>();
                for (int i = 0; i < 200; i++)
                {
                    fillTasks.Add(Task.Factory.StartNew(FillThreadWork));
                }
                Task.WaitAll(fillTasks.ToArray());
            }


            List<Task> tasks = new List<Task>();


            //timer.Interval = this.Resolution;
            timer.Elapsed += Timer_Elapsed;

            //Starts the only thread for this kind of benchmark
            for (int i = 0; i < this.ThreadNumber; i++)
            {
                tasks.Add(Task.Factory.StartNew(ThreadWork));
            }



            //timer.Start();


            //Launch the only thread
            Task.WaitAll(tasks.ToArray());



            //timer.Stop();


            sw.Close();
            Finished = true;
        }

        public void FillThreadWork()
        {
            while (TraceLog.CurrentDBSize() < StartingDBSize)
            {
                Log l = new Log(LogLevel.DEBUG, "01/03/2018 11:36:25	GenericRegulator	REG-PRESS-FLUX-P2	False	Regolatore Arrestato.	1	5");

                LogManager.TraceLog.Write(l);
            }
        }

        public override void ThreadWork()
        {
            int timeToSleep = 0;
            int j = 0;
            const int STEP = 64;
            long i = 0;
            for (int h = 0; h < 256; h++)
            {
                for (int p = 0; p < 64 * h; p++)
                    TraceLog.Write(new Log(LogLevel.DEBUG, "01/03/2018 11:36:25	GenericRegulator	REG-PRESS-FLUX-P2	False	Regolatore Arrestato.	1	5"));

                //if (h % STEP == 0)
                {
                    s.Restart();
                    long batchSize = LogManager.TraceLog.Flush();
                    s.Stop();
                    double time = s.Elapsed.TotalMilliseconds;
                    //string measure = "bytes";
                    long t = LogManager.TraceLog.CurrentDBSize();
                    PreviousBatchSize += batchSize;

                    File.AppendAllText(path, batchSize + ", " + time.ToString().Replace(",", ".") + ", " + t + "\n");
                    Console.WriteLine("flushed " + batchSize + " logs in " + time + " ms with size: " + t + " bytes");


                }


            }

            //sw.WriteLine("thread: " + tmp.ToString().Remove(6) + " finished @time: " + s.ElapsedMilliseconds);
            //Console.WriteLine("thread: " + tmp.ToString().Remove(6) + " finished @time: " + s.ElapsedMilliseconds);

        }
    }
}
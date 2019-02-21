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
    class BenchmarkFlushTimeSize : BenchmarkTool
    {
        public Stopwatch s = new Stopwatch();
        public StreamWriter sw = null;
        public System.Timers.Timer timer = new System.Timers.Timer();
        public bool stopCriterionReached = false;

        public long DBSize { get; set; }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //timer.Stop();
            
            s.Restart();
            long batchSize = LogManager.TraceLog.Flush();
            s.Stop();
            double time = s.Elapsed.TotalMilliseconds;
            //string measure = "bytes";
            long t = LogManager.TraceLog.CurrentDBSize();


            sw.WriteLine("flushed " + batchSize + " logs in " + time + " ms with size: " + t + " bytes");
            Console.WriteLine("flushed " + batchSize + " logs in " + time + " ms with size: " + t + " bytes");

            if(t > this.DBSize)
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
        public BenchmarkFlushTimeSize(int threads, int nlogs, double timerspan, long dbsize, string collectionName) : base(threads,nlogs,timerspan)
        {
            this.DBSize = dbsize;
            if(sw == null)
            {
                sw = new StreamWriter(@"F:\[T-" + this.ThreadNumber + ", L-" + this.MaxLogs + ", R-" + this.Resolution + ", S-" + this.DBSize+"]- BenchmarkFlushTimeSize.txt");
            }
            
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
            //Thread.Sleep(2000);
            List<Task> tasks = new List<Task>();
            //System.Timers.Timer timer = new System.Timers.Timer();

            timer.Interval = this.Resolution;
            timer.Elapsed += Timer_Elapsed;

            for (int i = 0; i < this.ThreadNumber; i++)
            {
                tasks.Add(Task.Factory.StartNew(ThreadWork));
            }



            timer.Start();



            Task.WaitAll(tasks.ToArray());



            //timer.Stop();


            sw.Close();
            Finished = true;
        }

        public override void ThreadWork()
        {
            int timeToSleep = 0;

            const int STEP = 10;
            long i = 0;
            while (!stopCriterionReached)
            {
                //ogni 10 log fa la media del tempo di inserimento e la inserisce nel dictionary delle medie
                if (i % STEP == 0)
                {

                    //e sceglie anche un nuovo tempo di attesa random
                    timeToSleep = new Random().Next(10, 5000);

                }
                Thread.Sleep(timeToSleep);
                Log l = new Log(LogLevel.DEBUG, "01/03/2018 11:36:25	GenericRegulator	REG-PRESS-FLUX-P2	False	Regolatore Arrestato.	1	5");

                LogManager.TraceLog.Write(l);


                i++;
            }
            //sw.WriteLine("thread: " + tmp.ToString().Remove(6) + " finished @time: " + s.ElapsedMilliseconds);
            //Console.WriteLine("thread: " + tmp.ToString().Remove(6) + " finished @time: " + s.ElapsedMilliseconds);

        }
    }
}

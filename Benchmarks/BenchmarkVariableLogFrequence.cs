using LogManager;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Benchmarks
{
    class BenchmarkVariableLogFrequence : BenchmarkTool
    {

        //il concurrent dictionary che conterrà riferimenti ad ogni thread e la sua media parziale
        //di inserimento di un thread nel buffer con la concorrenza simulata ogni 100 logs
        public ConcurrentDictionary<Guid, double> timeTable = new ConcurrentDictionary<Guid, double>();
        //la lista che conterrà la media delle medie
        public List<double> averageTime = new List<double>();
        public Stopwatch s = new Stopwatch();
        public StreamWriter sw = null;

        public BenchmarkVariableLogFrequence(int threads, int maxLogs, double resolution, string collectionName) : base(threads, maxLogs, resolution)
        {
            if (sw == null)
            {
                sw = new StreamWriter(@"F:\[T-" + this.ThreadNumber + ", L-" + this.MaxLogs + ", R-"+ this.Resolution+"]- BenchmarkVariableLogFrequence.txt");
            }
            Start(collectionName);
        }

        public override void Start(string collectionName)
        {
            try
            {

                LogManager.TraceLog.Connect(collectionName);
            }catch (TraceLogStateException e)
            {
                
            }

            List<Task> tasks = new List<Task>();
            System.Timers.Timer timer = new System.Timers.Timer();
            //ogni 100 ms calcola la media totale
            timer.Interval = this.Resolution;
            timer.Elapsed += Timer_Elapsed;

            sw.WriteLine("average of " + ((averageTime.Count == 0) ? 0 : averageTime.Average()) + " ms @time: " + s.ElapsedMilliseconds);
            Console.WriteLine("average of " + ((averageTime.Count == 0) ? 0 : averageTime.Average()) + " ms @time: " + s.ElapsedMilliseconds);

            for (int i = 0; i < this.ThreadNumber; i++)
            {
                tasks.Add(Task.Factory.StartNew(ThreadWork));
            }

            //Stopwatch s = new Stopwatch();

            timer.Start();

            s.Start();

            Task.WaitAll(tasks.ToArray());

            s.Stop();

            timer.Stop();
            //tempo totale di completamento di inserire 1000 logs per 100 threads
            double time = s.Elapsed.TotalMilliseconds;

            sw.WriteLine("time to insert " + this.ThreadNumber + "x" + this.MaxLogs + " logs in buffers: " + time);
            Console.WriteLine("time to insert " + this.ThreadNumber + "x" + this.MaxLogs + " logs in buffers: " + time);

            LogManager.TraceLog.Flush();

            //StreamWriter sw = new StreamWriter(@"D:\new_benchmark_mongodb.txt");
            //foreach (double d in averageTime)
            //{
            //    sw.WriteLine(d);
            //}
            sw.Close();
            Finished = true;
        }

        public override void ThreadWork()
        {
            //lista del tempo di scrittura di un log di ogni thread
            List<double> writeTime = new List<double>();
            //identificativo di ogni thread per le medie
            Guid tmp = Guid.NewGuid();

            Stopwatch sP = new Stopwatch();

            timeTable.AddOrUpdate(tmp, 0, (a, b) => { return 0; });
            int timeToSleep = 0;
            const int NLOGS = 100;
            const int STEP = 10;
            for (int i = 0; i < NLOGS; i++)
            {
                //ogni 10 log fa la media del tempo di inserimento e la inserisce nel dictionary delle medie
                if (i % STEP == 0)
                {
                    if (writeTime.Count > 0)
                        timeTable.AddOrUpdate(tmp, writeTime.Average(), (a, b) => { return writeTime.Average(); });
                    writeTime.Clear();
                    //e sceglie anche un nuovo tempo di attesa random
                    timeToSleep = new Random().Next(10, 5000);

                }
                Thread.Sleep(timeToSleep);
                Log l = new Log(LogLevel.DEBUG, "01/03/2018 11:36:25	GenericRegulator	REG-PRESS-FLUX-P2	False	Regolatore Arrestato.	1	5");
                sP.Restart();
                LogManager.TraceLog.Write(l);
                sP.Stop();
                writeTime.Add(sP.Elapsed.TotalMilliseconds);
            }
            sw.WriteLine("thread: " + tmp.ToString().Remove(6) + " finished @time: " + s.ElapsedMilliseconds);
            Console.WriteLine("thread: " + tmp.ToString().Remove(6) + " finished @time: " + s.ElapsedMilliseconds);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            double printAverage = 0;
            averageTime.Add(timeTable.Values.Average());
            if (averageTime.Count > 0)
            {

                printAverage = averageTime.Average();
            }
            sw.WriteLine("average of " + printAverage + " ms @time: " + s.ElapsedMilliseconds);
            Console.WriteLine("average of " + printAverage + " ms @time: " + s.ElapsedMilliseconds);
        }
    }
}

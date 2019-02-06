using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using LogManager;

namespace ConsoleTest
{
    class Program
    {
        static List<long> AvgDict = new List<long>();
        const int NLOGS = 50;
        const int NTHREADS = 50;
        static void Print()
        {
            double avg = 0;

<<<<<<< HEAD
<<<<<<< HEAD
=======
=======
>>>>>>> parent of 79ede7c... adjusted classes for benchmark
            const int NLOGS = 10;

>>>>>>> parent of 79ede7c... adjusted classes for benchmark
            Random r = new Random();

            Stopwatch stopw = new Stopwatch();
            for (int i = 0; i < NLOGS; i++)
            {
                //Thread.Sleep(500);
                Log l = new Log(LogLevel.DEBUG, "This is a test log");
                stopw.Restart();

                ArbiterConcurrentTrace.Write(l);

                stopw.Stop();
                long finish = stopw.ElapsedMilliseconds;
                AvgDict.Add(finish);
                //avg += finish;
            }

            //avg = avg / NLOGS;
            //AvgDict.GetOrAdd(Guid.NewGuid(), avg);

        }

        static void Main(string[] args)
        {
<<<<<<< HEAD
<<<<<<< HEAD
            //MongoDB
            //ArbiterConcurrentTrace.BufferSize = 256;
            //ArbiterConcurrentTrace.NumberOfBuffers = 64;

            //ArangoDB
            ArbiterConcurrentTraceArangoDB.BufferSize = 256;
            ArbiterConcurrentTraceArangoDB.NumberOfBuffers = 64;


            //ArbiterConcurrentTrace.Connect("logs");
            ArbiterConcurrentTraceArangoDB.Connect("logs");

            for (int tries = 0; tries < 10; tries++)
=======
            ArbiterConcurrentTrace.BufferSize = 10;
            ArbiterConcurrentTrace.NumberOfBuffers = 5;

=======
            ArbiterConcurrentTrace.BufferSize = 10;
            ArbiterConcurrentTrace.NumberOfBuffers = 5;

>>>>>>> parent of 79ede7c... adjusted classes for benchmark
            ArbiterConcurrentTrace.Connect("TestConcurrent2");
            List<Task> tasks = new List<Task>();
            Stopwatch s = new Stopwatch();
            s.Start();
            for (int i = 0; i < 10; i++)
<<<<<<< HEAD
>>>>>>> parent of 79ede7c... adjusted classes for benchmark
=======
>>>>>>> parent of 79ede7c... adjusted classes for benchmark
            {
                List<Task> tasks = new List<Task>();
                Stopwatch s = new Stopwatch();
                s.Start();
                for (int i = 0; i < NTHREADS; i++)
                {
                    tasks.Add(Task.Factory.StartNew(Print));
                }

                Task.WaitAll(tasks.ToArray());
                s.Stop();
                long time1 = s.ElapsedMilliseconds;
                s.Reset();
                Console.WriteLine("time to write all the logs to the buffers at step " + tries + " : " + time1);
                
                s.Start();
                //ArbiterConcurrentTrace.Flush();
                ArbiterConcurrentTraceArangoDB.Flush();
                s.Stop();
                long time2 = s.ElapsedMilliseconds;
                s.Reset();
                Console.WriteLine("time to flush everything at step " + tries + ": " + time2);
                AvgDict.Add(time1);
                AvgDict.Add( time2);
                using (StreamWriter file = new StreamWriter("D:\\ArbiterConcurrentTrace_benchmarkArangoDB["+tries+"].txt"))
                    for (var k = 0; k < AvgDict.Count; k++)
                        file.WriteLine(AvgDict[k].ToString());
                AvgDict.Clear();
            }
<<<<<<< HEAD
=======

            Task.WaitAll(tasks.ToArray());
            s.Stop();
            long time = s.ElapsedMilliseconds;
            Console.WriteLine(time);
            ArbiterConcurrentTrace.Flush();

            //using (StreamWriter file = new StreamWriter("ArbiterConcurrentTrace_benchmark.txt"))
                //foreach (var entry in AvgDict)
                    //file.WriteLine("{0} , {1}", entry.Key.ToString().Substring(0,4) , entry.Value.ToString().Replace(',','.'));

>>>>>>> parent of 79ede7c... adjusted classes for benchmark
            Console.WriteLine("done");
            Console.ReadLine();

        }
    }
}

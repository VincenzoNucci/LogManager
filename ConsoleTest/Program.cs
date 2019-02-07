#define TRACE_LOG
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
        static ConcurrentDictionary<Guid, double> AvgDict = new ConcurrentDictionary<Guid, double>();
        const int NLOGS = 50;
        const int NTHREADS = 50;

        static void Print()
        {
            double avg = 0;

            Random r = new Random();

            Stopwatch stopw = new Stopwatch();
            for (int i = 0; i < NLOGS; i++)
            {
                //Thread.Sleep(500);
                Log l = new Log(LogLevel.DEBUG, "01/03/2018 11:36:25	GenericRegulator	REG-PRESS-FLUX-P1	False	Regolatore Arrestato.	1	5");
                stopw.Restart();

                //ArbiterConcurrentTrace.Write(l);
                //ArbiterConcurrentTraceArangoDB.Write(l);
                //ArbiterConcurrentTraceCouchDB.Write(l);
                ArbiterConcurrentTraceLiteDB.Write(l);

                stopw.Stop();
                long finish = stopw.ElapsedMilliseconds;
                avg += finish;
            }

            avg = avg / NLOGS;
            AvgDict.GetOrAdd(Guid.NewGuid(), avg);

        }

        static void Main(string[] args)
        {
            //ArbiterConcurrentTrace.BufferSize = 256;
            //ArbiterConcurrentTrace.NumberOfBuffers = 64;

            //ArbiterConcurrentTraceArangoDB.BufferSize = 256;
            //ArbiterConcurrentTraceArangoDB.NumberOfBuffers = 64;

            //ArbiterConcurrentTraceCouchDB.BufferSize = 256;
            //ArbiterConcurrentTraceCouchDB.NumberOfBuffers = 64;

            ArbiterConcurrentTraceLiteDB.BufferSize = 256;
            ArbiterConcurrentTraceLiteDB.NumberOfBuffers = 64;

            //ArbiterConcurrentTrace.Connect("logs");
            //ArbiterConcurrentTraceArangoDB.Connect("logs");
            //ArbiterConcurrentTraceCouchDB.Connect("");
            ArbiterConcurrentTraceLiteDB.Connect("logs");

            for (int iterations = 0; iterations < 10; iterations++)
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
                Console.WriteLine(time1);
               
                s.Restart();

                //ArbiterConcurrentTrace.Flush();
                //ArbiterConcurrentTraceArangoDB.Flush();
                //ArbiterConcurrentTraceCouchDB.Flush();
                ArbiterConcurrentTraceLiteDB.Flush();
                s.Stop();
                long time2 = s.ElapsedMilliseconds;
                using (StreamWriter file = new StreamWriter("D:\\LiteDB_benchmark["+iterations+"].txt"))
                {
                    foreach (var entry in AvgDict)
                        file.WriteLine(entry.Value.ToString().Replace(',', '.'));
                    file.WriteLine("------------");
                    file.WriteLine(time1);
                    file.WriteLine(time2);
                }
                AvgDict.Clear();
            }

            Console.WriteLine("done");
            Console.ReadLine();

        }
    }
}

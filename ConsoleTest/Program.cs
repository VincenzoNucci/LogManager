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
<<<<<<< HEAD
        static ConcurrentDictionary<Guid, double> AvgDict = new ConcurrentDictionary<Guid, double>();
        const int NLOGS = 1000;
        const int NTHREADS = 50;

        static void Print()
        {
            double avg = 0;

            Random r = new Random();
            int timeToWait = 0;
            Stopwatch stopw = new Stopwatch();
            for (int i = 0; i < NLOGS; i++)
            {
                if (i % 100 == 0)
                    timeToWait = r.Next(10, 5000);
                Thread.Sleep(timeToWait);
                Log l = new Log(LogLevel.DEBUG, "01/03/2018 11:36:25	GenericRegulator	REG-PRESS-FLUX-P1	False	Regolatore Arrestato.	1	5");
                stopw.Restart();

                ArbiterConcurrentTrace.Write(l);
                //ArbiterConcurrentTraceArangoDB.Write(l);
                //ArbiterConcurrentTraceCouchDB.Write(l);
                //ArbiterConcurrentTraceLiteDB.Write(l);

                stopw.Stop();
                long finish = stopw.ElapsedMilliseconds;
                avg += finish;
=======
        static void Print()
        {
            const int NLOGS = 200;
            for (int i = 0; i < NLOGS; i++)
            {

                Log l = new Log(LogLevel.DEBUG, "This is a test log");
                LogManager.Trace.Write(l);
>>>>>>> upstream/master
            }
        }

        static void Main(string[] args)
        {
<<<<<<< HEAD
            ArbiterConcurrentTrace.BufferSize = 256;
            ArbiterConcurrentTrace.NumberOfBuffers = 64;

            //ArbiterConcurrentTraceArangoDB.BufferSize = 256;
            //ArbiterConcurrentTraceArangoDB.NumberOfBuffers = 64;

            //ArbiterConcurrentTraceCouchDB.BufferSize = 256;
            //ArbiterConcurrentTraceCouchDB.NumberOfBuffers = 64;

            //ArbiterConcurrentTraceLiteDB.BufferSize = 256;
            //ArbiterConcurrentTraceLiteDB.NumberOfBuffers = 64;

            ArbiterConcurrentTrace.Connect("logs");
            //ArbiterConcurrentTraceArangoDB.Connect("logs");
            //ArbiterConcurrentTraceCouchDB.Connect("");
            //ArbiterConcurrentTraceLiteDB.Connect("logs");

            for (int iterations = 0; iterations < 10; iterations++)
            {
                List<Task> tasks = new List<Task>();
                Stopwatch s = new Stopwatch();
                
                for (int i = 0; i < NTHREADS; i++)
                {
                    tasks.Add(Task.Factory.StartNew(Print));
                }
                s.Start();
                Task.WaitAll(tasks.ToArray());
                s.Stop();
                double time1 = s.Elapsed.TotalMilliseconds;
                Console.WriteLine("tempo totale di inserimento (thread peggiore): "+time1);
               
                s.Restart();

                ArbiterConcurrentTrace.Flush();
                //ArbiterConcurrentTraceArangoDB.Flush();
                //ArbiterConcurrentTraceCouchDB.Flush();
                //ArbiterConcurrentTraceLiteDB.Flush();
                s.Stop();
                double time2 = s.Elapsed.TotalMilliseconds;
                Console.WriteLine("tempo totale di flush: "+time2);
                using (StreamWriter file = new StreamWriter("D:\\MongoDB_benchmark["+iterations+"].txt"))
                {
                    foreach (var entry in AvgDict)
                        file.WriteLine(entry.Value.ToString().Replace(',', '.'));
                    file.WriteLine("------------");
                    file.WriteLine(time1);
                    file.WriteLine(time2);
                }
                AvgDict.Clear();
            }
=======
            LogManager.Trace.Connect("TestConcurrent2");

            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 100; i++)
            {
                tasks.Add(Task.Factory.StartNew(Print));
            }

            Stopwatch s = new Stopwatch();

            s.Start();

            Task.WaitAll(tasks.ToArray());

            s.Stop();

            long time = s.ElapsedMilliseconds;

            Console.WriteLine(time);

            LogManager.Trace.Flush();
>>>>>>> upstream/master

            Console.WriteLine("done");
            Console.ReadLine();

        }
    }
}

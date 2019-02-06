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

        static void Print()
        {
            double avg = 0;

            const int NLOGS = 50;

            Random r = new Random();

            Stopwatch stopw = new Stopwatch();
            for (int i = 0; i < NLOGS; i++)
            {
                //Thread.Sleep(500);
                Log l = new Log(LogLevel.DEBUG, "This is a test log");
                stopw.Restart();

                //ArbiterConcurrentTrace.Write(l);
                ArbiterConcurrentTraceArangoDB.Write(l);

                stopw.Stop();
                long finish = stopw.ElapsedMilliseconds;
                avg += finish;
            }

            avg = avg / NLOGS;
            AvgDict.GetOrAdd(Guid.NewGuid(), avg);

        }

        static void Main(string[] args)
        {
            //ArbiterConcurrentTrace.BufferSize = 10;
            //ArbiterConcurrentTrace.NumberOfBuffers = 5;

            ArbiterConcurrentTraceArangoDB.BufferSize = 256;
            ArbiterConcurrentTraceArangoDB.NumberOfBuffers = 64;

            //ArbiterConcurrentTrace.Connect("TestConcurrent2");
            ArbiterConcurrentTraceArangoDB.Connect("logs");
            List<Task> tasks = new List<Task>();
            Stopwatch s = new Stopwatch();
            s.Start();
            for (int i = 0; i < 50; i++)
            {
                tasks.Add(Task.Factory.StartNew(Print));
            }

            Task.WaitAll(tasks.ToArray());
            s.Stop();
            long time = s.ElapsedMilliseconds;
            Console.WriteLine(time);
            //ArbiterConcurrentTrace.Flush();
            ArbiterConcurrentTraceArangoDB.Flush();

            using (StreamWriter file = new StreamWriter("D:\\ArbiterConcurrentTrace_benchmarkArangoDB.txt"))
                foreach (var entry in AvgDict)
                    file.WriteLine("{0} , {1}", entry.Key.ToString().Substring(0,4) , entry.Value.ToString().Replace(',','.'));

            Console.WriteLine("done");
            Console.ReadLine();

        }
    }
}

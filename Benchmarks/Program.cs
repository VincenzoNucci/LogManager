using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using LogManager;

namespace Benchmarks
{
    class Program
    {
        private class BenchmarkTool
        {
            int ThreadNumber, MaxLogs, Resolution;
            public BenchmarkTool(int threadNumber, int maxLogs, int resolution)
            {
                ThreadNumber = threadNumber;
                MaxLogs = maxLogs;
                Resolution = resolution;
            }

            public void Start(string collectionName)
            {
                TraceLog.Connect(collectionName);

            }

            public List<double> Results()
            {
                throw new NotImplementedException();
            }

        }
        static void Main(string[] args)
        {
            //BenchmarkVariableLogFrequence
            //2 threads
            /*var t2 = new BenchmarkVariableLogFrequence(2, 1000, TraceLog.FlushInterval.TotalMilliseconds, "TestConcurrent80");
            while (!t2.IsFinished()) ;
            //5 threads
            var t5 = new BenchmarkVariableLogFrequence(5, 1000, TraceLog.FlushInterval.TotalMilliseconds, "TestConcurrent81");
            while (!t5.IsFinished()) ;
            //10 threads
            var t10 = new BenchmarkVariableLogFrequence(10, 1000, TraceLog.FlushInterval.TotalMilliseconds, "TestConcurrent82");
            while (!t10.IsFinished()) ;
            //20 threads
            var t20 = new BenchmarkVariableLogFrequence(20, 1000, TraceLog.FlushInterval.TotalMilliseconds, "TestConcurrent83");
            while (!t20.IsFinished()) ;
            //50 threads
            var t50 = new BenchmarkVariableLogFrequence(50, 1000, TraceLog.FlushInterval.TotalMilliseconds, "TestConcurrent84");
            while (!t50.IsFinished()) ;
            //100 threads
            var t100 = new BenchmarkVariableLogFrequence(100, 1000, TraceLog.FlushInterval.TotalMilliseconds, "TestConcurrent85");
            while (!t100.IsFinished()) ;
            //150 threads
            var t150 = new BenchmarkVariableLogFrequence(150, 1000, TraceLog.FlushInterval.TotalMilliseconds, "TestConcurrent86");
            while (!t150.IsFinished()) ;
            */
            //200 threads
            //var t200 = new BenchmarkVariableLogFrequence(200, 1000, TraceLog.FlushInterval.TotalMilliseconds, "TestConcurrent1");
            //while (!t200.IsFinished()) ;

            //BenchmarkFlushTimeSize
            //2 threads
            /*var u2 = new BenchmarkFlushTimeSize(2, 1000, TraceLog.FlushInterval.TotalMilliseconds, 1000000000, "TestConcurrent90");
            while (!u2.IsFinished()) ;
            //5 threads
            var u5 = new BenchmarkFlushTimeSize(5, 1000, TraceLog.FlushInterval.TotalMilliseconds, 100000000, "TestConcurrent91");
            while (!u5.IsFinished()) ;
            //10 threads
            var u10 = new BenchmarkFlushTimeSize(10, 1000, TraceLog.FlushInterval.TotalMilliseconds, 1000000000, "TestConcurrent92");
            while (!u10.IsFinished()) ;
            //20 threads
            var u20 = new BenchmarkFlushTimeSize(20, 1000, TraceLog.FlushInterval.TotalMilliseconds, 1000000000, "TestConcurrent93");
            while (!u20.IsFinished()) ;
            //50 threads
            var u50 = new BenchmarkFlushTimeSize(50, 1000, TraceLog.FlushInterval.TotalMilliseconds, 1000000000, "TestConcurrent94");
            while (!u50.IsFinished()) ;

            //100 threads
            var u100 = new BenchmarkFlushTimeSize(100, 1000, TraceLog.FlushInterval.TotalMilliseconds, 1000000000, "TestConcurrent95");
            while (!u100.IsFinished()) ;
            //150 threads
            var u150 = new BenchmarkFlushTimeSize(150, 1000, TraceLog.FlushInterval.TotalMilliseconds, 1000000000, "TestConcurrent96");
            while (!u150.IsFinished()) ;
            //200 threads
            */
            //var u200 = new BenchmarkFlushTimeSize(200, 1000, TraceLog.FlushInterval.TotalMilliseconds, 1000000000, "TestConcurrent1");
            //while (!u200.IsFinished()) ;
            //while (!t.IsFinished()) ;

            TraceLog.Connect("TestConcurrent1");
            //var t = new BenchmarkMaxTimeFullBuffers(1, 1000, TraceLog.FlushInterval.TotalMilliseconds, 0, 100000000, "TestConcurrent100");
            TraceLog.CloneCollections("localhost", 27017, "server", "clone");
            Console.WriteLine("done");
            
            /*var psi = new ProcessStartInfo("shutdown", "/s /t 0");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            Process.Start(psi);
            */
            Console.ReadLine();

        }
    }
}

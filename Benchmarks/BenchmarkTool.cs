using LogManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Benchmarks
{
    public abstract class BenchmarkTool
    {
        //resolution è ogni quanti millisecondi fare la media delle medie, quando il timer fa elapsed
        public int ThreadNumber, MaxLogs;
        public double Resolution;

        //private List<double> ResultsList = new List<double>();
        protected bool Finished = false;

        public BenchmarkTool(int threads, int maxLogs, double resolution)
        {
            ThreadNumber = threads;
            MaxLogs = maxLogs;
            Resolution = resolution;
        }

        public abstract void Start(string collectionName);

        public abstract void ThreadWork();

        public bool IsFinished()
        {
            return Finished;
        }
    }
}

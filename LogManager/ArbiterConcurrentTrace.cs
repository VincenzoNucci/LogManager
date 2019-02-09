﻿using MongoDB.Driver;
using MongoDB.Driver.Core.Clusters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace LogManager
{
    public static class ArbiterConcurrentTrace
    {
        public static int BufferSize = 256;
        public static int NumberOfBuffers = 64;
        public static TimeSpan FlushInterval = TimeSpan.FromSeconds(10);

        private static LogBuffer[] Buffers = null;
        private static readonly object critSec = new object();
        private static MongoClient client = null;
        private static IMongoCollection<Log> Collection = null;
        private static Arbiter2 Arbiter = null;

        private static Timer timer = null;

        /// <summary>
        /// Connects to the localhost database where the logs will be saved.
        /// </summary>
        /// <param name="collectionName">Name of the collection where the logs will be saved.</param>
        public static void Connect(string collectionName, string domain = "localhost", uint port = 27017)
        {
            if (Collection != null) throw new TraceStateException("Connection already established.");

            client = new MongoClient($"mongodb://{domain}:{port}");

            //just to update the description state
            var databases = client.ListDatabases();

            if (client.Cluster.Description.State == ClusterState.Disconnected)
                throw new TraceStateException("Local db is unreachable.");

            var database = client.GetDatabase(Dns.GetHostName());
            Collection = database.GetCollection<Log>(collectionName);

            Buffers = new LogBuffer[NumberOfBuffers];
            for (int i = 0; i < NumberOfBuffers; i++)
            {
                Buffers[i] = new LogBuffer();
            }

            Arbiter = new Arbiter2(Buffers);
            //I create a new delegate in order to call a method with a Conditional Attribute
            Arbiter.OnAllBuffersFilled += Flush;

            timer = new Timer(FlushInterval.TotalMilliseconds);
            timer.AutoReset = false;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Flush();
        }

        /// <summary>
        /// Writes the log into the buffer.
        /// </summary>
        /// <param name="log">The log to be saved.</param>
        public static void Write(Log log)
        {
            if (client == null || client.Cluster.Description.State == ClusterState.Disconnected)
                throw new TraceStateException("No connection to local db.");
           
            LogBuffer freeBuffer = Arbiter.Wait();
            

            freeBuffer.Add(log);
            

            Arbiter.Release(freeBuffer);
        }

        /// <summary>
        /// Transfers synchronously all the logs from the buffer into the database.
        /// </summary>
        public static void Flush()
        {
            if (client == null || client.Cluster.Description.State == ClusterState.Disconnected)
                throw new TraceStateException("No connection to local db.");

            lock (critSec)
            {
                timer.Stop();
                List<Log> b = new List<Log>();
                IEnumerable<LogBuffer> logs = Arbiter.ToList();
                
                foreach (LogBuffer logBuff in logs)
                {
                    b.AddRange(logBuff.Logs);
                }

                if (b.Count == 0) return;

                Collection.InsertMany(b);
                Arbiter.Clear();
                timer.Start();
            }
        }
    }
}

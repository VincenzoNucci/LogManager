﻿using MyCouch;
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
    public static class ArbiterConcurrentTraceCouchDB
    {
        public static int BufferSize = 256;
        public static int NumberOfBuffers = 64;

        private volatile static LogBuffer[] Buffers = null;
        private static readonly object critSec = new object();
        private static MyCouchClient client = null;
        private static IEntities Collection = null;
        private static Arbiter2 Arbiter = null;

        private static Timer timer = null;

        /// <summary>
        /// Connects to the localhost database where the logs will be saved.
        /// </summary>
        /// <param name="collectionName">Name of the collection where the logs will be saved.</param>
        [Conditional("TRACE_LOG")]
        public static void Connect(string collectionName)
        {
            if (Collection != null) throw new TraceStateException("Connection already established.");

            client = new MyCouchClient("http://root:root@localhost:5984","test");


            //just to update the description state
            var databases = client.Database.GetAsync();

            if (client.Connection == null)
                throw new TraceStateException("Local db is unreachable.");


            Collection = client.Entities;

            Buffers = new LogBuffer[NumberOfBuffers];
            for (int i = 0; i < NumberOfBuffers; i++)
            {
                Buffers[i] = new LogBuffer();
            }

            Arbiter = new Arbiter2(Buffers);
            //I create a new delegate in order to call a method with a Conditional Attribute
            Arbiter.OnAllBuffersFilled += delegate { Flush(); };

            timer = new Timer(2000);
            timer.AutoReset = true;
            timer.Elapsed += delegate { Timer_Elapsed(null, null); };
            timer.Start();
        }

      

        

        [Conditional("TRACE_LOG")]
        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (client == null || client.Connection == null)
                throw new TraceStateException("No connection to local db.");

            lock (critSec)
            {
                List<Log> b = new List<Log>();
                foreach (LogBuffer logBuff in Arbiter.ToList())
                {
                    b.AddRange(logBuff.Logs);
                }

                if (b.Count == 0) return;

                foreach (Log l in b)
                    Collection.PostAsync<Log>(l);
                Arbiter.Clear();
            }
        }

        /// <summary>
        /// Writes the log into the buffer.
        /// </summary>
        /// <param name="log">The log to be saved.</param>
        [Conditional("TRACE_LOG")]
        public static void Write(Log log)
        {
            if (client == null || client.Connection == null)
                throw new TraceStateException("No connection to local db.");

            LogBuffer freeBuffer = Arbiter.Wait();

            freeBuffer.Add(log);

            Arbiter.Release(freeBuffer);
        }

        /// <summary>
        /// Transfers synchronously all the logs from the buffer into the database.
        /// </summary>
        [Conditional("TRACE_LOG")]
        public static void Flush()
        {
            if (client == null || client.Connection == null)
                throw new TraceStateException("No connection to local db.");

            lock (critSec)
            {
                timer.Stop();
                List<Log> b = new List<Log>();
                foreach (LogBuffer logBuff in Arbiter.ToList())
                {
                    b.AddRange(logBuff.Logs);
                }

                if (b.Count == 0) return;

                foreach (Log l in b)
                    Collection.PostAsync<Log>(l);
                Arbiter.Clear();
                timer.Start();
            }
        }
    }
}

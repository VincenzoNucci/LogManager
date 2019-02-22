using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using LogManager;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Benchmarks
{
    class BenchmarkSendToServer : BenchmarkTool
    {
        public MongoDB.Driver.IMongoClient client = null;
        public Stopwatch s = new Stopwatch();
        public StreamWriter sw = null;
        public System.Timers.Timer timer = new System.Timers.Timer();
        public bool stopCriterionReached = false;
        public string path = "";

        public BenchmarkSendToServer(int threads, int maxLogs, double res, string serverAddress, uint serverPort) : base(threads, maxLogs, res)
        {
            if (sw == null)
            {
                //sw = new StreamWriter(@"D:\[T-" + this.ThreadNumber + ", L-" + this.MaxLogs + ", R-" + this.Resolution + "]- BenchmarkSendToServer.txt");
                path = @"D:\[T-" + this.ThreadNumber + ", L-" + this.MaxLogs + ", R-" + this.Resolution + "]- BenchmarkSendToServer.txt";
            }
            //File.WriteAllText(path, "collSize,time\n");
            client = new MongoClient("mongodb://" + serverAddress + ":" + serverPort);
            //client.ListDatabases();
            //if (client.Cluster.Description.State == MongoDB.Driver.Core.Clusters.ClusterState.Disconnected)
            //    throw new TraceLogStateException("Server not connected");
            Start("");
        }

        public override void Start(string collectionName)
        {
            try
            {

                LogManager.TraceLog.Connect("TestConcurrent50");
            }
            catch (TraceLogStateException e)
            {

            }

            List<Log> tmpDocuments = new List<Log>();
            List<string> collectionsName = new List<string>();
            List<BsonDocument> collections = new List<BsonDocument>();
            var cursor = TraceLog.Collection.Database.ListCollections();
            while (cursor.MoveNext())
            {
                //raccolgo tutte le collection nel database PC-PC
                collections.AddRange(cursor.Current);
            }
            //tmpDocuments will have the name of all the collections present inside the database
            //to pick the name of a collection navigate the bson document with "name"
            collections.ForEach(_ => {
                collectionsName.Add(_["name"].ToString());
            });
            foreach (string s in collectionsName)
            {
                //prendo tutti i documenti dentro la collection s
                try
                {
                    tmpDocuments.AddRange(TraceLog.Collection.Database.GetCollection<Log>(s).Find(_ => true).ToList<Log>());
                }
                catch (FormatException e)
                {
                    continue;
                }


                try
                {
                    BsonDocument bd = new BsonDocument();
                    BsonDocumentCommand<BsonDocument> bdc = new BsonDocumentCommand<BsonDocument>(new BsonDocument("collStats", s));
                    //Console.WriteLine("bdc: " + bdc.ToJson());
                    //BsonDocument bd = Collection.Database.RunCommand<BsonDocument>("db.stats()");
                    try
                    {
                        bd = TraceLog.Collection.Database.RunCommand<BsonDocument>(bdc);
                    }
                    catch (MongoCommandException e)
                    {

                    }
                    //Console.WriteLine("bd: " + bd.ToJson());
                    long size = bd["size"].ToInt64();
                    this.s.Restart();
                    client.GetDatabase("server").GetCollection<Log>("clone").InsertMany(tmpDocuments, new InsertManyOptions() { IsOrdered = false });
                    //clear the collection once it has sent all the logs to the server

                    //o la pulisce ma la lascia nel db
                    //Collection.Database.GetCollection<Log>(s).DeleteMany(_ => true);
                    //o la toglie direttamente, scegli tu
                    TraceLog.Collection.Database.DropCollection(s);
                    this.s.Stop();
                    Console.WriteLine("collection " + s + " scritta e droppata");
                    File.AppendAllText(path, this.s.Elapsed.TotalMilliseconds.ToString().Replace(",", ".") + ", " + size + "\n");
                    client.GetDatabase("server").GetCollection<Log>("clone").DeleteMany(_ => true);
                    //sw.WriteLine(this.s.Elapsed.TotalMilliseconds.ToString().Replace(",", ".") + ", " + size);
                }
                catch (MongoBulkWriteException e)
                {
                    continue;
                }
            }


            sw.Close();
            Finished = true;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void ThreadWork()
        {
            throw new NotImplementedException();
        }
    }
}
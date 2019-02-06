using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using ArangoDB.Client;
using Elasticsearch.Net;
using CouchDB.Client;
using Raven.Client.Documents;
using Couchbase.Collections;

namespace LogManager
{
    static class Collection
    {
        public static IMongoCollection<Log> mongoDB;
        public static IDocumentCollection arangoDB;
        public static ElasticLowLevelClient elasticDB;
        public static CouchClient couchDB;
        public static IDocumentStore ravenDB;
        public static CouchbaseCollectionBase<Log> couchBaseDB;
        
    }
}

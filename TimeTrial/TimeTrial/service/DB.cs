using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace TimeTrialResults
{
    public class DB
    {

        public static IMongoDatabase GetDatabase()
        {
            IMongoDatabase rtn = null;

            var appSettings = new ServiceStack.Configuration.AppSettings();

            string DbName = appSettings.Get<string>("DbName", "");
            string DbUserName = appSettings.Get<string>("DbUserName", "");
            string DbUserPassword = appSettings.Get<string>("DbUserPassword", "");
            string DbServerHost = appSettings.Get<string>("DbServerHost", "");
            int DbServerPort = appSettings.Get<int>("DbServerPort", 0);


            /*
             FOR SOME REASON THIS DOESNT WORK WITH MLAB \ MONGO 3 - GO OLD SKOOL CONNECTION STRING INSTEAD - SEE BELOW
            var credential = MongoCredential.CreateMongoCRCredential(DbName, DbUserName, DbUserPassword);

            MongoClientSettings settings;

            if (credential != null)
                settings = new MongoClientSettings { Credentials = new[] { credential }, Server = new MongoServerAddress(DbServerHost, DbServerPort) };
            else
                settings = new MongoClientSettings { Server = new MongoServerAddress(DbServerHost, DbServerPort) };

            var mongoClient = new MongoClient(settings);

            var server = mongoClient.GetServer();
            rtn = server.GetDatabase(DbName);
            )*/

            var connectionString = $"mongodb://{ DbUserName }:{ DbUserPassword }@{ DbServerHost }:{ DbServerPort }/{ DbName }";
            var client = new MongoClient(connectionString);
            rtn = client.GetDatabase(DbName);

            return rtn;
        }

        public class TableNames
        {
            public const string user = "user";
        }

        public class user
        {
            public ObjectId id { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string pwd { get; set; }
            //public string FirstName { get; set; }
            //public string FirstName { get; set; }
            //public string FirstName { get; set; }
            //public string FirstName { get; set; }
            //public string FirstName { get; set; }

        }



    }
}
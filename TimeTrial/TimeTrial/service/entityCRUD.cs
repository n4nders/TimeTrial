

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

using ServiceStack;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.Common.Web;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;

namespace TimeTrialResults
{


    public class UserDataCRUD
    {

		public const string DefaultTableName = "UserData";

        public string TableName { get; set; }

        public UserDataCRUD() { this.TableName = DefaultTableName; }

        public UserDataCRUD(string TableName) { this.TableName = TableName; }

        public IList<UserData> g()
        {
            var rtn = new List<UserData>();

            var collection = DB.GetDatabase().GetCollection<UserData>(TableName);
            rtn = collection.AsQueryable<UserData>().ToList();

            return rtn;
        }
        public UserData g(string id)
        {
            UserData rtn = null;

			if(id != null)
			{
                var oId = new ObjectId(id);

                var collection = DB.GetDatabase().GetCollection<UserData>(TableName);
                rtn = collection.AsQueryable<UserData>().Where(o => o.Id == oId).FirstOrDefault();
			}

            return rtn;
        }

        public void cr(UserData item)
        {
            var collection = DB.GetDatabase().GetCollection<UserData>(TableName);
            collection.Insert(item);
        }

        public void u(string id, UserData item)
        {
            var collection = DB.GetDatabase().GetCollection<UserData>(TableName);
            collection.Save(item);
        }
        public void d(string id)
        {

            var oId = new ObjectId(id);
            var q = Query<UserData>.EQ(o => o.Id, oId);

            var collection = DB.GetDatabase().GetCollection<UserData>(TableName);
            collection.Remove(q);
        }

        public void dAll()
        {
            DB.GetDatabase().GetCollection<UserData>(TableName).RemoveAll();
        }

    }


}
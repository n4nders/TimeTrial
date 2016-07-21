

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

namespace sched7
{

	
#if !DEBUG
    [Authenticate]
#endif

	public class StatusSummaryService : Service
	{
        // Request Definitions

        [Route("/StatusSummarys")]
        public class GetStatusSummarys : IReturn<List<StatusSummary>> { }

        [Route("/StatusSummarys/{id}")]
        public class GetStatusSummary : IReturn<StatusSummary>
        {
            public string Id { get; set; }
        }

        [Route("/StatusSummarys", "PUT")]
        public class CreateStatusSummary
        {
            public StatusSummary item { get; set; }
        }

        [Route("/StatusSummarys/{id}", "PATCH")]
        public class UpdateStatusSummary
        {
            public string Id { get; set; }
            public StatusSummary item { get; set; }
        }

        [Route("/StatusSummarys/{id}", "DELETE")]
        public class DeleteStatusSummary
        {
            public string Id { get; set; }
        }

        // methods to service requests, conforming to servicestack convention

        public object Any(GetStatusSummarys request)
        {
            //... return summary of all interests 
            return new StatusSummaryCRUD().g();
        }

        public object Any(GetStatusSummary request)
        {
            //... return specific interest 
            return new StatusSummaryCRUD().g(request.Id);
        }

        public object Any(CreateStatusSummary request)
        {
            //... create the interest here
            new StatusSummaryCRUD().cr(request.item);
            return new HttpResult { StatusCode = HttpStatusCode.Created };
        }

        public object Any(UpdateStatusSummary request)
        {
            //... update the interest here
            new StatusSummaryCRUD().u(request.Id, request.item);
            return new HttpResult { StatusCode = HttpStatusCode.Accepted };
        }

        // need to provide 2 equivalent post handlers for some reason.
        public object Post(UpdateStatusSummary request)
        {
            // return above method ^^^
            return Any(request);
        }

        public object Any(DeleteStatusSummary request)
        {
            //... delete the interest here
            new StatusSummaryCRUD().d(request.Id);
            return new HttpResult { StatusCode = HttpStatusCode.Accepted };
        }
	}

	 public class StatusSummaryCRUD
    {

		public const string DefaultTableName = "StatusSummary";

        public string TableName { get; set; }

        public StatusSummaryCRUD() { this.TableName = DefaultTableName; }

        public StatusSummaryCRUD(string TableName) { this.TableName = TableName; }

        public IList<StatusSummary> g()
        {
            var rtn = new List<StatusSummary>();

            var collection = DB.GetDatabase().GetCollection<StatusSummary>(TableName);
            rtn = collection.AsQueryable<StatusSummary>().ToList();

            return rtn;
        }
        public StatusSummary g(string id)
        {
            StatusSummary rtn = null;

            var oId = new ObjectId(id);

            var collection = DB.GetDatabase().GetCollection<StatusSummary>(TableName);
            rtn = collection.AsQueryable<StatusSummary>().Where(o => o.Id == oId).FirstOrDefault();

            return rtn;
        }

        public void cr(StatusSummary item)
        {
            var collection = DB.GetDatabase().GetCollection<StatusSummary>(TableName);
            collection.Insert(item);
        }

        public void u(string id, StatusSummary item)
        {
            var collection = DB.GetDatabase().GetCollection<StatusSummary>(TableName);
            collection.Save(item);
        }
        public void d(string id)
        {

            var oId = new ObjectId(id);
            var q = Query<StatusSummary>.EQ(o => o.Id, oId);

            var collection = DB.GetDatabase().GetCollection<StatusSummary>(TableName);
            collection.Remove(q);
        }

        public void dAll()
        {
            DB.GetDatabase().GetCollection<StatusSummary>(TableName).RemoveAll();
        }

    }

	
#if !DEBUG
    [Authenticate]
#endif

	public class ReleaseVersionCountService : Service
	{
        // Request Definitions

        [Route("/ReleaseVersionCounts")]
        public class GetReleaseVersionCounts : IReturn<List<ReleaseVersionCount>> { }

        [Route("/ReleaseVersionCounts/{id}")]
        public class GetReleaseVersionCount : IReturn<ReleaseVersionCount>
        {
            public string Id { get; set; }
        }

        [Route("/ReleaseVersionCounts", "PUT")]
        public class CreateReleaseVersionCount
        {
            public ReleaseVersionCount item { get; set; }
        }

        [Route("/ReleaseVersionCounts/{id}", "PATCH")]
        public class UpdateReleaseVersionCount
        {
            public string Id { get; set; }
            public ReleaseVersionCount item { get; set; }
        }

        [Route("/ReleaseVersionCounts/{id}", "DELETE")]
        public class DeleteReleaseVersionCount
        {
            public string Id { get; set; }
        }

        // methods to service requests, conforming to servicestack convention

        public object Any(GetReleaseVersionCounts request)
        {
            //... return summary of all interests 
            return new ReleaseVersionCountCRUD().g();
        }

        public object Any(GetReleaseVersionCount request)
        {
            //... return specific interest 
            return new ReleaseVersionCountCRUD().g(request.Id);
        }

        public object Any(CreateReleaseVersionCount request)
        {
            //... create the interest here
            new ReleaseVersionCountCRUD().cr(request.item);
            return new HttpResult { StatusCode = HttpStatusCode.Created };
        }

        public object Any(UpdateReleaseVersionCount request)
        {
            //... update the interest here
            new ReleaseVersionCountCRUD().u(request.Id, request.item);
            return new HttpResult { StatusCode = HttpStatusCode.Accepted };
        }

        // need to provide 2 equivalent post handlers for some reason.
        public object Post(UpdateReleaseVersionCount request)
        {
            // return above method ^^^
            return Any(request);
        }

        public object Any(DeleteReleaseVersionCount request)
        {
            //... delete the interest here
            new ReleaseVersionCountCRUD().d(request.Id);
            return new HttpResult { StatusCode = HttpStatusCode.Accepted };
        }
	}

	 public class ReleaseVersionCountCRUD
    {

		public const string DefaultTableName = "ReleaseVersionCount";

        public string TableName { get; set; }

        public ReleaseVersionCountCRUD() { this.TableName = DefaultTableName; }

        public ReleaseVersionCountCRUD(string TableName) { this.TableName = TableName; }

        public IList<ReleaseVersionCount> g()
        {
            var rtn = new List<ReleaseVersionCount>();

            var collection = DB.GetDatabase().GetCollection<ReleaseVersionCount>(TableName);
            rtn = collection.AsQueryable<ReleaseVersionCount>().ToList();

            return rtn;
        }
        public ReleaseVersionCount g(string id)
        {
            ReleaseVersionCount rtn = null;

            var oId = new ObjectId(id);

            var collection = DB.GetDatabase().GetCollection<ReleaseVersionCount>(TableName);
            rtn = collection.AsQueryable<ReleaseVersionCount>().Where(o => o.Id == oId).FirstOrDefault();

            return rtn;
        }

        public void cr(ReleaseVersionCount item)
        {
            var collection = DB.GetDatabase().GetCollection<ReleaseVersionCount>(TableName);
            collection.Insert(item);
        }

        public void u(string id, ReleaseVersionCount item)
        {
            var collection = DB.GetDatabase().GetCollection<ReleaseVersionCount>(TableName);
            collection.Save(item);
        }
        public void d(string id)
        {

            var oId = new ObjectId(id);
            var q = Query<ReleaseVersionCount>.EQ(o => o.Id, oId);

            var collection = DB.GetDatabase().GetCollection<ReleaseVersionCount>(TableName);
            collection.Remove(q);
        }

        public void dAll()
        {
            DB.GetDatabase().GetCollection<ReleaseVersionCount>(TableName).RemoveAll();
        }

    }

	
#if !DEBUG
    [Authenticate]
#endif

	public class OutstandingIssueBySoftwareAreaCountService : Service
	{
        // Request Definitions

        [Route("/OutstandingIssueBySoftwareAreaCounts")]
        public class GetOutstandingIssueBySoftwareAreaCounts : IReturn<List<OutstandingIssueBySoftwareAreaCount>> { }

        [Route("/OutstandingIssueBySoftwareAreaCounts/{id}")]
        public class GetOutstandingIssueBySoftwareAreaCount : IReturn<OutstandingIssueBySoftwareAreaCount>
        {
            public string Id { get; set; }
        }

        [Route("/OutstandingIssueBySoftwareAreaCounts", "PUT")]
        public class CreateOutstandingIssueBySoftwareAreaCount
        {
            public OutstandingIssueBySoftwareAreaCount item { get; set; }
        }

        [Route("/OutstandingIssueBySoftwareAreaCounts/{id}", "PATCH")]
        public class UpdateOutstandingIssueBySoftwareAreaCount
        {
            public string Id { get; set; }
            public OutstandingIssueBySoftwareAreaCount item { get; set; }
        }

        [Route("/OutstandingIssueBySoftwareAreaCounts/{id}", "DELETE")]
        public class DeleteOutstandingIssueBySoftwareAreaCount
        {
            public string Id { get; set; }
        }

        // methods to service requests, conforming to servicestack convention

        public object Any(GetOutstandingIssueBySoftwareAreaCounts request)
        {
            //... return summary of all interests 
            return new OutstandingIssueBySoftwareAreaCountCRUD().g();
        }

        public object Any(GetOutstandingIssueBySoftwareAreaCount request)
        {
            //... return specific interest 
            return new OutstandingIssueBySoftwareAreaCountCRUD().g(request.Id);
        }

        public object Any(CreateOutstandingIssueBySoftwareAreaCount request)
        {
            //... create the interest here
            new OutstandingIssueBySoftwareAreaCountCRUD().cr(request.item);
            return new HttpResult { StatusCode = HttpStatusCode.Created };
        }

        public object Any(UpdateOutstandingIssueBySoftwareAreaCount request)
        {
            //... update the interest here
            new OutstandingIssueBySoftwareAreaCountCRUD().u(request.Id, request.item);
            return new HttpResult { StatusCode = HttpStatusCode.Accepted };
        }

        // need to provide 2 equivalent post handlers for some reason.
        public object Post(UpdateOutstandingIssueBySoftwareAreaCount request)
        {
            // return above method ^^^
            return Any(request);
        }

        public object Any(DeleteOutstandingIssueBySoftwareAreaCount request)
        {
            //... delete the interest here
            new OutstandingIssueBySoftwareAreaCountCRUD().d(request.Id);
            return new HttpResult { StatusCode = HttpStatusCode.Accepted };
        }
	}

	 public class OutstandingIssueBySoftwareAreaCountCRUD
    {

		public const string DefaultTableName = "OutstandingIssueBySoftwareAreaCount";

        public string TableName { get; set; }

        public OutstandingIssueBySoftwareAreaCountCRUD() { this.TableName = DefaultTableName; }

        public OutstandingIssueBySoftwareAreaCountCRUD(string TableName) { this.TableName = TableName; }

        public IList<OutstandingIssueBySoftwareAreaCount> g()
        {
            var rtn = new List<OutstandingIssueBySoftwareAreaCount>();

            var collection = DB.GetDatabase().GetCollection<OutstandingIssueBySoftwareAreaCount>(TableName);
            rtn = collection.AsQueryable<OutstandingIssueBySoftwareAreaCount>().ToList();

            return rtn;
        }
        public OutstandingIssueBySoftwareAreaCount g(string id)
        {
            OutstandingIssueBySoftwareAreaCount rtn = null;

            var oId = new ObjectId(id);

            var collection = DB.GetDatabase().GetCollection<OutstandingIssueBySoftwareAreaCount>(TableName);
            rtn = collection.AsQueryable<OutstandingIssueBySoftwareAreaCount>().Where(o => o.Id == oId).FirstOrDefault();

            return rtn;
        }

        public void cr(OutstandingIssueBySoftwareAreaCount item)
        {
            var collection = DB.GetDatabase().GetCollection<OutstandingIssueBySoftwareAreaCount>(TableName);
            collection.Insert(item);
        }

        public void u(string id, OutstandingIssueBySoftwareAreaCount item)
        {
            var collection = DB.GetDatabase().GetCollection<OutstandingIssueBySoftwareAreaCount>(TableName);
            collection.Save(item);
        }
        public void d(string id)
        {

            var oId = new ObjectId(id);
            var q = Query<OutstandingIssueBySoftwareAreaCount>.EQ(o => o.Id, oId);

            var collection = DB.GetDatabase().GetCollection<OutstandingIssueBySoftwareAreaCount>(TableName);
            collection.Remove(q);
        }

        public void dAll()
        {
            DB.GetDatabase().GetCollection<OutstandingIssueBySoftwareAreaCount>(TableName).RemoveAll();
        }

    }

	
#if !DEBUG
    [Authenticate]
#endif

	public class OutstandingIssueBySoftwareAreaDetailService : Service
	{
        // Request Definitions

        [Route("/OutstandingIssueBySoftwareAreaDetails")]
        public class GetOutstandingIssueBySoftwareAreaDetails : IReturn<List<OutstandingIssueBySoftwareAreaDetail>> { }

        [Route("/OutstandingIssueBySoftwareAreaDetails/{id}")]
        public class GetOutstandingIssueBySoftwareAreaDetail : IReturn<OutstandingIssueBySoftwareAreaDetail>
        {
            public string Id { get; set; }
        }

        [Route("/OutstandingIssueBySoftwareAreaDetails", "PUT")]
        public class CreateOutstandingIssueBySoftwareAreaDetail
        {
            public OutstandingIssueBySoftwareAreaDetail item { get; set; }
        }

        [Route("/OutstandingIssueBySoftwareAreaDetails/{id}", "PATCH")]
        public class UpdateOutstandingIssueBySoftwareAreaDetail
        {
            public string Id { get; set; }
            public OutstandingIssueBySoftwareAreaDetail item { get; set; }
        }

        [Route("/OutstandingIssueBySoftwareAreaDetails/{id}", "DELETE")]
        public class DeleteOutstandingIssueBySoftwareAreaDetail
        {
            public string Id { get; set; }
        }

        // methods to service requests, conforming to servicestack convention

        public object Any(GetOutstandingIssueBySoftwareAreaDetails request)
        {
            //... return summary of all interests 
            return new OutstandingIssueBySoftwareAreaDetailCRUD().g();
        }

        public object Any(GetOutstandingIssueBySoftwareAreaDetail request)
        {
            //... return specific interest 
            return new OutstandingIssueBySoftwareAreaDetailCRUD().g(request.Id);
        }

        public object Any(CreateOutstandingIssueBySoftwareAreaDetail request)
        {
            //... create the interest here
            new OutstandingIssueBySoftwareAreaDetailCRUD().cr(request.item);
            return new HttpResult { StatusCode = HttpStatusCode.Created };
        }

        public object Any(UpdateOutstandingIssueBySoftwareAreaDetail request)
        {
            //... update the interest here
            new OutstandingIssueBySoftwareAreaDetailCRUD().u(request.Id, request.item);
            return new HttpResult { StatusCode = HttpStatusCode.Accepted };
        }

        // need to provide 2 equivalent post handlers for some reason.
        public object Post(UpdateOutstandingIssueBySoftwareAreaDetail request)
        {
            // return above method ^^^
            return Any(request);
        }

        public object Any(DeleteOutstandingIssueBySoftwareAreaDetail request)
        {
            //... delete the interest here
            new OutstandingIssueBySoftwareAreaDetailCRUD().d(request.Id);
            return new HttpResult { StatusCode = HttpStatusCode.Accepted };
        }
	}

	 public class OutstandingIssueBySoftwareAreaDetailCRUD
    {

		public const string DefaultTableName = "OutstandingIssueBySoftwareAreaDetail";

        public string TableName { get; set; }

        public OutstandingIssueBySoftwareAreaDetailCRUD() { this.TableName = DefaultTableName; }

        public OutstandingIssueBySoftwareAreaDetailCRUD(string TableName) { this.TableName = TableName; }

        public IList<OutstandingIssueBySoftwareAreaDetail> g()
        {
            var rtn = new List<OutstandingIssueBySoftwareAreaDetail>();

            var collection = DB.GetDatabase().GetCollection<OutstandingIssueBySoftwareAreaDetail>(TableName);
            rtn = collection.AsQueryable<OutstandingIssueBySoftwareAreaDetail>().ToList();

            return rtn;
        }
        public OutstandingIssueBySoftwareAreaDetail g(string id)
        {
            OutstandingIssueBySoftwareAreaDetail rtn = null;

            var oId = new ObjectId(id);

            var collection = DB.GetDatabase().GetCollection<OutstandingIssueBySoftwareAreaDetail>(TableName);
            rtn = collection.AsQueryable<OutstandingIssueBySoftwareAreaDetail>().Where(o => o.Id == oId).FirstOrDefault();

            return rtn;
        }

        public void cr(OutstandingIssueBySoftwareAreaDetail item)
        {
            var collection = DB.GetDatabase().GetCollection<OutstandingIssueBySoftwareAreaDetail>(TableName);
            collection.Insert(item);
        }

        public void u(string id, OutstandingIssueBySoftwareAreaDetail item)
        {
            var collection = DB.GetDatabase().GetCollection<OutstandingIssueBySoftwareAreaDetail>(TableName);
            collection.Save(item);
        }
        public void d(string id)
        {

            var oId = new ObjectId(id);
            var q = Query<OutstandingIssueBySoftwareAreaDetail>.EQ(o => o.Id, oId);

            var collection = DB.GetDatabase().GetCollection<OutstandingIssueBySoftwareAreaDetail>(TableName);
            collection.Remove(q);
        }

        public void dAll()
        {
            DB.GetDatabase().GetCollection<OutstandingIssueBySoftwareAreaDetail>(TableName).RemoveAll();
        }

    }

	
#if !DEBUG
    [Authenticate]
#endif

	public class IssueFullDetailService : Service
	{
        // Request Definitions

        [Route("/IssueFullDetails")]
        public class GetIssueFullDetails : IReturn<List<IssueFullDetail>> { }

        [Route("/IssueFullDetails/{id}")]
        public class GetIssueFullDetail : IReturn<IssueFullDetail>
        {
            public string Id { get; set; }
        }

        [Route("/IssueFullDetails", "PUT")]
        public class CreateIssueFullDetail
        {
            public IssueFullDetail item { get; set; }
        }

        [Route("/IssueFullDetails/{id}", "PATCH")]
        public class UpdateIssueFullDetail
        {
            public string Id { get; set; }
            public IssueFullDetail item { get; set; }
        }

        [Route("/IssueFullDetails/{id}", "DELETE")]
        public class DeleteIssueFullDetail
        {
            public string Id { get; set; }
        }

        // methods to service requests, conforming to servicestack convention

        public object Any(GetIssueFullDetails request)
        {
            //... return summary of all interests 
            return new IssueFullDetailCRUD().g();
        }

        public object Any(GetIssueFullDetail request)
        {
            //... return specific interest 
            return new IssueFullDetailCRUD().g(request.Id);
        }

        public object Any(CreateIssueFullDetail request)
        {
            //... create the interest here
            new IssueFullDetailCRUD().cr(request.item);
            return new HttpResult { StatusCode = HttpStatusCode.Created };
        }

        public object Any(UpdateIssueFullDetail request)
        {
            //... update the interest here
            new IssueFullDetailCRUD().u(request.Id, request.item);
            return new HttpResult { StatusCode = HttpStatusCode.Accepted };
        }

        // need to provide 2 equivalent post handlers for some reason.
        public object Post(UpdateIssueFullDetail request)
        {
            // return above method ^^^
            return Any(request);
        }

        public object Any(DeleteIssueFullDetail request)
        {
            //... delete the interest here
            new IssueFullDetailCRUD().d(request.Id);
            return new HttpResult { StatusCode = HttpStatusCode.Accepted };
        }
	}

	 public class IssueFullDetailCRUD
    {

		public const string DefaultTableName = "IssueFullDetail";

        public string TableName { get; set; }

        public IssueFullDetailCRUD() { this.TableName = DefaultTableName; }

        public IssueFullDetailCRUD(string TableName) { this.TableName = TableName; }

        public IList<IssueFullDetail> g()
        {
            var rtn = new List<IssueFullDetail>();

            var collection = DB.GetDatabase().GetCollection<IssueFullDetail>(TableName);
            rtn = collection.AsQueryable<IssueFullDetail>().ToList();

            return rtn;
        }
        public IssueFullDetail g(string id)
        {
            IssueFullDetail rtn = null;

            var oId = new ObjectId(id);

            var collection = DB.GetDatabase().GetCollection<IssueFullDetail>(TableName);
            rtn = collection.AsQueryable<IssueFullDetail>().Where(o => o.Id == oId).FirstOrDefault();

            return rtn;
        }

        public void cr(IssueFullDetail item)
        {
            var collection = DB.GetDatabase().GetCollection<IssueFullDetail>(TableName);
            collection.Insert(item);
        }

        public void u(string id, IssueFullDetail item)
        {
            var collection = DB.GetDatabase().GetCollection<IssueFullDetail>(TableName);
            collection.Save(item);
        }
        public void d(string id)
        {

            var oId = new ObjectId(id);
            var q = Query<IssueFullDetail>.EQ(o => o.Id, oId);

            var collection = DB.GetDatabase().GetCollection<IssueFullDetail>(TableName);
            collection.Remove(q);
        }

        public void dAll()
        {
            DB.GetDatabase().GetCollection<IssueFullDetail>(TableName).RemoveAll();
        }

    }

	
#if !DEBUG
    [Authenticate]
#endif

	public class SupportIssueFullDetailService : Service
	{
        // Request Definitions

        [Route("/SupportIssueFullDetails")]
        public class GetSupportIssueFullDetails : IReturn<List<SupportIssueFullDetail>> { }

        [Route("/SupportIssueFullDetails/{id}")]
        public class GetSupportIssueFullDetail : IReturn<SupportIssueFullDetail>
        {
            public string Id { get; set; }
        }

        [Route("/SupportIssueFullDetails", "PUT")]
        public class CreateSupportIssueFullDetail
        {
            public SupportIssueFullDetail item { get; set; }
        }

        [Route("/SupportIssueFullDetails/{id}", "PATCH")]
        public class UpdateSupportIssueFullDetail
        {
            public string Id { get; set; }
            public SupportIssueFullDetail item { get; set; }
        }

        [Route("/SupportIssueFullDetails/{id}", "DELETE")]
        public class DeleteSupportIssueFullDetail
        {
            public string Id { get; set; }
        }

        // methods to service requests, conforming to servicestack convention

        public object Any(GetSupportIssueFullDetails request)
        {
            //... return summary of all interests 
            return new SupportIssueFullDetailCRUD().g();
        }

        public object Any(GetSupportIssueFullDetail request)
        {
            //... return specific interest 
            return new SupportIssueFullDetailCRUD().g(request.Id);
        }

        public object Any(CreateSupportIssueFullDetail request)
        {
            //... create the interest here
            new SupportIssueFullDetailCRUD().cr(request.item);
            return new HttpResult { StatusCode = HttpStatusCode.Created };
        }

        public object Any(UpdateSupportIssueFullDetail request)
        {
            //... update the interest here
            new SupportIssueFullDetailCRUD().u(request.Id, request.item);
            return new HttpResult { StatusCode = HttpStatusCode.Accepted };
        }

        // need to provide 2 equivalent post handlers for some reason.
        public object Post(UpdateSupportIssueFullDetail request)
        {
            // return above method ^^^
            return Any(request);
        }

        public object Any(DeleteSupportIssueFullDetail request)
        {
            //... delete the interest here
            new SupportIssueFullDetailCRUD().d(request.Id);
            return new HttpResult { StatusCode = HttpStatusCode.Accepted };
        }
	}

	 public class SupportIssueFullDetailCRUD
    {

		public const string DefaultTableName = "SupportIssueFullDetail";

        public string TableName { get; set; }

        public SupportIssueFullDetailCRUD() { this.TableName = DefaultTableName; }

        public SupportIssueFullDetailCRUD(string TableName) { this.TableName = TableName; }

        public IList<SupportIssueFullDetail> g()
        {
            var rtn = new List<SupportIssueFullDetail>();

            var collection = DB.GetDatabase().GetCollection<SupportIssueFullDetail>(TableName);
            rtn = collection.AsQueryable<SupportIssueFullDetail>().ToList();

            return rtn;
        }
        public SupportIssueFullDetail g(string id)
        {
            SupportIssueFullDetail rtn = null;

            var oId = new ObjectId(id);

            var collection = DB.GetDatabase().GetCollection<SupportIssueFullDetail>(TableName);
            rtn = collection.AsQueryable<SupportIssueFullDetail>().Where(o => o.Id == oId).FirstOrDefault();

            return rtn;
        }

        public void cr(SupportIssueFullDetail item)
        {
            var collection = DB.GetDatabase().GetCollection<SupportIssueFullDetail>(TableName);
            collection.Insert(item);
        }

        public void u(string id, SupportIssueFullDetail item)
        {
            var collection = DB.GetDatabase().GetCollection<SupportIssueFullDetail>(TableName);
            collection.Save(item);
        }
        public void d(string id)
        {

            var oId = new ObjectId(id);
            var q = Query<SupportIssueFullDetail>.EQ(o => o.Id, oId);

            var collection = DB.GetDatabase().GetCollection<SupportIssueFullDetail>(TableName);
            collection.Remove(q);
        }

        public void dAll()
        {
            DB.GetDatabase().GetCollection<SupportIssueFullDetail>(TableName).RemoveAll();
        }

    }

	
#if !DEBUG
    [Authenticate]
#endif

	public class SupportFirmCallCountService : Service
	{
        // Request Definitions

        [Route("/SupportFirmCallCounts")]
        public class GetSupportFirmCallCounts : IReturn<List<SupportFirmCallCount>> { }

        [Route("/SupportFirmCallCounts/{id}")]
        public class GetSupportFirmCallCount : IReturn<SupportFirmCallCount>
        {
            public string Id { get; set; }
        }

        [Route("/SupportFirmCallCounts", "PUT")]
        public class CreateSupportFirmCallCount
        {
            public SupportFirmCallCount item { get; set; }
        }

        [Route("/SupportFirmCallCounts/{id}", "PATCH")]
        public class UpdateSupportFirmCallCount
        {
            public string Id { get; set; }
            public SupportFirmCallCount item { get; set; }
        }

        [Route("/SupportFirmCallCounts/{id}", "DELETE")]
        public class DeleteSupportFirmCallCount
        {
            public string Id { get; set; }
        }

        // methods to service requests, conforming to servicestack convention

        public object Any(GetSupportFirmCallCounts request)
        {
            //... return summary of all interests 
            return new SupportFirmCallCountCRUD().g();
        }

        public object Any(GetSupportFirmCallCount request)
        {
            //... return specific interest 
            return new SupportFirmCallCountCRUD().g(request.Id);
        }

        public object Any(CreateSupportFirmCallCount request)
        {
            //... create the interest here
            new SupportFirmCallCountCRUD().cr(request.item);
            return new HttpResult { StatusCode = HttpStatusCode.Created };
        }

        public object Any(UpdateSupportFirmCallCount request)
        {
            //... update the interest here
            new SupportFirmCallCountCRUD().u(request.Id, request.item);
            return new HttpResult { StatusCode = HttpStatusCode.Accepted };
        }

        // need to provide 2 equivalent post handlers for some reason.
        public object Post(UpdateSupportFirmCallCount request)
        {
            // return above method ^^^
            return Any(request);
        }

        public object Any(DeleteSupportFirmCallCount request)
        {
            //... delete the interest here
            new SupportFirmCallCountCRUD().d(request.Id);
            return new HttpResult { StatusCode = HttpStatusCode.Accepted };
        }
	}

	 public class SupportFirmCallCountCRUD
    {

		public const string DefaultTableName = "SupportFirmCallCount";

        public string TableName { get; set; }

        public SupportFirmCallCountCRUD() { this.TableName = DefaultTableName; }

        public SupportFirmCallCountCRUD(string TableName) { this.TableName = TableName; }

        public IList<SupportFirmCallCount> g()
        {
            var rtn = new List<SupportFirmCallCount>();

            var collection = DB.GetDatabase().GetCollection<SupportFirmCallCount>(TableName);
            rtn = collection.AsQueryable<SupportFirmCallCount>().ToList();

            return rtn;
        }
        public SupportFirmCallCount g(string id)
        {
            SupportFirmCallCount rtn = null;

            var oId = new ObjectId(id);

            var collection = DB.GetDatabase().GetCollection<SupportFirmCallCount>(TableName);
            rtn = collection.AsQueryable<SupportFirmCallCount>().Where(o => o.Id == oId).FirstOrDefault();

            return rtn;
        }

        public void cr(SupportFirmCallCount item)
        {
            var collection = DB.GetDatabase().GetCollection<SupportFirmCallCount>(TableName);
            collection.Insert(item);
        }

        public void u(string id, SupportFirmCallCount item)
        {
            var collection = DB.GetDatabase().GetCollection<SupportFirmCallCount>(TableName);
            collection.Save(item);
        }
        public void d(string id)
        {

            var oId = new ObjectId(id);
            var q = Query<SupportFirmCallCount>.EQ(o => o.Id, oId);

            var collection = DB.GetDatabase().GetCollection<SupportFirmCallCount>(TableName);
            collection.Remove(q);
        }

        public void dAll()
        {
            DB.GetDatabase().GetCollection<SupportFirmCallCount>(TableName).RemoveAll();
        }

    }


}
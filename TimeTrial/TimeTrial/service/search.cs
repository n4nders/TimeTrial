using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

using ServiceStack;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.Common.Web;

namespace TimeTrialResults
{
    //[Route("/search")]
    [Route("/search/{Query}")]
    public class SearchRequest
    {
        public string Name { get; set; }
    }

    //public class HelloResponse
    //{
    //    public string Result { get; set; }
    //}

    [Authenticate]
    public class SearchService : Service
    {
        public object Any(SearchRequest request)
        {
            //return new HelloResponse { Result = "Hello, " + request.Name };

            //var xx = new HttpRequest("", "", "");



            return null;
        }
    }

}
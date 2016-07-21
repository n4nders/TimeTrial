using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using ServiceStack;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;

using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;


namespace TimeTrialResults
{
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// Create your ServiceStack web service application with a singleton AppHost.   
        /// </summary>   
        public class AppHost : AppHostBase
        {
            /// <summary>             
            /// Initializes a new instance of your ServiceStack application, with the specified name and assembly containing the services.             
            /// </summary>
            public AppHost() : base("TimeTrialResults Web Services", typeof(Hello).Assembly) { }
            /// <summary>        
            /// Configure the container with the necessary routes for your ServiceStack application.
            /// </summary>             
            /// <param name="container">The built-in IoC used with ServiceStack.</param>             
            public override void Configure(Funq.Container container)
            {
                //register any dependencies your services use, e.g:
                //container.Register<ICacheClient>(new MemoryCacheClient());

                //Permit modern browsers (e.g. Firefox) to allow sending of any REST HTTP Method
                base.SetConfig(new EndpointHostConfig
                {
                    GlobalResponseHeaders = {
                        //{ "Access-Control-Allow-Origin", "*" },
                        { "Access-Control-Allow-Methods", "GET, POST, PATCH, PUT, DELETE, OPTIONS" },
                        { "Access-Control-Allow-Headers", "Content-Type" },
                    },
                });

                JsConfig.DateHandler = JsonDateHandler.ISO8601;

                // AUTH STUFF //

                // https://github.com/ServiceStack/ServiceStack/wiki/Authentication-and-authorization
                // http://enehana.nohea.com/general/customizing-iauthprovider-for-servicestack-net-step-by-step/

                Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                   new IAuthProvider[] { 
                   new CustomCredentialsAuthProvider() //HTML Form post of UserName/Password credentials
                }));

                //Plugins.Add(new RegistrationFeature());

                container.Register<ICacheClient>(new MemoryCacheClient());
                var userRep = new InMemoryAuthRepository();
                container.Register<IUserAuthRepository>(userRep);

                //The IUserAuthRepository is used to store the user credentials etc.
                //Implement this interface to adjust it to your app's data storage

                // END OF AUTH STUFF //
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            //Initialize your application            
            new AppHost().Init();

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

using ServiceStack;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface.Auth;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace TimeTrialResults
{
    public class CustomCredentialsAuthProvider : CredentialsAuthProvider
    {

        public CustomCredentialsAuthProvider()
            : base()
        {
            //var bp = 1;
        }

        public override bool TryAuthenticate(IServiceBase authService, string userName, string password)
        {
            bool rtn = false;

            var db = DB.GetDatabase();
            var u = db.GetCollection<DB.user>(DB.TableNames.user).AsQueryable().Where(o => o.UserName == userName).FirstOrDefault();

            {
                var pwdBytes = System.Text.ASCIIEncoding.UTF8.GetBytes(password);
                var pwdEncoded = ProgressEncode.Progress.Encode(pwdBytes); // use the progress encoding scheme. why not says I.

                rtn = u?.pwd == pwdEncoded;
            }
            
            return rtn;
        }

        public override object Authenticate(IServiceBase authService, IAuthSession session, Auth request)
        {
            return base.Authenticate(authService, session, request);
        }

        public override bool IsAuthorized(IAuthSession session, IOAuthTokens tokens, Auth request = null)
        {
            return base.IsAuthorized(session, tokens, request);
        }

        public override void OnAuthenticated(IServiceBase authService, IAuthSession session, IOAuthTokens tokens, Dictionary<string, string> authInfo)
        {
            
            var db = DB.GetDatabase();
            var u = db.GetCollection<DB.user>(DB.TableNames.user).AsQueryable().Where(o => o.UserName == session.UserAuthName).FirstOrDefault();

            if (u != null)
            {
                session.IsAuthenticated = true;
                session.FirstName = u.FirstName;
                session.LastName = u.LastName;

                session.UserAuthId = u.id.ToString(); // seems like a good place to store the db userid.
            }

            authService.SaveSession(session, SessionExpiry);

        }
    }
}
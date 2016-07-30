#define ENCODE_PASSWORDS

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

using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace TimeTrialResults
{

    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
    }

    [Route("/user")]
    public class GetUser : IReturn<User> { }

    [Authenticate]
    public class UserService : Service
    {
        public object Any(GetUser request)
        {
            var rtn = new User();

            var userSession = base.SessionAs<AuthUserSession>();

            rtn.FirstName = userSession.FirstName;
            rtn.LastName = userSession.LastName;
            rtn.DisplayName = userSession.DisplayName;
            rtn.Email = userSession.Email;

            return rtn;
        }
    }

    //////////////////////// PASSWORD CHANGE ///////////////////////////

    [Route("/changePassword", "PATCH")]
    public class UpdateChangePassword : IReturn<bool>
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    [Authenticate]
    public class ChangePasswordService : Service
    {
        public object Any(UpdateChangePassword request)
        {
            bool rtn = false;

            var userSession = base.SessionAs<AuthUserSession>();

            // not sure if this can be null, put in a check just in case
            if (userSession == null)
                throw new Exception("Can't find user session");

            //var db = DB.GetDatabase();
            var collection = DB.GetDatabase().GetCollection<DB.user>(DB.TableNames.user);


            var u = collection.AsQueryable<DB.user>().Where(o => o.UserName == userSession.UserAuthName).FirstOrDefault();

            if (u == null)
                throw new Exception("Cannot find user");

            // check existing password is correct
            {

                var pwdBytes = System.Text.ASCIIEncoding.UTF8.GetBytes(request.OldPassword);
                var pwdEncoded = ProgressEncode.Progress.Encode(pwdBytes); // use the progress encoding scheme. why not says I.

                if (u.pwd != pwdEncoded)
                    throw new Exception("Exisiting password entered incorrectly");
            }

            // check complexity requirements // could be configurable
            {
                // there is a minimum requirement of the auth provider that the pwd is not null\empty string. so do a minimum check for this

                if (string.IsNullOrWhiteSpace(request.NewPassword))
                    throw new Exception("New password cannot be blank");
            }

            // figure out encoded new password
            {
                var pwdBytes = System.Text.ASCIIEncoding.UTF8.GetBytes(request.NewPassword);
                var pwdEncoded = ProgressEncode.Progress.Encode(pwdBytes); // use the progress encoding scheme. why not says I.

                u.pwd = pwdEncoded;
            }

            // save user record back to the database

            // probably a poor way of doing an update. waiting for inspiration...
            var filter = Builders<DB.user>.Filter.Eq(nameof(u.id), u.id);
            collection.ReplaceOne(filter, u);

            rtn = true;

            return rtn;
        }
    }

#if !DEBUG
    [Authenticate]
#endif

    public class UserDataService : Service
    {
        // Request Definitions

        [Route("/UserData")]
        public class GetUserData : IReturn<UserData> { }

        [Route("/UserData", "PUT")]
        public class CreateUserData
        {
            public UserData item { get; set; }
        }

        [Route("/UserData", "PATCH")]
        public class UpdateUserData
        {
            public UserData item { get; set; }
        }

        [Route("/UserData", "DELETE")]
        public class DeleteUserData { }

        // methods to service requests, conforming to servicestack convention

        //public object Any(GetUserData request)
        //{
        //    //... return summary of all interests 
        //    return new UserDataCRUD().g();
        //}

        public object Any(GetUserData request)
        {
            //... return specific interest 
            return new UserDataCRUD().g(GetUserId());
        }

        public object Any(CreateUserData request)
        {
            //... create the interest here

            request.item.Id = ObjectId.Parse(GetUserId());

            new UserDataCRUD().cr(request.item);
            return new HttpResult { StatusCode = HttpStatusCode.Created };
        }

        public object Any(UpdateUserData request)
        {
            //... update the interest here

            var userId = GetUserId();

            if (userId != null)
            {
                request.item.Id = ObjectId.Parse(userId);

                new UserDataCRUD().u(userId, request.item);
                return new HttpResult { StatusCode = HttpStatusCode.Accepted };
            }

            return new HttpResult { StatusCode = HttpStatusCode.NotModified };
        }

        // need to provide 2 equivalent post handlers for some reason.
        public object Post(UpdateUserData request)
        {
            // return above method ^^^
            return Any(request);
        }

        public object Any(DeleteUserData request)
        {
            //... delete the interest here
            new UserDataCRUD().d(GetUserId());
            return new HttpResult { StatusCode = HttpStatusCode.Accepted };
        }

        private string GetUserId()
        {
            string rtn = null;

            var userSession = base.SessionAs<AuthUserSession>();

            // not sure if this can be null, put in a check just in case
            if (userSession == null) throw new Exception("Can't find user session");

            rtn = userSession.UserAuthId;

            return rtn;
        }

    }


    /////////////////////////////////////////////////////////////////////////

}


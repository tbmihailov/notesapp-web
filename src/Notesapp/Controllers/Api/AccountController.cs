using Notesapp.Filters;
using Notesapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebMatrix.WebData;

namespace Notesapp.Controllers.Api
{
    //[InitializeSimpleMembership]
    public class AccountController : ApiController
    {
        public UserProfile Login(string username, string password)
        {
            var userIdentity = User.Identity;
            //var roles = System.Web.Security.Roles.GetRolesForUser(userIdentity.)
            bool loggedin = WebSecurity.Login(username, password);
            if (loggedin)
            {
                var usersDb = new UsersContext();
                var userProfile = usersDb.UserProfiles.Single(u => u.UserName == username);
                if (string.IsNullOrEmpty(userProfile.AuthToken))
                {
                    userProfile.AuthToken = Guid.NewGuid().ToString("N");
                    usersDb.SaveChanges();
                }

                return userProfile;
            }

            throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.Unauthorized));
        }

    }
}

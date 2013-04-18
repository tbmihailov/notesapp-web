using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Notesapp.Filters
{
    public class AuthorizeTokenAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext != null)
            {
                if (!AuthorizeRequest(actionContext.ControllerContext.Request))
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { RequestMessage = actionContext.ControllerContext.Request };
                }
                else
                {

                }
                return;
            }
        }

        private bool AuthorizeRequest(System.Net.Http.HttpRequestMessage request)
        {
            bool authorized = false;
            if (request.Headers.Contains(AuthConstants.AUTH_TOKEN_NAME))
            {
                var tokenValue = request.Headers.GetValues(AuthConstants.AUTH_TOKEN_NAME);
                if (tokenValue.Count() == 1)
                {
                    var value = tokenValue.FirstOrDefault();
                    //Token validation logic here
                    //set authorized variable accordingly
                }
            }
            return authorized;
        }
    }

    public static class AuthConstants
    {
        public const string AUTH_TOKEN_NAME = "AUTH_TOKEN";
    }
}
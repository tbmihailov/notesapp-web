using Notesapp.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Notesapp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "LoginApi",
                routeTemplate: "api/Account/Login",
                defaults: new { controller="Account", action="Login"}
            );

            config.MessageHandlers.Add(new MobileAuthenticationMessageHandler());
            config.EnableQuerySupport();
        }
    }
}
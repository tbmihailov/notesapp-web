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
                name: "LoginApi",
                routeTemplate: "api/Account/Login",
                defaults: new { controller = "Account", action = "Login" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            //Reference serializing in WebAPI Help page
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;

            config.MessageHandlers.Add(new MobileAuthenticationMessageHandler());
            config.EnableQuerySupport();
        }
    }
}
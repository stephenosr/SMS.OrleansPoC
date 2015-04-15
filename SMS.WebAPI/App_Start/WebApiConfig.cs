using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SMS.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "FeeAPI",
                routeTemplate: "{controller}/{action}",
                defaults: new { action = "Post" }
            );
        }
    }
}

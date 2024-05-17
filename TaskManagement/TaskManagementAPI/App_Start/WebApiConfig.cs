using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TaskManagementAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var jsonSettings = config.Formatters.JsonFormatter.SerializerSettings;
            jsonSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            jsonSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            jsonSettings.Formatting = Newtonsoft.Json.Formatting.None; // Ensure single line JSON

            // Remove XML formatter
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
        }
    }
}

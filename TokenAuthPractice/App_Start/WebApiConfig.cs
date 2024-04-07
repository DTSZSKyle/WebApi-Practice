using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace TokenAuthPractice
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            //Register custom exception
            config.Filters.Add(new TokenAuthPractice.CustomFilters.CustomExceptionFilter());
            //Register global exception handler
            config.Services.Replace(typeof(IExceptionHandler), new TokenAuthPractice.CustomFilters.GlobalExceptionHandler());
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

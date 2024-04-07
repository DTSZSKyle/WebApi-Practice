using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http.Filters;
using TokenAuthPractice.Models;
using TokenAuthPractice.DAL;
namespace TokenAuthPractice.CustomFilters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            string exceptionMessage = actionExecutedContext.Exception.InnerException?.Message ?? actionExecutedContext.Exception.Message;

            // Log the exception to the database
            LogExceptionToDatabase(exceptionMessage, actionExecutedContext.Request.Method.ToString(), actionExecutedContext.Request.RequestUri.ToString());

            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An exception was thrown by service."),
                ReasonPhrase = "Internal Server Error. Please Contact your Administrator."
            };
            actionExecutedContext.Response = response;
        }
        
        private void LogExceptionToDatabase(string message, string requestMethod, string requestUri)
        {
            using (var context = new DataContext()) 
            {
                var errorLog = new ErrorLog
                {
                    Message = message,
                    RequestMethod = requestMethod,
                    RequestUri = requestUri,
                    TimeUtc = DateTime.UtcNow
                };

                context.ErrorLogs.Add(errorLog);
                context.SaveChanges();
            }
        }
    }
}
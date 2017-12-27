using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Server.Middleware
{
    public class Filter : ActionFilterAttribute
    {
        private readonly ILogger _logger;

        public Filter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("ClassConsoleLogActionOneFilter");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"Remote IpAddress: {context.HttpContext.Connection.RemoteIpAddress}");

            string Method = context.HttpContext.Request.Method;
            string Controller = context.ActionDescriptor.RouteValues["controller"].ToString();
            string Action = context.ActionDescriptor.RouteValues["action"].ToString();
            ParameterDescriptor[] paramaters = context.ActionDescriptor.Parameters.ToArray();

            base.OnActionExecuting(context);

            //// TODO implement some business logic for this...
            //if (context.HttpContext.Request.Method.Equals("GET"))
            //{
            //    context.HttpContext.Response.StatusCode = (Int32)HttpStatusCode.BadRequest;

            //    Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState = new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary();
            //    modelState.AddModelError("Error", "Not Get");

            //    Microsoft.AspNetCore.Mvc.BadRequestObjectResult badRequest = new Microsoft.AspNetCore.Mvc.BadRequestObjectResult(modelState);
            //    context.Result = badRequest;
            //}
            //else
            //{
            //    base.OnActionExecuting(context);
            //}
        }
    }
}

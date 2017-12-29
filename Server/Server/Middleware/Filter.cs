using EntityModel.DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Server.Middleware
{
    public class Filter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (CheckRole(context) == HttpStatusCode.BadRequest)
            {
                UnauthorizedResult unauthorized = new UnauthorizedResult();
                context.Result = unauthorized;
            }
            else
            {
                base.OnActionExecuting(context);
            }

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

        HttpStatusCode CheckRole(ActionExecutingContext context)
        {
            try
            {
                //IPAddress address = context.HttpContext.Connection.RemoteIpAddress;

                //ControllerActionDescriptor descriptor = (ControllerActionDescriptor)context.ActionDescriptor;
                //string Method = context.HttpContext.Request.Method.ToLower();
                //string Controller = descriptor.ControllerName.ToLower();
                //string Action = descriptor.ActionName.ToLower();
                //string Template = descriptor.AttributeRouteInfo.Template.ToLower();

                //IDictionary<string, object> dParams = context.ActionArguments;

                //aModel db = new aModel();

                //xAccount account = db.xAccount.Find(dParams["IDAccount"]);
                //if (account == null)
                //    return HttpStatusCode.BadRequest;

                //xUserFeature userFeature = db.xUserFeature
                //    .FirstOrDefault(x =>
                //        x.IDPermission == account.IDPermission &&
                //        x.Controller.Equals(Controller) &&
                //        x.Action.Equals(Action) &&
                //        x.Method.Equals(Method) &&
                //        x.Template.Equals(Template));
                //if (userFeature == null)
                //    return HttpStatusCode.BadRequest;

                //if (userFeature.TrangThai == 3)
                //    return HttpStatusCode.BadRequest;

                return HttpStatusCode.OK;
            }
            catch
            {
                return HttpStatusCode.BadRequest;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EntityModel.DataModel;
using Server.Service;
using System.Reflection;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    public class FeatureController : BaseController<xFeature>
    {
        public FeatureController(IRepositoryCollection Collection) : base(Collection)
        {
        }

        [Route("GetController")]
        public IActionResult GetController()
        {
            List<xFeature> lstFeatures = new List<xFeature>();

            Assembly asm = Assembly.GetExecutingAssembly();

            var Controllers = asm.GetExportedTypes()
                .Where(x => typeof(ControllerBase).IsAssignableFrom(x) && !x.Name.Equals(typeof(BaseController<>).Name))
                .Select(x => new
                {
                    Controller = x.Name,
                    Methods = x.GetMethods().Where(y => y.DeclaringType.IsSubclassOf(typeof(ControllerBase)) && y.IsPublic && !y.IsStatic).ToList()
                })
                .Select(x => new
                {
                    Controller = x.Controller,
                    Actions = x.Methods.Select(y => new { Action = y.Name, Attributes = y.GetCustomAttributes(true).ToList() })
                });

            foreach (var controller in Controllers)
            {
                List<xFeature> lstTemps = new List<xFeature>();

                foreach (var action in controller.Actions)
                {
                    xFeature f = new xFeature();
                    f.Controller = controller.Controller;
                    f.Action = action.Action;

                    HttpGetAttribute attr_Get = (HttpGetAttribute)action.Attributes.FirstOrDefault(x => x.GetType() == typeof(HttpGetAttribute));
                    if (attr_Get != null)
                    {
                        f.Method = HttpMethods.Get.ToLower();
                        f.Template = string.IsNullOrWhiteSpace(attr_Get.Template) ? string.Empty : attr_Get.Template;
                        lstTemps.Add(f);
                    }

                    HttpPostAttribute attr_Post = (HttpPostAttribute)action.Attributes.FirstOrDefault(x => x.GetType() == typeof(HttpPostAttribute));
                    if (attr_Post != null)
                    {
                        f.Method = HttpMethods.Post.ToLower();
                        f.Template = string.IsNullOrWhiteSpace(attr_Post.Template) ? string.Empty : attr_Post.Template;
                        lstTemps.Add(f);
                    }

                    HttpPutAttribute attr_Put = (HttpPutAttribute)action.Attributes.FirstOrDefault(x => x.GetType() == typeof(HttpPutAttribute));
                    if (attr_Put != null)
                    {
                        f.Method = HttpMethods.Put.ToLower();
                        f.Template = string.IsNullOrWhiteSpace(attr_Put.Template) ? string.Empty : attr_Put.Template;
                        lstTemps.Add(f);
                    }

                    HttpDeleteAttribute attr_Delete = (HttpDeleteAttribute)action.Attributes.FirstOrDefault(x => x.GetType() == typeof(HttpDeleteAttribute));
                    if (attr_Delete != null)
                    {
                        f.Method = HttpMethods.Delete.ToLower();
                        f.Template = string.IsNullOrWhiteSpace(attr_Delete.Template) ? string.Empty : attr_Delete.Template;
                        lstTemps.Add(f);
                    }

                    RouteAttribute attr_Route = (RouteAttribute)action.Attributes.FirstOrDefault(x => x.GetType() == typeof(RouteAttribute));
                    if (attr_Route != null)
                    {
                        f.Method = string.IsNullOrWhiteSpace(f.Method) ? HttpMethods.Get.ToLower() : f.Method;
                        f.Template = string.IsNullOrWhiteSpace(attr_Route.Template) ? string.Empty : attr_Route.Template;
                        lstTemps.Add(f);
                    }
                }

                lstFeatures.AddRange(lstTemps);
            }

            return Ok(lstFeatures.Select(x => new
            {
                Controller = x.Controller,
                Action = x.Action,
                Method = x.Method,
                Template = x.Template
            }).ToList());
        }
    }
}

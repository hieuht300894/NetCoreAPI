using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EntityModel.DataModel;
using Server.Service;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Server.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    public class FeatureController : BaseController<xFeature>
    {
        public FeatureController(IRepositoryCollection Collection) : base(Collection)
        {
        }

        [HttpPost("GetController")]
        public async Task<IActionResult> GetController()
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

            DateTime time = DateTime.Now;

            foreach (var controller in Controllers)
            {
                List<xFeature> lstTemps = new List<xFeature>();

                foreach (var action in controller.Actions)
                {
                    xFeature f = new xFeature();
                    f.KeyID = 0;
                    f.NgayTao = time;
                    f.Controller = controller.Controller.ToLower();
                    f.Action = action.Action.ToLower();

                    HttpGetAttribute attr_Get = (HttpGetAttribute)action.Attributes.FirstOrDefault(x => x.GetType() == typeof(HttpGetAttribute));
                    if (attr_Get != null)
                    {
                        f.Method = HttpMethods.Get.ToLower();
                        f.Template = string.IsNullOrWhiteSpace(attr_Get.Template) ? string.Empty : attr_Get.Template.ToLower();
                        lstTemps.Add(f);
                    }

                    HttpPostAttribute attr_Post = (HttpPostAttribute)action.Attributes.FirstOrDefault(x => x.GetType() == typeof(HttpPostAttribute));
                    if (attr_Post != null)
                    {
                        f.Method = HttpMethods.Post.ToLower();
                        f.Template = string.IsNullOrWhiteSpace(attr_Post.Template) ? string.Empty : attr_Post.Template.ToLower();
                        lstTemps.Add(f);
                    }

                    HttpPutAttribute attr_Put = (HttpPutAttribute)action.Attributes.FirstOrDefault(x => x.GetType() == typeof(HttpPutAttribute));
                    if (attr_Put != null)
                    {
                        f.Method = HttpMethods.Put.ToLower();
                        f.Template = string.IsNullOrWhiteSpace(attr_Put.Template) ? string.Empty : attr_Put.Template.ToLower();
                        lstTemps.Add(f);
                    }

                    HttpDeleteAttribute attr_Delete = (HttpDeleteAttribute)action.Attributes.FirstOrDefault(x => x.GetType() == typeof(HttpDeleteAttribute));
                    if (attr_Delete != null)
                    {
                        f.Method = HttpMethods.Delete.ToLower();
                        f.Template = string.IsNullOrWhiteSpace(attr_Delete.Template) ? string.Empty : attr_Delete.Template.ToLower();
                        lstTemps.Add(f);
                    }

                    RouteAttribute attr_Route = (RouteAttribute)action.Attributes.FirstOrDefault(x => x.GetType() == typeof(RouteAttribute));
                    if (attr_Route != null)
                    {
                        f.Method = string.IsNullOrWhiteSpace(f.Method) ? HttpMethods.Get.ToLower() : f.Method;
                        f.Template = string.IsNullOrWhiteSpace(attr_Route.Template) ? string.Empty : attr_Route.Template.ToLower();
                        lstTemps.Add(f);
                    }
                }

                lstFeatures.AddRange(lstTemps);
            }

            return await SaveData(lstFeatures.ToArray());
        }

        async Task<IActionResult> SaveData(xFeature[] features)
        {
            try
            {
                Instance.Context = new aModel();
                await Instance.BeginTransaction();
                IEnumerable<xFeature> lstRemoves = Instance.Context.xFeature.ToList();
                Instance.Context.xFeature.RemoveRange(lstRemoves.ToArray());
                await Instance.Context.AddRangeAsync(features.ToArray());
                await Instance.SaveChanges();
                Instance.CommitTransaction();
                return Ok(features);
            }
            catch (Exception ex)
            {
                Instance.RollbackTransaction();
                ModelState.AddModelError("Exception", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}

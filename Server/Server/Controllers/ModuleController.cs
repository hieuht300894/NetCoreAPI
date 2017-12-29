using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Service;
using Server.Model;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using EntityModel.DataModel;
using Server.Middleware;
using System.Reflection;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("API/[controller]")]
    public class ModuleController : Controller
    {
        [ServiceFilter(typeof(Filter))]
        [HttpPost("DataSeed")]
        public async Task<IEnumerable<IActionResult>> DataSeed()
        {
            IList<IActionResult> lstResult = new List<IActionResult>();

            lstResult.Add(await InitAgency());
            lstResult.Add(await InitTienTe());
            lstResult.Add(await InitTinhThanh());
            lstResult.Add(await InitDonViTinh());

            return lstResult;
        }
        async Task<IActionResult> InitAgency()
        {
            aModel db = new aModel();

            if (db.xAgency.Count() == 0)
            {
                try
                {
                    string Query = System.IO.File.ReadAllText($@"{Directory.GetCurrentDirectory()}\wwwroot\InitData\DATA_xAgency.sql");
                    await db.Database.ExecuteSqlCommandAsync(Query, new SqlParameter[] { });
                    return Ok($"Init data {(typeof(xAgency).Name)} success.");
                }
                catch (Exception ex) { return BadRequest($"Init data {(typeof(xAgency).Name)} fail: {ex}"); }
            }

            return Ok($"No init {(typeof(xAgency).Name)} data");
        }
        async Task<IActionResult> InitTienTe()
        {
            aModel db = new aModel();

            if (db.eTienTe.Count() == 0)
            {
                try
                {
                    string Query = System.IO.File.ReadAllText($@"{Directory.GetCurrentDirectory()}\wwwroot\InitData\DATA_eTienTe.sql");
                    await db.Database.ExecuteSqlCommandAsync(Query, new SqlParameter[] { });
                    return Ok($"Init data {(typeof(eTienTe).Name)} success.");
                }
                catch (Exception ex) { return BadRequest($"Init data {(typeof(eTienTe).Name)} fail: {ex}"); }
            }
            return Ok($"No init {(typeof(eTienTe).Name)} data");
        }
        async Task<IActionResult> InitTinhThanh()
        {
            aModel db = new aModel();

            if (db.eTinhThanh.Count() == 0)
            {
                try
                {
                    string Query = System.IO.File.ReadAllText($@"{Directory.GetCurrentDirectory()}\wwwroot\InitData\DATA_eTinhThanh.sql");
                    await db.Database.ExecuteSqlCommandAsync(Query, new SqlParameter[] { });
                    return Ok($"Init data {(typeof(eTinhThanh).Name)} success.");
                }
                catch (Exception ex) { return BadRequest($"Init data {(typeof(eTinhThanh).Name)} fail: {ex}"); }
            }
            return Ok($"No init {(typeof(eTinhThanh).Name)} data");
        }
        async Task<IActionResult> InitDonViTinh()
        {
            aModel db = new aModel();

            if (db.eDonViTinh.Count() == 0)
            {
                try
                {
                    string Query = System.IO.File.ReadAllText($@"{Directory.GetCurrentDirectory()}\wwwroot\InitData\DATA_eDonViTinh.sql");
                    await db.Database.ExecuteSqlCommandAsync(Query, new SqlParameter[] { });
                    return Ok($"Init data {(typeof(eDonViTinh).Name)} success.");
                }
                catch (Exception ex) { return BadRequest($"Init data {(typeof(eDonViTinh).Name)} fail: {ex}"); }
            }
            return Ok($"No init {(typeof(eDonViTinh).Name)} data");
        }

        [HttpGet("TimeServer")]
        public async Task<DateTime> TimeServer()
        {
            try { return await Task.Factory.StartNew(() => { return DateTime.Now; }); }
            catch { return DateTime.Now; }

        }

        [HttpPost("InitUser")]
        public async Task<IActionResult> InitUser()
        {
            aModel db = new aModel();
            DateTime time = DateTime.Now;

            try
            {
                await db.Database.BeginTransactionAsync();

                xPermission permission = new xPermission()
                {
                    KeyID = 0,
                    Ma = "ADMIN",
                    Ten = "ADMIN",
                    NgayTao = time
                };
                await db.xPermission.AddAsync(permission);
                await db.SaveChangesAsync();

                xPersonnel personnel = new xPersonnel()
                {
                    KeyID = 0,
                    Ma = "NV0001",
                    Ten = "Nhân viên 0001",
                    NgayTao = time
                };
                await db.xPersonnel.AddAsync(personnel);
                await db.SaveChangesAsync();

                xAccount account = new xAccount()
                {
                    KeyID = personnel.KeyID,
                    NgayTao = time,
                    PersonelName = personnel.Ten,
                    UserName = "admin",
                    Password = "admin",
                    IDPermission = permission.KeyID,
                    PermissionName = permission.Ten
                };
                await db.xAccount.AddAsync(account);
                await db.SaveChangesAsync();

                List<xFeature> features = await db.xFeature.ToListAsync();
                List<xUserFeature> userFeatures = new List<xUserFeature>();
                foreach (xFeature f in features)
                {
                    userFeatures.Add(new xUserFeature()
                    {
                        KeyID = 0,
                        IDPermission = permission.KeyID,
                        PermissionName = permission.Ten,
                        IDFeature = f.KeyID,
                        Controller = f.Controller,
                        Action = f.Action,
                        Method = f.Method,
                        Template = f.Template,
                        Path = f.Path,
                        NgayTao = time
                    });
                }
                await db.xUserFeature.AddRangeAsync(userFeatures.ToArray());
                await db.SaveChangesAsync();

                db.Database.CommitTransaction();
                return Ok(userFeatures);
            }
            catch (Exception ex)
            {
                db.Database.RollbackTransaction();
                ModelState.AddModelError("Exception_Message", ex.Message);
                ModelState.AddModelError("Exception_InnerException_Message", ex.InnerException.Message);
                return BadRequest(ModelState);
            }
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
                    Controller = x.Controller.ToLower().Replace("controller", string.Empty),
                    Actions = x.Methods.Select(y => new { Action = y.Name.ToLower(), Attributes = y.GetCustomAttributes(true).ToList() })
                });

            DateTime time = DateTime.Now;

            foreach (var controller in Controllers)
            {
                List<xFeature> lstTemps = new List<xFeature>();

                foreach (var action in controller.Actions)
                {
                    xFeature f = new xFeature();

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

                    f.KeyID = 0;
                    f.NgayTao = time;
                    f.Controller = controller.Controller;
                    f.Action = action.Action;
                    f.Path = string.Join('/', "api", f.Controller, f.Template).TrimEnd('/');
                }

                lstFeatures.AddRange(lstTemps);
            }

            return await SaveData(lstFeatures.ToArray());
        }
        async Task<IActionResult> SaveData(xFeature[] features)
        {
            aModel db = new aModel();
            try
            {
                await db.Database.BeginTransactionAsync();
                IEnumerable<xFeature> lstRemoves = await db.xFeature.ToListAsync();
                db.xFeature.RemoveRange(lstRemoves.ToArray());
                await db.AddRangeAsync(features.ToArray());
                await db.SaveChangesAsync();
                db.Database.CommitTransaction();
                return Ok(features);
            }
            catch (Exception ex)
            {
                db.Database.RollbackTransaction();
                ModelState.AddModelError("Exception_Message", ex.Message);
                ModelState.AddModelError("Exception_InnerException_Message", ex.InnerException.Message);
                return BadRequest(ModelState);
            }
        }
    }
}

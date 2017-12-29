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
                        NgayTao = time
                    });
                }
                await db.xUserFeature.AddRangeAsync(userFeatures.ToArray());
                await db.SaveChangesAsync();

                db.Database.CommitTransaction();
                return Ok();
            }
            catch (Exception ex)
            {
                db.Database.RollbackTransaction();
                ModelState.AddModelError("Exception_Message", ex.Message);
                ModelState.AddModelError("Exception_InnerException_Message", ex.InnerException.Message);
                return BadRequest(ModelState);
            }
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
    }
}

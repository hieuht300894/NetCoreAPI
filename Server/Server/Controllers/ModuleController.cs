﻿using System;
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
        [Route("DataSeed")]
        public async Task<IEnumerable<IActionResult>> DataSeed()
        {
            IList<IActionResult> lstResult = new List<IActionResult>();

            lstResult.Add(await InitAgency());
            lstResult.Add(await InitTienTe());
            lstResult.Add(await InitTinhThanh());
            lstResult.Add(await InitDonViTinh());

            return lstResult;
        }

        [Route("TimeServer")]
        public async Task<DateTime> TimeServer()
        {
            try { return await Task.Factory.StartNew(() => { return DateTime.Now; }); }
            catch { return DateTime.Now; }

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

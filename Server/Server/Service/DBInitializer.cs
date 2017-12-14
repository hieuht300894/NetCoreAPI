using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Server.Model;
using Server.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Service
{
    public class DBInitializer
    {
        public static void InitData()
        {
            InitAgency();
            InitTienTe();
            InitTinhThanh();
            InitDonViTinh();
        }
        async static void InitAgency()
        {
            aModel db = new aModel();
            await Task.Factory.StartNew(() =>
            {
                if (db.xAgency.Count() == 0)
                {
                    try
                    {
                        string Query = File.ReadAllText($@"{Directory.GetCurrentDirectory()}\wwwroot\InitData\DATA_xAgency.sql");
                        db.Database.ExecuteSqlCommandAsync(Query, new SqlParameter[] { }).Wait();
                    }
                    catch { }
                }
            });
        }
        async static void InitTienTe()
        {
            aModel db = new aModel();
            await Task.Factory.StartNew(() =>
            {
                if (db.eTienTe.Count() == 0)
                {
                    try
                    {
                        string Query = File.ReadAllText($@"{Directory.GetCurrentDirectory()}\wwwroot\InitData\DATA_eTienTe.sql");
                        db.Database.ExecuteSqlCommandAsync(Query, new SqlParameter[] { }).Wait();
                    }
                    catch { }
                }
            });
        }
        async static void InitTinhThanh()
        {
            aModel db = new aModel();
            await Task.Factory.StartNew(() =>
            {
                if (db.eTinhThanh.Count() == 0)
                {
                    try
                    {
                        string Query = File.ReadAllText($@"{Directory.GetCurrentDirectory()}\wwwroot\InitData\DATA_eTinhThanh.sql");
                        db.Database.ExecuteSqlCommandAsync(Query, new SqlParameter[] { }).Wait();
                    }
                    catch { }
                }
            });
        }
        async static void InitDonViTinh()
        {
            aModel db = new aModel();
            await Task.Factory.StartNew(() =>
            {
                if (db.eDonViTinh.Count() == 0)
                {
                    try
                    {
                        string Query = File.ReadAllText($@"{Directory.GetCurrentDirectory()}\wwwroot\InitData\DATA_eDonViTinh.sql");
                        db.Database.ExecuteSqlCommandAsync(Query, new SqlParameter[] { }).Wait();
                    }
                    catch { }
                }
            });
        }
    }
}

using EntityModel.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Server.Model
{
    public class aModel : zModel
    {
        public aModel() : base()
        {
        }
        public aModel(DbContextOptions<zModel> options) : base(options)
        {

        }

        public override int SaveChanges()
        {
            List<EntityEntry> entries = new List<EntityEntry>(ChangeTracker.Entries()
            .Where(e => (e.Entity.GetType().Name.StartsWith("e") || e.Entity.GetType().Name.StartsWith("x")) && (e.State == EntityState.Added || e.State == EntityState.Deleted || e.State == EntityState.Modified))
            .ToList());
            var lstObjs = AutoLog(entries);
            int res = base.SaveChanges();
            SaveLog(lstObjs);
            return res;
        }
        private List<ObjectBinding> AutoLog(List<EntityEntry> lstEntries)
        {
            List<ObjectBinding> lstObjs = new List<ObjectBinding>();
            foreach (var entry in lstEntries)
            {
                ObjectBinding obj = new ObjectBinding();

                if (entry.State == EntityState.Added)
                {
                    obj.State = entry.State;
                    obj.Entity = entry;
                    obj.CurrentValues = entry.CurrentValues;
                    lstObjs.Add(obj);
                }
                else if (entry.State == EntityState.Modified)
                {
                    obj.State = entry.State;
                    obj.Entity = entry;
                    obj.OriginalValues = entry.OriginalValues;
                    obj.CurrentValues = entry.CurrentValues;
                    lstObjs.Add(obj);
                }
                else if (entry.State == EntityState.Deleted)
                {
                    obj.State = entry.State;
                    obj.Entity = entry;
                    obj.OriginalValues = entry.OriginalValues;
                    lstObjs.Add(obj);
                }
            }
            return lstObjs;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            List<EntityEntry> entries = new List<EntityEntry>(ChangeTracker.Entries()
              .Where(e => (e.Entity.GetType().Name.StartsWith("e") || e.Entity.GetType().Name.StartsWith("x")) && (e.State == EntityState.Added || e.State == EntityState.Deleted || e.State == EntityState.Modified))
              .ToList());
            List<ObjectBinding> lstObjs = await AutoLogAsync(entries);
            var res = await base.SaveChangesAsync();
            SaveLog(lstObjs);
            return res;
        }
        private async Task<List<ObjectBinding>> AutoLogAsync(List<EntityEntry> lstEntries)
        {
            return await Task.Factory.StartNew(() =>
            {
                List<ObjectBinding> lstObjs = new List<ObjectBinding>();

                foreach (var entry in lstEntries)
                {
                    ObjectBinding obj = new ObjectBinding();

                    if (entry.State == EntityState.Added)
                    {
                        obj.State = entry.State;
                        obj.Entity = entry;
                        obj.CurrentValues = entry.CurrentValues;
                        lstObjs.Add(obj);
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        obj.State = entry.State;
                        obj.Entity = entry;
                        obj.OriginalValues = entry.OriginalValues;
                        obj.CurrentValues = entry.CurrentValues;
                        lstObjs.Add(obj);
                    }
                    else if (entry.State == EntityState.Deleted)
                    {
                        obj.State = entry.State;
                        obj.Entity = entry;
                        obj.OriginalValues = entry.OriginalValues;
                        lstObjs.Add(obj);
                    }
                }
                return lstObjs;
            });
        }

        private async void SaveLog(List<ObjectBinding> lstObjs)
        {
            try
            {
                using (zModel db = new zModel())
                {
                    DateTime CurrentDate = DateTime.Now;

                    foreach (var obj in lstObjs)
                    {
                        xLog log = new xLog();
                        log.KeyID = 0;
                        log.NguoiTao = 0;
                        log.NgayTao = CurrentDate;
                        log.Bang = obj.Entity.Entity.GetType().Name;
                        log.ThaoTac = obj.State.ToString();
                        log.TrangThai = (Int32)obj.State;

                        if (obj.OriginalValues != null)
                        {
                            Dictionary<string, object> ParamsValues = new Dictionary<string, object>();
                            foreach (IProperty prop in obj.OriginalValues.Properties) { ParamsValues.Add(prop.Name, obj.OriginalValues[prop.Name]); }
                            log.GiaTriCu = ParamsValues.SerializeJSON();
                        }
                        else
                        {
                            Dictionary<string, object> ParamsValues = new Dictionary<string, object>();
                            log.GiaTriCu = ParamsValues.SerializeJSON();
                        }
                        if (obj.CurrentValues != null)
                        {
                            Dictionary<string, object> ParamsValues = new Dictionary<string, object>();
                            foreach (IProperty prop in obj.CurrentValues.Properties) { ParamsValues.Add(prop.Name, obj.CurrentValues[prop.Name]); }
                            log.GiaTriMoi = ParamsValues.SerializeJSON();
                        }
                        else
                        {
                            Dictionary<string, object> ParamsValues = new Dictionary<string, object>();
                            log.GiaTriMoi = ParamsValues.SerializeJSON();
                        }
                        db.xLog.Add(log);
                    }
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }

    public class ObjectBinding
    {
        public EntityState State { get; set; }
        public EntityEntry Entity { get; set; }
        public PropertyValues CurrentValues { get; set; }
        public PropertyValues OriginalValues { get; set; }
    }

    public static class Json
    {
        public static T Clone<T>(this T source)
        {
            var serialized = JsonConvert.SerializeObject(
                source,
                Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            return JsonConvert.DeserializeObject<T>(serialized);
        }
        public static List<T> Clone<T>(this List<T> source)
        {
            var serialized = JsonConvert.SerializeObject(
                source,
                Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            return JsonConvert.DeserializeObject<List<T>>(serialized);
        }
        public static string SerializeJSON<T>(this T source)
        {
            var serialized = JsonConvert.SerializeObject(
                source,
                Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            return serialized;
        }
        public static T DeserializeJSON<T>(this string source) where T : new()
        {
            try { return JsonConvert.DeserializeObject<T>(source); }
            catch { return new T(); }
        }
    }
}

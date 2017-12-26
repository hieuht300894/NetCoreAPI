using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Server.Model;
using Server.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Server.Service
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        public aModel Context { get; set; }

        public Repository(aModel db)
        {
            Context = db;
        }

        public async Task<String> GetCode(String Prefix)
        {
            String bRe = Prefix + DateTime.Now.ToString("yyyyMMdd");

            try
            {
                DateTime time = DateTime.Now;

                IEnumerable<T> lstTemp = await Context.Set<T>().ToListAsync();
                T Item = lstTemp.OrderByDescending<T, T>("KeyID").FirstOrDefault();
                if (Item == null)
                {
                    bRe += "0001";
                }
                else
                {
                    String Code = Item.GetObjectByName<String>("Ma");
                    if (Code.StartsWith(bRe))
                    {
                        Int32 number = Int32.Parse(Code.Replace(bRe, String.Empty));
                        ++number;
                        bRe = String.Format("{0}{1:0000}", bRe, number);
                    }
                    else
                        bRe += "0001";
                }
                return bRe;
            }
            catch { return bRe += "0001"; }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                Context = new aModel();
                IEnumerable<T> lstTemp = await Context.Set<T>().ToListAsync();
                IList<T> lstResult = lstTemp.OrderBy<T, String>("Ten").ToList();
                return lstResult;
            }
            catch { return new List<T>(); }
        }

        public async Task<T> GetByID(Object id)
        {
            try
            {
                Context = new aModel();
                T item = await Context.Set<T>().FindAsync(id.ConvertType<T>());
                return item ?? new T();
            }
            catch { return new T(); }
        }

        public async Task<Boolean> AddEntry(T Item)
        {
            try
            {
                Context = new aModel();
                await Context.Database.BeginTransactionAsync();
                await Context.Set<T>().AddAsync(Item);
                await Context.SaveChangesAsync();
                Context.Database.CommitTransaction();
                return true;
            }
            catch
            {
                Context.Database.RollbackTransaction();
                return false;
            }
        }

        public async Task<Boolean> AddEntries(T[] Items)
        {
            try
            {
                Context = new aModel();
                Items = Items ?? new T[] { };
                await Context.Database.BeginTransactionAsync();
                await Context.Set<T>().AddRangeAsync(Items);
                await Context.SaveChangesAsync();
                Context.Database.CommitTransaction();
                return true;
            }
            catch
            {
                Context.Database.RollbackTransaction();
                return false;
            }
        }

        public async Task<Boolean> UpdateEntry(T Item)
        {
            try
            {
                Context = new aModel();
                await Context.Database.BeginTransactionAsync();
                Context.Set<T>().Update(Item);
                await Context.SaveChangesAsync();
                Context.Database.CommitTransaction();
                return true;
            }
            catch
            {
                Context.Database.RollbackTransaction();
                return false;
            }
        }

        public async Task<Boolean> UpdateEntries(T[] Items)
        {
            try
            {
                Context = new aModel();
                Items = Items ?? new T[] { };
                await Context.Database.BeginTransactionAsync();
                Context.Set<T>().UpdateRange(Items);
                await Context.SaveChangesAsync();
                Context.Database.CommitTransaction();
                return true;
            }
            catch
            {
                Context.Database.RollbackTransaction();
                return false;
            }
        }

        public async Task<Boolean> DeleteEntry(Object id)
        {
            try
            {
                Context = new aModel();
                await Context.Database.BeginTransactionAsync();
                T Item = await Context.Set<T>().FindAsync(id);
                Context.Set<T>().Remove(Item);
                await Context.SaveChangesAsync();
                Context.Database.CommitTransaction();
                return true;
            }
            catch
            {
                Context.Database.RollbackTransaction();
                return false;
            }
        }

        public async Task<Boolean> DeleteEntry(T Item)
        {
            try
            {
                Context = new aModel();
                await Context.Database.BeginTransactionAsync();
                Context.Set<T>().Attach(Item);
                Context.Set<T>().Remove(Item);
                await Context.SaveChangesAsync();
                Context.Database.CommitTransaction();
                return true;
            }
            catch
            {
                Context.Database.RollbackTransaction();
                return false;
            }
        }

        public async Task<Boolean> DeleteEntries(Object[] ids)
        {
            try
            {
                Context = new aModel();
                ids = ids ?? new object[] { };
                await Context.Database.BeginTransactionAsync();
                foreach (object id in ids)
                {
                    T Item = await Context.Set<T>().FindAsync(id);
                    Context.Set<T>().Remove(Item);
                }
                await Context.SaveChangesAsync();
                Context.Database.CommitTransaction();
                return true;
            }
            catch
            {
                Context.Database.RollbackTransaction();
                return false;
            }
        }

        public async Task<Boolean> DeleteEntries(T[] Items)
        {
            try
            {
                Context = new aModel();
                Items = Items ?? new T[] { };
                await Context.Database.BeginTransactionAsync();
                foreach (T Item in Items)
                {
                    Context.Set<T>().Attach(Item);
                    Context.Set<T>().Remove(Item);
                }
                await Context.SaveChangesAsync();
                Context.Database.CommitTransaction();
                return true;
            }
            catch
            {
                Context.Database.RollbackTransaction();
                return false;
            }
        }

        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return await Context.Database.BeginTransactionAsync();
        }

        public async Task<Int32> SaveChanges()
        {
            return await Context.SaveChangesAsync();
        }

        public void CommitTransaction()
        {
            Context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            Context.Database.RollbackTransaction();
        }
    }

    public class RepositoryCollection : IRepositoryCollection
    {
        private aModel db;

        public RepositoryCollection(aModel db)
        {
            this.db = db;
        }

        public Repository<T> GetRepository<T>() where T : class, new()
        {
            return new Repository<T>(db);
        }
    }
}

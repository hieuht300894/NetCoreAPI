using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                Context = new aModel();
                IEnumerable<T> lstResult = await Context.Set<T>().ToListAsync();
                return lstResult;
            }
            catch { return new List<T>(); }
        }

        public async Task<T> GetByID(object id)
        {
            try
            {
                Context = new aModel();
                T item = await Context.Set<T>().FindAsync(id.ConvertType<T>());
                return item ?? new T();
            }
            catch { return new T(); }
        }

        public async Task<bool> AddEntry(T Item)
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

        public async Task<bool> AddEntries(T[] Items)
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

        public async Task<bool> UpdateEntry(T Item)
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

        public async Task<bool> UpdateEntries(T[] Items)
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

        public async Task<bool> DeleteEntry(object id)
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

        public async Task<bool> DeleteEntry(T Item)
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

        public async Task<bool> DeleteEntries(object[] ids)
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

        public async Task<bool> DeleteEntries(T[] Items)
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

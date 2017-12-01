using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Service
{
    public interface IRepository<T> where T : class, new()
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByID(object id);
        Task<bool> AddEntry(T item);
        Task<bool> AddEntries(T[] items);
        Task<bool> UpdateEntry(T item);
        Task<bool> UpdateEntries(T[] items);
        Task<bool> DeleteEntry(object id);
        Task<bool> DeleteEntry(T item);
        Task<bool> DeleteEntries(object[] ids);
        Task<bool> DeleteEntries(T[] items);

    }
    public interface IRepositoryCollection
    {
        Repository<T> GetRepository<T>() where T : class, new();
    }
}

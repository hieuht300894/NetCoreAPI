﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Service;
using Server.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("API/[controller]")]
    public class BaseController<T> : Controller where T : class, new()
    {
        protected Repository<T> Instance;

        public BaseController(IRepositoryCollection Collection)
        {
            Instance = Collection.GetRepository<T>();
        }

        [HttpGet]
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            IEnumerable<T> Items = await Instance.GetAll();
            return Items;
        }

        [HttpGet("{id}")]
        public virtual async Task<T> GetByID(String id)
        {
            T Item = await Instance.GetByID(id);
            return Item;
        }

        [HttpPost]
        public virtual async Task<IActionResult> AddEntries([FromBody] T[] Items)
        {
            try
            {
                Instance.Context = new aModel();
                Items = Items ?? new T[] { };

                await Instance.Context.Database.BeginTransactionAsync();
                await Instance.Context.Set<T>().AddRangeAsync(Items);
                await Instance.Context.SaveChangesAsync();
                Instance.Context.Database.CommitTransaction();
                return Ok(Items);
            }
            catch
            {
                Instance.Context.Database.RollbackTransaction();
                return BadRequest();
            }
        }

        [HttpPut]
        public virtual async Task<IActionResult> UpdateEntries([FromBody] T[] Items)
        {
            try
            {
                Instance.Context = new aModel();
                Items = Items ?? new T[] { };
                await Instance.Context.Database.BeginTransactionAsync();
                Instance.Context.Set<T>().UpdateRange(Items);
                await Instance.Context.SaveChangesAsync();
                Instance.Context.Database.CommitTransaction();
                return Ok(Items);
            }
            catch
            {
                Instance.Context.Database.RollbackTransaction();
                return BadRequest();
            }
        }

        [HttpDelete]
        public virtual async Task<IActionResult> DeleteEntries([FromBody] T[] Items)
        {
            try
            {
                Instance.Context = new aModel();
                Items = Items ?? new T[] { };
                await Instance.Context.Database.BeginTransactionAsync();
                foreach (T Item in Items)
                {
                    Instance.Context.Set<T>().Attach(Item);
                    Instance.Context.Set<T>().Remove(Item);
                }
                await Instance.Context.SaveChangesAsync();
                Instance.Context.Database.CommitTransaction();
                return NoContent();
            }
            catch
            {
                Instance.Context.Database.RollbackTransaction();
                return BadRequest();
            }
        }
    }
}

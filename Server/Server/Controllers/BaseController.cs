using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Service;

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
        public virtual async Task<IActionResult> GetAll()
        {
            IEnumerable<T> Items = await Instance.GetAll();
            return Ok(Items);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetByID(String id)
        {
            T Item = await Instance.GetByID(id);
            return Ok(Item);
        }

        //[HttpPost]
        //public async Task<IActionResult> AddEntry([FromBody] T Item)
        //{
        //    await Instance.AddEntry(Item);
        //    return Ok(Item);
        //}

        [HttpPost]
        public virtual async Task<IActionResult> AddEntries([FromBody] T[] Items)
        {
            await Instance.AddEntries(Items);
            return Ok(Items);
        }

        //[HttpPut]
        //public async Task<IActionResult> UpdateEntry([FromBody] T Item)
        //{
        //    await Instance.UpdateEntry(Item);
        //    return Ok(Item);
        //}

        [HttpPut]
        public virtual async Task<IActionResult> UpdateEntries([FromBody] T[] Items)
        {
            await Instance.UpdateEntries(Items);
            return Ok(Items);
        }
    }
}

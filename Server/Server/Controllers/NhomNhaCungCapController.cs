using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EntityModel.DataModel;
using Server.Service;
using Server.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    public class NhomNhaCungCapController : BaseController<eNhomNhaCungCap>
    {
        public NhomNhaCungCapController(IRepositoryCollection Collection) : base(Collection)
        {
        }

        public async override Task<IActionResult> AddEntries([FromBody] eNhomNhaCungCap[] Items)
        {
            try
            {
                Instance.Context = new aModel();
                Items = Items ?? new eNhomNhaCungCap[] { };
                await Instance.Context.Database.BeginTransactionAsync();
                foreach (eNhomNhaCungCap item in Items)
                {
                    item.KeyID = item.KeyID > 0 ? item.KeyID : 0;

                    if (item.KeyID == 0)
                        await Instance.Context.eNhomNhaCungCap.AddAsync(item);
                    else
                        Instance.Context.eNhomNhaCungCap.Update(item);
                }
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
    }
}

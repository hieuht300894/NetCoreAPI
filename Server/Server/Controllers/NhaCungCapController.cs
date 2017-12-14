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
    public class NhaCungCapController : BaseController<eNhaCungCap>
    {
        public NhaCungCapController(IRepositoryCollection Collection) : base(Collection)
        {
        }

        public async override Task<IActionResult> AddEntries([FromBody] eNhaCungCap[] Items)
        {
            try
            {
                Instance.Context = new aModel();
                Items = Items ?? new eNhaCungCap[] { };
                await Instance.Context.Database.BeginTransactionAsync();
                foreach (eNhaCungCap item in Items)
                {
                    item.KeyID = item.KeyID > 0 ? item.KeyID : 0;

                    if (item.KeyID == 0)
                        await Instance.Context.eNhaCungCap.AddAsync(item);
                    else
                        Instance.Context.eNhaCungCap.Update(item);
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

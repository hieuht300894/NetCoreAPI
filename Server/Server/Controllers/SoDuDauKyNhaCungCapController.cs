﻿using System;
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
    public class SoDuDauKyNhaCungCapController : BaseController<eSoDuDauKyNhaCungCap>
    {
        public SoDuDauKyNhaCungCapController(IRepositoryCollection Collection) : base(Collection)
        {
        }

        public async override Task<IActionResult> AddEntries([FromBody] eSoDuDauKyNhaCungCap[] Items)
        {
            try
            {
                Instance.Context = new zModel();
                Items = Items ?? new eSoDuDauKyNhaCungCap[] { };
                await Instance.Context.Database.BeginTransactionAsync();
                foreach (eSoDuDauKyNhaCungCap item in Items)
                {
                    item.KeyID = item.KeyID > 0 ? item.KeyID : 0;

                    if (item.KeyID == 0)
                        await Instance.Context.eSoDuDauKyNhaCungCap.AddAsync(item);
                    else
                        Instance.Context.eSoDuDauKyNhaCungCap.Update(item);
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

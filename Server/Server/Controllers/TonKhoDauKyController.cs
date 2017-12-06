using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EntityModel.DataModel;
using Server.Service;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    public class TonKhoDauKyController : BaseController<eTonKhoDauKy>
    {
        public TonKhoDauKyController(IRepositoryCollection Collection) : base(Collection)
        {
        }
    }
}

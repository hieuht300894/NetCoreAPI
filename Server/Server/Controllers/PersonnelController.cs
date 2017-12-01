using EntityModel.DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Model;
using Server.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    public class PersonnelController : BaseController<xPersonnel>
    {
        public PersonnelController(IRepositoryCollection Collection) : base(Collection)
        {
        }

        //[Route("Test")]
        //public async Task<IEnumerable<eTinhThanh>> Test()
        //{
        //    Instance.Context = new zModel();
        //    Task<List<eTinhThanh>> task = Instance.Context.eTinhThanh.ToListAsync();
        //    return await task;
        //}

        //[Route("Test1")]
        //public async Task<IEnumerable<eTinhThanh>> Test1()
        //{
        //    Instance.Context = new zModel();
        //    IList<eTinhThanh> lstResult = await Instance.Context.eTinhThanh.ToListAsync();
        //    return lstResult;
        //}
    }
}

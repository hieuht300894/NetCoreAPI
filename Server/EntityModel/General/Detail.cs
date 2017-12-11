using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel.General
{
    public class Detail
    {
        public Detail()
        {
                KeyID = -Convert.ToInt32(DateTime.Now.ToString("hhmmssttSSS"));
        }

        public Int32 KeyID { get; set; }
        public String GhiChu { get; set; }
        public Int32 TrangThai { get; set; }
    }
}

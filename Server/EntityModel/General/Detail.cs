using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel.General
{
    public class Detail
    {
        private static Int32 _keyID = 0;
        public static Int32 _KeyID { get { return _keyID--; } }

        public Detail()
        {
            KeyID = _KeyID;
            TrangThai = 1;
        }

        public Int32 KeyID { get; set; }
        public String GhiChu { get; set; }
        public Int32 TrangThai { get; set; }
    }
}

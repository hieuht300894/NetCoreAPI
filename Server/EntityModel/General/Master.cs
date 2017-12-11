using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel.General
{
    public class Master
    {
        private static Int32 _keyID = 0;
        public static Int32 _KeyID { get { return _keyID--; } }

        public Master()
        {
            KeyID = _KeyID;
        }

        public Int32 KeyID { get; set; }
        public String Ma { get; set; }
        public String Ten { get; set; }
        public DateTime NgayTao { get; set; }
        public Int32 NguoiTao { get; set; }
        public String MaNguoiTao { get; set; }
        public String TenNguoiTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public Int32? NguoiCapNhat { get; set; }
        public String MaNguoiCapNhat { get; set; }
        public String TenNguoiCapNhat { get; set; }
        public String GhiChu { get; set; }
        public Int32 TrangThai { get; set; }
    }
}

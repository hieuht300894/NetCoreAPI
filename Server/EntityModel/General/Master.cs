﻿using System;
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

        public Int32 KeyID { get; set; } = _KeyID;
        public String Ma { get; set; } = string.Empty;
        public String Ten { get; set; } = string.Empty;
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public Int32 NguoiTao { get; set; } 
        public String MaNguoiTao { get; set; } = string.Empty;
        public String TenNguoiTao { get; set; } = string.Empty;
        public DateTime? NgayCapNhat { get; set; } 
        public Int32? NguoiCapNhat { get; set; } 
        public String MaNguoiCapNhat { get; set; } = string.Empty;
        public String TenNguoiCapNhat { get; set; } = string.Empty;
        public String GhiChu { get; set; } = string.Empty;
        public Int32 TrangThai { get; set; } = 1;
    }
}

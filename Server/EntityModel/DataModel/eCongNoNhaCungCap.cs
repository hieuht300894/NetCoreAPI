
namespace EntityModel.DataModel
{

    public class eCongNoNhaCungCap
    {

        public int KeyID { get; set; }

        public System.DateTime Ngay { get; set; }

        public int IDNhaCungCap { get; set; }

        public string MaNhaCungCap { get; set; }

        public string TenNhaCungCap { get; set; }

        public decimal TongTien { get; set; }

        public decimal ThanhToan { get; set; }

        public decimal ConLai { get; set; }

        public decimal NoCu { get; set; }

        public bool IsSoDuDauKy { get; set; }

        public bool IsNhapHang { get; set; }

        public bool IsTraHang { get; set; }

        public bool IsThanhToan { get; set; }

        public int IDMaster { get; set; }
    }
}

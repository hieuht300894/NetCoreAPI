
namespace EntityModel.DataModel
{
    public class eQuyDoiTienTe
    {
        public int KeyID { get; set; }
        public int IDTienTe { get; set; }
        public string TienTe { get; set; }
        public int IDTienTeQuyDoi { get; set; }
        public string TienTeQuyDoi { get; set; }
        public decimal GiaTri { get; set; }
    }
}

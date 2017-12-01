namespace EntityModel.DataModel
{
    public class xUserFeature
    {
        public int KeyID { get; set; }
        public int IDPermission { get; set; }
        public string IDFeature { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsPrintPreview { get; set; }
        public bool IsExportExcel { get; set; }
        public bool IsSave { get; set; }
        public bool IsEnable { get; set; }
    }
}

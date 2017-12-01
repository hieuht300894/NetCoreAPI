namespace EntityModel.DataModel
{
    public partial class xAccount
    {
        public int KeyID { get; set; }
        public string PersonelName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int IDPermission { get; set; }
        public string PermissionName { get; set; }
        public bool IsEnable { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public System.DateTime? ModifiedDate { get; set; }
    }
}

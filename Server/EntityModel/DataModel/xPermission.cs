namespace EntityModel.DataModel
{
    public partial class xPermission
    {
        public int KeyID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEnable { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public System.DateTime? ModifiedDate { get; set; }
    }
}

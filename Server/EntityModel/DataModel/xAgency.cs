namespace EntityModel.DataModel
{
    public partial class xAgency
    {
        public int KeyID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Credentials { get; set; }
        public string HostAddress { get; set; }
        public int HostPort { get; set; }
        public byte[] Logo { get; set; }
        public string Description { get; set; }
        public bool IsEnable { get; set; }
        public int CreatedBy { get; set; }
       public System.DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public System.DateTime? ModifiedDate { get; set; }
    }
}

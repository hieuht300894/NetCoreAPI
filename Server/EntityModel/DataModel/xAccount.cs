using EntityModel.General;

namespace EntityModel.DataModel
{
    public partial class xAccount : Master
    {
        public xAccount()
        {
            PersonelName = string.Empty;
            UserName = string.Empty;
            Password = string.Empty;
            PermissionName = string.Empty;
            IPAddress = string.Empty;
        }

        public string PersonelName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int IDPermission { get; set; }
        public string PermissionName { get; set; }
        public string IPAddress { get; set; }
    }
}

using EntityModel.General;

namespace EntityModel.DataModel
{
    public class xUserFeature : Master
    {
        public xUserFeature()
        {
            PermissionName = string.Empty;
            Controller = string.Empty;
            Action = string.Empty;
            Method = string.Empty;
            Template = string.Empty;
        }

        public int IDPermission { get; set; }
        public string PermissionName { get; set; }
        public int IDFeature { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Method { get; set; }
        public string Template { get; set; }
    }
}

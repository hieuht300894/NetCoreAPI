
namespace EntityModel.DataModel
{
    public class xLog
    {
        public int KeyID { get; set; }

        public int IDPersonnel { get; set; }

        public System.DateTime AccessDate { get; set; }

        public string State { get; set; }

        public string TableName { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }
    }
}

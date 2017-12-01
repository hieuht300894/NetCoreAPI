using DevExpress.XtraEditors;
using System.Collections.Generic;

namespace Client.Module
{
    public class ModuleHelper
    {
        public static List<FormItem> ListFormItem { get; set; } = new List<FormItem>();
    }

    public class FormItem
    {
        public XtraForm xForm { get; set; }
        public string Name { get; set; }
    }
}

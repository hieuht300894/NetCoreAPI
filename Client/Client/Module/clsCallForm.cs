using Client.GUI.Common;
using Client.GUI.DanhMuc;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Client.Module
{
    public class clsCallForm
    {
        public static void InitFormCollection()
        {
            try
            {
                Type[] types = Assembly.GetExecutingAssembly().GetTypes();
                foreach (Type type in types)
                {
                    if (type.BaseType == typeof(frmBase))
                    {
                        ModuleHelper.ListFormItem.Add(new FormItem() { fType = type, Name = type.Name });
                    }
                    if (type.BaseType == typeof(frmDanhMuc))
                    {
                        ModuleHelper.ListFormItem.Add(new FormItem() { fType = type, Name = type.Name });
                    }
                    if (type.BaseType == typeof(XtraForm))
                    {
                        ModuleHelper.ListFormItem.Add(new FormItem() { fType = type, Name = type.Name });
                    }
                }
            }
            catch { }
        }
        public static FormItem CreateNewForm(string bbiName)
        {
            return ModuleHelper.ListFormItem.Find(x => x.Name.Equals(bbiName));
        }
    }
}

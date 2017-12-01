using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Server.Utils
{
    public class ModuleHelper
    {
        public static String ConnectionString { get; set; }
        public static IConfiguration Configuration { get; set; }
        public static IServiceCollection ServiceCollection { get; set; }
        public static IApplicationBuilder ApplicationBuilder { get; set; }
        public static IHostingEnvironment HostingEnvironment { get; set; }
        public static IServiceScope ServiceScope { get; set; }
        public static List<IKey> ListKeys { get; set; }
    }

    public static class clsEntity
    {
        public static PropertyInfo GetPrimaryKey<T>() where T : class
        {
            IKey key = ModuleHelper.ListKeys.Find(x => x.DeclaringEntityType.Name.Equals(typeof(T).FullName));
            return key.Properties.FirstOrDefault().PropertyInfo;
        }
        public static object ConvertType<T>(this object obj) where T : class
        {
            PropertyInfo pInfo = GetPrimaryKey<T>();
            return
                obj != null ?
                Convert.ChangeType(obj, pInfo.PropertyType) :
                Convert.ChangeType(Activator.CreateInstance(pInfo.PropertyType), pInfo.PropertyType);
        }
    }
}

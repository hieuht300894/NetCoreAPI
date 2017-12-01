using Client.Module;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BLL.Common
{
    public class clsFunction
    {
        #region Base Method
        /// <summary>
        /// Lấy dữ liệu
        /// </summary>
        /// <returns></returns>
        public static IList<T> GetAll<T>(String api) where T : class, new()
        {
            try
            {
                IRestClient client = new RestClient(ModuleHelper.Url + "/" + api);
                IRestRequest request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                IList<T> lstResult = response.Content.DeserializeToList<T>();
                return lstResult;
            }
            catch(Exception ex) { return new List<T>(); }
        }

        /// <summary>
        /// Tìm kiếm dữ liệu
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public static T GetByID<T>(object KeyID) where T : class, new()
        {
            try { return new T(); }
            catch { return new T(); }
        }

        /// <summary>
        /// Thêm mới hoặc cập nhật dữ liệu
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public static bool AddOrUpdate<T>(T entry) where T : class, new()
        {
            try { return true; }
            catch { return false; }
        }

        /// <summary>
        /// Thêm mới hoặc cập nhật nhiều dữ liệu
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public static bool AddOrUpdate<T>(List<T> entries) where T : class, new()
        {
            try { return true; }
            catch { return false; }
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public virtual bool DeleteEntry<T>(T entry) where T : class, new()
        {
            try { return true; }
            catch { return false; }
        }
        #endregion
    }
}

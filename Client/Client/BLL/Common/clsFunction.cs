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
        public async static Task<IList<T>> GetAll<T>(String api) where T : class, new()
        {
            try
            {
                IRestClient client = new RestClient(ModuleHelper.Url + "/" + api.TrimStart('/'));
                IRestRequest request = new RestRequest();
                request.Method = Method.GET;
                IRestResponse response = await client.ExecuteTaskAsync(request);
                IList<T> lstResult = response.Content.DeserializeToList<T>();
                return lstResult;
            }
            catch (Exception ex) { return new List<T>(); }
        }

        /// <summary>
        /// Tìm kiếm dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public static async  Task<T> GetByID<T>(object KeyID) where T : class, new()
        {
            await Task.Factory.StartNew(() => { });
            try { return new T(); }
            catch { return new T(); }
        }

        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="api"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<bool> Post<T>(String api, T entity) where T : class, new()
        {
            try
            {
                IRestClient client = new RestClient(ModuleHelper.Url + "/" + api.TrimStart('/'));
                IRestRequest request = new RestRequest();
                request.Method = Method.POST;
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", (new List<T>() { entity }).SerializeToString(), ParameterType.RequestBody);
                IRestResponse response = await client.ExecuteTaskAsync(request);
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch { return false; }
        }

        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="api"></param>
        /// <param name="entries"></param>
        /// <returns></returns>
        public static async Task<bool> Post<T>(String api, List<T> entries) where T : class, new()
        {
            try
            {
                IRestClient client = new RestClient(ModuleHelper.Url + "/" + api.TrimStart('/'));
                IRestRequest request = new RestRequest();
                request.Method = Method.POST;
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", entries.SerializeToString(), ParameterType.RequestBody);
                IRestResponse response = await client.ExecuteTaskAsync(request);
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch { return false; }
        }

        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="api"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<bool> Put<T>(String api, T entity) where T : class, new()
        {
            try
            {
                IRestClient client = new RestClient(ModuleHelper.Url + "/" + api.TrimStart('/'));
                IRestRequest request = new RestRequest();
                request.Method = Method.PUT;
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", (new List<T>() { entity }).SerializeToString(), ParameterType.RequestBody);
                IRestResponse response = await client.ExecuteTaskAsync(request);
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch { return false; }
        }

        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="api"></param>
        /// <param name="entries"></param>
        /// <returns></returns>
        public static async Task<bool> Put<T>(String api, List<T> entries) where T : class, new()
        {
            try
            {
                IRestClient client = new RestClient(ModuleHelper.Url + "/" + api.TrimStart('/'));
                IRestRequest request = new RestRequest();
                request.Method = Method.PUT;
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", entries.SerializeToString(), ParameterType.RequestBody);
                IRestResponse response = await client.ExecuteTaskAsync(request);
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch { return false; }
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="api"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<bool> Delete<T>(String api, T entity) where T : class, new()
        {
            try
            {
                IRestClient client = new RestClient(ModuleHelper.Url + "/" + api.TrimStart('/'));
                IRestRequest request = new RestRequest();
                request.Method = Method.DELETE;
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", (new List<T>() { entity }).SerializeToString(), ParameterType.RequestBody);
                IRestResponse response = await client.ExecuteTaskAsync(request);
                return response.StatusCode == System.Net.HttpStatusCode.NoContent;
            }
            catch { return false; }
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="api"></param>
        /// <param name="entries"></param>
        /// <returns></returns>
        public static async Task<bool> Delete<T>(String api, List<T> entries) where T : class, new()
        {
            try
            {
                IRestClient client = new RestClient(ModuleHelper.Url + "/" + api.TrimStart('/'));
                IRestRequest request = new RestRequest();
                request.Method = Method.DELETE;
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", entries.SerializeToString(), ParameterType.RequestBody);
                IRestResponse response = await client.ExecuteTaskAsync(request);
                return response.StatusCode == System.Net.HttpStatusCode.NoContent;
            }
            catch { return false; }
        }
        #endregion
    }
}

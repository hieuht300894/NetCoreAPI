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
                return lstResult ?? new List<T>();
            }
            catch { return new List<T>(); }
        }

        /// <summary>
        /// Tìm kiếm dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public static async Task<T> GetByID<T>(String api, object KeyID) where T : class, new()
        {
            try
            {
                IRestClient client = new RestClient(ModuleHelper.Url + $"/{(api.TrimStart('/'))}/{KeyID}");
                IRestRequest request = new RestRequest();
                request.Method = Method.GET;
                IRestResponse response = await client.ExecuteTaskAsync(request);
                T item = response.Content.DeserializeToObject<T>();
                return item ?? new T();
            }
            catch { return new T(); }
        }

        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <param name="api"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<Tuple<bool, TOut>> Post<TIn, TOut>(String api, TIn entity)
            where TIn : class, new()
            where TOut : class, new()
        {
            try
            {
                IRestClient client = new RestClient(ModuleHelper.Url + "/" + api.TrimStart('/'));
                IRestRequest request = new RestRequest();
                request.Method = Method.POST;
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", (new List<TIn>() { entity }).SerializeToString(), ParameterType.RequestBody);
                IRestResponse response = await client.ExecuteTaskAsync(request);

                bool Status = response.StatusCode == System.Net.HttpStatusCode.OK;
                TOut Item = response.Content.DeserializeToList<TOut>().FirstOrDefault() ?? new TOut();

                return Tuple.Create(Status, Item);
            }
            catch { return Tuple.Create(false, new TOut()); }
        }

        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="api"></param>
        /// <param name="entries"></param>
        /// <returns></returns>
        public static async Task<Tuple<bool, List<TOut>>> Post<TIn, TOut>(String api, List<TIn> entries)
            where TIn : class, new()
            where TOut : class, new()
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

                bool Status = response.StatusCode == System.Net.HttpStatusCode.OK;
                List<TOut> Items = response.Content.DeserializeToList<TOut>() ?? new List<TOut>();

                return Tuple.Create(Status, Items);
            }
            catch { return Tuple.Create(false, new List<TOut>()); }
        }

        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="api"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<Tuple<bool, TOut>> Put<TIn, TOut>(String api, TIn entity)
            where TIn : class, new()
            where TOut : class, new()
        {
            try
            {
                IRestClient client = new RestClient(ModuleHelper.Url + "/" + api.TrimStart('/'));
                IRestRequest request = new RestRequest();
                request.Method = Method.PUT;
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", (new List<TIn>() { entity }).SerializeToString(), ParameterType.RequestBody);
                IRestResponse response = await client.ExecuteTaskAsync(request);

                bool Status = response.StatusCode == System.Net.HttpStatusCode.OK;
                TOut Item = response.Content.DeserializeToList<TOut>().FirstOrDefault() ?? new TOut();

                return Tuple.Create(Status, Item);
            }
            catch { return Tuple.Create(false, new TOut()); }
        }

        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="api"></param>
        /// <param name="entries"></param>
        /// <returns></returns>
        public static async Task<Tuple<bool, List<TOut>>> Put<TIn, TOut>(String api, List<TIn> entries)
            where TIn : class, new()
            where TOut : class, new()
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
                bool Status = response.StatusCode == System.Net.HttpStatusCode.OK;
                List<TOut> Items = response.Content.DeserializeToList<TOut>() ?? new List<TOut>();

                return Tuple.Create(Status, Items);
            }
            catch { return Tuple.Create(false, new List<TOut>()); }
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

using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Empleados.Web.General
{
    public class ServiceHelper<T>
    {
        public async Task<T> GetRestServiceDataAsync(string serviceAddress)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(serviceAddress);
            var response = await client.GetAsync(client.BaseAddress);
            response.EnsureSuccessStatusCode();
            var jsonResult = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(jsonResult);
            return result;
        }

        public async Task<T> PostRestServiceDataAsync(string serviceAddress, object obj)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(serviceAddress);
            var objJson = JsonConvert.SerializeObject(obj);
            var contentJson = new StringContent(objJson, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(client.BaseAddress, contentJson);
            response.EnsureSuccessStatusCode();
            var jsonResult = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(jsonResult);
            return result;
        }

        public async Task<T> PutRestServiceDataAsync(string serviceAddress, object obj)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(serviceAddress);
            var objJson = JsonConvert.SerializeObject(obj);
            var contentJson = new StringContent(objJson, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(client.BaseAddress, contentJson);
            response.EnsureSuccessStatusCode();
            var jsonResult = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(jsonResult);
            return result;
        }

        //public async Task<T> PostRestServiceWithParams(string serviceAddress, string[] keys, string[] values)
        //{
        //    try
        //    {
        //        var client = new HttpClient();
        //        //var url = "http://myserver/method";
        //        var parameters = new Dictionary<string, string>();
        //        //var parameters = new Dictionary<string, string> { { "param1", "1" }, { "param2", "2" } };

        //        //parameters.var parameters = new Dictionary<string, string> { { "param1", "1" }, { "param2", "2" } };

        //        for (int i = 0; i < keys.Length; i++)
        //        {
        //            parameters.Add(keys[i], values[i]);
        //        }

        //        var encodedContent = new FormUrlEncodedContent(parameters);

        //        var response = await client.PostAsync(serviceAddress, encodedContent).ConfigureAwait(false);

        //        if (response.StatusCode == HttpStatusCode.OK)
        //        {
        //            // Do something with response. Example get content:
        //            // var responseContent = await response.Content.ReadAsStringAsync ().ConfigureAwait (false);
        //            var jsonResult = await response.Content.ReadAsStringAsync();
        //            return JsonConvert.DeserializeObject<T>(jsonResult);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return default(T);
        //}
    }
}

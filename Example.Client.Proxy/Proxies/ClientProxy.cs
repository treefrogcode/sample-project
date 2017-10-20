using Example.Client.Proxy.Interfaces;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Example.Client.Proxy.Proxies
{
    public class ClientProxy : IClientProxy
    {
        private string _baseURL;
        private string _token;

        public ClientProxy()
        {
            _baseURL = ConfigurationManager.AppSettings["ApiUri"];
            _token = ConfigurationManager.AppSettings["ApiToken"];
        }

        public async Task<T> Get<T>(string uri)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                ConfigureClient(httpClient);
                var response = await httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                return await Task.Run(() => GetResultFromResponse<T>(response));
            }
        }

        public async Task<string> GetAsJson(string uri)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                ConfigureClient(httpClient);
                var response = await httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<T> Post<T>(string uri, T body)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                ConfigureClient(httpClient);
                var response = await httpClient.PostAsync(uri, ConvertObjectToByteArray(body));
                response.EnsureSuccessStatusCode();
                return await Task.Run(() => GetResultFromResponse<T>(response));
            }
        }

        public async Task<T> Put<T>(string uri, T body)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                ConfigureClient(httpClient);
                var response = await httpClient.PutAsync(uri, ConvertObjectToByteArray(body));
                response.EnsureSuccessStatusCode();
                return await Task.Run(() => GetResultFromResponse<T>(response));
            }
        }

        public async Task<bool> Delete(string uri)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                ConfigureClient(httpClient);
                var response = await httpClient.DeleteAsync(uri);
                response.EnsureSuccessStatusCode();
                return true;
            }
        }

        private void ConfigureClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri(_baseURL);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("ApiToken", _token);
        }

        private ByteArrayContent ConvertObjectToByteArray(object content)
        {
            var jsonContent = JsonConvert.SerializeObject(content);
            return new StringContent(jsonContent, Encoding.UTF8, "application/json");
        }

        private async Task<T> GetResultFromResponse<T>(HttpResponseMessage response)
        {
            string content = await response.Content.ReadAsStringAsync();
            return await Task.Run(() => JsonConvert.DeserializeObject<T>(content));
        }
    }
}
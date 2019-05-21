using CinemasTheBESTia.Utilities.Abstractions.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CinemasTheBESTia.Utilities.Helpers
{
    public class ApiClient : IAPIClient
    {

        /// <summary>  
        /// Common method for making GET calls  
        /// </summary>  
        public async Task<T> GetAsync<T>(Uri requestUrl)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(data);
            }
        }

        /// <summary>  
        /// Common method for making GET calls  
        /// </summary>  
        public async Task<T> GetAsync<T>(Uri requestUrl, string tokenName)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                var token = JObject.Parse(data).SelectToken(tokenName);
                return JsonConvert.DeserializeObject<T>(token.ToString());
            }
        }
    }
}

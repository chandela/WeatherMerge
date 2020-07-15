using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherMerge.Services
{
    public static class ApiService
    {

        /// <summary>
        /// Get Unique NetworkService
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Uri"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public static async Task<T> GetService<T>(string Uri, object payload = null)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response;
                    client.BaseAddress = new Uri(Uri);
                    response = await client.GetAsync(Uri);
                    response.EnsureSuccessStatusCode();
                    var stringResult = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(stringResult);
                }
                catch (HttpRequestException httpRequestException)
                {
                    throw new Exception($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
            }
        }
    }
}

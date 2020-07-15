using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherMerge.Services
{
    public class WeatherService
    {

        public async Task<OpenWeatherResponse> GetWeatherData(string uri)
        {
            return await  ApiService.GetService<OpenWeatherResponse>(uri);
        }
    }

    public class OpenWeatherResponse
    {
        public string Name { get; set; }

        public IEnumerable<WeatherDescription> Weather { get; set; }

        public Main Main { get; set; }
    }

    public class WeatherDescription
    {
        public string Main { get; set; }
        public string Description { get; set; }
    }

    public class Main
    {
        public string Temp { get; set; }
    }
}

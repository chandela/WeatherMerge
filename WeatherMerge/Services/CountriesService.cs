using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherMerge.Services
{
    public class CountriesService
    {
        /// <summary>
        /// Het All Countries
        /// </summary>
        /// <param name="Uri"></param>
        /// <returns></returns>
        public async Task<List<CountriesData>> GetCountries(string Uri)
        {
            return await  ApiService.GetService<List<CountriesData>>(Uri);

        }

    }


    /// <summary>
    /// Serialize Countries
    /// </summary>
    public class CountriesData
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("region")]
        public string region { get; set; }

        [JsonProperty("subregion")]
        public string subregion { get; set; }

        [JsonProperty("population")]
        public string population { get; set; }

        [JsonProperty("alpha2Code")]
        public string alpha2Code { get; set; }

    }
}

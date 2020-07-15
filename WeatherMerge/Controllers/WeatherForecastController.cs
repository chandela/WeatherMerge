using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WeatherMerge.Models;
using WeatherMerge.Services;

namespace WeatherMerge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private static Dictionary<Guid, WeatherMerge.Models.City> Cities = new Dictionary<Guid, WeatherMerge.Models.City>();
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("Getweather/{city}")]
        public async Task<IActionResult> Getweather(string city)
        {
            try
            {
                //using hardcoded Url because No Api key provided
                string uri = $"https://samples.openweathermap.org/data/2.5/weather?q={city},uk&appid=439d4b804bc8187953eb36d2a8c26a02";

                var data = await new WeatherService().GetWeatherData(uri);
                return Ok(new
                {
                    Temp = data.Main.Temp,
                    Summary = string.Join(",", data.Weather.Select(x => x.Main)),
                    City = data.Name
                });
            }


            catch (Exception httpRequestException)
            {
                return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
            }
        }

        [HttpGet]
        [Route("SearchCity/{cityname}")]
        public async Task<IActionResult> SearchCity(string cityname)
        {
            try
            {
                //To get all cities matches search criteria
                var payload = Cities.Where(x => x.Value.CityName == cityname).OrderBy(y => y.Value.CityName);
                if (payload.Count() < 1)
                    return Ok("No data Found On your Request Add a City First");
                else return Ok(payload);

            }


            catch (Exception httpRequestException)
            {
                return BadRequest($"Error getting data from restcountries.eu: {httpRequestException.Message}");
            }
        }

        [HttpGet]
        [Route("Countries")]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                string Uri =  new Uri("https://restcountries.eu/rest/v2/all").AbsoluteUri;
                var data = await new CountriesService().GetCountries(Uri);
                return Ok(data);
            }


            catch (Exception httpRequestException)
            {
                return BadRequest($"Error getting data from restcountries.eu: {httpRequestException.Message}");
            }
        }

        [HttpPost]
        [Route("AddCity")]
        public async Task<IActionResult> AddCity([FromBody] List<City>payload)
        {
            try
            {
                 foreach (var data in payload)
                {
                    Cities.Add(data.CityId, data);
                }
                return  Ok();
            }


            catch (Exception httpRequestException)
            {
                return BadRequest($"Error in adding Country: {httpRequestException.Message}");
            }
        }

        [HttpPost]
        [Route("UpdateCity")]
        public async Task<IActionResult> UpdateCity([FromBody] List<City> payload)
        {
            try
            {
                foreach (var data in payload)
                {
                   Cities.Remove(Cities.Where(x => x.Value.CityName == data.CityName).FirstOrDefault().Key);
                   Cities.Add(data.CityId, data);
                }
                return Ok();
            }


            catch (Exception ex)
            {
                return BadRequest($"Error in updating Country: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("DeleteCity")]
        public async Task<IActionResult> DeleteCity([FromBody] List<City> payload)
        {
            try
            {
                bool result = false;
                foreach (var data in payload)
                {
                    result =  Cities.Remove(Cities.Where(x => x.Value.CityName == data.CityName).FirstOrDefault().Key);
                }
                //Need validation due to time limit Skipped
                return Ok(result ? "Successfully Deleted" :"Failed to Delete No City Found");
            }

            catch (HttpRequestException httpRequestException)
            {
                return BadRequest($"Error in deleting Country: {httpRequestException.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error in deleting Country: {ex.Message}");
            }
        }
    }
}


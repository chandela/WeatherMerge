using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using WeatherMerge.Controllers;

namespace WeatherMerge.UnitTest
{
    [TestClass]
    public class UnitTest1
    {

        static ILoggerFactory Lf = new LoggerFactory();
        static  ILogger<WeatherForecastController> logger = new Logger<WeatherForecastController>(Lf);

        /// <summary>
        /// With No payload test
        /// </summary>
        [TestMethod]
        public void SearchCityTest()
        {
            var Controller = new WeatherForecastController(logger);
            var obj = (ObjectResult)Controller.SearchCity("").Result;
            Assert.AreEqual("No data Found On your Request Add a City First", obj.Value);
        }
    }
}

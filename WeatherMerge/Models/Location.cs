using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherMerge.Models
{
    public class Location
    {
        
        /// <summary>
        /// CountryId
        /// </summary>
       public Guid CountryId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Countryname
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Citydetrails
        /// </summary>
        public List<City> Cities { get; set; }
        public long EstimatedPopulation { get; set; }

    }


    /// <summary>
    /// City Details
    /// </summary>
    public class City
    {

        private short _rating = 0;

        /// <summary>
        /// CityId
        /// </summary>
        public Guid CityId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// CityName
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// City Ratings
        /// </summary>
        public short Ratings
        {
            get
            {
                return _rating;
            }

            set
            {
                if (value > 5)
                    _rating = 5;

                else if (value < 0)
                    _rating = 0;
                else
                {
                    _rating = value;
                }
            }
        }

        /// <summary>
        /// Estabilished date
        /// </summary>
        public DateTime EstabilishedDate { get; set; }

        /// <summary>
        /// Estimated Population
        /// </summary>
        public long EstimatedPopulation { get; set; }
    }
}

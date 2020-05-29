using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class CountriesModel
    {
        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("Slug")]
        public string Slug { get; set; }

        [JsonProperty("ISO2")]
        public string Iso2 { get; set; }
    }
}

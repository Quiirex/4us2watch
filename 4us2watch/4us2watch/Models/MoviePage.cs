using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace _4us2watch.Models
{
    class MoviePage
    {
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("results")]
        public Movie[] Results { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace _4us2watch.Models
{
    public class Movie
    {
        [JsonProperty("id")]
        public int idMovie { get; set; }
        [JsonProperty("title")]
        public string Name { get; set; }
        [JsonProperty("poster_path")]
        public string ImagePath { get; set; }
        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }
        [JsonProperty("genre_ids")]
        public int[] Genre_Ids { get; set; }
        [JsonProperty("overview")]
        public string Overview { get; set; }
        [JsonProperty("popularity")]
        public double Popularity { get; set; }
        [JsonProperty("vote_average")]
        public double Vote_Average { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PlattSampleApp.Models.SwApi
{
    public class Resident
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("birth_year")]
        public string BirthYear { get; set; }
        [JsonProperty("eye_color")]
        public string EyeColor { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("hair_color")]
        public string HairColor { get; set; }
        [JsonProperty("height")]
        public string Height { get; set; }
        [JsonProperty("mass")]
        public string Mass { get; set; }
        [JsonProperty("skin_color")]
        public string SkinColor { get; set; }
        [JsonProperty("homeworld")]
        public string HomeWorld { get; set; }
        [JsonProperty("films")]
        public List<string> Films { get; set; }
        [JsonProperty("species")]
        public List<string> Species { get; set; }
        [JsonProperty("starships")]
        public List<string> Starships { get; set; }
        [JsonProperty("vehicles")]
        public List<string> Vehicles { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}

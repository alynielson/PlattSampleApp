using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlattSampleApp.Models.SwApi
{
    public class PagedResult<T>
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("previous")]
        public string Previous { get; set; }
        [JsonProperty("next")]
        public string Next { get; set; }
        [JsonProperty("results")]
        public List<T> Results { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PlattSampleApp.Models.SwApi
{
    public class Starship
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("model")]
        public string Model { get; set; }
        [JsonProperty("starship_class")]
        public string StarshipClass { get; set; }
        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }
        [JsonProperty("cost_in_credits")]
        public string CostInCredits { get; set; }
        [JsonProperty("length")]
        public string Length { get; set; }
        [JsonProperty("crew")]
        public string Crew { get; set; }
        [JsonProperty("passengers")]
        public string Passengers { get; set; }
        [JsonProperty("max_atmosphering_speed")]
        public string MaxAtmospheringSpeed { get; set; }
        [JsonProperty("hyperdrive_rating")]
        public string HyperdriveRating { get; set; }
        [JsonProperty("MGLT")]
        public string MGLT { get; set; }
        [JsonProperty("cargo_capacity")]
        public string CargoCapacity { get; set; }
        [JsonProperty("consumables")]
        public string Consumables { get; set; }
        [JsonProperty("films")]
        public List<string> Films { get; set; }
        [JsonProperty("pilots")]
        public List<string> Pilots { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}

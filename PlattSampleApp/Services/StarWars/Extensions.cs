using PlattSampleApp.Models.SwApi;
using PlattSampleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlattSampleApp.Services.StarWars
{
    public static class Extensions
    {
        public static ResidentSummary ConvertToResidentSummary(this Resident resident)
        {
            return new ResidentSummary
            {
                Name = resident.Name,
                EyeColor = resident.EyeColor,
                Gender = resident.Gender,
                HairColor = resident.HairColor,
                Height = resident.Height,
                SkinColor = resident.SkinColor,
                Weight = resident.Mass
            };
        }

        public static SinglePlanetViewModel ConvertToPlanetViewModel(this Planet planet)
        {
            return new SinglePlanetViewModel
            {
                Climate = planet.Climate,
                Diameter = planet.Diameter,
                Gravity = planet.Gravity,
                LengthOfDay = planet.RotationPeriod,
                LengthOfYear = planet.OrbitalPeriod,
                Name = planet.Name,
                Population = planet.Population,
                SurfaceWaterPercentage = planet.SurfaceWater
            };
        }

        public static PlanetDetailsViewModel ConvertToPlanetDetailsViewModel(this Planet planet)
        {
            return new PlanetDetailsViewModel
            {
                Diameter = planet.Diameter,
                LengthOfYear = planet.OrbitalPeriod,
                Name = planet.Name,
                Population = planet.Population,
                Terrain = planet.Terrain
            };
        }
    }
}

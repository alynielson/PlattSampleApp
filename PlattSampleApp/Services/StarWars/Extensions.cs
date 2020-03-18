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

        public static VehicleStatsViewModel ConvertToVehicleStatsViewModel(this IGrouping<string, Vehicle> vehiclesPerManufacturer)
        {
            return new VehicleStatsViewModel
            {
                AverageCost = vehiclesPerManufacturer.Average(x => double.Parse(x.CostInCredits)),
                ManufacturerName = vehiclesPerManufacturer.Key,
                VehicleCount = vehiclesPerManufacturer.ToList().Count
            };
        }

        public static bool CanEvacuateToNextPlanet(this Starship ship, List<Resident> evacuees, double yearsToNextPlanet)
        {
            if (ship.Consumables is null)
                return false;

            var consumableTime = ship.Consumables.Substring(0, ship.Consumables.IndexOf(' '));
            if (!int.TryParse(consumableTime, out var consumableTimeAvailable))
                return false;
            if (yearsToNextPlanet > consumableTimeAvailable)
                return false;

            if (ship.Crew is null || !int.TryParse(ship.Crew, out var shipCrewRequired))
                return false;
            if (evacuees.Count < shipCrewRequired) // not enough people to crew
                return false;

            if (ship.Passengers is null || !int.TryParse(ship.Passengers, out var shipPassengers))
                return false;
            if (evacuees.Count - shipCrewRequired > shipPassengers) // too many people
                return false;

            if (ship.CargoCapacity is null || !int.TryParse(ship.CargoCapacity, out var cargoCapacityKg))
                return false;
            if (evacuees.Where(x => x.Mass != null && int.TryParse(x.Mass, out var _)).Select(x => int.Parse(x.Mass)).Sum() > cargoCapacityKg)
                return false;

            return true;
        }
    }
}

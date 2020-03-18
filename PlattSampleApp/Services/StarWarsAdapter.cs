using PlattSampleApp.ApiServices;
using PlattSampleApp.Models.SwApi;
using PlattSampleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlattSampleApp.Adapters
{
    public class StarWarsAdapter : IStarWarsAdapter
    {
        private readonly ISwApiService _swApiService;

        public StarWarsAdapter(ISwApiService swApiService)
        {
            _swApiService = swApiService;
        }

        public async Task<AllPlanetsViewModel> GetAllPlanetsViewModel()
        {
            var planets = await _swApiService.GetAllPlanets();
            if (planets is null)
            {
                return null;
            }

            var withDiameter = new List<(double diameter, PlanetDetailsViewModel planet)>();
            var noDiameter = new List<PlanetDetailsViewModel>();
            planets.ForEach(x =>
            {
                if (x.Diameter != null && double.TryParse(x.Diameter, out var diameter))
                {
                    withDiameter.Add((diameter, ConvertToPlanetDetailsViewModel(x)));
                }
                else
                {
                    noDiameter.Add(ConvertToPlanetDetailsViewModel(x));
                }
            });

            var planetsFormatted = withDiameter.OrderByDescending(x => x.diameter).Select(x => x.planet).ToList();
            planetsFormatted.AddRange(noDiameter);

            return new AllPlanetsViewModel
            {
                AverageDiameter = withDiameter.Average(x => x.diameter),
                Planets = planetsFormatted
            }; 
        }

        public async Task<PlanetResidentsViewModel> GetPlanetResidentsViewModel(string planetName)
        {
            var searchResult = await _swApiService.SearchPlanetsByName(planetName);
            var match = searchResult?.Results?
                .FirstOrDefault(x => x.Name?.Equals(planetName, StringComparison.OrdinalIgnoreCase) == true 
                && x.Residents != null && x.Residents.Count > 0);
            if (match is null)
            {
                return null;
            }
            var residents = await Task.WhenAll(match.Residents.Select(async x => await _swApiService.GetResidentByEndpoint(x)));
            return new PlanetResidentsViewModel
            {
                Residents = residents?.OrderBy(x => x.Name).Select(x => ConvertToResidentSummary(x)).ToList()
            };
        }

        public async Task<SinglePlanetViewModel> GetSinglePlanetViewModel(int planetId)
        {
            var planet = await _swApiService.GetPlanet(planetId);
            if (planet is null)
            {
                return null;
            }
            return ConvertToPlanetViewModel(planet);
        }

        private ResidentSummary ConvertToResidentSummary(Resident resident)
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

        private SinglePlanetViewModel ConvertToPlanetViewModel(Planet planet)
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

        private PlanetDetailsViewModel ConvertToPlanetDetailsViewModel(Planet planet)
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

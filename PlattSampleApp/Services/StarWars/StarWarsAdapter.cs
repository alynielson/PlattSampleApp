using PlattSampleApp.ApiServices.StarWars;
using PlattSampleApp.Models.SwApi;
using PlattSampleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlattSampleApp.Adapters.StarWars
{
    public class StarWarsAdapter : IStarWarsAdapter
    {
        private readonly ISwPlanetApiService _planetService;
        private readonly ISwPersonApiService _personService;
        private readonly ISwVehicleApiService _vehicleService;

        public StarWarsAdapter(ISwPlanetApiService planetService, ISwPersonApiService personService, ISwVehicleApiService vehicleService)
        {
            _planetService = planetService;
            _personService = personService;
            _vehicleService = vehicleService;
        }

        public async Task<AllPlanetsViewModel> GetAllPlanetsViewModel()
        {
            var planets = await _planetService.GetAllPlanets();
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
                    withDiameter.Add((diameter, x.ConvertToPlanetDetailsViewModel()));
                }
                else
                {
                    noDiameter.Add(x.ConvertToPlanetDetailsViewModel());
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
            var searchResult = await _planetService.SearchPlanetsByName(planetName);
            var match = searchResult?.Results?
                .FirstOrDefault(x => x.Name?.Equals(planetName, StringComparison.OrdinalIgnoreCase) == true 
                && x.Residents != null && x.Residents.Count > 0);
            if (match is null)
            {
                return null;
            }
            var residents = await Task.WhenAll(match.Residents.Select(async x => await _personService.GetResidentByEndpoint(x)));
            return new PlanetResidentsViewModel
            {
                Residents = residents?.OrderBy(x => x.Name).Select(x => x.ConvertToResidentSummary()).ToList()
            };
        }

        public async Task<SinglePlanetViewModel> GetSinglePlanetViewModel(int planetId)
        {
            var planet = await _planetService.GetPlanet(planetId);
            if (planet is null)
            {
                return null;
            }
            return planet.ConvertToPlanetViewModel();
        }

        public async Task<VehicleSummaryViewModel> GetVehicleSummaryViewModel()
        {
            var allVehicles = await _vehicleService.GetAllVehicles();
            if (allVehicles is null || allVehicles.Count == 0)
            {
                return null;
            }
            var knownCostVehicles = allVehicles.Where(x => x.CostInCredits != null && double.TryParse(x.CostInCredits, out var _)).ToList();
            var knownCostByMfgr = knownCostVehicles.GroupBy(x => x.Manufacturer);

            return new VehicleSummaryViewModel
            {
                Details = knownCostByMfgr.Select(x => ConvertToVehicleStatsViewModel(x))
                    .OrderByDescending(x => x.VehicleCount).ThenByDescending(x => x.AverageCost).ToList(),
                ManufacturerCount = knownCostByMfgr.ToList().Count,
                VehicleCount = knownCostVehicles.Count
            };
        }

        private VehicleStatsViewModel ConvertToVehicleStatsViewModel(IGrouping<string, Vehicle> vehiclesPerManufacturer)
        {
            return new VehicleStatsViewModel
            {
                AverageCost = vehiclesPerManufacturer.Average(x => double.Parse(x.CostInCredits)),
                ManufacturerName = vehiclesPerManufacturer.Key,
                VehicleCount = vehiclesPerManufacturer.ToList().Count
            };
        }

    }
}

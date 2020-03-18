using PlattSampleApp.ApiServices;
using PlattSampleApp.Models.SwApi;
using PlattSampleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlattSampleApp.Services
{
    public class StarWarsService : IStarWarsService
    {
        private readonly ISwApiService _swApiService;

        public StarWarsService(ISwApiService swApiService)
        {
            _swApiService = swApiService;
        }

        public async Task<AllPlanetsViewModel> GetAllPlanetsViewModel()
        {
            var planets = await _swApiService.GetAllPlanets();
            if (planets is null)
            {
                return new AllPlanetsViewModel();
            }

            var withDiameter = new List<(double diameter, PlanetDetailsViewModel planet)>();
            var noDiameter = new List<PlanetDetailsViewModel>();
            planets.ForEach(x =>
            {
                if (x.Diameter != null && double.TryParse(x.Diameter, out var diameter))
                {
                    withDiameter.Add((diameter, ConvertToViewModel(x)));
                }
                else
                {
                    noDiameter.Add(ConvertToViewModel(x));
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

        private PlanetDetailsViewModel ConvertToViewModel(Planet planet)
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

using PlattSampleApp.Models.SwApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlattSampleApp.ApiServices
{
    public interface ISwApiService
    {
        Task<List<Planet>> GetAllPlanets();

        Task<Planet> GetPlanet(int planetId);

        Task<PagedResult<Planet>> SearchPlanetsByName(string name);

        Task<List<Vehicle>> GetAllVehicles();

        Task<Resident> GetResidentByEndpoint(string endpoint);
    }
}

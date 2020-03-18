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
        Task<PagedResult<Planet>> GetPlanetsPage(HttpClient httpClient, string nextEndpoint);

        Task<List<Planet>> GetAllPlanets();

        Task<Planet> GetPlanet(int planetId);

        Task<PagedResult<Planet>> SearchPlanetsByName(string name);

        Task<Resident> GetResidentByEndpoint(string endpoint);

        Task<List<Vehicle>> GetAllVehicles();

        Task<PagedResult<Vehicle>> GetVehiclesPage(HttpClient httpClient, string nextPageEndpoint);
    }
}

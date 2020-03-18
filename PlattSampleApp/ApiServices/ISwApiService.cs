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
        Task<PagedResult<Planet>> GetPlanets(HttpClient httpClient, string nextEndpoint);

        Task<List<Planet>> GetAllPlanets();

        Task<Planet> GetPlanet(int planetId);
    }
}

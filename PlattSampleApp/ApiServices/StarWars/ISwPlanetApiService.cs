using PlattSampleApp.Models.SwApi;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlattSampleApp.ApiServices.StarWars
{
    public interface ISwPlanetApiService
    {
        Task<List<Planet>> GetAllPlanets();

        Task<Planet> GetPlanet(int planetId);

        Task<PagedResult<Planet>> SearchPlanetsByName(string name);
        
    }
}

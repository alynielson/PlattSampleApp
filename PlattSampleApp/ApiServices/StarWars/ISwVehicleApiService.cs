using PlattSampleApp.Models.SwApi;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlattSampleApp.ApiServices.StarWars
{
    public interface ISwVehicleApiService
    {
        Task<List<Vehicle>> GetAllVehicles();
    }
}

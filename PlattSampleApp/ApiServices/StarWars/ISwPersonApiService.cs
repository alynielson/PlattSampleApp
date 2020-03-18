using PlattSampleApp.Models.SwApi;
using System.Threading.Tasks;

namespace PlattSampleApp.ApiServices.StarWars
{
    public interface ISwPersonApiService
    {
        Task<Resident> GetResidentByEndpoint(string endpoint);
    }
}

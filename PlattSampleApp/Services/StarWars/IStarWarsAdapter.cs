using PlattSampleApp.ViewModels;
using System.Threading.Tasks;

namespace PlattSampleApp.Adapters.StarWars
{
    public interface IStarWarsAdapter
    {
        Task<AllPlanetsViewModel> GetAllPlanetsViewModel();

        Task<SinglePlanetViewModel> GetSinglePlanetViewModel(int planetId);

        Task<PlanetResidentsViewModel> GetPlanetResidentsViewModel(string planetName);

        Task<VehicleSummaryViewModel> GetVehicleSummaryViewModel();
    }
}

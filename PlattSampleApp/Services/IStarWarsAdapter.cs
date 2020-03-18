using PlattSampleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlattSampleApp.Adapters
{
    public interface IStarWarsAdapter
    {
        Task<AllPlanetsViewModel> GetAllPlanetsViewModel();

        Task<SinglePlanetViewModel> GetSinglePlanetViewModel(int planetId);
    }
}

using PlattSampleApp.Models.SwApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlattSampleApp.ApiServices.StarWars
{
    public interface ISwStarshipService
    {
        Task<Starship> GetStarshipByEndpoint(string endpoint);
    }
}

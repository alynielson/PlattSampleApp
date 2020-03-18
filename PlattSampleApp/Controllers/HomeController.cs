using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlattSampleApp.Services;
using PlattSampleApp.ViewModels;

namespace PlattSampleApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStarWarsService _starWarsService;

        public HomeController(IStarWarsService starWarsService)
        {
            _starWarsService = starWarsService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllPlanets()
        {
            var model = await _starWarsService.GetAllPlanetsViewModel();
            return View(model);
        }

        public IActionResult GetPlanetTwentyTwo(int planetid)
        {
            var model = new SinglePlanetViewModel();

            // TODO: Implement this controller action

            return View(model);
        }

        public IActionResult GetResidentsOfPlanetNaboo(string planetname)
        {
            var model = new PlanetResidentsViewModel();

            // TODO: Implement this controller action

            return View(model);
        }

        public IActionResult VehicleSummary()
        {
            var model = new VehicleSummaryViewModel();

            // TODO: Implement this controller action

            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

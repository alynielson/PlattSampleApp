﻿using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlattSampleApp.Adapters.StarWars;
using PlattSampleApp.Models;
using PlattSampleApp.ViewModels;

namespace PlattSampleApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStarWarsAdapter _starWarsService;

        public HomeController(IStarWarsAdapter starWarsService)
        {
            _starWarsService = starWarsService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllPlanets()
        {
            try
            {
                var model = await _starWarsService.GetAllPlanetsViewModel();
                return View(model);
            }
            catch (RESTException ex)
            {
                return Error(ex.Message, ex.StatusCode.ToString());
            }
        }

        public async Task<IActionResult> GetResidentsOfPlanetNaboo(string planetname)
        {
            try
            {
                var model = await _starWarsService.GetPlanetResidentsViewModel(planetname);
                return View(model);
            }
            catch (RESTException ex)
            {
                return Error(ex.Message, ex.StatusCode.ToString());
            }
        }

        public async Task<IActionResult> GetPlanetTwentyTwo(int planetid)
        {
            try
            {
                var model = await _starWarsService.GetSinglePlanetViewModel(planetid);
                return View(model);
            }
            catch (RESTException ex)
            {
                return Error(ex.Message, ex.StatusCode.ToString());
            }
        }

        public async Task<IActionResult> VehicleSummary()
        {
            try
            {
                var model = await _starWarsService.GetVehicleSummaryViewModel();
                return View(model);
            }
            catch (RESTException ex)
            {
                return Error(ex.Message, ex.StatusCode.ToString());
            }
        }

        /// <summary>
        /// Checks if an entire planet's population could hypothetically evacuate the planet on a starship and make it to the closest planet.
        /// </summary>
        /// <param name="planetId"></param>
        /// <param name="yearsToNextPlanet"></param>
        /// <returns></returns>
        public async Task<IActionResult> IsPlanetEvacuable(int planetId, double yearsToNextPlanet)
        {
            try
            {
                return View(await _starWarsService.IsPlanetEvacuable(planetId, yearsToNextPlanet));
            }
            catch (RESTException ex)
            {
                return Error(ex.Message, ex.StatusCode.ToString());
            }
        }

        public IActionResult Error(string reason, string code)
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                StatusCode = code,
                Reason = reason
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlattSampleApp.ViewModels;

namespace PlattSampleApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllPlanets()
        {
            var model = new AllPlanetsViewModel();

            // TODO: Implement this controller action

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

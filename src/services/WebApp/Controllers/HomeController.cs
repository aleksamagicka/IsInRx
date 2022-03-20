using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using RxApiClient;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRxClient _rxApiClient;

        public HomeController(IRxClient rxApiClient)
        {
            _rxApiClient = rxApiClient;
        }

        public async Task<IActionResult> Index()
        {
            return await Index(new DateViewModel { Date = DateTime.Now });
        }

        [HttpPost]
        public async Task<IActionResult> Index(DateViewModel dateModel)
        {
            var planetPeriods = await _rxApiClient.GetRxPlanetsAsync(dateModel.Date);

            dateModel.PlanetPeriods = planetPeriods.ToList();

            return View("Index", dateModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
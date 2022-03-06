using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using RxApiClient;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherForecastClient _weatherForecast;

        public HomeController(ILogger<HomeController> logger, IWeatherForecastClient weatherForecast)
        {
            _logger = logger;
            _weatherForecast = weatherForecast;
        }

        public async Task<IActionResult> Index()
        {
            var novo = await _weatherForecast.GetAsync();
            var st = "";
            foreach (var x in novo)
            {
                st += x.ToJson() + "<br><br><br>";
            }

            ViewBag.JSON = st;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
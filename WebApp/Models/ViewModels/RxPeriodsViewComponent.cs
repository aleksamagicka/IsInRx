using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models.ViewModels
{
    public class RxPeriodsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<RxApiClient.PlanetPeriodsDTO> planetPeriods)
        {
            return View(planetPeriods);
        }
    }
}

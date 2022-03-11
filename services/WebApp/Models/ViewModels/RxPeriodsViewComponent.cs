using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models.ViewModels
{
    public class RxPeriodsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(DateViewModel dateViewModel)
        {
            return View(dateViewModel);
        }
    }
}

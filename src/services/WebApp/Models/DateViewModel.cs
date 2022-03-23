using RxApiClient;

namespace WebApp.Models;

public class DateViewModel
{
    public DateTime Date { get; set; }

    public List<PlanetPeriodsDTO> PlanetPeriods { get; set; }
}
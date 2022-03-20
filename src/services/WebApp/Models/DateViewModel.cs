namespace WebApp.Models
{
    public class DateViewModel
    {
        public DateTime Date { get; set; }

        public List<RxApiClient.PlanetPeriodsDTO> PlanetPeriods { get; set; }
    }
}

namespace RxApi.Models
{
    public class PlanetPeriodsDTO
    {
        public string PlanetName { get; set; }
        public RetrogradePeriod? Current { get; set; }
        public RetrogradePeriod? Previous { get; set; }
        public RetrogradePeriod? After { get; set; }
    }
}

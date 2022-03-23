namespace RxApi.Models;

public class PlanetPosition
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Longitude { get; set; }
    public decimal Latitude { get; set; }
    public DateTime Time { get; set; }
    public decimal Degrees { get; set; }
    public decimal Minutes { get; set; }
    public decimal Seconds { get; set; }
    public int Sign { get; set; }
    public bool Retrograde { get; set; }
}
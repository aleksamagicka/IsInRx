using RxApiClient;

// ReSharper disable once CheckNamespace
namespace WebApp;

public static class Utilities
{
    public static string[] ZodiacSymbols = { "♈", "♉", "♊", "♋", "♌", "♍", "♎", "♏", "♐", "♑", "♒", "♓" };

    public static string PlanetGlyph(string planet)
    {
        return planet.ToLower() switch
        {
            "mercury" => "☿",
            "venus" => "♀",
            "mars" => "♂",
            "jupiter" => "♃",
            "saturn" => "♄",
            "uranus" => "♅",
            "neptune" => "♆",
            "pluto" => "♇",
            _ => ""
        };
    }

    public static string PrettyLongitude(PlanetPosition position)
    {
        return
            $"{Math.Truncate(position.Degrees % 30)}° {ZodiacSymbols[position.Sign - 1]} {Math.Truncate(position.Minutes)}\' {Math.Truncate(position.Seconds)}\"";
    }
}
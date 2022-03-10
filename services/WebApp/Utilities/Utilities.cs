using RxApiClient;

// ReSharper disable once CheckNamespace
namespace WebApp
{
    public static class Utilities
    {
        public static string[] ZodiacSymbols = { "♈", "♉", "♊", "♋", "♌", "♍", "♎", "♏", "♐", "♑", "♒", "♓" };

        public static string PlanetGlyph(string planet)
        {
            return planet switch
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
            return $"{position.Degrees % 30}° {ZodiacSymbols[position.Sign]} {position.Minutes}\" {Math.Truncate(position.Seconds)}\'";
        }
    }
}

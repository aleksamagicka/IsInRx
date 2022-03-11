using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RxDBParser.Data;

namespace RxDBParser;

internal class Program
{
    private static async Task Main(string[] args)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var context = new AstroContext();
        await context.Database.ExecuteSqlRawAsync(
            "delete from planet_positions7 where to_char(\"Time\", 'HH24:MI:SS') LIKE '00:00:00'"); // TODO: Currently hardcoded

        string[] planets = { "mercury", "venus", "mars", "jupiter", "saturn", "uranus", "neptune", "pluto" };

        foreach (var planet in planets)
        {
            await FindRxPeriods(context, planet);
            Console.WriteLine($"{planet} done...");
        }

        Console.WriteLine($"{Environment.NewLine}Exiting...");
    }

    private static async Task FindRxPeriods(AstroContext context, string name)
    {
        var startingTime = DateTime.Parse("2010-01-01 00:00:00"); // TODO: Currently hardcoded

        while (true)
        {
            var rxStart = await context.PlanetPositions.OrderBy(p => p.Time)
                .FirstOrDefaultAsync(p => p.Retrograde == true && p.Time > startingTime && p.Name == name);

            if (rxStart != null)
            {
                var rxEnd = await context.PlanetPositions.OrderBy(p => p.Time)
                    .FirstOrDefaultAsync(p => p.Retrograde == false && p.Time > rxStart.Time && p.Name == name);

                if (rxEnd != null)
                {
                    if ((rxEnd.Time - rxStart.Time).Days > 18) // && 
                    {
                        var periodExists = await context.RetrogradePeriods.AnyAsync(period =>
                            period.Name == name && period.StartPosition.Time == rxStart.Time &&
                            period.EndPosition.Time == rxEnd.Time);

                        if (!periodExists)
                        {
                            var rxPeriod = new RetrogradePeriod
                            {
                                Name = name,
                                StartPosition = rxStart,
                                EndPosition = rxEnd
                            };

                            context.Add(rxPeriod);
                            await context.SaveChangesAsync();
                        }
                    }

                    startingTime = rxEnd.Time;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
    }
}
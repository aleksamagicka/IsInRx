using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RxDBParser.Data;

namespace RxDBParser
{
    class Program
    {
        static async Task Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var context = new AstroContext();
            await context.Database.ExecuteSqlRawAsync("delete from planet_positions7 where to_char(\"Time\", 'HH24:MI:SS') LIKE '00:00:00'");

            string[] planets = { "mercury", "venus", "mars", "jupiter", "saturn", "uranus", "neptune", "pluto" };

            foreach (var planet in planets)
            {
                await FindRxPeriods(context, planet);
                Console.WriteLine($"{planet} done...");
            }
                
        }

        private static async Task FindRxPeriods(AstroContext context, string name)
        {
            var startingTime = DateTime.Parse("2010-01-01 00:00:00");

            

            while (true)
            {
                var pocetakRx = await context.PlanetPositions.OrderBy(p => p.Time)
                    .FirstOrDefaultAsync(p => p.Retrograde == true && p.Time > startingTime && p.Name == name);

                //Console.WriteLine($"moguci pocetni: {pocetakRx.Time}");

                if (pocetakRx != null)
                {
                    var krajRx = await context.PlanetPositions.OrderBy(p => p.Time)
                        .FirstOrDefaultAsync(p => p.Retrograde == false && p.Time > pocetakRx.Time && p.Name == name);

                    if (krajRx != null)
                    {
                        // pocetakRx.Time != krajRx.Time - TimeSpan.FromMinutes(1)
                        if ((krajRx.Time - pocetakRx.Time).Days > 18) // && 
                        {
                            //Ispisi($"Poceo RX: {pocetakRx.Time}, {(krajRx.Time - pocetakRx.Time).Days}");
                            //Ispisi($"Kraj RX: {krajRx.Time}");
                            //Ispisi(
                            //    $"{Environment.NewLine}--------------------------------------------{Environment.NewLine}");

                            var periodExists = await context.RetrogradePeriods.AnyAsync(period =>
                                period.Name == name && period.StartPosition.Time == pocetakRx.Time &&
                                period.EndPosition.Time == krajRx.Time);

                            if (!periodExists)
                            {
                                var rxPeriod = new RetrogradePeriod()
                                {
                                    Name = name,
                                    StartPosition = pocetakRx,
                                    EndPosition = krajRx
                                };

                                context.Add(rxPeriod);
                                await context.SaveChangesAsync();
                            }
                        }

                        startingTime = krajRx.Time;
                    }
                    else
                    {
                        //Ispisi("Nema vise krajnjeg RX-a!");
                        break;
                    }
                }
                else
                {
                    //Ispisi("Nema vise pocetnog RX-a!");
                    break;
                }
            }
        }
    }
}

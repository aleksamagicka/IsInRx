using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RxApi.Models;
using static System.Char;

namespace RxApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RxController : ControllerBase
    {
        private readonly AstroContext _context;

        public RxController(AstroContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<List<PlanetPeriodsDTO>>> GetRxPlanets(DateTime time)
        {
            string[] planets = { "mercury", "venus", "mars", "jupiter", "saturn", "uranus", "neptune", "pluto" };

            var planetDTOs = new List<PlanetPeriodsDTO>();

            foreach (var planet in planets)
            {
                var periodsDTO = new PlanetPeriodsDTO()
                {
                    PlanetName = ToUpper(planet[0]) + planet.Substring(1)
                };

                 // Try to find current retrograde period
                 var currentRxPeriod = await _context.RetrogradePeriods.SingleOrDefaultAsync(period =>
                    period.Name == planet && period.StartPosition.Time <= time && time <= period.EndPosition.Time);

                if (currentRxPeriod != null)
                {
                    // The planet is retrograde at this point in time
                    periodsDTO.Current = currentRxPeriod;

                    // Try to find the time when it exits its shadow (when it surpasses the original position where it started going backwards)
                    var nextShadowSurpass = await _context.PlanetPositions
                        .Where(position => position.Name == planet && position.Longitude > currentRxPeriod.StartPosition.Longitude)
                        .OrderBy(position => position.Longitude).FirstOrDefaultAsync();

                    if (nextShadowSurpass != null)
                    {
                        periodsDTO.ExitsShadow = nextShadowSurpass;
                    }
                }

                var nextTime = currentRxPeriod?.EndPosition.Time ?? time;

                // Try to find the next retrograde period
                var nextRxPeriod = await _context.RetrogradePeriods
                    .Where(period => period.Name == planet && period.StartPosition.Time > nextTime)
                    .OrderBy(period => period.StartPosition.Time).FirstOrDefaultAsync();

                if (nextRxPeriod != null)
                {
                    periodsDTO.After = nextRxPeriod;
                }

                // Try to find the previous retrograde period
                var pastRxPeriod = await _context.RetrogradePeriods
                    .Where(period => period.Name == planet && period.EndPosition.Time < time)
                    .OrderByDescending(period => period.EndPosition.Time).FirstOrDefaultAsync();

                if (pastRxPeriod != null)
                {
                    periodsDTO.Previous = pastRxPeriod;
                }

                // Done, add to list
                planetDTOs.Add(periodsDTO);
            }

            return Ok(planetDTOs);
        }
    }
}

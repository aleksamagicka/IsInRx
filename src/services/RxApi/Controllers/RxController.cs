using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RxApi.Models;

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
                    PlanetName = planet
                };

                 // Try to find current retrograde period
                 var currentRxPeriod = await _context.RetrogradePeriods.SingleOrDefaultAsync(period =>
                    period.Name == planet && period.StartPosition.Time <= time && time <= period.EndPosition.Time);

                if (currentRxPeriod != null)
                {
                    // The planet is retrograde at this point in time
                    periodsDTO.Current = currentRxPeriod;
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

                planetDTOs.Add(periodsDTO);
            }

            return Ok(planetDTOs);
        }
    }
}

using System;
using System.Collections.Generic;

namespace RxDBParser.Data
{
    public partial class PlanetPosition
    {
        public PlanetPosition()
        {
            RetrogradePeriodEndPositions = new HashSet<RetrogradePeriod>();
            RetrogradePeriodStartPositions = new HashSet<RetrogradePeriod>();
        }

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

        public virtual ICollection<RetrogradePeriod> RetrogradePeriodEndPositions { get; set; }
        public virtual ICollection<RetrogradePeriod> RetrogradePeriodStartPositions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using RxApi.Models;

namespace RxApi
{
    public partial class RetrogradePeriod
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int StartPositionId { get; set; }
        public int EndPositionId { get; set; }

        public virtual PlanetPosition EndPosition { get; set; } = null!;
        public virtual PlanetPosition StartPosition { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Codecool.DungeonCrawl.Logic.Actors
{
    public class DragonFire : Actor
    {
        public DragonFire(Cell cell)
            : base(cell)
        {
        }

        /// <inheritdoc/>
        public override string Tilename => "dragonfire";
    }
}

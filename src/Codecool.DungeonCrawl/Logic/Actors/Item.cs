using System;
using System.Collections.Generic;
using System.Text;

namespace Codecool.DungeonCrawl.Logic.Actors
{
    /// <summary>
    /// The game item
    /// </summary>
    public class Item : Actor
    {
        /// <inheritdoc/>
        public override string Tilename => "item";

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="cell">The starting cell</param>
        public Item(Cell cell)
            : base(cell)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Codecool.DungeonCrawl.Logic.Actors
{
    /// <summary>
    /// Enemy unit
    /// </summary>
    public class Ghost : Actor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ghost"/> class.
        /// </summary>
        /// <param name="cell">The starting cell</param>
        public Ghost(Cell cell)
            : base(cell)
        {
            Health = 20;
        }

        /// <inheritdoc/>
        public override string Tilename => "ghost";

        /// <summary>
        /// Moves ghost by a given amount
        /// </summary>
        /// <param name="dx">X direction</param>
        /// <param name="dy">Y direction</param>
        public override void Move(int dx, int dy)
        {
            try
            {
                Cell nextCell = Cell.GetNeighbor(dx, dy);

                if (nextCell.Tilename != "Empty")
                {
                    Cell.Actor = null;
                    nextCell.Actor = this;
                    Cell = nextCell;
                }
            }
            catch
            {
                Console.WriteLine("Ghost position outside of the map");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Codecool.DungeonCrawl.Logic.Actors
{
    /// <summary>
    /// Boss enemy
    /// </summary>
    public class Dragon : Actor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Dragon"/> class.
        /// </summary>
        /// <param name="cell">The starting cell</param>
        public Dragon(Cell cell)
            : base(cell)
        {
            Health = 50;
        }

        /// <inheritdoc/>
        public override string Tilename => "dragon";

        public bool DragonBreath(Player player)
        {
            int rangeX = Cell.X;
            int rangeY = Cell.Y;
            int[] rangeListX = { rangeX, rangeX, rangeX - 1, rangeX + 1 };
            int[] rangeListY = { rangeY - 1, rangeY - 2, rangeY - 2, rangeY - 2 };

            for (int index = 0; index < rangeListX.Length; index++)
            {
                if (player.X == rangeListX[index] && player.Y == rangeListY[index])
                {
                    player.Health -= 10;
                    Console.WriteLine("Dragon breaths into player and deals 10 damage");
                    return true;
                }
            }

            return false;
        }
    }
}

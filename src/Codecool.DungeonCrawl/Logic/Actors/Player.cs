using System.Threading;
using Codecool.DungeonCrawl.Logic.Items.Inventory;

namespace Codecool.DungeonCrawl.Logic.Actors
{
    /// <summary>
    /// The game player
    /// </summary>
    public class Player : Actor
    {
        /// <summary>
        /// Gets inventory
        /// </summary>
        public Inventory Inventory { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="cell">The starting cell</param>
        public Player(Cell cell)
            : base(cell)
        {
            Health = 100;
            Inventory = new Inventory();
        }

        /// <inheritdoc/>
        public override string Tilename => "player";
    }
}
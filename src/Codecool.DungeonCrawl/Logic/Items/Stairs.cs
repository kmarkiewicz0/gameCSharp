using System.Threading;

namespace Codecool.DungeonCrawl.Logic.Items
{
    /// <summary>
    /// Sample enemy
    /// </summary>
    public class Stairs : Item
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Stairs"/> class.
        /// </summary>
        /// <param name="cell">The starting cell</param>
        public Stairs(Cell cell)
            : base(cell)
        {
        }

        /// <inheritdoc/>
        public override string Tilename => "stairs";
    }
}
using System.Threading;

namespace Codecool.DungeonCrawl.Logic.Items
{
    /// <summary>
    /// Sample enemy
    /// </summary>
    public class Door : Item
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Door"/> class.
        /// </summary>
        /// <param name="cell">The starting cell</param>
        public Door(Cell cell)
            : base(cell)
        {
        }

        /// <inheritdoc/>
        public override string Tilename => "door";
    }
}
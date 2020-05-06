namespace Codecool.DungeonCrawl.Logic.Items
{
    /// <summary>
    /// Actor is a base class for every entity in the dungeon.
    /// </summary>
    public abstract class Item : IDrawable, ITakeable
    {
        /// <summary>
        /// Gets the cell where this actor is located
        /// </summary>
        public Cell Cell { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="cell">The cell of this actor</param>
        public Item(Cell cell)
        {
            Cell = cell;
            Cell.Item = this;
        }

        /// <summary>
        /// Gets the X position
        /// </summary>
        public int X => Cell.X;

        /// <summary>
        /// Gets the Y position
        /// </summary>
        public int Y => Cell.Y;

        /// <summary>
        /// Gets the name of this tile.
        /// </summary>
        public abstract string Tilename { get; }

        /// <inheritdoc/>
        public int AddToInventory()
        {
            int allItemsOfThisType = 0;
            allItemsOfThisType += 1;
            return allItemsOfThisType;
        }
    }
}
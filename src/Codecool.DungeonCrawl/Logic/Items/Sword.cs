namespace Codecool.DungeonCrawl.Logic.Items
{
    /// <summary>
    /// Sample enemy
    /// </summary>
    public class Sword : Item, IPickable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sword"/> class.
        /// </summary>
        /// <param name="cell">The starting cell</param>
        public Sword(Cell cell)
            : base(cell)
        {
        }

        /// <inheritdoc/>
        public void AddToInventory()
        {
            int allItemsOfThisType = 0;
            allItemsOfThisType += 1;

            // return allItemsOfThisType;
        }

        /// <inheritdoc/>
        public bool PickUp()
        {
            Cell.Item = null;
            return true;
        }

        /// <inheritdoc/>
        public override string Tilename => "sword";
    }
}
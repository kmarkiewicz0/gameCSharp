using Codecool.DungeonCrawl.Logic.Items.Inventory;

namespace Codecool.DungeonCrawl.Logic.Items
{
    /// <summary>
    /// Sample enemy
    /// </summary>
    public class KeyToDoor : Item, IPickable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyToDoor"/> class.
        /// </summary>
        /// <param name="cell">The starting cell</param>
        public KeyToDoor(Cell cell)
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
        public override string Tilename => "keyToDoor";

        /// <summary>
        /// Gets or sets many keys in inventory
        /// </summary>
        public int Quantity { get; set; }
    }
}
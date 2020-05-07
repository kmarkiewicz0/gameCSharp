using Codecool.DungeonCrawl.Logic.Items;

namespace Codecool.DungeonCrawl.Logic
{
    /// <summary>
    /// Interface for objects that can be drawn of the display
    /// </summary>
    public interface IPickable
    {
        /// <summary>
        /// Pick up item, delete from stage
        /// </summary>
        // void PickUp(Inventory Inventory);
        bool PickUp();

        /// <summary>
        /// Adds item to inventory
        /// </summary>
        void AddToInventory();
    }
}
namespace Codecool.DungeonCrawl.Logic
{
    /// <summary>
    /// Interface for objects that can be drawn of the display
    /// </summary>
    public interface ITakeable
    {
        /// <summary>
        /// Adds item to inventory
        /// </summary>
        int AddToInventory();
    }
}
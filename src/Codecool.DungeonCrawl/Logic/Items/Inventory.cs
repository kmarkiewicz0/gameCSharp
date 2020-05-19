using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;

namespace Codecool.DungeonCrawl.Logic.Items.Inventory
{
    /// <summary>
     /// Actor is a class for all items picked up by Player
     /// </summary>
    public class Inventory
    {
        /// <summary>
        /// Gets or sets An inventory list
        /// </summary>
        public Dictionary<string, int> InventoryDict { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Inentory"/> class.
        /// </summary>
        public Inventory()
        {
            InventoryDict = new Dictionary<string, int>()
        {
            { "swords", 0 }, { "potions", 0 },
        };

            InventoryDict.Add("keys", 1);
        }
    }
}

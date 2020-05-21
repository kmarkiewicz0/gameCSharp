using System;
using System.Threading;
using Codecool.DungeonCrawl.Logic.Items;
using Codecool.DungeonCrawl.Logic.Items.Inventory;

namespace Codecool.DungeonCrawl.Logic.Actors
{
    /// <summary>
    /// The game player
    /// </summary>
    public class Player : Actor
    {
        /// <summary>
        /// Gets or sets inventory
        /// </summary>
        public Inventory Inventory { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="cell">The starting cell</param>

        public Player(Cell cell)
            : base(cell)
        {
            Health = 100;
            Inventory = new Inventory();
            Message = "Welcome to Hell";
        }

        public bool IsSwordInInventory()
        {
            try
            {
                if (Inventory.InventoryDict["swords"] > 0)
                {
                    return true;
                }
            }
            catch
            {
            }

            return false;
        }

        /// <inheritdoc/>
        public override string Tilename => "player";

        /// <summary>
        /// Moves player by the given amount
        /// </summary>
        /// <param name="dx">X amount</param>
        /// <param name="dy">Y amount</param>
        public override void Move(int dx, int dy)
        {
            Cell nextCell = Cell.GetNeighbor(dx, dy);

            if (nextCell.Tilename == "Floor")
            {
                if (nextCell.Actor == null)
                {
                    if (nextCell.Item is Door)
                    {
                        if (nextCell.Item.Used)
                        {
                            Cell.Actor = null;
                            nextCell.Actor = this;
                            Cell = nextCell;
                        }
                        else
                        {
                            if (Inventory.InventoryDict["keys"] > 0)
                            {
                                Cell.Actor = null;
                                nextCell.Actor = this;
                                Cell = nextCell;
                                nextCell.Item.Used = true;
                                Inventory.InventoryDict["keys"] -= 1;
                            }
                        }
                    }
                    else
                    {
                        Cell.Actor = null;
                        nextCell.Actor = this;
                        Cell = nextCell;
                    }
                }
            }
        }

        public override void Attack(int dx, int dy)
        {
            Cell nextCell = Cell.GetNeighbor(dx, dy);
            if (nextCell.Actor != null)
            {
                if (nextCell.Actor.Tilename == "skeleton")
                {
                    if (IsSwordInInventory())
                    {
                        nextCell.Actor.Health -= 10;
                        Program.RenderMessage($"Player deals 10 damage to Skeleton. Skeleton hp left" +
                            $" {nextCell.Actor.Health}");
                    }
                    else
                    {
                        nextCell.Actor.Health -= 5;
                        Program.RenderMessage($"Player deals 5 damage to Skeleton. Skeleton hp left" +
                            $" {nextCell.Actor.Health}");
                    }

                    if (nextCell.Actor.Health != 0)
                    {
                        Cell.Actor.Health -= 2;
                    }
                }
                else if (nextCell.Actor.Tilename == "ghost")
                {
                    if (IsSwordInInventory())
                    {
                        nextCell.Actor.Health -= 10;
                        Program.RenderMessage($"Player deals 10 damage to Ghost. Ghost hp left" +
                            $" {nextCell.Actor.Health}");
                    }
                    else
                    {
                        nextCell.Actor.Health -= 5;
                        Program.RenderMessage($"Player deals 5 damage to Ghost. Ghost hp left" +
                            $" {nextCell.Actor.Health}");
                    }

                    if (nextCell.Actor.Health != 0)
                    {
                        Cell.Actor.Health -= 5;
                    }
                }
            }
        }
    }
}

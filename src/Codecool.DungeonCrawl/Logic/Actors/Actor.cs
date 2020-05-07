using System;
using System.Security.Cryptography;

namespace Codecool.DungeonCrawl.Logic.Actors
{
    /// <summary>
    /// Actor is a base class for every entity in the dungeon.
    /// </summary>
    public abstract class Actor : IDrawable
    {
        /// <summary>
        /// Gets the cell where this actor is located
        /// </summary>
        public Cell Cell { get; set; }

        /// <summary>
        /// Gets or sets this actors health
        /// </summary>
        public int Health { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Actor"/> class.
        /// </summary>
        /// <param name="cell">The cell of this actor</param>
        public Actor(Cell cell)
        {
            Cell = cell;
            Cell.Actor = this;
        }

        /// <summary>
        /// Moves this actor by the given amount
        /// </summary>
        /// <param name="dx">X amoount</param>
        /// <param name="dy">Y amount</param>
        public virtual void Move(int dx, int dy)
        {
            Cell nextCell = Cell.GetNeighbor(dx, dy);
            if (nextCell.Tilename == "Floor")
            {
                if (nextCell.Actor == null)
                {
                    Cell.Actor = null;
                    nextCell.Actor = this;
                    Cell = nextCell;
                }
            }
        }

        /// <summary>
        /// Deals damage to actors in same cell
        /// </summary>
        /// <param name="dx">X amount</param>
        /// <param name="dy">Y amount</param>
        public void Attack(int dx, int dy)
        {
            Cell nextCell = Cell.GetNeighbor(dx, dy);
            if (nextCell.Actor != null)
            {
                nextCell.Actor.Health -= 5;
                Console.WriteLine($"Player deals 5 damage to Skeleton. Health left" +
                    $" {nextCell.Actor.Health}");
                if (nextCell.Actor.Health != 0)
                {
                    Cell.Actor.Health -= 2;
                    Console.WriteLine($"Skeleton deals 2 damage to Player. Health left" +
                    $" {Cell.Actor.Health}");
                }
                else
                {
                    Console.WriteLine("Skeleton died");
                }
            }
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
    }
}
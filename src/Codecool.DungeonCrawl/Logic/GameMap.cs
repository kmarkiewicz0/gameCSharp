using System.Collections.Generic;
using System;
using Codecool.DungeonCrawl.Logic.Actors;
using Codecool.DungeonCrawl.Logic.Items;

namespace Codecool.DungeonCrawl.Logic
{
    /// <summary>
    /// The game map
    /// </summary>
    public class GameMap
    {
        /// <summary>
        /// The player
        /// </summary>
        public Player Player;

        /// <summary>
        /// A sample enemy list
        /// </summary>
        public List<Skeleton> Skeletons;

        /// <summary>
        /// A ghost enemy list
        /// </summary>
        public List<Ghost> Ghosts;

        /// <summary>
        /// A boss
        /// </summary>
        public Dragon Dragon;

        /// <summary>
        /// Dragon fire list
        /// </summary>
        public List<DragonFire> DragonFires;

        /// <summary>
        /// Key to the door
        /// </summary>
        public KeyToDoor KeyToDoor;

        /// <summary>
        /// Sword
        /// </summary>
        public Sword Sword;

        /// <summary>
        /// Door
        /// </summary>
        public Door Door;

        /// <summary>
        /// Stairs
        /// </summary>
        public Stairs Stairs;

        private readonly Cell[,] _cells;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameMap"/> class.
        /// </summary>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <param name="defaultCellType">Default cell type</param>
        public GameMap(int width, int height, CellType defaultCellType)
        {
            Width = width;
            Height = height;
            _cells = new Cell[width, height];
            Skeletons = new List<Skeleton>();
            Ghosts = new List<Ghost>();
            DragonFires = new List<DragonFire>();

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    _cells[x, y] = new Cell(this, x, y, defaultCellType);
                }
            }
        }

        /// <summary>
        /// Gets the cell at the given coordinate
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns>The cell</returns>
        public Cell GetCell(int x, int y) => _cells[x, y];

        /// <summary>
        /// Gets the width
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets the height
        /// </summary>
        public int Height { get; }
    }
}
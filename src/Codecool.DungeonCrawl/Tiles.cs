using System.Collections.Generic;
using Codecool.DungeonCrawl.Logic;
using Perlin.Geom;

namespace Codecool.DungeonCrawl
{
    /// <summary>
    /// Helper class to read the tile map image.
    /// </summary>
    public static class Tiles
    {
        /// <summary>
        /// Width of a single image in the tile map
        /// </summary>
        public const int TileWidth = 32;

        /// <summary>
        /// Coordinates of the player graphic.
        /// </summary>
        public static readonly Rectangle PlayerTile = CreateTile(27, 0);

        /// <summary>
        /// Coordinates of the skeleton graphic.
        /// </summary>
        public static readonly Rectangle SkeletonTile = CreateTile(29, 6);

        /// <summary>
        /// Coordinates of the ghost graphic.
        /// </summary>
        public static readonly Rectangle GhostTile = CreateTile(27, 6);

        /// <summary>
        /// Coordinates of the key to thr door graphic.
        /// </summary>
        public static readonly Rectangle KeyToDoorTile = CreateTile(18, 23);

        /// <summary>
        /// Coordinates of sword graphic.
        /// </summary>
        public static readonly Rectangle SwordTile = CreateTile(4, 30);

        /// <summary>
        /// Coordinates of sword graphic.
        /// </summary>
        public static readonly Rectangle DoorTile = CreateTile(13, 11);

        /// <summary>
        /// Coordinates of sword graphic.
        /// </summary>
        public static readonly Rectangle DoorOpenedTile = CreateTile(12, 11);

        private static readonly Dictionary<string, Rectangle> TileMap = new Dictionary<string, Rectangle>();

        static Tiles()
        {
            TileMap["empty"] = CreateTile(0, 0);
            TileMap["wall"] = CreateTile(10, 17);
            TileMap["floor"] = CreateTile(2, 0);
        }

        /// <summary>
        /// Returns the rectangular region of the given tile
        /// </summary>
        /// <param name="d">The IDrawable's region to return</param>
        /// <returns>The region</returns>
        public static Rectangle GetMapTile(IDrawable d)
        {
            return TileMap[d.Tilename.ToLower()];
        }

        private static Rectangle CreateTile(int i, int j)
        {
            return new Rectangle(i * (TileWidth + 2), j * (TileWidth + 2), TileWidth, TileWidth);
        }
    }
}
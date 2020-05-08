using System.IO;
using System;
using System.Collections.Generic;
using Codecool.DungeonCrawl.Logic.Actors;
using Codecool.DungeonCrawl.Logic.Items;

namespace Codecool.DungeonCrawl.Logic
{
    /// <summary>
    /// Helper class to load the map from the disk
    /// </summary>
    public static class MapLoader
    {
        /// <summary>
        /// Load the map from the disk
        /// </summary>
        /// <returns>The loaded map</returns>
        /// <exception cref="InvalidDataException">The map has unrecognized character(s)</exception>
        public static GameMap LoadMap()
        {
            var lines = File.ReadAllLines("map.txt");
            var dimensions = lines[0].Split(" ");
            var width = int.Parse(dimensions[0]);
            var height = int.Parse(dimensions[1]);
            Skeleton skeleton;

            GameMap map = new GameMap(width, height, CellType.Empty);
            for (var y = 0; y < height; y++)
            {
                var line = lines[y + 1];
                for (int x = 0; x < width; x++)
                {
                    if (x < line.Length)
                    {
                        Cell cell = map.GetCell(x, y);
                        switch (line[x])
                        {
                            case ' ':
                                cell.Type = CellType.Empty;
                                break;
                            case '#':
                                cell.Type = CellType.Wall;
                                break;
                            case '.':
                                cell.Type = CellType.Floor;
                                break;
                            case 's':
                                cell.Type = CellType.Floor;

                                skeleton = new Skeleton(cell);
                                Console.Write("Skeleton health: ");
                                Console.WriteLine(skeleton.Health);

                                // TODO change this code to allow more than one enemy
                                map.Skeletons.Add(new Skeleton(cell));
                                break;
                            case '@':
                                cell.Type = CellType.Floor;
                                map.Player = new Player(cell);
                                Console.Write("Player health: ");
                                Console.Write(map.Player.Health);
                                break;
                            case 'k':
                                cell.Type = CellType.Floor;
                                map.KeyToDoor = new KeyToDoor(cell);
                                break;
                            case '/':
                                cell.Type = CellType.Floor;
                                map.Sword = new Sword(cell);
                                break;
                            default:
                                throw new InvalidDataException($"Unrecognized character: '{line[x]}'");
                        }
                    }
                }
            }

            return map;
        }
    }
}
using System;
using System.Collections.Generic;
using Codecool.DungeonCrawl.Logic;
using Codecool.DungeonCrawl.Logic.Actors;
using Codecool.DungeonCrawl.Logic.Items;
using Perlin;
using Perlin.Display;
using SixLabors.Fonts;
using Veldrid;

namespace Codecool.DungeonCrawl
{
    /// <summary>
    /// The main class and entry point.
    /// </summary>
    public class Program
    {
        private GameMap _map;
        private TextField _healthTextField;
        private Sprite _mapContainer;
        private Sprite _playerGfx;
        private Sprite _keyToDoorGfx;
        private Sprite _swordGfx;
        private Sprite _skeletonGfx;
        private List<Sprite> _skeletonsSpriteList;

        /// <summary>
        /// Entry point
        /// </summary>
        public static void Main()
        {
            new Program();
        }

        private Program()
        {
            _map = MapLoader.LoadMap();
            PerlinApp.Start(_map.Width * Tiles.TileWidth,
                _map.Height * Tiles.TileWidth,
                "Dungeon Crawl",
                OnStart);
        }

        private void OnStart()
        {
            var stage = PerlinApp.Stage;

            stage.EnterFrameEvent += StageOnEnterFrameEvent;

            _mapContainer = new Sprite();
            stage.AddChild(_mapContainer);
            DrawMap();

            _skeletonsSpriteList = new List<Sprite>();
            for (int i = 0; i < _map.Skeletons.Count; i++)
            {
                _skeletonGfx = new Sprite("tiles.png", false, Tiles.SkeletonTile);
                _skeletonGfx.X = _map.Skeletons[i].X * Tiles.TileWidth;
                _skeletonGfx.Y = _map.Skeletons[i].Y * Tiles.TileWidth;
                _skeletonsSpriteList.Add(_skeletonGfx);
                stage.AddChild(_skeletonGfx);
            }

            _keyToDoorGfx = new Sprite("tiles.png", false, Tiles.KeyToDoorTile);
            _keyToDoorGfx.X = _map.KeyToDoor.X * Tiles.TileWidth;
            _keyToDoorGfx.Y = _map.KeyToDoor.Y * Tiles.TileWidth;
            stage.AddChild(_keyToDoorGfx);

            _swordGfx = new Sprite("tiles.png", false, Tiles.Sword);
            _swordGfx.X = _map.Sword.X * Tiles.TileWidth;
            _swordGfx.Y = _map.Sword.Y * Tiles.TileWidth;
            stage.AddChild(_swordGfx);

            _playerGfx = new Sprite("tiles.png", false, Tiles.PlayerTile);
            stage.AddChild(_playerGfx);

            // health textField
            string healthDisplayText = "HP: " + _map.Player.Health.ToString();
            _healthTextField = new TextField(
                PerlinApp.FontRobotoMono.CreateFont(14),
                healthDisplayText,
                false);
            _healthTextField.HorizontalAlign = HorizontalAlignment.Right;
            _healthTextField.Width = 100;
            _healthTextField.Height = 20;
            _healthTextField.X = _map.Width * Tiles.TileWidth - 100;
            stage.AddChild(_healthTextField);
    }

        private void DrawMap()
        {
            _mapContainer.RemoveAllChildren();
            for (int x = 0; x < _map.Width; x++)
            {
                for (int y = 0; y < _map.Height; y++)
                {
                    var cell = _map.GetCell(x, y);
                    var tile = Tiles.GetMapTile(cell);

                    // tiles are 16x16 pixels
                    var sp = new Sprite("tiles.png", false, tile);
                    sp.X = x * Tiles.TileWidth;
                    sp.Y = y * Tiles.TileWidth;
                    _mapContainer.AddChild(sp);
                }
            }
        }

        // this gets called every frame
        private void StageOnEnterFrameEvent(DisplayObject target, float elapsedtimesecs)
        {
            Random rnd = new Random();
            int randomY, randomX;

            // process inputs
            if (KeyboardInput.IsKeyPressedThisFrame(Key.W) || KeyboardInput.IsKeyPressedThisFrame(Key.Up))
            {
                _map.Player.Move(0, -1);
                foreach (Skeleton skeleton in _map.Skeletons)
                {
                    randomY = rnd.Next(-1, 2);
                    randomX = rnd.Next(-1, 2);
                    skeleton.Move(randomX, randomY);
                }
            }

            if (KeyboardInput.IsKeyPressedThisFrame(Key.S) || KeyboardInput.IsKeyPressedThisFrame(Key.Down))
            {
                _map.Player.Move(0, 1);
                foreach (Skeleton skeleton in _map.Skeletons)
                {
                    randomY = rnd.Next(-1, 2);
                    randomX = rnd.Next(-1, 2);
                    skeleton.Move(randomX, randomY);
                }
            }

            if (KeyboardInput.IsKeyPressedThisFrame(Key.A) || KeyboardInput.IsKeyPressedThisFrame(Key.Left))
            {
                _map.Player.Move(-1, 0);
                foreach (Skeleton skeleton in _map.Skeletons)
                {
                    randomY = rnd.Next(-1, 2);
                    randomX = rnd.Next(-1, 2);
                    skeleton.Move(randomX, randomY);
                }
            }

            if (KeyboardInput.IsKeyPressedThisFrame(Key.D) || KeyboardInput.IsKeyPressedThisFrame(Key.Right))
            {
                _map.Player.Move(1, 0);
                foreach (Skeleton skeleton in _map.Skeletons)
                {
                    randomY = rnd.Next(-1, 2);
                    randomX = rnd.Next(-1, 2);
                    skeleton.Move(randomX, randomY);
                }
            }

            // render changes
            _playerGfx.X = _map.Player.X * Tiles.TileWidth;
            _playerGfx.Y = _map.Player.Y * Tiles.TileWidth;

            int countSkeleton = 0;
            foreach (Skeleton skeleton in _map.Skeletons)
            {
                _skeletonsSpriteList[countSkeleton].X = skeleton.X * Tiles.TileWidth;
                _skeletonsSpriteList[countSkeleton].Y = skeleton.Y * Tiles.TileWidth;

                if (skeleton.Health == 0)
                {
                    PerlinApp.Stage.RemoveChild(_skeletonsSpriteList[countSkeleton]);
                    skeleton.Cell.Actor = null;
                }

                countSkeleton++;
            }
        }
    }
}
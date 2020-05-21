using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using Codecool.DungeonCrawl.Logic;
using Codecool.DungeonCrawl.Logic.Actors;
using Codecool.DungeonCrawl.Logic.Items;
using Codecool.DungeonCrawl.Logic.Items.Inventory;
using Perlin;
using Perlin.Display;
using SharpDX.Direct3D11;
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
        private TextField _inventoryTextField;
        private List<TextField> _inventoryList;
        private Sprite _mapContainer;
        private Sprite _playerGfx;
        private Sprite _keyToDoorGfx;
        private Sprite _swordGfx;
        private Sprite _skeletonGfx;
        private Sprite _ghostGfx;
        private Sprite _doorGfx;
        private List<Sprite> _skeletonsSpriteList;
        private List<Sprite> _ghostSpriteList;

        public static TextField GameMessage { get; set; }

        /// <summary>
        /// Entry point
        /// </summary>
        public static void Main()
        {
            new Program();
        }

        public static void RenderMessage(string message)
        {
            GameMessage.Text = message;
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

            // Skeletons rendering
            _skeletonsSpriteList = new List<Sprite>();
            for (int i = 0; i < _map.Skeletons.Count; i++)
            {
                _skeletonGfx = new Sprite("tiles2.png", false, Tiles.SkeletonTile);
                _skeletonGfx.X = _map.Skeletons[i].X * Tiles.TileWidth;
                _skeletonGfx.Y = _map.Skeletons[i].Y * Tiles.TileWidth;
                _skeletonsSpriteList.Add(_skeletonGfx);
                stage.AddChild(_skeletonGfx);
            }

            // Ghost rendering
            _ghostSpriteList = new List<Sprite>();
            for (int i = 0; i < _map.Ghosts.Count; i++)
            {
                _ghostGfx = new Sprite("tiles2.png", false, Tiles.GhostTile);
                _ghostGfx.X = _map.Ghosts[i].X * Tiles.TileWidth;
                _ghostGfx.Y = _map.Ghosts[i].Y * Tiles.TileWidth;
                _ghostSpriteList.Add(_ghostGfx);
                stage.AddChild(_ghostGfx);
            }

            // Key rendering
            _keyToDoorGfx = new Sprite("tiles2.png", false, Tiles.KeyToDoorTile);
            _keyToDoorGfx.X = _map.KeyToDoor.X * Tiles.TileWidth;
            _keyToDoorGfx.Y = _map.KeyToDoor.Y * Tiles.TileWidth;
            stage.AddChild(_keyToDoorGfx);

            //Sword rendering
            _swordGfx = new Sprite("tiles2.png", false, Tiles.SwordTile);
            _swordGfx.X = _map.Sword.X * Tiles.TileWidth;
            _swordGfx.Y = _map.Sword.Y * Tiles.TileWidth;
            stage.AddChild(_swordGfx);

            //Door rendering
            _doorGfx = new Sprite("tiles2.png", false, Tiles.DoorTile);
            _doorGfx.X = _map.Door.X * Tiles.TileWidth;
            _doorGfx.Y = _map.Door.Y * Tiles.TileWidth;
            stage.AddChild(_doorGfx);

            //Player rendering (first)
            _playerGfx = new Sprite("tiles2.png", false, Tiles.PlayerTile);
            stage.AddChild(_playerGfx);

            // health textField
            string healthDisplayText = "HP: " + _map.Player.Health.ToString();
            _healthTextField = new TextField(
                PerlinApp.FontRobotoMono.CreateFont(18),
                healthDisplayText,
                false);
            _healthTextField.HorizontalAlign = HorizontalAlignment.Right;
            _healthTextField.Width = 100;
            _healthTextField.Height = 30;
            _healthTextField.X = _map.Width * Tiles.TileWidth - 100;
            stage.AddChild(_healthTextField);

            // game message render
            string message = _map.Player.Message;
            GameMessage = new TextField(PerlinApp.FontRobotoMono.CreateFont(18), message, false);
            GameMessage.Width = 220;
            GameMessage.Height = 120;
            GameMessage.HorizontalAlign = HorizontalAlignment.Right;
            GameMessage.X = _map.Width * Tiles.TileWidth - 230;
            GameMessage.Y = (_map.Height * Tiles.TileWidth) / 2;
            stage.AddChild(GameMessage);

            InventoryRender();
        }

        private void InventoryRender()
        {
            _inventoryList = new List<TextField>();
                int itemCounter = 1;
                foreach (KeyValuePair<string, int> invItem in _map.Player.Inventory.InventoryDict)
                {
                    string itemText = invItem.Key + ":" + invItem.Value.ToString();
                    _inventoryTextField = new TextField(PerlinApp.FontRobotoMono.CreateFont(18), itemText, false);
                    _inventoryTextField.HorizontalAlign = HorizontalAlignment.Right;
                    _inventoryTextField.Width = 100;
                    _inventoryTextField.Height = 25;
                    _inventoryTextField.Y = _map.Height * Tiles.TileWidth - 25 * itemCounter;
                    _inventoryTextField.X = _map.Width * Tiles.TileWidth - 100;
                    itemCounter++;
                    _inventoryList.Add(_inventoryTextField);
                }

            foreach (TextField item in _inventoryList)
            {
                PerlinApp.Stage.AddChild(item);
            }
        }

        private void ClearInventory()
        {
            foreach (TextField item in _inventoryList)
            {
                PerlinApp.Stage.RemoveChild(item);
            }
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
                    var sp = new Sprite("tiles2.png", false, tile);
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
                _map.Player.Attack(0, -1);

                // Update skeleton position
                foreach (Skeleton skeleton in _map.Skeletons)
                {
                    randomY = rnd.Next(-1, 2);
                    randomX = rnd.Next(-1, 2);
                    skeleton.Move(randomX, randomY);
                }

                // Update ghost position
                foreach (Ghost ghost in _map.Ghosts)
                {
                    randomY = rnd.Next(-1, 2);
                    randomX = rnd.Next(-1, 2);
                    ghost.Move(randomX, randomY);
                }
            }

            if (KeyboardInput.IsKeyPressedThisFrame(Key.S) || KeyboardInput.IsKeyPressedThisFrame(Key.Down))
            {
                _map.Player.Move(0, 1);
                _map.Player.Attack(0, 1);
                foreach (Skeleton skeleton in _map.Skeletons)
                {
                    randomY = rnd.Next(-1, 2);
                    randomX = rnd.Next(-1, 2);
                    skeleton.Move(randomX, randomY);
                }

                foreach (Ghost ghost in _map.Ghosts)
                {
                    randomY = rnd.Next(-1, 2);
                    randomX = rnd.Next(-1, 2);
                    ghost.Move(randomX, randomY);
                }
            }

            if (KeyboardInput.IsKeyPressedThisFrame(Key.A) || KeyboardInput.IsKeyPressedThisFrame(Key.Left))
            {
                _map.Player.Move(-1, 0);
                _map.Player.Attack(-1, 0);
                foreach (Skeleton skeleton in _map.Skeletons)
                {
                    randomY = rnd.Next(-1, 2);
                    randomX = rnd.Next(-1, 2);
                    skeleton.Move(randomX, randomY);
                }

                foreach (Ghost ghost in _map.Ghosts)
                {
                    randomY = rnd.Next(-1, 2);
                    randomX = rnd.Next(-1, 2);
                    ghost.Move(randomX, randomY);
                }
            }

            if (KeyboardInput.IsKeyPressedThisFrame(Key.D) || KeyboardInput.IsKeyPressedThisFrame(Key.Right))
            {
                _map.Player.Move(1, 0);
                _map.Player.Attack(1, 0);
                foreach (Skeleton skeleton in _map.Skeletons)
                {
                    randomY = rnd.Next(-1, 2);
                    randomX = rnd.Next(-1, 2);
                    skeleton.Move(randomX, randomY);
                }

                foreach (Ghost ghost in _map.Ghosts)
                {
                    randomY = rnd.Next(-1, 2);
                    randomX = rnd.Next(-1, 2);
                    ghost.Move(randomX, randomY);
                }
            }

            // render changes
            _playerGfx.X = _map.Player.X * Tiles.TileWidth;
            _playerGfx.Y = _map.Player.Y * Tiles.TileWidth;
            _healthTextField.Text = "HP: " + _map.Player.Health.ToString();

            if (_map.Player.X == _map.KeyToDoor.X && _map.Player.Y == _map.KeyToDoor.Y && _map.KeyToDoor.Cell.Item != null)
            {
                //TODO: and if nacisnieto przycisk

                PerlinApp.Stage.RemoveChild(_keyToDoorGfx);
                _map.Player.Inventory.AddToInventory("keys");
                _map.KeyToDoor.PickUp();

                ClearInventory();
                InventoryRender();
            }

            if (_map.Player.X == _map.Sword.X && _map.Player.Y == _map.Sword.Y && _map.Sword.Cell.Item != null)
            {
                PerlinApp.Stage.RemoveChild(_swordGfx);

                // TODO: and if nacisnieto przycisk
                _map.Player.Inventory.AddToInventory("swords");
                _map.Sword.PickUp();

                ClearInventory();
                InventoryRender();
            }

            // Render skeleton changes
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

            // Render ghost changes
            int countGhost = 0;
            foreach (Ghost ghost in _map.Ghosts)
            {
                _ghostSpriteList[countGhost].X = ghost.X * Tiles.TileWidth;
                _ghostSpriteList[countGhost].Y = ghost.Y * Tiles.TileWidth;

                if (ghost.Health == 0)
                {
                    PerlinApp.Stage.RemoveChild(_ghostSpriteList[countGhost]);
                    ghost.Cell.Actor = null;
                }
            }
        }
    }
}
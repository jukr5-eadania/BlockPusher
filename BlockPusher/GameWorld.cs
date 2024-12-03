﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BlockPusher
{
    public enum GameState
    {
        MainMenu,
        LevelSelect,
        Playing
    }

    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<GameObject> gameObjects = new List<GameObject>();
        private Texture2D collisionTexture;

        private GameState _gameState = GameState.MainMenu;
        private int selectedMenuItem = 0;
        private string[] menuItems = { "Start Game", "Exit" };
        private SpriteFont menuFont;

        private Dictionary<Vector3, int> tiles;
        private Dictionary<Vector3, int> objects;
        private Texture2D textureAtlas;

        public static int Height { get; set; }
        public static int Width { get; set; }

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.HardwareModeSwitch = false;
            Window.IsBorderless = true;
            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            GameWorld.Height = _graphics.PreferredBackBufferHeight;
            GameWorld.Width = _graphics.PreferredBackBufferWidth;
            gameObjects.Add(new Player());
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            collisionTexture = Content.Load<Texture2D>("pixel");
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.LoadContent(Content);
            }

            menuFont = Content.Load<SpriteFont>("MenuFont");

            textureAtlas = Content.Load<Texture2D>("tilesheet");
            tiles = LoadMap("../../../Content/MapData/TestmapBlocks_Tiles.csv", 0);
            objects = LoadMap("../../../Content/MapData/TestmapBlocks_Objects.csv", 1);
            AddTiles(tiles);
            AddTiles(objects);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (_gameState)
            {
                case GameState.MainMenu:
                    UpdateMainMenu();
                    break;

                case GameState.LevelSelect:
                    // Handle Level Select State (to be implemented later)
                    break;

                case GameState.Playing:
                    // Handle Playing State
                    break;
            }

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(gameTime);
                foreach (GameObject other in gameObjects)
                {
                    gameObject.CheckCollision(other);
                }
            }
            CheckWin();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.BackToFront);

            switch (_gameState)
            {
                case GameState.MainMenu:
                    DrawMainMenu();
                    break;

                case GameState.LevelSelect:
                    // Draw Level Select screen (to be implemented)
                    break;

                case GameState.Playing:
                    // Draw the gameplay (as per your existing code)
                    break;
            }

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(_spriteBatch);
                DrawCollisionBox(gameObject);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void SetGameState(GameState gameState)
        {
            if (gameState != _gameState)
            {
                _gameState = gameState;
                switch (gameState)
                {
                    case GameState.MainMenu:
                        break;

                    case GameState.LevelSelect:
                        break;

                    case GameState.Playing:
                        break;
                }
            }
        }

        private void UpdateMainMenu()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                selectedMenuItem--;
                if (selectedMenuItem < 0) selectedMenuItem = menuItems.Length - 1;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                selectedMenuItem++;
                if (selectedMenuItem >= menuItems.Length) selectedMenuItem = 0;
            }
            else if (keyboardState.IsKeyDown(Keys.Enter))
            {
                SelectMenuItem();
            }
        }

        private void DrawMainMenu()
        {
            for (int i = 0; i < menuItems.Length; i++)
            {
                Color itemColor = i == selectedMenuItem ? Color.Yellow : Color.White;
                _spriteBatch.DrawString(menuFont, menuItems[i], new Vector2(Width / 2 - menuFont.MeasureString(menuItems[i]).X / 2, 150 + i * 40), itemColor);
            }
        }

        private void SelectMenuItem()
        {
            switch (selectedMenuItem)
            {
                case 0: // Start Game
                    SetGameState(GameState.LevelSelect);
                    break;

                case 1: // Exit
                    Exit();
                    break;
            }
        }

        private void DrawCollisionBox(GameObject go)
        {

            Rectangle collisionBox = go.collisionBox;
            Rectangle topLine = new Rectangle(collisionBox.X, collisionBox.Y, collisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(collisionBox.X, collisionBox.Y + collisionBox.Height, collisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(collisionBox.X + collisionBox.Width, collisionBox.Y, 1, collisionBox.Height);
            Rectangle leftLine = new Rectangle(collisionBox.X, collisionBox.Y, 1, collisionBox.Height);

            _spriteBatch.Draw(collisionTexture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 0);
            _spriteBatch.Draw(collisionTexture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 0);
            _spriteBatch.Draw(collisionTexture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 0);
            _spriteBatch.Draw(collisionTexture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Returns every position and type of tile from given tilemap
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private Dictionary<Vector3, int> LoadMap(string filepath, int layer)
        {
            Dictionary<Vector3, int> result = new();
            StreamReader reader = new(filepath);

            int y = 0;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(',');

                for (int x = 0; x < items.Length; x++)
                {
                    if (int.TryParse(items[x], out int value))
                    {
                        if (value > -1)
                        {
                            result[new Vector3(x, y, layer)] = value;
                        }
                    }
                }
                y++;
            }
            return result;
        }
        /// <summary>
        /// Adds tiles to list of gameobjects
        /// </summary>
        /// <param name="ground"></param>
        private void AddTiles(Dictionary<Vector3, int> ground)
        {
            foreach (var item in ground)
            {
                // Adjust to scale level size
                int displayTilesize = 128;
                int numTilesPerRow = 13;
                int pixelTilesize = 128;

                Rectangle destinationRectange = new((int)item.Key.X * displayTilesize, (int)item.Key.Y * displayTilesize, displayTilesize, displayTilesize);

                int x = item.Value % numTilesPerRow;
                int y = item.Value / numTilesPerRow;
                bool collision = (item.Value != 89);

                Rectangle source = new(x * pixelTilesize, y * pixelTilesize, pixelTilesize, pixelTilesize);

                if (item.Value == 3)
                {
                    gameObjects.Add(new Box2(textureAtlas, destinationRectange, source));
                }
                else if (item.Value == 102)
                {
                    gameObjects.Add(new Goal(textureAtlas, destinationRectange, source));
                }
                else if (item.Value == 24)
                {
                    Door doorOrange = new Door(textureAtlas, destinationRectange, source, collision, "orange");
                    gameObjects.Add(doorOrange);
                    Button.doors.Add(doorOrange);
                }
                else if (item.Value == 25)
                {
                    gameObjects.Add(new Button(textureAtlas, destinationRectange, source, "orange"));
                }
                else
                {
                    gameObjects.Add(new Wall(textureAtlas, destinationRectange, source, collision));
                }
            }
        }
        // Checks if all goals have a box on them, runs win logic if they do
        private void CheckWin()
        {
            int goalCount = gameObjects.Count(x => x is Goal);
            int activeGoals = 0;
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject is Goal && gameObject.goalPressed)
                {
                    activeGoals++;
                }
            }
            if (activeGoals >= goalCount)
            {
                // Win logic here
                Exit();
            }
        }
    }
}

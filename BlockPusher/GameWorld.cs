using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BlockPusher
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<GameObject> gameObjects = new List<GameObject>();
        private Texture2D collisionTexture;

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

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(_spriteBatch);
                DrawCollisionBox(gameObject);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
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

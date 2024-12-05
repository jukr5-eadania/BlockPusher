using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BlockPusher
{
    /// <summary>
    /// Enum used to decide what state the game is in
    /// </summary>
    public enum GameState
    {
        MainMenu,
        LevelSelect,
        Playing
    }

    /// <summary>
    /// "GameWorld" is the main class. It is here we "create" the game by initializing objects and loading in the content we need to 
    /// make the visual of the game. It is in charge of the main game loop.
    /// Made by: Julius, Emilie, Mads
    /// </summary>
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<GameObject> gameObjects = new List<GameObject>();
        private Texture2D collisionTexture;

        private SpriteFont menuFont;
        private GameState _gameState = GameState.MainMenu;
        private int selectedMainMenuItem = 0;
        private string[] mainMenuItems = { "Start Game", "Exit" };

        private int selectedLevelMenuItem = 0;
        private string[] levelMenuItems = { "level 1", "level 2", "level 3", "level 4", "level 5", "level 6", "level 7", "level 8", "level 9", "level 10", "Go Back" };

        private float inputDelay = 0.2f;
        private float timeSinceLastInput = 0f;

        private Dictionary<Vector3, int> tiles;
        private Dictionary<Vector3, int> objects;
        private Texture2D textureAtlas;
        private Matrix translation;

        public static int Height { get; set; }
        public static int Width { get; set; }

        /// <summary>
        /// "GameWorld()" is the window the game runs in.
        /// </summary>
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
            translation = Matrix.CreateScale(1f);
        }

        /// <summary>
        /// "Initialize" creates the objects
        /// </summary>
        protected override void Initialize()
        {
            GameWorld.Height = _graphics.PreferredBackBufferHeight;
            GameWorld.Width = _graphics.PreferredBackBufferWidth;
            base.Initialize();
        }

        /// <summary>
        /// Loads our game content in order to give the objects sprites
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            collisionTexture = Content.Load<Texture2D>("pixel");
            menuFont = Content.Load<SpriteFont>("MenuFont");
            textureAtlas = Content.Load<Texture2D>("tilesheet");
        }

        /// <summary>
        /// The main loop of the game
        /// </summary>
        /// <param name="gameTime">Takes a GameTime that provides the timespan since last call to update</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) && _gameState == GameState.Playing)
            {
                SetGameState(GameState.MainMenu);
                translation = Matrix.CreateScale(1f);
            }

            timeSinceLastInput += (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (_gameState)
            {
                case GameState.MainMenu:
                    UpdateMainMenu();
                    break;

                case GameState.LevelSelect:
                    UpdateLevelMenu();
                    break;

                case GameState.Playing:
                    foreach (GameObject gameObject in gameObjects)
                    {
                        gameObject.Update(gameTime);
                        foreach (GameObject other in gameObjects)
                        {
                            gameObject.CheckCollision(other);
                        }
                    }
                    CheckWin();
                    break;
            }
        }

        /// <summary>
        /// "Draw" is called regulary to take the current game stat and draw what we want on the screen
        /// </summary>
        /// <param name="gameTime">Takes a GameTime that provides the timespan since last call to update</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.BackToFront, transformMatrix: translation);

            switch (_gameState)
            {
                case GameState.MainMenu:
                    DrawMainMenu();
                    break;

                case GameState.LevelSelect:
                    DrawLevelMenu();
                    break;

                case GameState.Playing:
                    foreach (GameObject gameObject in gameObjects)
                    {
                        gameObject.Draw(_spriteBatch);
#if DEBUG
                        DrawCollisionBox(gameObject);
#endif
                    }
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Sets the gamestate based with enums based on input with parameter
        /// -Julius
        /// </summary>
        /// <param name="gameState">The gamestate as enum</param>
        public void SetGameState(GameState gameState)
        {
            if (gameState != _gameState)
            {
                _gameState = gameState;
            }
        }

        /// <summary>
        /// Controls the main menu based on player input
        /// -Julius
        /// </summary>
        private void UpdateMainMenu()
        {
            if (timeSinceLastInput < inputDelay)
            {
                return;
            }

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W))
            {
                selectedMainMenuItem--;
                if (selectedMainMenuItem < 0)
                {
                    selectedMainMenuItem = 0;
                }
                timeSinceLastInput = 0f;
            }
            else if (keyboardState.IsKeyDown(Keys.S))
            {
                selectedMainMenuItem++;
                if (selectedMainMenuItem >= mainMenuItems.Length)
                {
                    selectedMainMenuItem = mainMenuItems.Length - 1;
                }
                timeSinceLastInput = 0f;
            }
            else if (keyboardState.IsKeyDown(Keys.Enter))
            {
                SelectMainMenuItem();
                timeSinceLastInput = 0f;
            }
        }

        /// <summary>
        /// Draws the main menu for the player to see
        /// -Julius
        /// </summary>
        private void DrawMainMenu()
        {
            for (int i = 0; i < mainMenuItems.Length; i++)
            {
                Color itemColor = i == selectedMainMenuItem ? Color.HotPink : Color.White;
                _spriteBatch.DrawString(menuFont, mainMenuItems[i], new Vector2(Width / 2 - menuFont.MeasureString(mainMenuItems[i]).X / 2, 150 + i * 40), itemColor);
            }
        }

        /// <summary>
        /// Decides what each option in the main menu does based on player selection
        /// -Julius
        /// </summary>
        private void SelectMainMenuItem()
        {
            switch (selectedMainMenuItem)
            {
                case 0: // Start Game
                    SetGameState(GameState.LevelSelect);
                    break;

                case 1: // Exit
                    Exit();
                    break;
            }
        }

        /// <summary>
        /// Controls the level select menu based on player input
        /// -Julius
        /// </summary>
        private void UpdateLevelMenu()
        {
            if (timeSinceLastInput < inputDelay)
            {
                return;
            }

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W))
            {
                selectedLevelMenuItem--;
                if (selectedLevelMenuItem < 0)
                {
                    selectedLevelMenuItem = 0;
                }
                timeSinceLastInput = 0f;
            }
            else if (keyboardState.IsKeyDown(Keys.S))
            {
                selectedLevelMenuItem++;
                if (selectedLevelMenuItem >= levelMenuItems.Length)
                {
                    selectedLevelMenuItem = levelMenuItems.Length - 1;
                }
                timeSinceLastInput = 0f;
            }
            else if (keyboardState.IsKeyDown(Keys.Enter))
            {
                SelectLevelMenuItem();
                timeSinceLastInput = 0f;
            }
        }

        /// <summary>
        /// Draws the level select menu for the player to see
        /// -Julius
        /// </summary>
        private void DrawLevelMenu()
        {
            for (int i = 0; i < levelMenuItems.Length; i++)
            {
                Color itemColor = i == selectedLevelMenuItem ? Color.HotPink : Color.White;
                _spriteBatch.DrawString(menuFont, levelMenuItems[i], new Vector2(Width / 2 - menuFont.MeasureString(levelMenuItems[i]).X / 2, 150 + i * 40), itemColor);
            }
        }

        /// <summary>
        /// Decides what each option in the level select menu does based on player selection
        /// -Julius
        /// </summary>
        private void SelectLevelMenuItem()
        {
            switch (selectedLevelMenuItem)
            {
                case 0:
                    gameObjects.Clear();
                    LoadLevel(0);
                    translation = Matrix.CreateScale(0.75f);
                    SetGameState(GameState.Playing);
                    break;

                case 1:
                    gameObjects.Clear();
                    LoadLevel(1);
                    translation = Matrix.CreateScale(1f);
                    SetGameState(GameState.Playing);
                    break;

                case 2:
                    gameObjects.Clear();
                    LoadLevel(2);
                    translation = Matrix.CreateScale(1f);
                    SetGameState(GameState.Playing);
                    break;

                case 3:
                    gameObjects.Clear();
                    LoadLevel(3);
                    translation = Matrix.CreateScale(0.7f);
                    SetGameState(GameState.Playing);
                    break;

                case 4:
                    gameObjects.Clear();
                    Tiles.Door.buttons.Clear();
                    LoadLevel(4);
                    translation = Matrix.CreateScale(1f);
                    SetGameState(GameState.Playing);
                    break;

                case 5:
                    gameObjects.Clear();
                    LoadLevel(5);
                    translation = Matrix.CreateScale(1f);
                    SetGameState(GameState.Playing);
                    break;

                case 6:
                    gameObjects.Clear();
                    LoadLevel(6);
                    translation = Matrix.CreateScale(1f);
                    SetGameState(GameState.Playing);
                    break;

                case 7:
                    gameObjects.Clear();
                    LoadLevel(7);
                    translation = Matrix.CreateScale(1f);
                    SetGameState(GameState.Playing);
                    break;

                case 8:
                    gameObjects.Clear();
                    LoadLevel(8);
                    translation = Matrix.CreateScale(1f);
                    SetGameState(GameState.Playing);
                    break;

                case 9:
                    gameObjects.Clear();
                    LoadLevel(9);
                    translation = Matrix.CreateScale(1f);
                    SetGameState(GameState.Playing);
                    break;

                case 10: 
                    SetGameState(GameState.MainMenu);
                    break;
            }
        }

        /// <summary>
        /// Loads levels based on what level the player selected in the level selection menu
        /// -Julius
        /// </summary>
        /// <param name="lvl">Used to specify what level need to be loaded</param>
        public void LoadLevel(int lvl)
        {
            switch (lvl)
            {
                case 0:
                    tiles = LoadMap("../../../Content/MapData/Level1_Tiles.csv", 0);
                    objects = LoadMap("../../../Content/MapData/Level1_Obj.csv", 1);
                    AddTiles(tiles);
                    AddTiles(objects);
                    break;

                case 1:
                    tiles = LoadMap("../../../Content/MapData/Level2_Tiles.csv", 0);
                    objects = LoadMap("../../../Content/MapData/Level2_Obj.csv", 1);
                    AddTiles(tiles);
                    AddTiles(objects);
                    break;

                case 2:
                    tiles = LoadMap("../../../Content/MapData/Level3_Tiles.csv", 0);
                    objects = LoadMap("../../../Content/MapData/Level3_Obj.csv", 1);
                    AddTiles(tiles);
                    AddTiles(objects);
                    break;

                case 3:
                    tiles = LoadMap("../../../Content/MapData/Level4_Tiles.csv", 0);
                    objects = LoadMap("../../../Content/MapData/Level4_Obj.csv", 1);
                    AddTiles(tiles);
                    AddTiles(objects);
                    break;

                case 4:
                    tiles = LoadMap("../../../Content/MapData/Level5_Tiles.csv", 0);
                    objects = LoadMap("../../../Content/MapData/Level5_Obj.csv", 1);
                    AddTiles(tiles);
                    AddTiles(objects);
                    break;

                case 5:
                    tiles = LoadMap("../../../Content/MapData/Level6_Tiles.csv", 0);
                    objects = LoadMap("../../../Content/MapData/Level6_Obj.csv", 1);
                    AddTiles(tiles);
                    AddTiles(objects);
                    break;

                case 6:
                    tiles = LoadMap("../../../Content/MapData/Level7_Tiles.csv", 0);
                    objects = LoadMap("../../../Content/MapData/Level7_Obj.csv", 1);
                    AddTiles(tiles);
                    AddTiles(objects);
                    break;

                case 7:
                    tiles = LoadMap("../../../Content/MapData/Level8_Tiles.csv", 0);
                    objects = LoadMap("../../../Content/MapData/Level8_Obj.csv", 1);
                    AddTiles(tiles);
                    AddTiles(objects);
                    break;

                case 8:
                    tiles = LoadMap("../../../Content/MapData/Level9_Tiles.csv", 0);
                    objects = LoadMap("../../../Content/MapData/Level9_Obj.csv", 1);
                    AddTiles(tiles);
                    AddTiles(objects);
                    break;

                case 9:
                    tiles = LoadMap("../../../Content/MapData/TestmapBlocks_Tiles.csv", 0);
                    objects = LoadMap("../../../Content/MapData/TestmapBlocks_Objects.csv", 1);
                    AddTiles(tiles);
                    AddTiles(objects);
                    break;
            }
        }

        /// <summary>
        /// draws the red collision box
        /// </summary>
        /// <param name="go">Parameter for gameobjects</param>
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
        /// -Mads
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
        /// -Mads
        /// </summary>
        /// <param name="ground"></param>
        private void AddTiles(Dictionary<Vector3, int> ground)
        {
            foreach (var item in ground)
            {
                // Adjust to scale level size
                int displayTileSize = 128;
                int numTilesPerRow = 13;
                int pixelTileSize = 128;

                Rectangle destinationRectange = new((int)item.Key.X * displayTileSize, (int)item.Key.Y * displayTileSize, displayTileSize, displayTileSize);

                int x = item.Value % numTilesPerRow;
                int y = item.Value / numTilesPerRow;

                Rectangle source = new(x * pixelTileSize, y * pixelTileSize, pixelTileSize, pixelTileSize);

                if (item.Value == 3)
                {
                    gameObjects.Add(new Tiles.Box2(textureAtlas, destinationRectange, source));
                }
                else if (item.Value == 102)
                {
                    gameObjects.Add(new Tiles.Goal(textureAtlas, destinationRectange, source));
                }
                else if (item.Value == 24)
                {
                    Tiles.Door doorOrange = new Tiles.Door(textureAtlas, destinationRectange, source, "orange");
                    gameObjects.Add(doorOrange);
                }
                else if (item.Value == 25)
                {
                    Tiles.Button buttonOrange = new Tiles.Button(textureAtlas, destinationRectange, source, "orange");
                    gameObjects.Add(buttonOrange);
                    Tiles.Door.buttons.Add(buttonOrange);
                }
                else if (item.Value == 86)
                {
                    gameObjects.Add(new Tiles.Ice(textureAtlas, destinationRectange, source));
                }
                else if (item.Value == 89)
                {
                    gameObjects.Add(new Tiles.Floor(textureAtlas, destinationRectange, source));

                }
                else if (item.Value == 65)
                {
                    gameObjects.Add(new Player(textureAtlas, destinationRectange, source));
                }
                else
                {
                    gameObjects.Add(new Tiles.Wall(textureAtlas, destinationRectange, source));
                }
            }
        }

        /// <summary>
        /// Checks if all goals have a box on them, runs win logic if they do
        /// -Mads
        /// </summary>
        private void CheckWin()
        {
            int goalCount = gameObjects.Count(x => x is Tiles.Goal);
            int activeGoals = 0;
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject is Tiles.Goal && gameObject.goalPressed)
                {
                    activeGoals++;
                }
            }
            if (activeGoals >= goalCount)
            {
                translation = Matrix.CreateScale(1f);
                SetGameState(GameState.LevelSelect);
            }
        }
    }
}

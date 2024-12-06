using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BlockPusher
{
    /// <summary>
    /// Enum used to deside what sprite the player uses
    /// </summary>
    enum PlayerSprite
    {
        Front = 65,
        Back = 68,
        Right = 91,
        Left = 94
    }

    /// <summary>
    /// The player class controls the player 
    /// </summary>
    public class Player : GameObject
    {
              
        private float inputDelay = 0.2f;
        private float timeSinceLastInput = 0f;

        private int pixelTileSize = 128;
        int numTilesPerRow = 13;
        int value;

        
        public override Rectangle collisionBox
        {
            get => destinationRectangle;

        }
        /// <summary>
        /// Constructor used to set the stat for player
        /// </summary>
        /// <param name="textureAtlas"> the texture atlas for the player </param>
        /// <param name="destinationRectangle"> the location of the player </param>
        /// <param name="source"> the specified sprite from the texture atlas </param>
        public Player(Texture2D textureAtlas, Rectangle destinationRectangle, Rectangle source)
        {
            this.textureAtlas = textureAtlas;
            this.destinationRectangle = destinationRectangle;
            this.source = source;
        }

        /// <summary>
        /// the main loop of the player
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            timeSinceLastInput += (float)gameTime.ElapsedGameTime.TotalSeconds;
            HandleInput();

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureAtlas, destinationRectangle, source, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);

        }

        /// <summary>
        /// the method for handling the input used by the player
        /// </summary>
        public void HandleInput()
        {
            if (timeSinceLastInput < inputDelay)
            {
                return;
            }

            // get the current keyboard state
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.W))
            {
                destinationRectangle.Location += new Point(0, -128);
                timeSinceLastInput = 0f;
                moveDirection = "up";
                Animation("up");

            }

            if (keyState.IsKeyDown(Keys.S))
            {
                destinationRectangle.Location += new Point(0, 128);
                timeSinceLastInput = 0f;
                moveDirection = "down";
                Animation("down");
            }

            if (keyState.IsKeyDown(Keys.D))
            {
                destinationRectangle.Location += new Point(128, 0);
                timeSinceLastInput = 0f;
                moveDirection = "right";
                Animation("right");
            }

            if (keyState.IsKeyDown(Keys.A))
            {
                destinationRectangle.Location += new Point(-128, 0);
                timeSinceLastInput = 0f;
                moveDirection = "left";
                Animation("left");
            }

        }

        /// <summary>
        ///  Tells the program what happens when player collider with 
        /// another specified object.
        /// </summary>
        /// <param name="other"></param>
        public override void OnCollision(GameObject other)
        {

            if (other is Tiles.Wall || (other is Tiles.Door && other.collisionOn))
            {
                switch (moveDirection)
                {
                    case "right":
                        {
                            destinationRectangle.Location += new Point(-128, 0);
                            break;
                        }
                    case "left":
                        {
                            destinationRectangle.Location += new Point(128, 0);
                            break;
                        }
                    case "up":
                        {
                            destinationRectangle.Location += new Point(0, 128);
                            break;
                        }
                    case "down":
                        {
                            destinationRectangle.Location += new Point(0, -128);
                            break;
                        }
                }
            }
        }
        public void BoxCollision()
        {
            switch (moveDirection)
            {
                case "right":
                    {
                        destinationRectangle.Location += new Point(-128, 0);
                        break;
                    }
                case "left":
                    {
                        destinationRectangle.Location += new Point(128, 0);
                        break;
                    }
                case "up":
                    {
                        destinationRectangle.Location += new Point(0, 128);
                        break;
                    }
                case "down":
                    {
                        destinationRectangle.Location += new Point(0, -128);
                        break;
                    }
            }
        }

        /// <summary>
        /// Moves the source rectangle to the sprite in the texture atlas that
        /// match the direction the player is walking/moving
        /// </summary>
        /// <param name="direction"> the direction of the players movement </param>
        public void Animation(string direction)
        {
            switch (direction)
            {
                case "up":
                    {
                        value = (int)PlayerSprite.Back;
                        break;
                    }
                case "down":
                    {
                        value = (int)PlayerSprite.Front;
                        break;
                    }
                case "left":
                    {
                        value = (int)PlayerSprite.Left;
                        break;
                    }
                case "right":
                    {
                        value = (int)PlayerSprite.Right;
                        break;
                    }
            }

            int x = value % numTilesPerRow;
            int y = value / numTilesPerRow;
            source = new(x * pixelTileSize, y * pixelTileSize, pixelTileSize, pixelTileSize);
        }

    }
}

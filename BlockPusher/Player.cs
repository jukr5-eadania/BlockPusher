using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;

namespace BlockPusher
{

    /// <summary>
    /// The player class controls the player 
    /// </summary>
    internal class Player : GameObject
    {
        // Field //
        private int spriteSize = 128;
        private int texAtlasWidth = 13; // the width of our tilesheet (counted by images)
        //private int index; // default sprite
        //private int spriteX; // the X cordinate for the sprite upper left corner when drawing it
        //private int spriteY; // the Y cordinate for the sprite upper left corner when drawing it
        //private int spriteX; // the X cordinate for the sprite upper left corner when drawing it
        private float inputDelay = 0.2f;
        private float timeSinceLastInput = 0f;
        private string moveDirection;


        // Properties //
        Rectangle destinationRectangle;
        Rectangle source;


        // Properties //
        public override Rectangle collisionBox
        {
            get => destinationRectangle;

        }
        public Player(Texture2D textureAtlas, Rectangle destinationRectangle, Rectangle source)
        {
            this.textureAtlas = textureAtlas;
            this.destinationRectangle = destinationRectangle;
            this.source = source;
            
        }


        /// <summary>
        /// Loads the sprites / the spritesheet
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {

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
            }
            
            
            if (keyState.IsKeyDown(Keys.S))
            {
                destinationRectangle.Location += new Point(0, 128);
                timeSinceLastInput = 0f;
                moveDirection = "down";
            }
            
            
            if (keyState.IsKeyDown(Keys.D))
            {
                destinationRectangle.Location += new Point(128, 0);
                timeSinceLastInput = 0f;
                moveDirection = "right";
            }
            
            
            if (keyState.IsKeyDown(Keys.A))
            {
                destinationRectangle.Location += new Point(-128, 0);
                timeSinceLastInput = 0f;
                moveDirection = "left";
            }

            // When pressing R the Level resets
            if (keyState.IsKeyDown(Keys.R))
            {
                ResetLevel();
            }

        }
        

        public override void OnCollision(GameObject other)
        {

            if (other is Wall)
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

        /// <summary>
        /// Makes it possible to rested the level
        /// </summary>
        public void ResetLevel()
        {

        }

    }
}

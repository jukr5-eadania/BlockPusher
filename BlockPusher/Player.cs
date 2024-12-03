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


        public override Rectangle collisionBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, sprite.Width, sprite.Height);
            }
        }

        /// <summary>
        /// Constuctor used to set player stats
        /// </summary>
        public Player(Vector2 position)
        {
            this.Position = position;           
            speed = 300;
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
            HandleInput();
            Move(gameTime);
            
            
        }

        /// <summary>
        /// Draws the sprite so it is visual in the game
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            
            base.Draw(spriteBatch);
        }

        /// <summary>
        /// the method for handling the input used by the player
        /// </summary>
        public void HandleInput()
        {
            // reset velocity to make sure we will stop moving, when no key is pressed
            velocity = Vector2.Zero;
            
            // get the current keyboard state
            KeyboardState keyState = Keyboard.GetState();

            // Press W : Up
            if (keyState.IsKeyDown(Keys.W))
            {
                
                
            }

            // Press S : Down
            if (keyState.IsKeyDown(Keys.S))
            {
                
                
            }

            // Press A : Right
            if (keyState.IsKeyDown(Keys.D))
            {
                
            }
            
            // Press D : Left
            if (keyState.IsKeyDown(Keys.A))
            {
                
                
            }

            //To avoid moving faster when pressing more then one key,
            //the vectore needs to be normalized
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
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

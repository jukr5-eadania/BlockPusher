using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace BlockPusher
{
    /// <summary>
    /// The images for the players animations
    /// taken from the TileSheet
    /// </summary>
    enum PlayerSprite
    {
        WalkFront_0 = 65,
        WalkFront_1 = 66,
        WalkFront_2 = 67,
        WalkBack_0 = 68,
        WalkBack_1 = 69,
        WalkBack_2 = 70,
        WalkRight_0 = 91,
        WalkRight_1 = 92,
        WalkRight_2 = 93,
        WalkLeft_0 = 94,
        WalkLeft_1 = 95,
        WalkLeft_2 = 96,

    }

    /// <summary>
    /// The player class controls the player 
    /// </summary>
    internal class Player : GameObject
    {
        // Field //
        private Rectangle sourceRectangle;
       
        // Properties //

        // Methods //

        public override void LoadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput();
            Move(gameTime);
        }

        /// <summary>
        /// Constuctor used to set player stats
        /// </summary>
        public Player()
        {
            speed = 100;
        }

        public void HandleInput()
        {
            // reset velocity to make sure we will stop moving, when no key is pressed
            velocity = Vector2.Zero;

            // get the current keyboard state
            KeyboardState keyState = Keyboard.GetState();

            // Press W
            if (keyState.IsKeyDown(Keys.W))
            {
                velocity += new Vector2(0, -1);
            }

            // Press S
            if (keyState.IsKeyDown(Keys.S))
            {
                velocity += new Vector2(0, 1);
            }

            // Press A
            if (keyState.IsKeyDown(Keys.A))
            {
                velocity += new Vector2(-1, 0);
            }
            
            // Press D
            if (keyState.IsKeyDown(Keys.D))
            {
                velocity += new Vector2(1, 0);
            }

            // To avoid moving faster when pressing more then one key,
            // the vectore needs to be normalized
            if(velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            // When pressing R the Level resets
            if (keyState.IsKeyDown(Keys.R))
            {
                ResetLevel();
            }
        }

        public void ResetLevel()
        {

        }

    }
}

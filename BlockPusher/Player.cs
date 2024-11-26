﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;

namespace BlockPusher
{
    /// <summary>
    /// The images for the players animations
    /// taken from the TileSheet
    /// </summary>
    enum PlayerSprite
    {
        // front facing the camera
        WalkFront_0 = 65,
        WalkFront_1 = 66,
        WalkFront_2 = 67,
        // back facing the camera
        WalkBack_0 = 68,
        WalkBack_1 = 69,
        WalkBack_2 = 70,
        // looks towords right
        WalkRight_0 = 91,
        WalkRight_1 = 92,
        WalkRight_2 = 93,
        // looks towords left
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
        private Texture2D tilesheet;
        private int spriteSize = 128;
        private int tilesheetWidth = 13; // the width of our tilesheet (counted by images)
        private int index = (int)PlayerSprite.WalkFront_0; // default sprite
        private int spriteX; // the X cordinate for the sprite upper left corner when drawing it
        private int spriteY; // the Y cordinate for the sprite upper left corner when drawing it

        // Properties //
        public override Rectangle collisionBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, spriteSize, spriteSize);
            }
        }
        // Methods //

        /// <summary>
        /// Constuctor used to set player stats
        /// </summary>
        public Player()
        {
            position = new Vector2(500, 500);
            speed = 300;
        }

        /// <summary>
        /// Loads the sprites / the spritesheet
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            tilesheet = content.Load<Texture2D>("tilesheet");
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
            //spriteSize = 128;
            index = (int)PlayerSprite.WalkFront_0; // default sprite
            spriteX = index % tilesheetWidth;
            spriteY = index / tilesheetWidth;
            // create a sourceRectangle 
            sourceRectangle = new Rectangle(spriteX*spriteSize,spriteY*spriteSize, spriteSize, spriteSize);

            // only draw the area within the sourceRectangle
            spriteBatch.Draw(tilesheet, position, sourceRectangle, Color.White);
            
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
                velocity += new Vector2(0, -1);
            }

            // Press S : Down
            if (keyState.IsKeyDown(Keys.S))
            {
                velocity += new Vector2(0, 1);
            }

            // Press A : Right
            if (keyState.IsKeyDown(Keys.A))
            {
                velocity += new Vector2(-1, 0);
            }
            
            // Press D : Left
            if (keyState.IsKeyDown(Keys.D))
            {
                velocity += new Vector2(1, 0);
            }

            // To avoid moving faster when pressing more then one key,
            // the vectore needs to be normalized
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
            if (other is Box)
            {
                speed = 0; // collision test: if there is a collision player stops moving
            }
        }

        /// <summary>
        /// When close to a box you can press space to push it
        /// </summary>
        public void Push()
        {
            // get the current keyboard state
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Space))
            {
                // push the box... somehow
                
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

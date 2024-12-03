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
        private int index; // default sprite
        private int spriteX; // the X cordinate for the sprite upper left corner when drawing it
        private int spriteY; // the Y cordinate for the sprite upper left corner when drawing it
        private int spriteX; // the X cordinate for the sprite upper left corner when drawing it
        private float inputDelay = 0.2f;
        private float timeSinceLastInput = 0f;

        Animation animation;
        private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
        Animation animation;

        // Properties //
        Rectangle destinationRectangle;
        Rectangle source;

        // Properties //
            get => destinationRectangle;
        }

        // Properties //
        //public override Rectangle collisionBox
        //{
        //    get
        //    {
        //        return new Rectangle((int)position.X, (int)position.Y, spriteSize, spriteSize);
        //    }
        //}
        // Methods //
        }
        // Methods //

        /// <summary>
        public Player(Texture2D textureAtlas, Rectangle destinationRectangle, Rectangle source)
        {
            this.textureAtlas = textureAtlas;
            this.destinationRectangle = destinationRectangle;
            this.source = source;
            //position = new Vector2(640, 640);
            //speed = 300;
            position = new Vector2(640, 640);            
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
            timeSinceLastInput += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //animation.Update();

            animation.Update();

        }

        /// <summary>
        /// Draws the sprite so it is visual in the game
        /// </summary>
        /// <param name="spriteBatch"></param>
            //destinationRectangle = new Rectangle(640, 640, spriteSize, spriteSize);

            ////spriteSize = 128;
            //index = (int)PlayerSprite.WalkFront_0; // default sprite
            //spriteX = index % texAtlasWidth;
            //spriteY = index / texAtlasWidth;
            //// create a sourceRectangle 
            //sourceRectangle = new Rectangle(spriteX * spriteSize, spriteY * spriteSize, spriteSize, spriteSize);
            //destinationRectangle = new Rectangle(640, 640, 128, 128);

            //// only draw the area within the sourceRectangle
            //spriteBatch.Draw(textureAtlas, position, sourceRectangle, Color.White);
            // only draw the area within the sourceRectangle
            //spriteBatch.Draw(tilesheet, position, sourceRectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(textureAtlas, destinationRectangle, source, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
            //destinationRectangle = new Rectangle(640, 640, 128, 128);

            //// only draw the area within the sourceRectangle
            //spriteBatch.Draw(textureAtlas, position, sourceRectangle, Color.White);
            base.Draw(spriteBatch);
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

            // reset velocity to make sure we will stop moving, when no key is pressed
            velocity = Vector2.Zero;

            // get the current keyboard state
            KeyboardState keyState = Keyboard.GetState();
                //velocity += new Vector2(0, -128);
                //animation = new Animation(3, 3, new Vector2(128, 128), 3, 5);
                //velocity += new Vector2(0, -1);
                destinationRectangle.Location += new Point(0, -128);
                timeSinceLastInput = 0f;
            if (keyState.IsKeyDown(Keys.W))
            {
                velocity += new Vector2(0, -128);
                animation = new Animation(3, 3, new Vector2(128, 128), 3, 5);
            }
                //    velocity += new Vector2(0, 128);
                //    animation = new Animation(3, 3, new Vector2(128, 128), 0, 5);
                //velocity += new Vector2(0, 1);
                destinationRectangle.Location += new Point(0, 128);
                timeSinceLastInput = 0f;
            if (keyState.IsKeyDown(Keys.S))
            {
                velocity += new Vector2(0, 128);
                animation = new Animation(3, 3, new Vector2(128, 128), 0, 5);
            }
                //velocity += new Vector2(128, 0);
                //animation = new Animation(3, 3, new Vector2(128, 128), 0, 7);
                //velocity += new Vector2(-1, 0);
                destinationRectangle.Location += new Point(128, 0);
                timeSinceLastInput = 0f;
            if (keyState.IsKeyDown(Keys.D))
            {
                velocity += new Vector2(128, 0);
                animation = new Animation(3, 3, new Vector2(128, 128), 0, 7);
            }
                //velocity += new Vector2(-128, 0);
                //animation = new Animation(3, 3, new Vector2(128, 128), 3, 7);
                //velocity += new Vector2(1, 0);
                destinationRectangle.Location += new Point(-128, 0);
                timeSinceLastInput = 0f;
            if (keyState.IsKeyDown(Keys.A))
            {
                velocity += new Vector2(-128, 0);                
                animation = new Animation(3, 3, new Vector2(128, 128), 3, 7);
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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace BlockPusher
{
    abstract class GameObject
    {
        // Field //
        protected Vector2 velocity;
        protected float speed;
        public Vector2 position;        
        public bool collisionOn = true;
        public bool goalPressed = false;
        protected Texture2D textureAtlas;
        

        // Properties // 
        public virtual Rectangle collisionBox { get; }


        // Methods //
        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(null, Vector2.Zero, Color.White);
        }


        /// <summary>
        /// Checks if there have been a collision between two objects
        /// </summary>
        /// <param name="other"> name of the other object that is collided with </param>
        public virtual void CheckCollision(GameObject other)
        {
            if (collisionBox.Intersects(other.collisionBox) && other != this && other.collisionOn)
            {
                OnCollision(other);

            }
        }

        /// <summary>
        /// "OnCollision" tells the program what happens when two specified 
        /// objects collied.
        /// </summary>
        /// <param name="other"> name for the other gameobject that is collided with </param>
        public virtual void OnCollision(GameObject other)
        {

        }

    }
}

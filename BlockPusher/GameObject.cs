using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace BlockPusher
{
    /// <summary>
    /// "GameObject" is a superclass that all object will inherit from
    /// Made by: Julius, Emilie, Mads
    /// </summary>
    public abstract class GameObject
    {
        // Field //
        protected Texture2D textureAtlas;
        protected Rectangle destinationRectangle;
        protected Rectangle source;

        public bool goalPressed = false;
        protected List<GameObject> collidingObjects = new();
        public bool collisionOn = true;
        public string moveDirection;

        // Properties // 
        public virtual Rectangle collisionBox { get; }


        // Methods //
        /// <summary>
        /// The main loop of the object
        /// </summary>
        /// <param name="gameTime">Takes a GameTime that provides the timespan since last call to update</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Draws the sprites
        /// </summary>
        /// <param name="spriteBatch"></param>
        public abstract void Draw(SpriteBatch spriteBatch);


        /// <summary>
        /// Checks if there have been a collision between two objects
        /// </summary>
        /// <param name="other"> name of the other object that is collided with </param>
        public virtual void CheckCollision(GameObject other)
        {
            if (collisionBox.Intersects(other.collisionBox) && other != this)
            {
                OnCollision(other);
            }

            if (collisionBox.Intersects(other.collisionBox) && other != this && !collidingObjects.Contains(other))
            {
                OnCollisionEnter(other);
                collidingObjects.Add(other);
            }
            else if (collisionBox.Intersects(other.collisionBox) && other != this)
            {

            }
            else if (collidingObjects.Contains(other))
            {
                OnCollisionExit(other);
                collidingObjects.Remove(other);
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

        /// <summary>
        /// When exiting a collision this is used
        /// </summary>
        /// <param name="other">name for the other gameobject that is collided with</param>
        public virtual void OnCollisionExit(GameObject other)
        {

        }

        /// <summary>
        /// When entering an collision this is used
        /// </summary>
        /// <param name="other">name for the other gameobject that is collided with</param>
        public virtual void OnCollisionEnter(GameObject other)
        {

        }
    }
}

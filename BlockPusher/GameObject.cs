using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BlockPusher
{
    abstract class GameObject
    {
        // Field //
        protected Vector2 velocity;
        protected float speed;
        protected Vector2 position;

        // Properties // 
       

        // Methods //
        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(null, Vector2.Zero, Color.White);
        }

        /// <summary>
        /// "Move" calculates the objects movement using gameTime, 
        /// velocity and speed to find its new position.
        /// </summary>
        /// <param name="gameTime"></param>
        protected void Move(GameTime gameTime)
        {
            // Calculate deltaTime based on the gameTime
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Move the object
            position += ((velocity * speed) * deltaTime);

        }

        /// <summary>
        /// Checks if there have been a collision between two objects
        /// </summary>
        /// <param name="other"> name of the other object that is collided with </param>
        public void CheckCollision(GameObject other)
        {

        }
    }
}

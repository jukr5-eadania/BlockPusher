using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockPusher.Tiles
{
    /// <summary>
    /// Button Class that is charge of opening and closing doors based on collision
    /// The class is a child of GameObject
    /// Made by: Julius
    /// </summary>
    internal class Button : GameObject
    {
        private string color;
        private bool active;
        public override Rectangle collisionBox
        {
            get => destinationRectangle;
        }
        public string Color { get => color; set => color = value; }
        public bool Active { get => active; set => active = value; }

        /// <summary>
        /// Constructor used to set the stats of the button
        /// </summary>
        /// <param name="textureAtlas">The spritesheet of the object</param>
        /// <param name="destinationRectangle">The location of the object</param>
        /// <param name="source">The specific sprite from the spritesheet</param>
        /// <param name="color">The color of the button, Used to figure out which door to interact with</param>
        public Button(Texture2D textureAtlas, Rectangle destinationRectangle, Rectangle source, string color)
        {
            this.textureAtlas = textureAtlas;
            this.destinationRectangle = destinationRectangle;
            this.source = source;
            this.Color = color;
        }

        /// <summary>
        /// Draws the sprites
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureAtlas, destinationRectangle, source, Microsoft.Xna.Framework.Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
        }

        /// <summary>
        /// The main loop of the button
        /// </summary>
        /// <param name="gameTime">Takes a GameTime that provides the timespan since last call to update</param>
        public override void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Only checks when the objects first collide
        /// </summary>
        /// <param name="other">The other gameobjects the button collides with</param>
        public override void OnCollisionEnter(GameObject other)
        {
            if (other is Player || other is Box2)
            {
                active = true;
            }
        }

        /// <summary>
        /// Only checks when the objects stop colliding
        /// </summary>
        /// <param name="other">The other gameobjects the button collides with</param>
        public override void OnCollisionExit(GameObject other)
        {
            if (other is Player || other is Box2)
            {
                active = false;
            }
        }
    }
}

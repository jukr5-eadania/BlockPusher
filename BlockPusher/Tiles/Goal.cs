using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockPusher.Tiles
{
    /// <summary>
    /// A type of tile.
    /// If all goal tiles in a level have a box on it, the level is won.
    /// - Mads
    /// </summary>
    internal class Goal : GameObject
    {
        public override Rectangle collisionBox
        {
            get => destinationRectangle;
        }
        public Goal(Texture2D textureAtlas, Rectangle destinationRectangle, Rectangle source)
        {
            this.textureAtlas = textureAtlas;
            this.destinationRectangle = destinationRectangle;
            this.source = source;

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureAtlas, destinationRectangle, source, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        public override void Update(GameTime gameTime)
        {
            goalPressed = false;
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Box2)
            {
                goalPressed = true;
            }
        }
    }
}

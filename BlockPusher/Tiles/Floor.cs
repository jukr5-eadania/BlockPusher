using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockPusher.Tiles
{
    /// <summary>
    /// A type of tile.
    /// The floor tile doesn't have any special functionality.
    /// - Mads
    /// </summary>
    internal class Floor : GameObject
    {
        public override Rectangle collisionBox
        {
            get => destinationRectangle;
        }

        public Floor(Texture2D textureAtlas, Rectangle destinationRectange, Rectangle source)
        {
            this.textureAtlas = textureAtlas;
            destinationRectangle = destinationRectange;
            this.source = source;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureAtlas, destinationRectangle, source, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}

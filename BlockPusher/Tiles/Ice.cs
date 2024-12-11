using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockPusher.Tiles
{
    /// <summary>
    /// A type of tile.
    /// When a box moves over an ice tile, it keeps moving until it hits a non ice tile.
    /// - Mads
    /// </summary>
    internal class Ice : GameObject
    {
        public override Rectangle collisionBox
        {
            get => destinationRectangle;
        }

        public Ice(Texture2D textureAtlas, Rectangle destinationRectangle, Rectangle source)
        {
            this.textureAtlas = textureAtlas;
            this.destinationRectangle = destinationRectangle;
            this.source = source;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureAtlas, destinationRectangle, source, Color.LightSkyBlue, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}

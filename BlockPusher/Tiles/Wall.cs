using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockPusher.Tiles
{
    internal class Wall : GameObject
    {
        public override Rectangle collisionBox
        {
            get => destinationRectangle;
        }

        public Wall(Texture2D textureAtlas, Rectangle destinationRectangle, Rectangle source)
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

        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BlockPusher.Tiles
{
    internal class Floor : GameObject
    {
        Rectangle destinationRectangle;
        Rectangle source;

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
        public override void LoadContent(ContentManager content)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}

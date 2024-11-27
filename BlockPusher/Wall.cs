using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BlockPusher
{
    internal class Wall : GameObject
    {
        Rectangle destinationRectange;
        Rectangle source;
        public Wall(Texture2D textureAtlas, Rectangle destinationRectange, Rectangle source, bool collision)
        {
            this.textureAtlas = textureAtlas;
            this.destinationRectange = destinationRectange;
            this.source = source;
            this.collision = collision;

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureAtlas, destinationRectange, source, Color.White, 0, new Vector2(64, 64), SpriteEffects.None, 1);
        }
        public override void LoadContent(ContentManager content)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}

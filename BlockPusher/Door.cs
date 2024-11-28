using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BlockPusher
{
    internal class Door : GameObject
    {
        Rectangle destinationRectangle;
        Rectangle source;
        public override Rectangle collisionBox
        {
            get => destinationRectangle;
        }

        public Door(Texture2D textureAtlas, Rectangle destinationRectangle, Rectangle source, bool collision)
        {
            this.textureAtlas = textureAtlas;
            this.destinationRectangle = destinationRectangle;
            this.source = source;
            this.collisionOn = collision;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureAtlas, destinationRectangle, source, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
        }

        public override void LoadContent(ContentManager content)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (true)
            {
                
            }
        }
    }
}

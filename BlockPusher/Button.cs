using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BlockPusher
{
    internal class Button : GameObject
    {
        Rectangle destinationRectangle;
        Rectangle source;
        private bool active;
        public bool Active { get => active; set => active = value; }
        public override Rectangle collisionBox
        {
            get => destinationRectangle;
        }

        public Button(Texture2D textureAtlas, Rectangle destinationRectangle, Rectangle source)
        {
            this.textureAtlas = textureAtlas;
            this.destinationRectangle = destinationRectangle;
            this.source = source;
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
            
        }

        public override void OnCollisionEnter(GameObject other)
        {
            if (other is Player || other is Box2)
            {
                Active = true;
            }
        }

        public override void OnCollisionExit(GameObject other)
        {
            if (other is Player || other is Box2)
            {
                Active = false;
            }
        }
    }
}

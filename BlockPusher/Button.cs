using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BlockPusher
{
    internal class Button : GameObject
    {
        Rectangle destinationRectangle;
        Rectangle source;
        private bool active;
        public static List<Door> doors = new();
        private string color;
        public override Rectangle collisionBox
        {
            get => destinationRectangle;
        }

        public Button(Texture2D textureAtlas, Rectangle destinationRectangle, Rectangle source, string color)
        {
            this.textureAtlas = textureAtlas;
            this.destinationRectangle = destinationRectangle;
            this.source = source;
            this.color = color;
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
                foreach (Door door in doors)
                {
                    if (door.Color == color)
                    {
                        door.Active = true;
                    }
                }
            }
        }

        public override void OnCollisionExit(GameObject other)
        {
            if (other is Player || other is Box2)
            {
                foreach (Door door in doors)
                {
                    if (door.Color == color)
                    {
                        door.Active = false;
                    }
                }
            }
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Windows.Forms;

namespace BlockPusher
{
    internal class Box2 : GameObject
    {
        Rectangle destinationRectangle;
        Rectangle source;
        public override Rectangle collisionBox
        {
            get => destinationRectangle;
        }
        public Box2(Texture2D textureAtlas, Rectangle destinationRectangle, Rectangle source)
        {
            this.textureAtlas = textureAtlas;
            this.destinationRectangle = destinationRectangle;
            this.destinationRectangle.Location += new Point(-64, -64);
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

        public override void OnCollision(GameObject other)
        {
            switch (CheckPosition(other.position, destinationRectangle.Location))
            {
                case "right":
                    {
                        destinationRectangle.Location += new Point(-128, 0);

                        break;
                    }
                case "left":
                    {
                        destinationRectangle.Location += new Point(128, 0);
                        break;
                    }
                case "up":
                    {
                        destinationRectangle.Location += new Point(0, -128);
                        break;
                    }
                case "down":
                    {
                        destinationRectangle.Location += new Point(0, 128);
                        break;
                    }
            }
        }

        static string CheckPosition(Vector2 a, Point b)
        {
            Vector2 bVector = new Vector2(b.X, b.Y);
            var delta = a - bVector;

            if (Math.Abs(delta.X) >= Math.Abs(delta.Y))
            {
                if (delta.X >= 0)
                {
                    return "right";
                }
                else
                    {
                    return "left";
                } 
            }
            else
            {
                if (delta.Y >= 0)
                {
                    return "up";
                }
                else
                {
                    return "down";
                }
            }
        }
    }
}

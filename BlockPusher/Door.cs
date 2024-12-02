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
        private string color;
        private bool active;
        int pixelTilesize = 128;
        int numTilesPerRow = 13;
        int value = 24;
        
        public override Rectangle collisionBox
        {
            get => destinationRectangle;
        }
        public string Color { get => color; set => color = value; }
        public bool Active { get => active; set => active = value; }

        public Door(Texture2D textureAtlas, Rectangle destinationRectangle, Rectangle source, bool collision, string color)
        {
            this.textureAtlas = textureAtlas;
            this.destinationRectangle = destinationRectangle;
            this.source = source;
            this.collisionOn = collision;
            this.color = color;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                spriteBatch.Draw(textureAtlas, destinationRectangle, source, Microsoft.Xna.Framework.Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(textureAtlas, destinationRectangle, source, Microsoft.Xna.Framework.Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            }
        }

        public override void LoadContent(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            
            DoorState();
        }

        public void DoorState()
        {
            if (active == true)
            {
                value = 12;
                int x = value % numTilesPerRow;
                int y = value / numTilesPerRow;
                source = new(x * pixelTilesize, y * pixelTilesize, pixelTilesize, pixelTilesize);
                collisionOn = false;
            }
            else
            {
                value = 24;
                int x = value % numTilesPerRow;
                int y = value / numTilesPerRow;
                source = new(x * pixelTilesize, y * pixelTilesize, pixelTilesize, pixelTilesize);
                collisionOn = true;
            }
        }
    }
}

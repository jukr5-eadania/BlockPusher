using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BlockPusher
{
    enum Sprites
    {
        YellowBox = 1,
        RedBox = 2,
        BlueBox = 3,
        GreenBox = 4,
    }
    internal class Box : GameObject
    {
        // Field //
        private Rectangle sourceRectangle;
        private Texture2D tilesheet;
        private int spriteSize = 128;
        private int tilesheetWidth = 13; // the width of our tilesheet (counted by images)
        private int index = (int)Sprites.RedBox; // default sprite
        private int spriteX; // the X cordinate for the sprite upper left corner when drawing it
        private int spriteY; // the Y cordinate for the sprite upper left corner when drawing it


        // Method // 
        public Box()
        {
            position = new Vector2(600, 750);
        }
        public override void LoadContent(ContentManager content)
        {
            tilesheet = content.Load<Texture2D>("tilesheet");
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
                        
            spriteX = index % tilesheetWidth;
            spriteY = index / tilesheetWidth;
            // create a sourceRectangle 
            sourceRectangle = new Rectangle(spriteX * spriteSize, spriteY * spriteSize, spriteSize, spriteSize);

            // only draw the area within the sourceRectangle
            spriteBatch.Draw(tilesheet, position, sourceRectangle, Color.White);
            base.Draw(spriteBatch);
        }
    }
}

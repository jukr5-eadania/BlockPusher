using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BlockPusher.Tiles
{
    /// <summary>
    /// Door class in charge of opening or closing doors
    /// The class is a child of GameObject
    /// Made by: Julius
    /// </summary>
    internal class Door : GameObject
    {
        Rectangle destinationRectangle;
        Rectangle source;
        private string color;
        public static List<Button> buttons = new();
        private bool active;
        int pixelTileSize = 128;
        int numTilesPerRow = 13;
        int value = 24;
        
        public override Rectangle collisionBox
        {
            get => destinationRectangle;
        }
        public string Color { get => color; set => color = value; }
        public bool Active { get => active; set => active = value; }

        /// <summary>
        /// Constructor used to set the stats of the soor
        /// </summary>
        /// <param name="textureAtlas">The spritesheet of the object</param>
        /// <param name="destinationRectangle">The location of the object</param>
        /// <param name="source">The specific sprite from the spritesheet</param>
        /// <param name="color">The color of the door, Used to figure out which button to interact with</param>
        public Door(Texture2D textureAtlas, Rectangle destinationRectangle, Rectangle source, string color)
        {
            this.textureAtlas = textureAtlas;
            this.destinationRectangle = destinationRectangle;
            this.source = source;
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

        /// <summary>
        /// The main loop of the door
        /// </summary>
        /// <param name="gameTime">Takes a GameTime that provides the timespan since last call to update</param>
        public override void Update(GameTime gameTime)
        {
            DoorState();
        }

        /// <summary>
        /// Checks to see if the button related to the door is active
        /// </summary>
        public void CheckButton()
        {
            active = false;

            foreach (Button button in buttons)
            {
                if (button.Color == color && button.Active)
                {
                    active = true;
                }
            }
        }

        /// <summary>
        /// Is in charge of controlling if the door is open or closed 
        /// </summary>
        public void DoorState()
        {
            CheckButton();

            if (active == true)
            {
                value = 12;
                int x = value % numTilesPerRow;
                int y = value / numTilesPerRow;
                source = new(x * pixelTileSize, y * pixelTileSize, pixelTileSize, pixelTileSize);
                collisionOn = false;
            }
            else
            {
                value = 24;
                int x = value % numTilesPerRow;
                int y = value / numTilesPerRow;
                source = new(x * pixelTileSize, y * pixelTileSize, pixelTileSize, pixelTileSize);
                collisionOn = true;
            }
        }
    }
}

﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BlockPusher
{
    /// <summary>
    /// Button Class that is charge of opening and closing doors based on collision
    /// The class is a child of GameObject
    /// Made by: Julius
    /// </summary>
    internal class Button : GameObject
    {
        Rectangle destinationRectangle;
        Rectangle source;
        public static List<Door> doors = new();
        private string color;
        public override Rectangle collisionBox
        {
            get => destinationRectangle;
        }

        /// <summary>
        /// Constructor used to set the stats of the button
        /// </summary>
        /// <param name="textureAtlas">The spritesheet of the object</param>
        /// <param name="destinationRectangle">The location of the object</param>
        /// <param name="source">The specific sprite from the spritesheet</param>
        /// <param name="color">The color of the button, Used to figure out which door to interact with</param>
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

        /// <summary>
        /// The main loop of the button
        /// </summary>
        /// <param name="gameTime">Takes a GameTime that provides the timespan since last call to update</param>
        public override void Update(GameTime gameTime)
        {
            
        }

        /// <summary>
        /// Only checks when the objects first collide
        /// </summary>
        /// <param name="other">The other gameobjects the button collides with</param>
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

        /// <summary>
        /// Only checks when the objects stop colliding
        /// </summary>
        /// <param name="other">The other gameobjects the button collides with</param>
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

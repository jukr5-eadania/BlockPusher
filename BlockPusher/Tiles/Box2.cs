using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BlockPusher.Tiles
{
    /// <summary>
    /// The box object that the player pushes around.
    /// - Mads
    /// </summary>
    internal class Box2 : GameObject
    {
        private Player player;
        private string boxMoveDirection;
        private bool sliding;
        private bool moving;

        private SoundEffect soundBoxMove;

        public override Rectangle collisionBox
        {
            get => destinationRectangle;
        }

        public Box2(Texture2D textureAtlas, Rectangle destinationRectangle, Rectangle source)
        {
            this.textureAtlas = textureAtlas;
            this.destinationRectangle = destinationRectangle;
            this.source = source;
        }

        public override void LoadContent(ContentManager content)
        {
            soundBoxMove = content.Load<SoundEffect>("push");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureAtlas, destinationRectangle, source, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
        }

        public override void Update(GameTime gameTime)
        {
            // Moves the box if on ice and has been pushed by player
            if (sliding && moving)
            {
                switch (boxMoveDirection)
                {
                    case "left":
                        {
                            destinationRectangle.Location += new Point(-128, 0);
                            break;
                        }
                    case "right":
                        {
                            destinationRectangle.Location += new Point(128, 0);
                            break;
                        }
                    case "down":
                        {
                            destinationRectangle.Location += new Point(0, 128);
                            break;
                        }
                    case "up":
                        {
                            destinationRectangle.Location += new Point(0, -128);
                            break;
                        }
                }
            }
        }

        public override void OnCollision(GameObject other)
        {
            // If colliding with player, moves along with it
            if (other is Player)
            {
                player = (Player)other;
                moving = true;
                boxMoveDirection = other.moveDirection;
                switch (boxMoveDirection)
                {
                    case "left":
                        {
                            destinationRectangle.Location += new Point(-128, 0);
                            break;
                        }
                    case "right":
                        {
                            destinationRectangle.Location += new Point(128, 0);
                            break;
                        }
                    case "down":
                        {
                            destinationRectangle.Location += new Point(0, 128);
                            break;
                        }
                    case "up":
                        {
                            destinationRectangle.Location += new Point(0, -128);
                            break;
                        }
                }

                soundBoxMove.Play();
            }
            // Moves back if colliding with a wall, box or door
            else if (other is Wall || other is Box2 || (other is Door && other.collisionOn))
            {
                sliding = false;
                moving = false;
                switch (boxMoveDirection)
                {
                    case "left":
                        {
                            destinationRectangle.Location += new Point(128, 0);
                            break;
                        }
                    case "right":
                        {
                            destinationRectangle.Location += new Point(-128, 0);
                            break;
                        }
                    case "down":
                        {
                            destinationRectangle.Location += new Point(0, -128);
                            break;
                        }
                    case "up":
                        {
                            destinationRectangle.Location += new Point(0, 128);
                            break;
                        }
                }
                player.BoxCollision();
            }
            // stops sliding if on floor
            else if (other is Floor)
            {
                sliding = false;
            }
            // starts sliding if on ice
            else if (other is Ice)
            {
                sliding = true;
            }
        }
    }
}

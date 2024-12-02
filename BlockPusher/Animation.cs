using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockPusher
{

    internal class Animation
    {
        int numFrames;
        int numColumns;
        int colPosition;
        int rowPosition;
        Vector2 size;
        int counter;
        int activeFrame;
        int interval;



        public Animation(int numFrames, int numColumns, Vector2 size, int colPosition, int rowPosition)
        {
            this.numFrames = numFrames;
            this.numColumns = numColumns;
            this.size = size;

            counter = 0;
            activeFrame = 0;
            interval = 20;
            this.colPosition = colPosition;
            this.rowPosition = rowPosition;
        }

        public void Update()
        {

            counter++;
            if (counter > interval)
            {
                counter = 0;
                NextFrame();
            }
        }

        private void NextFrame()
        {
            activeFrame++;
            colPosition++;
            if (activeFrame >= numFrames)
            {
                ResetAnimation();
            }

            if(colPosition >= numColumns)
            {
                colPosition = 0;
                rowPosition+=2;
            }
        }

        private void ResetAnimation()
        {
            activeFrame = 0;
            colPosition = 0;
            rowPosition = 5;
        }

        public Rectangle GetFrame()
        {
            return new Rectangle(colPosition * (int)size.X, rowPosition * (int)size.Y, (int)size.X, (int)size.Y);
        }
    }
}

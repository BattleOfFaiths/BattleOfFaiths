using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BattleOfFaiths.Game
{
    public class Animation
    {
        private int frameCounter;
        private int switchFrame;
        private bool active;
        private Vector2 position, amountOfFrames, currentFrame;
        private Texture2D Image;
        private Rectangle sourceRect;

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        public Vector2 CurrentFrame
        {
            get { return currentFrame; }
            set { currentFrame = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Texture2D AnimationImage
        {
            set { Image = value; }
        }

        public int FrameWidth => Image.Width / (int) amountOfFrames.X;

        public int FrameHeight => Image.Height / (int) amountOfFrames.Y;

        public void Initialize(Vector2 position, Vector2 Frames)
        {
            active = false;
            switchFrame = 160;
            this.position = position;
            this.amountOfFrames = Frames;
        }

        public void Update(GameTime gameTime)
        {
            if (active)
                frameCounter += (int) gameTime.ElapsedGameTime.TotalMilliseconds;
            else
                frameCounter = 0;
            if (frameCounter >= switchFrame)
            {
                frameCounter = 0;
                currentFrame.X += FrameWidth;
                if (currentFrame.X >= Image.Width)
                {
                    currentFrame.X = 0;
                    active = false;
                }
            }
            sourceRect = new Rectangle((int)currentFrame.X, (int)currentFrame.Y, FrameWidth, FrameHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, position, sourceRect, Color.White);
        }
    }
}

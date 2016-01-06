using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nomnom.GameCode.Graphics
{
    public class FrameCollection
    {
        public struct AnimFrame
        {
            public int[] frame { get; set; }
            public int timer { get; set; }
        }

        List<AnimFrame> frames;
        double currentTimer;
        int currentFrame;

        public FrameCollection()
        {
            frames = new List<AnimFrame>();
            currentTimer = 0;
            currentFrame = 0;
        }

        public void AddFrame(int col, int row, int timer)
        {
            frames.Add(new AnimFrame()
            {
                timer = timer,
                frame = new int[2] { col, row }
            });
            RandomiseFrameTime();
        }

        private void RandomiseFrameTime()
        {
            currentFrame = Program.RNG.Next(frames.Count - 1);
            currentTimer = Program.RNG.Next(frames[currentFrame].timer);
        }

        public void Update(double millisecs)
        {
            double totalTime = currentTimer + millisecs;
            AnimFrame frame = frames.ElementAt(currentFrame);
            if (frame.timer < totalTime)
            {
                if (frames.Count() == currentFrame + 1) currentFrame = 0;
                else currentFrame += 1;
                currentTimer = totalTime - frame.timer;
            }
            else
                currentTimer = totalTime;
        }

        public int GetCurrentRow()
        {
            return frames.ElementAt(currentFrame).frame[1];
        }

        public int GetCurrentCol()
        {
            return frames.ElementAt(currentFrame).frame[0];
        }
    }

    public class SpriteAnimation
    {
        SpriteSheet spriteSheet;
        FrameCollection frames;

        public SpriteAnimation(string name, ContentManager content, int cols, int rows)
        {
            spriteSheet = new SpriteSheet(name);
            spriteSheet.LoadTexture(content, cols, rows);
            frames = new FrameCollection();
        }

        public void AddFrame(int x, int y, int time)
        {
            frames.AddFrame(x, y, time);
        }

        public void Update(double msThisUpdate)
        {
            frames.Update(msThisUpdate);
            spriteSheet.SetCurrentSprite(frames.GetCurrentCol(), frames.GetCurrentRow());
        }

        public void Draw(SpriteBatch sb)
        {
            spriteSheet.Draw(sb);
        }

        public void SetPos(Vector2 v)
        {
            spriteSheet.SetPos(v);
        }

        public float GetY()
        {
            return spriteSheet.GetY();
        }

        public int GetWidth()
        {
            return spriteSheet.GetWidth();
        }

        public int GetHeight()
        {
            return spriteSheet.GetHeight();
        }

        internal void SetScale(float scale)
        {
            spriteSheet.SetScale(scale);
        }

        internal void SetRotation(float rads, Vector2 centerOfRotation)
        {
            spriteSheet.SetRotation(rads, centerOfRotation);
        }

        public void SetEffects(SpriteEffects effect)
        {
            spriteSheet.SetEffects(effect);
        }
    }
}

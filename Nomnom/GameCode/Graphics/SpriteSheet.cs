using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nomnom.GameCode.Graphics
{
    public class SpriteSheet
    {
        public int ID { get; set; }
        public string _contentName { get; set; }
        public int MaxCols { get { return cols; } }
        public int MaxRows { get { return rows; } }
        public int spriteWidth;
        public int spriteHeight;

        Texture2D _texture;
        Vector2 _position;
        Vector2 _offsetVector;
        float _rotation;
        Vector2 _centerOfRotation;
        int cols, rows, startX, startY;
        Vector2 scale;
        SpriteEffects _effects;

        public SpriteSheet(string contentName)
        {
            _contentName = contentName;
            scale = new Vector2(1, 1);
            ID = new Random().Next(100000); // TODO: change this to something unique
        }


        /// <summary>
        /// Load the texture for the SpriteSheet using the given ContentManager.
        /// </summary>
        /// <param name="content">ContentManager used to load textures.</param>
        /// <param name="cols">Number of cols in the SpriteSheet. Not 0 indexed.</param>
        /// <param name="rows">Number of cols in the SpriteSheet. Not 0 indexed.</param>
        public void LoadTexture(ContentManager content, int cols, int rows)
        {
            this.cols = cols;
            this.rows = rows;
            _texture = content.Load<Texture2D>(_contentName);
            spriteWidth = (int)_texture.Width / cols;
            spriteHeight = (int)_texture.Height / rows;
            SetOffsetVector();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _texture,
                _position,
                null,
                new Rectangle(startX, startY, spriteWidth, spriteHeight),
                _centerOfRotation,
                _rotation,
                scale,
                null,
                _effects,
                0f);
        }

        public void SetScale(float scale)
        {
            this.scale = new Vector2(scale, scale);
            SetOffsetVector();
        }

        public void SetCurrentSprite(int x, int y)
        {
            startX = x * spriteWidth;
            startY = y * spriteHeight;
        }

        public void SetPos(Vector2 v)
        {
            _position = v;
        }

        public void SetRotation(float rads, Vector2 centerOfRotation)
        {
            _rotation = rads;
            _centerOfRotation = centerOfRotation;
        }

        public float GetY()
        {
            return _position.Y;
        }

        public int GetWidth()
        {
            return spriteWidth > 0 ? (int)(spriteWidth * scale.X) : 1;
        }

        public int GetHeight()
        {
            return spriteHeight > 0 ? (int)(spriteHeight * scale.Y) : 1;
        }

        public void SetEffects(SpriteEffects effect)
        {
            _effects = effect;
        }

        private void SetOffsetVector()
        {
            _offsetVector = new Vector2(GetWidth() * 0.5f, GetHeight() * 0.5f);
        }
    }
}

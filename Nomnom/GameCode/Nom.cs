using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nomnom.GameCode
{
    class Nom
    {
        Vector2 _position;
        Texture2D _texture;
        Body _body;
        int _width = 16;
        int _height = 16;
        float _speed = 40f / 1000f;
        Vector2 _movementPos;

        #region Encapsulations
        public int Width
        {
            get
            {
                return _width;
            }

            set
            {
                _width = value;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }

            set
            {
                _height = value;
            }
        }
        #endregion

        public Nom(GraphicsDevice graphicsDevice)
        {
            _texture = new Texture2D(graphicsDevice, _width, _height);
            Color[] data = new Color[_width * _height];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            _texture.SetData(data);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position);
        }

        public void Update(GameTime gameTime)
        {
            Vector2 bodyPos = ConvertUnits.ToDisplayUnits(_body.Position);
            Vector2 direction = Vector2.Subtract(_movementPos, bodyPos);
            float length = direction.Length();
            if (length > 0)
            {
                if (length < 1) _movementPos = bodyPos;
                direction = Vector2.Normalize(direction);

                float thisSpeed = _speed * gameTime.ElapsedGameTime.Milliseconds;
                direction = direction * thisSpeed;

                _body.LinearVelocity = direction;
            }
        }

        public void SetPosition(int x, int y)
        {
            _position.X = x;
            _position.Y = y;
        }

        public void SetPosition(Vector2 vec)
        {
            _position = vec;
        }

        public Vector2 GetPosition()
        {
            return _position;
        }

        public void RegisterBody(Body body)
        {
            _body = body;
        }

        public void MoveToPosition(Vector2 pos)
        {
            _movementPos = pos;
        }
    }
}

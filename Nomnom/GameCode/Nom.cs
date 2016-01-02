using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Nomnom.GameCode.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nomnom.GameCode
{
    class Nom
    {
        Vector2 _position;
        Body _body;
        int _width = 32;
        int _height = 32;
        float _speed = 40f / 1000f;
        float _rotationSpeed = 0.5f;
        float _rotation;
        Vector2 _movementPos;
        Vector2 _offsetPos;
        SpriteSheet _spriteSheet;

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

        public Nom(ContentManager content)
        {
            _spriteSheet = new SpriteSheet("nomnom");
            _spriteSheet.LoadTexture(content, 10, 1);
            _spriteSheet.SetCurrentSprite(0, 0);
            _offsetPos = new Vector2(_width * 0.5f, _height * 0.5f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _spriteSheet.SetRotation(_rotation);
            _spriteSheet.SetPos(_position - _offsetPos);
            _spriteSheet.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            HandleAngularVelocity(gameTime.ElapsedGameTime.Milliseconds);
            HandleLinearVelocity(gameTime.ElapsedGameTime.Milliseconds);
        }

        private void HandleAngularVelocity(int milliseconds)
        {
            _body.AngularVelocity -= _body.AngularVelocity * 0.5f;
            if (_body.Rotation < 0) _body.AngularVelocity = _rotationSpeed;
            else if (_body.Rotation > 0) _body.AngularVelocity = -_rotationSpeed;                
        }

        private void HandleLinearVelocity(int milliseconds)
        {
            _body.LinearVelocity -= _body.LinearVelocity * 0.5f;
            Vector2 bodyPos = ConvertUnits.ToDisplayUnits(_body.Position);
            Vector2 direction = Vector2.Subtract(_movementPos, bodyPos);
            float length = direction.Length();
            if (length > 0)
            {
                if (length < 1)
                {
                    _movementPos = bodyPos;
                    return;
                }

                direction = Vector2.Normalize(direction);

                float thisSpeed = _speed * milliseconds;
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

        public void SetRotation(float radians)
        {
            _rotation = radians;
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

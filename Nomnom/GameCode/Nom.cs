using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        Body _body;
        int _width = 16;
        int _height = 16;
        float _speed = 40f / 1000f;
        Vector2 _movementPos;
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _spriteSheet.SetPos(_position);
            _spriteSheet.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
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

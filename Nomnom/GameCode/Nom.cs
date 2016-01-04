using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Nomnom.GameCode.Graphics;
using Nomnom.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nomnom.GameCode
{
    class Nom
    {
        double _movementAngle;
        Vector2 _position;
        Body _body;
        int _width = 16;
        int _height = 32;
        float _speed = 60f / 1000f;
        float _rotationSpeed = 1f;
        float _rotation;
        float _size = 3f;
        Vector2 _movementPos;
        Vector2 _offsetPos;
        SpriteAnimation _spriteAnim;

        #region Encapsulations
        public float Width
        {
            get
            {
                return (_width / 2 - 1)  * _size ;
            }
        }

        public float Height
        {
            get
            {
                return _height * _size;
            }
        }

        public double MovementAngle
        {
            get
            {
                return _movementAngle;
            }

            protected set
            {
                _movementAngle = value;
            }
        }
        #endregion

        public Nom(ContentManager content)
        {
            _spriteAnim = new SpriteAnimation("nomnom", content, 10, 1);
            for (int i = 0; i < 10; i++)
            {
                _spriteAnim.AddFrame(i, 0, 100);
            }
            _offsetPos = new Vector2(_width * 0.5f, _height * 0.5f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _spriteAnim.SetScale(_size);
            _spriteAnim.SetRotation(_rotation);
            _spriteAnim.SetPos(_position - _offsetPos);
            _spriteAnim.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            var msThisUpdate = gameTime.ElapsedGameTime.Milliseconds;
            _spriteAnim.Update(msThisUpdate);
            HandleAngularVelocity();
            HandleLinearVelocity(msThisUpdate);
        }

        private void HandleAngularVelocity()
        {
            if (_body.Rotation < -.8f) _body.Rotation = -0.75f;
            else if (_body.Rotation > .8f) _body.Rotation = 0.75f;
            else if (_body.Rotation < -.2) _body.ApplyTorque(_rotationSpeed);
            else if (_body.Rotation > .2) _body.ApplyTorque(-_rotationSpeed);
        }

        private void HandleMovementAngle(Vector2 direction)
        {
            this.MovementAngle = direction.GetAngle();
        }

        private void HandleLinearVelocity(int milliseconds)
        {
            _body.LinearVelocity -= _body.LinearVelocity * 0.5f;
            Vector2 bodyPos = ConvertUnits.ToDisplayUnits(_body.Position);
            Vector2 direction = Vector2.Subtract(_movementPos, bodyPos);
            HandleMovementAngle(direction);
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

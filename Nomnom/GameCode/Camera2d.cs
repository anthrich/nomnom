using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nomnom.GameCode
{
    public class Camera2d
    {
        protected float _zoom; // Camera Zoom
        public Matrix _transform; // Matrix Transform
        public Vector2 _pos; // Camera Position
        protected float _rotation; // Camera Rotation
        protected GraphicsDevice _graphicsDevice;

        public Camera2d(GraphicsDevice graphicsDevice)
        {
            _zoom = 1.0f;
            _rotation = 0.0f;
            _pos = Vector2.Zero;
            _graphicsDevice = graphicsDevice;
        }

        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; } // Negative zoom will flip image
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        /// <summary>
        /// Move the camera by an amount in a Vector2.
        /// </summary>
        /// <param name="amount"></param>
        public void Move(Vector2 amount)
        {
            _pos += amount;
        }

        public Vector2 Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }

        public Vector2 TopLeftPos
        {
            get { return _pos - CameraProportions * 0.5f; }
        }

        public Vector2 CameraProportions
        {
            get { return new Vector2(_graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height); }
        }

        public Matrix GetTransformation()
        {
            int viewportWidth = _graphicsDevice.Viewport.Width;
            int viewportHeight = _graphicsDevice.Viewport.Height;

            _transform =       // Thanks to o KB o for this solution
              Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(viewportWidth * 0.5f, viewportHeight * 0.5f, 0));
            return _transform;
        }
    }
}

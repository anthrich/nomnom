using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nomnom.ExtensionMethods
{
    public static class Vector2Extensions
    {
        public static double GetAngle(this Vector2 vec)
        {
            var angle = MathHelper.ToDegrees((float)Math.Atan2(vec.X, -vec.Y));
            if (angle < 0) angle = 360 + angle;
            return angle;
        }
    }
}

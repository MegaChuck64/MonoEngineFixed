using System;

using Microsoft.Xna.Framework;

namespace Utilities
{
    public static class Helper
    {

        public static Color RandColor(Random rand)
        {
            return new Color(rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, 256));
        }
    }
}

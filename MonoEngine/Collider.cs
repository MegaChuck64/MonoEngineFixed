using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEngine
{
    class Collider : UpdateableComponent
    {

        public Rectangle collisonRect { get; set; }

        
        public Collider(int width, int height)
        {
            collisonRect = new Rectangle(0, 0, width, height);
        }

        public override void Update(GameTime gameTime)
        {
            collisonRect = new Rectangle((int)owner.transform.Position.X, (int)owner.transform.Position.Y, collisonRect.Size.X, collisonRect.Size.Y);
        }


        public bool isColliding(Collider col)
        {
            if (collisonRect.Intersects(col.collisonRect))
            {
                return true;
            }
            return false;
        }
    }
}

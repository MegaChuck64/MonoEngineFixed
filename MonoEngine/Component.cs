using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoEngine
{
    abstract class Component:GameItem
    {
        public GameObject owner;
    }

    abstract class UpdateableComponent:Component
    {
        public abstract void Update(GameTime gameTime);

    }

    abstract class DrawableComponent:UpdateableComponent
    {
        public abstract void Draw();

    }
}

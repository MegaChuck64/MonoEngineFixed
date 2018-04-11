using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MonoEngine
{
    class GameObjectScript : GameItem
    {


        public GameObject gameObject;
        public List<Component> components = new List<Component>();
        public Transform transform { get { return gameObject.transform; } set { gameObject.transform = value; } }

        public float deltaTime { get; private set; }

        List<Collider> colliders = new List<Collider>();

        public void AddComponent(Component component)
        {
            component.owner = gameObject;
            components.Add(component);

            if (component.GetType() == typeof(Collider))
            {
                colliders.Add((Collider)component);
            }
        }

        public Component GetComponent<T>(T componentType)
        {
            foreach (Component c in components)
            {
                if (c.GetType() == componentType.GetType())
                {
                    return c;
                }
            }

            return null;
        }


        public GameTime gameTime = new GameTime();


        public void OnStart()
        {

            gameObject = new GameObject("New GameObject");

            System.Console.WriteLine("OnStart called in G-O-S");
        }


        public void OnUpdate(GameTime _gameTime)
        {
            gameTime = _gameTime;

            if (gameObject.isEnabled)
            {
                foreach (UpdateableComponent c in components.OfType<UpdateableComponent>())
                {
                    c.Update(gameTime);
                }

                for (int i = 0; i < colliders.Count; i++)
                {
                    for (int j = i + 1; j < colliders.Count; j++)
                    {
                        if (colliders[i].isColliding(colliders[j]))
                        {
                            OnCollision(colliders[j]);
                        }
                    }
                }
            }

            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        }

        public void OnDraw()
        {
            if (gameObject.isEnabled)
            {
                foreach (DrawableComponent c in components.OfType<DrawableComponent>())
                {
                    c.Draw();
                }
            }
        }

        public void OnCollision(Collider other)
        {

        }

    }
}

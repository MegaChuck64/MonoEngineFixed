using System.Collections.Generic;

using static Utilities.Structures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEngine
{
    class GameObject
    {

        public string name { get; protected set; }

        public bool isEnabled = true;

        public Transform transform;

        public GameObject(string _name)
        {
            name = _name;
            transform = new Transform(Vector2.Zero);
        }

        public GameObject(string _name, Vector2 position)
        {
            name = _name;
            transform = new Transform(position);

        }

 
       


    }
}

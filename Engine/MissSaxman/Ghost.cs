using Microsoft.Xna.Framework;
using System;



namespace MonoEngine.Games
{
    class Ghost:GameObject
    {

        public Ghost(string _name) : base(_name)
        {

        }

        public Ghost(string _name, Vector2 _position) : base(_name, _position)
        {

        }





        public void Start(Sprite sprite)
        {
         //   AddComponent(new Renderer(sprite, false, 15));
        }

        public void Update()
        {

        }

        public void Draw()
        {

        }

 

    }
}

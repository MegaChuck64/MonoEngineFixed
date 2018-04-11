using Utilities;
using Microsoft.Xna.Framework;

namespace MonoEngine.TestGame
{
    class Player:GameObjectScript
    {

        float speed = 100;

        private void Start()
        {
            AddComponent(new Renderer(new Sprite(32, 32, Color.Yellow, game.GraphicsDevice)));
            AddComponent(new Collider(33, 33));
        }

        Vector2 movement;


        public void Update()
        {
            movement.X = Input.GetAxis("Horizontal");

            movement.Y = Input.GetAxis("Vertical");

            transform.Move(movement * speed * deltaTime);
        }
    }
}

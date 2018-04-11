
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Utilities;
using static Utilities.Structures;

namespace MonoEngine.MissSaxman
{
    class MissSaxman : GameObject
    {
        public MissSaxman(string _name) : base(_name)
        {
        }

        public MissSaxman(string _name, Vector2 _position) : base(_name, _position)
        {
        }

        int radius;
        Color col;
        Texture2D texture;
        int speed;

        GraphicsDevice graphics;



        public void Start(int _radius, int _speed, Color _col, GraphicsDevice _graphics)
        {
            radius = _radius;
            speed = _speed;
            col = _col;
            graphics = _graphics;
            CreateTexture();
           //AddComponent(new Renderer(new Sprite(texture, 1, texture.Width)));
        }

        void CreateTexture()
        {
            Vector2 center = new Vector2(radius, radius);

            texture = new Texture2D(graphics, 2 * radius, 2 * radius);

            Color[] cols = new Color[(2*radius)*(2*radius)];
            for (int x = 0; x < 2*radius; x++)
            {
                for (int y = 0; y < 2*radius; y++)
                {
                    if (Vector2.Distance(new Vector2(x,y), center) <= radius)
                    {
                        cols[x * (2 * radius) + y] = col;
                    }
                }
            }

            texture.SetData<Color>(cols);
        }



        //Vector2 movement = Vector2.Zero;
        //public override void Update(GameTime gameTime)
        //{


        //    if (Input.keyboard.IsKeyDown(Keys.W))
        //    {

        //        movement.Y = -speed;
  
        //    }
        //    else if (Input.keyboard.IsKeyDown(Keys.S))
        //    {
        //            movement.Y = speed;
        //    }
        //    else
        //    {
        //            movement.Y = 0;
        //    }

        //    if (Input.keyboard.IsKeyDown(Keys.D))
        //    {
        //            movement.X = speed;
        //    }
        //    else if (Input.keyboard.IsKeyDown(Keys.A))
        //    {
        //            movement.X = -speed;
        //    }
        //    else
        //    {
        //        movement.X = 0;
        //    }


            

        //    transform.Move(movement);

        //    base.Update(gameTime);
        //}



    }
}

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Utilities.Structures;
using Utilities;
using Microsoft.Xna.Framework.Input;

namespace MonoEngine.CellularA
{
    class LangtonsAnt
    {
        Cell<AntState>[,] cells;

        int width, height;

        int scale;

        Texture2D texture;

        GraphicsDevice graphics;

        Random rand;

        SpriteFont font;

        bool isPlaying = true;

        int stepsPerFrame = 1;

        Color[] colorStates =
            {
                Color.Black,        //ALIVE
                Color.DeepPink,     //Hungry
                Color.PaleGreen,    //Sick
                Color.White,        //DEAD
            };


        int tickCount = 0;

        enum AntState : int
        {
            ALIVE,
            HUNGRY,
            SICK,
            DEAD,
        }

        struct Ant
        {
            public Direction dir;
            public Point pos;
            public Color col;
        }

        Ant[] ants = new Ant[3];

        public void Start(int _width, int _height, int _scale, Random _rand, SpriteFont _font, GraphicsDevice _graphics)
        {
            width = _width;
            height = _height;

            scale = _scale;

            rand = _rand;

            font = _font;

            graphics = _graphics;



            for (int a = 0; a < ants.Length; a++)
            {
                ants[a].pos = new Point(rand.Next(5, 95), rand.Next(5, 95));

                ants[a].dir = (Direction)rand.Next(0, 4);

                ants[a].col = Helper.RandColor(rand);

            }

            for (int i = 0; i < colorStates.Length; i++)
            {
                colorStates[i] = Helper.RandColor(rand);
            }


            tickCount = 0;
            InitTexture();
            InitCells();
        }



        void InitTexture()
        {
            Color[] cols = new Color[scale * scale];

            for (int x = 0; x < scale; x++)
            {
                for (int y = 0; y < scale; y++)
                {
                    if (x == 0 || x == scale - 1 || y == 0 || y == scale - 1)
                    {
                        cols[x * scale + y] = Color.LightSalmon;
                        cols[x * scale + y].A -= 100;
                    }
                    else
                    {
                        cols[x * scale + y] = Color.White;
                    }
                }
            }


            texture = new Texture2D(graphics, scale, scale);

            texture.SetData<Color>(cols);
        }

        void InitCells()
        {
            cells = new Cell<AntState>[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int i = 0; i < ants.Length; i++)
                    {

                        if (x == ants[i].pos.X && y == ants[i].pos.Y)
                        {
                            cells[x, y] = new Cell<AntState>(AntState.ALIVE);
                        }
                        else
                        {
                            cells[x, y] = new Cell<AntState>(AntState.DEAD);
                        }
                    }

                }
            }
        }


        void ChangeState(AntState _state, int x, int y)
        {
            switch (_state)
            {
                case AntState.ALIVE:
                    cells[x, y].state = AntState.HUNGRY;
                    break;

                case AntState.HUNGRY:
                    cells[x, y].state = AntState.SICK;
                    break;

                case AntState.SICK:
                    cells[x, y].state = AntState.DEAD;
                    break;

                case AntState.DEAD:
                    cells[x, y].state = AntState.ALIVE;
                    break;
            }
        }


        void Turn(AntState _state, int antNdx)
        {
            switch (_state)
            {
                case AntState.ALIVE:
                    //((index % n) + n) % n;
                    ants[antNdx].dir = (Direction)((int)ants[antNdx].dir + (int)Direction.Left);
                    ants[antNdx].dir = (Direction)((((int)ants[antNdx].dir % 4) + 4) % 4);
                    break;

                case AntState.HUNGRY:
                    ants[antNdx].dir = (Direction)((int)ants[antNdx].dir + (int)Direction.Up);
                    ants[antNdx].dir = (Direction)((((int)ants[antNdx].dir % 4) + 4) % 4);
                    break;

                case AntState.SICK:
                    ants[antNdx].dir = (Direction)((int)ants[antNdx].dir + (int)Direction.Down);
                    ants[antNdx].dir = (Direction)((((int)ants[antNdx].dir % 4) + 4) % 4);
                    break;

                case AntState.DEAD:
                    ants[antNdx].dir = (Direction)((int)ants[antNdx].dir + (int)Direction.Right);
                    ants[antNdx].dir = (Direction)((((int)ants[antNdx].dir % 4) + 4) % 4);
                    break;

            }
        }


        void Progress()
        {

            tickCount++;
            for (int i = 0; i < ants.Length; i++)
            {
                switch (ants[i].dir)
                {
                    case Direction.Up:

                        //flip color of current square
                        ChangeState(cells[(int)ants[i].pos.X, (int)ants[i].pos.Y].state, (int)ants[i].pos.X, (int)ants[i].pos.Y);

                        if (ants[i].pos.Y - 1 >= 0)
                        {

                            //change direction facing

                            Turn(cells[(int)ants[i].pos.X, (int)ants[i].pos.Y - 1].state, i);

                            //move in the direction was facing from last step
                            ants[i].pos.Y--;

                        }
                        else
                        {
                            //change direction facing
                            Turn(cells[(int)ants[i].pos.X, height - 1].state, i);

                            //move in the direction was facing from last step
                            ants[i].pos.Y = height - 1;
                        }
                        break;

                    case Direction.Right:
                        //flip color of current square
                        ChangeState(cells[(int)ants[i].pos.X, (int)ants[i].pos.Y].state, (int)ants[i].pos.X, (int)ants[i].pos.Y);

                        if (ants[i].pos.X + 1 <= width - 1)
                        {

                            //change direction facing

                            Turn(cells[(int)ants[i].pos.X + 1, (int)ants[i].pos.Y].state, i);

                            //move in the direction was facing from last step
                            ants[i].pos.X++;

                        }
                        else
                        {
                            //change direction facing
                            Turn(cells[0, (int)ants[i].pos.Y].state, i);

                            //move in the direction was facing from last step
                            ants[i].pos.X = 0;
                        }
                        break;

                    case Direction.Down:
                        ChangeState(cells[(int)ants[i].pos.X, (int)ants[i].pos.Y].state, (int)ants[i].pos.X, (int)ants[i].pos.Y);

                        if (ants[i].pos.Y + 1 <= height - 1)
                        {

                            //change direction facing

                            Turn(cells[(int)ants[i].pos.X, (int)ants[i].pos.Y + 1].state, i);

                            //move in the direction was facing from last step
                            ants[i].pos.Y++;

                        }
                        else
                        {
                            //change direction facing
                            Turn(cells[(int)ants[i].pos.X, 0].state, i);

                            //move in the direction was facing from last step
                            ants[i].pos.Y = 0;
                        }
                        break;

                    case Direction.Left:

                        //flip color of current square
                        ChangeState(cells[(int)ants[i].pos.X, (int)ants[i].pos.Y].state, (int)ants[i].pos.X, (int)ants[i].pos.Y);

                        if (ants[i].pos.X - 1 >= 0)
                        {

                            //change direction facing

                            Turn(cells[(int)ants[i].pos.X - 1, (int)ants[i].pos.Y].state, i);

                            //move in the direction was facing from last step
                            ants[i].pos.X--;

                        }
                        else
                        {
                            //change direction facing
                            Turn(cells[width - 1, (int)ants[i].pos.Y].state, i);

                            //move in the direction was facing from last step
                            ants[i].pos.X = width - 1;
                        }
                        break;
                }
            }


        }

        float elapsed;
        float timer = 0;

        public void Update(GameTime gameTime)
        {

            elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;


            if (Input.wasPressed(Keys.Space))
            {
                isPlaying = !isPlaying;
            }

            if (Input.wasPressed(Keys.Up))
            {
                if (stepsPerFrame < 1000)
                    stepsPerFrame += 10;
                else
                    stepsPerFrame = 10;
            }
            if (Input.wasPressed(Keys.Down))
            {
                if (stepsPerFrame > 10)
                    stepsPerFrame -= 10;
                else
                    stepsPerFrame = 1000;
            }

            if (Input.wasPressed(Keys.R))
            {
                Start(width, height, scale, rand, font, graphics);
            }

            timer += elapsed;
            if (timer > (rand.Next(15, 60)))
            {
                Start(width, height, scale, rand, font, graphics);
                timer = 0;
            }

            if (isPlaying)
            {
                for (int i = 0; i < stepsPerFrame; i++)
                {
                    Progress();
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            int aliveCount = 0;
            Color col = Color.White;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (cells[x, y].state != AntState.DEAD)
                    {
                        aliveCount++;
                    }


                    col = colorStates[(int)cells[x, y].state];

                    for (int a = 0; a < ants.Length; a++)
                    {
                        if (x == ants[a].pos.X && y == ants[a].pos.Y)
                        {
                            col = ants[a].col;
                        }
                    }




                    sb.Draw(texture, new Vector2(x * scale + 8, y * scale + 24), col);

                }
            }

            sb.DrawString(font, "Alive: " + aliveCount, new Vector2(300, 2), Color.Black);
            sb.DrawString(font, "Ticks Per Frame: " + stepsPerFrame, new Vector2(480, 2), Color.Black);
            sb.DrawString(font, "Ticks: " + tickCount, new Vector2(2, 2), Color.Black);

        }
    }
}

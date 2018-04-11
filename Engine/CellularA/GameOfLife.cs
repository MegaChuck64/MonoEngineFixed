using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Utilities;
using System;
using static Utilities.Structures;

namespace MonoEngine.CellularA
{
    class GameOfLife
    {


        public enum CellState
        {
            Alive, Dead
        }

        Cell<CellState>[,] cells;


        int fps;

        bool isPlaying = true;

        int width, height;

        Texture2D texture;


        GraphicsDevice graphics;

        Random rand;

        float aliveChance = 0;

        int pixelSize = 4;

        int offset = 1;


        Vector2 driver = Vector2.Zero;

        Color onColor;
        Color offColor;


        /// <param name="aliveChance"> 0.0f through 1.0f </param>
        public void Start(int _width, int _height, int framesPerSecond, int _pixelSize, float _aliveChance, Random _rand, GraphicsDevice _graphics)
        {

            graphics = _graphics;

            rand = _rand;

            pixelSize = _pixelSize;

            aliveChance = _aliveChance;

            width = _width;
            height = _height;

            cells = new Cell<CellState>[width, height];

            fps = framesPerSecond;
            timer = 1 / fps;


            onColor = Helper.RandColor(rand);
            offColor = Helper.RandColor(rand);

            CreateTexture();
            InitCells();
        }


        void CreateTexture()
        {
            Color[] white = new Color[pixelSize * pixelSize];
            for (int x = 0; x < pixelSize; x++)
            {
                for (int y = 0; y < pixelSize; y++)
                {
                    white[x * pixelSize + y] = Color.White;
                }

            }

            texture = new Texture2D(graphics, pixelSize, pixelSize);
            texture.SetData<Color>(white);

        }

        void InitCells()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (rand.NextDouble() <= aliveChance)
                    {
                        cells[x, y].state = CellState.Alive;
                    }
                    else
                    {
                        cells[x, y].state = CellState.Dead;
                    }
                }
            }
        }

        public void CheckNeighbors(int x, int y)
        {
            ////if this cell is dead, get outta here!
            //if (cell.state == CellState.Dead)
            //{
            //    return;
            //}


            int neighborCount = 0;


            //starting at the top left corner adjacent to the given cell
            //looping through to bottom right corner adjacent to the given cell 
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {

                    //checking if we are in bounds
                    if ((i + x >= 0 && y + j >= 0) && (x + i < width && y + j < height))
                    {
                        if(x == driver.X && y == driver.Y)
                        {
                            cells[x + i, y + j].state = CellState.Dead;
                            neighborCount++;
                        }
                        else if (cells[x + i, y + j].state == CellState.Alive)
                        {
                            neighborCount++;
                        }
                    }
                }
            }





            neighborCount -= (cells[x, y].state == CellState.Alive) ? 1 : 0;

            //////////////////////////////////////////////////////////////////////////
            ///
            ///                             Rule 1:
            ///     If an alive cell has less than two alive neighbors, die from loneliness
            ///    
            ///                             Rule 2:
            ///     If an alive cell has more than three alive neighbors, die from overpopulation
            ///   
            ///                             Rule 3:
            ///     If an alive cell has 2 or 3 neighbors, every things good       
            ///     
            ///                             Rule 4:
            ///     If a dead cell has 3 alive neighbors, it becomes alive 
            ///
            //////////////////////////////////////////////////////////////////////////


            CellState lastState = cells[x, y].state;

            if (cells[x, y].state == CellState.Alive)
            {

                if (neighborCount < 2)  //Rule 1
                {
                    cells[x, y].state = CellState.Dead;
                }
                else if (neighborCount > 4)
                {
                    cells[x, y].state = CellState.Dead;
                }

            }
            else if (cells[x, y].state == CellState.Dead)
            {
                if (neighborCount == 3)
                {
                    cells[x, y].state = CellState.Alive;
                }

            }

            if (cells[x, y].state == lastState)
            {
                cells[x, y].ticksOld++;
            }
            else
            {
                cells[x, y].ticksOld = 0;
            }



        }

        void Progress()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    CheckNeighbors(x, y);
                }
            }
        }

        float elapsed;
        float timer = 0;

        public void Update(GameTime gameTime)
        {
            elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;


            if (Input.wasPressed(Keys.Up))
            {
                fps += 4;
            }


            if (Input.wasPressed(Keys.Down))
            {
                fps -= 4;
            }

            if (Input.wasPressed(Keys.Space))
            {
                isPlaying = !isPlaying;
            }
            if (Input.mouse.LeftButton == ButtonState.Pressed)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Rectangle cellRect = new Rectangle(x * pixelSize + (offset * pixelSize), y * pixelSize + (offset * pixelSize), pixelSize, pixelSize);
                        if (cellRect.Contains(Input.mouse.X, Input.mouse.Y) && !cellRect.Contains(Input.lastMouse.X, Input.lastMouse.Y))
                        {
                           
                            cells[x, y].state = (cells[x, y].state == CellState.Alive) ? CellState.Dead : CellState.Alive;
                        }
                    }

                }
            }

            if (isPlaying)
            {
                timer -= elapsed;
                if (timer <= 0)
                {
                    Progress();
                    UpdateDrive();
                    timer = 1 / (float)fps;
                }
            }
        }

        int decissionClock = 0;

        float decissionTimer(int min, int max)
        {
            return rand.Next(min, max);
        }

        Direction driverDir = Direction.Up;

        void UpdateDrive()
        {
            decissionClock++;

            if (decissionClock >= decissionTimer(60, 360))
            {
                decissionClock = 0;

                onColor = Helper.RandColor(rand);
                offColor = Helper.RandColor(rand);

                int ranDir = rand.Next(0, 4);
                switch (ranDir)
                {
                    case 0:
                        driverDir = Direction.Up;
                        break;

                    case 1:
                        driverDir = Direction.Right;
                        break;

                    case 2:
                        driverDir = Direction.Down;
                        break;

                    case 3:
                        driverDir = Direction.Left;
                        break;
                }
            }

            switch (driverDir)
            {
                case Direction.Up:
                    driver += new Vector2(0, -1);
                    break;
                case Direction.Right:
                    driver += new Vector2(1, 0);
                    break;
                case Direction.Down:
                    driver += new Vector2(0, 1);
                    break;
                case Direction.Left:
                    driver += new Vector2(-1, 0);
                    break;
            }

            if (driver.X < 0)
            {
                driver.X = width - 1;
            }
            if (driver.Y < 0)
            {
                driver.Y = height - 1;
            }
            if (driver.Y > height -1)
            {
                driver.Y = 0;
            }
            if (driver.X > width - 1)
            {
                driver.X = 0;
            }

            if (cells[(int)driver.X,(int)driver.Y].state == CellState.Dead)
            {
                cells[(int)driver.X, (int)driver.Y].state = CellState.Alive;
                cells[(int)driver.X, (int)driver.Y].ticksOld = 0;
            }
        }

        int randOldness = 0;
        public void Draw(SpriteBatch spriteBatch)
        {

            Color col;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {


                    col = (cells[x, y].state == CellState.Alive) ? onColor : offColor;

                    col = new Color(col.R, col.G, col.B, 255 - cells[x, y].ticksOld);

                    if (isPlaying)
                    {
                        randOldness = rand.Next(170, 350);
                        if (cells[x, y].ticksOld >= randOldness)
                        {


                            cells[x, y].ticksOld = 0;
                            cells[x, y].state = CellState.Dead;

                        }

                    }
                    if (x == driver.X && y == driver.Y)
                    {
                        col = Color.LawnGreen;
                    }

                    spriteBatch.Draw(texture, new Vector2(x * pixelSize + (offset * pixelSize), y * pixelSize + (offset * pixelSize)), col);
                }
            }
        }
    }
}

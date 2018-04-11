
namespace MonoEngine.CellularA
{
    struct RatSimulator
    {
    }
}

//        int width, height;

//        Texture2D texture;

//        Point cellSize;

//        GraphicsDevice graphics;

//        Point offset = new Point(8, 24);

//        Random rand;

//        struct Rat
//        {
//            byte hunger;
//            byte boredom;
//            byte horniness;
//            bool sex;

//            Direction direction;

//            Point position;
//        }

//        Color[] colorStates =
//        {
//            Color.LightSkyBlue,          //Content
//            Color.PaleVioletRed,         //Hungry
//            Color.PaleTurquoise,         //Horny
//            Color.MediumSpringGreen,     //Bored
//            Color.DarkSlateGray,         //Dead
//            Color.White                  //Empty
//        };
//        enum CellState
//        {
//            Content, 
//            Hungry,
//            Horny,
//            Bored,
//            Dead,
//            Empty
//        }

//        Cell<CellState>[,] cells;


//        public void Start(int _width, int _height, Point _cellSize, Random _rand,GraphicsDevice _graphics)
//        {
//            width = _width;
//            height = _height;
//            cellSize = _cellSize;
//            graphics = _graphics;
//            rand = _rand;

//            InitTexture();
//            InitCells();

//        }

//        void InitTexture()
//        {
//            Color[] cols = new Color[cellSize.X * cellSize.Y];

//            for (int x = 0; x < cellSize.X; x++)
//            {
//                for (int y = 0; y < cellSize.Y; y++)
//                {
//                    if (x == 0 || x == cellSize.X - 1 || y == 0 || y == cellSize.Y- 1)
//                    {
//                        cols[x * cellSize.X + y] = Color.LightSalmon;
//                        cols[x * cellSize.X + y].A -= 100;
//                    }
//                    else
//                    {
//                        cols[x * cellSize.X + y] = Color.White;
//                    }
//                }
//            }


//            texture = new Texture2D(graphics, cellSize.X, cellSize.Y);

//            texture.SetData<Color>(cols);
//        }

//        void InitCells()
//        {

//            cells = new Cell<CellState>[width, height];

//            for (int x = 0; x < width; x++)
//            {
//                for (int y = 0; y < height; y++)
//                {
//                    cells[x, y] = new Cell<CellState>((CellState)rand.Next(0, 6));
//                }
//            }

//        }


//        void Progress()
//        {
//            //for (int r = 0; r < rats.Lenght; r++)
//            //{

//            //}
//        }

//        public void Update(GameTime gameTime)
//        {

//        }

//        public void Draw(SpriteBatch sb)
//        {
//            for (int x = 0; x < width; x++)
//            {
//                for (int y = 0; y < height; y++)
//                {
//                    sb.Draw(texture, new Vector2(x * cellSize.X + offset.X, y * cellSize.Y + offset.Y), colorStates[(int)cells[x, y].state]);
//                }
//            }
//        }
//    }
//}

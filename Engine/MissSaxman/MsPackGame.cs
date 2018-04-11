
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Utilities.Structures;

namespace MonoEngine.MissSaxman
{
    public class MsPackGame
    {
        int width, height;

        int tileSize;

        Texture2D tileTexture;

        GraphicsDevice graphics;

        enum TileState
        {
            Wall,
            Empty,

        }

        struct Tile
        {
            public TileState state;

            public Tile(TileState _state)
            {
                state = _state;
            }
        }



        Tile[,] tiles;

        Vector2 offset = new Vector2(8, 24);


        MissSaxman msPacman = new MissSaxman("MissPM", new Vector2(100,100));

        public void Start(int _width, int _height, int _tileSize, GraphicsDevice _graphics)
        {
            width = _width;
            height = _height;
            graphics = _graphics;
            tileSize = _tileSize;


            msPacman.Start(16, 128, Color.Yellow, graphics);

            InitTexture();
            InitBoard();
        }

        void InitTexture()
        {
            Color[] cols = new Color[tileSize * tileSize];

            for (int x = 0; x < tileSize; x++)
            {
                for (int y = 0; y < tileSize; y++)
                {
                    if (x == 0 || x == tileSize - 1 || y == 0 || y == tileSize - 1)
                    {
                        cols[x * tileSize + y] = Color.LightSalmon;
                        cols[x * tileSize + y].A -= 100;
                    }
                    else
                    {
                        cols[x * tileSize + y] = Color.White;
                    }
                }
            }


            tileTexture = new Texture2D(graphics, tileSize, tileSize);

            tileTexture.SetData<Color>(cols);
        }


        void InitBoard()
        {
            tiles = new Tile[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    {
                        tiles[x, y].state = TileState.Wall;
                    }
                    else
                    {
                        tiles[x, y].state = TileState.Empty;
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
          //  msPacman.collision = IsWallAhead();
          //  msPacman.Update(gameTime);
        }

        public void Draw(SpriteBatch sb)
        {

            Color col = Color.Black;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    switch (tiles[x,y].state)
                    {
                        case TileState.Empty:
                            col = Color.LightBlue;
                            break;
                        case TileState.Wall:
                            col = Color.DarkOrange;
                            break;
                    }

                    sb.Draw(tileTexture, new Vector2(x * tileSize + offset.X, y * tileSize + offset.Y), col);
                }
            }

//            msPacman.Draw(sb);


        }
    }
}

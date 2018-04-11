using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace MonoEngine
{
    class Sprite
    {
        public Texture2D texture;
        public int frameCount;
        public int frameWidth;

        public int currentFrame = 0;

        public Color tint = Color.White;


        public Sprite(Texture2D _texture, int _frameCount, int _frameWidth)
        {
            texture = _texture;
            frameCount = _frameCount;
            frameWidth = _frameWidth;
        }

        /// <summary>
        /// For just single color images.
        /// </summary>
        public Sprite(int width, int height,Color color, GraphicsDevice graphics)
        {
            Texture2D text = new Texture2D(graphics, width, height);
            Color[] cols = new Color[width*height];
            for (int i = 0; i < height * width; i++)
            {
                cols[i] = color;
            }
            text.SetData<Color>(cols);

            texture = text;
            frameCount = 1;
            frameWidth = texture.Width;
            
           
        }




    }
}

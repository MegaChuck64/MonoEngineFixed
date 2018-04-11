using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEngine
{
    class Renderer:DrawableComponent
    {
        public Sprite sprite;

        public bool isPlaying;

        public int framesPerSecond;

        public Renderer(Sprite _sprite, bool _isPlaying = true, int _fps = 15)
        {
            sprite = _sprite;
            isPlaying = _isPlaying;
            framesPerSecond = _fps;
        }

        public override void Update(GameTime gameTime)
        {
            if (isPlaying)
            {
                Play(framesPerSecond, gameTime);
            }
        }

        public override void Draw()
        {
            game.spriteBatch.Draw(sprite.texture, owner.transform.Position, new Rectangle(sprite.currentFrame * sprite.frameWidth, 0, sprite.frameWidth, sprite.texture.Height), sprite.tint, owner.transform.Rotation, new Vector2(sprite.frameWidth / 2, sprite.texture.Height / 2), owner.transform.Scale, SpriteEffects.None, 0);
        }

        float animationTimer = 0;

        void Play(int fps, GameTime gameTime)
        {
            if (fps > 0)
            {
                animationTimer += gameTime.ElapsedGameTime.Milliseconds;

                if (animationTimer > 1000 / fps)
                {
                    if (sprite.currentFrame < sprite.frameCount - 1)
                    {
                        sprite.currentFrame++;
                    }
                    else
                    {
                        sprite.currentFrame = 0;
                    }

                    animationTimer = 0f;
                }
            }
        }
    }
}

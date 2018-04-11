using Microsoft.Xna.Framework;
using static Utilities.Structures;


namespace MonoEngine
{
    class Transform
    {
        protected Vector2 Up = new Vector2(0, -1);
        protected Vector2 Down = new Vector2(0, 1);
        protected Vector2 Left = new Vector2(0, 1);
        protected Vector2 Right = new Vector2(0, 1);

        //protected GameObject owner { get; set; }

        public Vector2 Position { get; protected set; }

        public float Rotation { get; protected set; }

        public Vector2 Scale { get; protected set; }


        public Transform(Vector2 _position)
        {
            Position = _position;
            Rotation = 0;
            Scale = new Vector2(1, 1);
        }

        public Transform(Vector2 _position, float _rotation, Vector2 _scale)
        {
            Position = _position;
            Rotation = _rotation;
            Scale = _scale;
        }

        public void Move(Direction direction, float pixelsPerSecond)
        {
            switch (direction)
            {
                case Direction.Up:
                    Position += Up;
                    break;
                case Direction.Right:
                    Position += Right;
                    break;
                case Direction.Down:
                    Position += Down;
                    break;
                case Direction.Left:
                    Position += Left;
                    break;
            }

            Position *= pixelsPerSecond;
        }

        public void Move(Vector2 pixelsPerSecond)
        {
            Position += pixelsPerSecond;
        }

        public void Rotate(Direction direction, float degreesPerSecond)
        {

            switch (direction)
            {
                case Direction.Clockwise:
                    Rotation += MathHelper.ToRadians(degreesPerSecond);
                    break;
                case Direction.CounterClockwise:
                    Rotation -= MathHelper.ToRadians(degreesPerSecond);
                    break;
            }

        }

        public void Rotate(float degreesPerSecond)
        {
            Rotation += MathHelper.ToRadians(degreesPerSecond);
        }


    }
}

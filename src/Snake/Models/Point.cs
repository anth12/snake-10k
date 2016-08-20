using System;
using Snake.Models.Enum;

namespace Snake.Models
{
    public struct Point
    {
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X;
        public int Y;

        public bool IsCollision(Point point)
        {
            return this.X == point.X
                && this.Y == point.Y;
        }

        public Point Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Point(this.X, this.Y - 1);
                case Direction.Down:
                    return new Point(this.X, this.Y + 1);

                case Direction.Left:
                    return new Point(this.X - 1, this.Y);
                case Direction.Right:
                    return new Point(this.X + 1, this.Y);
            }

            throw new Exception($"Unrecognised direction {direction}");
        }

        public Point MoveReverse(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return this.Move(Direction.Down);
                case Direction.Down:
                    return this.Move(Direction.Up);

                case Direction.Left:
                    return this.Move(Direction.Right);
                case Direction.Right:
                    return this.Move(Direction.Left);
            }

            throw new Exception($"Unrecognised direction {direction}");
        }

        public Point Constrain(int maxX, int maxY)
        {

            if (this.X < 0)
            {
                this.X = maxX + (this.X % maxX);
            }
            if (this.X > maxX)
            {
                this.X = this.X % maxX;
            }

            if (this.Y < 0)
            {
                this.Y = maxY + (this.Y % maxY);
            }
            if (this.Y > maxY)
            {
                this.Y = this.Y % maxY;
            }

            return this;
        }
    }
}

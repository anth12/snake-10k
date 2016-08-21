using System.Collections.Generic;
using Snake.Models.Enum;

namespace Snake.Models
{
    public class Snake
    {
        public Point HeadPoint;
        public List<Point> Body { get; set; } = new List<Point>();
        public Direction Direction { get; set; }
        
        public List<Direction> DirectionQueue { get; set; } = new List<Direction>();
        public int Length { get; set; } = 4;
    }
}

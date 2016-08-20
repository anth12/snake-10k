using System;
using System.Collections.Generic;
using Snake.Models;

namespace Snake.Sockets.ServerDto
{
    public class SnakeDto
    {
        // TODO change GUID to smaller value
        public Guid Id { get; set; }
        public Point HeadPoint { get; set; }
        public List<Point> Body { get; set; } = new List<Point>();
    }
}


using System;

namespace Snake.Models
{
    public class GameBoard
    {
        public Guid Id { get; set; } = new Guid();
        public bool IsActive { get; set; } = true;
        public int Width { get; set; }
        public int Height { get; set; }
        public int Velocity { get; set; }

        public int MaxUsers { get; set; } = 350;
    }
}

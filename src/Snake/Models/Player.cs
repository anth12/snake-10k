using System;

namespace Snake.Models
{
    public class Player
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid Token { get; set; } = new Guid();
        public string Username { get; set; }
        public Snake Snake { get; set; }
        public int HighScore { get; set; }
    }
}

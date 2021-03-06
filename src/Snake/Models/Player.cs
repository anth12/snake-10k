﻿using System;

namespace Snake.Models
{
    public class Player
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid Token { get; set; } = Guid.NewGuid();
        public bool IsPlaying { get; set; }
        public string Username { get; set; }
        public Snake Snake { get; set; }
        public int HighScore { get; set; }
    }
}

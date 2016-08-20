using System;
using Snake.Models;

namespace Snake.Sockets.ServerDto
{
    public class PlayerDto : BaseServerDto
    {
        public PlayerDto(Player user)
        {
            Id = user.Id;
            Token = user.Token;
            Username = user.Username;
            Snake = user.Snake;
            HighScore = user.HighScore;
        }

        public Guid Id { get; set; }
        public Guid Token { get; set; }
        public string Username { get; set; }
        public Models.Snake Snake { get; set; }
        public int HighScore { get; set; }
    }
}

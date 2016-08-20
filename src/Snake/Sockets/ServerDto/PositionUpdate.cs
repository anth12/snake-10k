using System.Collections.Generic;

namespace Snake.Sockets.ServerDto
{
    public class PositionUpdate : BaseServerDto
    {
        public List<SnakeDto> Snakes { get; set; }
    }
}

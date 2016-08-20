using System.Collections.Generic;
using Snake.Sockets.Attributes;

namespace Snake.Sockets.ServerDto
{
    [SocketCode("P")]
    public class PositionUpdate : BaseServerDto
    {
        public List<SnakeDto> Snakes { get; set; }
    }
}

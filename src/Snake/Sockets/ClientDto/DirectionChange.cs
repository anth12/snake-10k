using Snake.Models.Enum;

namespace Snake.Sockets.ClientDto
{
    public class DirectionChange : BaseClientDto
    {
        public Direction Direction { get; set; }
    }
}

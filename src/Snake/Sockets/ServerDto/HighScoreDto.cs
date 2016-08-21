using Snake.Sockets.Attributes;

namespace Snake.Sockets.ServerDto
{
    [SocketCode("H")]
    public class HighScoreDto : BaseServerDto
    {
        public string Html { get; set; } = "";
    }
}

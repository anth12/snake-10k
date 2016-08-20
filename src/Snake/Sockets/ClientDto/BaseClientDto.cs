using System;

namespace Snake.Sockets.ClientDto
{
    public abstract class BaseClientDto
    {
        public Guid UserId { get; set; }
        public Guid UserToken { get; set; }
    }
}

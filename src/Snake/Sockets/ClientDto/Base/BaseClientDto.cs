using System;

namespace Snake.Sockets.ClientDto
{
    public abstract class BaseClientDto : IClientDto
    {
        public Guid User { get; set; }
        public Guid Token { get; set; }
    }
    
}

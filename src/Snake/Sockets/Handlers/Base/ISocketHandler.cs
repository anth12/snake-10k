
using Snake.Sockets.ClientDto;

namespace Snake.Sockets.Handlers
{
    public interface ISocketHandler
    {
        void Handle(IClientDto data);
        IClientDto ParseRequest(string request);
    }
}

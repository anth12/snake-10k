
using Newtonsoft.Json;
using Snake.Sockets.ClientDto;

namespace Snake.Sockets.Handlers.Base
{
    public abstract class BaseSocketHandler<TData> : ISocketHandler 
        where TData : IClientDto
    {
        public IClientDto ParseRequest(string request)
        {
            return JsonConvert.DeserializeObject<TData>(request);
        }

        public abstract void Handle(TData data);

        public void Handle(IClientDto data)
        {
            Handle((TData)data);
        }
    }
}

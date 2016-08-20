using Snake.Extensions;
using Snake.Game;
using Snake.Sockets.ClientDto;
using Snake.Sockets.Handlers.Base;

namespace Snake.Sockets.Handlers
{
    public class UsernameChangeHandler : BaseSocketHandler<UsernameChange>
    {

        public override void Handle(UsernameChange request)
        {
            var player = GameBackgroundStateManager.Current.Players.GetPlayer(request);
            
            player.Username = request.Username;
        }
        
    }
}

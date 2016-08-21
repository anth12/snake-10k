using Snake.Extensions;
using Snake.Game;
using Snake.Sockets.ClientDto;
using Snake.Sockets.Handlers.Base;

namespace Snake.Sockets.Handlers
{
    public class UserStatusChangeHandler : BaseSocketHandler<UserStatusChange>
    {

        public override void Handle(UserStatusChange request)
        {
            var player = GameBackgroundStateManager.Current.Players.GetPlayer(request);
            
            player.IsPlaying = request.Playing;
        }
        
    }
}

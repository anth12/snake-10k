using Snake.Extensions;
using Snake.Game;
using Snake.Sockets.ClientDto;

namespace Snake.Sockets.Handlers
{
    public class UsernameChangeHandler
    {
        public static void Handle(UsernameChange request)
        {
            var player = GameBackgroundStateManager.Current.Players.GetPlayer(request);

            // Restrict the queue to 3 moves
            if (player.Snake.DirectionQueue.Count >= 3)
                return;

            player.Username = request.Username;
        }
    }
}

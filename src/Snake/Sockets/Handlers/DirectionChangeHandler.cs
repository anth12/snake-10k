using Snake.Extensions;
using Snake.Game;
using Snake.Sockets.ClientDto;
using Snake.Sockets.Handlers.Base;

namespace Snake.Sockets.Handlers
{
    public class DirectionChangeHandler : BaseSocketHandler<DirectionChange>
    {
        public override void Handle(DirectionChange request)
        {
            var player = GameBackgroundStateManager.Current.Players.GetPlayer(request);

            // Restrict the queue to 3 moves
            if (player.Snake.DirectionQueue.Count >= 3)
                return;

            player.Snake.DirectionQueue.Add(request.Direction);
        }
    }
}

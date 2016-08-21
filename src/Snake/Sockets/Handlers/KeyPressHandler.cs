using Snake.Extensions;
using Snake.Game;
using Snake.Models.Enum;
using Snake.Sockets.ClientDto;
using Snake.Sockets.Handlers.Base;

namespace Snake.Sockets.Handlers
{
    public class KeyPressHandler : BaseSocketHandler<KeyPress>
    {
        public override void Handle(KeyPress request)
        {
            switch (request.Code)
            {
                case 87: // W
                case 38: // Up
                    DirectionHandler(request, Direction.Up);
                    break;

                case 68: // D
                case 39: // Right
                    DirectionHandler(request, Direction.Right);
                    break;

                case 83: // S
                case 40: // Down
                    DirectionHandler(request, Direction.Down);
                    break;

                case 65: // A
                case 37: // Left
                    DirectionHandler(request, Direction.Left);
                    break;

            }
        }

        private void DirectionHandler(KeyPress request, Direction direction)
        {
            var player = GameBackgroundStateManager.Current.Players.GetPlayer(request);

            // Restrict the queue to 3 moves
            if (player.Snake.DirectionQueue.Count >= 3)
                return;

            player.Snake.DirectionQueue.Add(direction);
        }
    }
}

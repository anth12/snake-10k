using System;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Snake.Game;
using Snake.Services;
using Snake.Sockets.ServerDto;

namespace Snake.Sockets.Handlers
{
    public class ConnectionOpenedHandler
    {
        public static async Task<Guid> Handle(WebSocket socket)
        {
            var user = UserService.CreateUser();
            
            // Spawn the player at a random position
            var rand = new Random();
            user.Snake.HeadPoint.X = rand.Next(0, GameBackgroundStateManager.Current.GameBoard.Width);
            user.Snake.HeadPoint.Y = rand.Next(0, GameBackgroundStateManager.Current.GameBoard.Height);


            GameBackgroundStateManager.Current.Players.Add(user.Id, user);
            GameBackgroundStateManager.Current.Connections.Add(user.Id, socket);

            var dto = new PlayerDto(user);
            await WebSocketBroker.Broadcast(socket, dto);

            return user.Id;
        }
    }
}

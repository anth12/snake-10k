using System;
using System.Linq;
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
             // TODO
            //user.Snake.HeadPoint.X = rand.Next(0, GameBackgroundStateManager.Current.GameBoard.Width);
            //user.Snake.HeadPoint.Y = rand.Next(0, GameBackgroundStateManager.Current.GameBoard.Height);


            GameBackgroundStateManager.Current.Players.Add(user.Id, user);
            GameBackgroundStateManager.Current.Connections.Add(user.Id, socket);

            var dto = new PlayerDto(user);
            await WebSocketBroker.Broadcast(socket, dto);

            SendHighscores(socket);

            return user.Id;
        }

        private static async void SendHighscores(WebSocket socket)
        {
            var dto = new HighScoreDto();

            dto.Html = @"
<thead>
    <tr>
        <td>Username</td>
        <td>Score</td>
    </tr>
</head>";

            GameBackgroundStateManager.Current.Players
                .Select(x => x.Value)
                .OrderByDescending(p => p.HighScore)
                .ThenBy(p=> p.Username)
                .Take(10)
                .ToList()
                .ForEach(player=> dto.Html += $@"
<tr>
    <td>{player.Username}</td>
    <td>{player.HighScore}</td>
</tr>
");

            await WebSocketBroker.Broadcast(socket, dto);
        }
    }
}

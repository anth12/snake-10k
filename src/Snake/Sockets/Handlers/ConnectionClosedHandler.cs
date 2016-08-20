using System;
using System.Net.WebSockets;
using Snake.Game;

namespace Snake.Sockets.Handlers
{
    public class ConnectionClosedHandler
    {
        public static async void Handle(Guid userId, WebSocket socket)
        {

            GameBackgroundStateManager.Current.Players.Remove(userId);
            GameBackgroundStateManager.Current.Connections.Remove(userId);
        }
    }
}

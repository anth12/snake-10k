using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Snake.Game;
using Snake.Sockets.ClientDto;
using Snake.Sockets.Handlers;
using Snake.Sockets.ServerDto;

namespace Snake.Sockets
{
    public class WebSocketBroker
    {
        public static async Task HandleRequest(HttpContext http, Func<Task> next)
        {
            if (http.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await http.WebSockets.AcceptWebSocketAsync();

                var userId = await ConnectionOpenedHandler.Handle(webSocket);

                while (webSocket.State == WebSocketState.Open)
                {
                    var token = CancellationToken.None;
                    var buffer = new ArraySegment<Byte>(new Byte[4096]);
                    var received = await webSocket.ReceiveAsync(buffer, token);

                    switch (received.MessageType)
                    {
                        case WebSocketMessageType.Text:
                            var request = Encoding.UTF8.GetString(buffer.Array,
                                                    buffer.Offset,
                                                    buffer.Count);

                            // Handle the message
                            
                            // TODO assuming all requests are to Change Direction
                            var data = JsonConvert.DeserializeObject<DirectionChange>(request);
                            DirectionChangeHandler.Handle(data);

                            break;
                    }
                }
                
                ConnectionClosedHandler.Handle(userId, webSocket);
            }
            else
            {
                await next();
            }

        }

        // TODO change serializable to protobuf, msgpack or similar


        internal static async Task Broadcast<TData>(Guid id, TData data)
            where TData : BaseServerDto
        {
            WebSocket socket;
            if (GameBackgroundStateManager.Current.Connections.TryGetValue(id, out socket))
            {
                await Broadcast(socket, data);
            }
        }

        internal static async Task Broadcast<TData>(WebSocket socket, TData data)
            where TData : BaseServerDto
        {
            if (socket != null && socket.State == WebSocketState.Open)
            {

                var message = typeof(TData).Name + 
                    ":" + JsonConvert.SerializeObject(data);

                var buffer = new ArraySegment<Byte>(Encoding.UTF8.GetBytes(message));
                await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
            }

        }

        internal static async Task BroadcastAll<TData>(TData data)
            where TData : BaseServerDto
        {
            foreach (var socket in GameBackgroundStateManager.Current.Connections)
            {
                if (socket.Value != null && socket.Value.State == WebSocketState.Open)
                {
                    var message = JsonConvert.SerializeObject(data);
                    var buffer = new ArraySegment<Byte>(Encoding.UTF8.GetBytes(message));
                    await socket.Value.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }
}

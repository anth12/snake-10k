using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Snake.Models;
using Snake.Sockets;
using Snake.Sockets.ServerDto;

namespace Snake.Game
{
    public class GameBackgroundStateManager
    {
        #region Singleton

        public static GameBackgroundStateManager Current { get; } = new GameBackgroundStateManager();

        #endregion

        #region Properties

        
        public GameBoard GameBoard { get; set; }

        public IDictionary<Guid, Player> Players = new ConcurrentDictionary<Guid, Player>();

        public IDictionary<Guid, WebSocket> Connections = new ConcurrentDictionary<Guid, WebSocket>();

        public IDictionary<Guid, Point> Lives = new ConcurrentDictionary<Guid, Point>();

        #endregion

        public void Start()
        {
            GameBoard = new GameBoard
            {
                Id = Guid.NewGuid(),
                Height = 500,
                Width = 500,
                Velocity = 350,
                IsActive = true
            };

            // Start the background threads
            Task.Run(()=> UpdatePositions());
        }

        #region Background async tasks

        private DateTime LastPositionUpdate = DateTime.Now;
        private void UpdatePositions()
        {
            while (GameBoard.IsActive)
            {
                if (DateTime.Now.AddMilliseconds(-GameBoard.Velocity) < LastPositionUpdate)
                {
                    continue;
                }

                LastPositionUpdate = DateTime.Now;


                var renderData = new PositionUpdate();

                Parallel.ForEach(Players.Where(x=> x.Value.IsPlaying), (keyValue) =>
                {
                    var snake = keyValue.Value.Snake;

                    // Move the head to the body
                    if(snake.Body.Count >= snake.Length)
                        snake.Body.RemoveAt(0);

                    snake.Body.Add(snake.HeadPoint);

                    // Update the direction
                    if (snake.DirectionQueue.Any())
                    {
                        snake.Direction = snake.DirectionQueue.First();
                        snake.DirectionQueue.RemoveAt(0);
                    }

                    // Create the new head position
                    snake.HeadPoint = snake.HeadPoint
                            .Move(snake.Direction)
                            .Constrain(GameBoard.Width, GameBoard.Height);

                    if (!renderData.Snakes.TryAdd(keyValue.Key, snake.Body))
                    {
                        // TODO handle errors
                    }
                });
                
                // Map the client data
                renderData.LifePoints = Lives.Select(l => l.Value).ToList();


                #pragma warning disable 4014
                WebSocketBroker.BroadcastAllActive(renderData);
                #pragma warning restore 4014
            }
        }

        #endregion
        
    }
}

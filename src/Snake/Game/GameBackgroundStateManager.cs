using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Snake.Models;

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

        #endregion

        public void Start()
        {
            GameBoard = new GameBoard
            {
                Id = Guid.NewGuid(),
                Height = 1500,
                Width = 1500
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
                if (LastPositionUpdate <= DateTime.Now.AddMilliseconds(-GameBoard.Velocity))
                {
                    continue;
                }
                LastPositionUpdate = DateTime.Now;

                Parallel.ForEach(Players, (keyValue) =>
                {
                    var snake = keyValue.Value.Snake;

                    // Move the head to th body
                    snake.Body.RemoveAt(0);
                    snake.Body.Add(snake.HeadPoint);

                    // Update the direction
                    if (snake.DirectionQueue.Any())
                    {
                        snake.Direction = snake.DirectionQueue.First();
                        snake.DirectionQueue.RemoveAt(0);
                    }

                    // Create the new head position
                    snake.HeadPoint = snake.HeadPoint.Move(snake.Direction).Constrain(GameBoard.Width, GameBoard.Height);
                });
                
            }
        }

        #endregion
        
    }
}

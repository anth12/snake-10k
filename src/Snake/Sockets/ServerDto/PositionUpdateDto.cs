using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Snake.Models;
using Snake.Sockets.Attributes;

namespace Snake.Sockets.ServerDto
{
    [SocketCode("P")]
    public class PositionUpdateDto : BaseServerDto
    {
        public ConcurrentDictionary<Guid, List<Point>> Snakes { get; set; } = new ConcurrentDictionary<Guid, List<Point>>();
        public List<Point> LifePoints { get; set; } = new List<Point>();
    }
}

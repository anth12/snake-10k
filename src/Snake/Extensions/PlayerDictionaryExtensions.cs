using System;
using System.Collections.Generic;
using Snake.Models;
using Snake.Sockets.ClientDto;

namespace Snake.Extensions
{
    public static class PlayerDictionaryExtensions
    {
        public static Player GetPlayer(this IDictionary<Guid, Player> dictionary, BaseClientDto request)
        {
            Player player;
            if (dictionary.TryGetValue(request.UserId, out player))
            {
                if (player.Token == request.UserToken)
                {
                    return player;
                }
            }

            throw new Exception($"Invalid User Id or Access token");
        }
    }
}

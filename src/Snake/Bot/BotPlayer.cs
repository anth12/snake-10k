
namespace Snake.Bot
{
    internal class BotPlayer
    {
        /// <summary>
        /// [0-100] How fast the players reaction time is, on average
        /// </summary>
        public int ReactionTime { get; set; }

        /// <summary>
        /// [0-100] How much foresight the player has- ability to make moves in advance
        /// </summary>
        public int Vision { get; set; }

        /// <summary>
        /// [0-100] Amount of self awareness the player has- how likely they are to collide with their body
        /// </summary>
        public int Awareness { get; set; }

        /// <summary>
        /// [0-100] Players ability to pickout the closest target
        /// </summary>
        public int DepthPerception { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Tennis
{
    internal class PlayerScore
    {
        internal Player Player { get; set; }
        internal Scoring Score { get; set; }

        public PlayerScore(Player player)
        {
            Player = player;
        }

        public void IncrementScore()
        {
            Score++;
        }
    }

    internal class Scoreboard
    {
        private readonly Player player2;
        private readonly Dictionary<Player, PlayerScore> playerScores = new Dictionary<Player, PlayerScore>();
        private readonly Player player1;

        public Scoreboard(Player player1, Player player2)
        {
            playerScores.Add(player1, new PlayerScore(player1));
            playerScores.Add(player2, new PlayerScore(player2));
            this.player1 = player1;
            this.player2 = player2;
        }

        public void IncrementPlayerScore(Player player)
        {
            playerScores[player].IncrementScore();
        }

        public override string ToString()
        {
            if (playerScores.All(x => x.Value.Score == playerScores.First().Value.Score))
            {
                return playerScores[player1].Score >= Scoring.Forty ? "Deuce" : $"{playerScores[player2].Score}-All";
            }

            if (playerScores.All(x => x.Value.Score <= Scoring.Forty))
            {
                return $"{playerScores[player1].Score}-{playerScores[player2].Score}";
            }

            var leader = playerScores.OrderByDescending(x => x.Value.Score).First().Key;
            return Math.Abs(playerScores[player1].Score - playerScores[player2].Score) == 1 ? $"Advantage {leader.Name}" : $"Win for {leader.Name}";
        }
    }

    internal enum Scoring
    {
        Love = 0,
        Fifteen = 1,
        Thirty = 2,
        Forty = 3
    }
}
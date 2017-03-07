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
        private readonly Dictionary<Player, Scoring> scores = new Dictionary<Player, Scoring>();
        private readonly List<PlayerScore> playerScores = new List<PlayerScore>();
        private readonly Player player1;

        public Scoreboard(Player player1, Player player2)
        {
            playerScores.Add(new PlayerScore(player1));
            playerScores.Add(new PlayerScore(player2));
            this.player1 = player1;
            this.player2 = player2;
            scores.Add(player1, 0);
            scores.Add(player2, 0);
        }

        public void IncrementPlayerScore(Player player)
        {
            playerScores.First(x => x.Player == player).IncrementScore();
            scores[player]++;
        }

        public override string ToString()
        {
            if (playerScores.All(x => x.Score == playerScores.First().Score))
            {
                return scores[player1] >= Scoring.Forty ? "Deuce" : $"{scores[player2]}-All";
            }

            if (scores.All(x => x.Value <= Scoring.Forty))
            {
                return $"{scores[player1]}-{scores[player2]}";
            }

            var leader = scores.OrderByDescending(x => x.Value).First().Key;
            return Math.Abs(scores[player1] - scores[player2]) == 1 ? $"Advantage {leader.Name}" : $"Win for {leader.Name}";
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
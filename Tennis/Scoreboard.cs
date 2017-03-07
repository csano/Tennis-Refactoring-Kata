using System;
using System.Collections.Generic;
using System.Linq;

namespace Tennis
{
    internal class PlayerScore
    {
        private readonly Player player;

        public PlayerScore(Player player)
        {
            this.player = player;
        }
    }

    internal class Scoreboard
    {
        private readonly Player player2;
        private readonly Dictionary<Player, Scoring> scores = new Dictionary<Player, Scoring>();
        private readonly List<PlayerScore> playerScores = new List<PlayerScore>();
        private readonly PlayerScore playerScore;
        private readonly Player player1;

        public Scoreboard(Player player1, Player player2)
        {
            playerScore = new PlayerScore(player1);
            playerScores.Add(new PlayerScore(player1));
            this.player1 = player1;
            this.player2 = player2;
            scores.Add(player1, 0);
            scores.Add(player2, 0);
        }

        public void IncrementPlayerScore(Player player)
        {
            scores[player]++;
        }

        public override string ToString()
        {
            if (scores.All(x => x.Value == scores.First().Value))
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

        private enum Scoring
        {
            Love = 0,
            Fifteen = 1,
            Thirty = 2,
            Forty = 3
        }
    }
}
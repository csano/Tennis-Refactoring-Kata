using System;
using System.Collections.Generic;
using System.Linq;

namespace Tennis
{
    internal class PlayerScore
    {
        private readonly Player player1;

        public PlayerScore(Player player1)
        {
            this.player1 = player1;
        }
    }

    internal class Scoreboard
    {
        private readonly Player player2;
        private readonly Dictionary<Player, Scoring> scores = new Dictionary<Player, Scoring>();
        private readonly PlayerScore playerScore;

        public Scoreboard(Player player1, Player player2)
        {
            playerScore = new PlayerScore(player1);
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
                return scores[playerScore.player1] >= Scoring.Forty ? "Deuce" : $"{scores[playerScore.player1]}-All";
            }

            if (scores.All(x => x.Value <= Scoring.Forty))
            {
                return $"{scores[playerScore.player1]}-{scores[player2]}";
            }

            var leader = scores.OrderByDescending(x => x.Value).First().Key;
            return Math.Abs(scores[playerScore.player1] - scores[player2]) == 1 ? $"Advantage {leader.Name}" : $"Win for {leader.Name}";
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
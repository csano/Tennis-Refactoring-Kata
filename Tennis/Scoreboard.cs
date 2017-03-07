using System;
using System.Collections.Generic;
using System.Linq;

namespace Tennis
{
    public class PlayerScore
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

    public interface IScoringRule
    {
        string Evaluate(PlayerScore player1, PlayerScore player2);
    }

    public class TieRule : IScoringRule
    {
        public string Evaluate(PlayerScore player1, PlayerScore player2)
        {
            if (player1.Score == player2.Score)
            {
                if (player1.Score == Scoring.Forty)
                {
                    return "Deuce";
                }
                else
                {
                   return $"{player1.Score}-All"; 
                }
                
            }

            return null;
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
            var result = new TieRule().Evaluate(playerScores[player1], playerScores[player2]);
            if (result != null)
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
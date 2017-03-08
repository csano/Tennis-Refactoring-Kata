using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

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
            if (player1.Score != player2.Score) {
                return null;
            }
            return player1.Score >= Scoring.Forty ? "Deuce" : $"{player1.Score}-All";
        }
    }

    public class ScoreIsNotATieAndFortyOrUnderRule : IScoringRule
    {
        public string Evaluate(PlayerScore player1, PlayerScore player2)
        {
            if (player1.Score != player2.Score && ScoreIsLessThanOrEqualToForty(player1) && ScoreIsLessThanOrEqualToForty(player2))
            {
                return $"{player1.Score}-{player2.Score}";
            }
            return null;
        }

        private static bool ScoreIsLessThanOrEqualToForty(PlayerScore player)
        {
            return player.Score <= Scoring.Forty;
        }
    }

    internal class AdvantageRule : RuleBase
    {
        public override string Evaluate(PlayerScore player1Score, PlayerScore player2Score)
        {
            if (player1Score.Score >= Scoring.Forty && player2Score.Score >= Scoring.Forty && Math.Abs(player1Score.Score - player2Score.Score) == 1)
            {
                return $"Advantage {GetHighestPlayerScore(player1Score, player2Score).Player.Name}";
            }
            return null;
        }
    }

    internal class WinnerRule : IScoringRule
    {
        private static PlayerScore GetHighestPlayerScore(PlayerScore player1Score, PlayerScore player2Score)
        {
            return player1Score.Score >= player2Score.Score ? player1Score : player2Score;
        }

        public string Evaluate(PlayerScore player1, PlayerScore player2)
        {
            if ((player1.Score > Scoring.Forty || player2.Score > Scoring.Forty) && Math.Abs(player1.Score - player2.Score) > 1)
            {
                var highest = GetHighestPlayerScore(player1, player2);
                return $"Win for {highest.Player.Name}";
            }
            return null;
        }
    }

    internal class RuleFactory
    {
        private static List<IScoringRule> scoringRules = GetScoringRules();

        private static List<IScoringRule> GetScoringRules()
        {
            return new List<IScoringRule>
            {
                new TieRule(),
                new AdvantageRule(),
                new ScoreIsNotATieAndFortyOrUnderRule(),
                new WinnerRule()
            };
        }

        public string Evaluate(PlayerScore player1Score, PlayerScore player2Score)
        {
            foreach (var rule in GetScoringRules())
            {
                var result = rule.Evaluate(player1Score, player2Score);
                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }
            }
            return "";
        }
    }

    internal class Scoreboard
    {
        private readonly Dictionary<Player, PlayerScore> playerScores = new Dictionary<Player, PlayerScore>();

        public Scoreboard(Player player1, Player player2)
        {
            playerScores.Add(player1, new PlayerScore(player1));
            playerScores.Add(player2, new PlayerScore(player2));
        }

        public void IncrementPlayerScore(Player player)
        {
            playerScores[player].IncrementScore();
        }

        public override string ToString()
        {
            var player1Score = playerScores.First().Value;
            var player2Score = playerScores.Skip(1).First().Value;

            return new RuleFactory().Evaluate(player1Score, player2Score);
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
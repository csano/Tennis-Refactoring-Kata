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

    public interface IScoringCondition
    {
        string Evaluate(PlayerScore player1Score, PlayerScore player2Score);
    }

    public class TieCondition : IScoringCondition
    {
        public string Evaluate(PlayerScore player1Score, PlayerScore player2Score)
        {
            if (player1Score.Score == player2Score.Score)
            {
                return player1Score.Score >= Scoring.Forty ? "Deuce" : $"{player1Score.Score}-All";
            }
            return null;
        }
    }

    public class ScoreIsNotATieAndFortyOrUnderCondition : IScoringCondition
    {
        public string Evaluate(PlayerScore player1Score, PlayerScore player2Score)
        {
            if (player1Score.Score != player2Score.Score && ScoreIsLessThanOrEqualToForty(player1Score) && ScoreIsLessThanOrEqualToForty(player2Score))
            {
                return $"{player1Score.Score}-{player2Score.Score}";
            }
            return null;
        }

        private static bool ScoreIsLessThanOrEqualToForty(PlayerScore player)
        {
            return player.Score <= Scoring.Forty;
        }
    }

    internal class AdvantageCondition : ConditionBase
    {
        public override string Evaluate(PlayerScore player1Score, PlayerScore player2Score)
        {
            if ((player1Score.Score > Scoring.Forty || player2Score.Score > Scoring.Forty) && CalculateScoreDifferential(player1Score, player2Score) == 1)
            {
                return $"Advantage {GetHighestPlayerScore(player1Score, player2Score).Player.Name}";
            }
            return null;
        }
    }

    internal class WinnerCondition : ConditionBase
    {
        public override string Evaluate(PlayerScore player1Score, PlayerScore player2Score)
        {
            if (AtLeastOnePlayerHasScoreGreaterThan(player1Score, player2Score, Scoring.Forty) && CalculateScoreDifferential(player1Score, player2Score) > 1)
            {
                var highest = GetHighestPlayerScore(player1Score, player2Score);
                return $"Win for {highest.Player.Name}";
            }
            return null;
        }

        private static bool AtLeastOnePlayerHasScoreGreaterThan(PlayerScore player1Score, PlayerScore player2Score, Scoring score)
        {
            return player1Score.Score > score || player2Score.Score > score;
        }
    }

    internal class ScoreDisplay
    {
        private static IEnumerable<IScoringCondition> GetScoringRules()
        {
            return new List<IScoringCondition>
            {
                new TieCondition(),
                new AdvantageCondition(),
                new ScoreIsNotATieAndFortyOrUnderCondition(),
                new WinnerCondition()
            };
        }

        public string Generate(PlayerScore player1Score, PlayerScore player2Score)
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

            return new ScoreDisplay().Generate(player1Score, player2Score);
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
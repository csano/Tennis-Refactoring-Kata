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

    public class TieCondition : ConditionBase
    {
        public override string Evaluate(PlayerScore player1Score, PlayerScore player2Score)
        {
            if (PlayerScoresAreEqual(player1Score, player2Score))
            {
                return player1Score.Score >= Scoring.Forty ? "Deuce" : $"{player1Score.Score}-All";
            }
            return null;
        }
    }

    public class ScoreIsNotATieAndFortyOrUnderCondition : ConditionBase
    {
        public override string Evaluate(PlayerScore player1Score, PlayerScore player2Score)
        {
            if (!PlayerScoresAreEqual(player1Score, player2Score) && ScoreIsLessThanOrEqualToForty(player1Score) && ScoreIsLessThanOrEqualToForty(player2Score))
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
            if (AtLeastOnePlayerHasScoreGreaterThan(player1Score, player2Score, Scoring.Forty) && ScoreDifferential(player1Score, player2Score) == 1)
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
            if (AtLeastOnePlayerHasScoreGreaterThan(player1Score, player2Score, Scoring.Forty) && ScoreDifferential(player1Score, player2Score) > 1)
            {
                return $"Win for {GetHighestPlayerScore(player1Score, player2Score).Player.Name}";
            }
            return null;
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
        private readonly PlayerScore[] playerScores = new PlayerScore[2];

        public Scoreboard(Player player1, Player player2)
        {
            playerScores[0] = new PlayerScore(player1);
            playerScores[1] = new PlayerScore(player2);
        }

        public void IncrementPlayerScore(Player player)
        {
            playerScores.First(x => x.Player == player).IncrementScore();
        }

        public override string ToString()
        {
            return new ScoreDisplay().Generate(playerScores[0], playerScores[1]);
        }
    }

    public enum Scoring
    {
        Love = 0,
        Fifteen = 1,
        Thirty = 2,
        Forty = 3
    }
}
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


    internal class AdvantageRule : IScoringRule
    {
        private static PlayerScore GetHighestPlayerScore(PlayerScore player1Score, PlayerScore player2Score)
        {
            return player1Score.Score >= player2Score.Score ? player1Score : player2Score;
        }

        public string Evaluate(PlayerScore player1Score, PlayerScore player2Score)
        {
            if (Math.Abs(player1Score.Score - player2Score.Score) == 1)
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
            if ((player1.Score > Scoring.Forty || player2.Score > Scoring.Forty) && Math.Abs(player1.Score - player2.Score) >= 1)
            {
                var highest = GetHighestPlayerScore(player1, player2);
                return $"Win for {highest.Player.Name}";
            }
            return null;
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

            var tieRuleResult = new TieRule().Evaluate(player1Score, player2Score);
            if (tieRuleResult != null)
            {
                return tieRuleResult;
            }

            var scoreIsNotATieResult = new ScoreIsNotATieAndFortyOrUnderRule().Evaluate(player1Score, player2Score);
            if (scoreIsNotATieResult != null)
            {
                return scoreIsNotATieResult;
            }

            var advantageRuleResult = new AdvantageRule().Evaluate(player1Score, player2Score);
            if (advantageRuleResult != null)
            {
                return advantageRuleResult;
            }

            var winningRuleResult = new WinnerRule().Evaluate(player1Score, player2Score);
            if (winningRuleResult != null)
            {
                return winningRuleResult;
            }

            return string.Empty;
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
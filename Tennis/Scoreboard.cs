using System;
using System.Collections.Generic;
using System.Linq;

namespace Tennis
{
    internal class RuleEvaluator
    {
        private static IEnumerable<IScoringCondition> GetScoringRules()
        {
            return new List<IScoringCondition>
            {
                new TieRule(),
                new AdvantageRule(),
                new ScoreIsNotATieAndFortyOrUnderCondition(),
                new WinnerCondition()
            };
        }

        public string Generate(PlayerScore player1Score, PlayerScore player2Score)
        {
            return null;
                //GetScoringRules().Select(x => x.Evaluate(player1Score, player2Score)).FirstOrDefault(x => x != null);
        }
    }

    internal class TieRepresentation : IConditionStringConverter
    {
        public string Convert(PlayerScore player1Score, PlayerScore player2Score)
        {
            throw new NotImplementedException();
        }
    }

    internal interface IConditionStringConverter
    {
        string Convert(PlayerScore player1Score, PlayerScore player2Score);
    }

    internal class Scoreboard
    {
        private readonly PlayerScore[] playerScores = new PlayerScore[2];

        public Scoreboard(Player player1, Player player2)
        {
            playerScores[0] = new PlayerScore(player1);
            playerScores[1] = new PlayerScore(player2);
        }

        public void GivePointToPlayer(Player player)
        {
            playerScores.First(x => x.Player == player).IncrementScore();
        }

        public override string ToString()
        {
            return new RuleEvaluator().Generate(playerScores[0], playerScores[1]);
        }
    }

    public enum Scoring
    {
        Love = 0,
        Fifteen = 1,
        Thirty = 2,
        Forty = 3,
        Advantage = 4,
        Winner = 5
    }
}
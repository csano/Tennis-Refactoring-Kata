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
                new ScoreIsNotATieRule(),
                new WinnerCondition()
            };
        }

        public bool Evaluate(PlayerScore player1Score, PlayerScore player2Score)
        {
            return GetScoringRules().Select(x => x.Evaluate(player1Score, player2Score)).FirstOrDefault(x => true);
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

        public void GivePointToPlayer(Player player)
        {
            playerScores.First(x => x.Player == player).IncrementScore();
        }

        public override string ToString()
        {
            /*
            var successCondition = new RuleEvaluator().Evaluate(playerScores[0], playerScores[1]);
            if (successCondition)
            {
                return new RuleStringConverter().Convert(successCondition, playerScores[0], playerScores[1]);
            }
            */
            return "";
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
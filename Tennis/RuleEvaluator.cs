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

        public IScoringCondition Evaluate(PlayerScore player1Score, PlayerScore player2Score)
        {
            return GetScoringRules().FirstOrDefault(x => x.Evaluate(player1Score, player2Score));
        }
    }
}
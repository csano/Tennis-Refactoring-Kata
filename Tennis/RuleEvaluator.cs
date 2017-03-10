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
}
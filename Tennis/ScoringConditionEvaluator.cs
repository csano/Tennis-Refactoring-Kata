using System.Collections.Generic;
using System.Linq;

namespace Tennis
{
    internal class ScoringConditionEvaluator
    {
        private static IEnumerable<IScoringCondition> GetScoringRules()
        {
            return AssemblyUtility.CreateInstancesFromAssemblyTypes<IScoringCondition>();
        }

        public IScoringCondition Evaluate(PlayerScore player1Score, PlayerScore player2Score)
        {
            return GetScoringRules().FirstOrDefault(x => x.Evaluate(player1Score, player2Score));
        }
    }
}
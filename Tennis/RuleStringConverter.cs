using System;
using System.Collections.Generic;
using System.Linq;

namespace Tennis
{
    internal class RuleStringConverter
    {
        private static Dictionary<Type, IConditionStringConverter> GetStringConverters()
        {
            return new Dictionary<Type, IConditionStringConverter>
            {
                { typeof(TieRule), new TieConditionStringConverter() },
                { typeof(ScoreIsNotATieRule), new ScoreIsNotATieStringConverter() },
                { typeof(AdvantageRule), new AdvantageRuleStringConverter() },
                { typeof(WinnerCondition), new WinnerStringConverter() }
            };
        }

        public string Convert(IScoringCondition scoringCondition, PlayerScore player1Score, PlayerScore player2Score) 
        {
            return GetStringConverters().Where(x => x.Key == scoringCondition.GetType()).Select(x => x.Value).FirstOrDefault()?.Convert(player1Score, player2Score);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tennis
{
    internal class RuleStringConverter
    {
        private static IEnumerable<IConditionStringConverter> GetStringConverters()
        {
            return new List<IConditionStringConverter>
            {
                 new TieConditionStringConverter() ,
                 new ScoreIsNotATieStringConverter() ,
                 new AdvantageRuleStringConverter() ,
                 new WinnerStringConverter()
            };
        }

        public string Convert(IScoringCondition scoringCondition, PlayerScore player1Score, PlayerScore player2Score) 
        {
            return GetStringConverters().FirstOrDefault(x => x.ConditionType == scoringCondition.GetType())?.Convert(player1Score, player2Score);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tennis
{
    internal class RuleStringConverter
    {
        private static IEnumerable<IScoringConditionStringConverter> GetStringConverters()
        {
            return AppDomain.CurrentDomain
                            .GetAssemblies()
                            .SelectMany(s => s.GetTypes())
                            .Where(p => typeof(IScoringConditionStringConverter).IsAssignableFrom(p) && !p.IsInterface)
                            .Select(x => (IScoringConditionStringConverter)Activator.CreateInstance(x));
        }

        public string Convert(IScoringCondition scoringCondition, PlayerScore player1Score, PlayerScore player2Score) 
        {
            return GetStringConverters().FirstOrDefault(x => x.ConditionType == scoringCondition.GetType())?.Convert(player1Score, player2Score);
        }
    }
}
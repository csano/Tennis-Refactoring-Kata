using System;

namespace Tennis
{
    internal class AdvantageScoringConditionStringConverter : IScoringConditionStringConverter
    {
        public Type ConditionType => typeof(AdvantageScoringCondition);

        public string Convert(PlayerScore player1Score, PlayerScore player2Score)
        {
            return $"Advantage {GetHighestPlayerScore(player1Score, player2Score).Player.Name}";
        }

        protected PlayerScore GetHighestPlayerScore(PlayerScore player1Score, PlayerScore player2Score)
        {
            return player1Score.Score >= player2Score.Score ? player1Score : player2Score;
        }
    }
}
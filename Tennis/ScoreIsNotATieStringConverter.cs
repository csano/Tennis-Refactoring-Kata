using System;

namespace Tennis
{
    internal class ScoreIsNotATieStringConverter : IScoringConditionStringConverter
    {
        public Type ConditionType => typeof(ScoreIsNotATieRule);

        public string Convert(PlayerScore player1Score, PlayerScore player2Score)
        {
            return $"{player1Score.Score}-{player2Score.Score}";
        }

        protected PlayerScore GetHighestPlayerScore(PlayerScore player1Score, PlayerScore player2Score)
        {
            return player1Score.Score >= player2Score.Score ? player1Score : player2Score;
        }
    }
}
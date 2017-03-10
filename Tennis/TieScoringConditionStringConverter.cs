using System;

namespace Tennis
{
    internal class TieScoringConditionStringConverter : IScoringConditionStringConverter
    {
        public string Convert(PlayerScore player1Score, PlayerScore player2Score)
        {
            return player1Score.Score >= Scoring.Forty ? "Deuce" : $"{player1Score.Score}-All";
        }

        public Type ConditionType => typeof(TieScoringCondition);
    }
}
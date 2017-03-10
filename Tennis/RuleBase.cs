using System;

namespace Tennis
{
    public abstract class ConditionBase : IScoringCondition
    {
        public abstract bool Evaluate(PlayerScore player1Score, PlayerScore player2Score);

        protected static int ScoreDifferential(PlayerScore player1Score, PlayerScore player2Score)
        {
            return Math.Abs(player1Score.Score - player2Score.Score);
        }

        public static bool AtLeastOnePlayerHasScoreGreaterThan(PlayerScore player1Score, PlayerScore player2Score, Scoring score)
        {
            return player1Score.Score > score || player2Score.Score > score;
        }

        protected static bool PlayerScoresAreEqual(PlayerScore player1Score, PlayerScore player2Score)
        {
            return player1Score.Score == player2Score.Score;
        }
    }
}
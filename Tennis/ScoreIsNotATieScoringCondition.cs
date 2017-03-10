using System;

namespace Tennis
{
    public class ScoreIsNotATieScoringCondition : ScoringConditionBase
    {
        public override bool Evaluate(PlayerScore player1Score, PlayerScore player2Score)
        {
            return !PlayerScoresAreEqual(player1Score, player2Score) && ScoreIsLessThanOrEqualToForty(player1Score) && ScoreIsLessThanOrEqualToForty(player2Score);
        }

        private static bool ScoreIsLessThanOrEqualToForty(PlayerScore player)
        {
            return player.Score <= Scoring.Forty;
        }
    }
}
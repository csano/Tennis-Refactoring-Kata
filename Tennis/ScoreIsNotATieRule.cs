using System;

namespace Tennis
{
    public class ScoreIsNotATieRule : ConditionBase
    {
        public Func<PlayerScore, PlayerScore, bool> Condition = (player1Score, player2Score) => !PlayerScoresAreEqual(player1Score, player2Score) && ScoreIsLessThanOrEqualToForty(player1Score) && ScoreIsLessThanOrEqualToForty(player2Score);
        public override bool Evaluate(PlayerScore player1Score, PlayerScore player2Score)
        {
            return Condition(player1Score, player2Score);
        }

        private static bool ScoreIsLessThanOrEqualToForty(PlayerScore player)
        {
            return player.Score <= Scoring.Forty;
        }
    }
}
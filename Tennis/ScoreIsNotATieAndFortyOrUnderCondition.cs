using System;

namespace Tennis
{
    public class ScoreIsNotATieAndFortyOrUnderCondition : ConditionBase
    {
        public Func<PlayerScore, PlayerScore, bool> Condition = (player1Score, player2Score) => !PlayerScoresAreEqual(player1Score, player2Score) && ScoreIsLessThanOrEqualToForty(player1Score) && ScoreIsLessThanOrEqualToForty(player2Score);
        public override string Evaluate(PlayerScore player1Score, PlayerScore player2Score)
        {
            if (Condition(player1Score, player2Score))
            {
                return $"{player1Score.Score}-{player2Score.Score}";
            }
            return null;
        }

        private static bool ScoreIsLessThanOrEqualToForty(PlayerScore player)
        {
            return player.Score <= Scoring.Forty;
        }
    }
}
namespace Tennis
{
    internal class AdvantageRule : ConditionBase
    {
        public override bool Evaluate(PlayerScore player1Score, PlayerScore player2Score)
        {
            return AtLeastOnePlayerHasScoreGreaterThan(player1Score, player2Score, Scoring.Forty) && ScoreDifferential(player1Score, player2Score) == 1;
        }
    }
}
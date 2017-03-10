namespace Tennis
{
    internal class WinnerCondition : ConditionBase
    {
        public override string Evaluate(PlayerScore player1Score, PlayerScore player2Score)
        {
            if (AtLeastOnePlayerHasScoreGreaterThan(player1Score, player2Score, Scoring.Forty) && ScoreDifferential(player1Score, player2Score) > 1)
            {
                return $"Win for {GetHighestPlayerScore(player1Score, player2Score).Player.Name}";
            }
            return null;
        }
    }
}
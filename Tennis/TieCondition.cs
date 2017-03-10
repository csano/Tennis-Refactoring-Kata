namespace Tennis
{
    public class TieCondition : ConditionBase
    {
        public override string Evaluate(PlayerScore player1Score, PlayerScore player2Score)
        {
            if (PlayerScoresAreEqual(player1Score, player2Score))
            {
                return player1Score.Score >= Scoring.Forty ? "Deuce" : $"{player1Score.Score}-All";
            }
            return null;
        }
    }
}
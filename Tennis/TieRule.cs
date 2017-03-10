namespace Tennis
{
    public class TieRule : ConditionBase
    {
        public override bool Evaluate(PlayerScore player1Score, PlayerScore player2Score)
        {
            return PlayerScoresAreEqual(player1Score, player2Score);
                //return player1Score.Score >= Scoring.Forty ? "Deuce" : $"{player1Score.Score}-All";
        }
    }
}
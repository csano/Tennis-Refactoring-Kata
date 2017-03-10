namespace Tennis
{
    public class TieCondition : ConditionBase
    {
        public override bool Evaluate(PlayerScore player1Score, PlayerScore player2Score)
        {
            return PlayerScoresAreEqual(player1Score, player2Score);
        }
    }
}
namespace Tennis
{
    public interface IScoringCondition
    {
        string Evaluate(PlayerScore player1Score, PlayerScore player2Score);
    }
}
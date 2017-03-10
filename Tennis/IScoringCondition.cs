namespace Tennis
{
    public interface IScoringCondition
    {
        bool Evaluate(PlayerScore player1Score, PlayerScore player2Score);
    }
}
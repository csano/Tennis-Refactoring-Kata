namespace Tennis
{
    internal abstract class RuleBase : IScoringRule
    {
        protected PlayerScore GetHighestPlayerScore(PlayerScore player1Score, PlayerScore player2Score)
        {
            return player1Score.Score >= player2Score.Score ? player1Score : player2Score;
        }

        public abstract string Evaluate(PlayerScore player1, PlayerScore player2);
    }
}
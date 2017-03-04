namespace Tennis
{
    internal class TennisGame1 : ITennisGame
    {
        private readonly Score score;

        public TennisGame1(Player player1, Player player2)
        {
            score = new Score(player1, player2);
        }

        public void AwardPointToPlayer(Player player)
        {
            score.IncrementPlayerScore(player);
        }

        public string GetCurrentScore()
        {
            return score.ToString();
        }
    }
}


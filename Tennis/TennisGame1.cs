namespace Tennis
{
    internal class TennisGame1 : ITennisGame
    {
        private readonly Scoreboard scoreboard;

        public TennisGame1(Player player1, Player player2)
        {
            scoreboard = new Scoreboard(player1, player2);
        }

        public void AwardPointToPlayer(Player player)
        {
            scoreboard.GivePointToPlayer(player);
        }

        public string GetCurrentScore()
        {
            return scoreboard.ToString();
        }
    }
}


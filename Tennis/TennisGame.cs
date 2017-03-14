namespace Tennis
{
    internal class TennisGame : ITennisGame
    {
        private readonly Scoreboard scoreboard;

        public TennisGame(Player player1, Player player2)
        {
            scoreboard = new Scoreboard(player1, player2);
        }

        public void AwardPointToPlayer(Player player)
        {
            player.Score++;
        }

        public string GetCurrentScore()
        {
            return scoreboard.Announce();
        }
    }
}


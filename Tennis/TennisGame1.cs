namespace Tennis
{
    internal class TennisGame1 : ITennisGame
    {
        private readonly Player player1;
        private readonly Player player2;
        private int player1Score;
        private int player2Score;
        private readonly Scoreboard scoreboard;

        public TennisGame1(Player player1, Player player2)
        {
            this.player1 = player1;
            this.player2 = player2;
            scoreboard = new Scoreboard(this);
        }

        public void AwardPointToPlayer(Player player)
        {
            if (player == player1)
                player1Score += 1;
            else
                player2Score += 1;
        }

        public string GetCurrentScore()
        {
            return scoreboard.Announce();
        }

        private static string StringifyScore(int tempScore)
        {
            switch (tempScore)
            {
                case 0:
                    return "Love";
                case 1:
                    return "Fifteen";
                case 2:
                    return "Thirty";
                case 3:
                    return "Forty";
            }
            return string.Empty;
        }
    }
}


namespace Tennis
{
    internal class TennisGame1 : ITennisGame
    {
        private readonly Player player1;
        private readonly Player player2;
        private int player1Score;
        private int player2Score;

        public TennisGame1(Player player1, Player player2)
        {
            this.player1 = player1;
            this.player2 = player2;
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
            if (player1Score == player2Score)
            {
                return player1Score > 2 ? "Deuce" : $"{StringifyScore(player1Score)}-All";
            }

            if (player1Score < 4 && player2Score < 4)
            { 
                return $"{StringifyScore(player1Score)}-{StringifyScore(player2Score)}";
            }

            var minusResult = player1Score - player2Score;
            if (minusResult == 1) return "Advantage player1";
            if (minusResult == -1) return "Advantage player2";
            if (minusResult >= 2) return "Win for player1";
            return "Win for player2";
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


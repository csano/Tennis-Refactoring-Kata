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
            var score = "";
            var tempScore = 0;
            if (player1Score == player2Score)
            {
                score = StringifyScore(player1Score);
                if (player1Score > 2)
                {
                    score = "Deuce";
                }
                else
                {
                    score += "-All";
                }
            }
            else if (player1Score >= 4 || player2Score >= 4)
            {
                var minusResult = player1Score - player2Score;
                if (minusResult == 1) score = "Advantage player1";
                else if (minusResult == -1) score = "Advantage player2";
                else if (minusResult >= 2) score = "Win for player1";
                else score = "Win for player2";
            }
            else
            {
                for (var i = 1; i < 3; i++)
                {
                    if (i == 1) tempScore = player1Score;
                    else { score += "-"; tempScore = player2Score; }
                    score += StringifyScore(tempScore);
                }
            }
            return score;
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


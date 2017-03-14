namespace Tennis
{
    internal class Scoreboard
    {
        private readonly Player player1;
        private readonly Player player2;

        public Scoreboard(Player player1, Player player2)
        {
            this.player1 = player1;
            this.player2 = player2;
        }

        public string Announce()
        {
            var score = "";
            if (player1.Score == player2.Score)
            {
                return player1.Score > 2 ? "Deuce" : $"{StringifyScore(player1.Score)}-All";
            }
            if (player1.Score >= 4 || player2.Score >= 4)
            {
                var scoreDifference = player1.Score - player2.Score;
                if (scoreDifference == 1) score = "Advantage player1";
                else if (scoreDifference == -1) score = "Advantage player2";
                else if (scoreDifference >= 2) score = "Win for player1";
                else score = "Win for player2";
            }
            else
            {
                for (var i = 1; i < 3; i++)
                {
                    int tempScore;
                    if (i == 1) tempScore = player1.Score;
                    else
                    {
                        score += "-";
                        tempScore = player2.Score;
                    }
                    score += StringifyScore(tempScore);
                }
            }
            return score;
        }

        private static string StringifyScore(int tempScore)
        {
            return new[] {"Love", "Fifteen", "Thirty", "Forty"}[tempScore];
        }
    }
}
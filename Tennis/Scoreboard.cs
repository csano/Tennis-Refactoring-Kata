using System;

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
                score = StringifyScore(player1.Score);
                if (player1.Score > 2)
                {
                    score = "Deuce";
                }
                else
                {
                    score += "-All";
                }
            }
            else if (player1.Score >= 4 || player2.Score >= 4)
            {
                var minusResult = player1.Score - player2.Score;
                if (minusResult == 1) score = "Advantage player1";
                else if (minusResult == -1) score = "Advantage player2";
                else if (minusResult >= 2) score = "Win for player1";
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
            return String.Empty;
        }
    }
}
using System;

namespace Tennis
{
    internal class Scoreboard
    {
        private TennisGame1 tennisGame1;

        public Scoreboard(TennisGame1 tennisGame1)
        {
            this.tennisGame1 = tennisGame1;
        }

        public string Announce()
        {
            var score = "";
            if (tennisGame1.player1Score == tennisGame1.player2Score)
            {
                score = StringifyScore(tennisGame1.player1Score);
                if (tennisGame1.player1Score > 2)
                {
                    score = "Deuce";
                }
                else
                {
                    score += "-All";
                }
            }
            else if (tennisGame1.player1Score >= 4 || tennisGame1.player2Score >= 4)
            {
                var minusResult = tennisGame1.player1Score - tennisGame1.player2Score;
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
                    if (i == 1) tempScore = tennisGame1.player1Score;
                    else
                    {
                        score += "-";
                        tempScore = tennisGame1.player2Score;
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
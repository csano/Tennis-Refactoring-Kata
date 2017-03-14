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

        private Player GetPlayerInLead()
        {
            return player1.Score > player2.Score ? player1 : player2;
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
                return $"{(Math.Abs(scoreDifference) == 1 ? "Advantage" : "Win for")} {GetPlayerInLead().Name}";
            }
            else
            {
                return $"{StringifyScore(player1.Score)}-{StringifyScore(player2.Score)}";
            }
            return score;
        }

        private static string StringifyScore(int tempScore)
        {
            return new[] {"Love", "Fifteen", "Thirty", "Forty"}[tempScore];
        }
    }
}
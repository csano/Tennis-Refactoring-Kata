using System.Collections.Generic;
using System.Linq;

namespace Tennis
{
    internal class TennisGame1 : ITennisGame
    {
        private readonly Player player1;
        private readonly Player player2;
        private int player1Score;
        private int player2Score;
        private readonly Dictionary<Player, int> scores = new Dictionary<Player,int>();

        public TennisGame1(Player player1, Player player2)
        {
            this.player1 = player1;
            this.player2 = player2;
            scores.Add(player1, 0);
            scores.Add(player2, 0);
        }

        public void AwardPointToPlayer(Player player)
        {
            scores[player]++;
            if (player == player1)
                player1Score += 1;
            else
                player2Score += 1;
        }

        public string GetCurrentScore()
        {
            if (scores[player1] == scores[player2])
            {
                return player1Score > 2 ? "Deuce" : $"{StringifyScore(player1Score)}-All";
            }

            if (scores.All(x => x.Value < 4))
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


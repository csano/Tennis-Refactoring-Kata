namespace Tennis
{
    public class TennisGame3 : ITennisGame
    {
        private readonly Player player1;
        private readonly Player player2;
        private int p2;
        private int p1;
        private string p1N;
        private string p2N;

        public TennisGame3(Player player1, Player player2)
        {
            p1N = player1.Name;
            p2N = player2.Name;
            this.player1 = player1;
            this.player2 = player2;
        }

        public string GetScore()
        {
            string s;
            if ((p1 < 4 && p2 < 4) && (p1 + p2 < 6))
            {
                string[] p = { "Love", "Fifteen", "Thirty", "Forty" };
                s = p[p1];
                return (p1 == p2) ? s + "-All" : s + "-" + p[p2];
            }
            else
            {
                if (p1 == p2)
                    return "Deuce";
                s = p1 > p2 ? p1N : p2N;
                return ((p1 - p2) * (p1 - p2) == 1) ? "Advantage " + s : "Win for " + s;
            }
        }

        public void AwardPointToPlayer(Player player)
        {
            if (player == player1)
                this.p1 += 1;
            else
                this.p2 += 1;
        }

    }
}


namespace Tennis
{
    public interface ITennisGame
    {
        void AwardPointToPlayer(Player player);
        string GetScore();
    }
}


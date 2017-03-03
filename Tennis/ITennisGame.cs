namespace Tennis
{
    public interface ITennisGame
    {
        void AwardPointToPlayer(string playerName);
        string GetScore();
    }
}


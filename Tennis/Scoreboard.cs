using System.Linq;

namespace Tennis
{
    internal class Scoreboard
    {
        private readonly PlayerScore[] playerScores = new PlayerScore[2];

        public Scoreboard(Player player1, Player player2)
        {
            playerScores[0] = new PlayerScore(player1);
            playerScores[1] = new PlayerScore(player2);
        }

        public void GivePointToPlayer(Player player)
        {
            playerScores.First(x => x.Player == player).IncrementScore();
        }

        public string AnnounceScore()
        {
            var player1Score = playerScores[0];
            var player2Score = playerScores[1];

            var successCondition = new ScoringConditionEvaluator().Evaluate(player1Score, player2Score);
            return successCondition != null ? new RuleStringConverter().Convert(successCondition, player1Score, player2Score) : string.Empty;
        }
    }
}
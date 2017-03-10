using System;

namespace Tennis
{
    internal interface IScoringConditionStringConverter
    {
        Type ConditionType { get; }
        string Convert(PlayerScore player1Score, PlayerScore player2Score);
    }
}
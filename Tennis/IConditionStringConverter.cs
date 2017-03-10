using System;

namespace Tennis
{
    internal interface IConditionStringConverter
    {
        Type ConditionType { get; }
        string Convert(PlayerScore player1Score, PlayerScore player2Score);
    }
}
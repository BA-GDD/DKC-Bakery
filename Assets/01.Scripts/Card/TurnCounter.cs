using System;
using System.Collections.Generic;
using UnityEngine;

public enum TurnType
{
    Player,
    Enemy
}

public static class TurnCounter
{
    private static TurnType _currentTurnType = TurnType.Player;
    public static TurnType CurrentTurnType { get; private set; }

    private static int _turnCount = 0;
    public static int TurnCount { get; private set; }

    public static Action EnemyTurnStartEvent;
    public static Action EnemyTurnEndEvent;

    public static Action PlayerTurnStartEvent;
    public static Action PlayerTurnEndEvent;

    public static void ChangeTurn()
    {
        if(CurrentTurnType == TurnType.Player)
        {
            EnemyTurnEndEvent?.Invoke();
            PlayerTurnStartEvent?.Invoke();
        }
        else
        {
            PlayerTurnEndEvent?.Invoke();
            EnemyTurnStartEvent?.Invoke();
        }
    }
}

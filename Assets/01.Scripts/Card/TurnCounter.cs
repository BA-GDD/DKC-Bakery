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
    public static TurnType CurrentTurnType { get; private set; } = TurnType.Player;
    public static int TurnCount { get; private set; }
    public static int RoundCount { get; private set; }

    public static Action RoundEndEvent;
    public static Action RoundStartEvent;

    public static Action EnemyTurnStartEvent;
    public static Action EnemyTurnEndEvent;

    public static Action PlayerTurnStartEvent;
    public static Action PlayerTurnEndEvent;

    private static TurnCounting _turnCounting;
    public static TurnCounting TurnCounting
    {
        get
        {
            if(_turnCounting != null)
            {
                return _turnCounting;
            }
            _turnCounting = GameObject  .FindObjectOfType<TurnCounting>();
            return _turnCounting;
        }
    }

    public static void ChangeRound()
    {
        if(RoundCount != 0)
        {
            RoundEndEvent?.Invoke();
        }

        RoundCount++;
        RoundStartEvent?.Invoke();
    }

    public static void ChangeTurn()
    {
        TurnCount++;

        if(CurrentTurnType == TurnType.Player)
        {
            EnemyTurnEndEvent?.Invoke();
            PlayerTurnStartEvent?.Invoke();

            CurrentTurnType = TurnType.Enemy;
        }
        else
        {
            PlayerTurnEndEvent?.Invoke();
            EnemyTurnStartEvent?.Invoke();

            CurrentTurnType = TurnType.Player;
        }
    }
}

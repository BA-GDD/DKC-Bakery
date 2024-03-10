using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class CostCalculator
{
    public static int CurrentMoney { get; private set; } = 10;
    public static Action<int> MoneyChangeEvent;

    public static void UseCost(int toUseCost)
    {
        CurrentMoney = Mathf.Clamp(CurrentMoney - toUseCost, 0, int.MaxValue);
        MoneyChangeEvent?.Invoke(CurrentMoney);
    }

    public static bool CanUseCost(int toUseCost)
    {
        return toUseCost <= CurrentMoney;
    }
}

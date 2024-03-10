using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CostCalculator
{
    private static int money;
    public static int CurrentMoney => money;

    public static void UseCost(int toUseCost)
    {

    }

    public static bool CanUseCost(int toUseCost)
    {
        return toUseCost > CurrentMoney;
    }
}

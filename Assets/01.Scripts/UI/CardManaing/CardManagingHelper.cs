using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CardManagingHelper
{
    public static int GetCardCost(CardShameElementSO shameData)
    {
        return shameData.cardShameDataList[shameData.cardLevel].list.FirstOrDefault(x => x.cardShameType == CardShameType.Cost).currentShame;
    }

    public static float GetCardDamage(CardShameElementSO shameData)
    {
        return shameData.cardShameDataList[shameData.cardLevel].list.FirstOrDefault(x => x.cardShameType == CardShameType.Damage).currentShame;
    }

    public static float GetCardBuffValue(CardShameElementSO shameData)
    {
        return shameData.cardShameDataList[shameData.cardLevel].list.FirstOrDefault(x => x.cardShameType == CardShameType.Buff).currentShame;
    }

    public static float GetCardDeBuffValue(CardShameElementSO shameData)
    {
        return shameData.cardShameDataList[shameData.cardLevel].list.FirstOrDefault(x => x.cardShameType == CardShameType.Debuff).currentShame;
    }

    public static float GetCardAbillityTurn(CardShameElementSO shameData)
    {
        return shameData.cardShameDataList[shameData.cardLevel].list.FirstOrDefault(x => x.cardShameType == CardShameType.Turn).currentShame;
    }
}

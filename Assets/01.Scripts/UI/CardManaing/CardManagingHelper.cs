using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CardManagingHelper
{
    public static int GetCardCost(CardShameElementSO shameData)
    {
        return shameData.cardShameDataList[0][shameData.cardLevel - 1].FirstOrDefault(x => x.cardShameType == CardShameType.Cost).currentShame;
    }

    public static int GetCardShame(CardShameElementSO shameData, CardShameType type, int combineLevel)
    {
        return shameData.cardShameDataList[combineLevel][shameData.cardLevel - 1].FirstOrDefault(x => (x.cardShameType & type) != 0).currentShame;
    }

    public static int GetAfterLevelShame(CardShameElementSO shameData, CardShameType type, int combineLevel)
    {
        if (shameData.cardLevel >= 5) return 0;

        return shameData.cardShameDataList[combineLevel][shameData.cardLevel].FirstOrDefault(x => (x.cardShameType & type) != 0).currentShame;
    }

    public static CardShameData GetCardShameData(CardShameElementSO shameData, CardShameType type, int combineLevel)
    {
        return shameData.cardShameDataList[combineLevel][shameData.cardLevel].FirstOrDefault(x => x.cardShameType == type);
    }
}

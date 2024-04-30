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
}

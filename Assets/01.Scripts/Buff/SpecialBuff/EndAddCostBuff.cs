using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAddCostBuff : SpecialBuff, IOnRoundStart
{
    public int addCostValue = 3;


    public void RoundStart()
    {
        CostCalculator.GetCost(addCostValue);
        SetIsComplete(true);
    }
}

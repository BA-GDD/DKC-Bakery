using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingBuff : SpecialBuff
{
    public int healingAmount;
    public int turnDuration;

    public override void Active()
    {
        entity.HealthCompo.ApplyHeal(Mathf.RoundToInt(entity.HealthCompo.maxHealth * healingAmount * 0.01f));
        turnDuration--;

        if(turnDuration <= 0)
        {
            SetIsComplete(true);
        }
    }
}

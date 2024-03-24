using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LightningCardBase : CardBase
{
    protected void ExtraAttack()
    {
        foreach (var e in battleController.onFieldMonsterList)
        {
            Debug.Log("ddd");
            try
            {
                e.HealthCompo.AilmentStat.UsedToAilment(AilmentEnum.Shocked);
            }
            catch (Exception ex)
            {
                Debug.Log(e);
            }
        }
    }
}

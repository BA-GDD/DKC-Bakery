using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildAttackBuff : SpecialBuff, IOnTakeDamage
{
    private List<Health> appliedEnemy = new();
    public override void Active()
    {
    }

    public void TakeDamage(Health health)
    {
        if (appliedEnemy.Contains(health)) return;

        appliedEnemy.Add(health);
        health.AilmentStat.ApplyAilments(AilmentEnum.Chilled);
    }
}

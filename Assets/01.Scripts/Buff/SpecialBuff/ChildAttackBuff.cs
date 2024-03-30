using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildAttackBuff : SpecialBuff, IOnTakeDamage
{
    private List<Health> appliedEnemy = new();

    public void TakeDamage(Health health)
    {
        if (appliedEnemy.Contains(health)) return;

        if (appliedEnemy.Count <= 0) entity.BeforeChainingEvent.AddListener(EndAttack);

        appliedEnemy.Add(health);
        health.AilmentStat.ApplyAilments(AilmentEnum.Chilled);
    }

    private void EndAttack()
    {
        SetIsComplete(true);
    }

    public override void SetIsComplete(bool value)
    {
        base.SetIsComplete(value);
        if(value)
            entity.BeforeChainingEvent.RemoveListener(EndAttack);
    }
}

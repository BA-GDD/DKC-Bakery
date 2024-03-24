using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockedAilment : Ailment
{
    public ShockedAilment(AilmentStat stat, Health health, AilmentEnum ailment) : base(stat, health, ailment)
    {
    }

    public override void UseAilment()
    {
        duration = 0;
        if (duration <= 0)
        {
            _stat.CuredAilment(_ailment);
        }
        _health.AilmentByDamage(Mathf.RoundToInt(_health.maxHealth * 0.07f));
    }
}

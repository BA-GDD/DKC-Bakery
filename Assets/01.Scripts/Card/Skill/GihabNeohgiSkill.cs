using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GihabNeohgiSkill : CardBase
{
    [SerializeField] private int duration;
    public override void Abillity()
    {
        Player.BuffStatCompo.AddBuff(buffSO, duration);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaesalhariSkill : ChilledCardBase, IUseEffectAnim
{
    public override void Abillity()
    {
        IsActivingAbillity = true;
        Player.BuffStatCompo.AddBuff(buffSO, 2);

        Player.VFXManager.OnEndEffectEvent += HandleEffectEnd;
    }

    public void HandleAnimationCall()
    {

    }

    public void HandleEffectEnd()
    {
        IsActivingAbillity = false;
    }
}   

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaesalhariSkill : ChilledCardBase, ISkillEffectAnim
{
    public override void Abillity()
    {
        IsActivingAbillity = true;
        Player.UseAbility(this);
        Player.VFXManager.PlayParticle(CardInfo);
        Player.VFXManager.OnEndEffectEvent += HandleEffectEnd;
    }

    public void HandleAnimationCall()
    {

    }

    public void HandleEffectEnd()
    {
        Player.VFXManager.OnEndEffectEvent -= HandleEffectEnd;
        Player.EndAbility();
        Player.VFXManager.EndParticle(CardInfo);
        IsActivingAbillity = false;
    }
}   

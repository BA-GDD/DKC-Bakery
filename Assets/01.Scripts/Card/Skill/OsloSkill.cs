using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsloSkill : CardBase,ISkillEffectAnim
{
    public override void Abillity()
    {
        IsActivingAbillity = true;
        Player.UseAbility(this);
        Player.VFXManager.PlayParticle(CardInfo);
        Player.BuffStatCompo.AddBuff(buffSO, 0);
        Player.VFXManager.OnEndEffectEvent += HandleEffectEnd;
    }

    public void HandleAnimationCall()
    {
    }

    public void HandleEffectEnd()
    {
        Player.EndAbility();
        Player.VFXManager.OnEndEffectEvent -= HandleEffectEnd;
        Player.VFXManager.EndParticle(CardInfo);
        IsActivingAbillity = true;
    }
}

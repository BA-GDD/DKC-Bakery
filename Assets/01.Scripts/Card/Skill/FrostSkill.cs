using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostSkill : ChilledCardBase ,ISkillEffectAnim
{
    public override void Abillity()
    {
        IsActivingAbillity = true;
        Player.UseAbility(this);
        Player.OnAnimationCall += HandleAnimationCall;
        Player.VFXManager.OnEndEffectEvent += HandleEffectEnd;
    }

    public void HandleAnimationCall()
    {
        Player.VFXManager.PlayParticle(CardInfo);
        StartCoroutine(ChiledCor());
        Player.OnAnimationCall -= HandleAnimationCall;
    }
    private IEnumerator ChiledCor()
    {
        yield return new WaitForSeconds(0.3f);

        foreach (var i in battleController.onFieldMonsterList)
        {
            i?.HealthCompo.AilmentStat.ApplyAilments(AilmentEnum.Chilled);
        }
    }
    public void HandleEffectEnd()
    {
        Player.EndAbility();
        Player.VFXManager.EndParticle(CardInfo);
        IsActivingAbillity = false;
        Player.VFXManager.OnEndEffectEvent -= HandleEffectEnd;
    }
}

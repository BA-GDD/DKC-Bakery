using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : CardBase, ISkillEffectAnim
{
    public int _healingAmount = 20;

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
        StartCoroutine(HealCor());
        Player.OnAnimationCall -= HandleAnimationCall;
    }

    public void HandleEffectEnd()
    {
        Player.EndAbility();
        Player.VFXManager.EndParticle(CardInfo);
        IsActivingAbillity = false;
        Player.VFXManager.OnEndEffectEvent -= HandleEffectEnd;
    }

    private IEnumerator HealCor()
    {
        yield return new WaitForSeconds(0.3f);

        Player.HealthCompo.ApplyHeal(Mathf.RoundToInt(Player.HealthCompo.maxHealth * _healingAmount * 0.01f));
    }
}

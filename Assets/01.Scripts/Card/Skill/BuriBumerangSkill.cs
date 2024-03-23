using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuriBumerangSkill : CardBase, IUseEffectAnim
{
    public override void Abillity()
    {
        IsActivingAbillity = true;
        Player.target = battleController.onFieldMonsterList[3];
        Player.UseAbility(this);
        Player.OnAnimationCall += HandleAnimationCall;
        Player.VFXManager.OnEndEffectEvent += HandleEffectEnd;
    }

    public void HandleAnimationCall()
    {
        Player.VFXManager.PlayParticle(CardInfo);
        StartCoroutine(AttackCor());
        Player.OnAnimationCall -= HandleAnimationCall;
    }

    public void HandleEffectEnd()
    {
        Player.EndAbility();
        Player.VFXManager.EndParticle(CardInfo);
        IsActivingAbillity = false;
        Player.VFXManager.OnEndEffectEvent -= HandleEffectEnd;
    }

    private IEnumerator AttackCor()
    {
        yield return new WaitForSeconds(0.3f);

        Player.target.HealthCompo.ApplyDamage(4, Player);
        Player.HealthCompo.ApplyHeal(Mathf.RoundToInt(Player.HealthCompo.maxHealth * 0.07f));

        yield return new WaitForSeconds(1.2f);

        Player.target.HealthCompo.ApplyDamage(4, Player);
        Player.HealthCompo.ApplyHeal(Mathf.RoundToInt(Player.HealthCompo.maxHealth * 0.07f));
    }
}

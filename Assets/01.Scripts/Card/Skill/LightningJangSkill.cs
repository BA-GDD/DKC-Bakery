using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningJangSkill : LightningCardBase, ISkillEffectAnim
{
    public override void Abillity()
    {
        IsActivingAbillity = true;
        Player.target = battleController.onFieldMonsterList[0];
        Player.UseAbility(this);
        Player.OnAnimationCall += HandleAnimationCall;
        Player.VFXManager.OnEndEffectEvent += HandleEffectEnd;
    }

    public void HandleAnimationCall()
    {
        Player.VFXManager.PlayParticle(CardInfo, Player.target.transform.position);
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
        yield return new WaitForSeconds(0.1f);

        Player.target.HealthCompo.ApplyDamage(10, Player);

        ExtraAttack();
    }
}

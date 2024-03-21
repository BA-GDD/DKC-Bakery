using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleStepSkill : CardBase, ISkillEffectAnim
{
    public override void Abillity()
    {
        IsActivingAbillity = true;
        Player.target = battleController.onFieldMonsterList[1];
        Player.UseAbility(this, true);
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
        Player.MoveToOriginPos();
        Player.VFXManager.EndParticle(CardInfo);
        IsActivingAbillity = false;
        Player.VFXManager.OnEndEffectEvent -= HandleEffectEnd;
    }

    private IEnumerator AttackCor()
    {
        for(int i = 0; i < 2; ++i)
        {
            yield return new WaitForSeconds(0.3f);
            battleController.onFieldMonsterList[0].HealthCompo.ApplyDamage(3, Player);
        }

        yield return new WaitForSeconds(2.4f);

        battleController.onFieldMonsterList[0].HealthCompo.ApplyDamage(5, Player);
    }
}

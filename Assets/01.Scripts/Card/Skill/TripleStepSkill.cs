using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleStepSkill : CardBase, ISkillEffectAnim
{
    public override void Abillity()
    {
        IsActivingAbillity = true;
        int targetIdx = -1;
        foreach (var e in battleController.onFieldMonsterList)
        {
            if (e != null)
            {
                targetIdx++;
            }
        }
        Player.target = battleController.onFieldMonsterList[targetIdx];
        Player.UseAbility(this, true);
        Player.OnAnimationCall += HandleAnimationCall;
        Player.VFXManager.OnEndEffectEvent += HandleEffectEnd;
    }

    public void HandleAnimationCall()
    {
        Player.VFXManager.PlayParticle(CardInfo, Player.forwardTrm.position + new Vector3(2.5f, 1.8f, 0f)); ;
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
            yield return new WaitForSeconds(0.2f);
            Player.target.HealthCompo.ApplyDamage(3, Player);
        }

        yield return new WaitForSeconds(2.6f);

        Player.target.HealthCompo.ApplyDamage(5, Player);
    }
}

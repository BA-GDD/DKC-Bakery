using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyStreamBurstSkill : CardBase, IUseEffectAnim
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
        yield return new WaitForSeconds(1f);

        for(int i = 0; i < 5; ++i)
        {
            yield return new WaitForSeconds(0.3f);

            foreach(var e in battleController.onFieldMonsterList)
            {
                Player.Attack(e, 2);
            }
        }
    }
}

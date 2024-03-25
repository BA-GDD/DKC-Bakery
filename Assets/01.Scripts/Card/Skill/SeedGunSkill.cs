using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGunSkill : CardBase, ISkillEffectAnim
{
    public override void Abillity()
    {
        IsActivingAbillity = true;
        int targetIdx = -1;
        foreach(var e in battleController.onFieldMonsterList)
        {
            if(e != null)
            {
                targetIdx++;
            }
        }
        Player.target = battleController.onFieldMonsterList[targetIdx];
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

        for(int i = 0; i < 3; ++i)
        {
            Debug.Log(i);
            yield return new WaitForSeconds(0.2f);
            Player.target?.HealthCompo.ApplyDamage(3, Player);
        }
    }
}

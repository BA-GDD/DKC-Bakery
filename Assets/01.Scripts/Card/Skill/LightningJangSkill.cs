using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningJangSkill : LightningCardBase, ISkillEffectAnim
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

        Player.target.HealthCompo.ApplyDamage(13, Player);

        if(Random.value * 100 >= 30f)
        {
            Player.target.HealthCompo.AilmentStat.ApplyAilments(AilmentEnum.Shocked);
        }

        ExtraAttack();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoSkill : CardBase
{
    public override void Abillity()
    {
        IsActivingAbillity = true;
        Player.UseAbility(this);
        Player.OnAnimationCall += HandleAnimationCall;
        Player.VFXManager.OnEndEffectEvent += HandleEffectEnd;
    }
    private void HandleAnimationCall()
    {
        Player.VFXManager.PlayParticle(CardInfo);
        StartCoroutine(AttackCor());
        Player.OnAnimationCall -= HandleAnimationCall;
    }

    private void HandleEffectEnd()
    {
        Player.EndAbility();
        Player.VFXManager.EndParticle(CardInfo);
        IsActivingAbillity = false;
        Player.VFXManager.OnEndEffectEvent -= HandleEffectEnd;
    }

    private IEnumerator AttackCor()
    {
        yield return new WaitForSeconds(0.3f);
        List<Enemy> list = battleController.onFieldMonsterList;
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.26f);
            foreach (var e in list)
            {
                e.HealthCompo.ApplyDamage(2, Player);
            }
        }
    }
}
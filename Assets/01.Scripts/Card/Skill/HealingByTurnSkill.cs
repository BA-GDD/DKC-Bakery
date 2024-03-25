using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingByTurnSkill : CardBase, ISkillEffectAnim
{
    public int healingAmount;

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
        // 일단 치유 상태 만들어 놨고 민수가 적용시키는거 만들면 적용시켜주면 됨

        yield return null;
    }
}

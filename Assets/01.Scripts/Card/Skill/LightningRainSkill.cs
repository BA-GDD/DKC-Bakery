using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningRainSkill : LightningCardBase, ISkillEffectAnim
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
        SoundManager.PlayAudio(soundEffect);
        Player.VFXManager.PlayParticle(CardInfo, Player.transform.position, (int)CombineLevel);
        StartCoroutine(AttackCor());
        Player.OnAnimationCall -= HandleAnimationCall;
    }

    public void HandleEffectEnd()
    {
        Player.EndAbility();
        Player.VFXManager.EndParticle(CardInfo, (int)CombineLevel);
        IsActivingAbillity = false;
        Player.VFXManager.OnEndEffectEvent -= HandleEffectEnd;
    }

    private IEnumerator AttackCor()
    {
        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < 3; ++i)
        {
            foreach (var e in Player.GetSkillTargetEnemyList[this])
            {
                e?.HealthCompo.ApplyDamage(GetDamage(CombineLevel)[0], Player);
                if (e != null)
                {
                    GameObject obj = Instantiate(CardInfo.hitEffect.gameObject, Player.GetSkillTargetEnemyList[this][0].transform.position, Quaternion.identity);
                    Destroy(obj, 1.0f);
                }
            }
            ExtraAttack();
            yield return new WaitForSeconds(0.7f);
        }

        foreach(var e in Player.GetSkillTargetEnemyList[this])
        {
            RandomApplyShockedAilment(e, 20f);
        }
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningJangSkill : LightningCardBase, ISkillEffectAnim
{
    private Color minimumColor = new Color(255,255,255,.1f);
    private Color maxtimumColor = new Color(255, 255, 255, 1.0f);

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
        Player.OnAnimationCall += HandleAnimationCall;
        Player.VFXManager.OnEndEffectEvent += HandleEffectEnd;
        Player.UseAbility(this,false,true);

        Player.VFXManager.BackgroundColor(Color.gray);

        if(Player.target != null)
        {
            GameObject obj = Instantiate(CardInfo.hitEffect.gameObject, Player.target.transform.position, Quaternion.identity);
            Destroy(obj, 1.0f);

            foreach (var m in battleController.onFieldMonsterList)
            {
                if (m == Player.target) continue;
                m.SpriteRendererCompo.DOColor(minimumColor, .5f);
            }
        }
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

        foreach (var m in battleController.onFieldMonsterList)
        {
            if (m == null) continue;
            m.SpriteRendererCompo.DOColor(maxtimumColor, .5f);
        }
    }

    private IEnumerator AttackCor()
    {
        yield return new WaitForSeconds(0.1f);

        Player.target.HealthCompo.ApplyDamage(GetDamage(CombineLevel), Player);

        if(Random.value * 100 >= 30f)
        {
            Player.target.HealthCompo.AilmentStat.ApplyAilments(AilmentEnum.Shocked);
        }

        FeedbackManager.Instance.EndSpeed = 3.0f;
        FeedbackManager.Instance.ShakeScreen(2.0f);

        ExtraAttack();
    }
}

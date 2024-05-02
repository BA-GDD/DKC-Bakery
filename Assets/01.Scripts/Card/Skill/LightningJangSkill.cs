using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningJangSkill : LightningCardBase, ISkillEffectAnim
{
    private Color minimumColor = new Color(255, 255, 255, .1f);
    private Color maxtimumColor = new Color(255, 255, 255, 1.0f);


    public override void Abillity()
    {
        IsActivingAbillity = true;
        targets = Player.GetSkillTargetEnemyList[this];
        Player.OnAnimationCall += HandleAnimationCall;
        Player.VFXManager.OnEndEffectEvent += HandleEffectEnd;
        Player.UseAbility(this, false, true);

        Player.VFXManager.BackgroundColor(Color.gray);

        if (targets.Count > 0)
        {
            //GameObject obj = Instantiate(CardInfo.hitEffect.gameObject, Player.target.transform.position, Quaternion.identity);
            //Destroy(obj, 1.0f);

            foreach (var m in battleController.onFieldMonsterList)
            {
                if (targets.Contains(m)) continue;
                m?.SpriteRendererCompo.DOColor(minimumColor, .5f);
            }
        }
    }

    public void HandleAnimationCall()
    {
        foreach (var e in targets)
        {
            Vector3 pos = e.transform.position;
            Player.VFXManager.PlayParticle(CardInfo, pos, (int)CombineLevel);
            CameraController.Instance.GetVCam(0.1f).SetCameraWithClamp(pos, 5.1f, 0.25f);
        }
        if (targets.Count > 0)
        {
            StartCoroutine(AttackCor());
        }
        else
            Player.VFXManager.PlayParticle(CardInfo, battleController.enemySpawnPos[0], (int)CombineLevel);

        Player.OnAnimationCall -= HandleAnimationCall;
    }

    public void HandleEffectEnd()
    {
        Player.EndAbility();
        Player.VFXManager.EndParticle(CardInfo, (int)CombineLevel);
        IsActivingAbillity = false;
        Player.VFXManager.OnEndEffectEvent -= HandleEffectEnd;

        foreach (var m in battleController.onFieldMonsterList)
        {
            if (targets.Contains(m)) continue;
            m?.SpriteRendererCompo.DOColor(maxtimumColor, .5f);
        }
    }

    private IEnumerator AttackCor()
    {
        yield return new WaitForSeconds(0.1f);

        foreach (var e in targets)
        {
            e.HealthCompo.ApplyDamage(GetDamage(CombineLevel)[0], Player);
            if (Random.value * 100 >= 30f)
            {
                e.HealthCompo.AilmentStat.ApplyAilments(AilmentEnum.Shocked);
            }
            GameObject obj = Instantiate(CardInfo.hitEffect.gameObject, e.transform.position, Quaternion.identity);
            Destroy(obj, 1.0f);
        }


        FeedbackManager.Instance.EndSpeed = 3.0f;
        FeedbackManager.Instance.ShakeScreen(2.0f);

        ExtraAttack();
    }
}

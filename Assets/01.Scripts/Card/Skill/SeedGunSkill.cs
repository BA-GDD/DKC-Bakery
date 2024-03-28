using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGunSkill : CardBase, ISkillEffectAnim
{
    private Color minimumColor = new Color(255, 255, 255, .1f);
    private Color maxtimumColor = new Color(255, 255, 255, 1.0f);

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
        
        foreach (var e in battleController.onFieldMonsterList)
        {
            if (e == Player.target) continue;
            e.SpriteRendererCompo.DOColor(minimumColor, 0.5f);
        }

            GameObject obj = Instantiate(CardInfo.targetEffect.gameObject, Player.target.transform.position, Quaternion.identity);
        Destroy(obj, 1.0f);
    }

    public void HandleAnimationCall()
    {
        Player.VFXManager.PlayParticle(CardInfo, (int)CombineLevel);
        StartCoroutine(AttackCor());
        Player.OnAnimationCall -= HandleAnimationCall;
    }

    public void HandleEffectEnd()
    {
        Player.EndAbility();
        Player.VFXManager.EndParticle(CardInfo, (int)CombineLevel);
        IsActivingAbillity = false;
        Player.VFXManager.OnEndEffectEvent -= HandleEffectEnd;

        foreach (var e in battleController.onFieldMonsterList)
        {
            if (e == null) continue;
            e.SpriteRendererCompo.DOColor(maxtimumColor, 0.5f);
        }
    }

    private IEnumerator AttackCor()
    {
        yield return new WaitForSeconds(1f);

        for(int i = 0; i < 3; ++i)
        {
            yield return new WaitForSeconds(0.15f);
            Player.target.HealthCompo.ApplyDamage(GetDamage(CombineLevel), Player);

            GameObject obj = Instantiate(CardInfo.hitEffect.gameObject, Player.target.transform.position, Quaternion.identity);
            Destroy(obj, 1.0f);

            float randNumX = UnityEngine.Random.Range(-.5f, .5f);
            float randNumY = UnityEngine.Random.Range(-.5f, .5f);
            FeedbackManager.Instance.ShakeScreen(new Vector3(randNumX, randNumY, 0.0f));

            Debug.Log(i);
            
            yield return new WaitForSeconds(0.2f);
            Player.target?.HealthCompo.ApplyDamage(GetDamage(CombineLevel), Player);
        }
    }
}

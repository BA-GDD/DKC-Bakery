using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyStreamBurstSkill : CardBase, ISkillEffectAnim
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
        Player.VFXManager.PlayParticle(CardInfo, (int)CombineLevel);
        FeedbackManager.Instance.EndSpeed = 1.5f;
        FeedbackManager.Instance.ShakeScreen(2.0f);
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
        yield return new WaitForSeconds(1f);

        for(int i = 0; i < 5; ++i)
        {
            yield return new WaitForSeconds(0.3f);

            float randNumX = UnityEngine.Random.Range(-.5f, .5f);
            float randNumY = UnityEngine.Random.Range(-.5f, .5f);
            FeedbackManager.Instance.ShakeScreen(new Vector3(randNumX, randNumY, 0.0f));
            
            foreach (var e in battleController.onFieldMonsterList)
            {
                e?.HealthCompo.ApplyDamage(GetDamage(CombineLevel), Player);

                if(e != null)
                {
                    GameObject obj = Instantiate(CardInfo.hitEffect.gameObject);
                    obj.transform.position = e.transform.position;
                    Destroy(obj, 1.0f);
                }
            }
        }
    }
}

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
        Player.VFXManager.PlayParticle(CardInfo,centerPos());
        StartCoroutine(AttackCor());
        FeedbackManager.Instance.EndSpeed = 1.0f;
        FeedbackManager.Instance.ShakeScreen(3);
        Player.OnAnimationCall -= HandleAnimationCall;
    }
    private Vector3 centerPos()
    {
        float minX = float.MaxValue, maxX = 0;
        float minY = float.MaxValue, maxY = 0;

        foreach (var e in battleController.onFieldMonsterList)
        {
            if(e != null)
            {
                Vector3 pos = e.transform.position;
                minX = Mathf.Min(minX, pos.x);
                maxX = Mathf.Max(maxX, pos.x);

                minY = Mathf.Min(minY, pos.y);
                maxY = Mathf.Min(maxY, pos.y);
            }
        }

        float x = (maxX - minX) * 0.5f + minX;
        float y = (maxY - minY) * 0.5f + minY;
        Vector2 dir = new Vector2(x, y);
        Debug.Log(dir);
        return dir;
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
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.26f);

            
            
            foreach (var e in battleController.onFieldMonsterList)
            {
                e?.HealthCompo.ApplyDamage(2, Player);
                
                if(e != null)
                {
                    Vector3 pos = e.transform.position;
                    GameObject obj = Instantiate(CardInfo.hitEffect.gameObject, pos, Quaternion.identity);
                    Destroy(obj, 1.0f);
                   
                    float randNumX = UnityEngine.Random.Range(-.5f, .5f);
                    float randNumY = UnityEngine.Random.Range(-.5f, .5f);
                    FeedbackManager.Instance.ShakeScreen(new Vector3(randNumX, randNumY, 0.0f));
                }
            }
        }
    }
}
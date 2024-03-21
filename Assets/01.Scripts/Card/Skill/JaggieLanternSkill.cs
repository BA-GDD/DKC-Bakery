using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaggieLanternSkill : CardBase, ISkillEffectAnim
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
        Player.VFXManager.PlayParticle(CardInfo);
        StartCoroutine(AttackCor());
        Player.OnAnimationCall -= HandleAnimationCall;
    }

    private Vector3 centerPos()
    {
        float minX = float.MaxValue, maxX = 0;
        float minY = float.MaxValue, maxY = 0;

        foreach (var e in battleController.onFieldMonsterList)
        {
            Vector3 pos = e.transform.position;
            minX = Mathf.Min(minX, pos.x);
            maxX = Mathf.Max(maxX, pos.x);

            minY = Mathf.Min(minY, pos.y);
            maxY = Mathf.Min(maxY, pos.y);
        }

        float x = (maxX - minX) * 0.5f + minX;
        float y = (maxY - minY) * 0.5f + minY;
        Vector2 dir = new Vector2(x, y);
        Debug.Log(dir);
        return dir;
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
        yield return new WaitForSeconds(1.7f);

        foreach(var e in battleController.onFieldMonsterList)
        {
            e?.HealthCompo.ApplyDamage(15, Player);
        }
    }
}

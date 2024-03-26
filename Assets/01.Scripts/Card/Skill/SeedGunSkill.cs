using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGunSkill : CardBase, ISkillEffectAnim
{
    public override void Abillity()
    {
        IsActivingAbillity = true;
        Player.target = battleController.onFieldMonsterList[0];
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

    public void HandleEffectEnd()
    {
        Player.EndAbility();
        Player.VFXManager.EndParticle(CardInfo);
        IsActivingAbillity = false;
        Player.VFXManager.OnEndEffectEvent -= HandleEffectEnd;
    }

    private IEnumerator AttackCor()
    {
        yield return new WaitForSeconds(1.8f);

        for(int i = 0; i < 3; ++i)
        {
            yield return new WaitForSeconds(0.15f);
            Player.target.HealthCompo.ApplyDamage(3, Player);

            float randNumX = UnityEngine.Random.Range(-.5f, .5f);
            float randNumY = UnityEngine.Random.Range(-.5f, .5f);
            FeedbackManager.Instance.ShakeScreen(new Vector3(randNumX, randNumY, 0.0f));
        }
    }
}

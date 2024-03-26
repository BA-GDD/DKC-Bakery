using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonStrowberry : Enemy
{
    protected override void Start()
    {
        base.Start();
        target = BattleController.Player;
        VFXPlayer.OnEndEffect += () => turnStatus = TurnStatus.End;
    }

    public override void Attack()
    {
        OnAttackStart?.Invoke();
        VFXPlayer.PlayParticle(attackParticle.particle, attackParticle.duration);
        StartCoroutine(AttackCor());
    }
    private IEnumerator AttackCor()
    {
        yield return new WaitForSeconds(1.4f);
        Vector3 vfxPos = attackParticle.particle.transform.position;
        Quaternion vfxQua = attackParticle.particle.transform.rotation;
        SetSeedVFXPos();

        for (int i = 0; i < 3; ++i)
        {
            yield return new WaitForSeconds(0.1f);
            target.HealthCompo.ApplyDamage(CharStat.GetDamage(), this);
        }
        attackParticle.particle.transform.position = vfxPos;
        attackParticle.particle.transform.rotation = vfxQua;

        turnStatus = TurnStatus.End;


        OnAttackEnd?.Invoke();
    }

    public override void SlowEntityBy(float percent)
    {
    }

    public override void TurnAction()
    {
        turnStatus = TurnStatus.Running;
        Attack();
    }

    public override void TurnEnd()
    {
    }

    public override void TurnStart()
    {
        turnStatus = TurnStatus.Ready;
    }

    protected override void HandleEndMoveToOriginPos()
    {
    }

    protected override void HandleEndMoveToTarget()
    {
    }
    private void SetSeedVFXPos()
    {
        Vector2 pos = (Vector2)attackParticle.particle.transform.position - new Vector2(1.64f, 0);
        Vector2 dir = (Vector2)target.transform.position - pos;

        attackParticle.particle.transform.position = pos + dir.normalized;
        attackParticle.particle.transform.right = dir.normalized * -1;
    }
}

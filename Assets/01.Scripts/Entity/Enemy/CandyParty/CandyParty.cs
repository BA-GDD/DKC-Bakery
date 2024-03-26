using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CandyParty : Enemy
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
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 5; ++i)
        {
            yield return new WaitForSeconds(0.3f);

            target.HealthCompo.ApplyDamage(CharStat.GetDamage(), this);
        }
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrMuddy : Enemy
{
    protected override void Start()
    {
        base.Start();
        target = BattleController.Player;
    }

    public override void Attack()
    {
        OnAttackStart?.Invoke();
        target.HealthCompo.ApplyDamage(CharStat.GetDamage(),this);
        MoveToOriginPos();
        OnAttackEnd?.Invoke();
    }

    public override void SlowEntityBy(float percent)
    {
    }

    public override void TurnAction()
    {
        turnStatus = TurnStatus.Running;
        MoveToTargetForward();
    }

    public override void TurnEnd()
    {
        base.TurnEnd();
    }

    public override void TurnStart()
    {
        base.TurnStart();

        turnStatus = TurnStatus.Ready;
    }

    protected override void HandleEndMoveToOriginPos()
    {
        turnStatus = TurnStatus.End;
    }

    protected override void HandleEndMoveToTarget()
    {
        Attack();
    }
}

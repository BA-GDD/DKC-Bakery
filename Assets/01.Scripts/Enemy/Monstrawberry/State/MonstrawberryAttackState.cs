using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstrawberryAttackState : EnemyState<MonstrawberryStateEnum>
{
    public MonstrawberryAttackState(Enemy enemyBase, EnemyStateMachine<MonstrawberryStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.StopImmediately(true);
    }

    public override void Exit()
    {
        _enemyBase.lastTimeAttacked = Time.time;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(_triggerCalled)
        {
            _stateMachine.ChangeState(MonstrawberryStateEnum.Battle);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyTreePrimaryAttackState : EnemyState<HoneyTreeStateEnum>
{
    public HoneyTreePrimaryAttackState(Enemy enemyBase, EnemyStateMachine<HoneyTreeStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
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
            _stateMachine.ChangeState(HoneyTreeStateEnum.Battle);
        }
    }
}

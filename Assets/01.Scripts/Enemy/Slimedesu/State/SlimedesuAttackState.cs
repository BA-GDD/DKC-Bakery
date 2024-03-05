using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimedesuAttackState : EnemyState<SlimedesuStateEnum>
{
    public SlimedesuAttackState(Enemy enemyBase, EnemyStateMachine<SlimedesuStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
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
            _stateMachine.ChangeState(SlimedesuStateEnum.Battle);
        }
    }
}
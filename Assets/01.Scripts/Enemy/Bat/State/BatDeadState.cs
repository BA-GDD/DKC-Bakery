using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatDeadState : EnemyState<BatStateEnum>
{
    public BatDeadState(Enemy enemyBase, EnemyStateMachine<BatStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _rigidbody.gravityScale = 10;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
}
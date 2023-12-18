using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyIdleState : EnemyState<SimpleEnemyStateEnum>
{
    public SimpleEnemyIdleState(Enemy enemyBase, EnemyStateMachine<SimpleEnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
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

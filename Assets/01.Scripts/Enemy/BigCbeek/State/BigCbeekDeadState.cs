using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCbeekDeadState : EnemyState<BigCbeekStateEnum>
{
    public BigCbeekDeadState(Enemy enemyBase, EnemyStateMachine<BigCbeekStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
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
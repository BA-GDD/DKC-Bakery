using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatStunedState : EnemyState<BatStateEnum>
{
    public BatStunedState(Enemy enemyBase, EnemyStateMachine<BatStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
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
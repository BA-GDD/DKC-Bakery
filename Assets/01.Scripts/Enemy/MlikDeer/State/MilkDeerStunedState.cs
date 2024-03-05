using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkDeerStunedState : EnemyState<MilkDeerStateEnum>
{
    public MilkDeerStunedState(Enemy enemyBase, EnemyStateMachine<MilkDeerStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
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
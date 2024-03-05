using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomDeadState : EnemyState<MushroomStateEnum>
{
    public MushroomDeadState(Enemy enemyBase, EnemyStateMachine<MushroomStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyBattleState : SimpleEnemyGroundState
{
    public SimpleEnemyBattleState(Enemy enemyBase, EnemyStateMachine<SimpleEnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Battle Enter");
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

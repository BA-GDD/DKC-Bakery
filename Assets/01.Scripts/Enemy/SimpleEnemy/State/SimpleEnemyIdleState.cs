using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SimpleEnemyIdleState : SimpleEnemyGroundState
{
    private bool _isAlreadyChange = false;
    public SimpleEnemyIdleState(Enemy enemyBase, EnemyStateMachine<SimpleEnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _isAlreadyChange = false;
        ChangeRoMoveWithDelay(Mathf.FloorToInt(_enemy.idleTime * 1000));
    }

    private async void ChangeRoMoveWithDelay(int timer)
    {
        await Task.Delay(timer);
        if (!_isAlreadyChange)
        {
            _stateMachine.ChangeState(SimpleEnemyStateEnum.Move);
        }
    }

    public override void Exit()
    {
        _isAlreadyChange = true;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WarmIdleState : WarmGroundState
{
    private bool _isAlreadyChange;
    public WarmIdleState(Enemy enemyBase, EnemyStateMachine<WarmStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        _isAlreadyChange = false;
        ChangeRoMoveWithDelay(Mathf.FloorToInt(_enemy.idleTime * 1000));
        base.Enter();
    }
    private async void ChangeRoMoveWithDelay(int timer)
    {
        await Task.Delay(timer);
        if (!_isAlreadyChange)
        {
            _stateMachine.ChangeState(WarmStateEnum.Move);
        }
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
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SlimedesuIdleState : SlimedesuGroundState
{
    private bool _isAlreadyChange = false;
    public SlimedesuIdleState(Enemy enemyBase, EnemyStateMachine<SlimedesuStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
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
            _stateMachine.ChangeState(SlimedesuStateEnum.Move);
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
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MilkDeerIdleState : MilkDeerGroundState
{
    private bool _isAlreadyChange;
    public MilkDeerIdleState(Enemy enemyBase, EnemyStateMachine<MilkDeerStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _isAlreadyChange = false;
        ChangeToMoveWithDelay();
    }
    private void ChangeToMoveWithDelay()
    {
        Task.Delay(Mathf.FloorToInt(_enemyBase.idleTime * 1000));
        if (!_isAlreadyChange)
        {
            _stateMachine.ChangeState(MilkDeerStateEnum.Move);
            return;
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
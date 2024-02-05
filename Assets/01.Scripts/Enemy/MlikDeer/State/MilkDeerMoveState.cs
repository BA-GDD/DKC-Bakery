using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkDeerMoveState : MilkDeerGroundState
{
    public MilkDeerMoveState(Enemy enemyBase, EnemyStateMachine<MilkDeerStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
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
        _enemyBase.SetVelocity(_enemyBase.FacingDirection * _enemyBase.moveSpeed, _rigidbody.velocity.y);

        if(_enemyBase.IsWallDetected() || !_enemyBase.IsGroundDetected())
        {
            _enemyBase.StopImmediately(false);
            _enemyBase.Flip();
            _stateMachine.ChangeState(MilkDeerStateEnum.Idle);
            return;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomMoveState : MushroomGroundState
{
    public MushroomMoveState(Enemy enemyBase, EnemyStateMachine<MushroomStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
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
        _enemy.SetVelocity(_enemy.moveSpeed * _enemy.FacingDirection, _rigidbody.velocity.y);

        if (_enemy.IsWallDetected() || !_enemy.IsGroundDetected())
        {
            _enemy.Flip();
            _enemy.StopImmediately(true);
            _stateMachine.ChangeState(MushroomStateEnum.Idle);
        }
    }
}

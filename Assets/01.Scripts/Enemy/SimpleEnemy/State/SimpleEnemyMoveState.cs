using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyMoveState : SimpleEnemyGroundState
{
    public SimpleEnemyMoveState(Enemy enemyBase, EnemyStateMachine<SimpleEnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Move Enter");

    }

    public override void Exit()
    {
        Debug.Log("Move Exit");
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
            _stateMachine.ChangeState(SimpleEnemyStateEnum.Idle);
        }
    }
}

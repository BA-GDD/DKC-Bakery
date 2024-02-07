using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMoveState : EnemyState<BatStateEnum>
{
    private Player _player;
    private Bat _enemy;
    public BatMoveState(Enemy enemyBase, EnemyStateMachine<BatStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = enemyBase as Bat;
    }

    public override void Enter()
    {
        base.Enter();
        _player = GameManager.Instance.Player;
        _enemy.targetAltitude = _enemy.transform.position.y;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _enemy.SetVelocity(_enemy.moveSpeed* _enemy.FacingDirection, _rigidbody.velocity.y);
        if (_enemy.IsWallDetected())
        {
            _enemy.Flip();
            _enemy.StopImmediately(true);
            _stateMachine.ChangeState(BatStateEnum.Idle);
        }
        float dis = _player.transform.position.x - _enemy.transform.position.x;
        if (dis < 10f)
        {
            _stateMachine.ChangeState(BatStateEnum.Battle);
        }
    }
}

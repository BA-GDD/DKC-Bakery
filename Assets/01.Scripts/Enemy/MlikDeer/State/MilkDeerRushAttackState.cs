using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkDeerRushAttackState : EnemyState<MilkDeerStateEnum>
{
    private MilkDeer _enemy;
    private bool _isAlreadyAttack;

    public MilkDeerRushAttackState(Enemy enemyBase, EnemyStateMachine<MilkDeerStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = enemyBase as MilkDeer;
    }

    public override void Enter()
    {
        base.Enter();
        _isAlreadyAttack = false;
    }

    public override void Exit()
    {
        base.Exit();
        _enemy.lastTimeAttacked = Time.time;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (!_isAlreadyAttack)
        {
            _isAlreadyAttack = _enemy.DamageCasterCompo.CastDamage();
        }
        _enemy.SetVelocity(_enemy.FacingDirection * _enemy.dashSpeed, _rigidbody.velocity.y);
        if (_enemy.IsWallDetected() || !_enemy.IsGroundDetected())
        {
            _stateMachine.ChangeState(MilkDeerStateEnum.Battle);
            return;
        }
    }
}
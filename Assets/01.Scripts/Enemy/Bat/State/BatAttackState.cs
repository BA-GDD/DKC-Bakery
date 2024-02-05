using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttackState : EnemyState<BatStateEnum>
{
    private Bat _enemy;
    public BatAttackState(Enemy enemyBase, EnemyStateMachine<BatStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = enemyBase as Bat;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        _enemy.StopImmediately(true);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _enemy.SetVelocity(_enemy.FacingDirection * _enemy.attackMoveSpeed, 0, true);
        if (_triggerCalled)
        {
            _stateMachine.ChangeState(BatStateEnum.Retreat);
        }
    }
}
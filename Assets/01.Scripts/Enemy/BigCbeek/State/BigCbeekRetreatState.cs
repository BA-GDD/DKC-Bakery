using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCbeekRetreatState : EnemyState<BigCbeekStateEnum>
{
    private BigCbeek _enemy;
    private Vector2 _targetDir;


    public BigCbeekRetreatState(Enemy enemyBase, EnemyStateMachine<BigCbeekStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = enemyBase as BigCbeek;
    }

    public override void Enter()
    {
        base.Enter();
        _targetDir = _enemy.attackPos - (Vector2)_enemy.transform.position;
    }

    public override void Exit()
    {
        base.Exit();
        _enemyBase.lastTimeAttacked = Time.time;
    }

    public override void UpdateState()
    {
        base.UpdateState();


        _enemy.SetVelocity(_targetDir.normalized * _enemy.attackMoveSpeed);
        if (Vector2.Distance(_enemy.attackPos, (Vector2)_enemy.transform.position) <= 0.5f)
        {
            _stateMachine.ChangeState(BigCbeekStateEnum.Battle);
        }
    }
}
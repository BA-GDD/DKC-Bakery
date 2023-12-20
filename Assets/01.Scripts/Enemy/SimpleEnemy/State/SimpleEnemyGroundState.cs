using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyGroundState : EnemyState<SimpleEnemyStateEnum>
{
    protected SimpleEnemy _enemy;
    protected Player _player;
    public SimpleEnemyGroundState(Enemy enemyBase, EnemyStateMachine<SimpleEnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = enemyBase as SimpleEnemy;
    }

    public override void Enter()
    {
        base.Enter();
        _player = GameManager.Instance.Player;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(_player.HealthCompo.isDead)
        {
            return;
        }

        RaycastHit2D hit = _enemy.IsPlayerDetected();

        float distance = Vector2.Distance(_player.transform.position, _enemy.transform.position);

        if(distance < 2f || (hit && !_enemy.IsWallDetected()))
        {
            //_stateMachine.ChangeState(SimpleEnemyStateEnum.Battle);
            return;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBattleState : EnemyState<BatStateEnum>
{
    private Player _player;
    private Bat _enemy;

    private float _moveDirection;
    public BatBattleState(Enemy enemyBase, EnemyStateMachine<BatStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = enemyBase as Bat;
    }

    public override void Enter()
    {
        base.Enter();
        _player = GameManager.Instance.Player;
        SetDirectionToEnemy();
    }
    private void SetDirectionToEnemy()
    {
        _enemy.FlipController(_player.transform.position.x - _enemy.transform.position.x);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _enemy.targetAltitude = _player.transform.position.y + 5f;

        float y = _enemy.targetAltitude - _enemy.transform.position.y;

        float dis = _player.transform.position.x - _enemy.transform.position.x;
        _moveDirection = Mathf.Sign(dis);
        float x = _enemy.moveSpeed * _moveDirection;
        if (Mathf.Abs(dis) < 6f)
        {
            x = 0;
            SetDirectionToEnemy();
            if (Time.time >= _enemy.lastTimeAttacked + _enemy.attackCooldown)
            {
                _stateMachine.ChangeState(BatStateEnum.Chase);
                return;
            }
        }
        _enemy.SetVelocity(x, y);
    }
}
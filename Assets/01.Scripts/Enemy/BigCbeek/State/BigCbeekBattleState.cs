using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCbeekBattleState : EnemyState<BigCbeekStateEnum>
{
    private Player _player;
    private BigCbeek _enemy;

    private int _moveDirection;
    private float _timer;
    public BigCbeekBattleState(Enemy enemyBase, EnemyStateMachine<BigCbeekStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = enemyBase as BigCbeek;
    }

    public override void Enter()
    {
        base.Enter();
        _player = GameManager.Instance.Player;
        _timer = _enemy.battleTime;
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
        _moveDirection = (int)Mathf.Sign(dis);
        float x = _enemy.moveSpeed * _moveDirection;
        if (Mathf.Abs(dis) < _enemy.runAwayDistance)
        {
            x = 0;
            SetDirectionToEnemy();
            _timer = _enemy.battleTime;
            if (Time.time >= _enemy.lastTimeAttacked + _enemy.attackCooldown && dis <= _enemy.attackDistance)
            {
                _stateMachine.ChangeState(BigCbeekStateEnum.Attack);
                return;
            }
        }
        else
        {
            _timer -= Time.deltaTime;
            if(_timer <= 0)
            {
                _stateMachine.ChangeState(BigCbeekStateEnum.Idle);
            }
        }
        _enemy.SetVelocity(x, y);
    }
}
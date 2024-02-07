using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyTreeBattleState : EnemyState<HoneyTreeStateEnum>
{
    private HoneyTree _enemy;
    private Player _player;

    private float _timer;

    public HoneyTreeBattleState(Enemy enemyBase, EnemyStateMachine<HoneyTreeStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = enemyBase as HoneyTree;
    }

    public override void Enter()
    {
        base.Enter();
        _timer = _enemy.battleTime;
        _player = GameManager.Instance.Player;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _enemy.FlipController(_player.transform.position.x - _enemy.transform.position.x);
        float distance = Vector2.Distance(_player.transform.position, _enemy.transform.position);
        if (distance < _enemy.attackDistance)
        {
            _timer = _enemy.battleTime;
            if (distance < 2f && CanPrimaryAttack())
            {
                _stateMachine.ChangeState(HoneyTreeStateEnum.PrimaryAttack);
            }
            else if(CanSpikeAttack())
            {
                _stateMachine.ChangeState(HoneyTreeStateEnum.SpikeAttack);
            }
        }
        else
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                _stateMachine.ChangeState(HoneyTreeStateEnum.Idle);
                return;
            }
        }

    }
    private bool CanPrimaryAttack()
    {
        return Time.time > _enemy.lastTimeAttacked + _enemy.attackCooldown;
    }
    private bool CanSpikeAttack()
    {
        RaycastHit2D hit = _enemy.IsGroundDetectedByPlayer(_player.transform.position);
        return Time.time > _enemy.lastTimeAttacked + _enemy.spikeAttackCooldown && hit;
    }
}
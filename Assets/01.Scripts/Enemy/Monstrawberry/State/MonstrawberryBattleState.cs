using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstrawberryBattleState : MonstrawberryGroundState
{
    private int _moveDirection;

    private float _timer;
    public MonstrawberryBattleState(Enemy enemyBase, EnemyStateMachine<MonstrawberryStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

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
        if (_enemy.HealthCompo.isDead)
        {
            _stateMachine.ChangeState(MonstrawberryStateEnum.Attack);
        }

        if (_player.transform.position.x > _enemy.transform.position.x)
            _moveDirection = 1;
        else
            _moveDirection = -1;
        _enemy.SetVelocity(_enemy.moveSpeed * _moveDirection, _rigidbody.velocity.y);

        RaycastHit2D hit = _enemy.IsPlayerDetected();

        if (hit && !_enemy.IsObstacleInLine(hit.distance))
        {
            _timer = _enemy.battleTime; //Ÿ�̸� ����

            if (hit.distance < _enemy.attackDistance && CanAttack())
            {
                _stateMachine.ChangeState(MonstrawberryStateEnum.Attack);
                return;
            }
        }
        float distance = Vector2.Distance(_player.transform.position, _enemy.transform.position);


        //���� �����̰ų� ���� �ٰŸ����.
        if (!_enemy.IsGroundDetected() || (distance <= _enemy.attackDistance))
        {
            _enemy.StopImmediately(false);  //�����ÿ� ����� �ִϸ��̼� ���¸� �ϳ� ������ ��.
            return;
        }

        if (_timer >= 0 && distance < _enemy.runAwayDistance)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            _stateMachine.ChangeState(MonstrawberryStateEnum.Idle); // �����ð��� �ʰ��ߴٸ� idle�� �̵�.
        }
    }

    private bool CanAttack()
    {
        return Time.time >= _enemy.lastTimeAttacked + _enemy.attackCooldown;
    }
}
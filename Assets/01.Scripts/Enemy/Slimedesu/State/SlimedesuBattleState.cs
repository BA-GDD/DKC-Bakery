using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimedesuBattleState : EnemyState<SlimedesuStateEnum>
{
    private int _moveDirection;
    private float _timer;

    private Player _player;
    private Slimedesu _enemy;
    public SlimedesuBattleState(Enemy enemyBase, EnemyStateMachine<SlimedesuStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = enemyBase as Slimedesu;
    }

    public override void Enter()
    {
        base.Enter();
        _player = GameManager.Instance.Player;
        _moveDirection = (int)Mathf.Sign(_player.transform.position.x - _enemy.transform.position.x);
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
        if (_enemy.HealthCompo.isDead)
        {
            _stateMachine.ChangeState(SlimedesuStateEnum.Attack);
        }

        if (Mathf.Abs(_player.transform.position.x - _enemy.transform.position.x) > 2f &&
            Mathf.Sign(_player.transform.position.x - _enemy.transform.position.x) != _moveDirection)
            _moveDirection *= -1;
        _enemy.SetVelocity(_enemy.moveSpeed * _moveDirection, _rigidbody.velocity.y);

        RaycastHit2D hit = _enemy.IsPlayerDetected();

        if (hit && !_enemy.IsObstacleInLine(hit.distance))
        {
            _timer = _enemy.battleTime; //Ÿ�̸� ����

            if (hit.distance < _enemy.attackDistance && CanAttack())
            {
                _stateMachine.ChangeState(SlimedesuStateEnum.Attack);
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
            _stateMachine.ChangeState(SlimedesuStateEnum.Idle); // �����ð��� �ʰ��ߴٸ� idle�� �̵�.
        }
    }

    private bool CanAttack()
    {
        return Time.time >= _enemy.lastTimeAttacked + _enemy.attackCooldown;
    }
}
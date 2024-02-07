using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBattleState : MushroomGroundState
{
    private readonly int waitAttackHash = Animator.StringToHash("AttackWait");

    private int _moveDirection;
    private float _timer;
    public MushroomBattleState(Enemy enemyBase, EnemyStateMachine<MushroomStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
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
        _enemy.AnimatorCompo.SetBool(waitAttackHash, false);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_enemy.HealthCompo.isDead)
        {
            _stateMachine.ChangeState(MushroomStateEnum.Attack);
        }

        if (Mathf.Abs(_player.transform.position.x - _enemy.transform.position.x) > 2f && 
            Mathf.Sign(_player.transform.position.x - _enemy.transform.position.x) != _moveDirection)
            _moveDirection *= -1;
        _enemy.SetVelocity(_enemy.moveSpeed * _moveDirection, _rigidbody.velocity.y);

        RaycastHit2D hit = _enemy.IsPlayerDetected();

        if (hit && !_enemy.IsObstacleInLine(hit.distance))
        {
            _timer = _enemy.battleTime; //타이머 설정

            if (hit.distance < _enemy.attackDistance && CanAttack())
            {
                _stateMachine.ChangeState(MushroomStateEnum.Attack);
                return;
            }
        }
        float distance = Vector2.Distance(_player.transform.position, _enemy.transform.position);


        //앞이 절벽이거나 적이 근거리라면.
        _enemy.AnimatorCompo.SetBool(waitAttackHash, false);
        if (!_enemy.IsGroundDetected() || (distance <= _enemy.attackDistance))
        {
            _enemy.StopImmediately(false);  //정지시에 재생할 애니메이션 상태를 하나 만들어야 해.
            _enemy.AnimatorCompo.SetBool(waitAttackHash, true);
            return;
        }

        if (_timer >= 0 && distance < _enemy.runAwayDistance)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            _stateMachine.ChangeState(MushroomStateEnum.Idle); // 전투시간을 초과했다면 idle로 이동.
        }
    }

    private bool CanAttack()
    {
        return Time.time >= _enemy.lastTimeAttacked + _enemy.attackCooldown;
    }
}
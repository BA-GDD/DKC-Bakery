using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkDeerBattleState : EnemyState<MilkDeerStateEnum>
{
    private MilkDeer _enemy;
    private Player _player;

    private float _timer;

    public MilkDeerBattleState(Enemy enemyBase, EnemyStateMachine<MilkDeerStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = enemyBase as MilkDeer;
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
        SetDirectionToEnemy();

        RaycastHit2D hit = _enemy.IsPlayerDetected();

        if (hit && !_enemy.IsObstacleInLine(hit.distance))
        {
            _timer = _enemy.battleTime; //Ÿ�̸� ����

            if (hit.distance < _enemy.attackDistance && CanAttack())
            {
                _stateMachine.ChangeState(MilkDeerStateEnum.RushAttack);
                return;
            }
        }
        float distance = Vector2.Distance(_player.transform.position, _enemy.transform.position);


        //���� �����̰ų� ���� �ٰŸ����.
        if (!_enemy.IsGroundDetected() || (distance <= _enemy.attackDistance))
        {
            _enemy.AnimatorCompo.SetBool("Wait", true);
            _enemy.StopImmediately(false);  //�����ÿ� ����� �ִϸ��̼� ���¸� �ϳ� ������ ��.
        }
        else
            _enemy.AnimatorCompo.SetBool("Wait", false);

        if (_timer >= 0 && distance < _enemy.runAwayDistance)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            _stateMachine.ChangeState(MilkDeerStateEnum.Idle); // �����ð��� �ʰ��ߴٸ� idle�� �̵�.
        }
    }
    private bool CanAttack()
    {
        return Time.time >= _enemy.lastTimeAttacked + _enemy.attackCooldown;
    }
}
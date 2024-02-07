using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmGroundState : EnemyState<WarmStateEnum>
{
    protected Warm _enemy;
    protected Player _player;
    public WarmGroundState(Enemy enemyBase, EnemyStateMachine<WarmStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = enemyBase as Warm;
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
        if (_player.HealthCompo.isDead)
        {
            return;
        }

        RaycastHit2D hit = _enemy.IsPlayerDetected();
        //�÷��̿��� �Ÿ� : ���ʿ��� �����ص� ������ �� �ֵ���

        if (_player.HealthCompo.isDead) return; //�׾����� �� �̵�.
        float distance = Vector2.Distance(_enemy.transform.position, _player.transform.position);
        if (distance < 2 || (hit && !_enemyBase.IsObstacleInLine(hit.distance)))
        {
            _stateMachine.ChangeState(WarmStateEnum.Battle);
            return;
        }
    }
}

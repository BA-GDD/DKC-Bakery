using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimedesuGroundState : EnemyState<SlimedesuStateEnum>
{
    protected Slimedesu _enemy;
    protected Player _player;
    public SlimedesuGroundState(Enemy enemyBase, EnemyStateMachine<SlimedesuStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = enemyBase as Slimedesu;
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

        float distance = Vector2.Distance(_player.transform.position, _enemy.transform.position);

        if (distance < 2f || (hit && !_enemy.IsWallDetected()))
        {
            _stateMachine.ChangeState(SlimedesuStateEnum.Battle);
            return;
        }
    }
}

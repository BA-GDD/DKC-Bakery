using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstrawberryGroundState : EnemyState<MonstrawberryStateEnum>
{
    protected Player _player;
    protected Monstrawberry _enemy;

    public MonstrawberryGroundState(Enemy enemyBase, EnemyStateMachine<MonstrawberryStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = enemyBase as Monstrawberry;
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
        float distance = Vector2.Distance(_player.transform.position, _enemy.transform.position);

        RaycastHit2D hit = _enemy.IsPlayerDetected();
        if (distance < 2f || (hit && !_enemy.IsWallDetected()))
        {
            _stateMachine.ChangeState(MonstrawberryStateEnum.Battle);
            return;
        }
    }
}

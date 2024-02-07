using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkDeerGroundState : EnemyState<MilkDeerStateEnum>
{
    private MilkDeer _enemy;
    private Player _player;
    public MilkDeerGroundState(Enemy enemyBase, EnemyStateMachine<MilkDeerStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = enemyBase as MilkDeer;
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
        base.UpdateState(); ;
        float distance = Vector2.Distance(_player.transform.position, _enemy.transform.position);
        RaycastHit2D hit = _enemy.IsPlayerDetected();
        if(distance < 2f || (hit && !_enemy.IsWallDetected()) )
        {
            _stateMachine.ChangeState(MilkDeerStateEnum.Battle);
            return;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyTreeSpikeAttackState : EnemyState<HoneyTreeStateEnum>
{
    private Player _player;
    private HoneyTree _enemy;
    
    public HoneyTreeSpikeAttackState(Enemy enemyBase, EnemyStateMachine<HoneyTreeStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = enemyBase as HoneyTree;
    }

    public override void Enter()
    {
        base.Enter();
        _player = GameManager.Instance.Player;

        RaycastHit2D hit = _enemy.IsGroundDetectedByPlayer(_player.transform.position);

        HoneyTreeSpike spike = PoolManager.Instance.Pop(PoolingType.HoneyTreeSpike) as HoneyTreeSpike;
        spike.transform.position = hit.point;
        spike.Init();
        spike.SetUp(_enemy);
        _enemy.spike = spike;
    }

    public override void Exit()
    {
        _enemy.lastTimeAttacked = Time.time;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(_triggerCalled)
        {
            _stateMachine.ChangeState(HoneyTreeStateEnum.Battle);
        }
    }
}
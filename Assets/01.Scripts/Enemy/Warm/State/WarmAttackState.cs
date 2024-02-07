using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmAttackState : EnemyState<WarmStateEnum>
{
    private Warm _enemy;
    public WarmAttackState(Enemy enemyBase, EnemyStateMachine<WarmStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = enemyBase as Warm;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        _enemy.lastTimeAttacked = Time.time;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _enemy.StopImmediately(false);

        if (_triggerCalled) //애니메이션이 끝났다면
        {
            _stateMachine.ChangeState(WarmStateEnum.Battle); //추적상태로 다시 전환.
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAttackState : EnemyState<MushroomStateEnum>
{
    private Mushroom _enemy;
    public MushroomAttackState(Enemy enemyBase, EnemyStateMachine<MushroomStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = enemyBase as Mushroom;
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
            _stateMachine.ChangeState(MushroomStateEnum.Battle); //추적상태로 다시 전환.
        }
    }
}
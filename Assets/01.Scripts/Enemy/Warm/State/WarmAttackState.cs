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

        if (_triggerCalled) //�ִϸ��̼��� �����ٸ�
        {
            _stateMachine.ChangeState(WarmStateEnum.Battle); //�������·� �ٽ� ��ȯ.
        }
    }
}
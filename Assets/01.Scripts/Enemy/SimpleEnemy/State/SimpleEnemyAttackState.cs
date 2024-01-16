using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyAttackState : EnemyState<SimpleEnemyStateEnum>
{
    private SimpleEnemy _enemy;

    public SimpleEnemyAttackState(Enemy enemyBase, EnemyStateMachine<SimpleEnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = enemyBase as SimpleEnemy;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _enemy.StopImmediately(false);

        if (_triggerCalled) //�ִϸ��̼��� �����ٸ�
        {
            _stateMachine.ChangeState(SimpleEnemyStateEnum.Battle); //�������·� �ٽ� ��ȯ.
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        _enemy.lastTimeAttacked = Time.time; //���������� ������ �ð��� �����.
    }
}
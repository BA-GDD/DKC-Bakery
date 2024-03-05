using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SimpleEnemyStunedState : EnemyState<SimpleEnemyStateEnum>
{
    private SimpleEnemy _enemy;
    public SimpleEnemyStunedState(Enemy enemyBase, EnemyStateMachine<SimpleEnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = enemyBase as SimpleEnemy;
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.SetVelocity(_enemy.stunDirection.x * -_enemy.FacingDirection, _enemy.stunDirection.y, doNotFlip: true); //맞았을 때 저장해둔 값으로 튕겨주고.
        ChangeToIdleAfterDelayTime(_enemy.stunDuration); //스턴시간 종료될때까지 대기.
    }

    private async void ChangeToIdleAfterDelayTime(float time)
    {
        await Task.Delay(Mathf.FloorToInt(time * 1000));
        _stateMachine.ChangeState(SimpleEnemyStateEnum.Idle);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
}

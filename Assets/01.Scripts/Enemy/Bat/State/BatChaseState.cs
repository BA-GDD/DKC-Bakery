using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatChaseState : BatAttackMoveState
{
    
    public BatChaseState(Enemy enemyBase, EnemyStateMachine<BatStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _targetPos = _playerTrm.position;

        _enemy.targetAltitude = _targetPos.y;

        dir = _targetPos - (Vector2)_enemy.transform.position;
    }
    public override void Exit()
    {
        base.Exit();

    }

    public override void UpdateState()
    {
        base.UpdateState();
        float x = Mathf.Lerp(0, dir.x, _timer) * 2;
        float y = Mathf.Lerp(dir.y, 0, _timer) * 2;
        _enemyBase.SetVelocity(x, y,true);
        float distance = Vector2.Distance(_targetPos, _enemyBase.transform.position);
        if (distance < 2f)
        {
            _enemyBase.StopImmediately(true);
            _stateMachine.ChangeState(BatStateEnum.Attack);
        }
    }
}

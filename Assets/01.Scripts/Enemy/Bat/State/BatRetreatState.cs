using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatRetreatState : BatAttackMoveState
{
    public BatRetreatState(Enemy enemyBase, EnemyStateMachine<BatStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _targetPos = new Vector2(_enemyBase.transform.position.x +_enemyBase.FacingDirection * 5f,_playerTrm.position.y + 5f);
        _enemy.targetAltitude = _targetPos.y;
        dir = _targetPos - (Vector2)_enemy.transform.position;
    }
    public override void Exit()
    {
        base.Exit();
        _enemyBase.lastTimeAttacked = Time.time;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        float x = Mathf.Lerp(dir.x, 0, _timer) * 2;
        float y = Mathf.Lerp(0, dir.y, _timer) * 2;
        _enemyBase.SetVelocity(x, y,true);
        if (_timer >= 1f)
        {
            _stateMachine.ChangeState(BatStateEnum.Battle);
        }
    }
}

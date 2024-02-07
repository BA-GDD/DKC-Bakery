using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MonstrawberryIdleState : MonstrawberryGroundState
{
    private bool _isAlreadyChanged;
    public MonstrawberryIdleState(Enemy enemyBase, EnemyStateMachine<MonstrawberryStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _isAlreadyChanged = false;
        ChangeToMoveWithDelay();
    }
    private void ChangeToMoveWithDelay()
    {
        Task.Delay(Mathf.FloorToInt(_enemy.idleTime * 1000));
        if (!_isAlreadyChanged)
        {
            _stateMachine.ChangeState(MonstrawberryStateEnum.Move);
        }
    }
    public override void Exit()
    {
        _isAlreadyChanged = true;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
}
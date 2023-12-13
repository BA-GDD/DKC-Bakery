using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.StopImmediately(false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        //���⼭ x ������ ���� ���ȴٸ� �̵����·� �������ָ� �ȴ�.
        float xInput = _player.PlayerInput.XInput;
        
        if(Mathf.Abs(xInput) > 0.05f)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Move);
        }
    }
}

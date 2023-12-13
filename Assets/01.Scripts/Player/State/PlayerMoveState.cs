using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        float xInput = _player.PlayerInput.XInput;

        //실제로 플레이어를 이동시켜주고(이건 나중에)
        _player.SetVelocity(xInput * _player.moveSpeed, _rigidbody.velocity.y);

        if(Mathf.Abs(xInput) < 0.05f)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }
}

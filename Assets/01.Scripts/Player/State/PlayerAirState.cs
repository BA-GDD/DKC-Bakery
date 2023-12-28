using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAirState : PlayerState
{
    protected PlayerAirState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.PlayerInput.PrimaryAttackEvent += HandleAirAttackEvent;
    }

    public override void Exit()
    {
        _player.PlayerInput.PrimaryAttackEvent -= HandleAirAttackEvent;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        float xInput = _player.PlayerInput.XInput;
        if(Mathf.Abs(xInput) > 0.05f)
        {
            _player.SetVelocity(_player.moveSpeed * 0.9f * xInput, _rigidbody.velocity.y);
        }

        if(_player.IsWallDetected() )
        {
            _stateMachine.ChangeState(PlayerStateEnum.WallSlide);
        }
    }
    private void HandleAirAttackEvent()
    {
        if (!_player.IsGroundDetected())
        {
            _stateMachine.ChangeState(PlayerStateEnum.AirAttack);
        }
    }
}

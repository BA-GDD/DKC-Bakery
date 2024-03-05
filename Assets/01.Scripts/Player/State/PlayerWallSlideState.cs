using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
        float yInput = _player.PlayerInput.YInput;

        if (yInput < 0)
        {
            _player.SetVelocity(0, _rigidbody.velocity.y, false);
        }
        else
        {
            _player.SetVelocity(0, _rigidbody.velocity.y * 0.7f, false);
        }

        if (Mathf.Abs(xInput) > 0.5f)
        {
            //x축이 눌렸고 현재 진행방향과 반대라면 Idle로 변경
            if (Mathf.Abs(_player.FacingDirection + Mathf.Sign(xInput)) < 0.5f)
            {
                _stateMachine.ChangeState(PlayerStateEnum.Idle);
                return;
            }
        }

        if (_player.IsGroundDetected())
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class PlayerGroundState : PlayerState
{
    protected PlayerGroundState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.PlayerInput.JumpEvent += HandleJumpEvent;
        _player.PlayerInput.PrimaryAttackEvent += HandlePrimaryAttackEvent;
    }

    public override void Exit()
    {
        _player.PlayerInput.JumpEvent -= HandleJumpEvent;
        _player.PlayerInput.PrimaryAttackEvent -= HandlePrimaryAttackEvent;
        base.Exit();
    }

    
    public override void UpdateState()
    {
        base.UpdateState();

        if(_player.IsGroundDetected() == false)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Fall);
        }

    }

    private void HandlePrimaryAttackEvent()
    {
        if(_player.IsGroundDetected())
        {
            _stateMachine.ChangeState(PlayerStateEnum.PrimaryAttack);
        }
    }

    private void HandleJumpEvent()
    {
        if (_player.IsGroundDetected())
        {
            _stateMachine.ChangeState(PlayerStateEnum.Jump);
        }
    }

}


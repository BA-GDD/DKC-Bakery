using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
        _player.PlayerInput.SwordAuraEvent += HandleSwordAuraEvent;
    }


    public override void Exit()
    {
        _player.PlayerInput.JumpEvent -= HandleJumpEvent;
        _player.PlayerInput.PrimaryAttackEvent -= HandlePrimaryAttackEvent;
        _player.PlayerInput.SwordAuraEvent -= HandleSwordAuraEvent;
        base.Exit();
    }


    public override void UpdateState()
    {
        base.UpdateState();

        if (_player.IsGroundDetected() == false)
            _player.coyoteCounter += Time.deltaTime;
        else
            _player.coyoteCounter = 0;

        if(_player.coyoteCounter > _player.CoyoteTime)
            _stateMachine.ChangeState(PlayerStateEnum.Fall);

    }

    private void HandleSwordAuraEvent()
    {
        
        if (_player.IsGroundDetected()&&_player.Skill.GetSkill<SwordAuraSkill>().AttemptUseSkill())
        {
            _stateMachine.ChangeState(PlayerStateEnum.SwordAura);
        }
    }

    private void HandlePrimaryAttackEvent()
    {
        if (_player.IsGroundDetected())
        {
            _stateMachine.ChangeState(PlayerStateEnum.PrimaryAttack);
        }
    }

    private void HandleJumpEvent()
    {
        if (_player.coyoteCounter < _player.CoyoteTime)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Jump);
        }
    }

}


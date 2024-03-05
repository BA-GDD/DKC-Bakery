using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordAuraState : PlayerGroundState
{

    public PlayerSwordAuraState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        float attackDirection = _player.FacingDirection;

        _player.AnimatorCompo.speed = _player.attackSpeed;

        Vector2 move = _player.attackMovement[2];
        _player.SetVelocity(move.x * attackDirection, move.y);

        _player.StartDelayAction(0.1f, () =>
        {
            _player.StopImmediately(false);
        });
        PlayerAnimationTriggers.AnimationEvent += _player.Skill.GetSkill<SwordAuraSkill>().SpawnAura;
    }

    public override void Exit()
    {
        PlayerAnimationTriggers.AnimationEvent -= _player.Skill.GetSkill<SwordAuraSkill>().SpawnAura;
        _player.AnimatorCompo.speed = 1;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(_endTriggerCalled)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }
}

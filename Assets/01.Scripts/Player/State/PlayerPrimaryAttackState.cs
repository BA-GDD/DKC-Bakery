using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int _comboCounter; //현재 콤보
    private float _lastAttackTime; //마지막으로 공격한 시간
    private float _comboWindow = 0.8f; //콤보가 끊길때까지의 시간.

    private readonly int _comboCounterHash = Animator.StringToHash("ComboCounter");
    public PlayerPrimaryAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        PlayerAnimationTriggers.AnimationEvent += HandleAnimationEvent;
        if (_comboCounter > 2 || Time.time >= _lastAttackTime + _comboWindow)
            _comboCounter = 0; //콤보 초기화

        _player.AnimatorCompo.SetInteger(_comboCounterHash, _comboCounter);

        _player.AnimatorCompo.speed = _player.attackSpeed;

        float attackDirection = _player.FacingDirection;
        float xInput = _player.PlayerInput.XInput;
        if (Mathf.Abs(xInput) > 0.05f)
        {
            attackDirection = xInput;
        }

        Vector2 move = _player.attackMovement[_comboCounter];
        _player.SetVelocity(move.x * attackDirection, move.y);

        _player.StartDelayAction(0.1f, () =>
        {
            _player.StopImmediately(false);
        });
    }

    private void HandleAnimationEvent()
    {
        _player.Attack();
    }

    public override void Exit()
    {
        ++_comboCounter;
        _lastAttackTime = Time.time;
        _player.AnimatorCompo.speed = 1f;
        PlayerAnimationTriggers.AnimationEvent -= HandleAnimationEvent;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_endTriggerCalled)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Fall);
        }
    }
}

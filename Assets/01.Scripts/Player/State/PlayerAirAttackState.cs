using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirAttackState : PlayerState
{
    private int _comboCounter; //현재 콤보
    private float _lastAttackTime; //마지막으로 공격한 시간

    private bool _attackTrigger;

    private float _originGravify;
    private float _originXVelocity;


    private readonly int _comboCounterHash = Animator.StringToHash("ComboCounter");
    public PlayerAirAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.PlayerInput.PrimaryAttackEvent += AttackInputHandle;
        PlayerAnimationTriggers.AnimationEvent += HandleAnimationEvent;
        _originXVelocity = _rigidbody.velocity.x;
        _originGravify = _rigidbody.gravityScale;
        _rigidbody.gravityScale = 1;

        _player.AnimatorCompo.speed = _player.attackSpeed;

        _player.OnAttackEvent.AddListener(HandleAttackHitEvent);
        _player.OnStartAttack?.Invoke(_comboCounter);
        //_player.StartDelayAction(0.1f, () =>
        //{
        //    _player.StopImmediately(false);
        //});
    }

    private void AttackInputHandle()
    {
        _attackTrigger = true;
    }

    public override void Exit()
    {
        _player.PlayerInput.PrimaryAttackEvent -= AttackInputHandle;
        PlayerAnimationTriggers.AnimationEvent -= HandleAnimationEvent;
        _player.OnAttackEvent.RemoveListener  (HandleAttackHitEvent);

        _comboCounter = 0;
        _player.AnimatorCompo.speed = 1f;
        _rigidbody.velocity = new Vector2(_originXVelocity, _rigidbody.velocity.y);
        _player.OnEndAttack?.Invoke();
        _rigidbody.gravityScale = _originGravify;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        float attackDirection = _player.FacingDirection;
        float xInput = _player.PlayerInput.XInput;
        if (Mathf.Abs(xInput) > 0.05f)
        {
            attackDirection = xInput;
        }

        if (_endTriggerCalled)
        {
            if (Time.time - _lastAttackTime > 1000000)
            {
                _stateMachine.ChangeState(PlayerStateEnum.Fall);
                return;
            }
            if(_attackTrigger)
            {
                _attackTrigger = false;
                _endTriggerCalled = false;
                if (_comboCounter > 2)
                    _comboCounter = 0; //콤보 초기화
                _player.AnimatorCompo.SetInteger(_comboCounterHash, _comboCounter);
            }
        }


    }

    private void HandleAnimationEvent()
    {
        _player.Attack();
    }
    private void HandleAttackHitEvent()
    {
        _player.SetVelocity(_rigidbody.velocity.x * _player.airXMovementRatio, _player.airAttackRising[_comboCounter]);
        _comboCounter++;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirAttackState : PlayerState
{
    private int _comboCounter; //현재 콤보
    private float _lastAttackTime = 0; //마지막으로 공격한 시간
    private float _lastStartTIme = 0;

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
        _player.stance = true;

        _player.PlayerInput.PrimaryAttackEvent += AttackInputHandle;
        PlayerAnimationTriggers.AnimationEvent += HandleAnimationEvent;
        _originXVelocity = _rigidbody.velocity.x;
        _originGravify = _rigidbody.gravityScale;

        _comboCounter = 0;
        _player.AnimatorCompo.SetInteger(_comboCounterHash, _comboCounter);


        _player.AnimatorCompo.speed = _player.attackSpeed;
        _player.OnStartAttack?.Invoke(_comboCounter);
        _player.OnAttackEvent.AddListener(HandleAttackHitEvent);
        //_player.StartDelayAction(0.1f, () =>
        //{
        //    _player.StopImmediately(false);
        //});
    }

    private void AttackInputHandle()
    {
        if (Time.time - _lastStartTIme > 0.3f)
            _attackTrigger = true;
    }

    public override void Exit()
    {
        _player.stance = false;

        _player.PlayerInput.PrimaryAttackEvent -= AttackInputHandle;
        PlayerAnimationTriggers.AnimationEvent -= HandleAnimationEvent;
        _player.OnAttackEvent.RemoveListener(HandleAttackHitEvent);


        _player.AnimatorCompo.speed = 1f;
        _rigidbody.velocity = new Vector2(_originXVelocity, _rigidbody.velocity.y);
        _rigidbody.gravityScale = _originGravify;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_player.stopDebug)
        {
            Debug.Log($"{_endTriggerCalled}/{Time.time}-{_lastAttackTime}/{_attackTrigger}");
        }


        if (_endTriggerCalled)
        {
            AirMovementControl();
            if (Time.time - _lastAttackTime > 0.01f)
            {
                _stateMachine.ChangeState(PlayerStateEnum.Fall);
                return;
            }
            if (_attackTrigger)
            {
                _lastStartTIme = Time.time;
                _attackTrigger = false;
                _endTriggerCalled = false;
                _comboCounter++;
                if (_comboCounter > 2)
                    _comboCounter = 0; //콤보 초기화
                _player.AnimatorCompo.SetInteger(_comboCounterHash, _comboCounter);
                _player.OnStartAttack?.Invoke(_comboCounter);
            }
        }


    }
    public override void AnimationEndTrigger()
    {
        _lastAttackTime = Time.time;
        base.AnimationEndTrigger();
        _player.OnEndAttack?.Invoke();
    }

    private void AirMovementControl()
    {
        float xInput = _player.PlayerInput.XInput;
        if (Mathf.Abs(xInput) > 0.05f)
        {
            float xVelocity = xInput * _player.moveSpeed;
            _player.SetVelocity(xVelocity * _player.airXMovementRatio, _rigidbody.velocity.y);
        }
    }
    private void HandleAnimationEvent()
    {
        _player.Attack();
    }
    private void HandleAttackHitEvent()
    {
        _rigidbody.gravityScale = 1;
        _player.SetVelocity(_rigidbody.velocity.x * _player.airXMovementRatio, _player.airAttackRising[_comboCounter]);
    }
}

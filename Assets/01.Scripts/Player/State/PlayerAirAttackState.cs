using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirAttackState : PlayerState
{
    private int _comboCounter; //현재 콤보
    private float _lastAttackTime; //마지막으로 공격한 시간
    private float _comboWindow = 0.8f; //콤보가 끊길때까지의 시간.
    private bool _hitLastAttack;

    private float asdf;

    private readonly int _comboCounterHash = Animator.StringToHash("ComboCounter");
    public PlayerAirAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        asdf = _rigidbody.velocity.x;
        PlayerAnimationTriggers.AnimationEvent += HandleAnimationEvent;

        if (_comboCounter > 2 || Time.time >= _lastAttackTime + _comboWindow)
            _comboCounter = 0; //콤보 초기화

        if (_comboCounter > 0 && _hitLastAttack == false) return;

        _hitLastAttack = false;

        _player.AnimatorCompo.SetInteger(_comboCounterHash, _comboCounter);

        _player.AnimatorCompo.speed = _player.attackSpeed;

        float attackDirection = _player.FacingDirection;
        float xInput = _player.PlayerInput.XInput;
        if (Mathf.Abs(xInput) > 0.05f)
        {
            attackDirection = xInput;
        }

        _player.AttackEvent.AddListener(HandleAttackHitEvent);

        //_player.StartDelayAction(0.1f, () =>
        //{
        //    _player.StopImmediately(false);
        //});
    }

    public override void Exit()
    {
        PlayerAnimationTriggers.AnimationEvent -= HandleAnimationEvent;
        _player.AttackEvent.RemoveListener  (HandleAttackHitEvent);

        ++_comboCounter;
        _lastAttackTime = Time.time;
        _player.AnimatorCompo.speed = 1f;
        _rigidbody.velocity = new Vector2(asdf, _rigidbody.velocity.y);
        base.Exit();
    }

    public override void UpdateState()
    {
        if (_endTriggerCalled)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Fall);
        }
        base.UpdateState();

    }

    private void HandleAnimationEvent()
    {
        _player.Attack();
    }
    private void HandleAttackHitEvent()
    {
        _hitLastAttack = true;
        _player.SetVelocity(_rigidbody.velocity.x * _player.airXMovementRatio, _player.airAttackRising[_comboCounter]);
    }
}

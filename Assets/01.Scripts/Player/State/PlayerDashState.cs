using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    private float _dashStartTime;
    private float _dashDirection;
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //��ÿ� ������ ���� x�� �Է��� �����ϸ� �ش� �������� ��� �𷺼��� �����ϰ� 
        float xInput = _player.PlayerInput.XInput;
        _dashDirection = Mathf.Abs(xInput) > 0.05f ? xInput : _player.FacingDirection;
        _dashStartTime = Time.time;
        // �׷��� �ʴٸ� FacingDirection���� ��� �𷺼��� ������
        // �׸��� ���� ��� ������ �ð��� ��� ���۽ð����� ����Ѵ�. 
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        //�÷��̾� �ӵ��� ��ÿ� �°� ���ߵ� �������ְ�
        _player.SetVelocity(_player.dashSpeed * _dashDirection, 0);
        // ���� ��� ���۽ð����κ��� ���ݱ��� ��� duration��ŭ �귶�ٸ� 
        if(_dashStartTime + _player.dashDuration <= Time.time)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
        // Idle���·� ��ȯ�Ѵ�. 
    }
    
    //��� �̺�Ʈ�� Player.cs �ڵ��� OnEnable���� �����ϰ� OnDisable���� �������� �Ѵ�.
    //��� Ű�� ������ ��� ���·� ���游 �Ѵ�.
}

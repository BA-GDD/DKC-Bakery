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
        //대시에 들어오면 만약 x축 입력이 존재하면 해당 방향으로 대시 디렉션을 설정하고 
        float xInput = _player.PlayerInput.XInput;
        _dashDirection = Mathf.Abs(xInput) > 0.05f ? xInput : _player.FacingDirection;
        _dashStartTime = Time.time;
        // 그렇지 않다면 FacingDirection으로 대시 디렉션을 설정해
        // 그리고 지금 대시 시작한 시간을 대시 시작시간으로 기록한다. 
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        //플레이어 속도를 대시에 맞게 알잘딱 조정해주고
        _player.SetVelocity(_player.dashSpeed * _dashDirection, 0);
        // 만약 대시 시작시간으로부터 지금까지 대시 duration만큼 흘렀다면 
        if(_dashStartTime + _player.dashDuration <= Time.time)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
        // Idle상태로 전환한다. 
    }
    
    //대시 이벤트는 Player.cs 코드의 OnEnable에서 구독하고 OnDisable에서 구독해제 한다.
    //대시 키가 눌리면 대시 상태로 변경만 한다.
}

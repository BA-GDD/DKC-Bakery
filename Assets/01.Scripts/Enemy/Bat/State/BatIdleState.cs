using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BatIdleState : EnemyState<BatStateEnum>
{
    private Player _player;
    private Bat _enemy;
    private bool _isAlreadyChange = false;
    public BatIdleState(Enemy enemyBase, EnemyStateMachine<BatStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = enemyBase as Bat;
    }

    public override void Enter()
    {
        base.Enter();
        _isAlreadyChange = false;
        _player = GameManager.Instance.Player;
        ChangeToMoveWithDelay(Mathf.FloorToInt(_enemy.idleTime * 1000));
    }
    private async void ChangeToMoveWithDelay(int timer)
    {
        await Task.Delay(timer);
        if (!_isAlreadyChange)
        {
            _stateMachine.ChangeState(BatStateEnum.Move);
        }
    }
    public override void Exit()
    {
        _isAlreadyChange = true;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        float dis = _player.transform.position.x - _enemy.transform.position.x;
        if (dis < 7f)
        {
            _stateMachine.ChangeState(BatStateEnum.Battle);
        }
    }
}
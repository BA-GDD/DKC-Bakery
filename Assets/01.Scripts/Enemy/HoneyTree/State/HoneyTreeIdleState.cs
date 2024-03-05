using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HoneyTreeIdleState : EnemyState<HoneyTreeStateEnum>
{
    private bool _isExitState;
    private HoneyTree _enemy;
    private Player _player;

    public HoneyTreeIdleState(Enemy enemyBase, EnemyStateMachine<HoneyTreeStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = enemyBase as HoneyTree;
    }
    private void TurnDirectionWithDelay()
    {
        Task.Delay(Mathf.FloorToInt(_enemy.turnDelay * 1000));
        if (!_isExitState)
        {
            _enemy.Flip();
        }
    }

    public override void Enter()
    {
        base.Enter();
        _player = GameManager.Instance.Player;

        _isExitState = false;
        TurnDirectionWithDelay();
    }

    public override void Exit()
    {
        _isExitState = true;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        float distance = Vector2.Distance(_enemy.transform.position, _player.transform.position);
        if (distance < _enemy.attackDistance)
        {
            _stateMachine.ChangeState(HoneyTreeStateEnum.Battle);
        }
    }
}
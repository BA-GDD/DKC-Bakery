using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttackMoveState : EnemyState<BatStateEnum>
{
    protected Bat _enemy;
    protected Transform _playerTrm;
    protected Vector2 _targetPos;
    protected float _timer;

    protected Vector2 dir;
    public BatAttackMoveState(Enemy enemyBase, EnemyStateMachine<BatStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = enemyBase as Bat;
    }

    public override void Enter()
    {
        base.Enter();
        _playerTrm = GameManager.Instance.PlayerTrm;
        _timer = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _timer += Time.deltaTime;
        
    }
}

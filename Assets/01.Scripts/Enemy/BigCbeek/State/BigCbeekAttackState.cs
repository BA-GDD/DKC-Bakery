using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCbeekAttackState : EnemyState<BigCbeekStateEnum>
{
    private BigCbeek _enemy;
    private bool _isHItPlayer;
    private Vector2 _attackDir;

    private float _timer;

    public BigCbeekAttackState(Enemy enemyBase, EnemyStateMachine<BigCbeekStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _enemy = enemyBase as BigCbeek;
    }

    public override void Enter()
    {
        base.Enter();
        _timer = 0;
        _enemy.attackPos = _enemy.transform.position;
        _isHItPlayer = false;
        _attackDir = GameManager.Instance.PlayerTrm.position - _enemy.transform.position;
        _enemy.OnAttackEvent.AddListener(AttackHandle);
    }

    public override void Exit()
    {
        _enemy.StopImmediately(true);
        _enemy.OnAttackEvent.RemoveListener(AttackHandle);
        base.Exit();
    }
    private void AttackHandle()
    {
        _isHItPlayer = true;
    }
    public override void UpdateState()
    {
        base.UpdateState();
        _timer += Time.deltaTime;
        if(!_isHItPlayer)
        {
            _enemy.Attack();
        }
        _enemy.SetVelocity(_attackDir.normalized  * _enemy.attackMoveSpeed, true);
        if (_timer > _enemy.attackDuration|| _isHItPlayer)
        {
            _stateMachine.ChangeState(BigCbeekStateEnum.Retreat);
        }
    }
}
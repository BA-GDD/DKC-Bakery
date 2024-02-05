using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BigCbeekIdleState : EnemyState<BigCbeekStateEnum>
{
    private Player _player;

    public BigCbeekIdleState(Enemy enemyBase, EnemyStateMachine<BigCbeekStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player = GameManager.Instance.Player;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Vector2 dir = new Vector2(Mathf.Sin(Time.time), Mathf.Cos(Time.time * 2));
        _enemyBase.SetVelocity(dir * _enemyBase.moveSpeed * 0.25f);
        float dis = _player.transform.position.x - _enemyBase.transform.position.x;
        if (dis < _enemyBase.runAwayDistance)
        {
            _stateMachine.ChangeState(BigCbeekStateEnum.Battle);
        }
    }
}
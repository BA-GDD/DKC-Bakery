using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WarmStateEnum{Idle, Battle, Attack, Stuned, Dead,Move }
public class Warm: Enemy
{
    public EnemyStateMachine<WarmStateEnum> StateMachine { get; private set; }
    public float fireBallSpeed;
    [SerializeField] private Transform _fireTrm;
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<WarmStateEnum>();

        foreach (WarmStateEnum state in Enum.GetValues(typeof(WarmStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Warm{typeName}State");
            if (t != null)
            {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<WarmStateEnum>;
                StateMachine.AddState(state, enemyState);
            }
            else
            {
                Debug.LogError($"WarmState : no state[{typeName}]");
            }
        }
    }
    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(WarmStateEnum.Idle);
    }

    protected override void Update()
    {
        base.Update();

        if (_isFrozen) return;
        StateMachine.CurrentState.UpdateState();
    }
    protected override void HandleDie(Vector2 direction)
    {
        StateMachine.ChangeState(WarmStateEnum.Dead);
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            StateMachine.ChangeState(WarmStateEnum.Stuned);
            return true;
        }

        return false;
    }
    public override void Attack()
    {
        /*WarmFireBall fireBall = PoolManager.Instance.Pop(PoolingType.WarmFireBall) as WarmFireBall;
        fireBall.transform.position = _fireTrm.position;
        fireBall.Fire(FacingDirection,this);*/
    }



    protected override void HandleHit()
    {
        base.HandleHit();
        if (!_isFrozenWithoutTimer)

            StateMachine.ChangeState(WarmStateEnum.Battle); //���ݻ��·� �ѱ��.
    }

    public override void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public override void SlowEntityBy(float percent)
    {
    }
}

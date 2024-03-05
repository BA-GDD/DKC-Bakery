using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BatStateEnum{Idle, Battle, Attack, Stuned, Dead, Move, Chase, Retreat}
public class Bat: Enemy
{
    public float targetAltitude;
    public float attackMoveSpeed;

    public EnemyStateMachine<BatStateEnum> StateMachine { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<BatStateEnum>();

        foreach (BatStateEnum state in Enum.GetValues(typeof(BatStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Bat{typeName}State");
            if (t != null)
            {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<BatStateEnum>;
                StateMachine.AddState(state, enemyState);
            }
            else
            {
                Debug.LogError($"BatState : no state[{typeName}]");
            }
        }
    }
    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(BatStateEnum.Idle);
    }

    protected override void Update()
    {
        base.Update();

        if (_isFrozen) return;
        StateMachine.CurrentState.UpdateState();
    }
    protected override void HandleDie(Vector2 direction)
    {
        StateMachine.ChangeState(BatStateEnum.Dead);
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            StateMachine.ChangeState(BatStateEnum.Stuned);
            return true;
        }

        return false;
    }



    protected override void HandleHit()
    {
        base.HandleHit();
        if (!_isFrozenWithoutTimer)

            StateMachine.ChangeState(BatStateEnum.Battle); //���ݻ��·� �ѱ��.
    }

    public override void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public override void SlowEntityBy(float percent)
    {
    }
}

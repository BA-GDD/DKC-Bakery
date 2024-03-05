using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonstrawberryStateEnum{Idle, Battle, Attack, Stuned, Dead, Move, }
public class Monstrawberry: Enemy
{
    public EnemyStateMachine<MonstrawberryStateEnum> StateMachine { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<MonstrawberryStateEnum>();

        foreach (MonstrawberryStateEnum state in Enum.GetValues(typeof(MonstrawberryStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Monstrawberry{typeName}State");
            if (t != null)
            {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<MonstrawberryStateEnum>;
                StateMachine.AddState(state, enemyState);
            }
            else
            {
                Debug.LogError($"MonstrawberryState : no state[{typeName}]");
            }
        }
    }
    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(MonstrawberryStateEnum.Idle);
    }

    protected override void Update()
    {
        base.Update();

        if (_isFrozen) return;
        StateMachine.CurrentState.UpdateState();
    }
    protected override void HandleDie(Vector2 direction)
    {
        StateMachine.ChangeState(MonstrawberryStateEnum.Dead);
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            StateMachine.ChangeState(MonstrawberryStateEnum.Stuned);
            return true;
        }

        return false;
    }



    protected override void HandleHit()
    {
        base.HandleHit();
        if (!_isFrozenWithoutTimer)

            StateMachine.ChangeState(MonstrawberryStateEnum.Battle); //���ݻ��·� �ѱ��.
    }

    public override void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public override void SlowEntityBy(float percent)
    {
    }
}

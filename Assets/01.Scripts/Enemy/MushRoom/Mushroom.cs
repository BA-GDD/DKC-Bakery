using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MushroomStateEnum{Idle, Battle, Attack, Stuned, Dead, Move}
public class Mushroom: Enemy
{
    public EnemyStateMachine<MushroomStateEnum> StateMachine { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<MushroomStateEnum>();

        foreach (MushroomStateEnum state in Enum.GetValues(typeof(MushroomStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Mushroom{typeName}State");
            if (t != null)
            {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<MushroomStateEnum>;
                StateMachine.AddState(state, enemyState);
            }
            else
            {
                Debug.LogError($"MushroomState : no state[{typeName}]");
            }
        }
    }
    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(MushroomStateEnum.Idle);
    }

    protected override void Update()
    {
        base.Update();

        if (_isFrozen) return;
        StateMachine.CurrentState.UpdateState();
    }
    protected override void HandleDie(Vector2 direction)
    {
        StateMachine.ChangeState(MushroomStateEnum.Dead);
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            StateMachine.ChangeState(MushroomStateEnum.Stuned);
            return true;
        }

        return false;
    }



    protected override void HandleHit()
    {
        base.HandleHit();
        if (!_isFrozenWithoutTimer)

            StateMachine.ChangeState(MushroomStateEnum.Battle); //���ݻ��·� �ѱ��.
    }

    public override void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public override void SlowEntityBy(float percent)
    {
    }
}

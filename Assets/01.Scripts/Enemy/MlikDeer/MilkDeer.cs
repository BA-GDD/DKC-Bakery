using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MilkDeerStateEnum{Idle, Battle, RushAttack, Stuned, Dead,Move }
public class MilkDeer: Enemy
{
    public float dashSpeed;
    public float dashDistance;
    public EnemyStateMachine<MilkDeerStateEnum> StateMachine { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<MilkDeerStateEnum>();

        foreach (MilkDeerStateEnum state in Enum.GetValues(typeof(MilkDeerStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"MilkDeer{typeName}State");
            if (t != null)
            {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<MilkDeerStateEnum>;
                StateMachine.AddState(state, enemyState);
            }
            else
            {
                Debug.LogError($"MlikDeerState : no state[{typeName}]");
            }
        }
    }
    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(MilkDeerStateEnum.Idle);
    }

    protected override void Update()
    {
        base.Update();

        if (_isFrozen) return;
        StateMachine.CurrentState.UpdateState();
    }
    protected override void HandleDie(Vector2 direction)
    {
        StateMachine.ChangeState(MilkDeerStateEnum.Dead);
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            StateMachine.ChangeState(MilkDeerStateEnum.Stuned);
            return true;
        }

        return false;
    }



    protected override void HandleHit()
    {
        base.HandleHit();
        if (!_isFrozenWithoutTimer)

            StateMachine.ChangeState(MilkDeerStateEnum.Battle); //���ݻ��·� �ѱ��.
    }

    public override void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public override void SlowEntityBy(float percent)
    {
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlimedesuStateEnum{Idle, Battle, Attack, Stuned, Dead, Move, }
public class Slimedesu: Enemy
{
    public EnemyStateMachine<SlimedesuStateEnum> StateMachine { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<SlimedesuStateEnum>();

        foreach (SlimedesuStateEnum state in Enum.GetValues(typeof(SlimedesuStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Slimedesu{typeName}State");
            if (t != null)
            {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<SlimedesuStateEnum>;
                StateMachine.AddState(state, enemyState);
            }
            else
            {
                Debug.LogError($"SlimedesuState : no state[{typeName}]");
            }
        }
    }
    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(SlimedesuStateEnum.Idle);
    }

    protected override void Update()
    {
        base.Update();

        if (_isFrozen) return;
        StateMachine.CurrentState.UpdateState();
    }
    protected override void HandleDie(Vector2 direction)
    {
        StateMachine.ChangeState(SlimedesuStateEnum.Dead);
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            StateMachine.ChangeState(SlimedesuStateEnum.Stuned);
            return true;
        }

        return false;
    }



    protected override void HandleHit()
    {
        base.HandleHit();
        if (!_isFrozenWithoutTimer)

            StateMachine.ChangeState(SlimedesuStateEnum.Battle); //���ݻ��·� �ѱ��.
    }

    public override void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public override void SlowEntityBy(float percent)
    {
    }
}

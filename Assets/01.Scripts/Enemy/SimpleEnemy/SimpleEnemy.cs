using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SimpleEnemyStateEnum
{
    Idle,
    Battle,
    Dead,
    Debug
}

public class SimpleEnemy : Enemy
{
    public EnemyStateMachine<SimpleEnemyStateEnum> StateMachine { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<SimpleEnemyStateEnum>();

        foreach (SimpleEnemyStateEnum state in Enum.GetValues(typeof(SimpleEnemyStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"SimpleEnemy{typeName}State");
            if (t != null)
            {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<SimpleEnemyStateEnum>;
                StateMachine.AddState(state, enemyState);
            }
            else
            {
                Debug.LogError($"SimpleEnemy : no stateP[{typeName}]");
            }
        }
    }
    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(SimpleEnemyStateEnum.Idle);
    }

    protected override void Update()
    {
        base.Update();

        if (_isFrozen) return;
        StateMachine.CurrentState.UpdateState();
    }

    protected override void HandleDie(Vector2 direction)
    {
        StateMachine.ChangeState(SimpleEnemyStateEnum.Dead);
    }

    //public override bool CanBeStunned()
    //{
    //    if (base.CanBeStunned())
    //    {
    //        StateMachine.ChangeState(SimpleEnemyStateEnum.Stuned);
    //        return true;
    //    }

    //    return false;
    //}


    protected override void HandleHit()
    {
        base.HandleHit();
        if (!_isFrozenWithoutTimer)
            StateMachine.ChangeState(SimpleEnemyStateEnum.Battle); //공격상태로 넘긴다.
    }

    public override void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public override void SlowEntityBy(float percent)
    {
    }
}

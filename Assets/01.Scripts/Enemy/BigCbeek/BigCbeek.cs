using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BigCbeekStateEnum{Idle, Battle, Attack, Stuned, Dead, Retreat }
public class BigCbeek: Enemy
{
    [Header("개별 속성")]
    public float targetAltitude;
    public float attackMoveSpeed;
    public float attackDuration;
    [HideInInspector] public Vector2 playerDir;
    [HideInInspector] public Vector2 attackPos;

    public EnemyStateMachine<BigCbeekStateEnum> StateMachine { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<BigCbeekStateEnum>();

        foreach (BigCbeekStateEnum state in Enum.GetValues(typeof(BigCbeekStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"BigCbeek{typeName}State");
            if (t != null)
            {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<BigCbeekStateEnum>;
                StateMachine.AddState(state, enemyState);
            }
            else
            {
                Debug.LogError($"BigCbeekState : no state[{typeName}]");
            }
        }
    }
    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(BigCbeekStateEnum.Idle);
    }

    protected override void Update()
    {
        base.Update();

        if (_isFrozen) return;
        StateMachine.CurrentState.UpdateState();
    }
    protected override void HandleDie(Vector2 direction)
    {
        StateMachine.ChangeState(BigCbeekStateEnum.Dead);
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            StateMachine.ChangeState(BigCbeekStateEnum.Stuned);
            return true;
        }

        return false;
    }

    protected override void HandleHit()
    {
        base.HandleHit();
        if (!_isFrozenWithoutTimer)

            StateMachine.ChangeState(BigCbeekStateEnum.Battle); //���ݻ��·� �ѱ��.
    }

    public override void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public override bool IsObstacleInLine(float distance)
    {
        return Physics2D.Raycast(_wallChecker.position, playerDir, distance, _whatIsObstacle);
    }
    public override void SlowEntityBy(float percent)
    {
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HoneyTreeStateEnum { Idle, Battle, SpikeAttack, PrimaryAttack, Stuned, Dead, }
public class HoneyTree : Enemy
{
    public float turnDelay;
    public float spikeAttackCooldown;
    [HideInInspector] public HoneyTreeSpike spike;
    public EnemyStateMachine<HoneyTreeStateEnum> StateMachine { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<HoneyTreeStateEnum>();

        foreach (HoneyTreeStateEnum state in Enum.GetValues(typeof(HoneyTreeStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"HoneyTree{typeName}State");
            if (t != null)
            {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<HoneyTreeStateEnum>;
                StateMachine.AddState(state, enemyState);
            }
            else
            {
                Debug.LogError($"HoneyTreeState : no state[{typeName}]");
            }
        }
    }
    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(HoneyTreeStateEnum.Idle);
    }

    protected override void Update()
    {
        base.Update();

        if (_isFrozen) return;
        StateMachine.CurrentState.UpdateState();
    }
    protected override void HandleDie(Vector2 direction)
    {
        StateMachine.ChangeState(HoneyTreeStateEnum.Dead);
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            StateMachine.ChangeState(HoneyTreeStateEnum.Stuned);
            return true;
        }

        return false;
    }
    protected override void HandleKnockback(Vector2 direction)
    {

    }



    protected override void HandleHit()
    {
        base.HandleHit();
        if (!_isFrozenWithoutTimer)

            StateMachine.ChangeState(HoneyTreeStateEnum.Battle); //���ݻ��·� �ѱ��.
    }

    public override void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    #region Spike 관련
    public void SpikeAttack()
    {
        if (spike != null)
        {
            spike.Attack();
            spike = null;
        }
    }
    public RaycastHit2D IsGroundDetectedByPlayer(Vector2 playerPos) => Physics2D.Raycast(playerPos, Vector2.down, 100f, _whatIsObstacle);
    #endregion

    public override void SlowEntityBy(float percent)
    {
    }
}

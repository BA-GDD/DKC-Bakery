using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [Header("movement settings")]
    public float moveSpeed = 12f;
    public float jumpForce = 12f;
    public float dashDuration = 0.4f;
    public float dashSpeed = 20f;

    [Header("attack settings")]
    public float attackSpeed = 1f;
    public Vector2[] attackMovement;
    public float[] airAttackRising;
    [Range(0,1)]
    public float airXMovementRatio;

    [field: SerializeField] public InputReader PlayerInput { get; private set; }

    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerStat PlayerStat { get; private set; }

    [HideInInspector]
    public SkillManager Skill { get; private set; }



    protected override void Awake()
    {
        base.Awake();

        StateMachine = new PlayerStateMachine();
        PlayerStat = CharStat as PlayerStat;

        foreach (PlayerStateEnum stateEnum in Enum.GetValues(typeof(PlayerStateEnum)))
        {
            string typeName = stateEnum.ToString();
            Type t = Type.GetType($"Player{typeName}State");

            PlayerState newState = Activator.CreateInstance(t, this, StateMachine, typeName) as PlayerState;
            StateMachine.AddState(stateEnum, newState);
        }
    }

    protected override void Start()
    {
        Skill = SkillManager.Instance;
        StateMachine.Initialize(PlayerStateEnum.Idle, this);
    }

    protected void OnEnable()
    {
        PlayerInput.DashEvent += HandleDashEvent;
    }

    protected void OnDisable()
    {
        PlayerInput.DashEvent -= HandleDashEvent;
    }

    #region handling input
    private void HandleDashEvent()
    {
        if (Skill.GetSkill<DashSkill>().AttemptUseSkill())
        {
            StateMachine.ChangeState(PlayerStateEnum.Dash);
        }
    }
    #endregion


    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.UpdateState();

        //if(Keyboard.current.pKey.wasPressedThisFrame)
        //{
        //    PlayerStat.IncreaseStatBy(10, 4f, PlayerStat.GetStatByType(StatType.strength));
        //}
    }

    public void AnimationEndTrigger()
    {
        StateMachine.CurrentState.AnimationEndTrigger();
    }

    protected override void HandleDie(Vector2 direction)
    {
    }

    public override void SlowEntityBy(float percent)
    {
    }
}

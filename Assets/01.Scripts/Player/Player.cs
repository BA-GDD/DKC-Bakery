using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
#if UNITY_EDITOR
    [HideInInspector] public PlayerStateEnum debugState;

    [CustomEditor(typeof(Player))]
    public class PlayerEditor : EntityEditor
    {
        private Player _player;
        private PlayerStateEnum _stateEnum;

        protected override void OnEnable()
        {
            base.OnEnable();
            _player = (Player)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            _stateEnum = _player.debugState;

            if (Application.isPlaying)
            {
                GUILayout.Label("강제 상태 전환");
                _player.debugState = (PlayerStateEnum)EditorGUILayout.EnumPopup("상태", _stateEnum);
                if (GUILayout.Button("ChangeState"))
                {
                    _player.StateMachine.ChangeState(_stateEnum);
                }
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(_player);
            }
        }
    }
#endif
    [Header("movement settings")]
    public float moveSpeed = 12f;
    public float jumpForce = 12f;
    public float dashDuration = 0.4f;
    public float dashSpeed = 20f;
    [field: SerializeField] public float CoyoteTime { get; private set; }
    [HideInInspector] public float coyoteCounter;
    [HideInInspector] public bool stance;

    [Header("attack settings")]
    public float attackSpeed = 1f;
    public Vector2[] attackMovement;
    public float[] airAttackRising;
    [Range(0, 1)]
    public float airXMovementRatio;

    public Action onPickUpItem;

    [field: SerializeField] public InputReader PlayerInput { get; private set; }

    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerStat PlayerStat { get; private set; }

    [HideInInspector]
    public SkillManager Skill { get; private set; }

    public bool stopDebug;

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
        Debug.Log(Skill.gameObject);
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

        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            stopDebug = true;
        }
    }

    protected override void HandleKnockback(Vector2 direction)
    {
        if (!stance)
        {
            base.HandleKnockback(direction);
        }
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

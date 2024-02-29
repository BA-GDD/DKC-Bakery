using System;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

public enum FacingDirectionEnum { Right = 1, Left = -1}

public abstract class Entity : MonoBehaviour
{
    [CustomEditor(typeof(Entity), true)]
    public class EntityEditor : Editor
    {
        private Entity _entity;


        protected virtual void OnEnable()
        {
            _entity = (Entity)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var boldtext = new GUIStyle(GUI.skin.label);
            boldtext.fontStyle = FontStyle.Bold;

            EditorGUILayout.Space();

            _entity._showCheckCollision = EditorGUILayout.Foldout(_entity._showCheckCollision, "CheckCollision", true);
            if (_entity._showCheckCollision)
            {
                _entity._groundChecker = EditorGUILayout.ObjectField("Ground Checker", _entity._groundChecker, typeof(Transform), true) as Transform;
                _entity._groundCheckDistance = EditorGUILayout.FloatField("Ground Check Distance", _entity._groundCheckDistance);

                _entity._wallChecker = EditorGUILayout.ObjectField("Wall Checker", _entity._wallChecker, typeof(Transform), true) as Transform;
                _entity._wallCheckDistance = EditorGUILayout.FloatField("Wall Check Distance", _entity._wallCheckDistance);

                LayerMask tempMask = EditorGUILayout.MaskField("WhatIsGroundAndWall", InternalEditorUtility.LayerMaskToConcatenatedLayersMask(_entity._whatIsGroundAndWall), InternalEditorUtility.layers);
                _entity._whatIsGroundAndWall = InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(tempMask);
                EditorGUILayout.Space();
            }
            _entity._showKnockbackSetting = EditorGUILayout.Foldout(_entity._showKnockbackSetting, "Knockback Setting", true);
            if (_entity._showKnockbackSetting)
            {
                _entity._knockbackDuration = EditorGUILayout.FloatField("KnockbackDuration", _entity._knockbackDuration);
                _entity._isKnocked = EditorGUILayout.Toggle("Is Knocked", _entity._isKnocked);

                EditorGUILayout.Space();
            }
            _entity._showStunInfo = EditorGUILayout.Foldout(_entity._showStunInfo, "Stun Setting", true);
            if (_entity._showStunInfo)
            {
                _entity.stunDuration = EditorGUILayout.FloatField("StunDuration", _entity.stunDuration);
                _entity.stunDirection = EditorGUILayout.Vector2Field("StunDirection", _entity.stunDirection);

            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(_entity);
            }
        }
    }
    #region ������ ����
    [HideInInspector, SerializeField] private bool _showCheckCollision;
    [HideInInspector, SerializeField] private bool _showKnockbackSetting;
    [HideInInspector, SerializeField] private bool _showStunInfo;
    #endregion

    #region CheckCollision
    [HideInInspector, SerializeField] protected Transform _groundChecker;
    [HideInInspector, SerializeField] protected float _groundCheckDistance;
    [HideInInspector, SerializeField] protected Transform _wallChecker;
    [HideInInspector, SerializeField] protected float _wallCheckDistance;
    [HideInInspector, SerializeField] protected LayerMask _whatIsGroundAndWall;
    #endregion

    #region Knockback 
    [HideInInspector, SerializeField] protected float _knockbackDuration;
    [HideInInspector, SerializeField] protected bool _isKnocked;
    #endregion

    #region StunInfo
    [HideInInspector] public float stunDuration;
    [HideInInspector] public Vector2 stunDirection;
    protected bool _canBeStuned;
    #endregion 

    #region components
    public Animator AnimatorCompo { get; private set; }
    public Rigidbody2D RigidbodyCompo { get; private set; }
    public Health HealthCompo { get; private set; }
    public DamageCaster DamageCasterCompo { get; private set; }
    public SpriteRenderer SpriteRendererCompo { get; private set; }
    public CapsuleCollider2D Collider { get; private set; }

    [field: SerializeField] public CharacterStat CharStat { get; private set; }
    #endregion

    #region facing 
    [SerializeField] private FacingDirectionEnum facingDirection = FacingDirectionEnum.Right;
    public int FacingDirection { get { return (int)facingDirection; } private set { facingDirection = (FacingDirectionEnum)value; } } //�������� 1, ������ -1
    #endregion

    public UnityEvent<float, float> OnHealthBarChanged;
    public UnityEvent OnAttackEvent;//������ �� ����� �̺�Ʈ��
    public Action<int> OnStartAttack;
    public Action OnEndAttack;

    protected virtual void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<Animator>();
        RigidbodyCompo = GetComponent<Rigidbody2D>();
        HealthCompo = GetComponent<Health>();
        DamageCasterCompo = transform.Find("DamageCaster").GetComponent<DamageCaster>();
        SpriteRendererCompo = visualTrm.GetComponent<SpriteRenderer>();
        Collider = GetComponent<CapsuleCollider2D>();
        DamageCasterCompo.SetOwner(this); //�ڽ��� ���Ȼ� �������� �־���.
        HealthCompo.SetOwner(this);

        HealthCompo.OnKnockBack += HandleKnockback;
        HealthCompo.OnHit += HandleHit;
        HealthCompo.OnDeathEvent.AddListener(HandleDie);
        HealthCompo.OnAilmentChanged.AddListener(HandleAilmentChanged);
        OnHealthBarChanged?.Invoke(HealthCompo.GetNormalizedHealth(), HealthCompo.GetNormalizedHealth()); //�ִ�ġ�� UI����.

        CharStat = Instantiate(CharStat); //������ ����
        CharStat.SetOwner(this);
    }

    private void OnDestroy()
    {
        HealthCompo.OnKnockBack -= HandleKnockback;
        HealthCompo.OnHit -= HandleHit;
        HealthCompo.OnDeathEvent.RemoveListener(HandleDie);
        HealthCompo.OnAilmentChanged.RemoveListener(HandleAilmentChanged);
    }

    //���ῡ ���� ó��.
    private void HandleAilmentChanged(Ailment ailment)
    {
        if ((ailment & Ailment.Chilled) > 0) //������¸� ���ǵ� ������
        {
            //���� ���׿� ���� ����
            float resistance = (100 - CharStat.magicResistance.GetValue()) * 0.01f;
            SlowEntityBy(0.5f * resistance);
        }
        else
        {
            ReturnDefaultSpeed();
        }
    }

    protected virtual void HandleHit()
    {
        //UI����
        OnHealthBarChanged?.Invoke(HealthCompo.GetNormalizedHealth(), HealthCompo.GetNormalizedHealth());
    }

    protected virtual void HandleKnockback(Vector2 direction)
    {
        StartCoroutine(HitKnockback(direction));
    }

    protected abstract void HandleDie(Vector2 direction);


    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    public virtual void Attack()
    {
        if ((DamageCasterCompo?.CastDamage()).Value)
        {
            OnAttackEvent?.Invoke();
        }
    }

    protected virtual IEnumerator HitKnockback(Vector2 direction)
    {
        _isKnocked = true;
        RigidbodyCompo.velocity = direction;
        yield return new WaitForSeconds(_knockbackDuration);
        _isKnocked = false;
    }

    public abstract void SlowEntityBy(float percent); //���ο�� �ڽĵ��� ����.

    protected virtual void ReturnDefaultSpeed()
    {
        AnimatorCompo.speed = 1; //���� ���ǵ�� �ǵ�����.
    }

    #region flip control
    public virtual void Flip()
    {
        FacingDirection = FacingDirection * -1; //����
        transform.Rotate(0, 180, 0);
    }

    public virtual void FlipController(float x)
    {
        bool goToRight = x > 0 && FacingDirection < 0;
        bool goToLeft = x < 0 && FacingDirection > 0;

        if (goToRight || goToLeft)
        {
            Flip();
        }
    }
    #endregion

    #region velocity control
    public void SetVelocity(Vector2 velocity, bool doNotFlip = false)
    {
        SetVelocity(velocity.x, velocity.y, doNotFlip);
    }
    public void SetVelocity(float x, float y, bool doNotFlip = false)
    {
        RigidbodyCompo.velocity = new Vector2(x, y);
        if (!doNotFlip)
        {
            FlipController(x);
        }
    }

    public void StopImmediately(bool withYAxis)
    {
        if (withYAxis)
        {
            RigidbodyCompo.velocity = Vector2.zero;
        }
        else
        {
            RigidbodyCompo.velocity = new Vector2(0, RigidbodyCompo.velocity.y);
        }
    }
    #endregion


    #region Collision Check logic
    public virtual bool IsGroundDetected() =>
        Physics2D.Raycast(_groundChecker.position, Vector2.down, _groundCheckDistance,
                        _whatIsGroundAndWall);

    public virtual bool IsWallDetected() =>
        Physics2D.Raycast(_wallChecker.position, Vector2.right * FacingDirection,
                        _wallCheckDistance, _whatIsGroundAndWall);
    #endregion


    #region delay coroutine logic
    public void StartDelayAction(float delayTime, Action todoAction)
    {
        StartCoroutine(DelayAction(delayTime, todoAction));
    }

    protected IEnumerator DelayAction(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }
    #endregion

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        if (_groundChecker != null)
            Gizmos.DrawLine(_groundChecker.position, _groundChecker.position + new Vector3(0, -_groundCheckDistance, 0));
        if (_wallChecker != null)
            Gizmos.DrawLine(_wallChecker.position, _wallChecker.position + new Vector3(_wallCheckDistance * FacingDirection, 0, 0));
    }
#endif
}

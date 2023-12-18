using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour
{

    [Header("collision checker")]
    [SerializeField] protected Transform _groundChecker;
    [SerializeField] protected float _groundCheckDistance;
    [SerializeField] protected Transform _wallChecker;
    [SerializeField] protected float _wallCheckDistance;
    [SerializeField] protected LayerMask _whatIsGroundAndWall;

    [Header("Knockback info")]
    [SerializeField] protected float _knockbackDuration;
    protected bool _isKnocked;

    [Header("Stun Info")]
    public float stunDuration;
    public Vector2 stunDirection;
    protected bool _canBeStuned;

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
    public int FacingDirection { get; private set; } = 1; //�������� 1, ������ -1
    #endregion

    public UnityEvent<float> OnHealthBarChanged;

    protected virtual void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<Animator>();
        RigidbodyCompo = GetComponent<Rigidbody2D>();
        HealthCompo = GetComponent<Health>();
        DamageCasterCompo = transform.Find("DamageCaster").GetComponent<DamageCaster>();
        SpriteRendererCompo = visualTrm.GetComponent<SpriteRenderer>();
        Collider = GetComponent<CapsuleCollider2D>();
        DamageCasterCompo.SetOwner(this, castByCloneSkill: false); //�ڽ��� ���Ȼ� �������� �־���.
        HealthCompo.SetOwner(this);

        HealthCompo.OnKnockBack += HandleKnockback;
        HealthCompo.OnHit += HandleHit;
        HealthCompo.OnDeathEvent.AddListener(HandleDie);
        HealthCompo.OnAilmentChanged.AddListener(HandleAilmentChanged);
        OnHealthBarChanged?.Invoke(HealthCompo.GetNormalizedHealth()); //�ִ�ġ�� UI����.

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
        OnHealthBarChanged?.Invoke(HealthCompo.GetNormalizedHealth());
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
        DamageCasterCompo?.CastDamage();
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

        if(goToRight || goToLeft)
        {
            Flip();
        }
    }
    #endregion

    #region velocity control
    public void SetVelocity(float x, float y, bool doNotFlip = false)
    {
        RigidbodyCompo.velocity = new Vector2(x, y);
        if(!doNotFlip)
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
            Gizmos.DrawLine(_groundChecker.position, _groundChecker.position + new Vector3(0, - _groundCheckDistance, 0));
        if (_wallChecker != null)
            Gizmos.DrawLine(_wallChecker.position, _wallChecker.position + new Vector3(_wallCheckDistance, 0, 0));
    }
#endif
}

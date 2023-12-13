using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{

    [Header("collision checker")]
    [SerializeField] protected Transform _groundChecker;
    [SerializeField] protected float _groundCheckDistance;
    [SerializeField] protected Transform _wallChecker;
    [SerializeField] protected float _wallCheckDistance;
    [SerializeField] protected LayerMask _whatIsGroundAndWall;

    [Header("Stun Info")]
    public float stunDuration;
    public Vector2 stunDirection;
    protected bool _canBeStuned;

    #region components
    public Animator AnimatorCompo { get; private set; }
    public Rigidbody2D RigidbodyCompo { get; private set; }

    [field: SerializeField] public CharacterStat CharStat { get; private set; }
    [field: SerializeField] public Health HealthCompo { get; private set; }
    #endregion

    #region facing 
    public int FacingDirection { get; private set; } = 1; //오른쪽이 1, 왼쪽이 -1
    #endregion

    public UnityEvent<float> OnHealthBarChanged;

    protected virtual void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<Animator>();
        RigidbodyCompo = GetComponent<Rigidbody2D>();


        CharStat = Instantiate(CharStat); //복제본 생성
        CharStat.SetOwner(this);
    }

    protected virtual void Update()
    {

    }

    #region flip control
    public virtual void Flip()
    {
        FacingDirection = FacingDirection * -1; //반전
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

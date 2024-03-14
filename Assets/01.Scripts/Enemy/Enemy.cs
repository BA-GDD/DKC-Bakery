using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Sequence = DG.Tweening.Sequence;

public class Enemy : Entity
{
    [Header("셋팅값들")]
    public Transform hpBarPos;

    [SerializeField] protected LayerMask _whatIsPlayer;


    private int _attackAnimationHash = Animator.StringToHash("attack");
    private int _attackTriggerAnimationHash = Animator.StringToHash("attackTrigger");

    protected override void Awake()
    {
        base.Awake();
        OnAnimationCall += TakeDamage;
        target = GameManager.Instance.Player;

    }
    private void Start()
    {
        Vector3 endPos = target.transform.position - _camLookPath.transform.position;
        endPos.y = 1.3f;
        endPos.x += 1;
        _camLookPath.m_Waypoints[1].position = endPos;

        for(int i = 0; i < _camFollowPath.m_Waypoints.Length; i++)
        {
            float z = CameraController.Instance._defaultCVCam.transform.position.z - _camFollowPath.transform.position.z;
            _camFollowPath.m_Waypoints[i].position.z = z;
        }
    }
    private void Update()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            Attack();
        }
    }
    public void AnimationFinishTrigger()
    {
        OnAnimationEnd?.Invoke();
    }
    public override void SlowEntityBy(float percent)
    {

    }
    public void Attack()
    {
        AnimatorCompo.SetBool(_attackAnimationHash, true);
        MoveToTargetForward();
        OnAnimationEnd += () =>
        {
            MoveToLastPos();
            AnimatorCompo.SetBool(_attackAnimationHash, false);
            CameraController.Instance.SetDefaultCam();
            OnAnimationEnd = null;

        };
    }

    public override void MoveToTargetForward()
    {
        CameraController.Instance.SetFollowCam(this, _camFollowPath, _camLookObjCart.transform);

        lastMovePos = transform.position;
        Vector3 forwardPos = target.transform.position + Vector3.right * 2;
        _camLookPath.transform.SetParent(null);
        _camFollowPath.transform.SetParent(null);
            

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(forwardPos, moveDuration));
        seq.Join(DOTween.To(() => _camLookObjCart.m_Position, x => _camLookObjCart.m_Position = x, _camLookPath.PathLength, moveDuration));
        seq.Join(DOTween.To(() => CameraController.Instance.DollyPos, x => CameraController.Instance.DollyPos = x, 1, moveDuration));

        seq.OnComplete(() =>
        {
            AnimatorCompo.SetTrigger(_attackTriggerAnimationHash);
        });
    }
    public override void MoveToLastPos()
    {
        base.MoveToLastPos();
        transform.DOMove(lastMovePos, moveDuration).OnComplete(() => _turnEnd = true);
    }

    private void TakeDamage()
    {
        target.HealthCompo.ApplyDamage(CharStat.GetDamage(), this);
    }

    public override void TurnStart()
    {
    }
    public override void TurnAction()
    {
        Attack();
    }
    public override void TurnEnd()
    {
        _camLookPath.transform.SetParent(transform);
        _camFollowPath.transform.SetParent(transform);
        _turnEnd = false;
    }
}
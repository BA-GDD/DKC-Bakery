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
    [SerializeField] private CameraMoveTrack camTrack;

    private int _attackAnimationHash = Animator.StringToHash("attack");
    private int _attackTriggerAnimationHash = Animator.StringToHash("attackTrigger");

    protected override void Awake()
    {
        base.Awake();
        OnAnimationCall += TakeDamage;
        target = GameManager.Instance.Player;

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
        CameraController.Instance.SetFollowCam(camTrack.targetTrm,transform);
        camTrack.StartMove();
        lastMovePos = transform.position;
        camTrack.transform.SetParent(null);


        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(target.forwardTrm.position, moveDuration));
        seq.Join(DOVirtual.DelayedCall(0.25f,() => CameraController.Instance.SetFollowCam(camTrack.targetTrm, target.transform)));
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
        camTrack.transform.SetParent(transform);
        _turnEnd = false;
    }
}
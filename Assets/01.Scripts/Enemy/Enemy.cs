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
        //CameraController.Instance.SetFollowCam(transform, target.transform);

        lastMovePos = transform.position;
        Vector3 forwardPos = target.transform.position + Vector3.right * 2;
        Sequence seq = DOTween.Sequence();


        seq.Append(transform.DOMove(forwardPos, moveDuration));
        //seq.Join(transform.DOMove(forwardPos, moveDuration));

        seq.OnComplete(() =>
        {
            AnimatorCompo.SetTrigger(_attackTriggerAnimationHash);
        });
    }
    public override void MoveToLastPos()
    {
        base.MoveToLastPos();
        transform.DOMove(lastMovePos, moveDuration).OnComplete(()=> _turnEnd = true);
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
        _turnEnd = false;
    }
}
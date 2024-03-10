using DG.Tweening;
using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

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
        OnAnimationCall += ApplyDamage;
        target = GameManager.Instance.Player;
    }
    private void Update()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            Attack();
        }
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            ApplyDamageTest();
        }
    }
    public void AnimationFinishTrigger()
    {
        OnAnimationEnd?.Invoke();
    }
    public override void SlowEntityBy(float percent)
    {

    }
    [ContextMenu("AttackTest")]
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
        CameraController.Instance.SetFollowCam(transform, target.transform);

        lastMovePos = transform.position;
        transform.DOMoveX(target.transform.position.x - 2, moveDuration).OnComplete(() =>
        {
            AnimatorCompo.SetTrigger(_attackTriggerAnimationHash);
        });
    }

    private void ApplyDamage()
    {
        target.HealthCompo.ApplyDamage(CharStat.GetDamage(), this);
    }
    [ContextMenu("Test")]
    private void ApplyDamageTest()
    {
        HealthCompo.ApplyDamage(CharStat.GetDamage(), this);
    }
}
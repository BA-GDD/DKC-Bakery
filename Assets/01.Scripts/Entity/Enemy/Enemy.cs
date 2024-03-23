using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Sequence = DG.Tweening.Sequence;

public abstract class Enemy : Entity
{
    [Header("셋팅값들")]
    public Transform hpBarPos;

    [SerializeField] protected LayerMask _whatIsPlayer;
    [SerializeField] protected CameraMoveTrack camTrack;

    protected int attackAnimationHash = Animator.StringToHash("attack");
    protected int attackTriggerAnimationHash = Animator.StringToHash("attackTrigger");
    protected int spawnAnimationHash = Animator.StringToHash("spawn");

    public TurnStatus turnStatus;

    protected override void Awake()
    {
        base.Awake();
    }

    public void AnimationFinishTrigger()
    {
        OnAnimationEnd?.Invoke();
    }


    public abstract void Attack();

    public abstract void TurnStart();
    public abstract void TurnAction();
    public abstract void TurnEnd();

    public abstract void Spawn(Vector3 spawnPos);
    public void MoveFormation(Vector3 pos)
    {
        transform.DOMove(pos, 1f);
    }

    [ContextMenu("TurnStart")]
    private void TestTurnStart() => TurnStart();
    [ContextMenu("TurnAction")]
    private void TestTurnAction() => TurnAction();
    [ContextMenu("TurnEnd")]
    private void TestTurnEnd() => TurnEnd();
}
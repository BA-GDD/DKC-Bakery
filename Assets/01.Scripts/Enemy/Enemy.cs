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

    public void AnimationFinishTrigger()
    {
        OnAnimationEnd?.Invoke();
    }
    

    public abstract void Attack();

    public abstract void TurnStart();
    public abstract void TurnAction();

    public abstract void TurnEnd();
    
}
using System;
using UnityEngine;
using DG.Tweening;

public class Cream : MonoBehaviour
{
    public Animator animator;

    public Action OnAnimationCall;
    public Action OnAnimationEnd;

    public void InvokeAnimationCall()
    {
        OnAnimationCall?.Invoke();
    }
    public void InvokeAnimationEnd()
    {
        OnAnimationEnd?.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlontrolAnimationEvent : MonoBehaviour
{
    private Flontrol _flontrol;

    private void Awake()
    {
        _flontrol = transform.parent.GetComponent<Flontrol>();
    }

    public void InvokeAnimationEvent()
    {
        _flontrol.animationEvent?.Invoke();
    }
    public void EndAnimationEvent()
    {
        _flontrol.AnimationFinishTrigger();
    }
    public void AttackAreaEvent()
    {
        _flontrol.attackAreaEvent?.Invoke();
    }
}

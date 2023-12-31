using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    public static Action AnimationEvent;

    private Player _player;

    private void Awake()
    {
        _player = transform.parent.GetComponent<Player>();
    }

    private void AnimationEndTrigger()
    {
        _player.AnimationEndTrigger();
    }
    private void AnimationEventTrigger()
    {
        AnimationEvent?.Invoke();
    }
}

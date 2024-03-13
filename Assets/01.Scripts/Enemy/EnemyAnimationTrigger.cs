using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    private Enemy _enemy;

    protected virtual void Awake()
    {
        _enemy = transform.parent.GetComponent<Enemy>();
    }

    private void AnimationTrigger()
    {
        _enemy.AnimationFinishTrigger();
    }

    private void CallAnimationEvent()
    {
        _enemy.OnAnimationCall?.Invoke();
    }
}

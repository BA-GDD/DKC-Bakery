using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsEnemy;
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = transform.parent.GetComponent<Enemy>();
    }

    private void AnimationTrigger()
    {
        _enemy.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        _enemy.Attack();
    }
}

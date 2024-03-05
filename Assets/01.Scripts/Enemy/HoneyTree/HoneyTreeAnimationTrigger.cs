using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyTreeAnimationTrigger : EnemyAnimationTrigger
{
    private HoneyTree _honeyTree;
    protected override void Awake()
    {
        base.Awake();
        _honeyTree = transform.parent.GetComponent<HoneyTree>();
    }
    private void SpikeAttackTrigger()
    {
        _honeyTree.SpikeAttack();
    }
}

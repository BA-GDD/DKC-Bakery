using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class AttackCoolDownNode : ActionNode
    {
        [SerializeField] private float _cooldown;
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (Time.time > enemy.lastTimeAttacked + _cooldown)
                return State.SUCCESS;
            else
                return State.FAILURE;
        }
    }
}
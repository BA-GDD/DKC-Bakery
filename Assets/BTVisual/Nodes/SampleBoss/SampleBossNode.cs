using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public abstract class SampleBossNode : ActionNode
    {
        private int animationHash;
        [SerializeField] private string parametorName;

        protected virtual void OnEnable()
        {
            animationHash = Animator.StringToHash(parametorName);
        }
    }
}
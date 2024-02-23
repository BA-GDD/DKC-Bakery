using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class FlontrolSmiteAttack : FlontrolPattrenNode
    {
        private bool _isAlreadyActive;
        protected override void OnStart()
        {
            base.OnStart();
            enemy.animationEvent += Attack;
        }
        protected override void OnStop()
        {
            base.OnStop();

            enemy.animationEvent -= Attack;
        }
        protected override State OnUpdate()
        {
            if (_isAlreadyActive)
                return State.FAILURE;
            if(enemy.endAnimationTrigger)
            {
                _isAlreadyActive = true; 
                return State.SUCCESS;
            }
            return State.RUNNING;
        }
        private void Attack()
        {

        }
    }
}


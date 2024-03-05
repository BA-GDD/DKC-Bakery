using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class FlontrolBerserk : FlontrolPattrenNode
    {

        protected override void OnStart()
        {
            base.OnStart();
            enemy.flower.SetActive(false);
            enemy.animationEvent += Berserk;
        }
        protected override void OnStop()
        {
            base.OnStop();
            enemy.animationEvent -= Berserk;
        }

        protected override State OnUpdate()
        {
            if (enemy.endAnimationTrigger)
            {
                return State.SUCCESS;
            }
            return State.RUNNING;
        }
        private void Berserk()
        {
            enemy.Berserk();
        }
    }
}

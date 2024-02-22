using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class FlontrolPrimaryAttack2_1 : FlontrolPattrenNode
    {
        protected override void OnStart()
        {
            base.OnStart();

            enemy.animationEvent += Attack;

        }

        protected override void OnStop()
        {
            base.OnStop();


            enemy.animationEvent -= Attack;

            enemy.lastTimeAttacked = Time.time;
        }

        protected override State OnUpdate()
        {
            if (enemy.endAnimationTrigger)
            {

                return State.SUCCESS;
            }
            return State.RUNNING;
        }
        public void Attack()
        {

        }
    }
}

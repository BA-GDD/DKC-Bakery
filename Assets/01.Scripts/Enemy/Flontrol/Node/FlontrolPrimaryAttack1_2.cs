using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class FlontrolPrimaryAttack1_2 : FlontrolPattrenNode
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
        private void Attack()
        {
            enemy.clapWave.Wave();
        }

    }
}
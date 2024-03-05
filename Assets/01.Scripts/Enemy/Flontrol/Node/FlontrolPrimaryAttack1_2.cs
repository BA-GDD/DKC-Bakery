using System;
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
            enemy.attackAreaEvent += SetArea;
        }

        protected override void OnStop()
        {
            base.OnStop();

            enemy.animationEvent -= Attack;
            enemy.attackAreaEvent -= SetArea;
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

        private void SetArea()
        {
            attackArea.Show();
        }

        private void Attack()
        {
            attackArea.End();
            enemy.clapWave.Wave();
        }

    }
}
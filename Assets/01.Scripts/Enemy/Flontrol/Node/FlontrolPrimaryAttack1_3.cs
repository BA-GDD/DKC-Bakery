using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class FlontrolPrimaryAttack1_3 : FlontrolPattrenNode
    {
        private int _callAnimEventCnt;
        protected override void OnStart()
        {
            base.OnStart();
            enemy.animationEvent += Attack;
            _callAnimEventCnt = 0;
        }

        protected override void OnStop()
        {
            base.OnStop();
            enemy.animationEvent -= Attack;
            enemy.lastTimeAttacked = Time.time;
        }

        protected override State OnUpdate()
        {
            if (_callAnimEventCnt == 1)
            {
                foreach (var d in enemy.leftArmDamageCast)
                {
                    d.CastDamage();
                }
            }
            else if (_callAnimEventCnt == 3)
            {
                foreach (var d in enemy.rightArmDamageCast)
                {
                    d.CastDamage();
                }
            }

            if (enemy.endAnimationTrigger)
            {

                return State.SUCCESS;
            }
            return State.RUNNING;
        }
        public void Attack()
        {
            _callAnimEventCnt++;
        }
    }
}
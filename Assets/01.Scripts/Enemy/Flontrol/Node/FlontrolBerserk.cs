using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class FlontrolBerserk : FlontrolPattrenNode
    {
        private bool _isAlreadyActive;

        private int _pattenCnt;
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
            if(_isAlreadyActive)
            {
                return State.FAILURE;
            }

            if (enemy.endAnimationTrigger)
            {
                _isAlreadyActive = true;
                return State.SUCCESS;
            }
            return State.RUNNING;
        }
        private void Berserk()
        {
            if(_pattenCnt < 3)
            {
                enemy.berserkAttackDamageCaster[_pattenCnt].CastDamage();
            }
            else
            {
                enemy.Berserk();
            }

            _pattenCnt++;
        }
    }
}

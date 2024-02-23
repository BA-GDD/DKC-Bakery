using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class FlontrolPrimaryAttack2_1 : FlontrolPattrenNode
    {
        private int _invokeCnt;
        private bool _leftAttack;

        protected override void OnStart()
        {
            base.OnStart();

            float playerX = GameManager.Instance.PlayerTrm.position.x;
            _leftAttack = playerX > 0;
            enemy.animationEvent += Attack;
        }

        protected override void OnStop()
        {
            base.OnStop();

            _invokeCnt = 0;
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
            if(_invokeCnt < 2)
                enemy.seqAttackDamageCaster[_leftAttack ? 0 : 1].CastDamage();
            else
                enemy.seqAttackDamageCaster[2].CastDamage();

            _leftAttack = !_leftAttack;
        }
    }
}

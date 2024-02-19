using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class FlontrolPrimaryAttack1_1 : FlontrolPattrenNode
    {
        private int _spikeType;
        protected override void OnStart()
        {
            base.OnStart();

            enemy.animationEvent += Attack;

            _spikeType = Random.Range(0, enemy.spikePatten.Count);
            foreach (var spike in enemy.spikePatten[_spikeType].spikes)
            {
                spike.gameObject.SetActive(true);
            }
        }

        protected override void OnStop()
        {
            base.OnStop();


            enemy.animationEvent -= Attack;

            foreach (var spike in enemy.spikePatten[_spikeType].spikes)
            {
                spike.gameObject.SetActive(false);
            }

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
            foreach (var spike in enemy.spikePatten[_spikeType].spikes)
            {
                spike.Attack();
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class FlontrolPrimaryAttack1_1 : FlontrolPattrenNode
    {
        private int _spikeType;

        private int _invokeCnt;
        protected override void OnStart()
        {
            base.OnStart();

            enemy.animationEvent += Attack;

            _spikeType = Random.Range(0, enemy.spikePatten.Count);

        }

        protected override void OnStop()
        {
            base.OnStop();


            enemy.animationEvent -= Attack;
            _invokeCnt = 0;

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
            switch (_invokeCnt)
            {
                case 0:
                    foreach (var spike in enemy.spikePatten[_spikeType].spikes)
                    {
                        spike.gameObject.SetActive(true);
                    }
                    break;
                case 1:
                    foreach (var spike in enemy.spikePatten[_spikeType].spikes)
                    {
                        spike.Attack();
                    }
                    break;
                default:
                    break;
            }
            _invokeCnt++;
        }
    }
}
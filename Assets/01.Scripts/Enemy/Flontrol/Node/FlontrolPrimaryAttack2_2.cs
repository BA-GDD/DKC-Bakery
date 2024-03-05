using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class FlontrolPrimaryAttack2_2 : FlontrolPattrenNode
    {
        private int _invokeCnt;

        private int _spikeType;

        private Queue<FlontrolSpike> spawnSpikes = new Queue<FlontrolSpike>();

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
                    enemy.StartCoroutine(SpawnChaseSpike());
                    break;
                case 1:
                    enemy.StartCoroutine(AttackChaseSpike());
                    break;
                case 2:
                    foreach (var spike in enemy.spikePatten[_spikeType].spikes)
                    {
                        spike.gameObject.SetActive(true);
                    }
                    break;
                case 3:
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

        private IEnumerator SpawnChaseSpike()
        {
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(0.1f);
                FlontrolSpike spike = PoolManager.Instance.Pop(PoolingType.FlontrolSpike) as FlontrolSpike;
                spike.Bind(enemy);
                spike.transform.position = enemy.IsGroundDetectedByPlayer(GameManager.Instance.PlayerTrm.position).point;
                spawnSpikes.Enqueue(spike);
            }
        }
        private IEnumerator AttackChaseSpike()
        {
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(0.1f);
                FlontrolSpike spike = spawnSpikes.Dequeue();
                spike.Attack(true);
            }
        }
    }
}

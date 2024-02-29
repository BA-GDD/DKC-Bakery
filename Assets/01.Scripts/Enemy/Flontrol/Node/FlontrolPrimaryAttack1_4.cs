using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class FlontrolPrimaryAttack1_4 : FlontrolPattrenNode
    {
        private int _shotCnt;
        protected override void OnStart()
        {
            base.OnStart();

            _shotCnt = Random.Range(3, 6);

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
            float angleDiff = 180 / (_shotCnt + 1);
            for (int i = 0; i < _shotCnt; i++)
            {
                FlowerBullet bullet = PoolManager.Instance.Pop(PoolingType.FlowerBullet) as FlowerBullet;
                bullet.transform.position = enemy.flowerShotTransfom.position;
                float angle = Mathf.Deg2Rad * (angleDiff * (i + 1) + 180);
                Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                bullet.SetOwner(enemy, dir);
            }


        }
    }
}